using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım raporları veri transfer nesnesi
    /// </summary>
    public class MaintenanceReportDTO
    {
        /// <summary>
        /// Rapor oluşturma tarihi
        /// </summary>
        public DateTime ReportDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Rapor dönemi başlangıç tarihi
        /// </summary>
        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Rapor dönemi bitiş tarihi
        /// </summary>
        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Toplam bakım sayısı
        /// </summary>
        public int TotalMaintenanceCount { get; set; }
        
        /// <summary>
        /// Yaklaşan bakım sayısı
        /// </summary>
        public int UpcomingMaintenanceCount { get; set; }
        
        /// <summary>
        /// Bekleyen bakım sayısı
        /// </summary>
        public int PendingMaintenanceCount { get; set; }
        
        /// <summary>
        /// Devam eden bakım sayısı
        /// </summary>
        public int InProgressMaintenanceCount { get; set; }
        
        /// <summary>
        /// Tamamlanan bakım sayısı
        /// </summary>
        public int CompletedMaintenanceCount { get; set; }
        
        /// <summary>
        /// Gecikmiş bakım sayısı
        /// </summary>
        public int OverdueMaintenanceCount { get; set; }
        
        /// <summary>
        /// İptal edilen bakım sayısı
        /// </summary>
        public int CancelledMaintenanceCount { get; set; }
        
        /// <summary>
        /// Zamanında tamamlanan bakımların yüzdesi
        /// </summary>
        public decimal OnTimeCompletionRate { get; set; }
        
        /// <summary>
        /// Bakım tipine göre dağılım
        /// </summary>
        public List<MaintenanceByTypeDTO> MaintenanceByType { get; set; } = new List<MaintenanceByTypeDTO>();
        
        /// <summary>
        /// Lokasyona göre dağılım
        /// </summary>
        public List<MaintenanceByLocationDTO> MaintenanceByLocation { get; set; } = new List<MaintenanceByLocationDTO>();
        
        /// <summary>
        /// Duruma göre dağılım
        /// </summary>
        public List<MaintenanceByStatusDTO> MaintenanceByStatus { get; set; } = new List<MaintenanceByStatusDTO>();
        
        /// <summary>
        /// Önceliğe göre dağılım
        /// </summary>
        public List<MaintenanceByPriorityDTO> MaintenanceByPriority { get; set; } = new List<MaintenanceByPriorityDTO>();
        
        /// <summary>
        /// Aylara göre dağılım
        /// </summary>
        public List<MaintenanceByMonthDTO> MaintenanceByMonth { get; set; } = new List<MaintenanceByMonthDTO>();
        
        /// <summary>
        /// En sık tekrar eden bakım çalışmaları
        /// </summary>
        public List<RecurringMaintenanceDTO> MostFrequentMaintenance { get; set; } = new List<RecurringMaintenanceDTO>();
        
        /// <summary>
        /// Personele göre dağılım
        /// </summary>
        public List<MaintenanceByStaffDTO> MaintenanceByStaff { get; set; } = new List<MaintenanceByStaffDTO>();
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Toplam bakım sayıları
        /// </summary>
        public int TotalUpcomingMaintenance { get; set; }
        public int TotalPendingMaintenance { get; set; }
        public int TotalInProgressMaintenance { get; set; }
        public int TotalCompletedMaintenance { get; set; }
        public int TotalOverdueMaintenance { get; set; }
        
        /// <summary>
        /// Yaklaşan bakımlar
        /// </summary>
        public List<MaintenanceScheduleDTO> UpcomingMaintenance { get; set; } = new List<MaintenanceScheduleDTO>();
        
        /// <summary>
        /// Bakım dağılımları
        /// </summary>
        public List<MaintenanceByTypeDTO> ByType { get; set; } = new List<MaintenanceByTypeDTO>();
        public List<MaintenanceByLocationDTO> ByLocation { get; set; } = new List<MaintenanceByLocationDTO>();
        public List<MaintenanceByStatusDTO> ByStatus { get; set; } = new List<MaintenanceByStatusDTO>();
        
        /// <summary>
        /// Bakıma özgü bilgiler
        /// </summary>
        public int MaintenanceScheduleId { get; set; }
        public string MaintenanceTitle { get; set; }
        public string MaintenanceDescription { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int CompletedTasks { get; set; }
        public int TotalTasks { get; set; }
        
        /// <summary>
        /// İlişkili bilgiler
        /// </summary>
        public List<MaintenanceLogDTO> LogEntries { get; set; } = new List<MaintenanceLogDTO>();
        public List<MaintenanceDocumentDTO> Attachments { get; set; } = new List<MaintenanceDocumentDTO>();
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Raporun oluşturulduğu tarih
        /// </summary>
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Toplam sayılar - normal property olarak tanımlandı
        /// </summary>
        public int TotalUpcoming { get; set; }
        public int TotalPending { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalOverdue { get; set; }
        
        /// <summary>
        /// Toplam tahmini maliyet
        /// </summary>
        public decimal TotalEstimatedCost { get; set; }
        
        /// <summary>
        /// Toplam gerçek maliyet
        /// </summary>
        public decimal TotalActualCost { get; set; }
        
        /// <summary>
        /// Ortalama tamamlanma süresi (saat)
        /// </summary>
        public decimal AverageCompletionTime { get; set; }
        
        /// <summary>
        /// Raporun oluşturulduğu tarih
        /// </summary>
        public DateTime ReportGeneratedDate { get; set; }

        public MaintenanceReportDTO()
        {
            ReportGeneratedDate = DateTime.Now;
        }
    }
    
    /// <summary>
    /// Bakım tiplerine göre dağılım DTO
    /// </summary>
    public class MaintenanceByTypeDTO
    {
        /// <summary>
        /// Bakım tipi
        /// </summary>
        public string MaintenanceType { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Toplam içindeki yüzde
        /// </summary>
        public decimal Percentage { get; set; }
        
        /// <summary>
        /// Bakım tipi (MaintenanceType ile aynı, geriye dönük uyumluluk için)
        /// </summary>
        public string Type { get; set; }
    }
    
    /// <summary>
    /// Duruma göre bakım dağılım DTO
    /// </summary>
    public class MaintenanceByStatusDTO
    {
        /// <summary>
        /// Bakım durumu
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Toplam içindeki yüzde
        /// </summary>
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// Önceliğe göre bakım dağılım DTO
    /// </summary>
    public class MaintenanceByPriorityDTO
    {
        /// <summary>
        /// Öncelik seviyesi
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Toplam içindeki yüzde
        /// </summary>
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// Aylara göre bakım dağılım DTO
    /// </summary>
    public class MaintenanceByMonthDTO
    {
        /// <summary>
        /// Ay (1-12)
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// Yıl
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// Ay adı
        /// </summary>
        public string MonthName { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
    }
    
    /// <summary>
    /// Sık tekrar eden bakım DTO
    /// </summary>
    public class RecurringMaintenanceDTO
    {
        /// <summary>
        /// Ekipman adı
        /// </summary>
        public string EquipmentName { get; set; }
        
        /// <summary>
        /// Lokasyon
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Bakım türü
        /// </summary>
        public string MaintenanceType { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Tekrar sıklığı (gün)
        /// </summary>
        public int FrequencyDays { get; set; }
    }
    
    /// <summary>
    /// Personele göre bakım dağılım DTO
    /// </summary>
    public class MaintenanceByStaffDTO
    {
        /// <summary>
        /// Personel ID
        /// </summary>
        public int StaffId { get; set; }
        
        /// <summary>
        /// Personel adı
        /// </summary>
        public string StaffName { get; set; }
        
        /// <summary>
        /// Departman
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// Tamamlanan bakım sayısı
        /// </summary>
        public int CompletedCount { get; set; }
        
        /// <summary>
        /// Devam eden bakım sayısı
        /// </summary>
        public int InProgressCount { get; set; }
        
        /// <summary>
        /// Ortalama tamamlanma süresi (saat)
        /// </summary>
        public decimal AverageCompletionTime { get; set; }
    }
} 