using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Sayfalama özelliği olan API yanıtları için sınıf
    /// </summary>
    /// <typeparam name="T">Döndürülecek veri tipi</typeparam>
    public class PagedResponse<T> : ApiResponse<IReadOnlyList<T>>
    {
        public PaginationMetadata Pagination { get; set; }

        public PagedResponse() : base() { }

        public PagedResponse(IReadOnlyList<T> data, PaginationMetadata pagination, bool isSuccess, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            : base(isSuccess, data, message, statusCode)
        {
            Pagination = pagination;
        }

        public static PagedResponse<T> Success(IReadOnlyList<T> data, PaginationMetadata pagination, string message = null)
        {
            return new PagedResponse<T>(data, pagination, true, message ?? "İşlem başarıyla tamamlandı.");
        }

        public static PagedResponse<T> Success(PagedList<T> pagedList, string message = null)
        {
            var pagination = new PaginationMetadata
            {
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious
            };

            return Success(pagedList.ToList(), pagination, message);
        }

        public static PagedResponse<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new PagedResponse<T>(null, null, false, message, statusCode);
        }
    }
} 