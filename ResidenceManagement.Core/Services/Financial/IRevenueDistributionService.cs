using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Models.Financial;

namespace ResidenceManagement.Core.Services.Financial
{
    // Gelir dağıtım servisi arayüzü
    public interface IRevenueDistributionService
    {
        // Gelir dağıtım ayarlarını getirir
        Task<RevenueDistributionSettings> GetRevenueDistributionSettingsAsync(int residenceId);
        
        // Gelir dağıtım ayarlarını kaydeder
        Task<RevenueDistributionSettings> SaveRevenueDistributionSettingsAsync(RevenueDistributionSettings settings);
        
        // Belirli bir gelir kaleminin dağıtımını hesaplar
        Task<RevenueShareResult> CalculateRevenueShareAsync(
            decimal amount, 
            RevenueDistributionSettings settings, 
            bool applyVAT = true, 
            bool applyWithholdingTax = true);
        
        // Belirli bir tarih aralığındaki gelir dağıtımını hesaplar
        Task<RevenueDistributionReport> CalculatePeriodDistributionAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Belirli bir daire için gelir dağıtımını hesaplar
        Task<ApartmentRevenueDistribution> CalculateApartmentDistributionAsync(
            int apartmentId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
        
        // Gelir dağıtım ayarları tarihçesini getirir
        Task<List<RevenueDistributionSettings>> GetRevenueDistributionSettingsHistoryAsync(
            int residenceId, 
            DateTime? startDate = null, 
            DateTime? endDate = null);
        
        // Belirli bir tarihte geçerli olan gelir dağıtım ayarlarını getirir
        Task<RevenueDistributionSettings> GetActiveRevenueDistributionSettingsAsync(
            int residenceId, 
            DateTime date);
        
        // Excel formatında gelir dağıtım raporu oluşturur
        Task<byte[]> GenerateRevenueDistributionReportAsync(
            int residenceId, 
            DateTime startDate, 
            DateTime endDate, 
            string currency = "TRY");
    }
    
    // Gelir dağıtım sonucu
    public class RevenueShareResult
    {
        // Gelir tutarı
        public decimal TotalAmount { get; set; }
        
        // Site yönetimi payı (brüt)
        public decimal ManagementShareGross { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Site yönetimi payı (KDV dahil)
        public decimal ManagementShareWithVAT => ManagementShareGross + VATAmount;
        
        // Stopaj tutarı
        public decimal WithholdingTaxAmount { get; set; }
        
        // Daire sahibi payı (brüt)
        public decimal OwnerShareGross { get; set; }
        
        // Daire sahibi payı (net - stopaj sonrası)
        public decimal OwnerShareNet => OwnerShareGross - WithholdingTaxAmount;
        
        // Dağıtım özeti
        public string Summary => $"Toplam: {TotalAmount:N2}, Yönetim: {ManagementShareGross:N2}, KDV: {VATAmount:N2}, Stopaj: {WithholdingTaxAmount:N2}, Malik: {OwnerShareNet:N2}";
    }
} 