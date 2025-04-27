namespace ResidenceManagement.Core.Models.Pagination
{
    /// <summary>
    /// API sorgularında kullanılacak sayfalama parametreleri modeli
    /// </summary>
    public class PaginationParameters
    {
        private const int _maxPageSize = 50;
        private const int _defaultPageSize = 10;
        private int _pageSize = _defaultPageSize;
        private int _pageNumber = 1;
        
        /// <summary>
        /// Sayfa numarası (1'den başlar)
        /// </summary>
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }
        
        /// <summary>
        /// Sayfa başına kayıt sayısı (max 50)
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value < 1 ? _defaultPageSize : value;
        }
    }
} 