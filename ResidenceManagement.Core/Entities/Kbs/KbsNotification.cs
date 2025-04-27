using System;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS bildirim kaydı entity sınıfı
    /// </summary>
    public class KbsNotification : BaseEntity
    {
        /// <summary>
        /// Bildirime konu olan kişinin ID'si (Kullanıcı ID veya Aile Üyesi ID)
        /// </summary>
        public int PersonId { get; set; }
        
        /// <summary>
        /// Kişi tipi: "Kullanici", "FamilyMember" gibi
        /// </summary>
        public string PersonType { get; set; }
        
        /// <summary>
        /// İlgili daire ID
        /// </summary>
        public int ApartmentId { get; set; }
        
        /// <summary>
        /// İlgili daire
        /// </summary>
        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }
        
        /// <summary>
        /// Bildirim tipi: "Giris", "Cikis" vb.
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim durumu: "Bekliyor", "Gonderildi", "Hata" vb.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Bildirim oluşturma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Bildirim gönderim tarihi
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
        
        /// <summary>
        /// KBS'den dönen referans numarası
        /// </summary>
        public string KbsReferenceNumber { get; set; }
        
        /// <summary>
        /// KBS yanıt/hata detayları
        /// </summary>
        public string ResponseDetails { get; set; }
        
        /// <summary>
        /// İşlem yapan kullanıcı ID
        /// </summary>
        public int? ProcessedByUserId { get; set; }
        
        /// <summary>
        /// Son deneme tarihi
        /// </summary>
        public DateTime? LastAttemptDate { get; set; }
        
        /// <summary>
        /// Deneme sayısı
        /// </summary>
        public int AttemptCount { get; set; } = 0;
        
        /// <summary>
        /// Bildirim verilerinin JSON formatında saklandığı alan
        /// </summary>
        public string NotificationData { get; set; }
    }
} 