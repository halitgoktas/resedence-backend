using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Data.Migrations
{
    /// <summary>
    /// Migrasyon işlemlerini yöneten utility sınıfı
    /// </summary>
    public static class MigrationManager
    {
        /// <summary>
        /// Uygulama başlangıcında çağrılarak migrasyonları otomatik uygular
        /// </summary>
        public static async Task MigrateDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                logger.LogInformation("Veritabanı migrasyon işlemi başlıyor...");
                
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                
                logger.LogInformation("Veritabanı migrasyon işlemi başarıyla tamamlandı.");

                // Migrasyon logunu kaydet
                await LogMigration(context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Veritabanı migrasyon işlemi sırasında bir hata oluştu!");
                throw;
            }
        }

        /// <summary>
        /// Migrasyon işlemlerinin kaydını tutar
        /// </summary>
        private static async Task LogMigration(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                // Mevcut migrasyon durumunu al
                var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                
                // Migrasyon detaylarını log'la
                logger.LogInformation($"Uygulanmış migrasyon sayısı: {appliedMigrations.Count()}");
                foreach (var migration in appliedMigrations)
                {
                    logger.LogInformation($"Uygulanmış migrasyon: {migration}");
                }
                
                logger.LogInformation($"Bekleyen migrasyon sayısı: {pendingMigrations.Count()}");
                foreach (var migration in pendingMigrations)
                {
                    logger.LogInformation($"Bekleyen migrasyon: {migration}");
                }
                
                // Migrasyon log dosyasını oluştur
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MigrationLogs");
                Directory.CreateDirectory(logDirectory);
                
                string logFilePath = Path.Combine(logDirectory, $"Migration_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    await writer.WriteLineAsync($"Migrasyon Tarihi: {DateTime.Now}");
                    await writer.WriteLineAsync($"Uygulanmış Migrasyonlar: {string.Join(", ", appliedMigrations)}");
                    await writer.WriteLineAsync($"Bekleyen Migrasyonlar: {string.Join(", ", pendingMigrations)}");
                    await writer.WriteLineAsync("----------------------------------------");
                }
                
                logger.LogInformation($"Migrasyon logu kaydedildi: {logFilePath}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migrasyon log işlemi sırasında bir hata oluştu!");
            }
        }

        /// <summary>
        /// Son migrasyon işlemini geri alır
        /// </summary>
        public static async Task RollbackLastMigration(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                logger.LogInformation("Son migrasyon geri alma işlemi başlıyor...");
                
                var context = services.GetRequiredService<ApplicationDbContext>();
                
                // Uygulanmış migrasyonları al
                var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                if (!appliedMigrations.Any())
                {
                    logger.LogWarning("Geri alınacak migrasyon bulunamadı.");
                    return;
                }
                
                // Son migrasyonu bul
                var lastMigration = appliedMigrations.Last();
                
                // Bir önceki migrasyona geri dön
                var previousMigration = appliedMigrations.Count() > 1 
                    ? appliedMigrations.ElementAt(appliedMigrations.Count() - 2) 
                    : "0";
                
                logger.LogInformation($"Son migrasyon '{lastMigration}' şu migrasyona geri alınıyor: '{previousMigration}'");
                
                await context.Database.MigrateAsync(previousMigration);
                
                // Migrasyon logunu kaydet
                await LogMigration(context, logger);
                
                logger.LogInformation("Son migrasyon geri alma işlemi başarıyla tamamlandı.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migrasyon geri alma işlemi sırasında bir hata oluştu!");
                throw;
            }
        }

        /// <summary>
        /// Özel bir migrasyona kadar veritabanını günceller
        /// </summary>
        public static async Task MigrateToVersion(IHost host, string targetMigration)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                logger.LogInformation($"Veritabanı '{targetMigration}' migrasyonuna güncelleniyor...");
                
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync(targetMigration);
                
                // Migrasyon logunu kaydet
                await LogMigration(context, logger);
                
                logger.LogInformation($"Veritabanı '{targetMigration}' migrasyonuna başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"'{targetMigration}' migrasyonuna güncelleme sırasında bir hata oluştu!");
                throw;
            }
        }
    }
}
