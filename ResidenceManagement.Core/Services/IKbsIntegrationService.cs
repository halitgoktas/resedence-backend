using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Kbs;
using ResidenceManagement.Core.DTOs;

namespace ResidenceManagement.Core.Services
{
    /// <summary>
    /// KBS entegrasyonu için servis arayüzü
    /// </summary>
    public interface IKbsIntegrationService
    {
        /// <summary>
        /// KBS'ye bildirim gönderir
        /// </summary>
        Task<KbsNotification> SendNotificationAsync(KbsNotification notification);

        /// <summary>
        /// KBS'ye gönderilen bildirimin durumunu sorgular
        /// </summary>
        Task<KbsNotificationLog> CheckNotificationStatusAsync(string referenceNumber);

        /// <summary>
        /// KBS'ye toplu bildirim gönderir
        /// </summary>
        Task<List<KbsNotification>> SendBatchNotificationsAsync(List<KbsNotification> notifications);

        // KBS bildirimleri listele (filtreleme ve sayfalama ile)
        Task<PaginatedResultDto<KbsNotificationDto>> GetNotificationsAsync(KbsNotificationFilterDto filter);
        
        // Belirli ID'ye sahip bir KBS bildirimini getir
        Task<KbsNotificationDetailDto> GetNotificationByIdAsync(int id);
        
        // Yeni bir KBS bildirimi oluştur
        Task<KbsNotificationDto> CreateNotificationAsync(KbsNotificationCreateDto model);
        
        // Mevcut bir KBS bildirimini güncelle
        Task<KbsNotificationDto> UpdateNotificationAsync(KbsNotificationUpdateDto model);
        
        // KBS bildirimini sil (soft delete)
        Task<bool> DeleteNotificationAsync(int id);
        
        // KBS bildirimi log kayıtlarını getir
        Task<List<KbsNotificationLogDto>> GetNotificationLogsAsync(int notificationId);
        
        // KBS bildirimi oluştur (rezervasyondan)
        Task<KbsNotificationDto> CreateFromReservationAsync(int reservationId);
        
        // KBS'ye bildirim gönder
        Task<KbsNotificationStatusDto> SubmitToKbsAsync(int notificationId, string userName);
        
        // KBS'ye bildirim tekrar gönder
        Task<KbsNotificationStatusDto> ResubmitToKbsAsync(int notificationId, string userName);
        
        // KBS bildirimini iptal et
        Task<KbsNotificationStatusDto> CancelNotificationAsync(int notificationId, string userName, string cancellationReason);
        
        // KBS entegrasyonu durum raporu getir
        Task<KbsStatusReportDto> GetStatusReportAsync(DateTime? startDate, DateTime? endDate);
        
        // CSV formatında KBS bildirimleri raporu oluştur
        Task<byte[]> GenerateKbsReportCsvAsync(DateTime startDate, DateTime endDate);
        
        // Otomatik KBS bildirim kontrol metodu (günlük job için)
        Task<KbsAutomationResultDto> ProcessPendingNotificationsAsync();
        
        // KBS konfigürasyonu güncelle
        Task UpdateKbsConfigurationAsync(KbsConfigurationDto model);
        
        // Güncel KBS konfigürasyonu getir
        Task<KbsConfigurationDto> GetKbsConfigurationAsync();
        
        // KBS bağlantı testi yap
        Task<KbsConnectionTestResultDto> TestKbsConnectionAsync(KbsConfigurationDto config = null);
        
        // KBS'ye bildirim gönderme
        Task<KbsSubmissionResultDto> SubmitNotificationAsync(KbsNotification notification);
        
        // KBS bildirimini iptal etme
        Task<KbsCancellationResultDto> CancelNotificationAsync(string kbsReferenceNumber, string cancellationReason);
        
        // KBS bildirimi durumunu sorgulama
        Task<KbsNotificationStatusResultDto> GetNotificationStatusAsync(string kbsReferenceNumber);
        
        // KBS bildirim günlük kayıtlarını alma
        Task<IEnumerable<KbsNotificationLog>> GetNotificationLogsAsync(string kbsReferenceNumber);
        
        // Günlük rapor alma
        Task<KbsDailyReportDto> GetDailyReportAsync(DateTime reportDate);
    }
} 