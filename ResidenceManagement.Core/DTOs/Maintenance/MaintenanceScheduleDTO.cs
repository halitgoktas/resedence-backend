// Bakım planlaması için veri transfer nesnesi
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım planlaması için veri transfer nesnesi
    /// </summary>
    public class MaintenanceScheduleDTO
    {
        /// <summary>
        /// Bakım planı ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bakım başlığı
        /// </summary>
        [Required(ErrorMessage = "Bakım başlığı zorunludur")]
        public string Title { get; set; }

        /// <summary>
        /// Bakım açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Bakım tipi (Periyodik, Arıza, Önleyici vb.)
        /// </summary>
        [Required(ErrorMessage = "Bakım tipi zorunludur")]
        public string Type { get; set; }

        /// <summary>
        /// Bakım tipi (Type ile aynı, geriye dönük uyumluluk için)
        /// </summary>
        public string MaintenanceType { get => Type; set => Type = value; }

        /// <summary>
        /// Öncelik seviyesi (Düşük, Orta, Yüksek, Acil)
        /// </summary>
        [Required(ErrorMessage = "Öncelik seviyesi zorunludur")]
        public string Priority { get; set; }

        /// <summary>
        /// Planlanan başlangıç tarihi
        /// </summary>
        [Required(ErrorMessage = "Planlanan tarih zorunludur")]
        public DateTime PlannedStartDate { get; set; }

        /// <summary>
        /// Planlanan başlangıç tarihi (PlannedStartDate ile aynı, geriye dönük uyumluluk için)
        /// </summary>
        public DateTime ScheduledDate { get => PlannedStartDate; set => PlannedStartDate = value; }

        /// <summary>
        /// Planlanan başlangıç saati
        /// </summary>
        public TimeSpan? ScheduledTime { get; set; }

        /// <summary>
        /// Planlanan bitiş tarihi
        /// </summary>
        public DateTime PlannedEndDate { get; set; }

        /// <summary>
        /// Planlanan bitiş tarihi (PlannedEndDate ile aynı, geriye dönük uyumluluk için)
        /// </summary>
        public DateTime? EndDate { get => PlannedEndDate; set => PlannedEndDate = value ?? PlannedEndDate; }

        /// <summary>
        /// Gerçek başlangıç tarihi
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// Gerçek bitiş tarihi
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        public int? EstimatedDurationMinutes { get; set; }

        /// <summary>
        /// Bakım durumu (Planlandı, Devam Ediyor, Tamamlandı, İptal Edildi)
        /// </summary>
        [Required(ErrorMessage = "Bakım durumu zorunludur")]
        public string Status { get; set; }

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
        /// Rezidans/Site adı
        /// </summary>
        public string ResidenceName { get; set; }

        /// <summary>
        /// Blok adı
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Daire numarası
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        /// Ekipman adı
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// Atanan personel adı
        /// </summary>
        public string AssignedToUserName { get; set; }

        /// <summary>
        /// Periyodik bakım mı?
        /// </summary>
        public bool IsRecurring { get; set; }

        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// Gerçek maliyet
        /// </summary>
        public decimal? ActualCost { get; set; }

        /// <summary>
        /// Maliyet merkezi
        /// </summary>
        public string CostCenter { get; set; }

        /// <summary>
        /// Bütçe kodu
        /// </summary>
        public string BudgetCode { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Tamamlama notları
        /// </summary>
        public string CompletionNotes { get; set; }

        /// <summary>
        /// Üst bakım ID
        /// </summary>
        public int? ParentMaintenanceId { get; set; }

        /// <summary>
        /// Tekrarlama deseni
        /// </summary>
        public string RecurrencePattern { get; set; }

        /// <summary>
        /// Tekrarlama bitiş tarihi
        /// </summary>
        public DateTime? RecurrenceEndDate { get; set; }

        /// <summary>
        /// Bildirim e-posta
        /// </summary>
        public string NotificationEmail { get; set; }

        /// <summary>
        /// Bildirim SMS
        /// </summary>
        public string NotificationSMS { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğeleri
        /// </summary>
        public List<MaintenanceChecklistItemDTO> ChecklistItems { get; set; }

        /// <summary>
        /// Bakım dokümanları
        /// </summary>
        public List<MaintenanceDocumentDTO> Documents { get; set; }

        /// <summary>
        /// Bakım logları
        /// </summary>
        public List<MaintenanceLogDTO> Logs { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Konum
        /// </summary>
        public string Location { get; set; }

        public MaintenanceScheduleDTO()
        {
            ChecklistItems = new List<MaintenanceChecklistItemDTO>();
            Documents = new List<MaintenanceDocumentDTO>();
            Logs = new List<MaintenanceLogDTO>();
        }
    }
} 