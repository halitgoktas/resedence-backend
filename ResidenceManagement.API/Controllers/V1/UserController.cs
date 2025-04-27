using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.User;
using ResidenceManagement.Core.Interfaces.Services;

namespace ResidenceManagement.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Şifremi unuttum işlemini başlatır
        /// </summary>
        /// <param name="forgotPasswordDto">Email bilgisi</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Validasyon hatası",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            
            var response = await _userService.ForgotPasswordAsync(forgotPasswordDto.Email);
            
            // Güvenlik nedeniyle, kullanıcı olsun veya olmasın aynı başarılı yanıtı döndürüyoruz
            // Böylece kötü niyetli kişiler sistemdeki email adreslerini tespit edemezler
            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogWarning($"Olmayan bir email için şifre sıfırlama isteği: {forgotPasswordDto.Email}");
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Şifre sıfırlama bağlantısı email adresinize gönderildi",
                    Data = true,
                    StatusCode = StatusCodes.Status200OK
                });
            }
            
            return StatusCode(response.StatusCode, response);
        }
        
        /// <summary>
        /// Şifre sıfırlama işlemini tamamlar
        /// </summary>
        /// <param name="resetPasswordDto">Şifre sıfırlama bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Validasyon hatası",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            
            var response = await _userService.ResetPasswordAsync(resetPasswordDto.Token, resetPasswordDto.NewPassword);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
} 