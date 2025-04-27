using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Yeni kullanıcı oluşturmak için kullanılan DTO
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter arasında olmalıdır.")]
        public string KullaniciAdi { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Telefon { get; set; }
        
        /// <summary>
        /// Ad
        /// </summary>
        [Required(ErrorMessage = "Ad gereklidir.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Ad { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        [Required(ErrorMessage = "Soyad gereklidir.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Soyad { get; set; }
        
        /// <summary>
        /// Şifre
        /// </summary>
        [Required(ErrorMessage = "Şifre gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        
        /// <summary>
        /// Şifre onayı
        /// </summary>
        [Required(ErrorMessage = "Şifre onayı gereklidir.")]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string SifreOnay { get; set; }
        
        /// <summary>
        /// Profil resmi URL'i
        /// </summary>
        public string ProfilResimUrl { get; set; }
        
        /// <summary>
        /// Firma ID
        /// </summary>
        [Required(ErrorMessage = "Firma bilgisi gereklidir.")]
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        [Required(ErrorMessage = "Şube bilgisi gereklidir.")]
        public int SubeId { get; set; }
    }
} 