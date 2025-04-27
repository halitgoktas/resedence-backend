using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Entities.Financial
{
    // Ödeme modeli
    public class Payment : BaseTransactionEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } // TRY, USD, EUR, GBP
        public decimal? ExchangeRate { get; set; }
        public decimal? AmountInTRY { get; set; }
        public string PaymentMethod { get; set; } // Cash, CreditCard, BankTransfer
        public string PaymentReference { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string PaymentType { get; set; } // Dues, Rent, Service, etc.
        
        // Navigation Properties
        public virtual User User { get; set; }
        public virtual ICollection<Dues> DuesPayments { get; set; }
    }
} 