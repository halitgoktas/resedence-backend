using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    // KBS Misafir Bilgileri sınıfı - KBS bildirimlerinde kullanılacak misafir bilgileri
    public class KbsGuestInfo : BaseEntity
    {
        // KBS Bildirim ID'si
        public int KbsNotificationId { get; set; }
        
        // Misafir türü (Yetişkin, Çocuk, vb.)
        public GuestType GuestType { get; set; }
        
        // Kimlik türü (TC Kimlik, Pasaport, vb.)
        public IdentityType IdentityType { get; set; }
        
        // Kimlik numarası (TC Kimlik No, Pasaport No, vb.)
        [StringLength(50)]
        public string IdentityNumber { get; set; }
        
        // Kimliğin verildiği ülke
        [StringLength(100)]
        public string IssuingCountry { get; set; }
        
        // Kimlik dokümanının son kullanma tarihi
        public DateTime? DocumentExpiryDate { get; set; }
        
        // Ad
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        // Soyad
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        // Baba adı
        [StringLength(100)]
        public string FatherName { get; set; }
        
        // Anne adı
        [StringLength(100)]
        public string MotherName { get; set; }
        
        // Doğum tarihi
        public DateTime? DateOfBirth { get; set; }
        
        // Doğum yeri (şehir)
        [StringLength(100)]
        public string PlaceOfBirth { get; set; }
        
        // Cinsiyet
        public Gender? Gender { get; set; }
        
        // Uyruk (Vatandaşlık)
        [Required]
        [StringLength(100)]
        public string Nationality { get; set; }
        
        // Medeni durum
        public MaritalStatus? MaritalStatus { get; set; }
        
        // Meslek
        [StringLength(100)]
        public string Profession { get; set; }
        
        // E-posta
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        
        // Telefon numarası
        [StringLength(30)]
        public string PhoneNumber { get; set; }
        
        // İkamet adresi
        [StringLength(500)]
        public string Address { get; set; }
        
        // Adres şehri
        [StringLength(100)]
        public string City { get; set; }
        
        // Adres ülkesi
        [StringLength(100)]
        public string Country { get; set; }
        
        // Adres posta kodu
        [StringLength(20)]
        public string PostalCode { get; set; }
        
        // Araç plakası
        [StringLength(20)]
        public string VehiclePlate { get; set; }
        
        // Bu konaklamaya özel notlar
        [StringLength(500)]
        public string Notes { get; set; }
        
        // KBS'de daha önce kayıtlı mı?
        public bool IsRegisteredInKbs { get; set; }
        
        // Sonraki ziyaretler için kayıtlı misafir mi? (Hızlı bildirim için)
        public bool IsRegisteredGuest { get; set; }
        
        // Oda başkanı mı? (Grup konaklamalarında bir kişi oda başkanı olarak belirtilir)
        public bool IsGroupLeader { get; set; }
        
        // Misafir fotoğrafı URL
        [StringLength(500)]
        public string PhotoUrl { get; set; }
        
        // Kimlik belgesinin ön yüzünün fotoğrafı URL
        [StringLength(500)]
        public string IdentityFrontPhotoUrl { get; set; }
        
        // Kimlik belgesinin arka yüzünün fotoğrafı URL
        [StringLength(500)]
        public string IdentityBackPhotoUrl { get; set; }
        
        // KBS işlem referans numarası
        [StringLength(50)]
        public string KbsReferenceNumber { get; set; }
        
        // KBS doğrulama durumu
        public bool IsVerifiedByKbs { get; set; }
        
        // KBS doğrulama tarihi
        public DateTime? KbsVerificationDate { get; set; }
        
        // KBS hata mesajı (eğer varsa)
        [StringLength(500)]
        public string KbsErrorMessage { get; set; }
        
        // KBS bildirimi ile ilişki
        public virtual KbsNotification KbsNotification { get; set; }
        
        // Yapıcı metod
        public KbsGuestInfo()
        {
            GuestType = GuestType.Adult;
            IdentityType = IdentityType.NationalId;
            Nationality = "Türkiye";
            Country = "Türkiye";
            IsRegisteredInKbs = false;
            IsRegisteredGuest = false;
            IsGroupLeader = false;
            IsVerifiedByKbs = false;
        }
        
        // Tam adı döndüren metod
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
        
        // Misafirin KBS için geçerli olup olmadığını kontrol eden metod
        public (bool IsValid, string ErrorMessage) ValidateForKbs()
        {
            // Çocuk ise kontroller daha az sıkı
            if (GuestType == GuestType.Child)
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                    return (false, "Çocuk için ad ve soyad bilgileri zorunludur");
                    
                return (true, string.Empty);
            }
            
            // Yetişkin misafirler için kontroller
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                return (false, "Ad ve soyad bilgileri zorunludur");
                
            if (string.IsNullOrEmpty(IdentityNumber))
                return (false, "Kimlik numarası zorunludur");
                
            if (string.IsNullOrEmpty(Nationality))
                return (false, "Uyruk bilgisi zorunludur");
                
            if (IdentityType == IdentityType.Passport)
            {
                if (string.IsNullOrEmpty(IssuingCountry))
                    return (false, "Pasaport için ülke bilgisi zorunludur");
                    
                if (!DocumentExpiryDate.HasValue)
                    return (false, "Pasaport için son kullanma tarihi zorunludur");
                    
                if (DocumentExpiryDate.Value < DateTime.Now)
                    return (false, "Pasaport süresi dolmuş");
            }
            
            return (true, string.Empty);
        }
        
        // Yetişkin mi kontrolü
        public bool IsAdult()
        {
            if (!DateOfBirth.HasValue)
                return true; // Doğum tarihi belirtilmemişse yetişkin kabul et
                
            var age = CalculateAge(DateOfBirth.Value);
            return age >= 18;
        }
        
        // Yaş hesaplama
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            
            if (birthDate.Date > today.AddYears(-age))
                age--;
                
            return age;
        }
    }
    
    // Misafir Türleri
    public enum GuestType
    {
        Adult = 0,        // Yetişkin
        Child = 1,        // Çocuk (18 yaş altı)
        Baby = 2,         // Bebek (2 yaş altı)
        Employee = 3,     // Çalışan
        Relative = 4,     // Akraba
        Other = 5         // Diğer
    }
    
    // Kimlik Türleri
    public enum IdentityType
    {
        NationalId = 0,   // TC Kimlik
        Passport = 1,     // Pasaport
        DrivingLicense = 2, // Ehliyet
        ResidencePermit = 3, // İkamet İzni
        ForeignId = 4,    // Yabancı Kimlik
        Other = 5         // Diğer
    }
    
    // Cinsiyet
    public enum Gender
    {
        Male = 0,         // Erkek
        Female = 1,       // Kadın
        Other = 2         // Diğer
    }
    
    // Medeni Durum
    public enum MaritalStatus
    {
        Single = 0,       // Bekar
        Married = 1,      // Evli
        Divorced = 2,     // Boşanmış
        Widowed = 3,      // Dul
        Other = 4         // Diğer
    }
} 