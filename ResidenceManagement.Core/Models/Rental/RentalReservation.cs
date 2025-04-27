using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Rental
{
    // Kiralık daireler için rezervasyon sınıfı
    public class RentalReservation
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Rezervasyon numarası (RV-2023-000001)
        public string ReservationNumber { get; set; }
        
        // İlişkili rezidans/site bilgisi
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        
        // İlişkili blok bilgisi
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        
        // İlişkili daire bilgisi
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        
        // Rezervasyon yapan müşteri
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerIdentityNumber { get; set; }
        
        // Rezervasyon tarihleri
        public DateTime ReservationDate { get; set; } // Rezervasyon yapıldığı tarih
        public DateTime CheckInDate { get; set; } // Giriş tarihi
        public DateTime CheckOutDate { get; set; } // Çıkış tarihi
        
        // Misafir bilgileri
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int BabyCount { get; set; }
        public int TotalGuestCount { get => AdultCount + ChildCount + BabyCount; }
        
        // Özel notlar ve istekler
        public string SpecialRequests { get; set; }
        
        // İlişkili fiyatlandırma
        public int RentalPricingId { get; set; }
        public string PricingName { get; set; }
        
        // Fiyat bilgileri
        public decimal BasePrice { get; set; } // KDV hariç temel fiyat
        public decimal DiscountAmount { get; set; } // İndirim tutarı
        public decimal DiscountRate { get; set; } // İndirim oranı (%)
        public decimal ExtraServicesTotal { get; set; } // Ekstra hizmet toplamı
        public decimal SubTotal { get; set; } // Ara toplam (KDV hariç)
        public decimal VATAmount { get; set; } // KDV tutarı
        public decimal TotalAmount { get; set; } // Toplam tutar (KDV dahil)
        public decimal DepositAmount { get; set; } // Depozito tutarı
        
        // Ödeme bilgileri
        public decimal PaidAmount { get; set; } // Ödenen tutar
        public decimal RemainingAmount { get; set; } // Kalan tutar
        public bool IsFullyPaid { get; set; } // Tamamen ödendi mi?
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // Rezervasyon durumu
        public ReservationStatus Status { get; set; }
        
        // Check-in ve Check-out bilgileri
        public bool IsCheckedIn { get; set; }
        public DateTime? ActualCheckInDate { get; set; }
        public string CheckInBy { get; set; }
        
        public bool IsCheckedOut { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
        public string CheckOutBy { get; set; }
        
        // Rezervasyon kaynağı (Web, Telefon, Acente vb.)
        public string ReservationSource { get; set; }
        
        // Acente bilgisi
        public int? AgencyId { get; set; }
        public string AgencyName { get; set; }
        public decimal? AgencyCommissionRate { get; set; }
        public decimal? AgencyCommissionAmount { get; set; }
        
        // Gelir dağılımı
        public decimal? ManagementCommissionRate { get; set; } // Yönetim komisyon oranı
        public decimal? ManagementCommissionAmount { get; set; } // Yönetim komisyonu
        public decimal? OwnerShareAmount { get; set; } // Mülk sahibine aktarılacak tutar
        public bool? IsOwnerShareTransferred { get; set; } // Mülk sahibine aktarıldı mı?
        public DateTime? OwnerShareTransferDate { get; set; } // Transfer tarihi
        
        // KBS (Kimlik Bildirme Sistemi) bilgileri
        public bool IsKbsReported { get; set; }
        public DateTime? KbsReportDate { get; set; }
        public string KbsReportNumber { get; set; }
        
        // İptal bilgileri
        public bool IsCancelled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string CancelledBy { get; set; }
        public string CancellationReason { get; set; }
        public decimal? RefundAmount { get; set; } // İade edilen tutar
        
        // Ek rezervasyon seçenekleri ve hizmetler
        public ICollection<RentalReservationOption> ReservationOptions { get; set; }
        
        // Rezervasyon misafir listesi
        public ICollection<RentalReservationGuest> Guests { get; set; }
        
        // Ödeme kayıtları
        public ICollection<RentalReservationPayment> Payments { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Yapıcı metot
        public RentalReservation()
        {
            ReservationDate = DateTime.Now;
            AdultCount = 1;
            ChildCount = 0;
            BabyCount = 0;
            Status = ReservationStatus.Draft;
            CreatedDate = DateTime.Now;
            IsFullyPaid = false;
            IsCheckedIn = false;
            IsCheckedOut = false;
            IsCancelled = false;
            IsKbsReported = false;
            CurrencyCode = "TRY";
            ReservationOptions = new List<RentalReservationOption>();
            Guests = new List<RentalReservationGuest>();
            Payments = new List<RentalReservationPayment>();
        }
        
        // Rezervasyon iptal etme
        public void CancelReservation(string cancelledBy, string reason, decimal refundAmount)
        {
            if (Status == ReservationStatus.Completed || Status == ReservationStatus.Cancelled)
                throw new InvalidOperationException("Tamamlanmış veya zaten iptal edilmiş rezervasyon iptal edilemez.");
                
            IsCancelled = true;
            CancellationDate = DateTime.Now;
            CancelledBy = cancelledBy;
            CancellationReason = reason;
            RefundAmount = refundAmount;
            Status = ReservationStatus.Cancelled;
            UpdatedDate = DateTime.Now;
            UpdatedBy = cancelledBy;
        }
        
        // Check-in işlemi
        public void CheckIn(string checkedInBy)
        {
            if (Status != ReservationStatus.Confirmed && Status != ReservationStatus.PartiallyPaid)
                throw new InvalidOperationException("Sadece onaylanmış veya kısmen ödenmiş rezervasyonlar için check-in yapılabilir.");
                
            IsCheckedIn = true;
            ActualCheckInDate = DateTime.Now;
            CheckInBy = checkedInBy;
            Status = ReservationStatus.CheckedIn;
            UpdatedDate = DateTime.Now;
            UpdatedBy = checkedInBy;
        }
        
        // Check-out işlemi
        public void CheckOut(string checkedOutBy)
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Sadece check-in yapılmış rezervasyonlar için check-out yapılabilir.");
                
            IsCheckedOut = true;
            ActualCheckOutDate = DateTime.Now;
            CheckOutBy = checkedOutBy;
            Status = ReservationStatus.Completed;
            UpdatedDate = DateTime.Now;
            UpdatedBy = checkedOutBy;
        }
        
        // Ödeme ekle
        public void AddPayment(decimal amount, string paymentMethod, string paidBy)
        {
            var payment = new RentalReservationPayment
            {
                ReservationId = Id,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentMethod,
                PaidBy = paidBy,
                Status = PaymentStatus.Completed,
                CreatedBy = paidBy,
                CreatedDate = DateTime.Now
            };
            
            Payments.Add(payment);
            PaidAmount += amount;
            RemainingAmount = TotalAmount - PaidAmount;
            IsFullyPaid = RemainingAmount <= 0;
            
            // Rezervasyon durumunu güncelle
            if (IsFullyPaid)
                Status = ReservationStatus.FullyPaid;
            else if (PaidAmount > 0)
                Status = ReservationStatus.PartiallyPaid;
                
            UpdatedDate = DateTime.Now;
            UpdatedBy = paidBy;
        }
        
        // Fiyat hesapla
        public void CalculatePricing()
        {
            // Ana fiyat hesaplama (gün sayısı * günlük fiyat mantığı)
            int totalDays = (int)(CheckOutDate - CheckInDate).TotalDays;
            
            // Ara toplam hesaplama
            SubTotal = BasePrice - (BasePrice * (DiscountRate / 100)) - DiscountAmount + ExtraServicesTotal;
            
            // KDV ve toplam tutarı hesaplama (Örnek: %18 KDV)
            decimal vatRate = 18m; // Gerçek uygulamada bu değer yapılandırmalardan alınabilir
            VATAmount = SubTotal * (vatRate / 100);
            TotalAmount = SubTotal + VATAmount;
            
            // Kalan tutarı güncelle
            RemainingAmount = TotalAmount - PaidAmount;
            IsFullyPaid = RemainingAmount <= 0;
        }
        
        // Gelir dağılımını hesapla
        public void CalculateRevenueSharing()
        {
            if (ManagementCommissionRate.HasValue)
            {
                ManagementCommissionAmount = TotalAmount * (ManagementCommissionRate.Value / 100);
                OwnerShareAmount = TotalAmount - ManagementCommissionAmount;
            }
            
            // Acente komisyonu hesaplanıyorsa
            if (AgencyId.HasValue && AgencyCommissionRate.HasValue)
            {
                AgencyCommissionAmount = TotalAmount * (AgencyCommissionRate.Value / 100);
                
                // Yönetim komisyonu da varsa, bunları güncelle
                if (ManagementCommissionAmount.HasValue)
                {
                    OwnerShareAmount = TotalAmount - ManagementCommissionAmount.Value - AgencyCommissionAmount.Value;
                }
            }
        }
    }
    
    // Rezervasyon ek seçenekleri
    public class RentalReservationOption
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili rezervasyon
        public int ReservationId { get; set; }
        
        // İlişkili fiyat seçeneği
        public int? PricingOptionId { get; set; }
        
        // Seçenek adı
        public string Name { get; set; }
        
        // Seçenek açıklaması
        public string Description { get; set; }
        
        // Birim fiyat
        public decimal UnitPrice { get; set; }
        
        // Miktar (kaç adet/gün/vs)
        public int Quantity { get; set; }
        
        // Toplam tutar
        public decimal TotalAmount { get; set; }
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // Ücretlendirme şekli (Sabit, Günlük, Haftalık, vb.)
        public PriceFrequency PriceFrequency { get; set; }
        
        // Durum (aktif/pasif)
        public bool IsActive { get; set; }
    }
    
    // Rezervasyon misafirleri
    public class RentalReservationGuest
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili rezervasyon
        public int ReservationId { get; set; }
        
        // Kişisel bilgiler
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; } // TC/Pasaport numarası
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        // Adres bilgileri
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        
        // Kimlik belgesi bilgileri
        public string IdentityDocumentType { get; set; } // Kimlik/Ehliyet/Pasaport
        public string IdentityDocumentNumber { get; set; }
        public DateTime? IdentityDocumentIssueDate { get; set; }
        public DateTime? IdentityDocumentExpiryDate { get; set; }
        
        // Misafir türü (Yetişkin, Çocuk, Bebek)
        public GuestType Type { get; set; }
        
        // KBS bilgileri
        public bool IsKbsReported { get; set; }
        public DateTime? KbsReportDate { get; set; }
        
        // Oluşturma bilgileri
        public DateTime CreatedDate { get; set; }
    }
    
    // Rezervasyon ödemeleri
    public class RentalReservationPayment
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili rezervasyon
        public int ReservationId { get; set; }
        
        // Ödeme tutarı
        public decimal Amount { get; set; }
        
        // Ödeme tarihi
        public DateTime PaymentDate { get; set; }
        
        // Ödeme yöntemi (Nakit, Kredi Kartı, Havale, vb.)
        public string PaymentMethod { get; set; }
        
        // Ödeme yapan kişi
        public string PaidBy { get; set; }
        
        // Ödeme referans numarası
        public string ReferenceNumber { get; set; }
        
        // Makbuz/fatura numarası
        public string ReceiptNumber { get; set; }
        
        // Notlar
        public string Notes { get; set; }
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // Ödeme durumu
        public PaymentStatus Status { get; set; }
        
        // Ödeme tipi (Ön Ödeme, Bakiye Ödemesi, Depozito, vb.)
        public PaymentType Type { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    
    // Rezervasyon durumu
    public enum ReservationStatus
    {
        // Taslak (henüz kaydedilmiş ama tamamlanmamış)
        Draft = 0,
        
        // Beklemede (onay bekliyor)
        Pending = 1,
        
        // Onaylandı (rezervasyon kabul edildi)
        Confirmed = 2,
        
        // Kısmen Ödendi
        PartiallyPaid = 3,
        
        // Tamamen Ödendi
        FullyPaid = 4,
        
        // Check-in Yapıldı
        CheckedIn = 5,
        
        // Tamamlandı (check-out yapıldı)
        Completed = 6,
        
        // İptal Edildi
        Cancelled = 7,
        
        // No-Show (Misafir gelmedi)
        NoShow = 8
    }
    
    // Ödeme durumu
    public enum PaymentStatus
    {
        // Beklemede
        Pending = 0,
        
        // Tamamlandı
        Completed = 1,
        
        // Başarısız
        Failed = 2,
        
        // İade Edildi
        Refunded = 3,
        
        // Kısmen İade Edildi
        PartiallyRefunded = 4
    }
    
    // Ödeme tipi
    public enum PaymentType
    {
        // Ön Ödeme
        Prepayment = 0,
        
        // Bakiye Ödemesi
        Balance = 1,
        
        // Depozito
        Deposit = 2,
        
        // Ekstra Ödeme (ek hizmetler vs.)
        Extra = 3,
        
        // İade
        Refund = 4
    }
    
    // Misafir türü
    public enum GuestType
    {
        // Yetişkin
        Adult = 0,
        
        // Çocuk (2-12 yaş)
        Child = 1,
        
        // Bebek (0-2 yaş)
        Baby = 2
    }
} 