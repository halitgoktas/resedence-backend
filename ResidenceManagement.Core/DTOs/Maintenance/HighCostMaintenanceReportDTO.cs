using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Yüksek maliyetli bakım raporu DTO sınıfı
    /// </summary>
    public class HighCostMaintenanceReportDTO
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
        /// Lokasyon
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Gerçekleşen maliyet
        /// </summary>
        public decimal ActualCost { get; set; }
        
        /// <summary>
        /// Toplam maliyetteki payı (%)
        /// </summary>
        public decimal PercentageOfTotal { get; set; }
    }
} 