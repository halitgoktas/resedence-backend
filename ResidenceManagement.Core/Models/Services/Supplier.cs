using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Tedarikçileri modelleyen sınıf
    /// </summary>
    public class Supplier : BaseEntity
    {
        /// <summary>
        /// Tedarikçi kodu
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Tedarikçi adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// İletişim kişisi
        /// </summary>
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Vergi dairesi
        /// </summary>
        public string TaxOffice { get; set; }
        
        /// <summary>
        /// Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }
        
        /// <summary>
        /// Anlaşma başlangıç tarihi
        /// </summary>
        public DateTime? ContractStartDate { get; set; }
        
        /// <summary>
        /// Anlaşma bitiş tarihi
        /// </summary>
        public DateTime? ContractEndDate { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Ürün/hizmet kategorileri (virgülle ayrılmış kategori ID'leri)
        /// </summary>
        public string CategoryIds { get; set; }
        
        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string PaymentTerms { get; set; }
        
        /// <summary>
        /// Varsayılan para birimi
        /// </summary>
        public string DefaultCurrency { get; set; }
        
        /// <summary>
        /// Tedarikçi logosu
        /// </summary>
        public string LogoUrl { get; set; }
        
        /// <summary>
        /// Web sitesi
        /// </summary>
        public string Website { get; set; }
        
        /// <summary>
        /// Aktif mi
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Kalite puanı (1-5 arası)
        /// </summary>
        public decimal QualityRating { get; set; }
        
        /// <summary>
        /// Fiyat performans puanı (1-5 arası)
        /// </summary>
        public decimal PricePerformanceRating { get; set; }
        
        /// <summary>
        /// Tepki süresi puanı (1-5 arası)
        /// </summary>
        public decimal ResponseTimeRating { get; set; }
        
        /// <summary>
        /// Değerlendirme sayısı
        /// </summary>
        public int RatingCount { get; set; }
        
        public Supplier()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            QualityRating = 0;
            PricePerformanceRating = 0;
            ResponseTimeRating = 0;
            RatingCount = 0;
            DefaultCurrency = "TRY";
        }
    }
} 