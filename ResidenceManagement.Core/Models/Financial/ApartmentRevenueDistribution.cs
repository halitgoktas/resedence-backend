using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Daire gelir dağıtım bilgilerini içeren sınıf
    public class ApartmentRevenueDistribution
    {
        // Daire ID
        public int ApartmentId { get; set; }
        
        // Daire bilgileri
        public string ApartmentNumber { get; set; }
        public string BlockName { get; set; }
        public int FloorNumber { get; set; }
        
        // Malik bilgileri
        public string OwnerName { get; set; }
        public string OwnerTaxNumber { get; set; }
        public string OwnerIdentityNumber { get; set; }
        public string OwnerIBAN { get; set; }
        public string OwnerContactInfo { get; set; }
        
        // Rapor dönemi
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Para birimi
        public string Currency { get; set; }
        
        // Toplam gelir tutarları
        public decimal TotalGrossIncome { get; set; }
        public decimal TotalNetIncome { get; set; }
        
        // Yönetim payı toplam tutar
        public decimal TotalManagementShare { get; set; }
        
        // KDV toplam tutar
        public decimal TotalVATAmount { get; set; }
        
        // Stopaj toplam tutar
        public decimal TotalWithholdingTaxAmount { get; set; }
        
        // Detaylı gelir kalemleri
        public List<ApartmentRevenueItem> RevenueItems { get; set; } = new List<ApartmentRevenueItem>();
        
        // Aylık dağılım
        public List<MonthlyApartmentRevenue> MonthlyDistribution { get; set; } = new List<MonthlyApartmentRevenue>();
        
        // Kategori dağılımı
        public List<CategoryApartmentRevenue> CategoryDistribution { get; set; } = new List<CategoryApartmentRevenue>();
        
        // Dağıtım ayarları
        public RevenueDistributionSettings AppliedSettings { get; set; }
        
        // Ödeme durumu
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentReference { get; set; }
        public decimal PaidAmount { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    
    // Daire gelir kalemi
    public class ApartmentRevenueItem
    {
        // Gelir kalemi ID
        public int Id { get; set; }
        
        // İlişkili daire
        public int ApartmentId { get; set; }
        
        // Gelir kalemi açıklaması
        public string Description { get; set; }
        
        // Gelir kategorisi
        public string Category { get; set; }
        
        // Gelir tarihi
        public DateTime Date { get; set; }
        
        // Toplam tutar (Brüt)
        public decimal GrossAmount { get; set; }
        
        // Yönetim payı tutarı
        public decimal ManagementShare { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Stopaj tutarı
        public decimal WithholdingTaxAmount { get; set; }
        
        // Malik net payı
        public decimal OwnerNetAmount { get; set; }
        
        // Para birimi
        public string Currency { get; set; }
        
        // Uygulanan dağıtım ayarları
        public RevenueDistributionSettings AppliedSettings { get; set; }
    }
    
    // Aylık daire gelir dağılımı
    public class MonthlyApartmentRevenue
    {
        // Yıl
        public int Year { get; set; }
        
        // Ay
        public int Month { get; set; }
        
        // Ay adı
        public string MonthName { get; set; }
        
        // Toplam gelir (Brüt)
        public decimal GrossIncome { get; set; }
        
        // Yönetim payı
        public decimal ManagementShare { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Stopaj tutarı
        public decimal WithholdingTaxAmount { get; set; }
        
        // Malik net gelir
        public decimal OwnerNetIncome { get; set; }
    }
    
    // Kategori bazında daire gelir dağılımı
    public class CategoryApartmentRevenue
    {
        // Kategori adı
        public string Category { get; set; }
        
        // Toplam gelir (Brüt)
        public decimal GrossIncome { get; set; }
        
        // Yönetim payı
        public decimal ManagementShare { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Stopaj tutarı
        public decimal WithholdingTaxAmount { get; set; }
        
        // Malik net gelir
        public decimal OwnerNetIncome { get; set; }
    }
} 