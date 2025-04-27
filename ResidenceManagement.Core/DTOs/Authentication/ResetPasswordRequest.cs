using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Şifre sıfırlama isteği için DTO
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Şifre sıfırlama token'ı
        /// </summary>
        [Required(ErrorMessage = "Token zorunludur")]
        public string Token { get; set; }

        /// <summary>
        /// Kullanıcının yeni şifresi
        /// </summary>
        [Required(ErrorMessage = "Yeni şifre zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string NewPassword { get; set; }
    }
} 