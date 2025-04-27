using Microsoft.AspNetCore.Mvc;
using ResidenceManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResidenceManagement.API.Controllers
{
    /// <summary>
    /// API controller'lar için temel sınıf
    /// </summary>
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Başarılı bir API yanıtı döner
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="data">Yanıt verisi</param>
        /// <param name="message">Mesaj (opsiyonel)</param>
        /// <returns>IActionResult</returns>
        protected IActionResult Success<T>(T data, string message = null)
        {
            var response = ApiResponse<T>.Success(data, message);
            return Ok(response);
        }

        /// <summary>
        /// Başarılı bir API yanıtı döner (veri içermeyen)
        /// </summary>
        /// <param name="message">Mesaj (opsiyonel)</param>
        /// <returns>IActionResult</returns>
        protected IActionResult Success(string message = null)
        {
            var response = ApiResponse.Success(message);
            return Ok(response);
        }

        /// <summary>
        /// Başarısız bir API yanıtı döner
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="statusCode">HTTP durum kodu</param>
        /// <param name="errors">Hata listesi (opsiyonel)</param>
        /// <returns>IActionResult</returns>
        protected IActionResult Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, List<string> errors = null)
        {
            var response = ApiResponse.Failure(message, statusCode);
            if (errors != null && errors.Any())
            {
                response.Errors.AddRange(errors);
            }
            return StatusCode((int)statusCode, response);
        }

        /// <summary>
        /// Sayfalanmış veri için metadata header'larını ekler
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="pagedList">Sayfalanmış veri</param>
        protected void AddPaginationHeader<T>(PagedList<T> pagedList)
        {
            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                hasPrevious = pagedList.HasPrevious,
                hasNext = pagedList.HasNext
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            Response.Headers.Add("X-Total-Count", pagedList.TotalCount.ToString());
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination, X-Total-Count");
        }

        /// <summary>
        /// URL parametrelerinden filtreleme parametrelerini çıkarır
        /// </summary>
        /// <param name="prefix">Parametre öneki (örn: "filter.")</param>
        /// <returns>Filtreleme parametreleri</returns>
        protected FilteringParameters ExtractFilterParameters(string prefix = "filter.")
        {
            var filterParams = new FilteringParameters();
            var queryParams = HttpContext.Request.Query
                .Where(q => q.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var param in queryParams)
            {
                string propertyName = param.Key.Substring(prefix.Length);
                string propertyValue = param.Value.ToString();

                // Filtre operatörünü belirle (varsayılan: Equals)
                FilterOperator @operator = FilterOperator.Equals;

                // Operatör belirleme (örn: "name:contains" gibi formatlar için)
                if (propertyName.Contains(':'))
                {
                    var parts = propertyName.Split(':');
                    propertyName = parts[0];

                    if (Enum.TryParse<FilterOperator>(parts[1], true, out var parsedOperator))
                    {
                        @operator = parsedOperator;
                    }
                }

                filterParams.AddFilter(propertyName, @operator, propertyValue);
            }

            return filterParams;
        }

        /// <summary>
        /// URL parametrelerinden sıralama parametrelerini çıkarır
        /// </summary>
        /// <returns>Sıralama parametreleri</returns>
        protected SortingParameters ExtractSortParameters()
        {
            var sortParams = new SortingParameters();

            if (HttpContext.Request.Query.TryGetValue("orderBy", out var orderByValues))
            {
                sortParams.OrderBy = orderByValues.ToString();
            }

            if (HttpContext.Request.Query.TryGetValue("orderDirection", out var orderDirectionValues))
            {
                sortParams.OrderDirection = orderDirectionValues.ToString();
            }

            return sortParams;
        }

        /// <summary>
        /// URL parametrelerinden sayfalama parametrelerini çıkarır
        /// </summary>
        /// <returns>Sayfalama parametreleri</returns>
        protected PaginationParameters ExtractPaginationParameters()
        {
            var paginationParams = new PaginationParameters();

            if (HttpContext.Request.Query.TryGetValue("pageNumber", out var pageNumberValues) &&
                int.TryParse(pageNumberValues, out int pageNumber))
            {
                paginationParams.Page = pageNumber;
            }

            if (HttpContext.Request.Query.TryGetValue("pageSize", out var pageSizeValues) &&
                int.TryParse(pageSizeValues, out int pageSize))
            {
                paginationParams.PageSize = pageSize;
            }

            return paginationParams;
        }
    }
} 