using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Integration
{
    /// <summary>
    /// KBS bildirim modeli, Kimlik Bildirim Sistemi'ne yapılan bildirimleri temsil eder.
    /// </summary>
    public class KbsNotification : BaseEntity
    {
        /// <summary>
        /// Bildirim yapılan kullanıcının ID'si
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// İlgili kişinin ID'si (misafir, aile üyesi vb.)
        /// </summary>
        public int RelatedPersonId { get; set; }

        /// <summary>
        /// İlgili kişinin tipi (User, FamilyMember)
        /// </summary>
        public string RelatedPersonType { get; set; }

        /// <summary>
        /// Bildirim tipi (CheckIn, CheckOut)
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// Bildirimin yapıldığı tarih
        /// </summary>
        public DateTime NotificationDate { get; set; }

        /// <summary>
        /// Bildirimin durumu (Pending, Success, Error)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Hata durumunda hata mesajı
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// KBS sisteminde oluşturulan kayıt numarası
        /// </summary>
        public string SystemRecordNumber { get; set; }

        /// <summary>
        /// KBS sisteminden dönen cevap detayları (JSON formatında)
        /// </summary>
        public string ResponseDetails { get; set; }

        /// <summary>
        /// Bildirimi işleyen kullanıcının ID'si
        /// </summary>
        public int? ProcessedByUserId { get; set; }

        /// <summary>
        /// Bildirimin otomatik olarak mı yoksa manuel mi oluşturulduğu
        /// </summary>
        public bool IsAutomatic { get; set; }

        /// <summary>
        /// İlgili rezervasyon ID'si, eğer bir rezervasyonla ilişkiliyse
        /// </summary>
        public int? ReservationId { get; set; }

        /// <summary>
        /// İlgili daire ID'si
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Bildirim ile ilişkili misafir bilgileri koleksiyonu
        /// </summary>
        public virtual ICollection<KbsGuestInfo> GuestInfos { get; set; }

        public KbsNotification()
        {
            GuestInfos = new HashSet<KbsGuestInfo>();
            Status = "Pending";
            NotificationDate = DateTime.Now;
            IsAutomatic = false;
        }

        /// <summary>
        /// Bildirimin başarılı olarak işaretlenmesi
        /// </summary>
        /// <param name="systemRecordNumber">KBS sisteminde oluşturulan kayıt numarası</param>
        /// <param name="responseDetails">KBS sisteminden dönen cevap detayları</param>
        /// <param name="processedByUserId">İşlemi yapan kullanıcı ID'si</param>
        public void MarkAsSuccess(string systemRecordNumber, string responseDetails, int processedByUserId)
        {
            Status = "Success";
            SystemRecordNumber = systemRecordNumber;
            ResponseDetails = responseDetails;
            ProcessedByUserId = processedByUserId;
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Bildirimin hatalı olarak işaretlenmesi
        /// </summary>
        /// <param name="errorMessage">Hata mesajı</param>
        /// <param name="responseDetails">KBS sisteminden dönen cevap detayları</param>
        /// <param name="processedByUserId">İşlemi yapan kullanıcı ID'si</param>
        public void MarkAsError(string errorMessage, string responseDetails, int processedByUserId)
        {
            Status = "Error";
            ErrorMessage = errorMessage;
            ResponseDetails = responseDetails;
            ProcessedByUserId = processedByUserId;
            UpdatedDate = DateTime.Now;
        }
    }
    
    // KBS bildirilen kişi bilgileri
    public class KbsNotificationPerson
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili bildirim
        public int NotificationId { get; set; }
        
        // İlişkili misafir (varsa)
        public int? GuestId { get; set; }
        
        // Kişi tipi (Misafir, Refakatçi, Aile Üyesi, vb.)
        public KbsPersonType PersonType { get; set; }
        
        // Vatandaşlık tipi (TC Vatandaşı, Yabancı, vb.)
        public KbsCitizenshipType CitizenshipType { get; set; }
        
        // Kişisel bilgiler
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string IdentityNumber { get; set; } // TC Kimlik / Pasaport No
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        
        // İletişim bilgileri
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        // Adres bilgileri
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        // Belge bilgileri
        public string DocumentType { get; set; } // Pasaport, Kimlik, Ehliyet
        public string DocumentNumber { get; set; }
        public string DocumentIssuingCountry { get; set; }
        public DateTime? DocumentIssueDate { get; set; }
        public DateTime? DocumentExpiryDate { get; set; }
        
        // Giriş-çıkış bilgileri
        public DateTime? EntryDate { get; set; } // Ülkeye giriş tarihi
        public string EntryPoint { get; set; } // Ülkeye giriş noktası (sınır kapısı)
        
        // KBS'ye bildirim durumu
        public bool IsReported { get; set; }
        public DateTime? ReportDate { get; set; }
        
        // Ek bilgiler (KBS için gerekebilecek diğer bilgiler)
        public string AdditionalInfo { get; set; }
        
        // Oluşturma bilgileri
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
    
    // KBS bildirim logları
    public class IntegrationKbsNotificationLog
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili bildirim
        public int NotificationId { get; set; }
        
        // Log tarihi
        public DateTime LogDate { get; set; }
        
        // İşlem
        public string Action { get; set; }
        
        // Durum
        public string Status { get; set; }
        
        // Kullanıcı
        public string UserName { get; set; }
        
        // Detaylar
        public string Details { get; set; }
        
        // API yanıtı
        public string ApiResponse { get; set; }
        
        public IntegrationKbsNotificationLog()
        {
            LogDate = DateTime.Now;
        }
    }
    
    // KBS bildirim tipi
    public enum KbsNotificationType
    {
        // Konaklama bildirimi - Misafirin tesiste konaklamaya başladığında KBS sistemine gönderilen bildirim türüdür. Konaklama başlangıcında zorunlu olarak yapılması gerekir.
        Accommodation = 0,
        
        // Günübirlik kullanım bildirimi - Misafirin tesiste konaklamadan sadece günübirlik olarak tesis hizmetlerinden (havuz, spa, restoran vb.) faydalandığında KBS sistemine gönderilen bildirim türüdür.
        DailyUse = 1,
        
        // Ayrılış bildirimi - Misafirin tesisten ayrılması durumunda KBS sistemine gönderilen bildirim türüdür. Konaklama sonlandığında yapılması gerekir.
        Departure = 2,
        
        // İptal bildirimi - Önceden yapılmış olan bildirimin iptal edildiğini belirten bildirim türüdür. Hatalı yapılan bildirimlerin düzeltilmesi için kullanılır.
        Cancellation = 3
    }
    
    // KBS bildirim durumu
    public enum KbsNotificationStatus
    {
        // Taslak
        Draft = 0,
        
        // Gönderildi (henüz onay yok)
        Submitted = 1,
        
        // Onaylandı
        Confirmed = 2,
        
        // Reddedildi
        Rejected = 3,
        
        // Başarısız oldu
        Failed = 4,
        
        // İptal edildi
        Cancelled = 5
    }
    
    // KBS kişi tipi
    public enum KbsPersonType
    {
        // Ana misafir
        PrimaryGuest = 0,
        
        // Refakatçi
        Companion = 1,
        
        // Aile üyesi
        FamilyMember = 2,
        
        // Grup üyesi
        GroupMember = 3
    }
    
    // KBS vatandaşlık tipi
    public enum KbsCitizenshipType
    {
        // TC Vatandaşı
        TurkishCitizen = 0,
        
        // Yabancı
        Foreigner = 1,
        
        // TC Vatandaşı (MERNİS Doğrulaması)
        TurkishCitizenVerified = 2,
        
        // Mavi Kartlı Vatandaş
        BlueCardHolder = 3
    }
} 