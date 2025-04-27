using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ResidenceManagement.Infrastructure.Services;

namespace ResidenceManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<ResidenceManagement.Core.Interfaces.IMaintenanceService, MaintenanceService>();
            
            // Tam nitelikli isim kullanarak
            services.AddScoped<ResidenceManagement.Core.Interfaces.Services.IMaintenanceScheduleService, MaintenanceScheduleService>();
            
            // Diğer servisler...
            
            return services;
        }
    }
} 