using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// ApplicationBuilder için extension metodları
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Middleware pipeline'ı yapılandırır
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        /// <param name="provider">API versiyon açıklama sağlayıcısı</param>
        /// <param name="environment">Hosting ortamı</param>
        public static void ConfigureMiddlewarePipeline(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, IHostEnvironment environment)
        {
            // Development ortamında Swagger'ı aktifleştir
            if (environment.IsDevelopment())
            {
                app.UseSwaggerConfiguration(provider);
            }

            // Global Exception Handler'ı pipeline'a ekle
            app.UseGlobalExceptionHandler();

            app.UseHttpsRedirection();

            // Security headerları ekle
            app.UseHsts();

            // Request/Response Loglama Middleware'ini Ekle
            // UseCustomRequestResponseLogging extension metodu bulunmadığı için yorum satırı haline getiriyorum
            // app.UseCustomRequestResponseLogging();
            // RequestResponseLoggingMiddleware sınıfı bulunamadığı için yorum satırına alıyorum
            // app.UseMiddleware<RequestResponseLoggingMiddleware>();

            // CORS Middleware'ini Ekle
            app.UseCors("CorsPolicy");

            // Performans metriklerini toplayan middleware'i ekle
            // UsePerformanceMetrics extension metodu bulunmadığı için yorum satırı haline getiriyorum
            // app.UsePerformanceMetrics();
            // PerformanceMetricsMiddleware sınıfı bulunamadığı için yorum satırına alıyorum
            // app.UseMiddleware<PerformanceMetricsMiddleware>();

            // Rate Limiting Middleware'ini Ekle
            app.UseApiRateLimiting();

            // Authentication Middleware'ini Ekle
            app.UseAuthentication();
            app.UseAuthorization();

            // MapControllers metodu IEndpointRouteBuilder üzerinde bulunur, IApplicationBuilder üzerinde değil
            // Bu yüzden UseEndpoints kullanıyoruz
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
} 