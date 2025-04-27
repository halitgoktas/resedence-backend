using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Faturaların ödemelerini takip eden sınıf
    public class Payment
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Ödeme numarası
        public string PaymentNumber { get; set; }
        
        // Ödeme tarihi
        public DateTime PaymentDate { get; set; }
        
        // Ödeme türü (nakit, kredi kartı, banka havalesi, vb.)
        public PaymentType PaymentType { get; set; }
        
        // Ödeme tutarı
        public decimal Amount { get; set; }
        
        // Ödeme açıklaması
        public string Description { get; set; }
        
        // Ödemeyi yapan kişi bilgisi
        public int? PersonId { get; set; }
        public string PayerName { get; set; }
        
        // Ödemeyi alan kişi/kurum
        public string ReceivedBy { get; set; }
        
        // Banka bilgileri (banka ödemeleri için)
        public int? BankAccountId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        
        // Çek bilgileri (çek ödemeleri için)
        public string CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string CheckBankName { get; set; }
        
        // Kredi kartı bilgileri (kredi kartı ödemeleri için)
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; } // Maskelenmiş olmalı
        public string CardType { get; set; }
        public int? InstallmentCount { get; set; }
        
        // Ödeme onay bilgileri
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        
        // İptal bilgileri
        public bool IsCancelled { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string CancellationReason { get; set; }
        
        // Bağlantılı faturalar
        public ICollection<PaymentInvoice> PaymentInvoices { get; set; }
        
        // Makbuz/Fiş bilgileri
        public string ReceiptNumber { get; set; }
        public string DocumentPath { get; set; }
        
        // Notlar
        public string Notes { get; set; }
        
        // Aktif/Pasif durumu
        public bool IsActive { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public Payment()
        {
            PaymentDate = DateTime.Now;
            IsApproved = false;
            IsCancelled = false;
            IsActive = true;
            CreatedDate = DateTime.Now;
            PaymentInvoices = new List<PaymentInvoice>();
        }
    }
    
    // Ödeme türü enum
    public enum PaymentType
    {
        Cash = 1,          // Nakit
        CreditCard = 2,    // Kredi Kartı
        BankTransfer = 3,  // Banka Havalesi
        Check = 4,         // Çek
        OnlinePayment = 5, // Online Ödeme
        Debit = 6,         // Otomatik Ödeme / Düzenli Ödeme
        Other = 99         // Diğer
    }
    
    // Ödeme ve fatura arasındaki ilişkiyi tanımlayan ara tablo
    public class PaymentInvoice
    {
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkili ödeme
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        
        // İlişkili fatura
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        
        // Bu ödeme ile faturaya yansıtılan tutar
        public decimal AllocatedAmount { get; set; }
        
        // Oluşturma bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 