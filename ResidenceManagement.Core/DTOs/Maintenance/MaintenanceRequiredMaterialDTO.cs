using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım için gerekli malzeme bilgilerini taşıyan DTO sınıfı
    /// </summary>
    public class MaintenanceRequiredMaterialDTO
    {
        /// <summary>
        /// Malzeme ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bakım planı ID
        /// </summary>
        [Required(ErrorMessage = "Bakım planı ID zorunludur")]
        public int MaintenanceScheduleId { get; set; }

        /// <summary>
        /// Malzeme adı
        /// </summary>
        [Required(ErrorMessage = "Malzeme adı zorunludur")]
        [StringLength(200, ErrorMessage = "Malzeme adı en fazla 200 karakter olabilir")]
        public string Name { get; set; }

        /// <summary>
        /// Malzeme açıklaması
        /// </summary>
        [StringLength(500, ErrorMessage = "Malzeme açıklaması en fazla 500 karakter olabilir")]
        public string Description { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalıdır")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Birim (adet, kg, lt, vb.)
        /// </summary>
        [Required(ErrorMessage = "Birim zorunludur")]
        [StringLength(20, ErrorMessage = "Birim en fazla 20 karakter olabilir")]
        public string Unit { get; set; }

        /// <summary>
        /// Tahmini birim fiyat
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Tahmini birim fiyat 0'dan küçük olamaz")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Tahmini toplam maliyet
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Tahmini toplam maliyet 0'dan küçük olamaz")]
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// Tedarik edildi mi?
        /// </summary>
        public bool IsSupplied { get; set; }

        /// <summary>
        /// Tedarik tarihi
        /// </summary>
        public DateTime? SuppliedDate { get; set; }

        /// <summary>
        /// Malzeme tarafından kullanıldı mı?
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        [Required(ErrorMessage = "Firma ID zorunludur")]
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        [Required(ErrorMessage = "Şube ID zorunludur")]
        public int SubeId { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
} 