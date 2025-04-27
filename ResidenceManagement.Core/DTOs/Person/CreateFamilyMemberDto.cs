using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Person
{
    /// <summary>
    /// Aile üyesi oluşturma veri transfer nesnesi
    /// </summary>
    public class CreateFamilyMemberDto
    {
        /// <summary>
        /// İlişkili birincil kullanıcı ID'si
        /// </summary>
        [Required(ErrorMessage = "İlişkili kullanıcı bilgisi gereklidir.")]
        public int RelatedUserId { get; set; }
        
        /// <summary>
        /// İlişkili apartman/daire ID'si
        /// </summary>
        [Required(ErrorMessage = "İlişkili daire bilgisi gereklidir.")]
        public int ApartmentId { get; set; }
        
        /// <summary>
        /// İlişki türü (Eş, Çocuk, Ebeveyn, vb.)
        /// </summary>
        [Required(ErrorMessage = "İlişki türü bilgisi gereklidir.")]
        [MaxLength(50, ErrorMessage = "İlişki türü en fazla 50 karakter olabilir.")]
        public string RelationType { get; set; }
        
        /// <summary>
        /// Ad
        /// </summary>
        [Required(ErrorMessage = "Ad bilgisi gereklidir.")]
        [MaxLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir.")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        [Required(ErrorMessage = "Soyad bilgisi gereklidir.")]
        [MaxLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir.")]
        public string LastName { get; set; }
        
        /// <summary>
        /// TC Kimlik Numarası / Pasaport Numarası
        /// </summary>
        [Required(ErrorMessage = "Kimlik numarası bilgisi gereklidir.")]
        [MaxLength(20, ErrorMessage = "Kimlik numarası en fazla 20 karakter olabilir.")]
        public string IdentityNumber { get; set; }
        
        /// <summary>
        /// Doğum tarihi
        /// </summary>
        [Required(ErrorMessage = "Doğum tarihi bilgisi gereklidir.")]
        public DateTime BirthDate { get; set; }
        
        /// <summary>
        /// Doğum yeri
        /// </summary>
        [MaxLength(100, ErrorMessage = "Doğum yeri en fazla 100 karakter olabilir.")]
        public string BirthPlace { get; set; }
        
        /// <summary>
        /// Baba adı
        /// </summary>
        [MaxLength(100, ErrorMessage = "Baba adı en fazla 100 karakter olabilir.")]
        public string FatherName { get; set; }
        
        /// <summary>
        /// Anne adı
        /// </summary>
        [MaxLength(100, ErrorMessage = "Anne adı en fazla 100 karakter olabilir.")]
        public string MotherName { get; set; }
        
        /// <summary>
        /// Cinsiyet (E/K)
        /// </summary>
        [MaxLength(10, ErrorMessage = "Cinsiyet en fazla 10 karakter olabilir.")]
        public string Gender { get; set; }
        
        /// <summary>
        /// Uyruk
        /// </summary>
        [MaxLength(50, ErrorMessage = "Uyruk en fazla 50 karakter olabilir.")]
        public string Nationality { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [MaxLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir.")]
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [MaxLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir.")]
        public string Email { get; set; }
        
        /// <summary>
        /// Adres bilgisi
        /// </summary>
        [MaxLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        public string Address { get; set; }
        
        /// <summary>
        /// Kimlik belge türü
        /// </summary>
        [MaxLength(50, ErrorMessage = "Belge türü en fazla 50 karakter olabilir.")]
        public string DocumentType { get; set; }
        
        /// <summary>
        /// Kimlik belgesi numarası
        /// </summary>
        [MaxLength(50, ErrorMessage = "Belge numarası en fazla 50 karakter olabilir.")]
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin verildiği ülke
        /// </summary>
        [MaxLength(50, ErrorMessage = "Belgenin verildiği ülke en fazla 50 karakter olabilir.")]
        public string DocumentIssuingCountry { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin verildiği tarih
        /// </summary>
        public DateTime? DocumentIssueDate { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin geçerlilik tarihi
        /// </summary>
        public DateTime? DocumentExpiryDate { get; set; }
        
        /// <summary>
        /// Giriş (taşınma) tarihi
        /// </summary>
        [Required(ErrorMessage = "Giriş tarihi bilgisi gereklidir.")]
        public DateTime EntryDate { get; set; }
        
        /// <summary>
        /// Çıkış (taşınma) tarihi
        /// </summary>
        public DateTime? ExitDate { get; set; }
        
        /// <summary>
        /// Firma ID (Multi-tenancy için)
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID (Multi-tenancy için)
        /// </summary>
        public int SubeId { get; set; }
    }
} 