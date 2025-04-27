using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Sayfalanmış veri listesi
    /// </summary>
    /// <typeparam name="T">Liste öğelerinin tipi</typeparam>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Sayfa başına öğe sayısı
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Toplam öğe sayısı
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Önceki sayfa var mı?
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// PagedList sınıfının yapıcı metodu
        /// </summary>
        /// <param name="items">Mevcut sayfadaki öğeler</param>
        /// <param name="count">Toplam öğe sayısı</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına öğe sayısı</param>
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        /// <summary>
        /// IQueryable'dan sayfalanmış liste oluşturur
        /// </summary>
        /// <param name="source">Veri kaynağı</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına öğe sayısı</param>
        /// <returns>Sayfalanmış liste</returns>
        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// IEnumerable'dan sayfalanmış liste oluşturur
        /// </summary>
        /// <param name="source">Veri kaynağı</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına öğe sayısı</param>
        /// <returns>Sayfalanmış liste</returns>
        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// IQueryable'dan asenkron olarak sayfalanmış liste oluşturur
        /// </summary>
        /// <param name="source">Veri kaynağı</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına öğe sayısı</param>
        /// <returns>Sayfalanmış liste task'i</returns>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Sayfalama parametrelerini kontrol eder ve varsayılan değerleri ayarlar
        /// </summary>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına öğe sayısı</param>
        /// <param name="defaultPageSize">Varsayılan sayfa boyutu</param>
        /// <param name="maxPageSize">Maksimum sayfa boyutu</param>
        /// <returns>Düzenlenmiş sayfalama parametreleri (pageNumber, pageSize)</returns>
        public static (int pageNumber, int pageSize) ValidateParameters(
            int? pageNumber, 
            int? pageSize, 
            int defaultPageSize = 10, 
            int maxPageSize = 100)
        {
            // Sayfa numarası kontrolü
            int validatedPageNumber = pageNumber.HasValue && pageNumber.Value > 0 
                ? pageNumber.Value 
                : 1;

            // Sayfa boyutu kontrolü
            int validatedPageSize = pageSize.HasValue && pageSize.Value > 0 
                ? (pageSize.Value > maxPageSize ? maxPageSize : pageSize.Value) 
                : defaultPageSize;

            return (validatedPageNumber, validatedPageSize);
        }
    }
} 