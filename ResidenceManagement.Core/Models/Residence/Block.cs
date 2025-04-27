using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Site/Rezidans içindeki blok bilgilerini tutan entity sınıfı
    public class Block : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int FloorCount { get; set; }
        public int ApartmentCount { get; set; }
        public DateTime ConstructionDate { get; set; }
        public int TotalArea { get; set; }
        public string Description { get; set; }
        public int ResidenceId { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual Residence Residence { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }

        public Block()
        {
            Apartments = new HashSet<Apartment>();
        }
    }
} 