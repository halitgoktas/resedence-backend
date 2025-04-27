using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Şifre sıfırlama işlemi için kullanılan DTO
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Şifre sıfırlama token'ı
        /// </summary>
        [Required(ErrorMessage = "Token gereklidir.")]
        public string Token { get; set; }
        
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