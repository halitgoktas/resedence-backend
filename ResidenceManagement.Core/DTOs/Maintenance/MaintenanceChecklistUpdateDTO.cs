using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    // Bakım kontrol listesi güncelleme için DTO sınıfı
    public class MaintenanceChecklistUpdateDTO
    {
        // Güncellenecek kontrol listesi öğeleri
        public List<MaintenanceChecklistItemDTO> Items { get; set; }

        // Güncellemeyi yapan kullanıcı ID'si
        public int UpdatedByUserId { get; set; }

        // Güncelleme tarihi
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 