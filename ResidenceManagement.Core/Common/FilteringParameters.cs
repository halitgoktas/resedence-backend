using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Filtreleme operatörü
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Eşittir (==)
        /// </summary>
        Equals,
        
        /// <summary>
        /// Eşit değildir (!=)
        /// </summary>
        NotEquals,
        
        /// <summary>
        /// Büyüktür (>)
        /// </summary>
        GreaterThan,
        
        /// <summary>
        /// Büyük veya eşittir (>=)
        /// </summary>
        GreaterThanOrEqual,
        
        /// <summary>
        /// Küçüktür (<)
        /// </summary>
        LessThan,
        
        /// <summary>
        /// Küçük veya eşittir (<=)
        /// </summary>
        LessThanOrEqual,
        
        /// <summary>
        /// İçerir (string.Contains)
        /// </summary>
        Contains,
        
        /// <summary>
        /// İle başlar (string.StartsWith)
        /// </summary>
        StartsWith,
        
        /// <summary>
        /// İle biter (string.EndsWith)
        /// </summary>
        EndsWith
    }

    /// <summary>
    /// Tekil filtre parametresi
    /// </summary>
    public class FilterParameter
    {
        /// <summary>
        /// Filtrelenecek alanın adı
        /// </summary>
        public string PropertyName { get; set; }
        
        /// <summary>
        /// Filtreleme operatörü
        /// </summary>
        public FilterOperator Operator { get; set; }
        
        /// <summary>
        /// Filtreleme değeri
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// API filtreleme parametreleri için temel sınıf
    /// </summary>
    public class FilteringParameters
    {
        /// <summary>
        /// Filtreleme parametreleri listesi
        /// </summary>
        public List<FilterParameter> Filters { get; set; } = new List<FilterParameter>();

        /// <summary>
        /// Filtreleme parametresi ekler
        /// </summary>
        /// <param name="propertyName">Alan adı</param>
        /// <param name="operator">Operatör</param>
        /// <param name="value">Değer</param>
        public void AddFilter(string propertyName, FilterOperator @operator, object value)
        {
            Filters.Add(new FilterParameter
            {
                PropertyName = propertyName,
                Operator = @operator,
                Value = value
            });
        }
    }

    /// <summary>
    /// IQueryable üzerinde filtreleme işlemleri yapmak için extension sınıfı
    /// </summary>
    public static class FilteringExtensions
    {
        /// <summary>
        /// IQueryable üzerinde dinamik filtreleme uygular
        /// </summary>
        /// <typeparam name="T">Sorgu tipi</typeparam>
        /// <param name="query">Sorgu</param>
        /// <param name="filteringParams">Filtreleme parametreleri</param>
        /// <returns>Filtrelenmiş sorgu</returns>
        public static IQueryable<T> ApplyFilters<T>(
            this IQueryable<T> query,
            FilteringParameters filteringParams)
        {
            if (filteringParams == null || !filteringParams.Filters.Any())
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combinedExpression = null;

            foreach (var filter in filteringParams.Filters)
            {
                // Filtrelenmek istenen property'nin bilgilerini alıyoruz
                var propertyInfo = typeof(T).GetProperty(filter.PropertyName, 
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                
                if (propertyInfo == null)
                {
                    // Belirtilen özellik bulunamazsa, bu filtreyi atlıyoruz
                    continue;
                }

                try
                {
                    // Filtre değerini uygun tipe dönüştürüyoruz
                    var convertedValue = ConvertValue(filter.Value, propertyInfo.PropertyType);
                    if (convertedValue == null && !IsNullableType(propertyInfo.PropertyType))
                    {
                        // Değer null ama property nullable değilse, bu filtreyi atlıyoruz
                        continue;
                    }

                    // Property erişim ifadesini oluşturuyoruz
                    var property = Expression.Property(parameter, propertyInfo);
                    
                    // Filtre sabit değerini ifade olarak oluşturuyoruz
                    var constant = Expression.Constant(convertedValue, propertyInfo.PropertyType);
                    
                    // Filtre operatörüne göre karşılaştırma ifadesini oluşturuyoruz
                    var filterExpression = CreateFilterExpression(property, filter.Operator, constant);
                    
                    // Tüm filtreler için AND operatörü ile birleştiriyoruz
                    if (combinedExpression == null)
                    {
                        combinedExpression = filterExpression;
                    }
                    else
                    {
                        combinedExpression = Expression.AndAlso(combinedExpression, filterExpression);
                    }
                }
                catch
                {
                    // Hata durumunda bu filtreyi atlıyoruz
                    continue;
                }
            }

            if (combinedExpression == null)
            {
                return query;
            }

            // Lambda ifadesini oluşturuyoruz
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            
            // Where metodu ile sorguyu filtreliyoruz
            return query.Where(lambda);
        }

        /// <summary>
        /// Belirtilen değeri hedef tipe dönüştürür
        /// </summary>
        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // Nullable tip kontrolü
            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType != null)
            {
                targetType = underlyingType;
            }

            // String olarak gelen değeri hedef tipe dönüştürmek için
            if (value is string stringValue)
            {
                if (targetType == typeof(Guid))
                {
                    return Guid.Parse(stringValue);
                }
                else if (targetType == typeof(DateTime))
                {
                    return DateTime.Parse(stringValue);
                }
                else if (targetType == typeof(bool))
                {
                    return bool.Parse(stringValue);
                }
                else if (targetType.IsEnum)
                {
                    return Enum.Parse(targetType, stringValue, true);
                }
            }

            // Normal tip dönüşümü
            return Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// Filtreleme operatörüne göre karşılaştırma ifadesi oluşturur
        /// </summary>
        private static Expression CreateFilterExpression(Expression property, FilterOperator @operator, Expression constant)
        {
            switch (@operator)
            {
                case FilterOperator.Equals:
                    return Expression.Equal(property, constant);
                
                case FilterOperator.NotEquals:
                    return Expression.NotEqual(property, constant);
                
                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(property, constant);
                
                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(property, constant);
                
                case FilterOperator.LessThan:
                    return Expression.LessThan(property, constant);
                
                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(property, constant);
                
                case FilterOperator.Contains:
                    // String.Contains metodunu çağıran bir ifade oluşturuyoruz
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    return Expression.Call(property, containsMethod, constant);
                
                case FilterOperator.StartsWith:
                    // String.StartsWith metodunu çağıran bir ifade oluşturuyoruz
                    var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                    return Expression.Call(property, startsWithMethod, constant);
                
                case FilterOperator.EndsWith:
                    // String.EndsWith metodunu çağıran bir ifade oluşturuyoruz
                    var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                    return Expression.Call(property, endsWithMethod, constant);
                
                default:
                    throw new NotSupportedException($"Operatör desteklenmiyor: {@operator}");
            }
        }

        /// <summary>
        /// Bir tipin nullable olup olmadığını kontrol eder
        /// </summary>
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
} 