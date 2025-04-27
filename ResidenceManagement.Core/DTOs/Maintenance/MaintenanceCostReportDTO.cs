using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım maliyeti raporu için kullanılan DTO sınıfı
    /// </summary>
    public class MaintenanceCostReportDTO
    {
        /// <summary>
        /// Rapor oluşturma tarihi
        /// </summary>
        public DateTime ReportDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Seçilen tarih aralığı başlangıç
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Seçilen tarih aralığı bitiş
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Toplam bakım sayısı
        /// </summary>
        public int TotalMaintenanceCount { get; set; }
        
        /// <summary>
        /// Tamamlanan bakım sayısı
        /// </summary>
        public int CompletedMaintenanceCount { get; set; }
        
        /// <summary>
        /// Toplam planlanan maliyet (TRY)
        /// </summary>
        public decimal TotalEstimatedCost { get; set; }
        
        /// <summary>
        /// Toplam gerçekleşen maliyet (TRY)
        /// </summary>
        public decimal TotalActualCost { get; set; }
        
        /// <summary>
        /// Maliyet farkı yüzdesi (%)
        /// </summary>
        public decimal CostDifferencePercentage { get; set; }
        
        /// <summary>
        /// Bakım türüne göre maliyet dağılımı
        /// </summary>
        public List<CostByTypeReportDTO> CostByType { get; set; } = new List<CostByTypeReportDTO>();
        
        /// <summary>
        /// Ekipman bazında maliyet dağılımı
        /// </summary>
        public List<CostByEquipmentReportDTO> CostByEquipment { get; set; } = new List<CostByEquipmentReportDTO>();
        
        /// <summary>
        /// Lokasyona göre maliyet dağılımı
        /// </summary>
        public List<CostByLocationReportDTO> CostByLocation { get; set; } = new List<CostByLocationReportDTO>();
        
        /// <summary>
        /// Aylara göre maliyet dağılımı
        /// </summary>
        public List<CostByMonthReportDTO> CostByMonth { get; set; } = new List<CostByMonthReportDTO>();
        
        /// <summary>
        /// En yüksek maliyetli bakımlar
        /// </summary>
        public List<HighCostMaintenanceReportDTO> HighestCostItems { get; set; } = new List<HighCostMaintenanceReportDTO>();
        
        /// <summary>
        /// Tahmini ve gerçekleşen maliyet farkının en yüksek olduğu bakımlar
        /// </summary>
        public List<CostVarianceMaintenanceReportDTO> HighestVarianceItems { get; set; } = new List<CostVarianceMaintenanceReportDTO>();
        
        /// <summary>
        /// Firma ID (Multi-tenant yapısı için)
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID (Multi-tenant yapısı için)
        /// </summary>
        public int SubeId { get; set; }
    }
} 