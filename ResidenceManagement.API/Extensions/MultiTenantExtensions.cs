using Microsoft.AspNetCore.Builder;
using ResidenceManagement.API.Middlewares;

namespace ResidenceManagement.API.Extensions
{
    // Multi-tenant middleware'i sisteme eklemek için extension metodları
    public static class MultiTenantExtensions
    {
        // Uygulama builder'a multi-tenant middleware'i ekler
        public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
        {
            // MultiTenantMiddleware'i pipeline'a ekle
            return app.UseMiddleware<MultiTenantMiddleware>();
        }
    }
} 