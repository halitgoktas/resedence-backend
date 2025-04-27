using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Rezidans veya sitedeki daireleri temsil eden entity sınıfı
    public class Apartment : BaseEntity
    {
        public string ApartmentNumber { get; set; }
        public string Block { get; set; }
        public int Floor { get; set; }
        public decimal SquareMeters { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public bool HasBalcony { get; set; }
        public string ApartmentType { get; set; } // 1+1, 2+1, 3+1 vb.
        public string Status { get; set; } // Boş, Dolu, Satılık, Kiralık vb.
        public decimal MonthlyDues { get; set; } // Aylık aidat tutarı
        public decimal? RentAmount { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime? LastOccupiedDate { get; set; }
        public string Notes { get; set; }
        public int BuildingId { get; set; }
        public int? OwnerId { get; set; } // Daire sahibi
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual ICollection<Resident> Residents { get; set; }
        
        public Apartment()
        {
            Residents = new HashSet<Resident>();
        }
    }
} 