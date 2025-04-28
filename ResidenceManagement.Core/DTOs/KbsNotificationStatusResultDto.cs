using System;

namespace ResidenceManagement.Core.DTOs
{
    /// <summary>
    /// KBS bildirim durum kontrolü sonucu için DTO sınıfı
    /// </summary>
    public class KbsNotificationStatusResultDto
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Durum mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Durum kodu
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Durum açıklaması
        /// </summary>
        public string StatusMessage { get; set; }
        
        /// <summary>
        /// Referans numarası
        /// </summary>
        public string ReferenceNumber { get; set; }
        
        /// <summary>
        /// HTTP durum kodu
        /// </summary>
        public string StatusCode { get; set; }
        
        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
        /// Hata mesajı (varsa)
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// İşlem tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Bildirim onaylandı mı?
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// Onay/red tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
    }
} 