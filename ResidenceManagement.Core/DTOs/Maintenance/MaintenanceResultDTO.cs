using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs
{
    // Bakım sonuçlarını döndüren DTO sınıfı
    public class MaintenanceResultDto
    {
        // Bakım bilgileri
        public int Id { get; set; }
        public string MaintenanceNumber { get; set; } = string.Empty;
        public string MaintenanceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        
        // Tarih bilgileri
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? NextScheduledDate { get; set; }
        public TimeSpan? Duration { get; set; }
        
        // Ekipman bilgileri
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string EquipmentSerialNumber { get; set; } = string.Empty;
        public string EquipmentCategory { get; set; } = string.Empty;
        public string EquipmentModel { get; set; } = string.Empty;
        
        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; } = string.Empty;
        public int? BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int? ApartmentId { get; set; }
        public string ApartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        
        // Teknisyen bilgileri
        public int? TechnicianId { get; set; }
        public string TechnicianName { get; set; } = string.Empty;
        public string TechnicianPhone { get; set; } = string.Empty;
        
        // Maliyet bilgileri
        public decimal? LaborCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? AdditionalCost { get; set; }
        public decimal TotalCost { get; set; }
        public string Currency { get; set; } = "TRY";
        
        // İş ve malzeme detayları
        public string Description { get; set; } = string.Empty;
        public string WorkPerformed { get; set; } = string.Empty;
        public string FindingsAndRecommendations { get; set; } = string.Empty;
        public List<MaintenanceMaterialDto> Materials { get; set; } = new List<MaintenanceMaterialDto>();
        public List<MaintenanceTaskDto> Tasks { get; set; } = new List<MaintenanceTaskDto>();
        
        // Onay bilgileri
        public bool IsApproved { get; set; }
        public int? ApprovedById { get; set; }
        public string ApprovedByName { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        
        // Garanti bilgileri
        public bool HasWarrantyClaim { get; set; }
        public string WarrantyClaimNumber { get; set; } = string.Empty;
        public string WarrantyClaimStatus { get; set; } = string.Empty;
        
        // Performans ölçümleri
        public List<MaintenancePerformanceDto> PerformanceMeasurements { get; set; } = new List<MaintenancePerformanceDto>();
        
        // Oluşturma bilgileri
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        
        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
    
    // Bakım malzemeleri DTO
    public class MaintenanceMaterialDto
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public string MaterialCode { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "TRY";
    }
    
    // Bakım görevleri DTO
    public class MaintenanceTaskDto
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string CompletedByName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
    
    // Bakım performans ölçümü DTO
    public class MaintenancePerformanceDto
    {
        public int Id { get; set; }
        public string MeasurementName { get; set; } = string.Empty;
        public string MeasurementUnit { get; set; } = string.Empty;
        public decimal ExpectedValue { get; set; }
        public decimal ActualValue { get; set; }
        public bool IsWithinRange { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
} 