using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Kullanıcı bilgilerini API üzerinden paylaşmak için kullanılan DTO
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string KullaniciAdi { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string Telefon { get; set; }
        
        /// <summary>
        /// Ad
        /// </summary>
        public string Ad { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        public string Soyad { get; set; }
        
        /// <summary>
        /// Tam ad (Ad + Soyad)
        /// </summary>
        public string TamAd { get; set; }
        
        /// <summary>
        /// Profil resmi URL'i
        /// </summary>
        public string ProfilResimUrl { get; set; }
        
        /// <summary>
        /// Son giriş tarihi
        /// </summary>
        public DateTime? SonGirisTarihi { get; set; }
        
        /// <summary>
        /// Hesap aktif mi
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Kullanıcının rolleri
        /// </summary>
        public List<string> Roller { get; set; }
    }
} 