using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Kullanıcı bilgilerini güncellemek için kullanılan DTO
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// E-posta adresi
        /// </summary>
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
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Ad { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Soyad { get; set; }
        
        /// <summary>
        /// Profil resmi URL'i
        /// </summary>
        public string ProfilResimUrl { get; set; }
        
        /// <summary>
        /// Kullanıcı aktif mi
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int? SubeId { get; set; }
    }
} 