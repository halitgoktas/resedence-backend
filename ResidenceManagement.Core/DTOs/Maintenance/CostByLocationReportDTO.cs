using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Lokasyon bazında maliyet raporu DTO sınıfı
    /// </summary>
    public class CostByLocationReportDTO
    {
        /// <summary>
        /// Lokasyon tipi (Rezidans, Blok, Daire)
        /// </summary>
        public string LocationType { get; set; }
        
        /// <summary>
        /// Lokasyon adı
        /// </summary>
        public string LocationName { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// Toplam maliyetin yüzdesi
        /// </summary>
        public decimal Percentage { get; set; }
    }
} 