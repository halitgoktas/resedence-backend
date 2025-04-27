using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Integration
{
    /// <summary>
    /// KBS bildirimi ile ilgili misafir bilgilerini içeren sınıf
    /// </summary>
    public class KbsGuestInfo : BaseEntity
    {
        /// <summary>
        /// İlişkili KBS bildirimi ID'si
        /// </summary>
        public int KbsNotificationId { get; set; }
        
        // İlişkili rezervasyon ID'si (varsa)
        public int? ReservationId { get; set; }
        
        /// <summary>
        /// Misafir tipi (Ana misafir, ek misafir, aile üyesi vb.)
        /// </summary>
        public GuestType GuestType { get; set; }
        
        /// <summary>
        /// Misafirin T.C. Kimlik Numarası veya Pasaport Numarası
        /// </summary>
        public string IdentityNumber { get; set; }
        
        /// <summary>
        /// Kimlik belgesi türü (T.C. Kimlik, Pasaport, vb.)
        /// </summary>
        public IdentityType IdentityType { get; set; }
        
        // Misafir adı
        public string FirstName { get; set; }
        
        // Misafir soyadı
        public string LastName { get; set; }
        
        // Baba Adı
        public string FatherName { get; set; }
        
        // Anne Adı
        public string MotherName { get; set; }
        
        /// <summary>
        /// Misafirin doğum tarihi
        /// </summary>
        public DateTime BirthDate { get; set; }
        
        // Doğum Yeri
        public string BirthPlace { get; set; }
        
        /// <summary>
        /// Misafirin cinsiyeti
        /// </summary>
        public string Gender { get; set; }
        
        /// <summary>
        /// Misafirin uyruğu
        /// </summary>
        public string Nationality { get; set; }
        
        // Uyruk Ülke Kodu
        public string CountryCode { get; set; }
        
        // Pasaport Verildiği Ülke
        public string PassportIssuingCountry { get; set; }
        
        // Pasaport Verildiği Tarih
        public DateTime? PassportIssueDate { get; set; }
        
        // Pasaport Bitiş Tarihi
        public DateTime? PassportExpiryDate { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin seri numarası
        /// </summary>
        public string DocumentSerialNumber { get; set; }
        
        // Belge geçerlilik tarihi
        public DateTime? DocumentValidUntil { get; set; }
        
        /// <summary>
        /// Pasaport için vize numarası (varsa)
        /// </summary>
        public string VisaNumber { get; set; }
        
        /// <summary>
        /// Misafirin telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Misafirin e-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Misafirin adresi
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Kimlik belgesi fotoğrafının dosya yolu
        /// </summary>
        public string IdentityDocumentPath { get; set; }
        
        /// <summary>
        /// Giriş (check-in) tarihi
        /// </summary>
        public DateTime CheckInDate { get; set; }
        
        /// <summary>
        /// Çıkış (check-out) tarihi
        /// </summary>
        public DateTime? CheckOutDate { get; set; }
        
        /// <summary>
        /// Misafirin geldiği ülke
        /// </summary>
        public string CountryOfOrigin { get; set; }
        
        /// <summary>
        /// Misafirin geldiği şehir
        /// </summary>
        public string CityOfOrigin { get; set; }
        
        /// <summary>
        /// Misafirin ziyaret amacı
        /// </summary>
        public string PurposeOfVisit { get; set; }
        
        // KBS'ye bildirim durumu
        public bool IsSubmittedToKbs { get; set; }
        
        // KBS'ye bildirim tarihi
        public DateTime? KbsSubmissionDate { get; set; }
        
        // Bildirim hatası (varsa)
        public string SubmissionError { get; set; }
        
        /// <summary>
        /// İlişkili KBS bildirimi
        /// </summary>
        public virtual KbsNotification KbsNotification { get; set; }
        
        /// <summary>
        /// Bildirimin geçerlilik durumu
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// Doğrulama hatası (varsa)
        /// </summary>
        public string ValidationError { get; set; }
        
        public KbsGuestInfo()
        {
            IsSubmittedToKbs = false;
            IsValid = true;
            CheckInDate = DateTime.Now;
        }
        
        // Misafirin tam adını döndüren metot
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
        
        /// <summary>
        /// Misafir bilgilerinin doğruluğunu kontrol eder
        /// </summary>
        /// <returns>Bilgilerin geçerli olup olmadığı</returns>
        public bool ValidateGuestInfo()
        {
            IsValid = true;
            ValidationError = string.Empty;

            // T.C. Kimlik Numarası kontrolü (basit kontrol)
            if (IdentityType == IdentityType.TCKimlik && (string.IsNullOrEmpty(IdentityNumber) || IdentityNumber.Length != 11))
            {
                IsValid = false;
                ValidationError = "T.C. Kimlik Numarası 11 haneli olmalıdır.";
                return false;
            }

            // Pasaport numarası kontrolü
            if (IdentityType == IdentityType.Pasaport && string.IsNullOrEmpty(IdentityNumber))
            {
                IsValid = false;
                ValidationError = "Pasaport numarası boş olamaz.";
                return false;
            }

            // Ad ve soyad kontrolü
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                IsValid = false;
                ValidationError = "Ad ve soyad alanları boş olamaz.";
                return false;
            }

            // Doğum tarihi kontrolü
            if (BirthDate > DateTime.Now.AddYears(-18))
            {
                IsValid = false;
                ValidationError = "18 yaşından küçük misafirler için veli/vasi bilgileri gereklidir.";
                return false;
            }

            // Check-in ve check-out tarihi kontrolü
            if (CheckOutDate.HasValue && CheckOutDate.Value < CheckInDate)
            {
                IsValid = false;
                ValidationError = "Çıkış tarihi, giriş tarihinden önce olamaz.";
                return false;
            }

            return true;
        }
    }
} 