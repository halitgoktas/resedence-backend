using System;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Rezidans veya sitede oturan kişilerin araçlarını temsil eden entity sınıfı
    public class Vehicle : BaseEntity
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string VehicleType { get; set; } // Otomobil, SUV, Motosiklet vb.
        public string ParkingSpaceNumber { get; set; } // Tahsis edilen otopark numarası
        public bool HasParkingPermit { get; set; }
        public DateTime PermitStartDate { get; set; }
        public DateTime? PermitEndDate { get; set; }
        public string Notes { get; set; }
        public int ResidentId { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual Resident Resident { get; set; }
    }
} 