using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Models.Financial;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Servis sağlayıcılarının faturalandırma işlemleri için ServiceInvoice sınıfı
    /// </summary>
    public class ServiceInvoice : BaseEntity
    {
        /// <summary>
        /// Bağlı olduğu fatura ID
        /// </summary>
        public int? InvoiceId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu fatura
        /// </summary>
        public virtual Invoice Invoice { get; set; }
        
        /// <summary>
        /// Servis sağlayıcı ID
        /// </summary>
        public int ServiceProviderId { get; set; }
        
        /// <summary>
        /// Servis sağlayıcı
        /// </summary>
        public virtual ServiceProvider ServiceProvider { get; set; }
        
        /// <summary>
        /// Servis talebi ID (varsa)
        /// </summary>
        public int? ServiceRequestId { get; set; }
        
        /// <summary>
        /// Servis talebi
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; }
        
        /// <summary>
        /// Bakım planı ID (varsa)
        /// </summary>
        public int? MaintenancePlanId { get; set; }
        
        /// <summary>
        /// Bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
        
        /// <summary>
        /// Servis raporu ID (varsa)
        /// </summary>
        public int? ServiceReportId { get; set; }
        
        /// <summary>
        /// Servis raporu
        /// </summary>
        public virtual ServiceReport ServiceReport { get; set; }
        
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// Fatura tarihi
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        
        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime? DueDate { get; set; }
        
        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VATAmount { get; set; }
        
        /// <summary>
        /// Toplam tutar (KDV dahil)
        /// </summary>
        public decimal GrandTotal { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Fatura dosya yolu
        /// </summary>
        public string InvoiceFilePath { get; set; }
        
        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal PaidAmount { get; set; }
        
        /// <summary>
        /// Kalan tutar
        /// </summary>
        public decimal RemainingAmount { get; set; }
        
        /// <summary>
        /// Son ödeme tarihi
        /// </summary>
        public DateTime? LastPaymentDate { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Varsayılan constructor
        /// </summary>
        public ServiceInvoice()
        {
            InvoiceDate = DateTime.Now;
            PaymentStatus = PaymentStatus.Unpaid;
            Currency = "TRY";
            PaidAmount = 0;
            RemainingAmount = 0;
            IsActive = true;
        }
        
        /// <summary>
        /// Toplam tutarları hesapla
        /// </summary>
        public void CalculateTotals()
        {
            GrandTotal = TotalAmount + VATAmount;
            RemainingAmount = GrandTotal - PaidAmount;
            
            if (RemainingAmount <= 0)
            {
                PaymentStatus = PaymentStatus.Paid;
                RemainingAmount = 0;
            }
            else if (PaidAmount > 0)
            {
                PaymentStatus = PaymentStatus.PartiallyPaid;
            }
            else
            {
                PaymentStatus = PaymentStatus.Unpaid;
            }
        }
        
        /// <summary>
        /// Ödeme ekle
        /// </summary>
        /// <param name="amount">Ödeme tutarı</param>
        /// <param name="paymentDate">Ödeme tarihi</param>
        public void AddPayment(decimal amount, DateTime paymentDate)
        {
            PaidAmount += amount;
            LastPaymentDate = paymentDate;
            CalculateTotals();
        }
    }
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Ödenmedi
        /// </summary>
        Unpaid = 0,
        
        /// <summary>
        /// Kısmen ödendi
        /// </summary>
        PartiallyPaid = 1,
        
        /// <summary>
        /// Tamamen ödendi
        /// </summary>
        Paid = 2,
        
        /// <summary>
        /// Gecikmiş ödeme
        /// </summary>
        Overdue = 3,
        
        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 4,
        
        /// <summary>
        /// İade edildi
        /// </summary>
        Refunded = 5
    }
} 