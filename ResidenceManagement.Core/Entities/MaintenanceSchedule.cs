using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Entities
{
    // Bakım planı entity sınıfı
    public class MaintenanceSchedule : BaseEntity
    {
        // Temel özellikler
        public string Title { get; set; }
        public string Description { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan? ScheduledTime { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimatedDuration { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; }
        public string CompletionNotes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public int? ActualDuration { get; set; }
        public string CancellationReason { get; set; }
        public string RequiredMaterials { get; set; }
        public string RequiredTools { get; set; }
        public string RequiredSkills { get; set; }
        public string PreMaintenanceChecklist { get; set; }
        public string MaintenanceSteps { get; set; }
        public string PostMaintenanceChecklist { get; set; }
        public string SafetyPrecautions { get; set; }
        public string EmergencyProcedures { get; set; }

        // Maliyet ve bütçe özellikleri
        public string CostCenter { get; set; }
        public string BudgetCode { get; set; }

        // Bildirim özellikleri
        public string NotificationEmail { get; set; }
        public string NotificationSMS { get; set; }
        public bool SendReminders { get; set; }
        public int? ReminderDaysBefore { get; set; }

        // Tekrarlama özellikleri
        public string RecurrencePattern { get; set; }
        public int? RecurrenceInterval { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public int? ParentMaintenanceId { get; set; }
        public virtual MaintenanceSchedule ParentMaintenance { get; set; }

        // İlişkili özellikler
        public int? ResidenceId { get; set; }
        public virtual Residence Residence { get; set; }
        
        public int? BlockId { get; set; }
        public virtual Block Block { get; set; }
        
        public int? ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        
        public int? EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }

        public int? AssignedToUserId { get; set; }
        public virtual User AssignedToUser { get; set; }
        public string AssignedToUserName { get; set; }

        public int? ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }

        public int? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        public int? UpdatedByUserId { get; set; }
        public virtual User UpdatedByUser { get; set; }

        public int? CompletedByUserId { get; set; }
        public virtual User CompletedByUser { get; set; }
        public string CompletedByUserName { get; set; }

        // Koleksiyonlar
        public virtual ICollection<MaintenanceChecklistItem> ChecklistItems { get; set; }
        public virtual ICollection<MaintenanceDocument> Documents { get; set; }
        public virtual ICollection<MaintenanceLog> Logs { get; set; }
        public virtual ICollection<MaintenanceSchedule> RecurrenceInstances { get; set; }

        // Çok kiracılı yapı için
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Yeni özellikler
        public string RecurrenceFrequency { get; set; } // Günlük, Haftalık, Aylık, Yıllık
        public bool SendNotification { get; set; }
        public int? NotificationDaysInAdvance { get; set; }
        public string PriorityDescription { get; set; }
        public string MaintenanceCategory { get; set; }
        public string MaintenanceSubCategory { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }

        public MaintenanceSchedule()
        {
            ChecklistItems = new List<MaintenanceChecklistItem>();
            Documents = new List<MaintenanceDocument>();
            Logs = new List<MaintenanceLog>();
            Status = "Planlandı"; // Varsayılan durum
            Priority = "Normal"; // Varsayılan öncelik
            SendReminders = true;
            ReminderDaysBefore = 1;
            EstimatedCost = 0; // Varsayılan tahmini maliyet
            CreatedDate = DateTime.Now;
            Tags = new List<string>();
            CustomFields = new Dictionary<string, string>();
            SendNotification = true;
            NotificationDaysInAdvance = 1;
        }
    }
} 