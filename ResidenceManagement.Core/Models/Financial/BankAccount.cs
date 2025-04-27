using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Banka hesaplarını temsil eden sınıf
    public class BankAccount
    {
        // Benzersiz kimlik
        public int Id { get; set; }

        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Hesap adı ve açıklaması
        public string AccountName { get; set; }
        public string Description { get; set; }

        // Banka bilgileri
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }

        // Hesap numarası ve IBAN
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }

        // Hesap türü ve para birimi
        public AccountType Type { get; set; }
        public string CurrencyCode { get; set; } // TRY, USD, EUR, vb.

        // Bakiye bilgileri
        public decimal CurrentBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? BlockedAmount { get; set; }
        public DateTime LastBalanceUpdateDate { get; set; }

        // Online bankacılık bilgileri
        public bool HasOnlineBanking { get; set; }
        public string OnlineBankingUsername { get; set; }
        public bool IsIntegrated { get; set; } // API entegrasyonu var mı

        // Hesap durumu ve aktiflik
        public bool IsActive { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        // Yetkilendirme bilgileri
        public string AuthorizedPersons { get; set; }
        public string AccountManagers { get; set; }

        // Varsayılan ayarlar
        public bool IsDefault { get; set; }
        public bool IsVisibleInReports { get; set; }

        // Muhasebe bilgileri
        public string AccountingCode { get; set; }
        public int? AccountingAccountId { get; set; }

        // İlişkili işlemler
        public virtual ICollection<BankTransaction> Transactions { get; set; }

        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public BankAccount()
        {
            CurrencyCode = "TRY";
            IsActive = true;
            Status = AccountStatus.Active;
            OpeningDate = DateTime.Now;
            LastBalanceUpdateDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            IsVisibleInReports = true;
            Transactions = new List<BankTransaction>();
        }
    }

    // Hesap türü
    public enum AccountType
    {
        Checking = 1,       // Vadesiz Mevduat
        Savings = 2,        // Vadeli Mevduat
        CreditCard = 3,     // Kredi Kartı
        Loan = 4,           // Kredi
        Investment = 5,     // Yatırım
        Business = 6,       // Ticari
        Cash = 7,           // Nakit/Kasa
        Virtual = 8,        // Sanal Hesap
        Other = 99          // Diğer
    }

    // Hesap durumu
    public enum AccountStatus
    {
        Active = 1,             // Aktif
        Inactive = 2,           // Pasif
        Suspended = 3,          // Askıya Alınmış
        Closed = 4,             // Kapatılmış
        PendingActivation = 5,  // Aktivasyon Bekliyor
        Blocked = 6             // Bloke
    }
} 