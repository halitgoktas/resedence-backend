using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Kbs;
using ResidenceManagement.Core.Interfaces.Repositories;

namespace ResidenceManagement.Core.Interfaces.Repositories
{
    /// <summary>
    /// KBS bildirimleri için repository interface'i
    /// </summary>
    public interface IKbsNotificationRepository : IGenericRepository<KbsNotification>
    {
        /// <summary>
        /// Bekleyen tüm bildirimleri getirir
        /// </summary>
        /// <returns>Bekleyen KBS bildirimleri listesi</returns>
        Task<List<KbsNotification>> GetPendingNotificationsAsync();
        
        /// <summary>
        /// Belirli bir kişi ve daire için mevcut bildirimleri getirir
        /// </summary>
        /// <param name="personId">Kişi ID</param>
        /// <param name="personType">Kişi tipi</param>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Kişi ve daire için mevcut bildirimler</returns>
        Task<List<KbsNotification>> GetNotificationsByPersonAndApartmentAsync(int personId, string personType, int apartmentId);
        
        /// <summary>
        /// Belirli bir daire için tüm bildirimleri getirir
        /// </summary>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Daire için tüm bildirimler</returns>
        Task<List<KbsNotification>> GetNotificationsByApartmentAsync(int apartmentId);
        
        /// <summary>
        /// Belirli bir tarih aralığında oluşturulan bildirimleri getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Tarih aralığındaki bildirimler</returns>
        Task<List<KbsNotification>> GetNotificationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Hata durumundaki bildirimleri getirir
        /// </summary>
        /// <returns>Hata durumundaki bildirimler</returns>
        Task<List<KbsNotification>> GetFailedNotificationsAsync();
    }
} 