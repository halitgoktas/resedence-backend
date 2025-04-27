using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ResidenceManagement.Core.Services;
using ResidenceManagement.Core.Models.Authentication;
using ResidenceManagement.Infrastructure.Services;
using ResidenceManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.API.Extensions;
using ResidenceManagement.API.Middlewares;
using Serilog;
using ResidenceManagement.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Serilog yapılandırması
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// FluentValidation entegrasyonu
builder.Services.AddFluentValidationServices();
builder.Services.ConfigureValidation();

// API Versiyonlama ekle
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Rate Limiting için Distributed Cache servisi ekle
builder.Services.AddDistributedMemoryCache();

// Rate Limiting yapılandırmasını ekle
builder.Services.Configure<RateLimitingOptions>(builder.Configuration.GetSection("RateLimiting"));

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// JWT Authentication Yapılandırması
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

// JWT authentication parametrelerini al
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

// JWT Authentication Servisini Ekle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Prod ortamında true yapılmalı
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// CORS Yapılandırması
var corsSettings = builder.Configuration.GetSection("CorsSettings");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>();
var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>();
var exposedHeaders = corsSettings.GetSection("ExposedHeaders").Get<string[]>();
var allowCredentials = corsSettings.GetValue<bool>("AllowCredentials");
var maxAge = corsSettings.GetValue<int>("MaxAge");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        var policyBuilder = policy.WithOrigins(allowedOrigins ?? new[] { "*" });
        
        if (allowedMethods != null && allowedMethods.Length > 0)
            policyBuilder.WithMethods(allowedMethods);
        else
            policyBuilder.AllowAnyMethod();
            
        if (allowedHeaders != null && allowedHeaders.Length > 0)
            policyBuilder.WithHeaders(allowedHeaders);
        else
            policyBuilder.AllowAnyHeader();
            
        if (exposedHeaders != null && exposedHeaders.Length > 0)
            policyBuilder.WithExposedHeaders(exposedHeaders);
            
        if (allowCredentials)
            policyBuilder.AllowCredentials();
        else
            policyBuilder.DisallowCredentials();
            
        if (maxAge > 0)
            policyBuilder.SetPreflightMaxAge(TimeSpan.FromSeconds(maxAge));
    });
});

// Infrastructure servislerini kaydet
builder.Services.RegisterInfrastructureServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // API Versiyonlaması için Swagger yapılandırması
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();
    
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerDoc(
            description.GroupName,
            new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = $"Residence Management API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Residence Management API"
            });
    }
    
    // Token için güvenlik tanımları
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();
        
        // Her API versiyonu için bir endpoint oluşturuluyor
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Residence Management API {description.ApiVersion}");
        }
    });
}

// Global Exception Handler'ı pipeline'a ekle
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

// Request/Response Loglama Middleware'ini Ekle
app.UseCustomRequestResponseLogging();

// CORS Middleware'ini Ekle
app.UseCors("CorsPolicy");

// Rate Limiting Middleware'ini Ekle
app.UseApiRateLimiting();

// Authentication Middleware'ini Ekle
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
