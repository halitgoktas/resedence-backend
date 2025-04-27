using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    // KBS Bildirim Logu sınıfı - KBS bildirimleri için yapılan işlemlerin kayıtlarını tutar
    public class KbsNotificationLog : BaseEntity
    {
        // İlişkili KBS bildirim ID'si
        public int KbsNotificationId { get; set; }
        
        // İşlem tipi
        public KbsLogActionType ActionType { get; set; }
        
        // İşlem tarihi
        public DateTime ActionDate { get; set; }
        
        // İşlemi yapan kullanıcı ID'si
        public int? UserId { get; set; }
        
        // İşlemi yapan kullanıcı adı
        [StringLength(100)]
        public string UserName { get; set; }
        
        // İşlem açıklaması
        [StringLength(500)]
        public string Description { get; set; }
        
        // İşlem sonucunda oluşan durum
        [StringLength(50)]
        public string ResultStatus { get; set; }
        
        // İşlem sırasında oluşan hata mesajı (varsa)
        [StringLength(500)]
        public string ErrorMessage { get; set; }
        
        // KBS sisteminden dönen yanıt kodu
        [StringLength(20)]
        public string ResponseCode { get; set; }
        
        // KBS sisteminden dönen yanıt mesajı
        [StringLength(500)]
        public string ResponseMessage { get; set; }
        
        // İşlem için gönderilen veri (JSON formatında)
        public string RequestData { get; set; }
        
        // KBS sisteminden dönen veri (JSON formatında)
        public string ResponseData { get; set; }
        
        // İşlemin IP adresi
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        // İşlemin başarılı olup olmadığı
        public bool IsSuccess { get; set; }
        
        // İşlem süresi (milisaniye)
        public int? ProcessDuration { get; set; }
        
        // Geri referans - İlişkili KBS bildirimi
        public virtual KbsNotification KbsNotification { get; set; }
        
        // Yapılandırıcı metod
        public KbsNotificationLog()
        {
            ActionDate = DateTime.Now;
            IsSuccess = false; // Varsayılan olarak başarısız olarak işaretlenir, işlem sonunda güncellenir
        }
        
        // İşlemin başarılı olduğunu kaydetmek için metod
        public void MarkAsSuccess(string responseCode = null, string responseMessage = null)
        {
            IsSuccess = true;
            ResponseCode = responseCode;
            ResponseMessage = responseMessage ?? "İşlem başarıyla tamamlandı";
            ResultStatus = "Başarılı";
        }
        
        // İşlemin başarısız olduğunu kaydetmek için metod
        public void MarkAsFailed(string errorMessage, string responseCode = null, string responseMessage = null)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ResultStatus = "Başarısız";
        }
        
        // İşlem süresini hesaplamak için başlangıç zamanı
        private DateTime? _startTime;
        
        // İşlem süresini ölçmeye başlama metodu
        public void StartProcessTimer()
        {
            _startTime = DateTime.Now;
        }
        
        // İşlem süresini ölçmeyi bitirme ve süreyi kaydetme metodu
        public void EndProcessTimer()
        {
            if (_startTime.HasValue)
            {
                ProcessDuration = (int)(DateTime.Now - _startTime.Value).TotalMilliseconds;
            }
        }
        
        // Log detaylarını formatlanmış string olarak döndürme metodu
        public override string ToString()
        {
            return $"{ActionDate:dd.MM.yyyy HH:mm:ss} - {ActionType} - {(IsSuccess ? "Başarılı" : "Başarısız")} - {Description}";
        }
    }
    
    // KBS log işlem tipleri
    public enum KbsLogActionType
    {
        Create = 0,           // Bildirim oluşturma
        Update = 1,           // Bildirim güncelleme
        Submit = 2,           // KBS'ye gönderme
        Resubmit = 3,         // KBS'ye tekrar gönderme
        Cancel = 4,           // Bildirimi iptal etme
        Delete = 5,           // Bildirimi silme
        AddGuest = 6,         // Misafir ekleme
        RemoveGuest = 7,      // Misafir çıkarma
        UpdateGuest = 8,      // Misafir bilgilerini güncelleme
        StatusChange = 9,     // Durum değişikliği
        CheckOut = 10,        // Çıkış işlemi
        ExtendStay = 11,      // Konaklama süresini uzatma
        VerifyIdentity = 12,  // Kimlik doğrulama
        SystemError = 13,     // Sistem hatası
        Other = 14            // Diğer işlemler
    }
} 