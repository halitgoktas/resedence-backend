using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Kullanıcı entity sınıfı
    /// </summary>
    public class Kullanici : BaseEntity, ResidenceManagement.Core.Common.ITenant
    {
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string KullaniciAdi { get; set; }
        
        /// <summary>
        /// Şifre (hash'lenmiş)
        /// </summary>
        public string Sifre { get; set; }
        
        /// <summary>
        /// Şifre için tuz (salt)
        /// </summary>
        public string SifreSalt { get; set; }
        
        /// <summary>
        /// Ad
        /// </summary>
        public string Ad { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        public string Soyad { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string Telefon { get; set; }
        
        /// <summary>
        /// Password hash değeri
        /// </summary>
        public byte[] PasswordHash { get; set; }
        
        /// <summary>
        /// Password salt değeri
        /// </summary>
        public byte[] PasswordSalt { get; set; }
        
        // Username - İngilizce kullanıcı adı (API uyumluluğu için)
        public string Username { get => KullaniciAdi; set => KullaniciAdi = value; }
        
        // Tam ad (Ad + Soyad)
        public string TamAd => $"{Ad} {Soyad}";

        // FirstName - İngilizce ad (API uyumluluğu için)
        public string FirstName { get => Ad; set => Ad = value; }

        // LastName - İngilizce soyad (API uyumluluğu için)
        public string LastName { get => Soyad; set => Soyad = value; }
        
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