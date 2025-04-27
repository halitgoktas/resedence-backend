using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    // KBS log kayıt sınıfı - KBS işlemleri için log kayıtlarını temsil eder
    public class KbsLog : BaseEntity
    {
        // Bağlı olduğu KBS bildirim ID
        public int KbsNotificationId { get; set; }
        
        // İlişkili KBS bildirimi
        public virtual KbsNotification KbsNotification { get; set; }
        
        // Log tipi
        public KbsLogType LogType { get; set; }
        
        // Log seviyesi
        public LogLevel LogLevel { get; set; }
        
        // Log oluşturulma tarihi ve saati
        public DateTime LogDate { get; set; }
        
        // İşlem başlangıç zamanı
        public DateTime StartTime { get; set; }
        
        // İşlem bitiş zamanı
        public DateTime? EndTime { get; set; }
        
        // İşlem süresi (milisaniye)
        public int? Duration { get; set; }
        
        // Log mesajı
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
        
        // İşlem açıklaması
        [StringLength(1000)]
        public string Details { get; set; }
        
        // İşlemi gerçekleştiren kullanıcı ID
        public int? UserId { get; set; }
        
        // İşlemi gerçekleştiren kullanıcı adı
        [StringLength(100)]
        public string UserName { get; set; }
        
        // İşlem sonucu
        public KbsOperationResult OperationResult { get; set; }
        
        // HTTP durum kodu (API çağrıları için)
        public int? HttpStatusCode { get; set; }
        
        // HTTP durum kodu (API çağrıları için) - StatusCode alanı
        public int? StatusCode { get; set; }
        
        // İstek URL (API çağrıları için)
        [StringLength(500)]
        public string RequestUrl { get; set; }
        
        // İstek methodu (GET, POST, PUT, DELETE)
        [StringLength(10)]
        public string RequestMethod { get; set; }
        
        // İstek içeriği (JSON formatında)
        public string RequestContent { get; set; }
        
        // İstek içeriği (JSON formatında) - RequestBody alanı
        public string RequestBody { get; set; }
        
        // Yanıt içeriği (JSON formatında)
        public string ResponseContent { get; set; }
        
        // Yanıt içeriği (JSON formatında) - ResponseBody alanı
        public string ResponseBody { get; set; }
        
        // Ek parametreler (JSON formatında)
        public string Parameters { get; set; }
        
        // KBS referans numarası (KBS sisteminden alınan)
        [StringLength(50)]
        public string KbsReferenceNumber { get; set; }
        
        // Hata kodu (varsa)
        [StringLength(50)]
        public string ErrorCode { get; set; }
        
        // KBS Hata kodu (varsa)
        [StringLength(50)]
        public string KbsErrorCode { get; set; }
        
        // Hata mesajı (varsa)
        [StringLength(500)]
        public string ErrorMessage { get; set; }
        
        // İstek süresi (milisaniye cinsinden)
        public int? RequestDuration { get; set; }

        // IP adresi
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        // Tarayıcı bilgisi
        [StringLength(250)]
        public string UserAgent { get; set; }
        
        // Bildirim durumu (işlem sonrası)
        public KbsLogNotificationStatus? NotificationStatus { get; set; }
        
        // Yapılandırıcı metod
        public KbsLog()
        {
            LogDate = DateTime.Now;
            StartTime = DateTime.Now;
            LogLevel = LogLevel.Information;
            LogType = KbsLogType.General;
            OperationResult = KbsOperationResult.Unknown;
        }
        
        // Başarılı log oluşturan statik metod
        public static KbsLog CreateSuccessLog(int kbsNotificationId, string message, KbsLogType logType, int? userId = null, string userName = null)
        {
            return new KbsLog
            {
                KbsNotificationId = kbsNotificationId,
                Message = message,
                LogType = logType,
                LogLevel = LogLevel.Information,
                OperationResult = KbsOperationResult.Success,
                UserId = userId,
                UserName = userName
            };
        }
        
        // Hata logu oluşturan statik metod
        public static KbsLog CreateErrorLog(int kbsNotificationId, string message, string errorMessage, KbsLogType logType, int? userId = null, string userName = null)
        {
            return new KbsLog
            {
                KbsNotificationId = kbsNotificationId,
                Message = message,
                ErrorMessage = errorMessage,
                LogType = logType,
                LogLevel = LogLevel.Error,
                OperationResult = KbsOperationResult.Failed,
                UserId = userId,
                UserName = userName
            };
        }
        
        // API çağrısı logu oluşturan statik metod
        public static KbsLog CreateApiCallLog(
            int kbsNotificationId, 
            string requestUrl, 
            string requestMethod, 
            string requestContent, 
            string responseContent, 
            int? httpStatusCode,
            int? requestDuration,
            KbsOperationResult result,
            string errorMessage = null)
        {
            return new KbsLog
            {
                KbsNotificationId = kbsNotificationId,
                LogType = KbsLogType.ApiCall,
                LogLevel = result == KbsOperationResult.Success ? LogLevel.Information : LogLevel.Error,
                Message = $"KBS API çağrısı: {requestMethod} {requestUrl}",
                RequestUrl = requestUrl,
                RequestMethod = requestMethod,
                RequestContent = requestContent,
                ResponseContent = responseContent,
                HttpStatusCode = httpStatusCode,
                RequestDuration = requestDuration,
                OperationResult = result,
                ErrorMessage = errorMessage
            };
        }
        
        // İşlemi tamamlama metodu
        public void CompleteOperation(int? statusCode = null, string responseBody = null, string kbsReferenceNumber = null)
        {
            EndTime = DateTime.Now;
            Duration = EndTime.HasValue ? (int)(EndTime.Value - StartTime).TotalMilliseconds : null;
            StatusCode = statusCode;
            ResponseBody = responseBody;
            KbsReferenceNumber = kbsReferenceNumber;
        }
        
        // Hata bilgisi ekleme metodu
        public void SetError(string errorMessage, string kbsErrorCode = null, int? statusCode = null)
        {
            LogType = KbsLogType.Error;
            ErrorMessage = errorMessage;
            KbsErrorCode = kbsErrorCode;
            StatusCode = statusCode;
            EndTime = DateTime.Now;
            Duration = (int)(EndTime.Value - StartTime).TotalMilliseconds;
        }
        
        // İstek ve yanıt JSON'larının temizlenmiş versiyonunu almak için kullanılır
        // (hassas bilgileri maskeleyebilir)
        public (string sanitizedRequest, string sanitizedResponse) GetSanitizedData()
        {
            // Hassas verileri maskele (örn: TC kimlik, pasaport numaraları)
            string sanitizedRequest = RequestBody;
            string sanitizedResponse = ResponseBody;
            
            // Burada maskeleme işlemleri yapılabilir
            
            return (sanitizedRequest, sanitizedResponse);
        }
        
        // Parametreleri JSON formatından nesneye dönüştürür
        public T GetParameters<T>() where T : class
        {
            if (string.IsNullOrEmpty(Parameters))
                return null;
                
            return JsonSerializer.Deserialize<T>(Parameters);
        }
        
        // Parametreleri nesne olarak kaydeder
        public void SetParameters<T>(T parameters) where T : class
        {
            Parameters = JsonSerializer.Serialize(parameters);
        }
    }
    
    // KBS log tipi
    public enum KbsLogType
    {
        General = 0,        // Genel log
        Creation = 1,       // Bildirim oluşturma
        Update = 2,         // Bildirim güncelleme
        Submission = 3,     // KBS'ye gönderim
        Cancellation = 4,   // Bildirim iptal
        Verification = 5,   // Bildirim doğrulama
        ApiCall = 6,        // API çağrısı
        GuestOperation = 7, // Misafir işlemleri
        StatusChange = 8,   // Durum değişikliği
        Error = 9           // Hata
    }
    
    // Log seviyesi
    public enum LogLevel
    {
        Debug = 0,       // Hata ayıklama
        Information = 1, // Bilgi
        Warning = 2,     // Uyarı
        Error = 3,       // Hata
        Critical = 4     // Kritik hata
    }
    
    // KBS işlem sonucu
    public enum KbsOperationResult
    {
        Unknown = 0,  // Bilinmiyor
        Success = 1,  // Başarılı
        Failed = 2,   // Başarısız
        Pending = 3   // Beklemede
    }
    
    // KBS bildirim durumu
    public enum KbsLogNotificationStatus
    {
        Draft = 0,           // Taslak
        Pending = 1,         // Beklemede
        Submitted = 2,       // Gönderildi
        Failed = 3,          // Başarısız
        Verified = 4,        // Doğrulandı
        Rejected = 5,        // Reddedildi
        Cancelled = 6,       // İptal edildi
        PartiallyVerified = 7 // Kısmen doğrulandı
    }
} 