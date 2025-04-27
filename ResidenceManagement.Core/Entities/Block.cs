using ResidenceManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Entities
{
    // Site/Rezidans içindeki bloklar için entity sınıfı
    public class Block : BaseEntity
    {
        // Blok adı (A Blok, B Blok, vs.)
        public string Name { get; set; }
        
        // Blok kodu
        public string Code { get; set; }
        
        // Blok açıklaması
        public string Description { get; set; }
        
        // Blok içindeki toplam daire sayısı
        public int TotalApartments { get; set; }
        
        // Blok içindeki toplam kat sayısı
        public int TotalFloors { get; set; }
        
        // Blok hangi siteye ait
        public int ResidenceId { get; set; }
        
        // Site/Rezidans ilişkisi
        public virtual Residence Residence { get; set; }
        
        // Blok içindeki dairelerin listesi
        public virtual ICollection<Apartment> Apartments { get; set; }
        
        // Bakım planları ilişkisi
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        
        public Block()
        {
            Apartments = new HashSet<Apartment>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
        }
    }
} 