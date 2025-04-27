using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ResidenceManagement.Core.Interfaces;
using System.Linq;
using ResidenceManagement.Core.Interfaces.Logging;
using System.Net;
using System.Diagnostics;

namespace ResidenceManagement.Infrastructure.Services
{
    /// <summary>
    /// Kimlik doğrulama servisi implementasyonu
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<Kullanici> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Rol> _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPerformanceMetricsLoggingService _performanceLogger;

        /// <summary>
        /// AuthenticationService constructor
        /// </summary>
        public AuthenticationService(
            IRepository<Kullanici> userRepository,
            IRepository<RefreshToken> refreshTokenRepository,
            IRepository<UserRole> userRoleRepository,
            IRepository<Rol> roleRepository,
            IConfiguration configuration,
            ILogger<AuthenticationService> logger,
            IUserService userService,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IPerformanceMetricsLoggingService performanceLogger)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _performanceLogger = performanceLogger;
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                // Kullanıcı adı veya email ile kullanıcıyı bul
                var user = await _userRepository.GetFirstOrDefaultAsync(u =>
                    u.KullaniciAdi == loginRequest.UserName || u.Email == loginRequest.UserName);
                
                if (user == null)
                {
                    _logger.LogWarning($"Giriş başarısız: Kullanıcı adı bulunamadı - {loginRequest.UserName}");
                    return ApiResponse<TokenResponse>.Failure("Kullanıcı adı veya şifre hatalı.", HttpStatusCode.Unauthorized);
                }

                // Kullanıcı aktif değilse
                if (!user.IsActive)
                {
                    _logger.LogWarning($"Giriş başarısız: Kullanıcı aktif değil - {loginRequest.UserName}");
                    return ApiResponse<TokenResponse>.Failure("Hesabınız devre dışı bırakılmıştır. Lütfen yönetici ile iletişime geçin.", HttpStatusCode.Unauthorized);
                }

                // Şifreyi doğrula
                if (!VerifyPasswordHash(loginRequest.Password, user.Sifre, user.SifreSalt))
                {
                    _logger.LogWarning($"Giriş başarısız: Şifre hatalı - {loginRequest.UserName}");
                    return ApiResponse<TokenResponse>.Failure("Kullanıcı adı veya şifre hatalı.", HttpStatusCode.Unauthorized);
                }

                // IP adresi
                string ipAddress = "127.0.0.1"; // Default değer, gerçek IP adresi controller'dan gelebilir

                // Token oluştur
                var tokens = await _tokenService.GenerateTokensAsync(user, ipAddress);

                // Kullanıcının son giriş bilgilerini güncelle
                user.SonGirisTarihi = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                var stopwatch = Stopwatch.StartNew();
                stopwatch.Stop();
                _performanceLogger.MeasureExecutionTime("LoginAsync", () => { }, $"Duration: {stopwatch.ElapsedMilliseconds}ms");

