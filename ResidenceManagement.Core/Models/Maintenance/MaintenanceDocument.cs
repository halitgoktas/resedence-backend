using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Maintenance
{
    // Bakım belgelerini temsil eden entity sınıfı
    public class MaintenanceDocument : BaseEntity
    {
        // Temel bilgiler
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        
        [StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }
        
        [StringLength(100)]
        public string FileType { get; set; }
        
        public long FileSize { get; set; } // Bayt cinsinden
        
        [StringLength(50)]
        public string DocumentType { get; set; } // Rapor, Fatura, Manuel, Resim, vs.
        
        // Yükleme bilgileri
        public DateTime UploadDate { get; set; }
        
        public int? UploadedByUserId { get; set; }
        
        [StringLength(100)]
        public string UploadedByUserName { get; set; }
        
        // İlişkiler
        public int MaintenanceScheduleId { get; set; }
        public virtual MaintenanceSchedule MaintenanceSchedule { get; set; }
        
        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        public MaintenanceDocument()
        {
            UploadDate = DateTime.Now;
        }
    }
} 