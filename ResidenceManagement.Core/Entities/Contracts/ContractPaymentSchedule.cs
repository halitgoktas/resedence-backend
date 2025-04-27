using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Financial;

namespace ResidenceManagement.Core.Entities.Contracts
{
    /// <summary>
    /// Sözleşmeye ait ödeme planı sınıfı
    /// </summary>
    public class ContractPaymentSchedule : BaseEntity
    {
        /// <summary>
        /// Sözleşme ID
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Taksit numarası
        /// </summary>
        public int InstallmentNumber { get; set; }

        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Taksit tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Ödeme durumu (1: Bekliyor, 2: Kısmi Ödendi, 3: Ödendi, 4: Gecikti)
        /// </summary>
        public int PaymentStatus { get; set; } = 1;

        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal? PaidAmount { get; set; }

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Ödeme referans numarası
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// İlgili ödeme işlemi ID
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        /// Gecikme ücreti
        /// </summary>
        public decimal? LateFee { get; set; }

        /// <summary>
        /// Açıklamalar
        /// </summary>
        public string Notes { get; set; }

        // Navigation Properties
        /// <summary>
        /// Bağlı olduğu sözleşme
        /// </summary>
        public virtual Contract Contract { get; set; }

        /// <summary>
        /// Ödeme kaydı
        /// </summary>
        public virtual Payment Payment { get; set; }
    }
} 