using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Site/Rezidans bilgilerini tutan entity sınıfı
    public class Residence : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public DateTime ConstructionDate { get; set; }
        public int TotalArea { get; set; }
        public string Description { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual ICollection<Block> Blocks { get; set; }

        public Residence()
        {
            Blocks = new HashSet<Block>();
        }
    }
} 