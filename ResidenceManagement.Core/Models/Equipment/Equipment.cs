using System;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Equipment
{
    // Ekipman bilgilerini temsil eden entity sınıfı
    public class Equipment : BaseEntity
    {
        // Temel bilgiler
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        
        // Kategori ve sınıflandırma
        public int? CategoryId { get; set; }
        public virtual EquipmentCategory Category { get; set; }
        public string Type { get; set; }
        
        // Konum bilgileri
        public int? ResidenceId { get; set; }
        public int? BlockId { get; set; }
        public int? ApartmentId { get; set; }
        public int? CommonAreaId { get; set; }
        public string LocationDetails { get; set; }
        
        // Durum bilgileri
        public string Status { get; set; } // Aktif, Pasif, Bakımda, Arızalı
        public string Condition { get; set; } // Yeni, İyi, Orta, Kötü
        
        // Teknik bilgiler
        public string Specifications { get; set; }
        public string OperatingInstructions { get; set; }
        public string SafetyInstructions { get; set; }
        
        // Satın alma bilgileri
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchaseCost { get; set; }
        public string Supplier { get; set; }
        public string SupplierContact { get; set; }
        public DateTime? WarrantyExpiration { get; set; }
        
        // Bakım bilgileri
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string MaintenanceRequirements { get; set; }
        public string MaintenanceSchedule { get; set; } // Günlük, Haftalık, Aylık, Yıllık
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 