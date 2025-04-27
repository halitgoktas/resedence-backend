using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Financial
{
    /// <summary>
    /// Finansal özet sınıfı (Dashboard için)
    /// </summary>
    public class FinancialSummary : BaseEntity
    {
        /// <summary>
        /// Site/Rezidans ID
        /// </summary>
        public int ResidenceId { get; set; }

        /// <summary>
        /// Dönem (yıl-ay formatında)
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Dönem başlangıç tarihi
        /// </summary>
        public DateTime PeriodStartDate { get; set; }

        /// <summary>
        /// Dönem bitiş tarihi
        /// </summary>
        public DateTime PeriodEndDate { get; set; }

        /// <summary>
        /// Toplam gelir
        /// </summary>
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// Toplam gider
        /// </summary>
        public decimal TotalExpense { get; set; }

        /// <summary>
        /// Net kar/zarar
        /// </summary>
        public decimal NetProfit => TotalIncome - TotalExpense;

        /// <summary>
        /// Aidat gelirleri
        /// </summary>
        public decimal DuesIncome { get; set; }

        /// <summary>
        /// Hizmet gelirleri
        /// </summary>
        public decimal ServiceIncome { get; set; }

        /// <summary>
        /// Diğer gelirler
        /// </summary>
        public decimal OtherIncome { get; set; }

        /// <summary>
        /// Personel giderleri
        /// </summary>
        public decimal StaffExpenses { get; set; }

        /// <summary>
        /// Bakım onarım giderleri
        /// </summary>
        public decimal MaintenanceExpenses { get; set; }

        /// <summary>
        /// Elektrik giderleri
        /// </summary>
        public decimal ElectricityExpenses { get; set; }

        /// <summary>
        /// Su giderleri
        /// </summary>
        public decimal WaterExpenses { get; set; }

        /// <summary>
        /// Doğalgaz giderleri
        /// </summary>
        public decimal GasExpenses { get; set; }

        /// <summary>
        /// İnternet ve telefon giderleri
        /// </summary>
        public decimal TelecomExpenses { get; set; }

        /// <summary>
        /// Güvenlik giderleri
        /// </summary>
        public decimal SecurityExpenses { get; set; }

        /// <summary>
        /// Temizlik giderleri
        /// </summary>
        public decimal CleaningExpenses { get; set; }

        /// <summary>
        /// Diğer giderler
        /// </summary>
        public decimal OtherExpenses { get; set; }

        /// <summary>
        /// Tahsil edilecek aidat tutarı
        /// </summary>
        public decimal DuesBilledAmount { get; set; }

        /// <summary>
        /// Tahsil edilen aidat tutarı
        /// </summary>
        public decimal DuesPaidAmount { get; set; }

        /// <summary>
        /// Aidat tahsilat oranı
        /// </summary>
        public decimal DuesCollectionRate => DuesBilledAmount > 0 ? (DuesPaidAmount / DuesBilledAmount) * 100 : 0;

        /// <summary>
        /// Toplam bekleyen ödeme tutarı
        /// </summary>
        public decimal TotalPendingPayments { get; set; }

        /// <summary>
        /// Toplam gecikmiş ödeme tutarı
        /// </summary>
        public decimal TotalOverduePayments { get; set; }

        /// <summary>
        /// Kasa bakiyesi
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// Banka bakiyesi
        /// </summary>
        public decimal BankBalance { get; set; }

        /// <summary>
        /// Toplam bakiye
        /// </summary>
        public decimal TotalBalance => CashBalance + BankBalance;

        /// <summary>
        /// Departman bazlı gider dağılımı (JSON formatında)
        /// </summary>
        public string DepartmentExpenseBreakdown { get; set; }

        /// <summary>
        /// Kategori bazlı gider dağılımı (JSON formatında)
        /// </summary>
        public string CategoryExpenseBreakdown { get; set; }

        /// <summary>
        /// Aylık gelir trendi (JSON formatında)
        /// </summary>
        public string MonthlyIncomeTrend { get; set; }

        /// <summary>
        /// Aylık gider trendi (JSON formatında)
        /// </summary>
        public string MonthlyExpenseTrend { get; set; }

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>
        /// Bu özeti oluşturan kullanıcı ID
        /// </summary>
        public int? GeneratedById { get; set; }

        /// <summary>
        /// Özeti oluşturan kullanıcı adı
        /// </summary>
        public string GeneratedByName { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
    }
} 