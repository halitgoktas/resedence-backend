using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    // KBS Bildirim Log sınıfı - KBS'ye gönderilen bildirimlerin log kayıtlarını tutar
    public class KbsSubmissionLog : BaseEntity
    {
        // İşlem tipi (Gönderim, Güncelleme, İptal, vb.)
        public SubmissionType SubmissionType { get; set; }
        
        // KBS Bildirim ID'si
        public int KbsNotificationId { get; set; }
        
        // Bildirim numarası
        [StringLength(50)]
        public string NotificationNumber { get; set; }
        
        // Bildirim tipi
        public KbsNotificationType NotificationType { get; set; }
        
        // İşlem tarihi
        public DateTime SubmissionDate { get; set; }
        
        // Yanıt alınma tarihi
        public DateTime? ResponseDate { get; set; }
        
        // İşlemi yapan kullanıcı ID'si
        public int SubmittedByUserId { get; set; }
        
        // İşlemi yapan kullanıcı adı
        [StringLength(100)]
        public string SubmittedByUserName { get; set; }
        
        // Gönderilen veriler (JSON formatında)
        [StringLength(4000)]
        public string RequestData { get; set; }
        
        // Alınan yanıt (JSON formatında)
        [StringLength(4000)]
        public string ResponseData { get; set; }
        
        // Durum kodu
        [StringLength(50)]
        public string StatusCode { get; set; }
        
        // Başarılı mı?
        public bool IsSuccessful { get; set; }
        
        // KBS referans numarası
        [StringLength(50)]
        public string KbsReferenceId { get; set; }
        
        // Hata mesajı (eğer varsa)
        [StringLength(500)]
        public string ErrorMessage { get; set; }
        
        // İşlem IP adresi
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        // İşlem süresi (milisaniye)
        public int ProcessingTimeMs { get; set; }
        
        // KBS'nin yanıt verme süresi (milisaniye)
        public int? KbsResponseTimeMs { get; set; }
        
        // İşlem notu
        [StringLength(500)]
        public string Notes { get; set; }
        
        // KBS bildirimi ile ilişki
        public virtual KbsNotification KbsNotification { get; set; }
        
        // Yapıcı metod
        public KbsSubmissionLog()
        {
            SubmissionDate = DateTime.Now;
            IsSuccessful = false;
        }
        
        // Başlama zamanı (performans ölçümü için)
        private DateTime _startTime;
        
        // İşlemi başlatma metodu
        public void StartProcessing()
        {
            _startTime = DateTime.Now;
        }
        
        // İşlemi sonlandırma metodu
        public void EndProcessing(bool isSuccessful, string statusCode, string referenceId = null, string errorMessage = null)
        {
            ResponseDate = DateTime.Now;
            ProcessingTimeMs = (int)(ResponseDate.Value - _startTime).TotalMilliseconds;
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            KbsReferenceId = referenceId;
            ErrorMessage = errorMessage;
        }
        
        // Geçen süreyi hesaplama
        public TimeSpan CalculateElapsedTime()
        {
            if (!ResponseDate.HasValue)
                return DateTime.Now - SubmissionDate;
                
            return ResponseDate.Value - SubmissionDate;
        }
        
        // İşlem tipini metin olarak alma
        public string GetSubmissionTypeText()
        {
            return SubmissionType switch
            {
                SubmissionType.Create => "Yeni Bildirim",
                SubmissionType.Update => "Güncelleme",
                SubmissionType.Cancel => "İptal",
                SubmissionType.CheckOut => "Çıkış",
                SubmissionType.ExtendStay => "Uzatma",
                SubmissionType.Query => "Sorgulama",
                _ => "Diğer"
            };
        }
        
        // İşlem durumunu metin olarak alma
        public string GetStatusText()
        {
            if (!IsSuccessful)
                return $"Başarısız: {ErrorMessage}";
                
            return "Başarılı";
        }
        
        // Başarılı bir log kaydı oluşturur
        public static KbsSubmissionLog CreateSuccessLog(
            int kbsNotificationId, 
            SubmissionType submissionType, 
            string submissionMethod,
            string requestPayload, 
            string responsePayload, 
            string kbsReferenceNumber,
            int? performedByUserId = null,
            string performedByUserName = null,
            string ipAddress = null,
            int? processingTimeMs = null)
        {
            return new KbsSubmissionLog
            {
                KbsNotificationId = kbsNotificationId,
                SubmissionType = submissionType,
                StatusCode = SubmissionStatus.Completed.ToString(),
                RequestData = requestPayload,
                ResponseData = responsePayload,
                IsSuccessful = true,
                KbsReferenceId = kbsReferenceNumber,
                SubmittedByUserId = performedByUserId ?? 0,
                SubmittedByUserName = performedByUserName,
                IpAddress = ipAddress,
                ProcessingTimeMs = processingTimeMs ?? 0
            };
        }
        
        // Başarısız bir log kaydı oluşturur
        public static KbsSubmissionLog CreateErrorLog(
            int kbsNotificationId, 
            SubmissionType submissionType, 
            string submissionMethod,
            string requestPayload, 
            string responsePayload, 
            string errorCode,
            string errorMessage,
            int? performedByUserId = null,
            string performedByUserName = null,
            string ipAddress = null,
            int? processingTimeMs = null)
        {
            return new KbsSubmissionLog
            {
                KbsNotificationId = kbsNotificationId,
                SubmissionType = submissionType,
                StatusCode = SubmissionStatus.Failed.ToString(),
                RequestData = requestPayload,
                ResponseData = responsePayload,
                IsSuccessful = false,
                ErrorMessage = errorMessage,
                SubmittedByUserId = performedByUserId ?? 0,
                SubmittedByUserName = performedByUserName,
                IpAddress = ipAddress,
                ProcessingTimeMs = processingTimeMs ?? 0
            };
        }
    }
    
    // Bildirim İşlem Tipleri
    public enum SubmissionType
    {
        Create = 0,       // Yeni bildirim oluşturma
        Update = 1,       // Bildirim güncelleme
        Cancel = 2,       // Bildirim iptal etme
        CheckOut = 3,     // Çıkış bildirimi
        ExtendStay = 4,   // Konaklama uzatma
        Query = 5,        // Bilgi sorgulama
        Other = 6         // Diğer işlemler
    }
    
    // Gönderim Durumu
    public enum SubmissionStatus
    {
        Pending = 0,            // Beklemede
        InProgress = 1,         // İşlemde
        Completed = 2,          // Tamamlandı
        Failed = 3,             // Başarısız
        Retrying = 4,           // Yeniden Deneniyor
        Cancelled = 5           // İptal Edildi
    }
} 