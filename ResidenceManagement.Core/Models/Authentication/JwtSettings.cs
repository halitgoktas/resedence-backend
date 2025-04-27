using System;

namespace ResidenceManagement.Core.Models.Authentication
{
    /// <summary>
    /// JWT kimlik doğrulama ayarlarını içeren model
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// JWT token'ı için kullanılacak gizli anahtar
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// JWT token'ı yayınlayan kurum/sistem
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// JWT token'ın hedef kitlesi
        /// </summary>
        public string Audience { get; set; }
        
        /// <summary>
        /// Access token geçerlilik süresi (dakika)
        /// </summary>
        public int AccessTokenExpirationMinutes { get; set; }
        
        /// <summary>
        /// Refresh token geçerlilik süresi (gün)
        /// </summary>
        public int RefreshTokenExpirationDays { get; set; }
    }
} 