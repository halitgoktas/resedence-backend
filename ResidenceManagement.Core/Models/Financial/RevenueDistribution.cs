using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Financial
{
    /// <summary>
    /// Kira ve diğer gelir dağılımını yönetmek için kullanılan sınıf
    /// </summary>
    public class RevenueDistribution : BaseEntity
    {
        /// <summary>
        /// Gelir dağıtım numarası
        /// </summary>
        public string DistributionNumber { get; set; }
        
        /// <summary>
        /// Gelirin ait olduğu apartman ID'si
        /// </summary>
        public Guid ApartmentId { get; set; }
        
        /// <summary>
        /// Apartman numarası
        /// </summary>
        public string ApartmentNumber { get; set; }
        
        /// <summary>
        /// Daire sahibinin ID'si
        /// </summary>
        public Guid OwnerId { get; set; }
        
        /// <summary>
        /// Daire sahibinin adı
        /// </summary>
        public string OwnerName { get; set; }
        
        /// <summary>
        /// Rezervasyon ID'si (kira durumunda)
        /// </summary>
        public Guid? ReservationId { get; set; }
        
        /// <summary>
        /// Rezervasyon numarası
        /// </summary>
        public string ReservationNumber { get; set; }
        
        /// <summary>
        /// Gelir türü (Kira, Aktivite Alanı, Servis Geliri, vb.)
        /// </summary>
        public string RevenueType { get; set; }
        
        /// <summary>
        /// Gelir açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Toplam gelir tutarı
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Gelirin para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Dövizse TL karşılığı
        /// </summary>
        public decimal? AmountInTRY { get; set; }
        
        /// <summary>
        /// Döviz kuru (TL'ye çevirmek için)
        /// </summary>
        public decimal? ExchangeRate { get; set; }
        
        /// <summary>
        /// Yönetim komisyon oranı (%)
        /// </summary>
        public decimal CommissionRate { get; set; }
        
        /// <summary>
        /// Yönetim komisyon tutarı
        /// </summary>
        public decimal CommissionAmount => Math.Round(TotalAmount * (CommissionRate / 100), 2);
        
        /// <summary>
        /// Stopaj oranı (%)
        /// </summary>
        public decimal WithholdingTaxRate { get; set; }
        
        /// <summary>
        /// Stopaj tutarı
        /// </summary>
        public decimal WithholdingTaxAmount => Math.Round(TotalAmount * (WithholdingTaxRate / 100), 2);
        
        /// <summary>
        /// KDV oranı (%)
        /// </summary>
        public decimal VATRate { get; set; }
        
        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal VATAmount => Math.Round(TotalAmount * (VATRate / 100), 2);
        
        /// <summary>
        /// Diğer kesintiler (elektrik, su, gaz, vb.)
        /// </summary>
        public decimal OtherDeductions { get; set; }
        
        /// <summary>
        /// Diğer kesintilerin açıklaması
        /// </summary>
        public string DeductionsDescription { get; set; }
        
        /// <summary>
        /// Toplam kesinti tutarı (komisyon + stopaj + KDV + diğer)
        /// </summary>
        public decimal TotalDeductions => CommissionAmount + WithholdingTaxAmount + VATAmount + OtherDeductions;
        
        /// <summary>
        /// Daire sahibine ödenecek net tutar
        /// </summary>
        public decimal NetAmountToOwner => TotalAmount - TotalDeductions;
        
        /// <summary>
        /// Gelir tarihi
        /// </summary>
        public DateTime RevenueDate { get; set; }
        
        /// <summary>
        /// Ödeme durumu (Beklemede, Ödendi, Kısmen Ödendi)
        /// </summary>
        public string PaymentStatus { get; set; }
        
        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public string PaymentMethod { get; set; }
        
        /// <summary>
        /// Ödeme referans numarası
        /// </summary>
        public string PaymentReference { get; set; }
        
        /// <summary>
        /// Mal sahibine ödenen tutar
        /// </summary>
        public decimal AmountPaidToOwner { get; set; }
        
        /// <summary>
        /// Kalan ödeme tutarı
        /// </summary>
        public decimal RemainingAmount => NetAmountToOwner - AmountPaidToOwner;
        
        /// <summary>
        /// Dağıtım durumu (Hesaplandı, Onaylandı, Ödendi)
        /// </summary>
        public string DistributionStatus { get; set; }
        
        /// <summary>
        /// Yönetim onay durumu
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// Onaylayan kişi ID'si
        /// </summary>
        public Guid? ApprovedById { get; set; }
        
        /// <summary>
        /// Onaylayan kişi adı
        /// </summary>
        public string ApprovedByName { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        /// <summary>
        /// Mal sahibi tarafından görüntülenme durumu
        /// </summary>
        public bool IsViewedByOwner { get; set; }
        
        /// <summary>
        /// Görüntülenme tarihi
        /// </summary>
        public DateTime? ViewedDate { get; set; }
        
        /// <summary>
        /// Dönem (ay-yıl)
        /// </summary>
        public string Period { get; set; }
        
        /// <summary>
        /// Dahili notlar
        /// </summary>
        public string InternalNotes { get; set; }

        public RevenueDistribution()
        {
            DistributionNumber = GenerateDistributionNumber();
            RevenueDate = DateTime.Now;
            DistributionStatus = "Hesaplandı";
            PaymentStatus = "Beklemede";
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        
        /// <summary>
        /// Benzersiz dağıtım numarası oluşturur
        /// </summary>
        private string GenerateDistributionNumber()
        {
            string prefix = "REV";
            string datePart = DateTime.Now.ToString("yyMM");
            string randomPart = new Random().Next(1000, 9999).ToString();
            
            return $"{prefix}{datePart}{randomPart}";
        }
        
        /// <summary>
        /// Dağıtım durumunu günceller
        /// </summary>
        /// <param name="newStatus">Yeni durum</param>
        public void UpdateDistributionStatus(string newStatus)
        {
            DistributionStatus = newStatus;
            UpdatedDate = DateTime.Now;
            
            if (newStatus == "Onaylandı")
            {
                IsApproved = true;
                ApprovalDate = DateTime.Now;
            }
        }
        
        /// <summary>
        /// Mal sahibine ödeme kaydeder
        /// </summary>
        /// <param name="amount">Ödeme tutarı</param>
        /// <param name="paymentMethod">Ödeme yöntemi</param>
        /// <param name="paymentReference">Ödeme referansı</param>
        public void RecordPaymentToOwner(decimal amount, string paymentMethod, string paymentReference)
        {
            AmountPaidToOwner += amount;
            PaymentMethod = paymentMethod;
            PaymentReference = paymentReference;
            PaymentDate = DateTime.Now;
            
            if (AmountPaidToOwner >= NetAmountToOwner)
            {
                PaymentStatus = "Ödendi";
                DistributionStatus = "Ödendi";
            }
            else if (AmountPaidToOwner > 0)
            {
                PaymentStatus = "Kısmen Ödendi";
            }
            
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Dağıtımı onaylar
        /// </summary>
        /// <param name="userId">Onaylayan kullanıcı ID'si</param>
        /// <param name="userName">Onaylayan kullanıcı adı</param>
        public void ApproveDistribution(Guid userId, string userName)
        {
            IsApproved = true;
            ApprovedById = userId;
            ApprovedByName = userName;
            ApprovalDate = DateTime.Now;
            DistributionStatus = "Onaylandı";
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Mal sahibi tarafından görüntülendiğini işaretler
        /// </summary>
        public void MarkAsViewedByOwner()
        {
            IsViewedByOwner = true;
            ViewedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Döviz cinsinden tutarı TL'ye çevirir
        /// </summary>
        /// <param name="exchangeRate">Döviz kuru</param>
        public void ConvertToTRY(decimal exchangeRate)
        {
            if (Currency != "TRY" && exchangeRate > 0)
            {
                ExchangeRate = exchangeRate;
                AmountInTRY = Math.Round(TotalAmount * exchangeRate, 2);
            }
        }
    }
} 