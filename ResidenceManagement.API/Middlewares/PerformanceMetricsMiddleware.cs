using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResidenceManagement.Core.Interfaces.Logging;

namespace ResidenceManagement.API.Middlewares
{
    /// <summary>
    /// API isteklerinin performansını ölçmek için middleware
    /// </summary>
    public class PerformanceMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _logger;

        /// <summary>
        /// PerformanceMetricsMiddleware constructor
        /// </summary>
        /// <param name="next">Request delegate</param>
        /// <param name="logger">Loglama servisi</param>
        public PerformanceMetricsMiddleware(RequestDelegate next, ILoggingService logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Middleware çalıştırma metodu
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <returns>Asenkron task</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint()?.DisplayName;
            var path = context.Request.Path;
            var method = context.Request.Method;
            var operationName = $"{method} {path}";

            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Bir sonraki middleware'i çağır
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var statusCode = context.Response.StatusCode;

                // Performans metriğini logla
                var logMessage = $"Request: {operationName} | Endpoint: {endpoint} | Status: {statusCode} | Duration: {elapsedMilliseconds} ms";
                
                // Kritik performans eşiğini tanımla (örneğin 1 saniye)
                if (elapsedMilliseconds > 1000)
                {
                    _logger.Warning(logMessage);
                }
                else
                {
                    _logger.Info(logMessage);
                }
            }
        }
    }

    /// <summary>
    /// PerformanceMetricsMiddleware için extension metotları
    /// </summary>
    public static class PerformanceMetricsMiddlewareExtensions
    {
        /// <summary>
        /// Performans metriklerini toplayan middleware'i uygulama pipeline'ına ekler
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder UsePerformanceMetrics(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PerformanceMetricsMiddleware>();
        }
    }
} 