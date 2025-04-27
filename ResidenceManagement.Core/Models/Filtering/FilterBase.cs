namespace ResidenceManagement.Core.Models.Filtering
{
    /// <summary>
    /// Tüm filtre sınıfları için temel sınıf
    /// </summary>
    public abstract class FilterBase
    {
        /// <summary>
        /// Filtreleme operatörü türü
        /// </summary>
        public enum FilterOperator
        {
            /// <summary>
            /// Eşittir
            /// </summary>
            Equals,
            
            /// <summary>
            /// Eşit değildir
            /// </summary>
            NotEquals,
            
            /// <summary>
            /// İçerir
            /// </summary>
            Contains,
            
            /// <summary>
            /// İle başlar
            /// </summary>
            StartsWith,
            
            /// <summary>
            /// İle biter
            /// </summary>
            EndsWith,
            
            /// <summary>
            /// Büyüktür
            /// </summary>
            GreaterThan,
            
            /// <summary>
            /// Büyük eşittir
            /// </summary>
            GreaterThanOrEqual,
            
            /// <summary>
            /// Küçüktür
            /// </summary>
            LessThan,
            
            /// <summary>
            /// Küçük eşittir
            /// </summary>
            LessThanOrEqual
        }
    }
} 