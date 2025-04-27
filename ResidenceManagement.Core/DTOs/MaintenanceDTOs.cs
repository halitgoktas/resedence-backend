using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.DTOs.Maintenance;

namespace ResidenceManagement.Core.DTOs
{
    // Bakım planı listesi DTO
    public class MaintenanceScheduleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MaintenanceType { get; set; }
        public int Priority { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan? ScheduledTime { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public string Status { get; set; }
        public string ResidenceName { get; set; }
        public string BlockName { get; set; }
        public string ApartmentNumber { get; set; }
        public string EquipmentName { get; set; }
        public string AssignedToUserName { get; set; }
        public bool IsRecurring { get; set; }
        public decimal EstimatedCost { get; set; }
    }

    // Bakım planı detay DTO
    public class MaintenanceScheduleDetailDTO : MaintenanceScheduleDTO
    {
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public int? EquipmentId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public string RecurrencePattern { get; set; }
        public int? RecurrenceFrequency { get; set; }
        public string RecurrenceDaysOfWeek { get; set; }
        public int? RecurrenceDayOfMonth { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public int? RecurrenceMaxOccurrences { get; set; }
        public string CostCenter { get; set; }
        public string BudgetCode { get; set; }
        public bool SendNotification { get; set; }
        public int? NotificationDaysInAdvance { get; set; }
        public string NotificationEmail { get; set; }
        public string NotificationSMS { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string CompletedBy { get; set; }
        public string CompletionNotes { get; set; }
        public decimal? ActualCost { get; set; }
        public int? ActualDurationMinutes { get; set; }
        public string CancellationReason { get; set; }
        public string RequiredTools { get; set; }
        public string RequiredMaterials { get; set; }
        public string RequiredSkills { get; set; }
        public string PreMaintenanceChecklist { get; set; }
        public string MaintenanceSteps { get; set; }
        public string PostMaintenanceChecklist { get; set; }
        public string SafetyPrecautions { get; set; }
        public string EmergencyProcedures { get; set; }
        public List<MaintenanceDocumentDTO> Documents { get; set; } = new List<MaintenanceDocumentDTO>();
        public List<MaintenanceChecklistItemDTO> ChecklistItems { get; set; } = new List<MaintenanceChecklistItemDTO>();
        public List<MaintenanceLogDTO> Logs { get; set; } = new List<MaintenanceLogDTO>();
    }

    // Bakım planı oluşturma DTO
    public class MaintenanceScheduleCreateDTO
    {
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Bakım tipi zorunludur.")]
        [StringLength(50, ErrorMessage = "Bakım tipi en fazla 50 karakter olabilir.")]
        public string MaintenanceType { get; set; }
        
        [Range(1, 5, ErrorMessage = "Öncelik 1-5 arasında olmalıdır.")]
        public int Priority { get; set; } = 3;
        
        // Lokasyon bilgileri (en az birisi zorunlu)
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public int? EquipmentId { get; set; }
        
        // Zamanlama bilgileri
        [Required(ErrorMessage = "Planlanan tarih zorunludur.")]
        public DateTime ScheduledDate { get; set; }
        
        public TimeSpan? ScheduledTime { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? EstimatedDurationMinutes { get; set; }
        
        // Tekrarlama bilgileri
        public bool IsRecurring { get; set; } = false;
        
        public string RecurrencePattern { get; set; }
        
        public int? RecurrenceFrequency { get; set; }
        
        public string RecurrenceDaysOfWeek { get; set; }
        
        public int? RecurrenceDayOfMonth { get; set; }
        
        public DateTime? RecurrenceEndDate { get; set; }
        
        public int? RecurrenceMaxOccurrences { get; set; }
        
        // Atama bilgileri
        public int? AssignedToUserId { get; set; }
        
        public string AssignedToUserName { get; set; }
        
        public int? ServiceProviderId { get; set; }
        
        public string ServiceProviderName { get; set; }
        
        // Maliyet bilgileri
        public decimal EstimatedCost { get; set; } = 0;
        
        public string CostCenter { get; set; }
        
        public string BudgetCode { get; set; }
        
        // Bildirim ayarları
        public bool SendNotification { get; set; } = false;
        
        public int? NotificationDaysInAdvance { get; set; }
        
        public string NotificationEmail { get; set; }
        
        public string NotificationSMS { get; set; }
        
        // Gerekli malzeme ve ekipmanlar
        public string RequiredTools { get; set; }
        
        public string RequiredMaterials { get; set; }
        
        public string RequiredSkills { get; set; }
        
        // Bakım prosedürleri
        public string PreMaintenanceChecklist { get; set; }
        
        public string MaintenanceSteps { get; set; }
        
        public string PostMaintenanceChecklist { get; set; }
        
        // Güvenlik bilgileri
        public string SafetyPrecautions { get; set; }
        
        public string EmergencyProcedures { get; set; }
        
        // Kontrol listesi öğeleri (opsiyonel)
        public List<MaintenanceChecklistItemCreateDTO> ChecklistItems { get; set; } = new List<MaintenanceChecklistItemCreateDTO>();
    }

    // Bakım planı güncelleme DTO
    public class MaintenanceScheduleUpdateDTO : MaintenanceScheduleCreateDTO
    {
        [Required(ErrorMessage = "ID alanı zorunludur.")]
        public int Id { get; set; }
        
        public string Status { get; set; }
    }

    // Bakım belgesi DTO
    public class MaintenanceDocumentDTO
    {
        public int Id { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
    }

    // Bakım log kaydı DTO
    public class MaintenanceLogDTO
    {
        public int Id { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string Activity { get; set; }
        public string Notes { get; set; }
        public DateTime LogDate { get; set; }
        public string PerformedBy { get; set; }
    }

    // Bakım raporu DTO
    public class MaintenanceReportDTO
    {
        // Toplam bakım sayıları
        public int TotalUpcomingMaintenance { get; set; }
        public int TotalPendingMaintenance { get; set; }
        public int TotalInProgressMaintenance { get; set; }
        public int TotalCompletedMaintenance { get; set; }
        public int TotalOverdueMaintenance { get; set; }
        
        // Yaklaşan bakımlar
        public List<MaintenanceScheduleDTO> UpcomingMaintenance { get; set; } = new List<MaintenanceScheduleDTO>();
        
        // Bakım dağılımları
        public List<MaintenanceByTypeDTO> MaintenanceByType { get; set; } = new List<MaintenanceByTypeDTO>();
        public List<MaintenanceByLocationDTO> MaintenanceByLocation { get; set; } = new List<MaintenanceByLocationDTO>();
        public List<MaintenanceByStatusDTO> MaintenanceByStatus { get; set; } = new List<MaintenanceByStatusDTO>();
        
        // Rapor oluşturma tarihi
        public DateTime ReportGeneratedDate { get; set; } = DateTime.Now;
        
        // Alias olarak eklenen özellikler
        public int TotalUpcoming => TotalUpcomingMaintenance;
        public int TotalPending => TotalPendingMaintenance;
        public int TotalInProgress => TotalInProgressMaintenance;
        public int TotalCompleted => TotalCompletedMaintenance;
        public int TotalOverdue => TotalOverdueMaintenance;
        public List<MaintenanceByTypeDTO> ByType => MaintenanceByType;
        public List<MaintenanceByLocationDTO> ByLocation => MaintenanceByLocation;
        public List<MaintenanceByStatusDTO> ByStatus => MaintenanceByStatus;
    }

    // Bakım tipi dağılım DTO
    public class MaintenanceByTypeDTO
    {
        public string MaintenanceType { get; set; }
        public int Count { get; set; }
        public decimal EstimatedTotalCost { get; set; }
    }

    // Durum bazlı bakım dağılım DTO
    public class MaintenanceByStatusDTO
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    // Bakım maliyet raporu DTO
    public class MaintenanceCostReportDTO
    {
        public decimal TotalEstimatedCost { get; set; }
        public decimal TotalActualCost { get; set; }
        public decimal CostVariance { get; set; }
        public List<MonthlyCostDTO> MonthlyCosts { get; set; } = new List<MonthlyCostDTO>();
        public List<MaintenanceTypeCostDTO> CostsByType { get; set; } = new List<MaintenanceTypeCostDTO>();
        public List<LocationCostDTO> CostsByLocation { get; set; } = new List<LocationCostDTO>();
        
        // Bakıma özgü bilgiler
        public int MaintenanceScheduleId { get; set; }
        public string MaintenanceTitle { get; set; }
        public decimal LaborCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal ToolsCost { get; set; }
        public decimal ExternalServiceCost { get; set; }
        public decimal OtherCosts { get; set; }
        public decimal TotalCost { get; set; }
        public string CurrencyCode { get; set; }
        public List<MaintenanceCostItemDTO> CostBreakdown { get; set; } = new List<MaintenanceCostItemDTO>();
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
        public decimal AverageCost { get; set; }
        public int? MostExpensiveMaintenanceId { get; set; }
        public decimal? MostExpensiveMaintenanceCost { get; set; }
        public List<CostByTypeDTO> CostByMaintenanceType { get; set; } = new List<CostByTypeDTO>();
        public List<HighCostMaintenanceDTO> HighCostMaintenances { get; set; } = new List<HighCostMaintenanceDTO>();
        public DateTime ReportGeneratedDate { get; set; } = DateTime.Now;
    }

    // Aylık maliyet DTO
    public class MonthlyCostDTO
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
    }

    // Bakım tipi bazlı maliyet DTO
    public class MaintenanceTypeCostDTO
    {
        public string MaintenanceType { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public int Count { get; set; }
    }

    // Lokasyon bazlı maliyet DTO
    public class LocationCostDTO
    {
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
    }
    
    // Bakım maliyet kalemi DTO
    public class MaintenanceCostItemDTO
    {
        public string CostType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime RecordDate { get; set; }
        public int? RecordedByUserId { get; set; }
    }
    
    // Tip bazlı maliyet DTO
    public class CostByTypeDTO
    {
        public string Type { get; set; }
        public decimal TotalEstimatedCost { get; set; }
        public decimal TotalActualCost { get; set; }
        public decimal AverageCost { get; set; }
        public int Count { get; set; }
    }
    
    // Lokasyon bazlı maliyet DTO
    public class CostByLocationDTO
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal TotalEstimatedCost { get; set; }
        public decimal TotalActualCost { get; set; }
    }
    
    // Yüksek maliyetli bakım DTO
    public class HighCostMaintenanceDTO
    {
        public int MaintenanceId { get; set; }
        public string MaintenanceTitle { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public decimal CostDifference { get; set; }
    }

    // Kontrol listesi öğesi oluşturma DTO
    public class MaintenanceChecklistItemCreateDTO
    {
        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        public string Description { get; set; }
        
        public int OrderIndex { get; set; }
        
        [Required(ErrorMessage = "Kategori alanı zorunludur.")]
        public string Category { get; set; }
    }
} 