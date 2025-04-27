using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.KBS
{
    /// <summary>
    /// KBS (Kimlik Bildirme Sistemi) bildirim işlemleri için log sınıfı
    /// </summary>
    public class KbsIdentityLog : BaseEntity
    {
        /// <summary>
        /// İşlem benzersiz numarası
        /// </summary>
        public string TransactionId { get; set; }
        
        /// <summary>
        /// İlgili KBS bildirimi ID'si
        /// </summary>
        public Guid KbsNotificationId { get; set; }
        
        /// <summary>
        /// İlgili KBS bildirimi numarası
        /// </summary>
        public string NotificationNumber { get; set; }
        
        /// <summary>
        /// KBS sisteminde verilen referans numarası
        /// </summary>
        public string KbsReferenceNumber { get; set; }
        
        /// <summary>
        /// İşlem türü (Giriş, Çıkış, Güncelleme, İptal vb.)
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// İşlem zamanı
        /// </summary>
        public DateTime OperationTime { get; set; }
        
        /// <summary>
        /// İşlem durumu (Başarılı, Başarısız, Beklemede vb.)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Bildirim gönderme işlemi başarılı oldu mu?
        /// </summary>
        public bool IsSuccessful { get; set; }
        
        /// <summary>
        /// Otomatik olarak mı gönderildi?
        /// </summary>
        public bool IsAutomatic { get; set; }
        
        /// <summary>
        /// Test modunda mı gönderildi?
        /// </summary>
        public bool IsTestMode { get; set; }
        
        /// <summary>
        /// KBS sistemine gönderilen istek bilgileri (JSON formatında)
        /// </summary>
        public string RequestPayload { get; set; }
        
        /// <summary>
        /// KBS sisteminden dönen yanıt (JSON formatında)
        /// </summary>
        public string ResponsePayload { get; set; }
        
        /// <summary>
        /// KBS sisteminden dönen cevap kodu 
        /// </summary>
        public string ResponseCode { get; set; }
        
        /// <summary>
        /// KBS sisteminden dönen mesaj
        /// </summary>
        public string ResponseMessage { get; set; }
        
        /// <summary>
        /// Hata durumunda oluşan hata mesajı
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Oluşan hata türü
        /// </summary>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// Talep gönderim IP adresi
        /// </summary>
        public string ClientIpAddress { get; set; }
        
        /// <summary>
        /// Hata detayları (stack trace vb.)
        /// </summary>
        public string ErrorDetails { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı ID'si
        /// </summary>
        public Guid? UserId { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// İlgili rezervasyon ID'si (varsa)
        /// </summary>
        public Guid? ReservationId { get; set; }
        
        /// <summary>
        /// İlgili daire ID'si (varsa)
        /// </summary>
        public Guid? ApartmentId { get; set; }
        
        /// <summary>
        /// Misafir/kiracı T.C. Kimlik numarası veya pasaport numarası
        /// </summary>
        public string IdentificationNumber { get; set; }
        
        /// <summary>
        /// KBS sistemine gönderim süresi (milisaniye)
        /// </summary>
        public int ProcessingTimeMs { get; set; }
        
        /// <summary>
        /// İşlem sonrası notlar
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Yeni bir KBS bildirim log kaydı oluşturur
        /// </summary>
        public KbsIdentityLog()
        {
            TransactionId = Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();
            OperationTime = DateTime.Now;
            Status = "Başlatıldı";
            IsSuccessful = false;
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        
        /// <summary>
        /// İşlem tamamlandığında başarı durumunu günceller
        /// </summary>
        /// <param name="isSuccessful">Başarılı mı?</param>
        /// <param name="responseCode">Yanıt kodu</param>
        /// <param name="responseMessage">Yanıt mesajı</param>
        /// <param name="processingTimeMs">İşlem süresi (milisaniye)</param>
        public void CompleteOperation(bool isSuccessful, string responseCode, string responseMessage, int processingTimeMs)
        {
            IsSuccessful = isSuccessful;
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            ProcessingTimeMs = processingTimeMs;
            Status = isSuccessful ? "Tamamlandı" : "Başarısız";
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// İşlem sırasında oluşan hatayı kaydeder
        /// </summary>
        /// <param name="errorType">Hata türü</param>
        /// <param name="errorMessage">Hata mesajı</param>
        /// <param name="errorDetails">Hata detayları</param>
        public void LogError(string errorType, string errorMessage, string errorDetails = null)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
            Status = "Başarısız";
            IsSuccessful = false;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// İşlemi yapan kullanıcı bilgilerini ayarlar
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <param name="userName">Kullanıcı adı</param>
        public void SetUser(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// İşlemi bekleyen durumuna alır
        /// </summary>
        public void SetPending()
        {
            Status = "Beklemede";
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// İşlemi işleniyor durumuna alır
        /// </summary>
        public void SetProcessing()
        {
            Status = "İşleniyor";
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// KBS referans numarasını ayarlar
        /// </summary>
        /// <param name="referenceNumber">KBS referans numarası</param>
        public void SetKbsReferenceNumber(string referenceNumber)
        {
            KbsReferenceNumber = referenceNumber;
            UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// İstek ve yanıt bilgilerini ayarlar
        /// </summary>
        /// <param name="requestPayload">İstek verisi (JSON)</param>
        /// <param name="responsePayload">Yanıt verisi (JSON)</param>
        public void SetPayloads(string requestPayload, string responsePayload)
        {
            RequestPayload = requestPayload;
            ResponsePayload = responsePayload;
            UpdatedDate = DateTime.Now;
        }
    }
} 