using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gelir dağıtım ayarlarını içeren sınıf
    public class RevenueDistributionSettings
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Firma ve şube bilgileri (multi-tenant yapı için)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkili rezidans/site bilgisi
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        
        // Ayarların geçerlilik başlangıç tarihi
        public DateTime EffectiveStartDate { get; set; }
        
        // Ayarların geçerlilik bitiş tarihi (null ise halen geçerli)
        public DateTime? EffectiveEndDate { get; set; }
        
        // Yönetim komisyon oranı (%)
        public decimal ManagementCommissionRate { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // Stopaj oranı (%)
        public decimal WithholdingTaxRate { get; set; }
        
        // KDV uygulama tercihi
        public bool ApplyVAT { get; set; }
        
        // Stopaj uygulama tercihi
        public bool ApplyWithholdingTax { get; set; }
        
        // Yönetim komisyon hesaplama yöntemi
        public CommissionCalculationMethod ManagementCommissionMethod { get; set; }
        
        // Yönetim komisyonu uygulama tercihi
        public bool ApplyManagementCommission { get; set; }
        
        // Özel gelir kategorileri ve uygulanacak oranlar
        public List<CategorySpecificRate> CategoryRates { get; set; } = new List<CategorySpecificRate>();
        
        // Özel daire oranları (daire bazında farklı oranlar uygulanabilir)
        public List<ApartmentSpecificRate> ApartmentRates { get; set; } = new List<ApartmentSpecificRate>();
        
        // Ödeme periyodu (aylık, 3 aylık, yıllık vb.)
        public PaymentPeriod PaymentPeriod { get; set; }
        
        // Ödeme günü (ayın hangi günü ödemeler yapılacak)
        public int PaymentDay { get; set; }
        
        // Varsayılan para birimi
        public string DefaultCurrency { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Durum (aktif/pasif)
        public bool IsActive { get; set; }
        
        // Onay durumu
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
    
    // Kategori bazında özel oran tanımlaması
    public class CategorySpecificRate
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Kategori adı
        public string CategoryName { get; set; }
        
        // Yönetim komisyon oranı (%)
        public decimal ManagementCommissionRate { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // Stopaj oranı (%)
        public decimal WithholdingTaxRate { get; set; }
        
        // KDV uygulama tercihi
        public bool ApplyVAT { get; set; }
        
        // Stopaj uygulama tercihi
        public bool ApplyWithholdingTax { get; set; }
        
        // Yönetim komisyon hesaplama yöntemi
        public CommissionCalculationMethod ManagementCommissionMethod { get; set; }
        
        // Yönetim komisyonu uygulama tercihi
        public bool ApplyManagementCommission { get; set; }
    }
    
    // Daire bazında özel oran tanımlaması
    public class ApartmentSpecificRate
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Daire ID
        public int ApartmentId { get; set; }
        
        // Daire bilgileri
        public string ApartmentNumber { get; set; }
        public string BlockName { get; set; }
        
        // Yönetim komisyon oranı (%)
        public decimal ManagementCommissionRate { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // Stopaj oranı (%)
        public decimal WithholdingTaxRate { get; set; }
        
        // KDV uygulama tercihi
        public bool ApplyVAT { get; set; }
        
        // Stopaj uygulama tercihi
        public bool ApplyWithholdingTax { get; set; }
        
        // Yönetim komisyon hesaplama yöntemi
        public CommissionCalculationMethod ManagementCommissionMethod { get; set; }
        
        // Yönetim komisyonu uygulama tercihi
        public bool ApplyManagementCommission { get; set; }
    }
    
    // Yönetim komisyonu hesaplama yöntemi
    public enum CommissionCalculationMethod
    {
        // Brüt tutardan (tüm vergiler öncesi)
        BeforeTaxes = 0,
        
        // Net tutardan (tüm vergiler sonrası)
        AfterTaxes = 1,
        
        // Sabit tutar
        FixedAmount = 2
    }
    
    // Ödeme periyodu
    public enum PaymentPeriod
    {
        // Aylık
        Monthly = 0,
        
        // Üç aylık
        Quarterly = 1,
        
        // Altı aylık
        BiAnnually = 2,
        
        // Yıllık
        Annually = 3
    }
} 