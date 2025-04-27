using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Financial;

namespace ResidenceManagement.Core.Entities
{
    // Daire/Apartman için entity sınıfı
    public class Apartment : BaseEntity
    {
        // Daire numarası
        public string Number { get; set; }
        
        // Daire tipi (1+1, 2+1, 3+1, vb.)
        public string Type { get; set; }
        
        // Dairenin hangi katta olduğu
        public int Floor { get; set; }
        
        // Metrekare (brüt)
        public double GrossArea { get; set; }
        
        // Metrekare (net)
        public double NetArea { get; set; }
        
        // Dairenin hangi blokta olduğu
        public int BlockId { get; set; }
        
        // Dairenin hangi siteye ait olduğu
        public int ResidenceId { get; set; }
        
        // Dairenin doluluk durumu
        public OccupancyStatus OccupancyStatus { get; set; }
        
        // Dairenin kullanım amacı (konut, iş yeri, vb.)
        public UsageType UsageType { get; set; }
        
        // Daire sahibi bilgileri
        public int? OwnerId { get; set; }
        
        // Kiracı bilgileri (varsa)
        public int? TenantId { get; set; }
        
        // Blok ilişkisi
        public virtual Block Block { get; set; }
        
        // Site/Rezidans ilişkisi
        public virtual Residence Residence { get; set; }
        
        // Daire sahibi ilişkisi
        public virtual User Owner { get; set; }
        
        // Kiracı ilişkisi
        public virtual User Tenant { get; set; }
        
        // Daireye ait aidat ödemeleri
        public virtual ICollection<Dues> DuesPayments { get; set; }
        
        // Daireye ait bakım planları
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        
        public Apartment()
        {
            OccupancyStatus = OccupancyStatus.Empty;
            UsageType = UsageType.Residential;
            DuesPayments = new HashSet<Dues>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
        }
    }
} 