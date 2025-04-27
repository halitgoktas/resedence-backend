using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Models.KBS;

namespace ResidenceManagement.Core.Models.KBS
{
    /// <summary>
    /// KBS (Kimlik Bildirme Sistemi) bildirimleri için kullanılan sınıf
    /// </summary>
    public class KbsNotification : BaseEntity
    {
        /// <summary>
        /// Benzersiz bildirim numarası
        /// </summary>
        [Required]
        [StringLength(50)]
        public string NotificationNumber { get; set; }
        
        /// <summary>
        /// İlişkili rezervasyon ID (varsa)
        /// </summary>
        public int? ReservationId { get; set; }
        
        /// <summary>
        /// İlişkili daire ID
        /// </summary>
        public int ApartmentId { get; set; }
        
        /// <summary>
        /// İlişkili daire numarası
        /// </summary>
        [Required]
        [StringLength(20)]
        public string ApartmentNumber { get; set; }
        
        /// <summary>
        /// İlişkili blok ID
        /// </summary>
        public int? BlockId { get; set; }
        
        /// <summary>
        /// İlişkili blok adı
        /// </summary>
        [StringLength(50)]
        public string BlockName { get; set; }
        
        /// <summary>
        /// İlişkili rezidans ID
        /// </summary>
        public int ResidenceId { get; set; }
        
        /// <summary>
        /// İlişkili rezidans adı
        /// </summary>
        [StringLength(100)]
        public string ResidenceName { get; set; }
        
        /// <summary>
        /// Bildirim türü
        /// </summary>
        public KbsNotificationType NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim durumu
        /// </summary>
        public KbsNotifyStatus Status { get; set; }
        
        /// <summary>
        /// Giriş tarihi
        /// </summary>
        [Required]
        public DateTime CheckInDate { get; set; }
        
        /// <summary>
        /// Planlanan çıkış tarihi
        /// </summary>
        public DateTime PlannedCheckOutDate { get; set; }
        
        /// <summary>
        /// Gerçek çıkış tarihi
        /// </summary>
        public DateTime? ActualCheckOutDate { get; set; }
        
        /// <summary>
        /// Toplam misafir sayısı
        /// </summary>
        public int GuestCount => Guests?.Count ?? 0;
        
        /// <summary>
        /// Bildirim oluşturma tarihi
        /// </summary>
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// Bildirim gönderilme tarihi (KBS'ye)
        /// </summary>
        public DateTime? SubmissionDate { get; set; }
        
        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }
        
        /// <summary>
        /// Bildirimi oluşturan kullanıcı ID
        /// </summary>
        public int CreatedByUserId { get; set; }
        
        /// <summary>
        /// Bildirimi oluşturan kullanıcı adı
        /// </summary>
        [StringLength(100)]
        public string CreatedByUserName { get; set; }
        
