using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    // Bakım dokümanı oluşturma için DTO sınıfı
    public class MaintenanceDocumentCreateDTO
    {
        // Doküman başlığı
        public string Title { get; set; }

        // Doküman açıklaması
        public string Description { get; set; }

        // Dosya tipi (örn: pdf, doc, jpg)
        public string FileType { get; set; }

        // Dosya yolu
        public string FilePath { get; set; }

        // Dosya boyutu (bytes)
        public long FileSize { get; set; }

        // Yükleyen kullanıcı ID'si
        public int UploadedByUserId { get; set; }

        // Yükleme tarihi
        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 