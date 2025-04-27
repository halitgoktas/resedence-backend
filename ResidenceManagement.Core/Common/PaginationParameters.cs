using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Sayfalama parametreleri
    /// </summary>
    public class PaginationParameters
    {
        private const int MaxPageSize = 100;
        private const int DefaultPageSize = 10;
        private int _pageSize = DefaultPageSize;

        /// <summary>
        /// Mevcut sayfa (1'den başlar)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Sayfa numarası en az 1 olmalıdır.")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        [Range(1, 100, ErrorMessage = "Sayfa boyutu 1 ile 100 arasında olmalıdır.")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary>
        /// Sıralama alanı
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Sıralama yönü (asc/desc)
        /// </summary>
        public string OrderDirection { get; set; } = "asc";

        /// <summary>
        /// Arama terimi
        /// </summary>
        public string SearchTerm { get; set; }
    }
} 