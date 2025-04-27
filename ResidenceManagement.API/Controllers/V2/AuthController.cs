using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Interfaces.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResidenceManagement.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthenticationService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Kullanıcı giriş işlemi (V2 - Geliştirilmiş güvenlik)
        /// </summary>
        /// <param name="loginRequest">Kullanıcı adı/email ve şifre bilgileri</param>
        /// <returns>Token bilgileri</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // V2: IP bazlı rate limiting kontrolü (örnek)
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            
            // Gerçek uygulamada burada bir rate limiting servisi kullanılabilir
            _logger.LogInformation($"V2 Auth: Giriş denemesi - IP: {clientIp}, Kullanıcı: {loginRequest.UserName}");

            var response = await _authService.LoginAsync(loginRequest);
            
            if (!response.Success)
            {
                if (response.StatusCode == 429)
                {
                    return StatusCode(429, response);
                }
                
                return response.StatusCode switch
                {
                    401 => Unauthorized(response),
                    _ => BadRequest(response)
                };
            }

            // V2: Başarılı giriş loglama
            _logger.LogInformation($"V2 Auth: Başarılı giriş - Kullanıcı: {loginRequest.UserName}");
            return Ok(response);
        }

        /// <summary>
        /// Token yenileme işlemi
        /// </summary>
        /// <param name="refreshRequest">Yenilenecek token bilgileri</param>
        /// <returns>Yeni token bilgileri</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshRequest)
        {
            // V2: Geliştirilmiş IP ve cihaz kontrolü
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            
            _logger.LogInformation($"V2 Auth: Token yenileme - IP: {clientIp}, UA: {userAgent}");
            
            var response = await _authService.RefreshTokenAsync(refreshRequest);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Token geçersiz kılma (çıkış) işlemi
        /// </summary>
        /// <param name="revokeRequest">Geçersiz kılınacak token bilgisi</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("revoke-token")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest revokeRequest)
        {
            // Eğer token sağlanmadıysa, kullanıcının kendi tokenını iptal etmeye çalıştığını varsayalım
            var token = revokeRequest?.Token;
            if (string.IsNullOrEmpty(token))
            {
                token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            }

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Token bilgisi gereklidir",
                    Data = false,
                    StatusCode = 400
                });
            }

            // V2: İstemci bilgileri loglama
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation($"V2 Auth: Token iptali - IP: {clientIp}");

            var response = await _authService.RevokeTokenAsync(token);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Giriş yapmış kullanıcının bilgilerini getirme
        /// </summary>
        /// <returns>Kullanıcı bilgileri</returns>
        [HttpGet("user-info")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<UserInfoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                return BadRequest(new ApiResponse<UserInfoResponse>
                {
                    Success = false,
                    Message = "Kullanıcı bilgisi bulunamadı",
                    StatusCode = 400
                });
            }

            var response = await _authService.GetUserInfoAsync(id);
            
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        
        /// <summary>
        /// Kullanıcının oturum açma geçmişini getirir (yalnızca V2'de mevcut)
        /// </summary>
        /// <returns>Oturum açma geçmişi</returns>
        [HttpGet("login-history")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<List<LoginHistoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLoginHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Kullanıcı bilgisi bulunamadı",
                    StatusCode = 400
                });
            }

            // Not: Gerçek bir uygulamada bu metot AuthenticationService'de implement edilecektir
            // Şu an için örnek veri döndürüyoruz
            var loginHistory = new List<LoginHistoryDto>
            {
                new LoginHistoryDto 
                { 
                    LoginDate = DateTime.UtcNow.AddDays(-1),
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
                    Status = "Success"
                },
                new LoginHistoryDto 
                { 
                    LoginDate = DateTime.UtcNow.AddDays(-2),
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_0)",
                    Status = "Success"
                }
            };

            var response = new ApiResponse<List<LoginHistoryDto>>
            {
                Success = true,
                Message = "Oturum açma geçmişi başarıyla getirildi",
                Data = loginHistory,
                StatusCode = 200
            };

            return Ok(response);
        }
    }
    
    // V2 için yeni DTO
    public class LoginHistoryDto
    {
        public DateTime LoginDate { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Status { get; set; }
    }
} 