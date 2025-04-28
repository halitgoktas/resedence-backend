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
using ResidenceManagement.Infrastructure.Extensions;

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

// JWT Authentication servislerini ekle
builder.Services.AddJwtAuthentication(builder.Configuration);

// Güvenlik headerlarını ekle
builder.Services.AddSecurityHeaders();

// CORS servislerini ekle
builder.Services.AddCorsPolicy(builder.Configuration);

// Infrastructure servislerini kaydet
// Tam namespace kullanarak belirsizliği gideriyoruz
ResidenceManagement.Infrastructure.ServicesRegistration.AddInfrastructureServices(builder.Services, builder.Configuration);

// Swagger servislerini ekle
builder.Services.AddSwaggerServices();

var app = builder.Build();

// API versiyon sağlayıcısını al
var apiVersionDescriptionProvider = app.Services.GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();

// Middleware pipeline'ı yapılandır
app.ConfigureMiddlewarePipeline(apiVersionDescriptionProvider, app.Environment);

app.Run();
