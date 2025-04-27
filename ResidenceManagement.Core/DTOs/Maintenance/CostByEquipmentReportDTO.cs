using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Ekipman bazında maliyet dağılımı DTO
    /// </summary>
    public class CostByEquipmentReportDTO
    {
        /// <summary>
        /// Ekipman ID
        /// </summary>
        public int EquipmentId { get; set; }
        
        /// <summary>
        /// Ekipman adı
        /// </summary>
        public string EquipmentName { get; set; }
        
        /// <summary>
        /// Ekipman kodu
        /// </summary>
        public string EquipmentCode { get; set; }
        
        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// Toplam içindeki yüzde
        /// </summary>
        public decimal Percentage { get; set; }
    }
} 