// ExpensePaymentMethod enum'u gider ödeme yöntemlerini tanımlar
using System;

namespace ResidenceManagement.Core.Models.Financial.Enums
{
    /// <summary>
    /// Gider ödeme yöntemlerini tanımlar
    /// </summary>
    public enum ExpensePaymentMethod
    {
        /// <summary>
        /// Nakit
        /// </summary>
        Cash = 1,
        
        /// <summary>
        /// Kredi Kartı
        /// </summary>
        CreditCard = 2,
        
        /// <summary>
        /// Banka Havalesi
        /// </summary>
        BankTransfer = 3,
        
        /// <summary>
        /// Çek
        /// </summary>
        Check = 4,
        
        /// <summary>
        /// Diğer
        /// </summary>
        Other = 5
    }
} 