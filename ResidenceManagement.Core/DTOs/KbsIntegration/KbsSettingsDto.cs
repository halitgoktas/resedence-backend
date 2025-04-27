using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.KbsIntegration
{
    /// <summary>
    /// KBS entegrasyonu için ayarlar DTO'su
    /// </summary>
    public class KbsSettingsDto
    {
        /// <summary>
        /// KBS Web Servis URL'i
        /// </summary>
        [Required(ErrorMessage = "KBS Web Servis URL'i gereklidir")]
        public string ServiceUrl { get; set; }
        
        /// <summary>
        /// KBS Kullanıcı Adı
        /// </summary>
        [Required(ErrorMessage = "KBS Kullanıcı Adı gereklidir")]
        public string Username { get; set; }
        
        /// <summary>
        /// KBS Şifresi
        /// </summary>
        [Required(ErrorMessage = "KBS Şifresi gereklidir")]
        public string Password { get; set; }
        
        /// <summary>
        /// Tesis Kodu
        /// </summary>
        [Required(ErrorMessage = "Tesis Kodu gereklidir")]
        public string TesisKodu { get; set; }
        
        /// <summary>
        /// İşletme/Otel Adı
        /// </summary>
        [Required(ErrorMessage = "İşletme/Otel Adı gereklidir")]
        public string IsletmeAdi { get; set; }
        
        /// <summary>
        /// MERSIS No
        /// </summary>
        public string MersisNo { get; set; }
        
        /// <summary>
        /// Bağlantı zaman aşımı (saniye)
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;
        
        /// <summary>
        /// Maksimum deneme sayısı
        /// </summary>
        public int MaxRetryCount { get; set; } = 3;
        
        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
    }
} 