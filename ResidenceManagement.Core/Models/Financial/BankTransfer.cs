using System;

namespace ResidenceManagement.Core.Models.Financial
{
    // Bankalar arası para transferlerini temsil eden sınıf
    public class BankTransfer
    {
        // Benzersiz kimlik
        public int Id { get; set; }

        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Transfer referans numarası
        public string TransferNumber { get; set; }

        // Kaynak ve hedef hesap bilgileri
        public int SourceBankAccountId { get; set; }
        public virtual BankAccount SourceBankAccount { get; set; }

        public int DestinationBankAccountId { get; set; }
        public virtual BankAccount DestinationBankAccount { get; set; }

        // Alternatif olarak, sistem dışı hesap için
        public string ExternalDestinationBank { get; set; }
        public string ExternalDestinationIBAN { get; set; }
        public string ExternalDestinationAccountName { get; set; }

        // Transfer bilgileri
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } // TRY, USD, EUR, vb.
        public decimal? ExchangeRate { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime? ValueDate { get; set; } // Valör tarihi (işlem gerçekleşme tarihi)

        // Transfer durumu
        public TransferStatus Status { get; set; }
        public string StatusDescription { get; set; }

        // Transfer türü
        public TransferType Type { get; set; }

        // Transfer açıklaması ve referans
        public string Description { get; set; }
        public string ReferenceCode { get; set; }

        // Transfer maliyeti/komisyon
        public decimal? Fee { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalCost { get; set; }

        // İşlemi yapan kişi bilgileri
        public string AuthorizedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }

        // Bağlantılı ödeme veya tahsilat
        public int? RelatedPaymentId { get; set; }

        // Notlar
        public string Notes { get; set; }

        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public BankTransfer()
        {
            CurrencyCode = "TRY";
            Status = TransferStatus.Pending;
            TransferDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }

    // Transfer durumu
    public enum TransferStatus
    {
        Pending = 1,         // Beklemede
        InProgress = 2,      // İşleniyor
        Completed = 3,       // Tamamlandı
        Failed = 4,          // Başarısız
        Canceled = 5,        // İptal Edildi
        Returned = 6         // İade Edildi
    }

    // Transfer türü
    public enum TransferType
    {
        InternalTransfer = 1,    // Aynı bankadaki hesaplar arası transfer
        DomesticTransfer = 2,    // Yurtiçi EFT
        InternationalTransfer = 3, // Swift/Uluslararası transfer
        CashDeposit = 4,         // Nakit yatırma
        CashWithdrawal = 5       // Nakit çekme
    }
} 