using System;

namespace ResidenceManagement.Core.Entities.Base
{
    // İşlem kayıtları için kullanılan temel sınıf
    public abstract class BaseTransactionEntity : BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public int? ProcessedBy { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? RelatedEntityId { get; set; }
        public string RelatedEntityType { get; set; }
        public decimal Amount { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
} 