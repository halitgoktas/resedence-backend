using System;
using System.Collections.Generic;
using System.Text;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım maliyet raporu DTO
    /// </summary>
    public class MaintenanceCostDTO
    {
        /// <summary>
        /// Bakım planı ID
        /// </summary>
        public int MaintenanceScheduleId { get; set; }
        
        /// <summary>
        /// Bakım başlığı
        /// </summary>
        public string MaintenanceTitle { get; set; }
        
        /// <summary>
        /// İşçilik maliyeti
        /// </summary>
        public decimal LaborCost { get; set; }
        
        /// <summary>
        /// Malzeme maliyeti
        /// </summary>
        public decimal MaterialCost { get; set; }
        
        /// <summary>
        /// Alet/ekipman maliyeti
        /// </summary>
        public decimal ToolsCost { get; set; }
        
        /// <summary>
        /// Dış hizmet maliyeti
        /// </summary>
        public decimal ExternalServiceCost { get; set; }
        
        /// <summary>
        /// Diğer maliyetler
        /// </summary>
        public decimal OtherCosts { get; set; }
        
        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Maliyet dökümü
        /// </summary>
        public List<MaintenanceCostItemDTO> CostBreakdown { get; set; } = new List<MaintenanceCostItemDTO>();
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal TotalActualCost { get; set; }
        
        /// <summary>
        /// Ortalama maliyet
        /// </summary>
        public decimal AverageCost { get; set; }
        
        /// <summary>
        /// En pahalı bakım ID'si
        /// </summary>
        public int MostExpensiveMaintenanceId { get; set; }
        
        /// <summary>
        /// En pahalı bakım başlığı
        /// </summary>
        public string MostExpensiveMaintenanceTitle { get; set; }
        
        /// <summary>
        /// En pahalı bakım maliyeti
        /// </summary>
        public decimal MostExpensiveMaintenanceCost { get; set; }
        
        /// <summary>
        /// Bakım türüne göre maliyet
        /// </summary>
        public List<CostByTypeReportDTO> CostByMaintenanceType { get; set; } = new List<CostByTypeReportDTO>();
        
        /// <summary>
        /// Rapor oluşturma tarihi
        /// </summary>
        public DateTime ReportGeneratedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Yüksek maliyetli bakımlar
        /// </summary>
        public List<HighCostMaintenanceReportDTO> HighCostMaintenances { get; set; } = new List<HighCostMaintenanceReportDTO>();
    }
    
    /// <summary>
    /// Bakım maliyeti dağılım DTO
    /// </summary>
    public class CostByTypeDTO
    {
        /// <summary>
        /// Maliyet türü
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Toplam tahmini maliyet
        /// </summary>
        public decimal TotalEstimatedCost { get; set; }
        
        /// <summary>
        /// Toplam gerçek maliyet
        /// </summary>
        public decimal TotalActualCost { get; set; }
        
        /// <summary>
        /// Ortalama maliyet
        /// </summary>
        public decimal AverageCost { get; set; }
    }
    
    /// <summary>
    /// Lokasyona göre maliyet dağılım DTO
    /// </summary>
    public class CostByLocationDTO
    {
        /// <summary>
        /// Lokasyon
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Toplam tahmini maliyet
        /// </summary>
        public decimal TotalEstimatedCost { get; set; }
        
        /// <summary>
        /// Toplam gerçek maliyet
        /// </summary>
        public decimal TotalActualCost { get; set; }
    }
    
    /// <summary>
    /// Yüksek maliyetli bakım DTO
    /// </summary>
    public class HighCostMaintenanceDTO
    {
        /// <summary>
        /// Bakım ID
        /// </summary>
        public int MaintenanceId { get; set; }
        
        /// <summary>
        /// Bakım başlığı
        /// </summary>
        public string MaintenanceTitle { get; set; }
        
        /// <summary>
        /// Bakım tamamlanma tarihi
        /// </summary>
        public DateTime? CompletionDate { get; set; }
        
        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }
        
        /// <summary>
        /// Gerçek maliyet
        /// </summary>
        public decimal ActualCost { get; set; }
        
        /// <summary>
        /// Maliyet farkı
        /// </summary>
        public decimal CostDifference { get; set; }
    }
    
    /// <summary>
    /// Bakım maliyet öğesi DTO
    /// </summary>
    public class MaintenanceCostItemDTO
    {
        /// <summary>
        /// Maliyet türü
        /// </summary>
        public string CostType { get; set; }
        
        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Kayıt tarihi
        /// </summary>
        public DateTime RecordDate { get; set; }
        
        /// <summary>
        /// Kaydı oluşturan kullanıcı ID
        /// </summary>
        public int? RecordedByUserId { get; set; }
    }
} 