using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.DTOs.User;
using ResidenceManagement.Core.Interfaces.Services;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResidenceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthenticationService authService, IUserService userService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _userService = userService;
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
            
            if (!response.IsSuccess)
            {
                return (int)response.StatusCode switch
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
            
            if (!response.IsSuccess)
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
                return BadRequest(ApiResponse<bool>.Failure(
                    message: "Token bilgisi gereklidir",
                    statusCode: HttpStatusCode.BadRequest));
            }

            var response = await _authService.RevokeTokenAsync(token);
            
            if (!response.IsSuccess)
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
                return BadRequest(ApiResponse<UserInfoResponse>.Failure(
                    message: "Kullanıcı bilgisi bulunamadı",
                    statusCode: HttpStatusCode.BadRequest));
            }

            var response = await _authService.GetUserInfoAsync(id);
            
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Şifremi unuttum işlemi
        /// </summary>
        /// <param name="request">Kullanıcı email adresi</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(ApiResponse<bool>.Failure(
                    message: "Email adresi gereklidir",
                    statusCode: HttpStatusCode.BadRequest));
            }

            var response = await _userService.ForgotPasswordAsync(request.Email);

            if (!response.IsSuccess)
            {
                return (int)response.StatusCode switch
                {
                    404 => NotFound(response),
                    _ => StatusCode((int)response.StatusCode, response)
                };
            }

            return Ok(response);
        }

        /// <summary>
        /// Şifre sıfırlama işlemi
        /// </summary>
        /// <param name="request">Sıfırlama token ve yeni şifre bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest(ApiResponse<bool>.Failure(
                    message: "Token ve yeni şifre bilgileri gereklidir",
                    statusCode: HttpStatusCode.BadRequest));
            }

            var response = await _userService.ResetPasswordAsync(request.Token, request.NewPassword);

            if (!response.IsSuccess)
            {
                return (int)response.StatusCode switch
                {
                    400 => BadRequest(response),
                    _ => StatusCode((int)response.StatusCode, response)
                };
            }

            return Ok(response);
        }
    }
} 