using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Kullanıcı giriş isteği için kullanılan DTO
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Kullanıcı adı veya email
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı adı veya email adresi gereklidir.")]
        public string UserName { get; set; }

        /// <summary>
        /// Kullanıcı şifresi
        /// </summary>
        [Required(ErrorMessage = "Şifre gereklidir.")]
        public string Password { get; set; }

        /// <summary>
        /// Oturumu hatırla
        /// </summary>
        public bool RememberMe { get; set; }
    }
} 