using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Maliyet farkı olan bakım raporu DTO sınıfı
    /// </summary>
    public class CostVarianceMaintenanceReportDTO
    {
        /// <summary>
        /// Bakım ID
        /// </summary>
        public int MaintenanceId { get; set; }
        
        /// <summary>
        /// Bakım başlığı
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Bakım tipi
        /// </summary>
        public string MaintenanceType { get; set; }
        
        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }
        
        /// <summary>
        /// Gerçekleşen maliyet
        /// </summary>
        public decimal ActualCost { get; set; }
        
        /// <summary>
        /// Maliyet farkı
        /// </summary>
        public decimal Variance { get; set; }
        
        /// <summary>
        /// Maliyet farkı yüzdesi
        /// </summary>
        public decimal VariancePercentage { get; set; }
    }
} 