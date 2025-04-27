using System;

namespace ResidenceManagement.Core.DTOs
{
    // Ekipman listeleme ekranlarında kullanılacak özet bilgi sınıfı
    public class EquipmentListItemDto
    {
        // Temel bilgiler
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public string ModelNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        
        // Kategori ve tür bilgileri
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        
        // Statü bilgileri
        public string Status { get; set; } = string.Empty;
        public string MaintenanceStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsUnderWarranty { get; set; }
        public bool IsMaintenanceOverdue { get; set; }
        public bool IsCritical { get; set; }
        
        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; } = string.Empty;
        public int? BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int? ApartmentId { get; set; }
        public string ApartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        
        // Finansal bilgiler
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; } = "TRY";
        public decimal CurrentValue { get; set; }
        public DateTime? PurchaseDate { get; set; }
        
        // Bakım bilgileri
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string? MaintenanceProviderName { get; set; }
        
        // Garanti bilgileri
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string? WarrantyProvider { get; set; }
        
        // Sorumluluk bilgileri
        public int? AssignedToId { get; set; }
        public string AssignedToName { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        
        // Tarih bilgileri
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        
        // Gerekli firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 