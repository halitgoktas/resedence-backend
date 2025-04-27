using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    // Bakım maliyeti sınıfı
    public class MaintenanceCost : BaseEntity
    {
        // İlişkili bakım planı
        public int MaintenanceScheduleId { get; set; }
        public virtual MaintenanceSchedule MaintenanceSchedule { get; set; }
        
        // Maliyet tipi
        public string CostType { get; set; } // Labor, Material, Tools, ExternalService, Other
        
        // Maliyet kategori bilgileri
        public string CostCategory { get; set; } // İşçilik, Malzeme, Araç/Gereç, Dış Hizmet, Diğer
        public string CostDescription { get; set; }
        
        // Maliyet bilgileri
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";
        
        // Fatura detayları
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string SupplierName { get; set; }
        
        // Kayıt bilgileri
        public DateTime RecordDate { get; set; } = DateTime.Now;
        public int? RecordedByUserId { get; set; }
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 