using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Enums;
using ResidenceManagement.Core.Models.Services;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gider işlemlerini temsil eden sınıf
    public class ExpenseTransaction
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Firma ve şube bilgileri (multi-tenant yapı için)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkili rezidans/site bilgisi
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        
        // İşlem tipi
        public ExpenseTransactionType TransactionType { get; set; }
        
        // Gider kategorisi
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        
        // İşlem tarihi
        public DateTime TransactionDate { get; set; }
        
        // Tedarikçi bilgisi
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        
        // İşlem açıklaması
        public string Description { get; set; }
        
        // Gider tutarı
        public decimal Amount { get; set; }
        
        // Para birimi
        public string Currency { get; set; }
        
        // Kullanılan döviz kuru (TL dışındaki para birimleri için)
        public decimal? ExchangeRate { get; set; }
        
        // TL cinsinden tutar
        public decimal TRYAmount { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Toplam tutar (KDV dahil)
        public decimal TotalAmount { get; set; }
        
        // Ödeme durumu
        public ResidenceManagement.Core.Enums.PaymentStatus PaymentStatus { get; set; }
        
        // Ödeme tarihi
        public DateTime? PaymentDate { get; set; }
        
        // Ödeme yöntemi
        public PaymentMethod? PaymentMethod { get; set; }
        
        // Banka transferi ise referans numarası
        public string BankTransferReference { get; set; }
        
        // Bütçe dönemi
        public DateTime BudgetPeriodStart { get; set; }
        public DateTime BudgetPeriodEnd { get; set; }
        
        // Bütçe ID
        public int? BudgetId { get; set; }
        
        // Belge numarası
        public string DocumentNumber { get; set; }
        
        // İlgili dokümanlar listesi
        public List<ExpenseDocument> Documents { get; set; } = new List<ExpenseDocument>();
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // İşlem durumu
        public bool IsActive { get; set; }
        
        // Onay durumu
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        
        // İptal edilme durumu
        public bool IsCancelled { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string CancellationReason { get; set; }
        
        public ExpenseTransaction()
        {
            TransactionDate = DateTime.Now;
            PaymentStatus = ResidenceManagement.Core.Enums.PaymentStatus.Beklemede;
            IsActive = true;
            Currency = "TRY";
        }
    }
    
    // İşlem dokümanları
    public class ExpenseDocument
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili işlem ID
        public int TransactionId { get; set; }
        
        // Doküman tipi
        public ExpenseDocumentType DocumentType { get; set; }
        
        // Dosya adı
        public string FileName { get; set; }
        
        // Dosya yolu
        public string FilePath { get; set; }
        
        // Dosya boyutu (byte)
        public long FileSize { get; set; }
        
        // MIME tipi
        public string ContentType { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Yükleme tarihi
        public DateTime UploadDate { get; set; }
        
        // Yükleyen kullanıcı
        public string UploadedBy { get; set; }
    }
    
    // Gider işlem tipi
    public enum ExpenseTransactionType
    {
        // Genel giderler
        GeneralExpense = 0,
        
        // Personel ödemeleri
        StaffPayment = 1,
        
        // Bakım ve onarım
        MaintenanceRepair = 2,
        
        // Yatırım harcamaları
        CapitalExpenditure = 3,
        
        // Tedarikçi ödemeleri
        SupplierPayment = 4,
        
        // Vergiler ve resmi harçlar
        TaxesAndFees = 5,
        
        // Diğer giderler
        OtherExpense = 6
    }
    
    // Doküman tipi
    public enum ExpenseDocumentType
    {
        // Fatura
        Invoice = 0,
        
        // Makbuz
        Receipt = 1,
        
        // Dekont
        BankStatement = 2,
        
        // Sözleşme
        Contract = 3,
        
        // Teklif
        Quotation = 4,
        
        // Teslimat belgesi
        DeliveryNote = 5,
        
        // Diğer
        Other = 6
    }
} 