using System;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS bildirim durumlarını temsil eden enum
    /// </summary>
    public enum KbsNotificationStatus
    {
        /// <summary>
        /// Taslak
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Gönderilmeyi Bekliyor
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Gönderildi
        /// </summary>
        Submitted = 2,

        /// <summary>
        /// Başarılı
        /// </summary>
        Success = 3,

        /// <summary>
        /// Hatalı
        /// </summary>
        Failed = 4,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 5
    }

    /// <summary>
    /// KBS bildirim tiplerini temsil eden enum
    /// </summary>
    public enum KbsNotificationType
    {
        /// <summary>
        /// Giriş Bildirimi
        /// </summary>
        CheckIn = 0,

        /// <summary>
        /// Çıkış Bildirimi
        /// </summary>
        CheckOut = 1,

        /// <summary>
        /// Güncelleme Bildirimi
        /// </summary>
        Update = 2
    }

    /// <summary>
    /// KBS log işlem tiplerini temsil eden enum
    /// </summary>
    public enum KbsLogActionType
    {
        /// <summary>
        /// Bildirim Oluşturma
        /// </summary>
        Create = 0,

        /// <summary>
        /// Bildirim Güncelleme
        /// </summary>
        Update = 1,

        /// <summary>
        /// Bildirim Gönderme
        /// </summary>
        Submit = 2,

        /// <summary>
        /// Bildirim İptal
        /// </summary>
        Cancel = 3,

        /// <summary>
        /// Durum Sorgulama
        /// </summary>
        CheckStatus = 4
    }

    /// <summary>
    /// KBS bildirim durumlarını temsil eden enum
    /// </summary>
    public enum KbsNotifyStatus
    {
        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 0,

        /// <summary>
        /// İşleniyor
        /// </summary>
        Processing = 1,

        /// <summary>
        /// Başarılı
        /// </summary>
        Success = 2,

        /// <summary>
        /// Başarısız
        /// </summary>
        Failed = 3
    }

    /// <summary>
    /// Konaklama amacını temsil eden enum
    /// </summary>
    public enum StayPurpose
    {
        /// <summary>
        /// Turizm
        /// </summary>
        Tourism = 0,

        /// <summary>
        /// İş
        /// </summary>
        Business = 1,

        /// <summary>
        /// Eğitim
        /// </summary>
        Education = 2,

        /// <summary>
        /// Sağlık
        /// </summary>
        Health = 3,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 4
    }
} 