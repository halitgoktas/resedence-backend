using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Infrastructure.Data.Context;
using System;
using System.Threading.Tasks;

namespace ResidenceManagement.Infrastructure.Data.Seed.Extensions
{
    public static class ApplyMigrationsExtension
    {
        public static async Task ApplyMigrationsAsync(this IServiceProvider serviceProvider, ILogger logger)
        {
            try
            {
                logger.LogInformation("Veritabanı migrasyonları uygulanıyor...");

                using (var scope = serviceProvider.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // SQL Server veritabanı için migrasyon uygula
                    if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                        logger.LogInformation("Veritabanı migrasyonları başarıyla uygulandı.");
                    }
                    else
                    {
                        logger.LogWarning("SQL Server veritabanı kullanılmıyor, migrasyonlar atlandı.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Veritabanı migrasyonları uygulanırken bir hata oluştu.");
                throw;
            }
        }
    }
} 