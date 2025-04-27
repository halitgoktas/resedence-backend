using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Models.Common;
using ResidenceManagement.Core.Models.Residence;
using ResidenceManagement.Core.Models.Equipment;

namespace ResidenceManagement.Core.Models.Maintenance
{
    // Bakım takvimi/planı entity sınıfı
    public class MaintenanceSchedule : BaseEntity
    {
        // Temel bilgiler
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string MaintenanceType { get; set; }

        [StringLength(30)]
        public string Status { get; set; } // Planlandı, Beklemede, Tamamlandı, İptal Edildi

        [StringLength(50)]
        public string Priority { get; set; } // Düşük, Normal, Yüksek, Kritik

        // Tarih bilgileri
        public DateTime ScheduledDate { get; set; }
        public TimeSpan? ScheduledTime { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? NextScheduledDate { get; set; }
        public int? ActualDurationMinutes { get; set; }

        // Maliyet bilgileri
        [Column(TypeName = "decimal(18,2)")]
        public decimal? EstimatedCost { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualCost { get; set; }

        // İlişkiler - Lokasyon
        public int? ResidenceId { get; set; }
        public virtual Residence.Residence Residence { get; set; }
        
        public int? BlockId { get; set; }
        public virtual Block Block { get; set; }
        
        public int? ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        
        public int? CommonAreaId { get; set; }
        public virtual CommonArea CommonArea { get; set; }

        // İlişkiler - Ekipman
        public int? EquipmentId { get; set; }
        public virtual Equipment.Equipment Equipment { get; set; }
        
        public int? EquipmentCategoryId { get; set; }
        public virtual EquipmentCategory EquipmentCategory { get; set; }

        // İlişkiler - Görevlendirme
        public int? AssignedToUserId { get; set; }
        // ApplicationUser sınıfı eksik olduğu için referansı geçici olarak kaldırıldı
        
        [StringLength(100)]
        public string AssignedToUserName { get; set; }
        
        public int? MaintenanceTeamId { get; set; }
        // MaintenanceTeam sınıfı eksik olduğu için referansı geçici olarak kaldırıldı

        // Tekrarlama ayarları
        public bool IsRecurring { get; set; }
        
        [StringLength(30)]
        public string RecurrencePattern { get; set; } // Günlük, Haftalık, Aylık, Yıllık
        
        public int? RecurrenceInterval { get; set; } // 1 = Her gün, 2 = Her iki günde bir, vb.
        
        [StringLength(100)]
        public string RecurrenceDays { get; set; } // Haftalık tekrar için günler (Pazartesi, Salı, vb.)
        
        public DateTime? RecurrenceEndDate { get; set; }
        
        public int? ParentMaintenanceId { get; set; } // Tekrarlanan bakımın ana kaydı

        // Notlar ve tamamlama bilgileri
        [StringLength(1000)]
        public string MaintenanceNotes { get; set; }
        
        [StringLength(1000)]
        public string CompletionNotes { get; set; }
        
        [StringLength(500)]
        public string CancellationReason { get; set; }

        // Uyarı ve bildirim ayarları
        public bool SendNotification { get; set; }
        public int ReminderDaysBefore { get; set; }
        public bool NotifyResidents { get; set; }

        // İlişkili koleksiyonlar
        // İlgili sınıflar eksik olduğu için geçici olarak yorum satırına alındı
        //public virtual ICollection<MaintenanceChecklistItem> ChecklistItems { get; set; }
        public virtual ICollection<MaintenanceDocument> Documents { get; set; }
        public virtual ICollection<MaintenanceLog> Logs { get; set; }
        //public virtual ICollection<MaintenanceCost> Costs { get; set; }
        //public virtual ICollection<MaintenanceRequiredMaterial> RequiredMaterials { get; set; }

        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        public MaintenanceSchedule()
        {
            //ChecklistItems = new HashSet<MaintenanceChecklistItem>();
            Documents = new HashSet<MaintenanceDocument>();
            Logs = new HashSet<MaintenanceLog>();
            //Costs = new HashSet<MaintenanceCost>();
            //RequiredMaterials = new HashSet<MaintenanceRequiredMaterial>();
            Status = "Planlandı";
            Priority = "Normal";
            SendNotification = true;
            ReminderDaysBefore = 1;
            EstimatedDurationMinutes = 60;
            EstimatedCost = 0;
        }
    }
} 