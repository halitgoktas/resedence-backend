// Bakım çizelgesi oluşturma veri transfer nesnesi
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım çizelgesi oluşturma veri transfer nesnesi
    /// </summary>
    public class MaintenanceScheduleCreateDTO
    {
        /// <summary>
        /// Bakım başlığı
        /// </summary>
        [Required(ErrorMessage = "Bakım başlığı zorunludur")]
        [StringLength(200, ErrorMessage = "Bakım başlığı en fazla 200 karakter olabilir")]
        public string Title { get; set; }

        /// <summary>
        /// Bakım açıklaması
        /// </summary>
        [StringLength(1000, ErrorMessage = "Bakım açıklaması en fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        /// <summary>
        /// Bakım tipi (Periyodik, Arıza, Önleyici vb.)
        /// </summary>
        [Required(ErrorMessage = "Bakım tipi zorunludur")]
        public string MaintenanceType { get; set; }

        /// <summary>
        /// Öncelik seviyesi (Düşük, Orta, Yüksek, Acil)
        /// </summary>
        [Required(ErrorMessage = "Öncelik seviyesi zorunludur")]
        public string Priority { get; set; } = "Normal";

        /// <summary>
        /// Planlanan başlangıç tarihi
        /// </summary>
        [Required(ErrorMessage = "Planlanan tarih zorunludur")]
        public DateTime ScheduledDate { get; set; }

        /// <summary>
        /// Planlanan başlangıç saati
        /// </summary>
        public TimeSpan? ScheduledTime { get; set; }

        /// <summary>
        /// Planlanan bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Tahmini süre 0'dan büyük olmalıdır")]
        public int? EstimatedDurationMinutes { get; set; }

        /// <summary>
        /// Rezidans/Site ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Daire ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Ekipman ID
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Atanan personel ID
        /// </summary>
        public int? AssignedToUserId { get; set; }

        /// <summary>
        /// Periyodik bakım mı?
        /// </summary>
        public bool IsRecurring { get; set; }

        /// <summary>
        /// Tekrarlama düzeni (Günlük, Haftalık, Aylık, Yıllık)
        /// </summary>
        public string RecurrencePattern { get; set; }

        /// <summary>
        /// Tekrarlama aralığı (1 = her ay, 2 = her 2 ayda bir)
        /// </summary>
        public int? RecurrenceInterval { get; set; }

        /// <summary>
        /// Tekrarlama bitiş tarihi
        /// </summary>
        public DateTime? RecurrenceEndDate { get; set; }

        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Tahmini maliyet 0'dan küçük olamaz")]
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Hatırlatıcı gönderilsin mi?
        /// </summary>
        public bool SendReminders { get; set; }

        /// <summary>
        /// Hatırlatıcı kaç gün önceden gönderilsin
        /// </summary>
        public int? ReminderDaysBefore { get; set; }

        /// <summary>
        /// Bildirim gönderilecek e-posta adresleri
        /// </summary>
        public List<string> NotificationEmails { get; set; } = new List<string>();

        /// <summary>
        /// Kontrol listesi öğeleri
        /// </summary>
        public List<string> ChecklistItems { get; set; } = new List<string>();

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

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        [Required(ErrorMessage = "Oluşturan kullanıcı ID zorunludur")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Tekrarlama sıklığı (Günlük, Haftalık, Aylık, Yıllık)
        /// </summary>
        public string? RecurrenceFrequency { get; set; }

        /// <summary>
        /// Bildirim gönderilsin mi?
        /// </summary>
        public bool SendNotification { get; set; }

        /// <summary>
        /// Bildirim kaç gün önceden gönderilsin
        /// </summary>
        [Range(0, 365, ErrorMessage = "Bildirim günü 0-365 arasında olmalıdır")]
        public int? NotificationDaysInAdvance { get; set; }

        /// <summary>
        /// Bakım durumu
        /// </summary>
        [Required(ErrorMessage = "Bakım durumu zorunludur")]
        public string Status { get; set; } = "Planlandı";

        /// <summary>
        /// Bakım önceliği açıklaması
        /// </summary>
        [StringLength(500, ErrorMessage = "Öncelik açıklaması en fazla 500 karakter olabilir")]
        public string? PriorityDescription { get; set; }

        /// <summary>
        /// Bakım kategorisi
        /// </summary>
        public string? MaintenanceCategory { get; set; }

        /// <summary>
        /// Bakım alt kategorisi
        /// </summary>
        public string? MaintenanceSubCategory { get; set; }

        /// <summary>
        /// Bakım etiketleri
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Özel alanlar (key-value pairs)
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; } = new Dictionary<string, string>();
    }
} 