using ResidenceManagement.Core.Entities.Base;
using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Financial;
using ResidenceManagement.Core.Models.Financial;
using ResidenceManagement.Core.Entities.Management;

namespace ResidenceManagement.Core.Entities
{
    // Site/Rezidans bilgilerini tutan entity sınıfı
    public class Residence : BaseEntity
    {
        // Site/Rezidans adı
        public string Name { get; set; }
        
        // Adres bilgisi
        public string Address { get; set; }
        
        // İl
        public string City { get; set; }
        
        // İlçe
        public string District { get; set; }
        
        // Posta kodu
        public string PostalCode { get; set; }
        
        // Vergi numarası
        public string TaxNumber { get; set; }
        
        // Vergi dairesi
        public string TaxOffice { get; set; }
        
        // Toplam blok sayısı
        public int TotalBlocks { get; set; }
        
        // Toplam daire sayısı
        public int TotalApartments { get; set; }
        
        // Yönetim şirketi ID'si
        public int? ManagementCompanyId { get; set; }
        
        // Site kuruluş tarihi
        public DateTime EstablishmentDate { get; set; }
        
        // Site yöneticisi kullanıcı ID'si
        public int ManagerId { get; set; }
        public virtual User Manager { get; set; }
        
        // İlişkiler
        // Site'deki bloklar
        public virtual ICollection<Block> Blocks { get; set; }
        
        // Site/Rezidans için tanımlı aidatlar
        public virtual ICollection<Dues> Dues { get; set; }
        
        // Sitenin genel aidat tanımları
        public virtual ICollection<DuesDefinition> DuesDefinitions { get; set; }
        
        // Sitenin yöneticileri
        public virtual ICollection<ResidenceManager> Managers { get; set; }
        
        // Bakım planları ilişkisi
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        
        public Residence()
        {
            Blocks = new HashSet<Block>();
            Dues = new HashSet<Dues>();
            DuesDefinitions = new HashSet<DuesDefinition>();
            Managers = new HashSet<ResidenceManager>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
        }
    }
} 