using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Activity
{
    /// <summary>
    /// Ortak aktivite alanları için yapılan rezervasyonları tanımlayan sınıf
    /// </summary>
    public class ActivityReservation : BaseEntity
    {
        /// <summary>
        /// Rezervasyon numarası (sistem tarafından otomatik oluşturulur)
        /// </summary>
        public string ReservationNumber { get; set; }

        /// <summary>
        /// Rezerve edilen aktivite alanının ID'si
        /// </summary>
        public Guid ActivityAreaId { get; set; }

        /// <summary>
        /// Rezerve edilen aktivite alanı
        /// </summary>
        public virtual ActivityArea ActivityArea { get; set; }

        /// <summary>
        /// Rezervasyonu yapan kullanıcının ID'si
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Rezervasyonu yapan kullanıcının adı
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rezervasyona dahil olan kişi sayısı
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Rezervasyon amacı veya etkinlik tipi (ör. "Doğum Günü Partisi", "Toplantı")
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Rezervasyon başlangıç tarihi ve saati
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Rezervasyon bitiş tarihi ve saati
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Rezervasyon süresi (saat)
        /// </summary>
        public double Duration => (EndDateTime - StartDateTime).TotalHours;

        /// <summary>
        /// Rezervasyon durumu (Beklemede, Onaylandı, İptal Edildi, Tamamlandı)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Rezervasyonun onaylanma tarihi
        /// </summary>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// Rezervasyonu onaylayan personelin ID'si
        /// </summary>
        public Guid? ConfirmedById { get; set; }

        /// <summary>
        /// Rezervasyonu onaylayan personelin adı
        /// </summary>
        public string ConfirmedByName { get; set; }

        /// <summary>
        /// Ek talepler veya özel istekler
        /// </summary>
        public string SpecialRequests { get; set; }

        /// <summary>
        /// Ödenecek ücret
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Ödenecek depozito miktarı
        /// </summary>
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// Ödeme durumu (Ödenmedi, Kısmen Ödendi, Tamamen Ödendi)
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Ödeme referans numarası
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// Ödeme yöntemi (Nakit, Kredi Kartı, Banka Havalesi, vb.)
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal AmountPaid { get; set; }

        /// <summary>
        /// Kalan tutar
        /// </summary>
        public decimal AmountDue => Fee - AmountPaid;

        /// <summary>
        /// İptal edilme tarihi
        /// </summary>
        public DateTime? CancellationDate { get; set; }

        /// <summary>
        /// İptal nedeni
        /// </summary>
        public string CancellationReason { get; set; }

        /// <summary>
        /// İptal eden kişinin ID'si
        /// </summary>
        public Guid? CancelledById { get; set; }

        /// <summary>
        /// İptal eden kişinin adı
        /// </summary>
        public string CancelledByName { get; set; }

        /// <summary>
        /// İade edilen tutar
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// Rezervasyon sonrası değerlendirme puanı (1-5)
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Rezervasyon sonrası yapılan yorum
        /// </summary>
        public string Feedback { get; set; }

        /// <summary>
        /// Rezervasyon sırasında oluşan hasarlar
        /// </summary>
        public string DamageNotes { get; set; }

        /// <summary>
        /// Hasar bedeli
        /// </summary>
        public decimal? DamageCost { get; set; }

        /// <summary>
        /// Son hatırlatma gönderim tarihi
        /// </summary>
        public DateTime? LastReminderSent { get; set; }

        /// <summary>
        /// Rezervasyon ile ilgili dahili notlar
        /// </summary>
        public string InternalNotes { get; set; }

        /// <summary>
        /// Check-in yapılma durumu
        /// </summary>
        public bool CheckedIn { get; set; }

        /// <summary>
        /// Check-in tarihi ve saati
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// Check-out yapılma durumu
        /// </summary>
        public bool CheckedOut { get; set; }

        /// <summary>
        /// Check-out tarihi ve saati
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        public ActivityReservation()
        {
            ReservationNumber = GenerateReservationNumber();
            Status = "Beklemede";
            PaymentStatus = "Ödenmedi";
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Benzersiz rezervasyon numarası oluşturur
        /// </summary>
        private string GenerateReservationNumber()
        {
            // Rezervasyon numarası formatı: RES + YıllAy + 4 haneli rastgele sayı
            string prefix = "RES";
            string datePart = DateTime.Now.ToString("yyMM");
            string randomPart = new Random().Next(1000, 9999).ToString();
            
            return $"{prefix}{datePart}{randomPart}";
        }

        /// <summary>
        /// Rezervasyon durumunu günceller
        /// </summary>
        /// <param name="newStatus">Yeni durum</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void UpdateStatus(string newStatus, Guid userId, string userName)
        {
            Status = newStatus;
            UpdatedDate = DateTime.Now;
            
            switch (newStatus)
            {
                case "Onaylandı":
                    ConfirmationDate = DateTime.Now;
                    ConfirmedById = userId;
                    ConfirmedByName = userName;
                    break;
                case "İptal Edildi":
                    CancellationDate = DateTime.Now;
                    CancelledById = userId;
                    CancelledByName = userName;
                    break;
                case "Tamamlandı":
                    CheckedOut = true;
                    CheckOutTime = DateTime.Now;
                    break;
            }
        }

        /// <summary>
        /// Check-in işlemini yapar
        /// </summary>
        public void PerformCheckIn()
        {
            if (Status != "Onaylandı")
                throw new InvalidOperationException("Sadece onaylanmış rezervasyonlar için check-in yapılabilir.");
                
            CheckedIn = true;
            CheckInTime = DateTime.Now;
            Status = "Devam Ediyor";
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Check-out işlemini yapar
        /// </summary>
        /// <param name="damageNotes">Hasar notları</param>
        /// <param name="damageCost">Hasar bedeli</param>
        public void PerformCheckOut(string damageNotes = null, decimal? damageCost = null)
        {
            if (!CheckedIn)
                throw new InvalidOperationException("Check-in yapılmadan check-out yapılamaz.");
                
            CheckedOut = true;
            CheckOutTime = DateTime.Now;
            Status = "Tamamlandı";
            UpdatedDate = DateTime.Now;
            
            if (!string.IsNullOrEmpty(damageNotes))
            {
                DamageNotes = damageNotes;
                DamageCost = damageCost;
            }
        }

        /// <summary>
        /// Rezervasyonu iptal eder
        /// </summary>
        /// <param name="reason">İptal nedeni</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        /// <param name="refundAmount">İade edilecek tutar</param>
        public void CancelReservation(string reason, Guid userId, string userName, decimal? refundAmount = null)
        {
            if (Status == "Tamamlandı" || CheckedIn)
                throw new InvalidOperationException("Başlamış veya tamamlanmış rezervasyonlar iptal edilemez.");
                
            Status = "İptal Edildi";
            CancellationDate = DateTime.Now;
            CancellationReason = reason;
            CancelledById = userId;
            CancelledByName = userName;
            RefundAmount = refundAmount;
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Ödeme kaydeder
        /// </summary>
        /// <param name="amount">Ödeme tutarı</param>
        /// <param name="paymentMethod">Ödeme yöntemi</param>
        /// <param name="paymentReference">Ödeme referansı</param>
        public void RecordPayment(decimal amount, string paymentMethod, string paymentReference)
        {
            AmountPaid += amount;
            PaymentMethod = paymentMethod;
            PaymentReference = paymentReference;
            PaymentDate = DateTime.Now;
            
            if (AmountPaid >= Fee)
            {
                PaymentStatus = "Tamamen Ödendi";
            }
            else if (AmountPaid > 0)
            {
                PaymentStatus = "Kısmen Ödendi";
            }
            
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Müşteri değerlendirmesi ekler
        /// </summary>
        /// <param name="rating">Değerlendirme puanı (1-5)</param>
        /// <param name="feedback">Değerlendirme yorumu</param>
        public void AddFeedback(int rating, string feedback)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Değerlendirme puanı 1-5 arasında olmalıdır.");
                
            if (Status != "Tamamlandı")
                throw new InvalidOperationException("Sadece tamamlanmış rezervasyonlar değerlendirilebilir.");
                
            Rating = rating;
            Feedback = feedback;
            UpdatedDate = DateTime.Now;
        }
    }
} 