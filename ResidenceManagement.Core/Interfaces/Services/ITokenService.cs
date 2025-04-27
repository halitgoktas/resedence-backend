using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// JWT token oluşturma ve yönetimi için servis arayüzü
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Kullanıcı için JWT token oluşturur
        /// </summary>
        /// <param name="kullanici">Kullanıcı bilgileri</param>
        /// <param name="ipAddress">İstek yapılan IP adresi</param>
        /// <returns>Oluşturulan token response</returns>
        Task<TokenResponse> GenerateTokensAsync(Kullanici kullanici, string ipAddress);

        /// <summary>
        /// Access token oluşturur
        /// </summary>
        /// <param name="claims">Token içeriğindeki claim'ler</param>
        /// <param name="expires">Token geçerlilik süresi</param>
        /// <returns>JWT token string</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expires);

        /// <summary>
        /// Refresh token oluşturur
        /// </summary>
        /// <param name="kullaniciId">Kullanıcı kimliği</param>
        /// <param name="firmaId">Firma kimliği</param>
        /// <param name="subeId">Şube kimliği</param>
        /// <param name="ipAddress">İstek yapılan IP adresi</param>
        /// <returns>Oluşturulan refresh token</returns>
        Task<RefreshToken> GenerateRefreshTokenAsync(int kullaniciId, int firmaId, int subeId, string ipAddress);

        /// <summary>
        /// Refresh token'ı geçersiz kılar
        /// </summary>
        /// <param name="token">Geçersiz kılınacak token</param>
        /// <param name="ipAddress">İstek yapılan IP adresi</param>
        /// <param name="reason">Geçersiz kılma sebebi</param>
        /// <param name="replacedByToken">Yerine geçecek token (varsa)</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> RevokeRefreshTokenAsync(string token, string ipAddress, string reason = null, string replacedByToken = null);

        /// <summary>
        /// Refresh token ile yeni token üretir
        /// </summary>
        /// <param name="accessToken">Mevcut access token</param>
        /// <param name="refreshToken">Mevcut refresh token</param>
        /// <param name="ipAddress">İstek yapılan IP adresi</param>
        /// <returns>Yeni token response</returns>
        Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken, string ipAddress);

        /// <summary>
        /// Access token'dan claim'leri çıkarır
        /// </summary>
        /// <param name="token">JWT access token</param>
        /// <returns>Claim'ler listesi</returns>
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
} 