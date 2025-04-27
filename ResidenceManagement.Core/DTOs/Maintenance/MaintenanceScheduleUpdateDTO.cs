// Bakım çizelgesi güncelleme veri transfer nesnesi
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım çizelgesi güncelleme veri transfer nesnesi
    /// </summary>
    public class MaintenanceScheduleUpdateDTO
    {
        /// <summary>
        /// Bakım çizelgesi ID
        /// </summary>
        [Required(ErrorMessage = "ID zorunludur")]
        public int Id { get; set; }

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
        public string Priority { get; set; }

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
        /// Bakım durumu (Planlandı, Atandı, Devam Ediyor, Tamamlandı, İptal Edildi)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Tamamlanma yüzdesi (%)
        /// </summary>
        [Range(0, 100, ErrorMessage = "Tamamlanma yüzdesi 0-100 arasında olmalıdır")]
        public int? CompletionPercentage { get; set; }

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
        /// Gerçekleşen maliyet
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Gerçekleşen maliyet 0'dan küçük olamaz")]
        public decimal? ActualCost { get; set; }

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
        public List<string> NotificationEmails { get; set; }

        /// <summary>
        /// Tamamlanma notları
        /// </summary>
        [StringLength(2000, ErrorMessage = "Tamamlanma notları en fazla 2000 karakter olabilir")]
        public string CompletionNotes { get; set; }

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
        /// Güncelleyen kullanıcı ID
        /// </summary>
        [Required(ErrorMessage = "Güncelleyen kullanıcı ID zorunludur")]
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
} 