using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Equipment
{
    // Ekipman listesi için özet DTO
    public class EquipmentListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal CurrentValue { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
    }

    // Ekipman detayları DTO
    public class EquipmentDetailDto : EquipmentListItemDto
    {
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Vendor { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal PurchasePrice { get; set; }
        public string WarrantyInfo { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public List<MaintenanceRecordDto> MaintenanceHistory { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public List<PartDto> Parts { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }

    // Ekipman filtre DTO
    public class EquipmentFilterDto
    {
        public string SearchTerm { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public DateTime? AcquisitionStartDate { get; set; }
        public DateTime? AcquisitionEndDate { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public bool SortAscending { get; set; } = true;
    }

    // Ekipman oluşturma DTO
    public class EquipmentCreateDto
    {
        [Required(ErrorMessage = "Ekipman adı zorunludur")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Kategori zorunludur")]
        public string Category { get; set; }
        
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Vendor { get; set; }
        public string PurchaseOrderNumber { get; set; }
        
        [Required(ErrorMessage = "Alım tarihi zorunludur")]
        public DateTime AcquisitionDate { get; set; }
        
        [Required(ErrorMessage = "Alım fiyatı zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Alım fiyatı sıfırdan büyük olmalıdır")]
        public decimal PurchasePrice { get; set; }
        
        [Required(ErrorMessage = "Güncel değer zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Güncel değer sıfırdan büyük olmalıdır")]
        public decimal CurrentValue { get; set; }
        
        public string WarrantyInfo { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        
        [Required(ErrorMessage = "Durum zorunludur")]
        public string Status { get; set; }
        
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public string Location { get; set; }
        public string AssignedTo { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
    }

    // Ekipman güncelleme DTO
    public class EquipmentUpdateDto : EquipmentCreateDto
    {
        [Required(ErrorMessage = "ID zorunludur")]
        public int Id { get; set; }
    }

    // Durum güncelleme DTO
    public class StatusUpdateDto
    {
        [Required(ErrorMessage = "Durum zorunludur")]
        public string Status { get; set; }
        
        [Required(ErrorMessage = "Durum notu zorunludur")]
        public string StatusNote { get; set; }
    }

    // Durum sonuç DTO
    public class StatusResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    // Ekipman yer değiştirme DTO
    public class EquipmentRelocationDto
    {
        public int? NewResidenceId { get; set; }
        public string NewResidenceName { get; set; }
        public int? NewBlockId { get; set; }
        public string NewBlockName { get; set; }
        public int? NewApartmentId { get; set; }
        public string NewApartmentNumber { get; set; }
        public string NewLocation { get; set; }
        
        [Required(ErrorMessage = "Taşıma nedeni zorunludur")]
        public string RelocationReason { get; set; }
        
        [Required(ErrorMessage = "Taşıma tarihi zorunludur")]
        public DateTime RelocationDate { get; set; }
        
        public string ResponsiblePerson { get; set; }
        public string Notes { get; set; }
    }

    // Yer değiştirme sonuç DTO
    public class RelocationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string PreviousLocation { get; set; }
        public string NewLocation { get; set; }
        public DateTime RelocationDate { get; set; }
    }

    // Bakım kaydı DTO
    public class MaintenanceRecordDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Bakım tipi zorunludur")]
        public string MaintenanceType { get; set; }
        
        [Required(ErrorMessage = "Bakım açıklaması zorunludur")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Bakım tarihi zorunludur")]
        public DateTime MaintenanceDate { get; set; }
        
        public DateTime? CompletionDate { get; set; }
        
        [Required(ErrorMessage = "Bakım durumu zorunludur")]
        public string Status { get; set; }
        
        public string PerformedBy { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
        public List<PartDto> PartsReplaced { get; set; }
    }

    // Bakım sonuç DTO
    public class MaintenanceResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int MaintenanceId { get; set; }
    }

    // Bakım programlama DTO
    public class MaintenanceScheduleDto
    {
        [Required(ErrorMessage = "Bakım tipi zorunludur")]
        public string MaintenanceType { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Planlanan tarih zorunludur")]
        public DateTime ScheduledDate { get; set; }
        
        public string AssignedTo { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Notes { get; set; }
    }

    // Bakım programı sonuç DTO
    public class MaintenanceScheduleResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ScheduleId { get; set; }
        public DateTime ScheduledDate { get; set; }
    }

    // Doküman DTO
    public class DocumentDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Doküman adı zorunludur")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string DocumentType { get; set; }
        
        [Required(ErrorMessage = "Dosya yolu/URL zorunludur")]
        public string FilePath { get; set; }
        
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; }
    }

    // Doküman sonuç DTO
    public class DocumentResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int DocumentId { get; set; }
    }

    // Parça DTO
    public class PartDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Parça adı zorunludur")]
        public string Name { get; set; }
        
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Vendor { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public DateTime? ReplacementDate { get; set; }
    }

    // Parça sonuç DTO
    public class PartResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int PartId { get; set; }
    }

    // Metrik güncelleme DTO
    public class MetricUpdateDto
    {
        [Required(ErrorMessage = "Metrik adı zorunludur")]
        public string MetricName { get; set; }
        
        [Required(ErrorMessage = "Metrik değeri zorunludur")]
        public string MetricValue { get; set; }
        
        public string Unit { get; set; }
        public DateTime MeasurementDate { get; set; } = DateTime.Now;
        public string MeasuredBy { get; set; }
        public string Notes { get; set; }
    }

    // Metrik sonuç DTO
    public class MetricResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int MetricId { get; set; }
    }

    // Fotoğraf DTO
    public class PhotoDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Fotoğraf başlığı zorunludur")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Dosya yolu/URL zorunludur")]
        public string FilePath { get; set; }
        
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; }
    }

    // Fotoğraf sonuç DTO
    public class PhotoResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int PhotoId { get; set; }
    }

    // Aktivite log DTO
    public class ActivityLogEntryDto
    {
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public string PerformedBy { get; set; }
        public string Details { get; set; }
    }

    // Kategori rapor öğesi DTO
    public class CategoryReportItemDto
    {
        public string Category { get; set; }
        public int EquipmentCount { get; set; }
        public decimal TotalValue { get; set; }
        public int MaintenanceCount { get; set; }
        public decimal MaintenanceCosts { get; set; }
    }

    // Bina rapor öğesi DTO
    public class BuildingReportItemDto
    {
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int EquipmentCount { get; set; }
        public decimal TotalValue { get; set; }
        public int MaintenanceCount { get; set; }
        public decimal MaintenanceCosts { get; set; }
    }

    // Yaklaşan bakım DTO
    public class UpcomingMaintenanceDto
    {
        public int MaintenanceId { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string AssignedTo { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }

    // Bakım maliyeti rapor DTO
    public class MaintenanceCostReportDto
    {
        public decimal TotalCost { get; set; }
        public List<MaintenanceCostByMonthDto> CostsByMonth { get; set; }
        public List<MaintenanceCostByCategoryDto> CostsByCategory { get; set; }
        public List<MaintenanceCostByTypeDto> CostsByType { get; set; }
    }

    // Ay bazında bakım maliyeti DTO
    public class MaintenanceCostByMonthDto
    {
        public string Month { get; set; }
        public decimal TotalCost { get; set; }
        public int MaintenanceCount { get; set; }
    }

    // Kategori bazında bakım maliyeti DTO
    public class MaintenanceCostByCategoryDto
    {
        public string Category { get; set; }
        public decimal TotalCost { get; set; }
        public int MaintenanceCount { get; set; }
    }

    // Tip bazında bakım maliyeti DTO
    public class MaintenanceCostByTypeDto
    {
        public string MaintenanceType { get; set; }
        public decimal TotalCost { get; set; }
        public int MaintenanceCount { get; set; }
    }
} 