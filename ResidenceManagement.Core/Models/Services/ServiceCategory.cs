using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Hizmet kategorilerini tanımlayan sınıf
    /// </summary>
    public class ServiceCategory : BaseEntity
    {
        /// <summary>
        /// Kategori kodu
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Kategori adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Kategori açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Üst kategori ID'si (null ise ana kategoridir)
        /// </summary>
        public int? ParentCategoryId { get; set; }
        
        /// <summary>
        /// Üst kategori (null ise ana kategoridir)
        /// </summary>
        public virtual ServiceCategory ParentCategory { get; set; }
        
        /// <summary>
        /// Alt kategoriler
        /// </summary>
        public virtual ICollection<ServiceCategory> SubCategories { get; set; }
        
        /// <summary>
        /// Bu kategoriye ait hizmet tanımları
        /// </summary>
        public virtual ICollection<ServiceDefinition> ServiceDefinitions { get; set; }
        
        /// <summary>
        /// Kategori için varsayılan öncelik seviyesi
        /// </summary>
        public string DefaultPriority { get; set; }
        
        /// <summary>
        /// Ortalama tamamlanma süresi (saat)
        /// </summary>
        public decimal? AverageCompletionTime { get; set; }
        
        /// <summary>
        /// Kategori ikonu (CSS sınıfı veya dosya yolu)
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// Kategori renk kodu (HEX)
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Görüntüleme sırası
        /// </summary>
        public int DisplayOrder { get; set; }
        
        /// <summary>
        /// Müşteri portalında görünür mü?
        /// </summary>
        public bool IsVisibleToCustomers { get; set; }
        
        /// <summary>
        /// Kategori aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Kategori için varsayılan çalışma saatleri (JSON formatında)
        /// Örnek: {"Monday":"09:00-18:00","Tuesday":"09:00-18:00",...}
        /// </summary>
        public string DefaultWorkingHours { get; set; }
        
        /// <summary>
        /// Kategori için gerekli olan yetkinlikler (virgülle ayrılmış)
        /// </summary>
        public string RequiredSkills { get; set; }
        
        /// <summary>
        /// Bu kategorideki hizmetleri yapabilecek servis ekibi ID'si
        /// </summary>
        public int? DefaultServiceTeamId { get; set; }
        
        /// <summary>
        /// Müşteriler için notlar/açıklamalar
        /// </summary>
        public string CustomerNotes { get; set; }
        
        /// <summary>
        /// Teknisyenler için notlar/açıklamalar
        /// </summary>
        public string TechnicianNotes { get; set; }
        
        /// <summary>
        /// Minimum ücret (TRY)
        /// </summary>
        public decimal? MinimumCharge { get; set; }
        
        /// <summary>
        /// Önizleme görseli URL'i
        /// </summary>
        public string PreviewImageUrl { get; set; }
        
        /// <summary>
        /// Mobil uygulamada görünür mü?
        /// </summary>
        public bool IsVisibleInMobileApp { get; set; }
        
        /// <summary>
        /// Rezervasyon gerektirir mi?
        /// </summary>
        public bool RequiresReservation { get; set; }
        
        /// <summary>
        /// Rezervasyon için minimum süre (saat)
        /// </summary>
        public int? MinimumReservationHours { get; set; }
        
        /// <summary>
        /// Meta açıklaması (SEO için)
        /// </summary>
        public string MetaDescription { get; set; }
        
        /// <summary>
        /// Meta anahtar kelimeleri (SEO için)
        /// </summary>
        public string MetaKeywords { get; set; }
        
        /// <summary>
        /// SEO dostu URL
        /// </summary>
        public string SeoFriendlyUrl { get; set; }
        
        public ServiceCategory()
        {
            SubCategories = new List<ServiceCategory>();
            ServiceDefinitions = new List<ServiceDefinition>();
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsVisibleToCustomers = true;
            IsVisibleInMobileApp = true;
            DisplayOrder = 1;
            DefaultPriority = "Normal";
        }
        
        /// <summary>
        /// Alt kategori ekler
        /// </summary>
        /// <param name="subCategory">Eklenecek alt kategori</param>
        public void AddSubCategory(ServiceCategory subCategory)
        {
            subCategory.ParentCategoryId = this.Id;
            subCategory.ParentCategory = this;
            this.SubCategories.Add(subCategory);
        }
        
        /// <summary>
        /// Hizmet tanımı ekler
        /// </summary>
        /// <param name="serviceDefinition">Eklenecek hizmet tanımı</param>
        public void AddServiceDefinition(ServiceDefinition serviceDefinition)
        {
            serviceDefinition.ServiceCategoryId = this.Id;
            this.ServiceDefinitions.Add(serviceDefinition);
        }
        
        /// <summary>
        /// Tam kategori hiyerarşisini içeren bir isim döndürür
        /// </summary>
        /// <returns>Hiyerarşik kategori ismi</returns>
        public string GetFullCategoryName()
        {
            if (ParentCategory == null)
            {
                return Name;
            }
            
            return ParentCategory.GetFullCategoryName() + " > " + Name;
        }
        
        /// <summary>
        /// SEO dostu URL oluşturur
        /// </summary>
        public void GenerateSeoFriendlyUrl()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }
            
            SeoFriendlyUrl = Name.ToLower()
                .Replace(" ", "-")
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Replace("İ", "i")
                .Replace("Ğ", "g")
                .Replace("Ü", "u")
                .Replace("Ş", "s")
                .Replace("Ö", "o")
                .Replace("Ç", "c");
            
            // Özel karakterleri temizle
            SeoFriendlyUrl = System.Text.RegularExpressions.Regex.Replace(SeoFriendlyUrl, "[^a-z0-9\\-]", "");
            
            // Mükerrer tire karakterlerini temizle
            while (SeoFriendlyUrl.Contains("--"))
            {
                SeoFriendlyUrl = SeoFriendlyUrl.Replace("--", "-");
            }
        }
    }
    
    /// <summary>
    /// Hizmet alt kategorilerini tanımlayan sınıf
    /// </summary>
    public class ServiceSubCategory : BaseEntity
    {
        /// <summary>
        /// Alt kategori adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Alt kategori açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Bağlı olduğu ana kategori ID'si
        /// </summary>
        public int ServiceCategoryId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu ana kategori
        /// </summary>
        public virtual ServiceCategory ServiceCategory { get; set; }
        
        /// <summary>
        /// Bu alt kategoriye bağlı hizmetler
        /// </summary>
        public virtual ICollection<ServiceDefinition> Services { get; set; }
        
        /// <summary>
        /// Alt kategori aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Alt kategori sırası
        /// </summary>
        public int DisplayOrder { get; set; }
        
        public ServiceSubCategory()
        {
            Services = new List<ServiceDefinition>();
            IsActive = true;
            CreatedDate = DateTime.Now;
            IsDeleted = false;
            DisplayOrder = 1;
        }
    }
    
    // Hizmet kategori türleri
    public enum ServiceCategoryType
    {
        // Teknik servis
        Technical = 0,
        
        // Temizlik hizmetleri
        Cleaning = 1,
        
        // Bakım hizmetleri
        Maintenance = 2,
        
        // Onarım hizmetleri
        Repair = 3,
        
        // Dekorasyon hizmetleri
        Decoration = 4,
        
        // İnternet/TV hizmetleri
        CommunicationServices = 5,
        
        // Çamaşırhane/Kuru temizleme hizmetleri
        Laundry = 6,
        
        // Diğer hizmetler
        Other = 7
    }
} 