// KbsNotificationHistory sınıfı, bildirim durumu değişikliklerini takip etmek için kullanılır
using System;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Models.KBS;

namespace ResidenceManagement.Core.Models.KBS
{
    /// <summary>
    /// KBS bildirim geçmişi sınıfı
    /// </summary>
    public class KbsNotificationHistory : BaseEntity
    {
        /// <summary>
        /// İlgili KBS bildiriminin ID'si
        /// </summary>
        public int KbsNotificationId { get; set; }
        
        /// <summary>
        /// İlgili KBS bildirimi
        /// </summary>
        public KbsNotification KbsNotification { get; set; }
        
        /// <summary>
        /// Eski durum
        /// </summary>
        public KbsNotifyStatus OldStatus { get; set; }
        
        /// <summary>
        /// Yeni durum
        /// </summary>
        public KbsNotifyStatus NewStatus { get; set; }
        
        /// <summary>
        /// Değişiklik tarihi
        /// </summary>
        public DateTime ChangeDate { get; set; }
        
        /// <summary>
        /// Değişikliği yapan kullanıcı ID'si
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// Değişikliği yapan kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Değişiklik açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Ek bilgiler (JSON formatında saklanabilir)
        /// </summary>
        public string AdditionalInfo { get; set; }
        
        /// <summary>
        /// Varsayılan constructor
        /// </summary>
        public KbsNotificationHistory()
        {
            ChangeDate = DateTime.Now;
        }
    }
} 