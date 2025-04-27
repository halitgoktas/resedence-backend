using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Data.Migrations
{
    /// <summary>
    /// IHost için migrasyon extension metotları
    /// </summary>
    public static class MigrationExtensions
    {
        /// <summary>
        /// Program.cs içinde kullanılacak, uygulama başlatılmadan önce migrasyonları uygular
        /// </summary>
        public static IHost MigrateDatabase(this IHost host)
        {
            MigrationManager.MigrateDatabase(host).Wait();
            return host;
        }

        /// <summary>
        /// Middleware olarak kullanılabilir, uygulama başladığında migrasyonları uygular
        /// </summary>
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var logger = serviceScope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();
                
                try
                {
                    logger.LogInformation("Veritabanı migrasyonları kontrol ediliyor...");
                    context.Database.Migrate();
                    logger.LogInformation("Veritabanı migrasyonları başarıyla uygulandı.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Veritabanı migrasyon işlemi sırasında bir hata oluştu!");
                }
            }

            return app;
        }

        /// <summary>
        /// Belirli bir çevre için migrasyon stratejisi belirler ve uygular
        /// </summary>
        public static IHost MigrateDatabaseByEnvironment(this IHost host, string environment)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    logger.LogInformation($"{environment} ortamı için migrasyon stratejisi uygulanıyor...");
                    
                    // Çevre bazlı farklı stratejiler
                    switch (environment.ToLower())
                    {
                        case "development":
                            // Geliştirme ortamında veritabanı sıfırlanabilir (isteğe bağlı)
                            // context.Database.EnsureDeleted();
                            context.Database.Migrate();
                            logger.LogInformation("Development veritabanı migration işlemi tamamlandı.");
                            break;
                            
                        case "test":
                        case "staging":
                            // Test ortamında varsa migrasyon yapılır, yoksa oluşturulur
                            if (context.Database.GetPendingMigrations().Any())
                            {
                                context.Database.Migrate();
                                logger.LogInformation("Test/Staging veritabanı migration işlemi tamamlandı.");
                            }
                            break;
                            
                        case "production":
                            // Üretim ortamında sadece script çalıştırılır
                            // Üretim için script oluştur ama otomatik çalıştırma - DBA onayı gerekir
                            string scriptPath = AppDomain.CurrentDomain.BaseDirectory + $"/Migrations/Scripts/Migration_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(scriptPath));
                            
                            var migrator = context.Database.GetService<IMigrator>();
                            string migrationScript = migrator.GenerateScript(
                                fromMigration: context.Database.GetAppliedMigrations().LastOrDefault() ?? "",
                                toMigration: context.Database.GetMigrations().Last()
                            );
                            
                            System.IO.File.WriteAllText(scriptPath, migrationScript);
                            
                            logger.LogInformation($"Üretim için migrasyon scripti oluşturuldu: {scriptPath}");
                            logger.LogInformation("DİKKAT: Üretim ortamında migrasyon scripti manuel olarak çalıştırılacak");
                            break;
                            
                        default:
                            context.Database.Migrate();
                            logger.LogInformation("Default veritabanı migration işlemi tamamlandı.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{environment} ortamı için migrasyon işlemi sırasında hata oluştu!");
                }
            }

            return host;
        }
    }
}