                return new ApiResponse<TokenResponse>
                {
                    IsSuccess = true,
                    Message = "Giriş başarılı",
                    Data = tokens,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Login sırasında hata oluştu: {ex.Message}");
                _performanceLogger.MeasureExecutionTime("Login", () => { }, $"Duration: {Stopwatch.GetTimestamp()}ms");
                
                return new ApiResponse<TokenResponse>
                {
                    IsSuccess = false,
                    Message = "Giriş işlemi sırasında bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            try
            {
                // Refresh token veritabanında ara
                var refreshToken = await _refreshTokenRepository.GetFirstOrDefaultAsync(rt => 
                    rt.Token == request.RefreshToken);

                // Refresh token bulunamadıysa
                if (refreshToken == null)
                {
                    return new ApiResponse<TokenResponse>
                    {
                        IsSuccess = false,
                        Message = "Geçersiz refresh token.",
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }

                // Refresh token geçersiz kılınmışsa veya süresi dolmuşsa
                if (refreshToken.RevokedDate != null || refreshToken.IsExpired)
                {
                    return new ApiResponse<TokenResponse>
                    {
                        IsSuccess = false,
                        Message = "Refresh token süresi dolmuş veya geçersiz kılınmış.",
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }

                // Kullanıcıyı bul
                var user = await _userRepository.GetByIdAsync(refreshToken.KullaniciId);
                if (user == null || !user.IsActive)
                {
                    return new ApiResponse<TokenResponse>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı veya hesap aktif değil.",
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }

                // Kullanıcının rollerini al
                var userRoles = await _userRoleRepository.GetAllAsync(ur => ur.KullaniciId == user.Id);
                var roleIds = userRoles.Select(ur => ur.RolId).ToList();

                // roles listesini birleştirmek için yeni bir liste oluştur
                var roles = new List<Rol>();

                // Her bir roleId için rol bul
                foreach (var roleId in roleIds)
                {
                    var role = await _roleRepository.GetFirstOrDefaultAsync(r => r.Id.ToString() == roleId.ToString());
                    if (role != null)
                    {
                        roles.Add(role);
                    }
                }

                // Yeni token ve refresh token oluştur
                var jwtToken = GenerateJwtToken(user, roles.Select(r => r.RolAdi).ToList());
                var newRefreshToken = GenerateRefreshToken();

                // Eski refresh token'ı geçersiz kıl
                refreshToken.RevokedDate = DateTime.UtcNow;
                refreshToken.ReplacedByToken = newRefreshToken;
                refreshToken.ReasonRevoked = "Yenilendi";
                refreshToken.IsActive = false;

                await _refreshTokenRepository.UpdateAsync(refreshToken);

                // Yeni refresh token'ı kaydet
                var newRefreshTokenEntity = new RefreshToken
                {
                    Token = newRefreshToken,
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    CreatedByIp = "127.0.0.1", // IP bilgisi request'ten alınabilir
                    KullaniciId = user.Id,
                    FirmaId = user.FirmaId,
                    SubeId = user.SubeId
                };

                await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);
                await _refreshTokenRepository.SaveChangesAsync();

                var stopwatch = Stopwatch.StartNew();
                stopwatch.Stop();
                _performanceLogger.MeasureExecutionTime("RefreshToken", () => { }, $"Duration: {stopwatch.ElapsedMilliseconds}ms");

                return new ApiResponse<TokenResponse>
                {
                    IsSuccess = true,
                    Message = "Token yenilendi.",
                    Data = new TokenResponse
                    {
                        Token = jwtToken,
                        RefreshToken = newRefreshToken,
                        ExpiresIn = 3600 // Token geçerlilik süresi (saniye)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token yenileme sırasında hata oluştu: {ErrorMessage}", ex.Message);
                _performanceLogger.MeasureExecutionTime("RefreshToken", () => { }, $"Duration: {Stopwatch.GetTimestamp()}ms");
                return new ApiResponse<TokenResponse>
                {
                    IsSuccess = false,
                    Message = "Token yenileme sırasında bir hata oluştu.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> RevokeTokenAsync(string token)
        {
            try
            {
                // Refresh token veritabanında ara
                var refreshToken = await _refreshTokenRepository.GetFirstOrDefaultAsync(rt => 
                    rt.Token == token);

                // Refresh token bulunamadıysa
                if (refreshToken == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Geçersiz refresh token.",
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }

                // Refresh token zaten geçersiz kılınmışsa
                if (refreshToken.RevokedDate != null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Refresh token zaten geçersiz kılınmış.",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                // Refresh token'ı geçersiz kıl
                refreshToken.RevokedDate = DateTime.UtcNow;
                refreshToken.RevokedByIp = "127.0.0.1"; // IP bilgisi request'ten alınabilir
                refreshToken.ReasonRevoked = "Kullanıcı çıkış yaptı";
                refreshToken.IsActive = false;

                await _refreshTokenRepository.UpdateAsync(refreshToken);
                await _refreshTokenRepository.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Token başarıyla geçersiz kılındı.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token geçersiz kılma sırasında hata oluştu: {ErrorMessage}", ex.Message);
                _performanceLogger.MeasureExecutionTime("RevokeToken", () => { }, $"Duration: {Stopwatch.GetTimestamp()}ms");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Token geçersiz kılma sırasında bir hata oluştu.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<UserInfoResponse>> GetUserInfoAsync(int userId)
        {
            try
            {
                // Kullanıcıyı bul
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ApiResponse<UserInfoResponse>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Kullanıcının rollerini al
                var userRoles = await _userRoleRepository.GetAllAsync(ur => ur.KullaniciId == user.Id);
                var roleIds = userRoles.Select(ur => ur.RolId).ToList();

                // roles listesini birleştirmek için yeni bir liste oluştur
                var roles = new List<Rol>();

                // Her bir roleId için rol bul
                foreach (var roleId in roleIds)
                {
                    var role = await _roleRepository.GetFirstOrDefaultAsync(r => r.Id.ToString() == roleId.ToString());
                    if (role != null)
                    {
                        roles.Add(role);
                    }
                }

                // Kullanıcı bilgilerini DTO'ya aktar
                var userInfo = new UserInfoResponse
                {
                    UserId = user.Id,
                    Username = user.KullaniciAdi,
                    FirstName = user.Ad,
                    LastName = user.Soyad,
                    Email = user.Email,
                    PhoneNumber = user.Telefon,
                    Roles = roles.Select(r => r.RolAdi).ToList(),
                    CompanyId = user.FirmaId,
                    BranchId = user.SubeId
                };

                return new ApiResponse<UserInfoResponse>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı bilgileri başarıyla getirildi.",
                    Data = userInfo
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bilgileri getirme sırasında hata oluştu: {ErrorMessage}", ex.Message);
                _performanceLogger.MeasureExecutionTime("GetUserInfo", () => { }, $"Duration: {Stopwatch.GetTimestamp()}ms");
                return new ApiResponse<UserInfoResponse>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı bilgileri getirme sırasında bir hata oluştu.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        private string GenerateJwtToken(Kullanici user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.KullaniciAdi),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirmaId", user.FirmaId.ToString()),
                new Claim("SubeId", user.SubeId.ToString())
            };

            // Rolleri ekle
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash) || string.IsNullOrWhiteSpace(storedSalt)) 
                return false;
            
            try
            {
                var saltBytes = Convert.FromBase64String(storedSalt);
                var hashBytes = Convert.FromBase64String(storedHash);
                
                using (var hmac = new HMACSHA512(saltBytes))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(computedHash) == storedHash;
                }
            }
            catch
            {
                return false;
            }
        }
    }
} 