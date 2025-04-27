using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Services
{
    /// <summary>
    /// Hizmet türlerini tanımlayan sınıf (musluk tamiri, elektrik arızası, perde yıkama vb.)
    /// </summary>
    public class ServiceType : BaseLookupEntity
    {
        /// <summary>
        /// Hizmet kategori ID
        /// </summary>
        public int ServiceCategoryId { get; set; }

        /// <summary>
        /// Hizmet kodu
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Hizmet açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Standart fiyat (TL)
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// Standart para birimi
        /// </summary>
        public string BaseCurrency { get; set; } = "TRY";

        /// <summary>
        /// USD cinsinden fiyat
        /// </summary>
        public decimal? PriceUSD { get; set; }

        /// <summary>
        /// EUR cinsinden fiyat
        /// </summary>
        public decimal? PriceEUR { get; set; }

        /// <summary>
        /// GBP cinsinden fiyat
        /// </summary>
        public decimal? PriceGBP { get; set; }

        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        public int EstimatedDuration { get; set; }

        /// <summary>
        /// Acil durum ek ücreti (%)
        /// </summary>
        public decimal UrgentServiceFeePercentage { get; set; } = 50;

        /// <summary>
        /// Hafta sonu ek ücreti (%)
        /// </summary>
        public decimal WeekendFeePercentage { get; set; } = 25;

        /// <summary>
        /// Gece mesaisi ek ücreti (%)
        /// </summary>
        public decimal NightShiftFeePercentage { get; set; } = 35;

        /// <summary>
        /// Hizmet sırası
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Daire sahibi için ücretsiz mi?
        /// </summary>
        public bool IsFreeForOwners { get; set; }

        /// <summary>
        /// Kiracı için ücretsiz mi?
        /// </summary>
        public bool IsFreeForTenants { get; set; }

        /// <summary>
        /// Personel gereksinimleri
        /// </summary>
        public string StaffRequirements { get; set; }

        /// <summary>
        /// Ekipman gereksinimleri
        /// </summary>
        public string EquipmentRequirements { get; set; }

        /// <summary>
        /// Temel malzeme gereksinimleri
        /// </summary>
        public string MaterialRequirements { get; set; }

        /// <summary>
        /// Randevu gerekli mi?
        /// </summary>
        public bool RequiresAppointment { get; set; }

        /// <summary>
        /// Yönetim onayı gerekli mi?
        /// </summary>
        public bool RequiresApproval { get; set; }

        // Navigation Properties
        /// <summary>
        /// Bağlı olduğu kategori
        /// </summary>
        public virtual ServiceCategory ServiceCategory { get; set; }

        /// <summary>
        /// Bu tür ile ilişkili servis talepleri
        /// </summary>
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
} 