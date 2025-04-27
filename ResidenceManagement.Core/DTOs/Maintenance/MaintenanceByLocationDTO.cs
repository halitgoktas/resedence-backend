using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Lokasyon bazlı bakım dağılım bilgileri için veri transfer nesnesi
    /// </summary>
    public class MaintenanceLocationStatsDTO
    {
        /// <summary>
        /// Lokasyon tipi (Residence, Blok, Daire, Ortak Alan)
        /// </summary>
        [StringLength(50, ErrorMessage = "Lokasyon tipi en fazla 50 karakter olabilir")]
        public string LocationType { get; set; }

        /// <summary>
        /// Lokasyon adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Lokasyon adı en fazla 100 karakter olabilir")]
        public string LocationName { get; set; }

        /// <summary>
        /// Toplam bakım sayısı
        /// </summary>
        public int MaintenanceCount { get; set; }

        /// <summary>
        /// Tamamlanan bakım sayısı
        /// </summary>
        public int CompletedCount { get; set; }

        /// <summary>
        /// Devam eden bakım sayısı
        /// </summary>
        public int InProgressCount { get; set; }

        /// <summary>
        /// Planlanmış bakım sayısı
        /// </summary>
        public int PlannedCount { get; set; }

        /// <summary>
        /// Toplam bakım maliyeti
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Ortalama tamamlanma süresi (saat)
        /// </summary>
        public decimal? AverageCompletionTime { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
    }
    
    /// <summary>
    /// Lokasyon bazlı bakım dağılımı bilgilerini taşıyan veri transfer nesnesi
    /// </summary>
    public class MaintenanceByLocationDTO
    {
        /// <summary>
        /// Lokasyon tipi (Residence, Blok, Daire, Ortak Alan)
        /// </summary>
        public string LocationType { get; set; }
        
        /// <summary>
        /// Lokasyon adı
        /// </summary>
        public string LocationName { get; set; }
        
        /// <summary>
        /// Toplam bakım sayısı
        /// </summary>
        public int MaintenanceCount { get; set; }
        
        /// <summary>
        /// Toplam sayım (alias)
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Yüzde (toplam içindeki)
        /// </summary>
        public decimal Percentage { get; set; }
        
        /// <summary>
        /// Tamamlanan bakım sayısı
        /// </summary>
        public int CompletedCount { get; set; }
        
        /// <summary>
        /// Devam eden bakım sayısı
        /// </summary>
        public int InProgressCount { get; set; }
        
        /// <summary>
        /// Planlanmış bakım sayısı
        /// </summary>
        public int PlannedCount { get; set; }
        
        /// <summary>
        /// Toplam bakım maliyeti
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// Ortalama tamamlanma süresi (saat)
        /// </summary>
        public decimal? AverageCompletionTime { get; set; }

        /// <summary>
        /// Lokasyon ID
        /// </summary>
        public int LocationId { get; set; }
    }
} 