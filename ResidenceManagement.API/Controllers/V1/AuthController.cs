using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Interfaces.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResidenceManagement.API.Controllers.V1
{
    [ApiVersion("1.0")]
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
        /// Kullanıcı giriş işlemi
        /// </summary>
        /// <param name="loginRequest">Kullanıcı adı/email ve şifre bilgileri</param>
        /// <returns>Token bilgileri</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            
            if (!response.Success)
            {
                return response.StatusCode switch
                {
                    401 => Unauthorized(response),
                    _ => BadRequest(response)
                };
            }

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
    }
} 