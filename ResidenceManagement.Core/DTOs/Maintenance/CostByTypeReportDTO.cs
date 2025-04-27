using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım tipi bazında maliyet raporu DTO sınıfı
    /// </summary>
    public class CostByTypeReportDTO
    {
        /// <summary>
        /// Bakım tipi
        /// </summary>
        public string MaintenanceType { get; set; }
        
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