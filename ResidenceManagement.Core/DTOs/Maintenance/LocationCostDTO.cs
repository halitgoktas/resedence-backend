using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Lokasyon bazlı bakım maliyeti DTO
    /// </summary>
    public class LocationCostDTO
    {
        /// <summary>
        /// Lokasyon tipi (Rezidans, Blok, Daire, Ortak Alan)
        /// </summary>
        [StringLength(50, ErrorMessage = "Lokasyon tipi en fazla 50 karakter olabilir.")]
        public string LocationType { get; set; }

        /// <summary>
        /// Lokasyon adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Lokasyon adı en fazla 100 karakter olabilir.")]
        public string LocationName { get; set; }

        /// <summary>
        /// Site ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Site adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Site adı en fazla 100 karakter olabilir.")]
        public string ResidenceName { get; set; }

        /// <summary>
        /// Blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Blok adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Blok adı en fazla 50 karakter olabilir.")]
        public string BlockName { get; set; }

        /// <summary>
        /// Ortak alan ID
        /// </summary>
        public int? CommonAreaId { get; set; }

        /// <summary>
        /// Ortak alan adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Ortak alan adı en fazla 100 karakter olabilir.")]
        public string CommonAreaName { get; set; }

        /// <summary>
        /// Bakım sayısı
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Tahmini toplam maliyet
        /// </summary>
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// Gerçekleşen toplam maliyet
        /// </summary>
        public decimal ActualCost { get; set; }

        /// <summary>
        /// Maliyet farkı
        /// </summary>
        public decimal CostVariance { get; set; }

        /// <summary>
        /// Toplam içindeki yüzde
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Firma ID (Multi-tenant yapı için)
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID (Multi-tenant yapı için)
        /// </summary>
        public int SubeId { get; set; }
    }
} 