using System;

namespace ResidenceManagement.Core.Models.Financial
{
    // Banka işlemlerini/hareketlerini temsil eden sınıf
    public class BankTransaction
    {
        // Benzersiz kimlik
        public int Id { get; set; }

        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Bağlı olduğu banka hesabı
        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        // İşlem kodu/referans numarası
        public string TransactionCode { get; set; }

        // İşlem tutarı ve para birimi
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } // TRY, USD, EUR, vb.
        public decimal? ExchangeRate { get; set; }
        
        // İşlem sonrası bakiye
        public decimal BalanceAfterTransaction { get; set; }

        // İşlem tarihi bilgileri
        public DateTime TransactionDate { get; set; }
        public DateTime? ValueDate { get; set; } // Valör tarihi

        // İşlem türü
        public TransactionType Type { get; set; }
        
        // İşlem yönü (giriş veya çıkış)
        public TransactionDirection Direction { get; set; }

        // İşlem kategorisi
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        // İşlemin açıklaması
        public string Description { get; set; }

        // Karşı taraf bilgileri
        public string CounterpartyName { get; set; }
        public string CounterpartyAccount { get; set; }
        public string CounterpartyBank { get; set; }

        // İlişkili transfer, ödeme, tahsilat veya açıklama
        public int? RelatedTransferId { get; set; }
        public int? RelatedPaymentId { get; set; }
        public int? RelatedReceiptId { get; set; }
        
        // Komisyon, vergi ve maliyetler
        public decimal? Fee { get; set; }
        public decimal? Tax { get; set; }
        
        // İşlemi durumu
        public TransactionStatus Status { get; set; }
        
        // Notlar ve ek açıklamalar
        public string Notes { get; set; }
        
        // Banka dekont/belge numarası
        public string ReceiptNumber { get; set; }
        
        // Döküman ve dekont id'si
        public int? DocumentId { get; set; }
        
        // İşlemi gerçekleştiren kullanıcı
        public string ProcessedBy { get; set; }
        
        // Manuel girişli mi yoksa otomatik mi
        public bool IsManualEntry { get; set; }
        
        // Sisteme entegrasyon (API, internet bankacılığı) ile mi geldi
        public bool IsFromBankIntegration { get; set; }
        
        // Muhasebe entegrasyonu yapıldı mı
        public bool IsAccountingProcessed { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public BankTransaction()
        {
            CurrencyCode = "TRY";
            TransactionDate = DateTime.Now;
            Status = TransactionStatus.Completed;
            CreatedDate = DateTime.Now;
            Direction = TransactionDirection.In; // Varsayılan olarak giriş
            IsManualEntry = true; // Varsayılan olarak manuel giriş
        }
    }

    // İşlem türü
    public enum TransactionType
    {
        Deposit = 1,           // Para Yatırma
        Withdrawal = 2,        // Para Çekme
        Transfer = 3,          // Havale/Transfer
        Interest = 4,          // Faiz
        BankFee = 5,           // Banka Ücreti
        BankCharge = 6,        // Banka Masrafı
        DirectDebit = 7,       // Otomatik Ödeme
        LoanPayment = 8,       // Kredi Ödemesi
        ForeignExchange = 9,   // Döviz İşlemi
        ChequeDeposit = 10,    // Çek Yatırma
        ChequePayment = 11,    // Çek Ödeme
        EftIncoming = 12,      // Gelen EFT
        EftOutgoing = 13,      // Giden EFT
        SwiftIncoming = 14,    // Gelen Swift
        SwiftOutgoing = 15,    // Giden Swift
        Other = 99             // Diğer
    }

    // İşlem yönü
    public enum TransactionDirection
    {
        In = 1,    // Giriş (para girişi) - Pozitif
        Out = 2    // Çıkış (para çıkışı) - Negatif
    }

    // İşlem durumu
    public enum TransactionStatus
    {
        Pending = 1,    // Beklemede
        Completed = 2,  // Tamamlandı
        Failed = 3,     // Başarısız
        Canceled = 4,   // İptal Edildi
        Reconciled = 5, // Mutabakat Sağlandı
        Disputed = 6    // İtiraz Edildi
    }
} 