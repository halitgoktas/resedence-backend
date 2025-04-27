using System;

namespace ResidenceManagement.Core.Helpers
{
    /// <summary>
    /// Tip dönüşümleri için yardımcı metotlar
    /// </summary>
    public static class ConversionHelpers
    {
        /// <summary>
        /// String değeri nullable int'e dönüştürür
        /// </summary>
        public static int? ToNullableInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return int.TryParse(value, out int result) ? result : null;
        }
        
        /// <summary>
        /// Nullable int değerini int'e dönüştürür
        /// </summary>
        public static int ToInt(this int? value)
        {
            return value ?? 0;
        }
        
        /// <summary>
        /// Nullable decimal değerini decimal'e dönüştürür
        /// </summary>
        public static decimal ToDecimal(this decimal? value)
        {
            return value ?? 0m;
        }
        
        /// <summary>
        /// Nullable DateTime değerini DateTime'a dönüştürür
        /// </summary>
        public static DateTime ToDateTime(this DateTime? value)
        {
            return value ?? DateTime.Now;
        }
        
        /// <summary>
        /// Nullable int değerini double'a dönüştürür
        /// </summary>
        public static double ToDouble(this int? value)
        {
            return value ?? 0;
        }
        
        /// <summary>
        /// Nullable double değerini double'a dönüştürür
        /// </summary>
        public static double ToDouble(this double? value)
        {
            return value ?? 0;
        }
        
        /// <summary>
        /// Herhangi bir nesneyi güvenli bir şekilde string'e dönüştürür
        /// </summary>
        public static string ToSafeString(this object value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
} 