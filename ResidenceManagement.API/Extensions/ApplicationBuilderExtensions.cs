using Microsoft.AspNetCore.Builder;
using ResidenceManagement.API.Middlewares;

namespace ResidenceManagement.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
        
        public static IApplicationBuilder UseApiRateLimiting(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RateLimitingMiddleware>();
        }
    }
} 