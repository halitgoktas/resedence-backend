using System.Collections.Generic;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Doğrulama hatalarını standart bir formatla döndürmek için kullanılan sınıf
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string Message { get; set; } = "Doğrulama hataları oluştu";

        /// <summary>
        /// Hataların listesi
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        /// <summary>
        /// Yeni bir doğrulama hatası yanıtı oluşturur
        /// </summary>
        public static ValidationErrorResponse Create(string message = null)
        {
            return new ValidationErrorResponse
            {
                Message = message ?? "Doğrulama hataları oluştu"
            };
        }

        /// <summary>
        /// Hata listesi ile yeni bir doğrulama hatası yanıtı oluşturur
        /// </summary>
        public static ValidationErrorResponse Create(List<ValidationError> errors, string message = null)
        {
            return new ValidationErrorResponse
            {
                Message = message ?? "Doğrulama hataları oluştu",
                Errors = errors
            };
        }

        /// <summary>
        /// Yanıta hata ekler
        /// </summary>
        public ValidationErrorResponse AddError(string propertyName, string errorMessage)
        {
            Errors.Add(new ValidationError
            {
                PropertyName = propertyName,
                ErrorMessage = errorMessage
            });

            return this;
        }
    }

    /// <summary>
    /// Tek bir doğrulama hatasını temsil eder
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Hatanın oluştuğu property adı
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string ErrorMessage { get; set; }
    }
} 