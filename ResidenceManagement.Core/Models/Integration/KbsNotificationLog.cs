using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Integration
{
    // KBS bildirim log kayıtları için model sınıfı
    public class KbsIntegrationLog : BaseEntity
    {
        // İlişkili KBS bildirimin ID'si
        public int KbsNotificationId { get; set; }
        
        // Log kaydı oluşturulma tarihi
        public DateTime LogDate { get; set; }
        
        // Bildirim durumu
        public string Status { get; set; }
        
        // Log mesajı
        public string Message { get; set; }
        
        // İşlemi gerçekleştiren kullanıcı adı
        public string Username { get; set; }
        
        // İP adresi
        public string IpAddress { get; set; }
        
        // İlişkili KBS bildirimi
        public virtual KbsNotification KbsNotification { get; set; }
        
        public KbsIntegrationLog()
        {
            LogDate = DateTime.Now;
        }
    }
} 