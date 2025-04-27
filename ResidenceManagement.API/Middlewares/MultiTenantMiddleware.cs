using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.API.Middlewares
{
    // Multi-tenant yapısı için HTTP isteklerinden tenant (firma ve şube) bilgilerini alan middleware
    public class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MultiTenantMiddleware> _logger;
        
        public MultiTenantMiddleware(RequestDelegate next, ILogger<MultiTenantMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // JWT token'dan veya header'dan tenant bilgilerini al
                int firmaId = GetFirmaIdFromContext(context);
                int subeId = GetSubeIdFromContext(context);
                
                if (firmaId > 0 && subeId > 0)
                {
                    // DbContext'e firma ve şube ID değerlerini ayarla
                    var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                    dbContext.SetFirmaAndSubeId(firmaId, subeId);
                    
                    _logger.LogDebug("Tenant bilgileri ayarlandı: FirmaId={FirmaId}, SubeId={SubeId}", firmaId, subeId);
                }
                else
                {
                    // Public endpoint'ler için tenant filtresini devre dışı bırak
                    var pathValue = context.Request.Path.Value?.ToLower();
                    if (IsPublicEndpoint(pathValue))
                    {
                        var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                        dbContext.SetMultiTenantFilterEnabled(false);
                        
                        _logger.LogDebug("Public endpoint için tenant filtresi devre dışı bırakıldı: {Path}", pathValue);
                    }
                    else
                    {
                        _logger.LogWarning("Tenant bilgileri alınamadı: Path={Path}", context.Request.Path);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tenant bilgileri alınırken hata oluştu");
                // Hatayı yutma, middleware pipeline'ı devam ettir
            }
            
            // Sonraki middleware'e geç
            await _next(context);
        }
        
        private int GetFirmaIdFromContext(HttpContext context)
        {
            // 1. Header'dan kontrol et
            if (context.Request.Headers.TryGetValue("X-FirmaId", out var firmaIdHeader) && 
                int.TryParse(firmaIdHeader, out int firmaIdFromHeader))
            {
                return firmaIdFromHeader;
            }
            
            // 2. Claim'lerden kontrol et (JWT token)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var firmaIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "FirmaId");
                if (firmaIdClaim != null && int.TryParse(firmaIdClaim.Value, out int firmaIdFromClaim))
                {
                    return firmaIdFromClaim;
                }
            }
            
            // 3. Query string'den kontrol et (Debug/geliştirme için)
            if (context.Request.Query.TryGetValue("firmaId", out var firmaIdQuery) && 
                int.TryParse(firmaIdQuery, out int firmaIdFromQuery))
            {
                return firmaIdFromQuery;
            }
            
            return 0;
        }
        
        private int GetSubeIdFromContext(HttpContext context)
        {
            // 1. Header'dan kontrol et
            if (context.Request.Headers.TryGetValue("X-SubeId", out var subeIdHeader) && 
                int.TryParse(subeIdHeader, out int subeIdFromHeader))
            {
                return subeIdFromHeader;
            }
            
            // 2. Claim'lerden kontrol et (JWT token)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var subeIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "SubeId");
                if (subeIdClaim != null && int.TryParse(subeIdClaim.Value, out int subeIdFromClaim))
                {
                    return subeIdFromClaim;
                }
            }
            
            // 3. Query string'den kontrol et (Debug/geliştirme için)
            if (context.Request.Query.TryGetValue("subeId", out var subeIdQuery) && 
                int.TryParse(subeIdQuery, out int subeIdFromQuery))
            {
                return subeIdFromQuery;
            }
            
            return 0;
        }
        
        private bool IsPublicEndpoint(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            
            // Public endpoint'leri tanımla
            string[] publicEndpoints = new[]
            {
                "/api/auth/login",
                "/api/auth/register",
                "/api/auth/forgot-password",
                "/api/auth/reset-password",
                "/health",
                "/swagger"
            };
            
            return publicEndpoints.Any(endpoint => path.StartsWith(endpoint, StringComparison.OrdinalIgnoreCase));
        }
    }
} 