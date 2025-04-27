using System;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Ortak alanları temsil eden entity sınıfı
    public class CommonArea : BaseEntity
    {
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string AreaType { get; set; } // Havuz, Spor salonu, Sosyal tesis, vb.
        public decimal Area { get; set; }
        public string Location { get; set; } // Binanın içi, dışı, kat numarası vb.
        public int Capacity { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string UsageRules { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public bool RequiresReservation { get; set; }
        public decimal MaintenanceCost { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public string Notes { get; set; }
        
        // İlişkiler
        public int? ResidenceComplexId { get; set; }
        public int? BuildingId { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        public virtual ResidenceComplex ResidenceComplex { get; set; }
        public virtual Building Building { get; set; }
    }
} 