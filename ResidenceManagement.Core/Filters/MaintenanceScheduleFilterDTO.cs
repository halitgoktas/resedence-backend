using System;

namespace ResidenceManagement.Core.Filters
{
    // Bakım çizelgesi için filtre DTO sınıfı
    public class MaintenanceScheduleFilterDTO
    {
        // Sayfalama
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
        // Arama ve filtreleme
        public string SearchTerm { get; set; } = string.Empty;
        public string MaintenanceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        
        // Tarih aralığı
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        // İlişkili öğelere göre filtreleme
        public int? EquipmentId { get; set; }
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public int? AssignedToUserId { get; set; }
        
        // Sıralama
        public string SortBy { get; set; } = "ScheduledDate";
        public bool SortDescending { get; set; } = false;
        
        // Multi-tenant yapısı
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Yinelenen bakım filtreleri
        public bool? IsRecurring { get; set; }
        
        // Tamamlanma durumu
        public bool? IsCompleted { get; set; }
        public bool? IsOverdue { get; set; }
    }
} 