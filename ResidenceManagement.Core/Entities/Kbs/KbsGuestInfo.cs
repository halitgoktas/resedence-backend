/*
* Copyright (c) 2024 ResidenceManagement
* Tüm hakları saklıdır.
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Entities.Common;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS (Kimlik Bildirim Sistemi) misafir bilgilerini temsil eden sınıf.
    /// Bu sınıf, konaklama tesislerindeki misafirlerin KBS'ye bildirilmesi gereken bilgilerini içerir.
    /// </summary>
    public class KbsGuestInfo : BaseEntity
    {
        /// <summary>
        /// İlgili KBS bildiriminin ID'si
        /// </summary>
        public int KbsNotificationId { get; set; }

        /// <summary>
        /// Misafirin tipi (Normal, VIP, vs.)
        /// </summary>
        public GuestType GuestType { get; set; }

        /// <summary>
        /// Kimlik tipi (TC Kimlik, Pasaport, vs.)
        /// </summary>
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// Kimlik/Pasaport numarası
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Adı
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Soyadı
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Baba adı
        /// </summary>
        [MaxLength(50)]
        public string FatherName { get; set; }

        /// <summary>
        /// Anne adı
        /// </summary>
        [MaxLength(50)]
        public string MotherName { get; set; }

        /// <summary>
        /// Doğum tarihi
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Doğum yeri
        /// </summary>
        [MaxLength(50)]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Cinsiyet
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Medeni hali
        /// </summary>
        public MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// Uyruk
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; }

        /// <summary>
        /// Telefon numarası
        /// </summary>
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// E-posta adresi
        /// </summary>
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        [MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// İlgili KBS bildirimi
        /// </summary>
        [ForeignKey("KbsNotificationId")]
        public virtual KbsNotification KbsNotification { get; set; }

        /// <summary>
        /// KBS için gerekli alanların dolu olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Validasyon sonucu</returns>
        public bool ValidateForKbs()
        {
            return !string.IsNullOrEmpty(IdentityNumber) &&
                   !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   BirthDate != DateTime.MinValue &&
                   !string.IsNullOrEmpty(Nationality);
        }

        /// <summary>
        /// Misafirin 18 yaşından büyük olup olmadığını kontrol eder
        /// </summary>
        /// <returns>18 yaşından büyükse true, değilse false</returns>
        public bool IsAdult()
        {
            var age = DateTime.Today.Year - BirthDate.Year;
            if (BirthDate > DateTime.Today.AddYears(-age)) age--;
            return age >= 18;
        }
    }

    /// <summary>
    /// Misafir tiplerini temsil eden enum
    /// </summary>
    public enum GuestType
    {
        Normal = 0,
        VIP = 1,
        Business = 2,
        Student = 3
    }

    /// <summary>
    /// Kimlik tiplerini temsil eden enum
    /// </summary>
    public enum IdentityType
    {
        TCKimlik = 0,
        Pasaport = 1,
        EhliyetBelgesi = 2,
        DigerKimlikBelgesi = 3
    }

    /// <summary>
    /// Cinsiyet tiplerini temsil eden enum
    /// </summary>
    public enum Gender
    {
        Belirtilmemis = 0,
        Erkek = 1,
        Kadin = 2,
        Diger = 3
    }

    /// <summary>
    /// Medeni hal tiplerini temsil eden enum
    /// </summary>
    public enum MaritalStatus
    {
        Belirtilmemis = 0,
        Bekar = 1,
        Evli = 2,
        Bosanmis = 3,
        Dul = 4
    }
} 