        /// <summary>
        /// Son güncelleyen kullanıcı ID
        /// </summary>
        public int? LastUpdatedByUserId { get; set; }
        
        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(100)]
        public string LastUpdatedByUserName { get; set; }
        
        /// <summary>
        /// Bildirim iptal edildi mi?
        /// </summary>
        public bool IsCancelled { get; set; }
        
        /// <summary>
        /// İptal edilme tarihi
        /// </summary>
        public DateTime? CancellationDate { get; set; }
        
        /// <summary>
        /// İptal nedeni
        /// </summary>
        [StringLength(500)]
        public string CancellationReason { get; set; }
        
        /// <summary>
        /// İptal eden kullanıcı ID
        /// </summary>
        public int? CancelledByUserId { get; set; }
        
        /// <summary>
        /// İptal eden kullanıcı adı
        /// </summary>
        [StringLength(100)]
        public string CancelledByUserName { get; set; }
        
        /// <summary>
        /// KBS'den alınan bildirim ID
        /// </summary>
        [StringLength(50)]
        public string KbsReferenceId { get; set; }
        
        /// <summary>
        /// Son KBS yanıt kodu
        /// </summary>
        [StringLength(20)]
        public string LastResponseCode { get; set; }
        
        /// <summary>
        /// Son KBS yanıt mesajı
        /// </summary>
        [StringLength(500)]
        public string LastResponseMessage { get; set; }
        
        /// <summary>
        /// Konaklama amacı
        /// </summary>
        public StayPurpose StayPurpose { get; set; }
        
        /// <summary>
        /// Konaklama ücretli mi?
        /// </summary>
        public bool IsPaid { get; set; }
        
        /// <summary>
        /// Bildirim notları
        /// </summary>
        [StringLength(500)]
        public string Notes { get; set; }
        
        /// <summary>
        /// KBS hata açıklaması (eğer varsa)
        /// </summary>
        [StringLength(500)]
        public string ErrorDescription { get; set; }
        
        /// <summary>
        /// Bildirim URL'leri (JSON formatında - KBS sistem tarafından verilen raporlama URL'leri)
        /// </summary>
        public string NotificationUrls { get; set; }
        
        /// <summary>
        /// Misafir listesi (ilişkili KbsGuestInfo nesneleri)
        /// </summary>
        public virtual ICollection<KbsGuestInfo> Guests { get; set; }
        
        /// <summary>
        /// Bildirim logları
        /// </summary>
        public virtual ICollection<KbsNotificationLog> Logs { get; set; }
        
        /// <summary>
        /// KBS bildirim işlem geçmişi
        /// </summary>
        public virtual ICollection<KbsNotificationHistory> History { get; set; }
        
        /// <summary>
        /// Yapılandırıcı metod
        /// </summary>
        public KbsNotification()
        {
            Guests = new List<KbsGuestInfo>();
            Logs = new List<KbsNotificationLog>();
            History = new List<KbsNotificationHistory>();
            CreationDate = DateTime.Now;
            Status = KbsNotifyStatus.Draft;
            NotificationType = KbsNotificationType.CheckIn;
            IsCancelled = false;
            StayPurpose = StayPurpose.Tourism;
            NotificationNumber = GenerateNotificationNumber();
        }
        
        /// <summary>
        /// Bildirim numarası oluşturma
        /// </summary>
        private string GenerateNotificationNumber()
        {
            string prefix = "KBS";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string random = new Random().Next(1000, 9999).ToString();
            return $"{prefix}-{timestamp}-{random}";
        }
        
        /// <summary>
        /// Durumu güncelleme
        /// </summary>
        /// <param name="newStatus">Yeni durum</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void UpdateStatus(KbsNotifyStatus newStatus, string description = null, int? userId = null, string userName = null)
        {
            Status = newStatus;
            LastUpdateDate = DateTime.Now;
            LastUpdatedByUserId = userId;
            LastUpdatedByUserName = userName;
            
            // Status history kaydı oluştur
            var historyEntry = new KbsNotificationHistory
            {
                KbsNotificationId = Id,
                OldStatus = Status,
                NewStatus = newStatus,
                ChangeDate = DateTime.Now,
                UserId = userId,
                UserName = userName,
                Description = description ?? $"Durum {Status} olarak değiştirildi."
            };
            
            History.Add(historyEntry);
        }
        
        /// <summary>
        /// Bildirimi iptal etme
        /// </summary>
        /// <param name="reason">İptal nedeni</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void CancelNotification(string reason, int userId, string userName)
        {
            IsCancelled = true;
            CancellationDate = DateTime.Now;
            CancellationReason = reason;
            CancelledByUserId = userId;
            CancelledByUserName = userName;
            UpdateStatus(KbsNotifyStatus.Cancelled, $"Bildirim iptal edildi. Neden: {reason}", userId, userName);
        }
        
        /// <summary>
        /// Misafir ekleme
        /// </summary>
        /// <param name="guest">Yeni misafir</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void AddGuest(KbsGuestInfo guest, int userId, string userName)
        {
            Guests.Add(guest);
            
            // Log ekle
            var log = new KbsNotificationLog
            {
                KbsNotificationId = Id,
                ActionType = KbsLogActionType.AddGuest,
                Description = $"Misafir eklendi: {guest.FirstName} {guest.LastName}",
                UserId = userId,
                UserName = userName,
                IsSuccess = true,
                ResultStatus = "Başarılı"
            };
            
            Logs.Add(log);
            LastUpdateDate = DateTime.Now;
            LastUpdatedByUserId = userId;
            LastUpdatedByUserName = userName;
        }
        
        /// <summary>
        /// Bildirimin geçerli olup olmadığını kontrol et
        /// </summary>
        /// <returns>Geçerlilik ve hata mesajı</returns>
        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (Guests == null || !Guests.Any())
                return (false, "En az bir misafir bilgisi eklenmeli");
                
            if (CheckInDate > PlannedCheckOutDate)
                return (false, "Giriş tarihi, planlanan çıkış tarihinden sonra olamaz");
                
            if (string.IsNullOrEmpty(ApartmentNumber))
                return (false, "Daire numarası belirtilmeli");
                
            // Temel misafir bilgilerini kontrol et
            foreach (var guest in Guests)
            {
                if (string.IsNullOrEmpty(guest.FirstName) || string.IsNullOrEmpty(guest.LastName))
                    return (false, "Tüm misafirlerin ad ve soyadı belirtilmelidir");
                    
                if (string.IsNullOrEmpty(guest.IdentityNumber) && guest.GuestType != GuestType.Child)
                    return (false, "Tüm yetişkin misafirlerin kimlik numarası belirtilmelidir");
            }
            
            return (true, string.Empty);
        }
        
        /// <summary>
        /// Çıkış işlemi
        /// </summary>
        /// <param name="checkOutDate">Çıkış tarihi</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void CheckOut(DateTime checkOutDate, int userId, string userName)
        {
            ActualCheckOutDate = checkOutDate;
            UpdateStatus(KbsNotifyStatus.CheckedOut, $"Çıkış işlemi yapıldı: {checkOutDate:dd.MM.yyyy HH:mm}", userId, userName);
        }
        
        /// <summary>
        /// Konaklama süresini uzatma
        /// </summary>
        /// <param name="newCheckOutDate">Yeni çıkış tarihi</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        public void ExtendStay(DateTime newCheckOutDate, int userId, string userName)
        {
            if (newCheckOutDate <= PlannedCheckOutDate)
                throw new InvalidOperationException("Yeni çıkış tarihi mevcut çıkış tarihinden sonra olmalıdır");
                
            PlannedCheckOutDate = newCheckOutDate;
            UpdateStatus(KbsNotifyStatus.Extended, $"Konaklama süresi {newCheckOutDate:dd.MM.yyyy} tarihine uzatıldı", userId, userName);
        }
        
        /// <summary>
        /// Bildirim URL'lerini liste olarak al
        /// </summary>
        /// <returns>Bildirim URL'leri listesi</returns>
        public List<string> GetNotificationUrlsList()
        {
            if (string.IsNullOrEmpty(NotificationUrls))
                return new List<string>();
                
            try
            {
                return JsonSerializer.Deserialize<List<string>>(NotificationUrls);
            }
            catch
            {
                return new List<string>();
            }
        }
        
        /// <summary>
        /// Bildirim URL'lerini liste olarak ayarla
        /// </summary>
        /// <param name="urls">Bildirim URL'leri listesi</param>
        public void SetNotificationUrlsList(List<string> urls)
        {
            NotificationUrls = JsonSerializer.Serialize(urls);
        }
    }
    
    /// <summary>
    /// Bildirim türü
    /// </summary>
    public enum KbsNotificationType
    {
        CheckIn = 0,    // Giriş bildirimi
        CheckOut = 1,   // Çıkış bildirimi
        Extension = 2,  // Süre uzatımı
        Correction = 3  // Düzeltme
    }
    
    /// <summary>
    /// KBS Bildirim Durumu
    /// </summary>
    public enum KbsNotifyStatus
    {
        /// <summary>
        /// Taslak
        /// </summary>
        Draft = 0,
        
        /// <summary>
        /// Gönderilmeye Hazır
        /// </summary>
        Ready = 1,
        
        /// <summary>
        /// Gönderildi
        /// </summary>
        Submitted = 2,
        
        /// <summary>
        /// Kabul Edildi
        /// </summary>
        Accepted = 3,
        
        /// <summary>
        /// Reddedildi
        /// </summary>
        Rejected = 4,
        
        /// <summary>
        /// Hata
        /// </summary>
        Error = 5,
        
        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 6,
        
        /// <summary>
        /// Çıkış Yapıldı
        /// </summary>
        CheckedOut = 7,
        
        /// <summary>
        /// Uzatıldı
        /// </summary>
        Extended = 8,
        
        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 9
    }
    
    /// <summary>
    /// Konaklama amacı
    /// </summary>
    public enum StayPurpose
    {
        Tourism = 0,      // Turizm
        Business = 1,     // İş
        Health = 2,        // Sağlık
        Visit = 3,         // Ziyaret
        Education = 4,     // Eğitim
        Conference = 5,    // Konferans
        Sport = 6,          // Spor
        Culture = 7,        // Kültürel
        Official = 8,       // Resmi
        Other = 9          // Diğer
    }
} 