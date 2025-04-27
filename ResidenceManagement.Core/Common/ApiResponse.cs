using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.Json.Serialization;
using ResidenceManagement.Core.Extensions;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// API yanıtları için temel sınıf
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse() { }

        public ApiResponse(bool isSuccess, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        /// <summary>
        /// İşlemin başarılı olup olmadığı
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Yanıt mesajı
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Yanıtın zamanı
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Yanıtın HTTP durum kodu
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Hata listesi
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Başarılı bir yanıt oluşturur
        /// </summary>
        /// <param name="message">Yanıt mesajı</param>
        /// <returns>Başarılı API yanıtı</returns>
        public static ApiResponse Success(string message = null)
        {
            return new ApiResponse(true, message ?? "İşlem başarıyla tamamlandı.");
        }

        /// <summary>
        /// Başarısız bir yanıt oluşturur
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="statusCode">Hata durum kodu</param>
        /// <returns>Başarısız API yanıtı</returns>
        public static ApiResponse Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse(false, message, statusCode);
        }
    }

    /// <summary>
    /// Generic veri içeren API yanıtı
    /// </summary>
    /// <typeparam name="T">Dönüş verisi tipi</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse() : base() { }

        public ApiResponse(bool isSuccess, T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            : base(isSuccess, message, statusCode)
        {
            Data = data;
        }

        /// <summary>
        /// Yanıt verisi
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Veri içeren başarılı yanıt oluşturur
        /// </summary>
        /// <param name="data">Yanıt verisi</param>
        /// <param name="message">Yanıt mesajı</param>
        /// <returns>Başarılı veri içeren API yanıtı</returns>
        public static ApiResponse<T> Success(T data, string message = null)
        {
            return new ApiResponse<T>(true, data, message ?? "İşlem başarıyla tamamlandı.");
        }

        /// <summary>
        /// Başarısız bir yanıt oluşturur (veri olmadan)
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="statusCode">Hata durum kodu</param>
        /// <returns>Başarısız API yanıtı</returns>
        public static ApiResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>(false, default, message, statusCode);
        }
    }

    /// <summary>
    /// Sayfalama bilgilerini içeren metadata sınıfı
    /// </summary>
    public class PaginationMetadata
    {
        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Önceki sayfa var mı?
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// PagedList'den metadata oluşturur
        /// </summary>
        /// <typeparam name="T">Sayfalanmış liste tipi</typeparam>
        /// <param name="pagedList">Sayfalanmış liste</param>
        /// <returns>Sayfalama metadata'sı</returns>
        public static PaginationMetadata FromPagedList<T>(PagedList<T> pagedList)
        {
            return new PaginationMetadata
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasPrevious = pagedList.HasPrevious,
                HasNext = pagedList.HasNext
            };
        }
    }

    /// <summary>
    /// Sayfalanmış veri için API yanıtı
    /// </summary>
    /// <typeparam name="T">Dönüş verisi tipi</typeparam>
    public class PagedApiResponse<T> : ApiResponse
    {
        public PagedApiResponse() : base() { }

        public PagedApiResponse(bool isSuccess, PagedList<T> data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            : base(isSuccess, message, statusCode)
        {
            Data = data;
            if (data != null)
            {
                Pagination = new PaginationMetadata
                {
                    CurrentPage = data.CurrentPage,
                    PageSize = data.PageSize,
                    TotalCount = data.TotalCount,
                    TotalPages = data.TotalPages,
                    HasNext = data.HasNext,
                    HasPrevious = data.HasPrevious
                };
            }
        }

        /// <summary>
        /// Sayfalanmış veri
        /// </summary>
        public PagedList<T> Data { get; set; }

        /// <summary>
        /// Sayfalama bilgileri
        /// </summary>
        public PaginationMetadata Pagination { get; set; }

        /// <summary>
        /// Sayfalanmış veri içeren başarılı yanıt oluşturur
        /// </summary>
        /// <param name="pagedList">Sayfalanmış liste</param>
        /// <param name="message">Yanıt mesajı</param>
        /// <returns>Başarılı sayfalanmış veri içeren API yanıtı</returns>
        public static PagedApiResponse<T> Success(PagedList<T> pagedList, string message = null)
        {
            return new PagedApiResponse<T>(true, pagedList, message ?? "İşlem başarıyla tamamlandı.");
        }

        /// <summary>
        /// Sayfalama bilgisi ve veri ile başarılı yanıt oluşturur
        /// </summary>
        /// <param name="data">Listelenecek veri</param>
        /// <param name="pagination">Sayfalama bilgileri</param>
        /// <param name="message">Yanıt mesajı</param>
        /// <returns>Başarılı sayfalanmış veri içeren API yanıtı</returns>
        public static PagedApiResponse<T> Success(IEnumerable<T> data, PaginationMetadata pagination, string message = null)
        {
            var pagedList = new PagedList<T>(new List<T>(data), pagination.TotalCount, pagination.CurrentPage, pagination.PageSize);
            return new PagedApiResponse<T>(true, pagedList, message ?? "İşlem başarıyla tamamlandı.");
        }

        /// <summary>
        /// Başarısız bir yanıt oluşturur (sayfalanmış veri olmadan)
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="statusCode">Hata durum kodu</param>
        /// <returns>Başarısız API yanıtı</returns>
        public static PagedApiResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new PagedApiResponse<T>(false, null, message, statusCode);
        }
    }
} 