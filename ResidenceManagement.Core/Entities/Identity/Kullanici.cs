using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Entities.Identity
{
    // Kullanici sınıfı, sistem kullanıcılarını temsil eder
    public class Kullanici : BaseEntity, ITenant
    {
        // Kullanıcı adı (login için)
        public string KullaniciAdi { get; set; }
        
        // Username - İngilizce kullanıcı adı (API uyumluluğu için)
        public string Username { get => KullaniciAdi; set => KullaniciAdi = value; }
        
        // E-posta adresi
        public string Email { get; set; }
        
        // Telefon numarası
        public string Telefon { get; set; }
        
        // Ad
        public string Ad { get; set; }
        
        // Soyad
        public string Soyad { get; set; }
        
        // Tam ad (Ad + Soyad)
        public string TamAd => $"{Ad} {Soyad}";

        // FirstName - İngilizce ad (API uyumluluğu için)
        public string FirstName { get => Ad; set => Ad = value; }

        // LastName - İngilizce soyad (API uyumluluğu için)
        public string LastName { get => Soyad; set => Soyad = value; }
        
        // Şifre (hash'lenmiş olarak saklanır)
        public string Sifre { get; set; }
        
        // Şifre salt değeri
        public string SifreSalt { get; set; }
        
        // Profil resmi URL'i
        public string ProfilResimUrl { get; set; }
        
        // Son giriş tarihi
        public DateTime? SonGirisTarihi { get; set; }
        
        // Şifre sıfırlama token'ı
        public string ResetPasswordToken { get; set; }
        
        // Token son geçerlilik tarihi
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        
        // E-posta doğrulama durumu
        public bool EmailDogrulandiMi { get; set; }
        
        // PhoneNumber - İngilizce telefon (API uyumluluğu için)
        public string PhoneNumber { get => Telefon; set => Telefon = value; }

        // Navigation Properties
        // Kullanıcının bağlı olduğu firma
        public virtual Firma Firma { get; set; }
        
        // Kullanıcının bağlı olduğu şube
        public virtual Sube Sube { get; set; }
        
        // Kullanıcının rolleri
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        public Kullanici()
        {
            UserRoles = new HashSet<UserRole>();
            EmailDogrulandiMi = false;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
} 