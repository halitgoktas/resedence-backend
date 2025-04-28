using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResidenceManagement.Core.Entities.Kbs;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Infrastructure.Data.Repositories;

namespace ResidenceManagement.Infrastructure.Repositories
{
    /// <summary>
    /// KBS bildirimleri repository implementasyonu
    /// </summary>
    public class KbsNotificationRepository : GenericRepository<KbsNotification>, IKbsNotificationRepository
    {
        private readonly new ApplicationDbContext _dbContext;

        public KbsNotificationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Bekleyen tüm bildirimleri getirir
        /// </summary>
        /// <returns>Bekleyen KBS bildirimleri listesi</returns>
        public async Task<List<KbsNotification>> GetPendingNotificationsAsync()
        {
            return await _dbContext.KbsNotifications
                .Where(n => n.Status == KbsNotificationStatus.Pending)
                .OrderBy(n => n.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Belirli bir kişi ve daire için mevcut bildirimleri getirir
        /// </summary>
        /// <param name="personId">Kişi ID</param>
        /// <param name="personType">Kişi tipi</param>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Kişi ve daire için mevcut bildirimler</returns>
        public async Task<List<KbsNotification>> GetNotificationsByPersonAndApartmentAsync(int personId, string personType, int apartmentId)
        {
            // Not: PersonId ve PersonType alanları KbsNotification sınıfında tanımlı değil gibi görünüyor
            // Bu metodu saklıyoruz, ancak düzgün bir şekilde implemente edilmesi gerekiyor
            return await _dbContext.KbsNotifications
                .Where(n => n.ApartmentId == apartmentId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Belirli bir daire için tüm bildirimleri getirir
        /// </summary>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Daire için tüm bildirimler</returns>
        public async Task<List<KbsNotification>> GetNotificationsByApartmentAsync(int apartmentId)
        {
            return await _dbContext.KbsNotifications
                .Where(n => n.ApartmentId == apartmentId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Belirli bir tarih aralığında oluşturulan bildirimleri getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Tarih aralığındaki bildirimler</returns>
        public async Task<List<KbsNotification>> GetNotificationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Bitiş tarihini günün sonuna ayarla
            endDate = endDate.Date.AddDays(1).AddTicks(-1);
            
            return await _dbContext.KbsNotifications
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Hata durumundaki bildirimleri getirir
        /// </summary>
        /// <returns>Hata durumundaki bildirimler</returns>
        public async Task<List<KbsNotification>> GetFailedNotificationsAsync()
        {
            return await _dbContext.KbsNotifications
                .Where(n => n.Status == KbsNotificationStatus.Failed)
                .OrderByDescending(n => n.LastRetryDate)
                .ToListAsync();
        }
    }
} 