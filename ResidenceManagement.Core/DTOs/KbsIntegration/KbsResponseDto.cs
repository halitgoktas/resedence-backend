using System;

namespace ResidenceManagement.Core.DTOs.KbsIntegration
{
    /// <summary>
    /// KBS web servisinden gelen yanıt DTO'su
    /// </summary>
    public class KbsResponseDto
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Hata kodu
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// KBS Referans Numarası
        /// </summary>
        public string ReferenceNumber { get; set; }
        
        /// <summary>
        /// Yanıt detayları
        /// </summary>
        public string ResponseDetails { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
} 