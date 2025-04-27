using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResidenceManagement.Core.Models.Filtering;
using ResidenceManagement.Core.Models.Pagination;
using ResidenceManagement.Core.Models.Sorting;

namespace ResidenceManagement.Core.Extensions
{
    /// <summary>
    /// IQueryable sınıfları için uzantı metotları
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sorguya sayfalama uygular
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="query">Temel sorgu</param>
        /// <param name="parameters">Sayfalama parametreleri</param>
        /// <returns>Sayfalanmış sorgu</returns>
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PaginationParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
                
            return query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                       .Take(parameters.PageSize);
        }

        /// <summary>
        /// Sorguya sayfalama uygular ve sayfalanan veriyi döner
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="query">Temel sorgu</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <returns>Sayfalanmış veri ve toplam kayıt sayısı</returns>
        public static async Task<(List<T> Items, int TotalCount)> ToPagedListAsync<T>(
            this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalCount = await query.CountAsync();
            
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
                                  
            return (items, totalCount);
        }

        /// <summary>
        /// Sorguya sıralama uygular
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="query">Temel sorgu</param>
        /// <param name="parameters">Sıralama parametreleri</param>
        /// <returns>Sıralanmış sorgu</returns>
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, SortingParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
                
            if (string.IsNullOrWhiteSpace(parameters.SortBy))
                return query;

            // Dinamik olarak sıralama için property info alınır
            var propertyInfo = typeof(T).GetProperty(parameters.SortBy, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                
            if (propertyInfo == null)
                return query; // Eğer belirtilen alan bulunamazsa, sıralama yapılmaz

            // Dinamik sıralama için expression oluşturulur
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            // OrderBy veya OrderByDescending metodunu dinamik olarak çağırır
            string methodName = parameters.SortAscending ? "OrderBy" : "OrderByDescending";
            var orderByMethod = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                .Single();
                
            var elementType = propertyInfo.PropertyType;
            var genericMethod = orderByMethod.MakeGenericMethod(typeof(T), elementType);
            
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda });
        }

        /// <summary>
        /// Sorguya tekli filtre uygular
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <typeparam name="TValue">Filtre değeri tipi</typeparam>
        /// <param name="query">Temel sorgu</param>
        /// <param name="filter">Uygulanacak filtre</param>
        /// <returns>Filtrelenmiş sorgu</returns>
        public static IQueryable<T> ApplyFilter<T, TValue>(this IQueryable<T> query, Filter<TValue> filter)
        {
            if (filter == null)
                return query;
                
            if (string.IsNullOrWhiteSpace(filter.Field))
                return query;

            // Dinamik olarak filtreleme için property info alınır
            var propertyInfo = typeof(T).GetProperty(filter.Field, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                
            if (propertyInfo == null)
                return query; // Eğer belirtilen alan bulunamazsa, filtreleme yapılmaz

            // Dinamik filtreleme için expression oluşturulur
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            
            // Filtre ifadesi oluşturulur
            Expression comparisonExpression;
            
            // String tipindeki özel filtreler için
            if (typeof(TValue) == typeof(string) && propertyInfo.PropertyType == typeof(string))
            {
                var value = (string)(object)filter.Value;
                
                switch (filter.Operator)
                {
                    case FilterBase.FilterOperator.Contains:
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        comparisonExpression = Expression.Call(property, containsMethod, Expression.Constant(value));
                        break;
                        
                    case FilterBase.FilterOperator.StartsWith:
                        var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                        comparisonExpression = Expression.Call(property, startsWithMethod, Expression.Constant(value));
                        break;
                        
                    case FilterBase.FilterOperator.EndsWith:
                        var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                        comparisonExpression = Expression.Call(property, endsWithMethod, Expression.Constant(value));
                        break;
                        
                    default:
                        comparisonExpression = CreateComparisonExpression(property, filter.Operator, Expression.Constant(filter.Value));
                        break;
                }
            }
            else
            {
                comparisonExpression = CreateComparisonExpression(property, filter.Operator, Expression.Constant(filter.Value));
            }
            
            var lambda = Expression.Lambda<Func<T, bool>>(comparisonExpression, parameter);
            
            return query.Where(lambda);
        }

        /// <summary>
        /// Sorguya filtre koleksiyonu uygular
        /// </summary>
        /// <typeparam name="T">Veri tipi</typeparam>
        /// <param name="query">Temel sorgu</param>
        /// <param name="filterCollection">Uygulanacak filtre koleksiyonu</param>
        /// <returns>Filtrelenmiş sorgu</returns>
        public static IQueryable<T> ApplyFilterCollection<T>(this IQueryable<T> query, FilterCollection filterCollection)
        {
            if (filterCollection == null || filterCollection.Count == 0)
                return query;

            // Filtre kombinasyonları için dinamik lambda oluşturmak için
            var parameter = Expression.Parameter(typeof(T), "x");
            
            // Koleksiyondaki tüm filtreleri işle
            Expression combinedExpression = null;
            
            foreach (var filter in filterCollection.Filters)
            {
                Expression currentExpression = null;
                
                // Eğer alt koleksiyon ise
                if (filter is FilterCollection nestedCollection)
                {
                    var nestedQuery = query.ApplyFilterCollection(nestedCollection);
                    var nestedExpression = GetFilterExpression<T>(nestedQuery);
                    
                    if (nestedExpression != null)
                    {
                        currentExpression = nestedExpression;
                    }
                }
                // Eğer tekil filtre ise
                else if (filter.GetType().IsGenericType && filter.GetType().GetGenericTypeDefinition() == typeof(Filter<>))
                {
                    // Reflection ile Filter<T> üzerinde ApplyFilter metodunu çağır
                    var filterType = filter.GetType();
                    var valueType = filterType.GetGenericArguments()[0];
                    
                    var methodInfo = typeof(QueryableExtensions).GetMethod("ApplyFilter")
                        .MakeGenericMethod(typeof(T), valueType);
                        
                    var filteredQuery = (IQueryable<T>)methodInfo.Invoke(null, new object[] { query, filter });
                    var filterExpression = GetFilterExpression<T>(filteredQuery);
                    
                    if (filterExpression != null)
                    {
                        currentExpression = filterExpression;
                    }
                }
                
                // İfadeleri kombine et
                if (currentExpression != null)
                {
                    if (combinedExpression == null)
                    {
                        combinedExpression = currentExpression;
                    }
                    else
                    {
                        combinedExpression = filterCollection.Operator == FilterCollection.LogicalOperator.And
                            ? Expression.AndAlso(combinedExpression, currentExpression)
                            : Expression.OrElse(combinedExpression, currentExpression);
                    }
                }
            }
            
            if (combinedExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                return query.Where(lambda);
            }
            
            return query;
        }

        /// <summary>
        /// Karşılaştırma ifadesi oluşturur
        /// </summary>
        private static Expression CreateComparisonExpression(Expression property, FilterBase.FilterOperator filterOperator, Expression constant)
        {
            switch (filterOperator)
            {
                case FilterBase.FilterOperator.Equals:
                    return Expression.Equal(property, constant);
                    
                case FilterBase.FilterOperator.NotEquals:
                    return Expression.NotEqual(property, constant);
                    
                case FilterBase.FilterOperator.GreaterThan:
                    return Expression.GreaterThan(property, constant);
                    
                case FilterBase.FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(property, constant);
                    
                case FilterBase.FilterOperator.LessThan:
                    return Expression.LessThan(property, constant);
                    
                case FilterBase.FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(property, constant);
                    
                default:
                    return Expression.Equal(property, constant);
            }
        }

        /// <summary>
        /// Sorgudan filtre ifadesini çıkarır
        /// </summary>
        private static Expression GetFilterExpression<T>(IQueryable<T> query)
        {
            if (!(query.Expression is MethodCallExpression methodCallExpr))
                return null;
                
            if (methodCallExpr.Method.Name != "Where")
                return null;
                
            var lambdaExpr = methodCallExpr.Arguments[1] as UnaryExpression;
            if (lambdaExpr == null)
                return null;
                
            var lambda = lambdaExpr.Operand as LambdaExpression;
            return lambda?.Body;
        }
    }
} 