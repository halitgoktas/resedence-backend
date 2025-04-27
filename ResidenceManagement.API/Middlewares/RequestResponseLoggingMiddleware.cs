using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResidenceManagement.Core.Interfaces.Logging;

namespace ResidenceManagement.API.Middlewares
{
    /// <summary>
    /// HTTP isteklerini ve yanıtlarını loglamak için middleware
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestResponseLoggingService _loggingService;

        /// <summary>
        /// RequestResponseLoggingMiddleware constructor
        /// </summary>
        /// <param name="next">Request delegate</param>
        /// <param name="loggingService">Request/response loglama servisi</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, IRequestResponseLoggingService loggingService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }

        /// <summary>
        /// Middleware çalıştırma metodu
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <returns>Asenkron task</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // İsteği logla
            await _loggingService.LogRequestAsync(context);

            // Yanıtı yakalamak için response body stream'ı değiştiriyoruz
            var originalBodyStream = context.Response.Body;

            try
            {
                // Sonraki middleware'i çağır
                await _next(context);
                
                // Yanıtı logla
                await _loggingService.LogResponseAsync(context);
            }
            finally
            {
                // Orijinal body stream'i geri yükle
                context.Response.Body = originalBodyStream;
            }
        }
    }

    /// <summary>
    /// RequestResponseLoggingMiddleware için extension metotları
    /// </summary>
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        /// <summary>
        /// Request/response loglama middleware'ini uygulama pipeline'ına ekler
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder UseCustomRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
} 