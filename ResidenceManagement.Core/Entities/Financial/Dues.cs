using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Core.Entities.Financial
{
    // Aidat modeli
    public class Dues : BaseTransactionEntity
    {
        public int ApartmentId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } // TRY, USD, EUR, GBP
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal? PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public int? PaymentId { get; set; }
        
        // Navigation Properties
        public virtual Apartment Apartment { get; set; }
        public virtual Payment Payment { get; set; }
    }
} 