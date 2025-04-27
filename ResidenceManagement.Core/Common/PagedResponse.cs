using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Sayfalama özelliği olan API yanıtları için sınıf
    /// </summary>
    /// <typeparam name="T">Döndürülecek veri tipi</typeparam>
    public class PagedResponse<T> : ApiResponse<List<T>>
    {
        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Önceki sayfa var mı?
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// PagedResponse constructor
        /// </summary>
        public PagedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = "Veri başarıyla yüklendi";
            this.Success = true;
            this.StatusCode = 200;
            this.TotalRecords = totalRecords;
            this.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        /// <summary>
        /// Başarılı bir sayfalanmış yanıt oluşturur
        /// </summary>
        public static PagedResponse<T> CreateSuccess(List<T> data, int pageNumber, int pageSize, int totalRecords, string message = "Veri başarıyla yüklendi")
        {
            var response = new PagedResponse<T>(data, pageNumber, pageSize, totalRecords)
            {
                Message = message
            };
            
            return response;
        }

        /// <summary>
        /// Başarısız bir sayfalanmış yanıt oluşturur
        /// </summary>
        public static PagedResponse<T> CreateFail(string message, int statusCode, int errorCode, List<string> errors = null)
        {
            var response = new PagedResponse<T>(new List<T>(), 0, 0, 0)
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                ErrorCode = errorCode,
                Errors = errors ?? new List<string>()
            };
            
            return response;
        }
    }
} 