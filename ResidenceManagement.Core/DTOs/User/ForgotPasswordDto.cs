using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.User
{
    /// <summary>
    /// Şifremi unuttum işlemi için DTO
    /// </summary>
    public class ForgotPasswordDto
    {
        /// <summary>
        /// Kullanıcı e-posta adresi
        /// </summary>
        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
    }
} 