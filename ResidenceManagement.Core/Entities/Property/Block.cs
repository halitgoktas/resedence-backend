using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Entities.Property
{
    // Blok modeli, site içerisindeki blokları temsil eder
    public class Block : BaseLookupEntity
    {
        public int SiteId { get; set; }
        public string BlockName { get; set; }
        public string Description { get; set; }
        public int TotalFloors { get; set; }
        public int TotalApartments { get; set; }
        public int ApartmentsPerFloor { get; set; }
        public decimal BuildingArea { get; set; }
        public int ConstructionYear { get; set; }
        public BlockType BlockType { get; set; }
        public int? BlockManagerId { get; set; }
        public int NumberOfElevators { get; set; }
        public string HeatingSystem { get; set; }
        
        // Navigation Properties
        public virtual Site Site { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
} 