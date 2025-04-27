using System.Collections.Generic;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// API yanıtları için genel sınıf
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mesaj
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// HTTP durum kodu
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Hata kodu (başarısız işlemlerde)
        /// </summary>
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Veri (object tipinde)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Hata listesi
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        
        /// <summary>
        /// Başarılı bir yanıt oluşturur
        /// </summary>
        public static ApiResponse CreateSuccess(string message = "İşlem başarılı", object data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = 200,
                Data = data
            };
        }
        
        /// <summary>
        /// Başarısız bir yanıt oluşturur
        /// </summary>
        public static ApiResponse CreateFail(string message, int statusCode, int errorCode, List<string> errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                ErrorCode = errorCode,
                Errors = errors ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// Generic veri tipi için API yanıt sınıfı
    /// </summary>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Tipli veri
        /// </summary>
        public new T Data { get; set; }
        
        /// <summary>
        /// Başarılı bir tipli yanıt oluşturur
        /// </summary>
        public static ApiResponse<T> CreateSuccess(T data, string message = "İşlem başarılı")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = 200,
                Data = data
            };
        }
        
        /// <summary>
        /// Başarısız bir tipli yanıt oluşturur
        /// </summary>
        public static ApiResponse<T> CreateFail(string message, int statusCode, int errorCode, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                ErrorCode = errorCode,
                Errors = errors ?? new List<string>(),
                Data = default
            };
        }
    }
} 