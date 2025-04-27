using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    /// <summary>
    /// KBS (Kimlik Bildirme Sistemi) entegrasyonu için kullanılan ayarlar sınıfı
    /// </summary>
    public class KbsIntegrationSettings : BaseEntity
    {
        /// <summary>
        /// KBS servis URL'i
        /// </summary>
        public string ServiceUrl { get; set; }
        
        /// <summary>
        /// KBS kullanıcı adı
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// KBS şifresi (şifrelenmiş olarak saklanır)
        /// </summary>
        public string EncryptedPassword { get; set; }
        
        /// <summary>
        /// KBS tarafından sağlanan API anahtarı
        /// </summary>
        public string ApiKey { get; set; }
        
        /// <summary>
        /// Tesisatın KBS sistemindeki benzersiz kimliği
        /// </summary>
        public string FacilityCode { get; set; }
        
        /// <summary>
        /// Konaklama tesisi türü (Otel, Apart, Rezidans, vb.)
        /// </summary>
        public string FacilityType { get; set; }
        
        /// <summary>
        /// Tesisatın KBS sistemine kayıtlı resmi adı
        /// </summary>
        public string RegisteredFacilityName { get; set; }
        
        /// <summary>
        /// KBS sistemine kayıtlı tesisat adresi
        /// </summary>
        public string RegisteredAddress { get; set; }
        
        /// <summary>
        /// KBS sisteminde kayıtlı ilçe
        /// </summary>
        public string RegisteredDistrict { get; set; }
        
        /// <summary>
        /// KBS sisteminde kayıtlı il
        /// </summary>
        public string RegisteredCity { get; set; }
        
        /// <summary>
        /// KBS bildirimleri için bağlı olunan kolluk kuvveti (Polis/Jandarma)
        /// </summary>
        public string LawEnforcementType { get; set; }
        
        /// <summary>
        /// Bağlı olunan kolluk kuvveti şubesi
        /// </summary>
        public string LawEnforcementBranch { get; set; }
        
        /// <summary>
        /// KBS sistemine bağlantı zaman aşımı süresi (saniye)
        /// </summary>
        public int ConnectionTimeout { get; set; }
        
        /// <summary>
        /// Otomatik gönderimler aktif mi?
        /// </summary>
        public bool AutoSubmitEnabled { get; set; }
        
        /// <summary>
        /// Otomatik gönderim için asgari dakika beklemesi
        /// </summary>
        public int AutoSubmitDelayMinutes { get; set; }
        
        /// <summary>
        /// KBS için günlük giriş ve çıkış bildirimi en son gönderim zamanı
        /// </summary>
        public DateTime? LastDailySubmissionTime { get; set; }
        
        /// <summary>
        /// KBS bildirimi ile ilgili son hata mesajı
        /// </summary>
        public string LastErrorMessage { get; set; }
        
        /// <summary>
        /// Son hata tarihi
        /// </summary>
        public DateTime? LastErrorDate { get; set; }
        
        /// <summary>
        /// KBS entegrasyonu aktif mi?
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// Test modu aktif mi? (Test modunda gerçek KBS'ye bildirim gönderilmez)
        /// </summary>
        public bool TestModeEnabled { get; set; }
        
        /// <summary>
        /// KBS sistemi sertifikası için parmak izi
        /// </summary>
        public string CertificateThumbprint { get; set; }
        
        /// <summary>
        /// KBS sistemi için izin verilen maksimum günlük bildirim sayısı
        /// </summary>
        public int MaxDailySubmissions { get; set; }
        
        /// <summary>
        /// Mevcut günlük bildirim sayısı
        /// </summary>
        public int CurrentDailySubmissionCount { get; set; }
        
        /// <summary>
        /// İlgili ayarları son güncelleyen kullanıcı ID'si
        /// </summary>
        public Guid? LastUpdatedByUserId { get; set; }
        
        /// <summary>
        /// Ayarları son güncelleyen kullanıcı adı
        /// </summary>
        public string LastUpdatedByUserName { get; set; }
        
        /// <summary>
        /// KBS sistemi ile ilgili notlar
        /// </summary>
        public string Notes { get; set; }

        public KbsIntegrationSettings()
        {
            ConnectionTimeout = 60; // Varsayılan 60 saniye
            AutoSubmitEnabled = true;
            AutoSubmitDelayMinutes = 15; // Varsayılan 15 dakika
            IsEnabled = false; // Varsayılan olarak kapalı
            TestModeEnabled = true; // Varsayılan olarak test modu açık
            MaxDailySubmissions = 1000; // Varsayılan maksimum 1000 bildirim
            CurrentDailySubmissionCount = 0;
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        
        /// <summary>
        /// Günlük bildirim sayacını günün başında sıfırlar
        /// </summary>
        public void ResetDailySubmissionCount()
        {
            DateTime today = DateTime.Today;
            
            if (LastDailySubmissionTime == null || LastDailySubmissionTime.Value.Date < today)
            {
                CurrentDailySubmissionCount = 0;
                UpdatedDate = DateTime.Now;
            }
        }
        
        /// <summary>
        /// Bildirim gönderildiğinde sayaç ve son gönderim zamanını günceller
        /// </summary>
        public void IncrementSubmissionCount()
        {
            CurrentDailySubmissionCount++;
            LastDailySubmissionTime = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Hata durumunu kaydeder
        /// </summary>
        /// <param name="errorMessage">Hata mesajı</param>
        public void LogError(string errorMessage)
        {
            LastErrorMessage = errorMessage;
            LastErrorDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Günlük bildirim limitine ulaşıldı mı?
        /// </summary>
        public bool IsDailyLimitReached()
        {
            ResetDailySubmissionCount(); // Önce günü kontrol et ve gerekirse sıfırla
            return CurrentDailySubmissionCount >= MaxDailySubmissions;
        }
        
        /// <summary>
        /// Ayarları güncelleyen kullanıcı bilgilerini ayarlar
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <param name="userName">Kullanıcı adı</param>
        public void SetLastUpdatedBy(Guid userId, string userName)
        {
            LastUpdatedByUserId = userId;
            LastUpdatedByUserName = userName;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Test modunu açar veya kapatır
        /// </summary>
        /// <param name="isTestMode">Test modu durumu</param>
        public void SetTestMode(bool isTestMode)
        {
            TestModeEnabled = isTestMode;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// KBS entegrasyonunu açar veya kapatır
        /// </summary>
        /// <param name="isEnabled">Etkinleştirme durumu</param>
        public void EnableIntegration(bool isEnabled)
        {
            IsEnabled = isEnabled;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// KBS bağlantı bilgilerinin geçerli olduğunu kontrol eder
        /// </summary>
        public bool HasValidConnectionSettings()
        {
            return !string.IsNullOrEmpty(ServiceUrl) && 
                   !string.IsNullOrEmpty(Username) && 
                   !string.IsNullOrEmpty(EncryptedPassword) && 
                   !string.IsNullOrEmpty(FacilityCode);
        }
    }
} 