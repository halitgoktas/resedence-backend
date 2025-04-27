using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Equipment
{
    // Ekipman kategorisi bilgilerini temsil eden entity sınıfı
    public class EquipmentCategory : BaseEntity
    {
        // Temel bilgiler
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        // Hiyerarşi
        public int? ParentCategoryId { get; set; }
        public virtual EquipmentCategory ParentCategory { get; set; }
        
        // İlişkilendirilen ekipmanlar
        public virtual ICollection<Equipment> Equipments { get; set; }
        
        // Kategori özellikleri
        public string DefaultMaintenanceSchedule { get; set; } // Günlük, Haftalık, Aylık, Yıllık
        public int? DefaultMaintenanceIntervalDays { get; set; }
        public string MaintenanceRequirements { get; set; }
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        public EquipmentCategory()
        {
            Equipments = new HashSet<Equipment>();
        }
    }
} 