using System;

namespace ResidenceManagement.Core.DTOs
{
    // Bakım kaydı listesi için kullanılan DTO
    public class MaintenanceListItemDto
    {
        // Bakım kaydı ID'si ve temel bilgiler
        public int Id { get; set; }
        public string MaintenanceNumber { get; set; } = string.Empty;
        public string MaintenanceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        
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
        public string ApartmentNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        
        // Tarih bilgileri
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsScheduled { get; set; }
        public bool IsOverdue { get; set; }
        
        // Teknisyen bilgileri
        public int? TechnicianId { get; set; }
        public string TechnicianName { get; set; } = string.Empty;
        public string TechnicianPhone { get; set; } = string.Empty;
        
        // Maliyet bilgileri
        public decimal LaborCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal TotalCost { get; set; }
        public string Currency { get; set; } = "TRY";
        
        // Özet bilgiler
        public string Description { get; set; } = string.Empty;
        public string FindingsAndRecommendations { get; set; } = string.Empty;
        
        // Bakım süresi
        public int DurationInMinutes { get; set; }
        
        // Onay bilgileri
        public bool IsApproved { get; set; }
        public string ApprovedByName { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        
        // İlişkili veriler sayaçları
        public int NumberOfReplacedParts { get; set; }
        public int NumberOfAttachments { get; set; }
        public int NumberOfComments { get; set; }
        
        // Garanti ve kalite bilgileri
        public bool HasWarrantyClaim { get; set; }
        public bool IsQualityControlled { get; set; }
        
        // Eklenme tarihi
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        
        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public string SubeAdi { get; set; } = string.Empty;
    }
} 