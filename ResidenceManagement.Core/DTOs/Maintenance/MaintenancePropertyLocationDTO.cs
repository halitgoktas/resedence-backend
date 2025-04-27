using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    /// <summary>
    /// Bakım için mülk lokasyon bilgisi
    /// </summary>
    public class MaintenancePropertyLocationDTO
    {
        /// <summary>
        /// Lokasyon ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lokasyon tipi (Residence, Block, Apartment, CommonArea)
        /// </summary>
        [Required(ErrorMessage = "Lokasyon tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Lokasyon tipi en fazla 50 karakter olabilir")]
        public string LocationType { get; set; }

        /// <summary>
        /// Lokasyon adı
        /// </summary>
        [Required(ErrorMessage = "Lokasyon adı zorunludur")]
        [StringLength(100, ErrorMessage = "Lokasyon adı en fazla 100 karakter olabilir")]
        public string LocationName { get; set; }

        /// <summary>
        /// Site/Rezidans ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Daire ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Ortak Alan ID
        /// </summary>
        public int? CommonAreaId { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Oluşturma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Lokasyon seçim DTO
    /// </summary>
    public class LocationSelectDTO
    {
        /// <summary>
        /// Lokasyon ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lokasyon adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lokasyon tipi
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Üst lokasyon ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Alt lokasyonlar
        /// </summary>
        public List<LocationSelectDTO> Children { get; set; } = new List<LocationSelectDTO>();
    }

    /// <summary>
    /// Hiyerarşik lokasyon DTO
    /// </summary>
    public class HierarchicalLocationDTO
    {
        /// <summary>
        /// Site/Rezidans bilgileri
        /// </summary>
        public List<LocationSelectDTO> Residences { get; set; } = new List<LocationSelectDTO>();
    }
} 