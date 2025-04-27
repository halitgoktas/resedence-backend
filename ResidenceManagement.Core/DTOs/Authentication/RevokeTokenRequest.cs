using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Token iptal etme isteği için kullanılan DTO
    /// </summary>
    public class RevokeTokenRequest
    {
        /// <summary>
        /// İptal edilecek token değeri
        /// </summary>
        public string Token { get; set; }
    }
} 