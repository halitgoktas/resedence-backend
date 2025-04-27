// ExpensePaymentStatus enum'u gider ödeme durumlarını tanımlar
using System;

namespace ResidenceManagement.Core.Models.Financial.Enums
{
    /// <summary>
    /// Gider ödeme durumlarını tanımlar
    /// </summary>
    public enum ExpensePaymentStatus
    {
        /// <summary>
        /// Ödenmedi
        /// </summary>
        Unpaid = 1,
        
        /// <summary>
        /// Kısmen Ödendi
        /// </summary>
        PartiallyPaid = 2,
        
        /// <summary>
        /// Ödendi
        /// </summary>
        Paid = 3,
        
        /// <summary>
        /// Gecikmiş
        /// </summary>
        Overdue = 4,
        
        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 5
    }
} 