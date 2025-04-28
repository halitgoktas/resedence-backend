using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Core.Services;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Infrastructure.Data.Repositories;
using ResidenceManagement.Infrastructure.Services;
using ResidenceManagement.Core.Interfaces.Services;

namespace ResidenceManagement.Infrastructure.Extensions
{
    /// <summary>
    /// IServiceCollection için extension metotları
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Infrastructure katmanı servislerini kaydeder
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        /// <returns>Servis koleksiyonu</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext konfigürasyonu
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            // Repository ve UnitOfWork kayıtları
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Servis kayıtları
            services.AddScoped<ResidenceManagement.Core.Interfaces.IMaintenanceService, MaintenanceService>();
            services.AddScoped<ResidenceManagement.Core.Interfaces.Services.IMaintenanceScheduleService, MaintenanceScheduleService>();
            services.AddScoped<IEquipmentInventoryService, EquipmentInventoryService>();
            
            // KBS entegrasyon servisleri
            services.AddScoped<IKbsSettingsService, KbsSettingsService>();

            return services;
        }
    }
} 