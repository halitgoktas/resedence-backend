using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Enums;
using ResidenceManagement.Core.Models.Genel;

namespace ResidenceManagement.Core.Entities
{
    /// <summary>
    /// Bakım planı sınıfı
    /// </summary>
    public class MaintenancePlan
    {
        /// <summary>
        /// Bakım planı ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bakım planı adı
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Bakım açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Bakım kontrat numarası
        /// </summary>
        public string MaintenanceContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Bakım sağlayıcı firma
        /// </summary>
        public string MaintenanceProvider { get; set; } = string.Empty;

        /// <summary>
        /// Bakım tipi (Periyodik, Arıza giderme vb.)
        /// </summary>
        public MaintenanceType MaintenanceType { get; set; }

        /// <summary>
        /// Bakım sıklığı (Günlük, Haftalık, Aylık, Yıllık)
        /// </summary>
        public MaintenanceFrequency Frequency { get; set; }

        /// <summary>
        /// Bakım başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Bakım bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Son bakım tarihi
        /// </summary>
        public DateTime? LastMaintenanceDate { get; set; }

        /// <summary>
        /// Son bakımı yapan kişi
        /// </summary>
        public string LastMaintenanceBy { get; set; } = string.Empty;

        /// <summary>
        /// Sonraki planlanan bakım tarihi
        /// </summary>
        public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
        /// Bakım durumu
        /// </summary>
        public MaintenanceStatus Status { get; set; }

        /// <summary>
        /// Bakım planına bağlı ekipman ID'si
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Bakım planına bağlı ekipman
        /// </summary>
        public virtual Equipment? Equipment { get; set; }

        /// <summary>
        /// Bakım planına bağlı yapı ID'si (Bina, blok, vb.)
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// Bakım planına bağlı yapı
        /// </summary>
        public virtual Building? Building { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedById { get; set; }

        /// <summary>
        /// Aktif/Pasif durumu
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Bakım planı görevleri
        /// </summary>
        public virtual ICollection<MaintenanceTask> Tasks { get; set; } = new List<MaintenanceTask>();
    }

    /// <summary>
    /// Bakım görevi sınıfı
    /// </summary>
    public class MaintenanceTask
    {
        /// <summary>
        /// Görev ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Görev adı
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Görev açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Görev sırası
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Tamamlanma durumu
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Bağlı olduğu bakım planı ID
        /// </summary>
        public int MaintenancePlanId { get; set; }

        /// <summary>
        /// Bağlı olduğu bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
    }
} 