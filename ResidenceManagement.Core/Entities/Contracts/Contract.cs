using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Core.Entities.Contracts
{
    /// <summary>
    /// Sözleşme bilgilerini içeren sınıf
    /// </summary>
    public class Contract : BaseEntity
    {
        /// <summary>
        /// Sözleşme numarası
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Sözleşme türü (1: Kira, 2: Günübirlik, 3: Site Sakini, 4: Personel, 5: Tedarikçi)
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// Sözleşme başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Sözleşme açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sözleşme başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Sözleşme bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Sözleşmeyi yapan ilk taraf (Yönetim) ID
        /// </summary>
        public int FirstPartyId { get; set; }

        /// <summary>
        /// Sözleşmeyi yapan ilk taraf (Yönetim) adı
        /// </summary>
        public string FirstPartyName { get; set; }

        /// <summary>
        /// Sözleşmeyi yapan ikinci taraf (Kiracı, Sakin vb.) ID
        /// </summary>
        public int? SecondPartyId { get; set; }

        /// <summary>
        /// Sözleşmeyi yapan ikinci taraf (Kiracı, Sakin vb.) adı
        /// </summary>
        public string SecondPartyName { get; set; }

        /// <summary>
        /// İlgili daire ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Ödeme periyodu (1: Aylık, 2: 3 Aylık, 3: 6 Aylık, 4: Yıllık)
        /// </summary>
        public int PaymentPeriod { get; set; } = 1;

        /// <summary>
        /// Ödeme günü (ayın kaçında)
        /// </summary>
        public int PaymentDayOfMonth { get; set; } = 1;

        /// <summary>
        /// Sözleşme durumu (1: Taslak, 2: Aktif, 3: Sona Erdi, 4: İptal Edildi, 5: Yenilendi)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// Otomatik yenilenecek mi?
        /// </summary>
        public bool AutoRenew { get; set; }

        /// <summary>
        /// Depozito tutarı
        /// </summary>
        public decimal? DepositAmount { get; set; }

        /// <summary>
        /// Depozito para birimi
        /// </summary>
        public string DepositCurrency { get; set; } = "TRY";

        /// <summary>
        /// Depozito alındı mı?
        /// </summary>
        public bool IsDepositReceived { get; set; }

        /// <summary>
        /// Depozito alınma tarihi
        /// </summary>
        public DateTime? DepositReceivedDate { get; set; }

        /// <summary>
        /// Depozito iade edildi mi?
        /// </summary>
        public bool IsDepositReturned { get; set; }

        /// <summary>
        /// Depozito iade tarihi
        /// </summary>
        public DateTime? DepositReturnDate { get; set; }

        /// <summary>
        /// Sözleşme dosyası URL
        /// </summary>
        public string ContractDocumentUrl { get; set; }

        /// <summary>
        /// İmzalandı mı?
        /// </summary>
        public bool IsSigned { get; set; }

        /// <summary>
        /// İmzalanma tarihi
        /// </summary>
        public DateTime? SignedDate { get; set; }

        /// <summary>
        /// Dijital imza kullanıldı mı?
        /// </summary>
        public bool IsDigitallySigned { get; set; }

        /// <summary>
        /// Dijital imza referans kodu
        /// </summary>
        public string DigitalSignatureReference { get; set; }

        /// <summary>
        /// İmzalı sözleşme dosyası URL
        /// </summary>
        public string SignedDocumentUrl { get; set; }

        /// <summary>
        /// İmza istendi mi?
        /// </summary>
        public bool IsSignatureRequested { get; set; }

        /// <summary>
        /// İmza istenme tarihi
        /// </summary>
        public DateTime? SignatureRequestDate { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Ek dosyalar (JSON formatında)
        /// </summary>
        public string Attachments { get; set; }

        // Navigation Properties
        /// <summary>
        /// İlgili daire
        /// </summary>
        public virtual Apartment Apartment { get; set; }

        /// <summary>
        /// Ödeme planı
        /// </summary>
        public virtual ICollection<ContractPaymentSchedule> PaymentSchedule { get; set; }
    }
} 