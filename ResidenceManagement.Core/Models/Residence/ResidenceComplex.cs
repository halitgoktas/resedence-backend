using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;
using ResidenceManagement.Core.Models.Staff;

namespace ResidenceManagement.Core.Models.Residence
{
    // Rezidans ve site komplekslerini temsil eden entity sınıfı
    public class ResidenceComplex : BaseEntity
    {
        public string ComplexName { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal TotalArea { get; set; }
        public int TotalBuildings { get; set; }
        public int TotalApartments { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ComplexType { get; set; } // Rezidans, Site, Karma vb.
        public string SecurityType { get; set; } // Güvenlik düzeyi/türü
        public bool HasPool { get; set; }
        public bool HasGym { get; set; }
        public bool HasSocialAreas { get; set; }
        public bool HasSportAreas { get; set; }
        public bool HasChildrenPlayground { get; set; }
        public string Notes { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<CommonArea> CommonAreas { get; set; }
        public virtual ICollection<Staff.Staff> StaffMembers { get; set; }

        public ResidenceComplex()
        {
            Buildings = new HashSet<Building>();
            CommonAreas = new HashSet<CommonArea>();
            StaffMembers = new HashSet<Staff.Staff>();
        }
    }
} 