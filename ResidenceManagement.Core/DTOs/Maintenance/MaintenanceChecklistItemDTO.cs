using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım kontrol listesi öğelerini temsil eden veri transfer nesnesi
    /// </summary>
    public class MaintenanceChecklistItemDTO
    {
        /// <summary>
        /// Kontrol listesi öğesi ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bağlı olduğu bakım planı ID
        /// </summary>
        [Required(ErrorMessage = "Bakım planı ID zorunludur")]
        public int MaintenanceScheduleId { get; set; }

        /// <summary>
        /// Kontrol öğesi başlığı
        /// </summary>
        [Required(ErrorMessage = "Kontrol öğesi başlığı zorunludur")]
        [StringLength(200, ErrorMessage = "Kontrol öğesi başlığı en fazla 200 karakter olabilir")]
        public string Title { get; set; }

        /// <summary>
        /// Kontrol öğesi açıklaması
        /// </summary>
        [StringLength(1000, ErrorMessage = "Kontrol öğesi açıklaması en fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        /// <summary>
        /// Kontrol öğesi sırası
        /// </summary>
        [Required(ErrorMessage = "Sıra numarası zorunludur")]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Kontrol öğesi sıra indeksi
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Kategori
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Zorunlu kontrol öğesi mi?
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Tamamlanma tarihi
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Tamamlanma tarihi (alternatif)
        /// </summary>
        public DateTime? CompletedDate { 
            get { return CompletedAt; }
            set { CompletedAt = value; } 
        }

        /// <summary>
        /// Tamamlayan kullanıcı ID
        /// </summary>
        public int? CompletedByUserId { get; set; }

        /// <summary>
        /// Tamamlayan kullanıcı adı
        /// </summary>
        public string CompletedBy { get; set; }

        /// <summary>
        /// Tamamlama notu
        /// </summary>
        [StringLength(500, ErrorMessage = "Tamamlama notu en fazla 500 karakter olabilir")]
        public string CompletionNotes { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }

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
        public int CreatedBy { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
} 