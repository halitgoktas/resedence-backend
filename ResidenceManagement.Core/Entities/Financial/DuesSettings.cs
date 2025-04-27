using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Financial
{
    /// <summary>
    /// Aidat ayarları sınıfı
    /// </summary>
    public class DuesSettings : BaseEntity
    {
        /// <summary>
        /// Site/Apartman kimliği
        /// </summary>
        public int ResidenceId { get; set; }

        /// <summary>
        /// Aidat adı (Su aidatı, ortak alan aidatı vb.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Aidat açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Baz aidat tutarı (m² başına veya daire başına)
        /// </summary>
        public decimal BaseAmount { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Aidat hesaplama yöntemi (1: Sabit, 2: m² bazlı, 3: Oda sayısı bazlı, 4: Katsayı bazlı)
        /// </summary>
        public int CalculationMethod { get; set; } = 1;

        /// <summary>
        /// Tahakkuk dönemi (1: Aylık, 2: 3 Aylık, 3: 6 Aylık, 4: Yıllık)
        /// </summary>
        public int BillingPeriod { get; set; } = 1;

        /// <summary>
        /// Tahakkuk ayın kaçıncı günü oluşturulacak
        /// </summary>
        public int DueDayOfMonth { get; set; } = 1;

        /// <summary>
        /// Son ödeme günü (ayın kaçı)
        /// </summary>
        public int DueDateDayOfMonth { get; set; } = 15;

        /// <summary>
        /// Gecikme faizi uygulanacak mı?
        /// </summary>
        public bool ApplyLateFee { get; set; } = true;

        /// <summary>
        /// Gecikme faiz oranı (%)
        /// </summary>
        public decimal LateFeePercentage { get; set; } = 1.5m;

        /// <summary>
        /// Erken ödeme indirimi uygulanacak mı?
        /// </summary>
        public bool ApplyEarlyPaymentDiscount { get; set; } = false;

        /// <summary>
        /// Erken ödeme indirim oranı (%)
        /// </summary>
        public decimal EarlyPaymentDiscountPercentage { get; set; } = 0m;

        /// <summary>
        /// Otomatik tahakkuk oluşturulsun mu?
        /// </summary>
        public bool AutoGenerateBills { get; set; } = true;

        /// <summary>
        /// SMS bildirimi gönderilsin mi?
        /// </summary>
        public bool SendSmsNotification { get; set; } = true;

        /// <summary>
        /// E-posta bildirimi gönderilsin mi?
        /// </summary>
        public bool SendEmailNotification { get; set; } = true;

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
} 