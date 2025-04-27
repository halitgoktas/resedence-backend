using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ResidenceManagement.Infrastructure.Services;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Repositories;
using ResidenceManagement.Infrastructure.Logging;

namespace ResidenceManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<ResidenceManagement.Core.Interfaces.IMaintenanceService, MaintenanceService>();
            
            // Tam nitelikli isim kullanarak
            services.AddScoped<ResidenceManagement.Core.Interfaces.Services.IMaintenanceScheduleService, MaintenanceScheduleService>();
            
            // Loglama servislerini ekle
            services.AddLoggingServices(configuration);
            
            // Diğer servisler...
            
            return services;
        }
    }
} 