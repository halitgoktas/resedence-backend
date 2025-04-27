using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Services
{
    /// <summary>
    /// Hizmet kategorilerini tanımlayan sınıf (elektrik, temizlik, tesisat vb.)
    /// </summary>
    public class ServiceCategory : BaseLookupEntity
    {
        /// <summary>
        /// Kategori kodu
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Kategori açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Üst kategori ID (alt kategori ise)
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Sıralama
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// İkon/Görsel URL
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Kategori renk kodu
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// Varsayılan kategori mi?
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Kategori türü (1: Ana Kategori, 2: Alt Kategori)
        /// </summary>
        public int CategoryType { get; set; } = 1;

        // Navigation Properties
        /// <summary>
        /// Üst kategori referansı
        /// </summary>
        public virtual ServiceCategory ParentCategory { get; set; }

        /// <summary>
        /// Alt kategoriler
        /// </summary>
        public virtual ICollection<ServiceCategory> SubCategories { get; set; }

        /// <summary>
        /// Kategori altındaki hizmet türleri
        /// </summary>
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
    }
} 