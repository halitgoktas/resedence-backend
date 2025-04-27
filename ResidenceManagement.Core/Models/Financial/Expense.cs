using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Models.Services;
using ResidenceManagement.Core.Models.Financial.Enums;

namespace ResidenceManagement.Core.Models.Financial
{
    /// <summary>
    /// Gider kayıtlarını modelleyen sınıf
    /// </summary>
    public class Expense : BaseEntity
    {
        /// <summary>
        /// Gider kodu
        /// </summary>
        public string ExpenseCode { get; set; }
        
        /// <summary>
        /// Gider adı/başlığı
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Gider açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gider kategorisi ID
        /// </summary>
        public int ExpenseCategoryId { get; set; }
        
        /// <summary>
        /// Gider kategorisi
        /// </summary>
        public ExpenseCategory ExpenseCategory { get; set; }
        
        /// <summary>
        /// Gider tarihi
        /// </summary>
        public DateTime ExpenseDate { get; set; }
        
        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime? DueDate { get; set; }
        
        /// <summary>
        /// Tedarikçi/Satıcı ID
        /// </summary>
        public int? SupplierId { get; set; }
        
        /// <summary>
        /// Tedarikçi/Satıcı
        /// </summary>
        public Supplier Supplier { get; set; }
        
        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// KDV oranı
        /// </summary>
        public decimal VATRate { get; set; }
        
        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VATAmount { get; set; }
        
        /// <summary>
        /// Toplam tutar (KDV dahil)
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public ExpensePaymentMethod PaymentMethod { get; set; }
        
        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public ExpensePaymentStatus PaymentStatus { get; set; }
        
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// Belge/dosya yolu
        /// </summary>
        public string DocumentPath { get; set; }
        
        /// <summary>
        /// Gider notları
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Onay durumu
        /// </summary>
        public ExpenseApprovalStatus ApprovalStatus { get; set; }
        
        /// <summary>
        /// Onaylayan kişi
        /// </summary>
        public string ApprovedBy { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// İlişkili fatura kalemleri
        /// </summary>
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        
        /// <summary>
        /// Dağıtım yapıldı mı?
        /// </summary>
        public bool IsDistributed { get; set; }
        
        /// <summary>
        /// Gider dağıtımları
        /// </summary>
        public virtual ICollection<ExpenseDistribution> Distributions { get; set; }
        
        public Expense()
        {
            ExpenseDate = DateTime.Now;
            VATRate = 18; // Varsayılan KDV oranı %18
            Currency = "TRY";
            PaymentStatus = ExpensePaymentStatus.Unpaid;
            ApprovalStatus = ExpenseApprovalStatus.Draft;
            IsActive = true;
            IsDistributed = false;
            InvoiceItems = new HashSet<InvoiceItem>();
            Distributions = new HashSet<ExpenseDistribution>();
        }
        
        /// <summary>
        /// KDV tutarını ve toplam tutarı hesapla
        /// </summary>
        public void CalculateAmounts()
        {
            VATAmount = Amount * (VATRate / 100);
            TotalAmount = Amount + VATAmount;
        }
    }
} 