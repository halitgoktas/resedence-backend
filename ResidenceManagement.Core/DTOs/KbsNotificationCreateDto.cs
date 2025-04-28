using System;

namespace ResidenceManagement.Core.DTOs
{
    /// <summary>
    /// KBS bildirim oluşturmak için kullanılan DTO sınıfı
    /// </summary>
    public class KbsNotificationCreateDto
    {
        /// <summary>
        /// Referans numarası
        /// </summary>
        public string ReferenceId { get; set; }
        
        /// <summary>
        /// KBS Tesisi kodu
        /// </summary>
        public string FacilityCode { get; set; }
        
        /// <summary>
        /// Bildirim tipi (Giriş, Çıkış, vb.)
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Bildirim içeriği (JSON formatında)
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Daire/Oda numarası
        /// </summary>
        public string RoomNumber { get; set; }
        
        /// <summary>
        /// Giriş tarihi
        /// </summary>
        public DateTime CheckInDate { get; set; }
        
        /// <summary>
        /// Tahmini çıkış tarihi
        /// </summary>
        public DateTime ExpectedCheckOutDate { get; set; }
        
        /// <summary>
        /// Misafir kimlik bilgileri
        /// </summary>
        public CreateKbsGuestInfoDto GuestInfo { get; set; }
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Bildirim önceliği
        /// </summary>
        public string Priority { get; set; } = "Normal";
        
        /// <summary>
        /// Otomatik bildirim mi?
        /// </summary>
        public bool IsAutomated { get; set; } = false;
    }
} 