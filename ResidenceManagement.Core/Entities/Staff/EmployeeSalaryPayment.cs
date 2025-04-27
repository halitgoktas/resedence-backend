using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Staff
{
    /// <summary>
    /// Personel maaş ödemeleri sınıfı
    /// </summary>
    public class EmployeeSalaryPayment : BaseTransactionEntity
    {
        /// <summary>
        /// Personel ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Maaş dönemi yılı
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Maaş dönemi ayı
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Taban maaş
        /// </summary>
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// Brüt tutar
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// Net tutar
        /// </summary>
        public decimal NetAmount { get; set; }

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
        /// Ödeme tarihi
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string PaymentMethod { get; set; } // Cash, BankTransfer

        /// <summary>
        /// Banka transferi referans numarası
        /// </summary>
        public string BankReferenceNumber { get; set; }

        /// <summary>
        /// SGK kesintisi
        /// </summary>
        public decimal SocialSecurityDeduction { get; set; }

        /// <summary>
        /// Gelir vergisi kesintisi
        /// </summary>
        public decimal IncomeTaxDeduction { get; set; }

        /// <summary>
        /// Diğer kesintiler
        /// </summary>
        public decimal OtherDeductions { get; set; }

        /// <summary>
        /// Toplam kesintiler
        /// </summary>
        public decimal TotalDeductions { get; set; }

        /// <summary>
        /// Yemek yardımı
        /// </summary>
        public decimal MealAllowance { get; set; }

        /// <summary>
        /// Ulaşım yardımı
        /// </summary>
        public decimal TransportAllowance { get; set; }

        /// <summary>
        /// Fazla mesai ödemesi
        /// </summary>
        public decimal OvertimePayment { get; set; }

        /// <summary>
        /// Prim ödemesi
        /// </summary>
        public decimal BonusPayment { get; set; }

        /// <summary>
        /// Diğer ödemeler
        /// </summary>
        public decimal OtherAllowances { get; set; }

        /// <summary>
        /// Toplam ilave ödemeler
        /// </summary>
        public decimal TotalAllowances { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Bordro dosyası URL
        /// </summary>
        public string PayrollDocumentUrl { get; set; }

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

        // Navigation Properties
        /// <summary>
        /// Personel
        /// </summary>
        public virtual Employee Employee { get; set; }
    }
} 