using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs
{
    // Bakım kaydı için veri taşıma nesnesi
    public class MaintenanceRecordDTO
    {
        // Temel bilgiler
        public int Id { get; set; }
        public string RecordNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // İlişkili ekipman bilgileri
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string EquipmentSerialNumber { get; set; } = string.Empty;
        public string EquipmentCategory { get; set; } = string.Empty;
        public string EquipmentLocation { get; set; } = string.Empty;
        
        // Bakım planı bilgileri
        public int? MaintenanceScheduleId { get; set; }
        public string MaintenanceScheduleNumber { get; set; } = string.Empty;
        public bool IsScheduled { get; set; }
        
        // Bakım türü ve detayları
        public string MaintenanceType { get; set; } = string.Empty; // Periyodik, Önleyici, Arıza Giderme, Acil
        public string Priority { get; set; } = "Normal"; // Düşük, Normal, Yüksek, Kritik
        
        // Bakım zaman bilgileri
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int ActualDuration { get; set; } // Dakika olarak
        
        // Teknisyen bilgileri
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; } = string.Empty;
        public string TechnicianNotes { get; set; } = string.Empty;
        
        // Bakım sonuçları
        public string Status { get; set; } = "Tamamlandı"; // Tamamlandı, Kısmen Tamamlandı, Başarısız
        public string Findings { get; set; } = string.Empty;
        public string ActionsTaken { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        
        // Kontrol listesi sonuçları
        public List<MaintenanceChecklistResultDTO> ChecklistResults { get; set; } = new List<MaintenanceChecklistResultDTO>();
        
        // Kullanılan malzemeler
        public List<MaintenanceMaterialUsedDTO> MaterialsUsed { get; set; } = new List<MaintenanceMaterialUsedDTO>();
        
        // Maliyet bilgileri
        public decimal LaborCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal AdditionalCosts { get; set; }
        public string AdditionalCostDetails { get; set; } = string.Empty;
        public decimal TotalCost => LaborCost + MaterialCost + AdditionalCosts;
        public string Currency { get; set; } = "TRY";
        
        // Performans ölçümleri
        public List<MaintenancePerformanceMeasurementDTO> PerformanceMeasurements { get; set; } = new List<MaintenancePerformanceMeasurementDTO>();
        
        // Sorunlar ve çözümler
        public List<MaintenanceIssueDTO> Issues { get; set; } = new List<MaintenanceIssueDTO>();
        
        // Ekler
        public List<string> PhotoUrls { get; set; } = new List<string>();
        public List<string> DocumentUrls { get; set; } = new List<string>();
        
        // Onay bilgileri
        public bool RequiresApproval { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedByUserId { get; set; }
        public string ApprovedByUserName { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        public string ApprovalNotes { get; set; } = string.Empty;
        
        // Garanti bilgileri
        public bool WarrantyClaim { get; set; }
        public string WarrantyClaimNumber { get; set; } = string.Empty;
        public string WarrantyProvider { get; set; } = string.Empty;
        
        // Sıradaki bakım bilgileri
        public DateTime? NextMaintenanceDueDate { get; set; }
        
        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Kayıt bilgileri
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
    }
    
    // Bakım kontrol listesi sonuç öğesi
    public class MaintenanceChecklistResultDTO
    {
        public int Id { get; set; }
        public int ChecklistItemId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Tamam, Sorun, Yapılmadı
        public string ActualValue { get; set; } = string.Empty;
        public string ExpectedValue { get; set; } = string.Empty;
        public bool IsWithinAcceptableRange { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
    
    // Bakımda kullanılan malzeme
    public class MaintenanceMaterialUsedDTO
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public string UnitOfMeasure { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => QuantityUsed * UnitPrice;
        public string Currency { get; set; } = "TRY";
        public bool IsWarrantyPart { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
    
    // Performans ölçümü
    public class MaintenancePerformanceMeasurementDTO
    {
        public int Id { get; set; }
        public string MeasurementType { get; set; } = string.Empty;
        public string MeasurementName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string UnitOfMeasure { get; set; } = string.Empty;
        public string ExpectedRange { get; set; } = string.Empty;
        public bool IsWithinExpectedRange { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
    
    // Bakım sırasında tespit edilen sorun
    public class MaintenanceIssueDTO
    {
        public int Id { get; set; }
        public string IssueType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Severity { get; set; } = "Normal"; // Düşük, Normal, Yüksek, Kritik
        public string ResolutionStatus { get; set; } = string.Empty; // Çözüldü, Çözülmedi, İleri Tarihte Çözülecek
        public string ResolutionDetails { get; set; } = string.Empty;
        public DateTime? ResolutionDate { get; set; }
        public bool RequiresFollowUp { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; } = string.Empty;
    }
} 