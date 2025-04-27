using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Interfaces.Logging;
using Serilog;
using Serilog.Events;

namespace ResidenceManagement.Infrastructure.Logging
{
    /// <summary>
    /// Loglama servislerini DI container'a kaydetmek için extension metotlar
    /// </summary>
    public static class LoggingServiceExtensions
    {
        /// <summary>
        /// Loglama servislerini servislere ekler
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Konfigürasyon</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Serilog'u konfigürasyondan yükle
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            // Serilog'u statik Log sınıfına ata
            Log.Logger = logger;

            // Serilog'un logger'ını singleton olarak DI container'a kaydet
            services.AddSingleton(logger);

            // Loglama servislerini kaydet
            services.AddSingleton<ILoggingService, SerilogService>();
            services.AddScoped<IRequestResponseLoggingService, RequestResponseLoggingService>();

            return services;
        }
    }
} 