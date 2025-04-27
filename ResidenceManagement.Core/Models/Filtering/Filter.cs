using System;

namespace ResidenceManagement.Core.Models.Filtering
{
    /// <summary>
    /// Filtreleme için genel sınıf
    /// </summary>
    /// <typeparam name="T">Filtre değerinin tipi</typeparam>
    public class Filter<T> : FilterBase
    {
        /// <summary>
        /// Filtre uygulanacak alan adı
        /// </summary>
        public string Field { get; set; }
        
        /// <summary>
        /// Filtre değeri
        /// </summary>
        public T Value { get; set; }
        
        /// <summary>
        /// Filtre operatörü
        /// </summary>
        public FilterOperator Operator { get; set; } = FilterOperator.Equals;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Filter()
        {
        }
        
        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="field">Alan adı</param>
        /// <param name="value">Değer</param>
        /// <param name="operator">Operatör</param>
        public Filter(string field, T value, FilterOperator @operator = FilterOperator.Equals)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Value = value;
            Operator = @operator;
        }
        
        /// <summary>
        /// Eşittir filtresi oluşturur
        /// </summary>
        public static Filter<T> Equals(string field, T value) => new Filter<T>(field, value, FilterOperator.Equals);
        
        /// <summary>
        /// Eşit değildir filtresi oluşturur
        /// </summary>
        public static Filter<T> NotEquals(string field, T value) => new Filter<T>(field, value, FilterOperator.NotEquals);
        
        /// <summary>
        /// İçerir filtresi oluşturur (string tipinde)
        /// </summary>
        public static Filter<string> Contains(string field, string value) 
            => new Filter<string>(field, value, FilterOperator.Contains);
        
        /// <summary>
        /// İle başlar filtresi oluşturur (string tipinde)
        /// </summary>
        public static Filter<string> StartsWith(string field, string value) 
            => new Filter<string>(field, value, FilterOperator.StartsWith);
        
        /// <summary>
        /// İle biter filtresi oluşturur (string tipinde)
        /// </summary>
        public static Filter<string> EndsWith(string field, string value) 
            => new Filter<string>(field, value, FilterOperator.EndsWith);
        
        /// <summary>
        /// Büyüktür filtresi oluşturur
        /// </summary>
        public static Filter<T> GreaterThan(string field, T value) => new Filter<T>(field, value, FilterOperator.GreaterThan);
        
        /// <summary>
        /// Büyük eşittir filtresi oluşturur
        /// </summary>
        public static Filter<T> GreaterThanOrEqual(string field, T value) => new Filter<T>(field, value, FilterOperator.GreaterThanOrEqual);
        
        /// <summary>
        /// Küçüktür filtresi oluşturur
        /// </summary>
        public static Filter<T> LessThan(string field, T value) => new Filter<T>(field, value, FilterOperator.LessThan);
        
        /// <summary>
        /// Küçük eşittir filtresi oluşturur
        /// </summary>
        public static Filter<T> LessThanOrEqual(string field, T value) => new Filter<T>(field, value, FilterOperator.LessThanOrEqual);
    }
} 