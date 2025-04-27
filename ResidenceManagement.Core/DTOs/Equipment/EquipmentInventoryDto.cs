using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs
{
    // Ekipman envanter bilgilerini taşıyan DTO sınıfı
    public class EquipmentInventoryDto
    {
        // Temel bilgiler
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public string QrCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        
        // Kategori ve tür bilgileri
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        
        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; } = string.Empty;
        public int? BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string LocationNotes { get; set; } = string.Empty;
        
        // Teknik bilgiler
        public string Specifications { get; set; } = string.Empty;
        public string PowerRequirements { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; } = "kg";
        
        // Maliyet ve değer bilgileri
        public decimal? PurchasePrice { get; set; }
        public string Currency { get; set; } = "TRY";
        public decimal? CurrentValue { get; set; }
        public DateTime? ValuationDate { get; set; }
        public decimal? ReplacementCost { get; set; }
        public string AssetTag { get; set; } = string.Empty;
        
        // Tarih bilgileri
        public DateTime? PurchaseDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Garanti ve sözleşme bilgileri
        public DateTime? WarrantyExpiryDate { get; set; }
        public int? WarrantyDurationMonths { get; set; }
        public string WarrantyProvider { get; set; } = string.Empty;
        public string WarrantyNotes { get; set; } = string.Empty;
        public string MaintenanceContractNumber { get; set; } = string.Empty;
        public string MaintenanceProvider { get; set; } = string.Empty;
        
        // Satıcı ve üretici bilgileri
        public int? VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string VendorContact { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public string ManufacturerContact { get; set; } = string.Empty;
        
        // Atanan kullanıcı bilgileri
        public int? AssignedToId { get; set; }
        public string AssignedToName { get; set; } = string.Empty;
        public string AssignedToPhone { get; set; } = string.Empty;
        public DateTime? AssignmentDate { get; set; }
        
        // Ekipman yükümlülükleri
        public string ResponsibleDepartment { get; set; } = string.Empty;
        public string ResponsiblePerson { get; set; } = string.Empty;
        
        // Bakım ve kontrol bilgileri
        public int? MaintenanceFrequencyDays { get; set; }
        public string MaintenanceInstructions { get; set; } = string.Empty;
        public string OperatingInstructions { get; set; } = string.Empty;
        public string SafetyGuidelines { get; set; } = string.Empty;
        public string CertificateNumber { get; set; } = string.Empty;
        public DateTime? CertificationExpiryDate { get; set; }
        
        // Fotoğraf ve döküman bilgileri
        public string MainImageUrl { get; set; } = string.Empty;
        public List<string> AdditionalImageUrls { get; set; } = new List<string>();
        public List<string> DocumentUrls { get; set; } = new List<string>();
        
        // Kalite ve onarım bilgileri
        public decimal? LifeExpectancyYears { get; set; }
        public string Condition { get; set; } = string.Empty;
        public int? MaintenanceCount { get; set; }
        public int? RepairCount { get; set; }
        public DateTime? LastFailureDate { get; set; }
        public DateTime? LastRepairDate { get; set; }
        
        // İlgili parçalar ve bileşenler
        public List<EquipmentPartDto> Parts { get; set; } = new List<EquipmentPartDto>();
        
        // Son işlem bilgileri
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime? LastUpdatedDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        
        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
    
    // Ekipman parça bilgilerini taşıyan DTO sınıfı
    public class EquipmentPartDto
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Quantity { get; set; }
        public string QuantityUnit { get; set; } = string.Empty;
        public DateTime? InstallationDate { get; set; }
        public DateTime? ReplacementDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Currency { get; set; } = "TRY";
        public string Status { get; set; } = string.Empty;
        public bool IsReplaceable { get; set; }
        public bool IsConsumable { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
} 