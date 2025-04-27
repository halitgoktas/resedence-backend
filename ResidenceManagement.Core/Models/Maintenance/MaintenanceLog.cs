using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Maintenance
{
    // Bakım işlemleri için log kayıtları sınıfı
    public class MaintenanceLog : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string LogType { get; set; } // StatusUpdate, NoteAdded, TaskCompleted, vs.
        
        [Required]
        [StringLength(255)]
        public string Message { get; set; }
        
        [StringLength(1000)]
        public string Details { get; set; }
        
        public DateTime LogDate { get; set; }
        
        // İlgili kullanıcı bilgileri
        public int? UserId { get; set; }
        
        [StringLength(100)]
        public string UserName { get; set; }
        
        // İlişkiler
        public int MaintenanceScheduleId { get; set; }
        public virtual MaintenanceSchedule MaintenanceSchedule { get; set; }
        
        // Ek bilgiler
        [StringLength(50)]
        public string PreviousStatus { get; set; }
        
        [StringLength(50)]
        public string NewStatus { get; set; }
        
        public decimal? TimeSpentMinutes { get; set; }
        
        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        public MaintenanceLog()
        {
            LogDate = DateTime.Now;
        }
    }
} 