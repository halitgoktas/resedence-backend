using System;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Kimlik doğrulama işlemi sonucunda dönen token bilgilerini içeren DTO
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// JWT Access Token
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// Refresh Token - Yenileme için kullanılır
        /// </summary>
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// Token tipi (Bearer)
        /// </summary>
        public string TokenType { get; set; } = "Bearer";
        
        /// <summary>
        /// Access token son geçerlilik tarihi
        /// </summary>
        public DateTime AccessTokenExpiration { get; set; }
        
        /// <summary>
        /// Refresh token son geçerlilik tarihi
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }
        
        /// <summary>
        /// Token geçerlilik süresi (saniye)
        /// </summary>
        public int ExpiresIn { get; set; }
        
        /// <summary>
        /// Kullanıcı ID'si
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Kullanıcı email adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Firma ID'si
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID'si
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Token (AccessToken ile aynı, uyumluluk için)
        /// </summary>
        public string Token 
        { 
            get => AccessToken; 
            set => AccessToken = value; 
        }
        
        /// <summary>
        /// Expires (AccessTokenExpiration ile aynı, uyumluluk için)
        /// </summary>
        public DateTime Expires 
        { 
            get => AccessTokenExpiration; 
            set => AccessTokenExpiration = value; 
        }
    }
} 