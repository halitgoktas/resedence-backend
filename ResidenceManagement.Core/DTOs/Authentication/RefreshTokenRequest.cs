using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Token yenileme isteği için kullanılan DTO
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Access Token
        /// </summary>
        [Required(ErrorMessage = "Access token gereklidir.")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required(ErrorMessage = "Refresh token gereklidir.")]
        public string RefreshToken { get; set; }
    }
} 