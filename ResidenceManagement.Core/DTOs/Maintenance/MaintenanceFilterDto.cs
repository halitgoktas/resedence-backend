using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım planı filtreleme parametrelerini içeren DTO sınıfı
    /// </summary>
    public class MaintenanceFilterDTO
    {
        /// <summary>
        /// Başlığa göre arama yapmak için kullanılır
        /// </summary>
        public string TitleSearch { get; set; }
        
        /// <summary>
        /// Duruma göre filtreleme yapar (Beklemede, Tamamlandı, İptal Edildi, vb.)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Bakım tipine göre filtreleme yapar (Periyodik, Arıza, Önleyici, vb.)
        /// </summary>
        public string MaintenanceType { get; set; }
        
        /// <summary>
        /// Önceliğe göre filtreleme yapar (Düşük, Orta, Yüksek, Acil)
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// Atanan personele göre filtreleme yapar
        /// </summary>
        public int? AssignedToUserId { get; set; }
        
        /// <summary>
        /// Lokasyona göre filtreleme yapar (Daire, Blok, Ortak Alan ID'si)
        /// </summary>
        public int? LocationId { get; set; }
        
        /// <summary>
        /// Lokasyon tipine göre filtreleme yapar (Daire, Blok, Ortak Alan)
        /// </summary>
        public string LocationType { get; set; }
        
        /// <summary>
        /// Ekipmana göre filtreleme yapar
        /// </summary>
        public int? EquipmentId { get; set; }
        
        /// <summary>
        /// Başlangıç tarihine göre filtreleme yapar
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Bitiş tarihine göre filtreleme yapar
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Sayfalama - Sayfa numarası
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Sayfalama - Sayfa boyutu
        /// </summary>
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// Sıralama alanı
        /// </summary>
        public string SortBy { get; set; } = "ScheduledDate";
        
        /// <summary>
        /// Sıralama yönü (Artan/Azalan)
        /// </summary>
        public bool SortDescending { get; set; } = false;
        
        /// <summary>
        /// Yinelenen bakım planlarını filtreleme
        /// </summary>
        public bool? IsRecurring { get; set; }
        
        /// <summary>
        /// Tamamlanma durumuna göre filtreleme
        /// </summary>
        public bool? IsCompleted { get; set; }
        
        /// <summary>
        /// Zaman aşımı durumuna göre filtreleme
        /// </summary>
        public bool? IsOverdue { get; set; }
        
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