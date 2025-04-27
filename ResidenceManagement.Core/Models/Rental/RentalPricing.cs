using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Rental
{
    // Kiralık daireler için fiyatlandırma tablosu sınıfı
    public class RentalPricing
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Fiyat kodu (örn: "STD2023", "PREM2023Q4" vb.)
        public string PriceCode { get; set; }
        
        // Fiyat adı (örn: "Standart Fiyat", "Yaz Sezonu" vb.)
        public string Name { get; set; }
        
        // Fiyat açıklaması
        public string Description { get; set; }
        
        // İlişkili rezidans/site bilgisi
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        
        // Blok bilgisi (null ise tüm bloklarda geçerli)
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        
        // Daire tipi (stüdyo, 1+1, 2+1 vb.)
        public string ApartmentType { get; set; }
        
        // Daire alanı (m² aralığı)
        public decimal? MinArea { get; set; }
        public decimal? MaxArea { get; set; }
        
        // Kat aralığı (min-max)
        public int? MinFloor { get; set; }
        public int? MaxFloor { get; set; }
        
        // Fiyat geçerlilik süresi
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        
        // Günlük fiyat
        public decimal DailyPrice { get; set; }
        
        // Haftalık fiyat
        public decimal WeeklyPrice { get; set; }
        
        // Aylık fiyat
        public decimal MonthlyPrice { get; set; }
        
        // 3 Aylık fiyat
        public decimal QuarterlyPrice { get; set; }
        
        // 6 Aylık fiyat
        public decimal SemiAnnualPrice { get; set; }
        
        // Yıllık fiyat
        public decimal AnnualPrice { get; set; }
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // KDV dahil mi?
        public bool IsVATIncluded { get; set; }
        
        // Komisyon oranı (%)
        public decimal CommissionRate { get; set; }
        
        // Depozito miktarı
        public decimal DepositAmount { get; set; }
        
        // Sezon bilgisi (Yüksek, Orta, Düşük)
        public string SeasonType { get; set; }
        
        // Minimum kiralama süresi (gün)
        public int MinimumRentalDays { get; set; }
        
        // Online rezervasyona açık mı?
        public bool IsAvailableForOnlineReservation { get; set; }
        
        // Manuel fiyat girişine izin verilsin mi?
        public bool AllowManualPriceInput { get; set; }
        
        // Durum (aktif/pasif)
        public bool IsActive { get; set; }
        
        // Özel fiyatlandırma seçenekleri
        public ICollection<RentalPricingOption> PricingOptions { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public RentalPricing()
        {
            EffectiveFrom = DateTime.Now;
            IsActive = true;
            CurrencyCode = "TRY";
            VATRate = 18; // Varsayılan %18 KDV
            IsVATIncluded = false;
            CommissionRate = 10; // Varsayılan %10 komisyon
            MinimumRentalDays = 1;
            IsAvailableForOnlineReservation = true;
            AllowManualPriceInput = false;
            CreatedDate = DateTime.Now;
            PricingOptions = new List<RentalPricingOption>();
        }
        
        // Belirli bir tarih aralığı için fiyat hesaplama
        public decimal CalculatePriceForDateRange(DateTime startDate, DateTime endDate)
        {
            int days = (int)(endDate - startDate).TotalDays;
            
            if (days <= 0)
                return 0;
                
            // 1 yıl ve üzeri
            if (days >= 365)
                return AnnualPrice * (days / 365.0m);
                
            // 6 ay ve üzeri
            if (days >= 180)
                return SemiAnnualPrice * (days / 180.0m);
                
            // 3 ay ve üzeri
            if (days >= 90)
                return QuarterlyPrice * (days / 90.0m);
                
            // 1 ay ve üzeri
            if (days >= 30)
                return MonthlyPrice * (days / 30.0m);
                
            // 1 hafta ve üzeri
            if (days >= 7)
                return WeeklyPrice * (days / 7.0m);
                
            // Günlük
            return DailyPrice * days;
        }
        
        // KDV tutarını hesaplama
        public decimal CalculateVATAmount(decimal baseAmount)
        {
            if (IsVATIncluded)
                return baseAmount - (baseAmount / (1 + (VATRate / 100)));
            
            return baseAmount * (VATRate / 100);
        }
        
        // Toplam fiyatı hesaplama (KDV dahil)
        public decimal CalculateTotalPrice(decimal baseAmount)
        {
            if (IsVATIncluded)
                return baseAmount;
            
            return baseAmount + CalculateVATAmount(baseAmount);
        }
    }
    
    // Özel fiyatlandırma seçeneği (ör: "Ekstra temizlik", "Havuz kullanımı" vb.)
    public class RentalPricingOption
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili fiyat tablosu
        public int RentalPricingId { get; set; }
        
        // Seçenek adı
        public string Name { get; set; }
        
        // Seçenek açıklaması
        public string Description { get; set; }
        
        // Ek ücret (sabit)
        public decimal AdditionalPrice { get; set; }
        
        // Ek ücret (yüzde) - base price'ın %X'i şeklinde
        public decimal? AdditionalPricePercentage { get; set; }
        
        // Ücretlendirme şekli (Sabit, Günlük, Haftalık, vb.)
        public PriceFrequency PriceFrequency { get; set; }
        
        // Varsayılan olarak seçili mi?
        public bool IsSelectedByDefault { get; set; }
        
        // Zorunlu mu?
        public bool IsRequired { get; set; }
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // Durum (aktif/pasif)
        public bool IsActive { get; set; }
    }
    
    // Fiyat sıklığı (ne kadar süre için geçerli olduğu)
    public enum PriceFrequency
    {
        // Sabit (tüm süre için tek bir fiyat)
        Fixed = 0,
        
        // Toplam süre boyunca günlük
        Daily = 1,
        
        // Toplam süre boyunca haftalık
        Weekly = 2,
        
        // Toplam süre boyunca aylık
        Monthly = 3,
        
        // Bir defaya mahsus
        OneTime = 4
    }
} 