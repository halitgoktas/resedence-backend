using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Constants;
using System.Net;

namespace ResidenceManagement.API.Controllers.Base
{
    /// <summary>
    /// Tüm API Controller'lar için temel sınıf
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Başarılı API yanıtı oluşturur
        /// </summary>
        /// <typeparam name="T">Döndürülecek veri tipi</typeparam>
        /// <param name="data">Döndürülecek veri</param>
        /// <param name="message">Başarı mesajı</param>
        /// <returns>API yanıtı</returns>
        protected IActionResult Success<T>(T data, string message = null)
        {
            return Ok(ApiResponse<T>.Success(data, message));
        }

        /// <summary>
        /// Veri içermeyen başarılı API yanıtı oluşturur
        /// </summary>
        /// <param name="message">Başarı mesajı</param>
        /// <returns>API yanıtı</returns>
        protected IActionResult Success(string message = null)
        {
            return Ok(ApiResponse.Success(message));
        }

        /// <summary>
        /// Başarısız API yanıtı oluşturur
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="statusCode">HTTP durum kodu</param>
        /// <returns>API yanıtı</returns>
        protected IActionResult Failure(string message, int statusCode = 400)
        {
            var httpStatusCode = (HttpStatusCode)statusCode;
            return StatusCode(statusCode, ApiResponse.Failure(message, httpStatusCode));
        }

        /// <summary>
        /// Mevcut kullanıcının ID'sini döndürür
        /// </summary>
        /// <returns>Kullanıcı ID</returns>
        protected int GetCurrentUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                return 0; // Kullanıcı kimliği bulunamadı veya geçerli değil
            }

            return userId;
        }

        /// <summary>
        /// Mevcut kullanıcının firma ID'sini döndürür
        /// </summary>
        /// <returns>Firma ID</returns>
        protected int GetCurrentFirmaId()
        {
            var firmaIdString = User.FindFirst(CustomClaimTypes.FirmaId)?.Value;
            if (!int.TryParse(firmaIdString, out int firmaId))
            {
                return 0; // Firma ID bulunamadı veya geçerli değil
            }

            return firmaId;
        }

        /// <summary>
        /// Mevcut kullanıcının şube ID'sini döndürür
        /// </summary>
        /// <returns>Şube ID</returns>
        protected int GetCurrentSubeId()
        {
            var subeIdString = User.FindFirst(CustomClaimTypes.SubeId)?.Value;
            if (!int.TryParse(subeIdString, out int subeId))
            {
                return 0; // Şube ID bulunamadı veya geçerli değil
            }

            return subeId;
        }

        /// <summary>
        /// Mevcut kullanıcının tenant bilgilerini döndürür
        /// </summary>
        /// <returns>Tenant bilgisi</returns>
        protected TenantInfo GetTenantInfo()
        {
            return new TenantInfo
            {
                FirmaId = GetCurrentFirmaId(),
                SubeId = GetCurrentSubeId()
            };
        }
    }
} 