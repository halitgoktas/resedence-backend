using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Şifremi unuttum isteği için DTO
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// Kullanıcının email adresi
        /// </summary>
        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }
    }
} 