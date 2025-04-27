using System;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS entegrasyonu için ayarlar entity sınıfı
    /// </summary>
    public class KbsSettings : BaseEntity, ITenant
    {
        /// <summary>
        /// KBS Web Servis URL'i
        /// </summary>
        public string ServiceUrl { get; set; }
        
        /// <summary>
        /// KBS Kullanıcı Adı
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// KBS Şifresi
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Tesis Kodu
        /// </summary>
        public string TesisKodu { get; set; }
        
        /// <summary>
        /// İşletme/Otel Adı
        /// </summary>
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
        /// Son başarılı oturum açma tarihi
        /// </summary>
        public DateTime? LastSuccessfulLogin { get; set; }
        
        /// <summary>
        /// Son oturum açma sonucu (hata mesajı veya başarılı)
        /// </summary>
        public string LastLoginResult { get; set; }
        
        /// <summary>
        /// Son KBS talep tarihi
        /// </summary>
        public DateTime? LastRequestDate { get; set; }
        
        /// <summary>
        /// Otomatik gönderim aktif mi?
        /// </summary>
        public bool AutoSendEnabled { get; set; } = false;
        
        /// <summary>
        /// Günlük otomatik gönderim saati (0-23)
        /// </summary>
        public int AutoSendHour { get; set; } = 3; // Varsayılan olarak gece 3'te
        
        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Firma ID (Multi-tenancy için)
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID (Multi-tenancy için)
        /// </summary>
        public int SubeId { get; set; }
    }
} 