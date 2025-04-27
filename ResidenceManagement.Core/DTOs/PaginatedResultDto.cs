using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs
{
    // Sayfalanmış sonuçlar için generic DTO sınıfı
    // T tipinde bir liste ve sayfalama bilgilerini içerir
    public class PaginatedResultDto<T>
    {
        // Sayfa verilerini içeren liste
        public List<T> Items { get; set; } = new List<T>();
        
        // Toplam öğe sayısı
        public int TotalCount { get; set; }
        
        // Şu anki sayfa numarası
        public int PageNumber { get; set; }
        
        // Sayfa başına öğe sayısı
        public int PageSize { get; set; }
        
        // Toplam sayfa sayısı
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        
        // Önceki sayfa var mı?
        public bool HasPreviousPage => PageNumber > 1;
        
        // Sonraki sayfa var mı?
        public bool HasNextPage => PageNumber < TotalPages;
        
        // Firma ve şube bilgileri (multi-tenant)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Constructor
        public PaginatedResultDto() { }

        // Parametreli constructor
        public PaginatedResultDto(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
} 