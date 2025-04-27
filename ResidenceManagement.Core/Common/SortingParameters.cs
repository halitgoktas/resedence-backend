using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// API sıralama parametreleri için temel sınıf
    /// </summary>
    public class SortingParameters
    {
        /// <summary>
        /// Sıralama alanı
        /// </summary>
        public string OrderBy { get; set; } = string.Empty;

        /// <summary>
        /// Sıralama yönü (asc veya desc)
        /// </summary>
        public string OrderDirection { get; set; } = "asc";

        /// <summary>
        /// Sıralama yönünün azalan olup olmadığını belirtir
        /// </summary>
        public bool IsDescending => OrderDirection.ToLowerInvariant() == "desc";
    }

    /// <summary>
    /// IQueryable üzerinde sıralama işlemleri yapmak için extension sınıfı
    /// </summary>
    public static class SortingExtensions
    {
        /// <summary>
        /// IQueryable üzerinde dinamik sıralama uygular
        /// </summary>
        /// <typeparam name="T">Sorgu tipi</typeparam>
        /// <param name="query">Sorgu</param>
        /// <param name="sortingParams">Sıralama parametreleri</param>
        /// <param name="defaultOrderBy">Varsayılan sıralama alanı</param>
        /// <returns>Sıralanmış sorgu</returns>
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> query, 
            SortingParameters sortingParams, 
            string defaultOrderBy = "Id")
        {
            if (string.IsNullOrWhiteSpace(sortingParams.OrderBy))
            {
                sortingParams.OrderBy = defaultOrderBy;
            }

            // Sıralama alanı için property bilgisini alıyoruz
            var propertyInfo = GetPropertyInfo<T>(sortingParams.OrderBy);
            if (propertyInfo == null)
            {
                // Eğer belirtilen sıralama alanı bulunamazsa, varsayılan alanı kullanıyoruz
                propertyInfo = GetPropertyInfo<T>(defaultOrderBy);
                if (propertyInfo == null)
                {
                    // Varsayılan alan da bulunamazsa, sıralama yapmadan geri dönüyoruz
                    return query;
                }
            }

            // Sıralama için gerekli expression'ları oluşturuyoruz
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            // Sıralama yönüne göre metod çağrısını belirliyoruz
            string methodName = sortingParams.IsDescending ? "OrderByDescending" : "OrderBy";

            // Generic Order metodu için gerekli type argümanları
            Type[] types = new Type[] { typeof(T), propertyInfo.PropertyType };

            // Reflection ile OrderBy veya OrderByDescending metodunu çağırıyoruz
            var orderByMethod = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                .Where(m => m.GetParameters().Length == 2)
                .Single();

            var genericMethod = orderByMethod.MakeGenericMethod(types);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda });
        }

        /// <summary>
        /// Tip üzerinde belirtilen property'yi bulur
        /// </summary>
        private static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            // Case-insensitive karşılaştırma yapıyoruz
            return typeof(T).GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        }
    }
} 