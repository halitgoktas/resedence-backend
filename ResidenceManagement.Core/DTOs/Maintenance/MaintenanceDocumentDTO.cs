using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım dokümanları için veri transfer nesnesi
    /// </summary>
    public class MaintenanceDocumentDTO
    {
        /// <summary>
        /// Doküman ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bağlı olduğu bakım planı ID
        /// </summary>
        [Required(ErrorMessage = "Bakım planı ID zorunludur")]
        public int MaintenanceScheduleId { get; set; }

        /// <summary>
        /// Doküman başlığı
        /// </summary>
        [Required(ErrorMessage = "Doküman başlığı zorunludur")]
        [StringLength(200, ErrorMessage = "Doküman başlığı en fazla 200 karakter olabilir")]
        public string Title { get; set; }

        /// <summary>
        /// Doküman açıklaması
        /// </summary>
        [StringLength(1000, ErrorMessage = "Doküman açıklaması en fazla 1000 karakter olabilir")]
        public string Description { get; set; }

        /// <summary>
        /// Doküman tipi
        /// </summary>
        [Required(ErrorMessage = "Doküman tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Doküman tipi en fazla 50 karakter olabilir")]
        public string DocumentType { get; set; }

        /// <summary>
        /// Dosya yolu
        /// </summary>
        [Required(ErrorMessage = "Dosya yolu zorunludur")]
        public string FilePath { get; set; }

        /// <summary>
        /// Dosya tipi
        /// </summary>
        [StringLength(50, ErrorMessage = "Dosya tipi en fazla 50 karakter olabilir")]
        public string FileType { get; set; }

        /// <summary>
        /// Dosya boyutu (bytes)
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Dosya uzantısı
        /// </summary>
        [StringLength(10, ErrorMessage = "Dosya uzantısı en fazla 10 karakter olabilir")]
        public string FileExtension { get; set; }

        /// <summary>
        /// Yükleme tarihi
        /// </summary>
        public DateTime UploadDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Yükleyen kullanıcı ID
        /// </summary>
        [Required(ErrorMessage = "Yükleyen kullanıcı ID zorunludur")]
        public int UploadedByUserId { get; set; }

        /// <summary>
        /// Yükleyen kullanıcı adı
        /// </summary>
        public string UploadedByUserName { get; set; }

        /// <summary>
        /// Yükleyen
        /// </summary>
        public string UploadedBy { get; set; }

        /// <summary>
        /// Belge dosya adı
        /// </summary>
        public string DocumentName { get; set; }

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
} 