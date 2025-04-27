using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım takvim detayları için veri transfer nesnesi
    /// </summary>
    public class MaintenanceScheduleDetailDTO : MaintenanceScheduleDTO
    {
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
        /// Servis sağlayıcı ID
        /// </summary>
        public int? ServiceProviderId { get; set; }

        /// <summary>
        /// Servis sağlayıcı adı
        /// </summary>
        public string ServiceProviderName { get; set; }
        
        /// <summary>
        /// Tekrarlama modeli (Günlük, Haftalık, Aylık, Yıllık)
        /// </summary>
        public string RecurrencePattern { get; set; }

        /// <summary>
        /// Tekrarlama frekansı
        /// </summary>
        public int? RecurrenceFrequency { get; set; }

        /// <summary>
        /// Tekrarlanacak haftanın günleri (Pazartesi,Salı,...)
        /// </summary>
        public string RecurrenceDaysOfWeek { get; set; }

        /// <summary>
        /// Tekrarlanacak ayın günü
        /// </summary>
        public int? RecurrenceDayOfMonth { get; set; }

        /// <summary>
        /// Tekrarlama bitiş tarihi
        /// </summary>
        public DateTime? RecurrenceEndDate { get; set; }

        /// <summary>
        /// Maksimum tekrarlama sayısı
        /// </summary>
        public int? RecurrenceMaxOccurrences { get; set; }
        
        /// <summary>
        /// Maliyet merkezi
        /// </summary>
        public string CostCenter { get; set; }

        /// <summary>
        /// Bütçe kodu
        /// </summary>
        public string BudgetCode { get; set; }
        
        /// <summary>
        /// Bildirim gönderilecek mi?
        /// </summary>
        public bool SendNotification { get; set; }

        /// <summary>
        /// Bildirim kaç gün önceden gönderilecek
        /// </summary>
        public int? NotificationDaysInAdvance { get; set; }

        /// <summary>
        /// Bildirim e-posta adresi
        /// </summary>
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string NotificationEmail { get; set; }

        /// <summary>
        /// Bildirim SMS numarası
        /// </summary>
        public string NotificationSMS { get; set; }
        
        /// <summary>
        /// Tamamlanma tarihi
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Tamamlayan kişi
        /// </summary>
        public string CompletedBy { get; set; }

        /// <summary>
        /// Tamamlama notları
        /// </summary>
        [StringLength(1000, ErrorMessage = "Tamamlama notları en fazla 1000 karakter olabilir")]
        public string CompletionNotes { get; set; }

        /// <summary>
        /// Gerçekleşen maliyet
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Gerçekleşen maliyet 0'dan küçük olamaz")]
        public decimal? ActualCost { get; set; }

        /// <summary>
        /// Gerçekleşen süre (dakika)
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Gerçekleşen süre 0'dan küçük olamaz")]
        public int? ActualDurationMinutes { get; set; }

        /// <summary>
        /// İptal nedeni
        /// </summary>
        [StringLength(500, ErrorMessage = "İptal nedeni en fazla 500 karakter olabilir")]
        public string CancellationReason { get; set; }
        
        /// <summary>
        /// Gerekli araçlar
        /// </summary>
        public string RequiredTools { get; set; }

        /// <summary>
        /// Gerekli malzemeler
        /// </summary>
        public string RequiredMaterials { get; set; }

        /// <summary>
        /// Gerekli yetenekler/yetkinlikler
        /// </summary>
        public string RequiredSkills { get; set; }
        
        /// <summary>
        /// Bakım öncesi kontrol listesi
        /// </summary>
        public string PreMaintenanceChecklist { get; set; }

        /// <summary>
        /// Bakım adımları
        /// </summary>
        public string MaintenanceSteps { get; set; }

        /// <summary>
        /// Bakım sonrası kontrol listesi
        /// </summary>
        public string PostMaintenanceChecklist { get; set; }
        
        /// <summary>
        /// Güvenlik önlemleri
        /// </summary>
        public string SafetyPrecautions { get; set; }

        /// <summary>
        /// Acil durum prosedürleri
        /// </summary>
        public string EmergencyProcedures { get; set; }
        
        /// <summary>
        /// İlişkili dokümanlar
        /// </summary>
        public List<MaintenanceDocumentDTO> Documents { get; set; } = new List<MaintenanceDocumentDTO>();

        /// <summary>
        /// Kontrol listesi öğeleri
        /// </summary>
        public List<MaintenanceChecklistItemDTO> ChecklistItems { get; set; } = new List<MaintenanceChecklistItemDTO>();

        /// <summary>
        /// Bakım logları
        /// </summary>
        public List<MaintenanceLogDTO> Logs { get; set; } = new List<MaintenanceLogDTO>();
    }
} 