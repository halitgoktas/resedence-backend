using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Logging
{
    /// <summary>
    /// Kullanıcı işlemleri denetim logu entity
    /// </summary>
    public class AuditLog : BaseEntity
    {
        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime LogDate { get; set; }
        
        /// <summary>
        /// Controller adı
        /// </summary>
        public string Controller { get; set; }
        
        /// <summary>
        /// Action adı
        /// </summary>
        public string Action { get; set; }
        
        /// <summary>
        /// İstek yolu
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// İşlem parametreleri (JSON formatında)
        /// </summary>
        public string Parameters { get; set; }
        
        /// <summary>
        /// İşlem sonucu (JSON formatında)
        /// </summary>
        public string Result { get; set; }
        
        /// <summary>
        /// İstemci IP adresi
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// İstemci tarayıcı bilgisi
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// İşlemin durumu (Başarılı/Başarısız)
        /// </summary>
        public bool IsSuccessful { get; set; }
        
        /// <summary>
        /// İşlem süresi (ms)
        /// </summary>
        public long ExecutionTime { get; set; }
        
        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public AuditLog()
        {
            LogDate = DateTime.UtcNow;
            IsSuccessful = true;
        }
    }
} 