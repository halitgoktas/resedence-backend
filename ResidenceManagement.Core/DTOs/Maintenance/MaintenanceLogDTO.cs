using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım işlem kayıtları için veri transfer nesnesi
    /// </summary>
    public class MaintenanceLogDTO
    {
        /// <summary>
        /// Log kaydı ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bağlı olduğu bakım planı ID
        /// </summary>
        [Required(ErrorMessage = "Bakım planı ID zorunludur")]
        public int MaintenanceScheduleId { get; set; }

        /// <summary>
        /// İşlem türü (Başlatma, Güncelleme, Tamamlama, İptal vb.)
        /// </summary>
        [Required(ErrorMessage = "İşlem türü zorunludur")]
        [StringLength(50, ErrorMessage = "İşlem türü en fazla 50 karakter olabilir")]
        public string ActivityType { get; set; }

        /// <summary>
        /// İşlem
        /// </summary>
        [Required(ErrorMessage = "İşlem zorunludur")]
        [StringLength(100, ErrorMessage = "İşlem en fazla 100 karakter olabilir")]
        public string Activity { get; set; }

        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        [Required(ErrorMessage = "İşlem açıklaması zorunludur")]
        [StringLength(1000, ErrorMessage = "İşlem açıklaması en fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        /// <summary>
        /// İşlem notları
        /// </summary>
        [StringLength(1000, ErrorMessage = "İşlem notları en fazla 1000 karakter olabilir")]
        public string Notes { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime LogDate { get; set; } = DateTime.Now;

        /// <summary>
        /// İşlemi yapan kullanıcı ID
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı ID zorunludur")]
        public int PerformedByUserId { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcı
        /// </summary>
        public string PerformedBy { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string PerformedByUserName { get; set; }

        /// <summary>
        /// İşlem türü
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        [Required(ErrorMessage = "Firma ID zorunludur")]
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        [Required(ErrorMessage = "Şube ID zorunludur")]
        public int SubeId { get; set; }
    }
    
    // Kullanılan malzeme için yardımcı DTO
    public class UsedMaterialDTO
    {
        public int? MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost { get; set; }
        public string Notes { get; set; }
    }

    // Bakım log kayıtları için veri transfer nesnesi
    public class MaintenanceLogDetailDTO
    {
        // Temel bilgiler
        public int Id { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string MaintenanceTitle { get; set; }
        
        // Log detayları
        public string LogType { get; set; } // İşlem Başlatıldı, Tamamlandı, Ertelendi, İptal Edildi, vb.
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan LogTime { get; set; }
        
        // Teknisyen bilgileri
        public int? TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        
        // İlgili donanım bilgileri
        public int? EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        
        // Durum bilgileri
        public string PreviousStatus { get; set; }
        public string NewStatus { get; set; }
        
        // Maliyet bilgileri
        public decimal? CostAmount { get; set; }
        public string CostDescription { get; set; }
        
        // Medya ve ekler
        public List<string> AttachmentUrls { get; set; } = new List<string>();
        public bool HasPhotos { get; set; }
        
        // Konum bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        
        // İşlem süresi
        public TimeSpan? WorkDuration { get; set; }
        
        // Notlar ve açıklamalar
        public string Notes { get; set; }
        public string ActionTaken { get; set; }
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
    
    // Basit bakım log DTO
    public class MaintenanceLogSummaryDTO
    {
        public int Id { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string LogType { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public string TechnicianName { get; set; }
        public string Status { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
    
    // Bakım günlüğü oluşturma nesnesi
    public class MaintenanceLogCreateDTO
    {
        // Bağlantı bilgisi
        public int MaintenanceScheduleId { get; set; }
        
        // Log detayları
        public string LogType { get; set; } // Başlangıç, İlerleme, Sorun, Tamamlanma, İptal, vb.
        public string Description { get; set; }
        
        // Durum bilgileri
        public string NewStatus { get; set; }
        public string StatusChangeReason { get; set; }
        
        // Süre bilgisi
        public int? WorkDurationMinutes { get; set; }
        public decimal? WorkCompletionPercentage { get; set; }
        
        // Teknik detaylar
        public string ActionTaken { get; set; }
        public string ProblemDescription { get; set; }
        public string SolutionDescription { get; set; }
        public List<string> PartsReplaced { get; set; } = new List<string>();
        
        // Maliyet bilgileri
        public decimal? CostAmount { get; set; }
        public string CostDescription { get; set; }
        
        // Değerlendirme
        public int? Rating { get; set; } // 1-5 arası
        public string FeedbackNotes { get; set; }
        
        // Ekler ve dokümanlar
        public List<int> AttachmentIds { get; set; } = new List<int>();
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
    
    // Bakım log güncelleme DTO
    public class MaintenanceLogUpdateDTO
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan LogTime { get; set; }
        public int? TechnicianId { get; set; }
        public string PreviousStatus { get; set; }
        public string NewStatus { get; set; }
        public decimal? CostAmount { get; set; }
        public string CostDescription { get; set; }
        public List<string> AttachmentUrls { get; set; } = new List<string>();
        public TimeSpan? WorkDuration { get; set; }
        public string Notes { get; set; }
        public string ActionTaken { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 