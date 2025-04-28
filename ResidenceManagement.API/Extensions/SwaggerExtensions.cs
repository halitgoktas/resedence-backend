using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// Swagger yapılandırması için extension metodları
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Swagger servislerini ekler
        /// </summary>
        /// <param name="services">Servis koleksiyonu</param>
        /// <returns>Güncellenen servis koleksiyonu</returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // API Versiyonlaması için Swagger yapılandırması
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = $"Residence Management API {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "Residence Management API"
                        });
                }
                
                // Token için güvenlik tanımları
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// Swagger middleware'i yapılandırır
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        /// <param name="provider">API versiyon açıklama sağlayıcısı</param>
        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Her API versiyonu için bir endpoint oluşturuluyor
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"Residence Management API {description.ApiVersion}");
                }
            });
        }
    }
} 