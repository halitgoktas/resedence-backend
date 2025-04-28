using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Core.Interfaces.Services;
using ResidenceManagement.Core.Models.Authentication;

namespace ResidenceManagement.Infrastructure.Services
{
    /// <summary>
    /// JWT token oluşturma ve yönetimi için servis implementasyonu
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ResidenceManagement.Core.Interfaces.IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRepository<Kullanici> _kullaniciRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork _unitOfWork;

        /// <summary>
        /// TokenService constructor
        /// </summary>
        public TokenService(
            IOptions<JwtSettings> jwtSettings,
            ResidenceManagement.Core.Interfaces.IRefreshTokenRepository refreshTokenRepository,
            IRepository<Kullanici> kullaniciRepository,
            IRepository<Rol> rolRepository,
            IRepository<UserRole> userRoleRepository,
            ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork unitOfWork)
        {
            _jwtSettings = jwtSettings.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _kullaniciRepository = kullaniciRepository;
            _rolRepository = rolRepository;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<TokenResponse> GenerateTokensAsync(Kullanici kullanici, string ipAddress)
        {
            // Token geçerlilik süresi
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            
            // Kullanıcı rollerini getir
            var userRoles = await GetUserRolesAsync(kullanici.Id);
            
            // Claims oluştur
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, kullanici.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, kullanici.Email),
                new Claim("username", kullanici.KullaniciAdi),
                new Claim("firmaId", kullanici.FirmaId.ToString()),
                new Claim("subeId", kullanici.SubeId.ToString()),
                new Claim("fullName", kullanici.TamAd)
            };
            
            // Rolleri claim'lere ekle
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            // Access token oluştur
            var accessToken = GenerateAccessToken(claims, accessTokenExpiration);
            
            // Refresh token oluştur
            var refreshToken = await GenerateRefreshTokenAsync(kullanici.Id, kullanici.FirmaId, kullanici.SubeId, ipAddress);
            
            // Yanıt oluştur
            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshToken.ExpirationDate,
                UserId = kullanici.Id,
                UserName = kullanici.KullaniciAdi,
                Email = kullanici.Email,
                FirmaId = kullanici.FirmaId,
                SubeId = kullanici.SubeId
            };
        }

        /// <inheritdoc/>
        public string GenerateAccessToken(IEnumerable<Claim> claims, DateTime expires)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signinCredentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        /// <inheritdoc/>
        public async Task<RefreshToken> GenerateRefreshTokenAsync(int kullaniciId, int firmaId, int subeId, string ipAddress)
        {
            // Rastgele token üret
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            var refreshToken = Convert.ToBase64String(randomBytes);
            
            // Refresh token geçerlilik süresi
            var expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            
            // Yeni refresh token oluştur
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpirationDate = expires,
                CreatedDate = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                KullaniciId = kullaniciId,
                FirmaId = firmaId,
                SubeId = subeId
            };
            
            // Veritabanına ekle
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();
            
            return refreshTokenEntity;
        }

        /// <inheritdoc/>
        public async Task<bool> RevokeRefreshTokenAsync(string token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            
            if (refreshToken == null || !refreshToken.IsActive)
                return false;
            
            // Token'ı geçersiz kıl
            refreshToken.RevokedDate = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;
            
            // Veritabanını güncelle
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }

        /// <inheritdoc/>
        public async Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken, string ipAddress)
        {
            var principal = GetPrincipalFromToken(accessToken);
            
            // Eğer token geçerli değilse veya claim'leri yoksa
            if (principal == null)
                return null;
            
            // Sub claim'inden kullanıcı ID'sini al
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return null;
            
            // Refresh token'ı veritabanından al
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            
            // Token geçerli değilse veya farklı bir kullanıcıya aitse
            if (storedRefreshToken == null || storedRefreshToken.KullaniciId != userId || !storedRefreshToken.IsActive)
                return null;
            
            // Kullanıcıyı al
            var kullanici = await _kullaniciRepository.GetByIdAsync(userId);
            
            if (kullanici == null || !kullanici.IsActive)
                return null;
            
            // Eski token'ı geçersiz kıl ve yeni token oluştur
            await RevokeRefreshTokenAsync(refreshToken, ipAddress, "Refreshed", null);
            
            return await GenerateTokensAsync(kullanici, ipAddress);
        }

        /// <inheritdoc/>
        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false, // Token süresini kontrol etmiyoruz çünkü süresi dolmuş olabilir
                ClockSkew = TimeSpan.Zero
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            
            try
            {
                // Token'ı validate et
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                
                // Token formatını kontrol et
                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || 
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;
                
                return principal;
            }
            catch
            {
                // Token validasyonu başarısız oldu
                return null;
            }
        }
        
        /// <summary>
        /// Kullanıcının rollerini getirir
        /// </summary>
        private async Task<List<string>> GetUserRolesAsync(int userId)
        {
            // Kullanıcının rol bağlantılarını al
            var userRoles = await _userRoleRepository.FindAsync(ur => ur.KullaniciId == userId);
            
            if (userRoles == null || !userRoles.Any())
                return new List<string>();
            
            // Rol ID'lerini al
            var roleIds = userRoles.Select(ur => ur.RolId).ToList();
            
            // Rolleri getir - roleIds ve r.Id'nin string olarak karşılaştırılması için
            // her bir rolü ayrı ayrı sorgulayıp birleştiriyoruz
            var roles = new List<Rol>();
            foreach (var roleId in roleIds)
            {
                var role = await _rolRepository.GetFirstOrDefaultAsync(r => r.Id.ToString() == roleId.ToString());
                if (role != null)
                {
                    roles.Add(role);
                }
            }
            
            // Rol adlarını döndür
            return roles.Select(r => r.RolAdi).ToList();
        }
    }
} 