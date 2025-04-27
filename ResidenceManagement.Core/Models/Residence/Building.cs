using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Rezidans veya sitedeki binaları temsil eden entity sınıfı
    public class Building : BaseEntity
    {
        public string BuildingName { get; set; }
        public string BuildingCode { get; set; }
        public string Address { get; set; }
        public int TotalFloors { get; set; }
        public int TotalApartments { get; set; }
        public DateTime ConstructionDate { get; set; }
        public decimal TotalArea { get; set; }
        public bool HasElevator { get; set; }
        public bool HasGenerator { get; set; }
        public bool HasGarage { get; set; }
        public int TotalParkingSpaces { get; set; }
        public string BuildingType { get; set; } // Rezidans, Apartman, Villa vs.
        public string Notes { get; set; }
        public int ResidenceComplexId { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual ResidenceComplex ResidenceComplex { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<CommonArea> CommonAreas { get; set; }

        public Building()
        {
            Apartments = new HashSet<Apartment>();
            CommonAreas = new HashSet<CommonArea>();
        }
    }
} 