using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gelir kategorilerini temsil eden sınıf
    public class RevenueCategory
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Firma ve şube bilgileri (multi-tenant yapı için)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Kategori adı
        public string Name { get; set; }
        
        // Kategori açıklaması
        public string Description { get; set; }
        
        // Üst kategori ID (hiyerarşik yapı için)
        public int? ParentCategoryId { get; set; }
        
        // Varsayılan KDV oranı (%)
        public decimal DefaultVATRate { get; set; }
        
        // Varsayılan stopaj oranı (%)
        public decimal DefaultWithholdingTaxRate { get; set; }
        
        // Stopaj uygulansın mı?
        public bool ApplyWithholdingTax { get; set; }
        
        // Varsayılan yönetim komisyonu oranı (%)
        public decimal DefaultManagementCommissionRate { get; set; }
        
        // Kategori sırası (UI'da gösterim için)
        public int DisplayOrder { get; set; }
        
        // Kategori tipi
        public RevenueCategoryType CategoryType { get; set; }
        
        // Kategori rengi (UI'da gösterim için)
        public string ColorCode { get; set; }
        
        // Kategori ikonu (UI'da gösterim için)
        public string IconName { get; set; }
        
        // Kategori Dağıtım metodu
        public DistributionMethod DistributionMethod { get; set; }
        
        // Alt kategoriler
        public List<RevenueCategory> SubCategories { get; set; } = new List<RevenueCategory>();
        
        // İlişkili işlemler
        public List<RevenueTransaction> Transactions { get; set; } = new List<RevenueTransaction>();
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Kategori durumu
        public bool IsActive { get; set; }
        
        // Sistem tarafından tanımlı mı?
        public bool IsSystemDefined { get; set; }
    }
    
    // Gelir kategori tipleri
    public enum RevenueCategoryType
    {
        // Kira geliri
        Rent = 0,
        
        // Ortak alan geliri
        CommonArea = 1,
        
        // Hizmet geliri
        Service = 2,
        
        // Diğer gelirler
        Other = 3
    }
    
    // Dağıtım metodu
    public enum DistributionMethod
    {
        // Alan (m²) bazlı
        ByArea = 0,
        
        // Daire sayısına göre eşit
        EqualPerUnit = 1,
        
        // Daire başına sabit tutar
        FixedAmountPerUnit = 2,
        
        // Özel dağıtım oranları
        CustomRates = 3,
        
        // Daire sahibine doğrudan
        DirectToOwner = 4
    }
} 