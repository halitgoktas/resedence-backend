using System;

namespace ResidenceManagement.Application.DTOs
{
    /// <summary>
    /// Bakım çizelgesi veri transfer nesnesi
    /// </summary>
    public class MaintenanceScheduleDTO
    {
        /// <summary>
        /// Bakım çizelgesi ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Bakım başlığı
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Bakım açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Bakım tipi
        /// </summary>
        public string MaintenanceType { get; set; } = string.Empty;
        
        /// <summary>
        /// Planlanan tarih
        /// </summary>
        public DateTime ScheduledDate { get; set; }
        
        /// <summary>
        /// Tamamlanma tarihi
        /// </summary>
        public DateTime? CompletionDate { get; set; }
        
        /// <summary>
        /// Durum (Planlandı, Devam Ediyor, Tamamlandı, İptal Edildi)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// Öncelik (Düşük, Orta, Yüksek, Acil)
        /// </summary>
        public string Priority { get; set; } = string.Empty;
        
        /// <summary>
        /// İlişkili ekipman ID
        /// </summary>
        public int? EquipmentId { get; set; }
        
        /// <summary>
        /// İlişkili site ID
        /// </summary>
        public int? ResidenceId { get; set; }
        
        /// <summary>
        /// İlişkili blok ID
        /// </summary>
        public int? BlockId { get; set; }
        
        /// <summary>
        /// İlişkili daire ID
        /// </summary>
        public int? ApartmentId { get; set; }
        
        /// <summary>
        /// Atanan kullanıcı ID
        /// </summary>
        public int? AssignedToUserId { get; set; }
        
        /// <summary>
        /// Atanan kullanıcı adı
        /// </summary>
        public string AssignedToUserName { get; set; } = string.Empty;
        
        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }
        
        /// <summary>
        /// Gerçekleşen maliyet
        /// </summary>
        public decimal? ActualCost { get; set; }
        
        /// <summary>
        /// Yineleme periyodu (Günlük, Haftalık, Aylık, Yıllık)
        /// </summary>
        public string RecurrencePeriod { get; set; } = string.Empty;
        
        /// <summary>
        /// Tamamlanma yüzdesi
        /// </summary>
        public int CompletionPercentage { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Firma ID (multi-tenant)
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID (multi-tenant)
        /// </summary>
        public int SubeId { get; set; }
    }
} 