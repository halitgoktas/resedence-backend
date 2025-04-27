using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
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
        
        [StringLength(1000)]
        public string Description { get; set; }
        
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
        
        // Teknik detaylar
        [StringLength(1000)]
        public string ActionTaken { get; set; }
        
        [StringLength(1000)]
        public string ProblemDescription { get; set; }
        
        [StringLength(1000)]
        public string SolutionDescription { get; set; }
        
        // Süre ve maliyet bilgileri
        public decimal? TimeSpentMinutes { get; set; }
        public decimal? CostAmount { get; set; }
        
        [StringLength(500)]
        public string CostDescription { get; set; }
        
        // Değerlendirme
        public int? Rating { get; set; } // 1-5 arası
        
        [StringLength(1000)]
        public string FeedbackNotes { get; set; }
        
        // Aktivite türü ve içerik
        public string Activity { get; set; }
        public string Notes { get; set; }
        public string PerformedBy { get; set; }
        
        // Servisin ihtiyaç duyduğu özellikler
        public string ActionType { get; set; }
        public int? PerformedByUserId { get; set; }
        public DateTime? ActionDate { get; set; }
        
        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        public MaintenanceLog()
        {
            LogDate = DateTime.Now;
        }
    }
} 