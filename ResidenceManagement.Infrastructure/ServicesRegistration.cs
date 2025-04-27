using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Core.Services;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Infrastructure.Data.Repositories;
using ResidenceManagement.Infrastructure.Services;
using System;

namespace ResidenceManagement.Infrastructure
{
    // Infrastructure katmanı servis kayıtları
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext konfigürasyonu
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            // Unit of Work pattern kaydı
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Servis kayıtları
            services.AddScoped<IEquipmentInventoryService, EquipmentInventoryService>();

            // Servis ömürleri için IServiceCollection'a extension method
            // Gerektiğinde burada diğer infrastructure servisleri eklenebilir

            return services;
        }

        // Multi-tenant için firma ve şube ID değerlerini DbContext'e ayarlamak için
        public static void ConfigureMultiTenant(this IServiceProvider serviceProvider, int firmaId, int subeId)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.SetFirmaAndSubeId(firmaId, subeId);
        }
    }
} 