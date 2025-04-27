using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Şifre değiştirmek için kullanılan DTO
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Mevcut şifre
        /// </summary>
        [Required(ErrorMessage = "Mevcut şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Yeni şifre
        /// </summary>
        [Required(ErrorMessage = "Yeni şifre gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Yeni şifre onayı
        /// </summary>
        [Required(ErrorMessage = "Yeni şifre onayı gereklidir.")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
} 