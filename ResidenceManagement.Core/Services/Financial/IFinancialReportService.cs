using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Models.Financial;

namespace ResidenceManagement.Core.Services.Financial
{
    // Finansal raporlama servisi arayüzü
    public interface IFinancialReportService
    {
        // Belirli bir tarih aralığındaki gelir-gider raporunu oluşturur
        Task<IncomeExpenseReport> GetIncomeExpenseReportAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Belirli bir daire için gelir-gider raporunu oluşturur
        Task<IncomeExpenseReport> GetApartmentIncomeExpenseReportAsync(
            int apartmentId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Kira gelirlerinin dağılımını içeren raporu oluşturur
        Task<Dictionary<string, decimal>> GetRentalIncomeDistributionAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Kategori bazlı gelir dağılımını oluşturur
        Task<Dictionary<string, decimal>> GetIncomeByCategoryAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Kategori bazlı gider dağılımını oluşturur
        Task<Dictionary<string, decimal>> GetExpenseByCategoryAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Aylık gelir trendini oluşturur
        Task<Dictionary<string, decimal>> GetMonthlyIncomeTrendAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Aylık gider trendini oluşturur
        Task<Dictionary<string, decimal>> GetMonthlyExpenseTrendAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Karşılaştırmalı gelir-gider raporunu oluşturur (önceki dönem ile karşılaştırma)
        Task<ComparativeFinancialReport> GetComparativeFinancialReportAsync(
            int residenceId, 
            DateTime currentPeriodStart, 
            DateTime currentPeriodEnd, 
            DateTime previousPeriodStart, 
            DateTime previousPeriodEnd, 
            string currency = "TRY");
        
        // Aidat tahsilat performans raporunu oluşturur
        Task<DuesCollectionReport> GetDuesCollectionPerformanceReportAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Site yönetimi ve daire sahipleri arasındaki gelir dağılımını hesaplar
        Task<RevenueDistributionReport> CalculateRevenueDistributionAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Finansal raporu Excel formatında dışa aktarır
        Task<byte[]> ExportReportToExcelAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string reportType,
            string currency = "TRY");
    }
    
    // Karşılaştırmalı finansal rapor
    public class ComparativeFinancialReport
    {
        public IncomeExpenseReport CurrentPeriod { get; set; }
        public IncomeExpenseReport PreviousPeriod { get; set; }
        
        // Toplam gelirdeki değişim (%)
        public decimal IncomeChangePercentage => CalculateChangePercentage(CurrentPeriod.TotalIncome, PreviousPeriod.TotalIncome);
        
        // Toplam giderdeki değişim (%)
        public decimal ExpenseChangePercentage => CalculateChangePercentage(CurrentPeriod.TotalExpense, PreviousPeriod.TotalExpense);
        
        // Net bakiyedeki değişim (%)
        public decimal NetBalanceChangePercentage => CalculateChangePercentage(CurrentPeriod.NetBalance, PreviousPeriod.NetBalance);
        
        // Kategori bazlı değişim yüzdeleri
        public Dictionary<string, decimal> CategoryChangePercentages { get; set; }
        
        public ComparativeFinancialReport()
        {
            CategoryChangePercentages = new Dictionary<string, decimal>();
        }
        
        // Değişim yüzdesini hesaplayan yardımcı metot
        private decimal CalculateChangePercentage(decimal current, decimal previous)
        {
            if (previous == 0)
                return current > 0 ? 100 : 0;
                
            return ((current - previous) / Math.Abs(previous)) * 100;
        }
    }
    
    // Aidat tahsilat raporu
    public class DuesCollectionReport
    {
        // Toplam tahakkuk eden aidat
        public decimal TotalDuesAmount { get; set; }
        
        // Tahsil edilen aidat
        public decimal CollectedAmount { get; set; }
        
        // Tahsil edilemeyen aidat
        public decimal UncollectedAmount => TotalDuesAmount - CollectedAmount;
        
        // Tahsilat oranı (%)
        public decimal CollectionRate => TotalDuesAmount > 0 ? (CollectedAmount / TotalDuesAmount) * 100 : 0;
        
        // Daire bazlı aidat tahsilatları
        public List<ApartmentDuesCollection> ApartmentCollections { get; set; }
        
        // Aylık tahsilat performansı
        public Dictionary<string, decimal> MonthlyCollectionRates { get; set; }
        
        public DuesCollectionReport()
        {
            ApartmentCollections = new List<ApartmentDuesCollection>();
            MonthlyCollectionRates = new Dictionary<string, decimal>();
        }
    }
    
    // Daire bazlı aidat tahsilatı
    public class ApartmentDuesCollection
    {
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public decimal TotalDuesAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal UncollectedAmount => TotalDuesAmount - CollectedAmount;
        public decimal CollectionRate => TotalDuesAmount > 0 ? (CollectedAmount / TotalDuesAmount) * 100 : 0;
    }
    
    // Gelir dağıtım raporu
    public class RevenueDistributionReport
    {
        // Toplam gelir
        public decimal TotalRevenue { get; set; }
        
        // Site yönetimi payı
        public decimal ManagementShare { get; set; }
        
        // Daire sahipleri payı
        public decimal OwnersShare { get; set; }
        
        // Daire bazlı dağıtım detayları
        public List<ApartmentRevenueDistribution> ApartmentDistributions { get; set; }
        
        // Gelir tipi bazlı dağıtım
        public Dictionary<string, RevenueTypeDistribution> RevenueTypeDistributions { get; set; }
        
        public RevenueDistributionReport()
        {
            ApartmentDistributions = new List<ApartmentRevenueDistribution>();
            RevenueTypeDistributions = new Dictionary<string, RevenueTypeDistribution>();
        }
    }
    
    // Daire bazlı gelir dağıtımı
    public class ApartmentRevenueDistribution
    {
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal ManagementShare { get; set; }
        public decimal OwnerShare { get; set; }
        public decimal VATAmount { get; set; }
        public decimal WithholdingTaxAmount { get; set; }
        public decimal NetPayable => OwnerShare - WithholdingTaxAmount;
    }
    
    // Gelir tipi bazlı dağıtım
    public class RevenueTypeDistribution
    {
        public string RevenueType { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ManagementShare { get; set; }
        public decimal OwnersShare { get; set; }
    }
} 