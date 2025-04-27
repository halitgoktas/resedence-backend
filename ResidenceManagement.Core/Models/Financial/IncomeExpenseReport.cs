using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gelir ve gider raporlaması için model
    public class IncomeExpenseReport
    {
        // Raporun başlangıç tarihi
        public DateTime StartDate { get; set; }
        
        // Raporun bitiş tarihi
        public DateTime EndDate { get; set; }
        
        // Toplam gelir
        public decimal TotalIncome { get; set; }
        
        // Toplam gider
        public decimal TotalExpense { get; set; }
        
        // Net bakiye (gelir-gider)
        public decimal NetBalance => TotalIncome - TotalExpense;
        
        // Para birimi
        public string Currency { get; set; }
        
        // Gelir kalemleri listesi
        public List<IncomeExpenseItem> IncomeItems { get; set; }
        
        // Gider kalemleri listesi
        public List<IncomeExpenseItem> ExpenseItems { get; set; }
        
        // Kategori bazlı gelir dağılımı
        public Dictionary<string, decimal> IncomeCategoryDistribution { get; set; }
        
        // Kategori bazlı gider dağılımı
        public Dictionary<string, decimal> ExpenseCategoryDistribution { get; set; }
        
        // Aylık gelir dağılımı
        public Dictionary<string, decimal> MonthlyIncomeDistribution { get; set; }
        
        // Aylık gider dağılımı
        public Dictionary<string, decimal> MonthlyExpenseDistribution { get; set; }
        
        // Daire bazlı gelir dağılımı
        public Dictionary<string, decimal> ApartmentIncomeDistribution { get; set; }
        
        // Daire bazlı gider dağılımı
        public Dictionary<string, decimal> ApartmentExpenseDistribution { get; set; }
        
        // Site yönetimi ve daire sahipleri arasındaki gelir dağılımı
        public Dictionary<string, decimal> RevenueDistribution { get; set; }

        public IncomeExpenseReport()
        {
            IncomeItems = new List<IncomeExpenseItem>();
            ExpenseItems = new List<IncomeExpenseItem>();
            IncomeCategoryDistribution = new Dictionary<string, decimal>();
            ExpenseCategoryDistribution = new Dictionary<string, decimal>();
            MonthlyIncomeDistribution = new Dictionary<string, decimal>();
            MonthlyExpenseDistribution = new Dictionary<string, decimal>();
            ApartmentIncomeDistribution = new Dictionary<string, decimal>();
            ApartmentExpenseDistribution = new Dictionary<string, decimal>();
            RevenueDistribution = new Dictionary<string, decimal>();
        }
    }
    
    // Gelir veya gider kalemi
    public class IncomeExpenseItem
    {
        // Kalemin benzersiz kimliği
        public int Id { get; set; }
        
        // Kalem açıklaması
        public string Description { get; set; }
        
        // Kalem kategorisi (aidat, kira, bakım vs.)
        public string Category { get; set; }
        
        // Tutar
        public decimal Amount { get; set; }
        
        // İşlem tarihi
        public DateTime Date { get; set; }
        
        // İlgili daire ID (varsa)
        public int? ApartmentId { get; set; }
        
        // İlgili daire numarası (varsa)
        public string ApartmentNumber { get; set; }
        
        // İlgili kişi/firma adı
        public string RelatedParty { get; set; }
        
        // Fatura/belge numarası
        public string ReferenceNumber { get; set; }
        
        // Ödeme metodu
        public string PaymentMethod { get; set; }
        
        // Para birimi
        public string Currency { get; set; }
        
        // Döviz kuru
        public decimal? ExchangeRate { get; set; }
        
        // TL karşılığı
        public decimal? AmountInTRY { get; set; }
    }
} 