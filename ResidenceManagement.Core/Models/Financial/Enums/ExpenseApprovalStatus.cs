// ExpenseApprovalStatus enum'u gider onay durumlarını tanımlar
using System;

namespace ResidenceManagement.Core.Models.Financial.Enums
{
    /// <summary>
    /// Gider onay durumlarını tanımlar
    /// </summary>
    public enum ExpenseApprovalStatus
    {
        /// <summary>
        /// Taslak
        /// </summary>
        Draft = 1,
        
        /// <summary>
        /// İnceleme Bekliyor
        /// </summary>
        PendingReview = 2,
        
        /// <summary>
        /// Onaylandı
        /// </summary>
        Approved = 3,
        
        /// <summary>
        /// Reddedildi
        /// </summary>
        Rejected = 4,
        
        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 5
    }
} 