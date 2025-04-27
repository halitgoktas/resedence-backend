using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Aylık maliyet raporu DTO sınıfı
    /// </summary>
    public class CostByMonthReportDTO
    {
        /// <summary>
        /// Ay (1-12)
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// Yıl
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// Ay adı
        /// </summary>
        public string MonthName { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }
        
        /// <summary>
        /// Gerçekleşen maliyet
        /// </summary>
        public decimal ActualCost { get; set; }
    }
} 