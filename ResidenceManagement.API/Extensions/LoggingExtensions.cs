using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// Loglama yapılandırması için uzantı metotları
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Serilog yapılandırmasını oluşturur
        /// </summary>
        public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
        {
            // Log klasörünü oluştur (yoksa)
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            // Serilog yapılandırması - appsettings.json'dan alınıyor fakat ek ayarlar da ekleniyor
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithProperty("ApplicationName", "ResidenceManagement")
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    Path.Combine(logDirectory, "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    Path.Combine(logDirectory, "log-.json"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30
                )
                .CreateLogger();

            // Global logger'ı ayarla
            Log.Logger = logger;

            // Host'u Serilog ile yapılandır
            builder.Host.UseSerilog();

            return builder;
        }

        /// <summary>
        /// Request-Response loglama middleware'ini ekler
        /// </summary>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
                
                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
                
                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
                    
                    if (httpContext.Request.Headers.TryGetValue("User-Agent", out var userAgent))
                    {
                        diagnosticContext.Set("UserAgent", userAgent.ToString());
                    }
                    
                    if (httpContext.User.Identity?.IsAuthenticated == true)
                    {
                        diagnosticContext.Set("UserId", httpContext.User.FindFirst("sub")?.Value);
                        diagnosticContext.Set("UserName", httpContext.User.FindFirst("name")?.Value);
                    }
                };
            });
        }

        /// <summary>
        /// Performans metriklerini loglama middleware'ini ekler
        /// </summary>
        public static IApplicationBuilder UsePerformanceLogging(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                // Başlangıç zamanını al
                var startTime = DateTime.UtcNow;
                
                // Request büyüklüğünü kaydet
                var requestSize = 0L;
                if (context.Request.ContentLength.HasValue)
                {
                    requestSize = context.Request.ContentLength.Value;
                }
                
                // Response büyüklüğünü ölçmek için orijinal body'yi kaydet ve yeni bir MemoryStream ile değiştir
                var originalResponseBody = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                
                try
                {
                    // Sonraki middleware'leri çağır
                    await next.Invoke();
                    
                    // Bitiş zamanını al ve performans metriklerini logla
                    var endTime = DateTime.UtcNow;
                    var elapsedTime = endTime - startTime;
                    
                    // Response büyüklüğünü hesapla
                    var responseSize = responseBody.Length;
                    
                    // Performans metriklerini logla
                    Log.Information(
                        "Performance: {RequestMethod} {RequestPath} - Duration: {ElapsedMilliseconds} ms, Request Size: {RequestSize} bytes, Response Size: {ResponseSize} bytes",
                        context.Request.Method,
                        context.Request.Path,
                        elapsedTime.TotalMilliseconds,
                        requestSize,
                        responseSize
                    );
                    
                    // Response'u orijinal stream'e kopyala
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalResponseBody);
                }
                finally
                {
                    // Orijinal response body'yi geri yükle
                    context.Response.Body = originalResponseBody;
                }
            });
        }
    }
} 