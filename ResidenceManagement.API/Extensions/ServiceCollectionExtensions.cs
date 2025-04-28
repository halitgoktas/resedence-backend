using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ResidenceManagement.Core.Models.Authentication;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// API katmanı için servis koleksiyon extension metodları
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// JWT authentication servislerini ekler
        /// </summary>
        /// <param name="services">Servis koleksiyonu</param>
        /// <param name="configuration">Konfigurasyon</param>
        /// <returns>Güncellenen servis koleksiyonu</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Authentication Yapılandırması
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            // JWT authentication parametrelerini al
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            // JWT Authentication Servisini Ekle
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true; // Güvenlik için HTTPS zorunlu
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

            return services;
        }

        /// <summary>
        /// CORS servislerini yapılandırır
        /// </summary>
        /// <param name="services">Servis koleksiyonu</param>
        /// <param name="configuration">Konfigurasyon</param>
        /// <returns>Güncellenen servis koleksiyonu</returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = configuration.GetSection("CorsSettings");
            var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
            var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>();
            var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>();
            var exposedHeaders = corsSettings.GetSection("ExposedHeaders").Get<string[]>();
            var allowCredentials = corsSettings.GetValue<bool>("AllowCredentials");
            var maxAge = corsSettings.GetValue<int>("MaxAge");

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    // AllowAnyOrigin yerine belirli originler kullanılmalı
                    var policyBuilder = policy.WithOrigins(allowedOrigins ?? new[] { "https://localhost:4200", "https://residencemanagement.com" });
                    
                    // Sadece gerekli metodlara izin veriliyor
                    if (allowedMethods != null && allowedMethods.Length > 0)
                        policyBuilder.WithMethods(allowedMethods);
                    else
                        policyBuilder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                        
                    // Sadece gerekli headerlara izin veriliyor
                    if (allowedHeaders != null && allowedHeaders.Length > 0)
                        policyBuilder.WithHeaders(allowedHeaders);
                    else
                        policyBuilder.WithHeaders("Content-Type", "Authorization", "Accept");
                        
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

            return services;
        }

        /// <summary>
        /// Güvenlik header'larını ekler
        /// </summary>
        /// <param name="services">Servis koleksiyonu</param>
        /// <returns>Güncellenen servis koleksiyonu</returns>
        public static IServiceCollection AddSecurityHeaders(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            return services;
        }
    }
} 