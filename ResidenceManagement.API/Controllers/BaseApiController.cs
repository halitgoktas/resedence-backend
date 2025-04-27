using Microsoft.AspNetCore.Mvc;

namespace ResidenceManagement.API.Controllers
{
    /// <summary>
    /// Tüm API controllerları için temel sınıf.
    /// Bu sınıf, API route yapısını standartlaştırır ve ortak controller özelliklerini sağlar.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Pagination için HTTP header'larını ekler
        /// </summary>
        /// <param name="currentPage">Mevcut sayfa</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        /// <param name="totalCount">Toplam kayıt sayısı</param>
        /// <param name="totalPages">Toplam sayfa sayısı</param>
        protected void AddPaginationHeaders(int currentPage, int pageSize, int totalCount, int totalPages)
        {
            var paginationHeaders = new
            {
                currentPage,
                pageSize,
                totalCount,
                totalPages
            };

            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationHeaders));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }
    }
} 