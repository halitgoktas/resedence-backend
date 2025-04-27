using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Entities.Property
{
    // Site modeli, bir konut sitesini temsil eder
    public class Site : BaseLookupEntity
    {
        public string SiteName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public int TotalBlocks { get; set; }
        public int TotalApartments { get; set; }
        public decimal TotalArea { get; set; }
        public decimal GreenArea { get; set; }
        public int ParkingCapacity { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public int? SiteManagerId { get; set; }
        public int? ManagementCompanyId { get; set; }
        public SiteStatus Status { get; set; }
        public string Amenities { get; set; } // JSON
        public DuesCalculationType DuesCalculationType { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Block> Blocks { get; set; }
    }
} 