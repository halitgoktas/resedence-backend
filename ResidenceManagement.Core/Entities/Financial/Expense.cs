using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Financial
{
    /// <summary>
    /// Gider sınıfı
    /// </summary>
    public class Expense : BaseTransactionEntity
    {
        /// <summary>
        /// Gider başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gider açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gider kategori ID
        /// </summary>
        public int ExpenseCategoryId { get; set; }

        /// <summary>
        /// Gider tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Döviz kuru (TL karşılığı)
        /// </summary>
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// TL karşılığı tutar
        /// </summary>
        public decimal? AmountInTRY { get; set; }

        /// <summary>
        /// Gider tarihi
        /// </summary>
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// Fiş/Fatura numarası
        /// </summary>
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// Fiş/Fatura resmi URL
        /// </summary>
        public string ReceiptImageUrl { get; set; }

        /// <summary>
        /// Tedarikçi adı
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Tedarikçi vergi numarası
        /// </summary>
        public string SupplierTaxNumber { get; set; }

        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string PaymentMethod { get; set; } // Cash, CreditCard, BankTransfer

        /// <summary>
        /// Ödeme yapıldı mı?
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Site/Rezidans ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Blok ID (eğer blok özelinde gider ise)
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Onaylı mı?
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Onaylayan kullanıcı ID
        /// </summary>
        public int? ApprovedById { get; set; }

        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Bütçeye dahil mi?
        /// </summary>
        public bool IncludeInBudget { get; set; } = true;

        /// <summary>
        /// Yinelenen gider mi?
        /// </summary>
        public bool IsRecurring { get; set; }

        /// <summary>
        /// Yineleme dönemi (1: Aylık, 2: 3 Aylık, 3: 6 Aylık, 4: Yıllık)
        /// </summary>
        public int? RecurrencePeriod { get; set; }

        /// <summary>
        /// Bir sonraki yineleme tarihi
        /// </summary>
        public DateTime? NextRecurrenceDate { get; set; }

        // Navigation Properties
        /// <summary>
        /// Gider kategorisi
        /// </summary>
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
} 