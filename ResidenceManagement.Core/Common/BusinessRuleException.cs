using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// İş kuralı ihlallerini yönetmek için özel exception sınıfı
    /// </summary>
    public class BusinessRuleException : Exception
    {
        /// <summary>
        /// Hata mesajları listesi
        /// </summary>
        public List<string> Errors { get; }
        
        /// <summary>
        /// Hata kodu
        /// </summary>
        public int ErrorCode { get; }
        
        /// <summary>
        /// Yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public BusinessRuleException(string message) 
            : base(message)
        {
            ErrorCode = ErrorCodes.BusinessRuleViolation;
            Errors = new List<string>();
        }
        
        /// <summary>
        /// Yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="errors">Hata mesajları listesi</param>
        public BusinessRuleException(string message, List<string> errors) 
            : base(message)
        {
            ErrorCode = ErrorCodes.BusinessRuleViolation;
            Errors = errors ?? new List<string>();
        }
        
        /// <summary>
        /// Yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="innerException">İç istisna</param>
        public BusinessRuleException(string message, Exception innerException) 
            : base(message, innerException)
        {
            ErrorCode = ErrorCodes.BusinessRuleViolation;
            Errors = new List<string>();
        }
        
        /// <summary>
        /// Belirtilen hata kodu ile yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        public BusinessRuleException(int errorCode) 
            : base(ErrorCodes.GetErrorMessage(errorCode))
        {
            ErrorCode = errorCode;
            Errors = new List<string>();
        }
        
        /// <summary>
        /// Belirtilen hata kodu ve özel mesaj ile yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        /// <param name="customMessage">Özel hata mesajı</param>
        public BusinessRuleException(int errorCode, string customMessage) 
            : base(ErrorCodes.GetErrorMessage(errorCode, customMessage))
        {
            ErrorCode = errorCode;
            Errors = new List<string>();
        }
        
        /// <summary>
        /// Belirtilen hata kodu, özel mesaj ve hata listesi ile yeni bir iş kuralı istisnası oluşturur.
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        /// <param name="customMessage">Özel hata mesajı</param>
        /// <param name="errors">Hata mesajları listesi</param>
        public BusinessRuleException(int errorCode, string customMessage, List<string> errors) 
            : base(ErrorCodes.GetErrorMessage(errorCode, customMessage))
        {
            ErrorCode = errorCode;
            Errors = errors ?? new List<string>();
        }
    }
} 