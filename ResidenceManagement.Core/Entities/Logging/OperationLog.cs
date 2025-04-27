using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Logging
{
    /// <summary>
    /// Sistem operasyonları için log entity
    /// </summary>
    public class OperationLog : BaseEntity
    {
        /// <summary>
        /// Operasyon tipi
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// Log mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı ID
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// Operasyon tarihi
        /// </summary>
        public DateTime LogDate { get; set; }
        
        /// <summary>
        /// Operasyon detayları (JSON formatında)
        /// </summary>
        public string Details { get; set; }
        
        /// <summary>
        /// Log seviyesi (Info, Warning, Error, Critical)
        /// </summary>
        public LogLevel LogLevel { get; set; }
        
        /// <summary>
        /// İşlemi yapan makine
        /// </summary>
        public string MachineName { get; set; }
        
        /// <summary>
        /// İşlemi yapan process ID
        /// </summary>
        public string ProcessId { get; set; }
        
        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public OperationLog()
        {
            LogDate = DateTime.UtcNow;
            LogLevel = LogLevel.Information;
        }
    }
    
    /// <summary>
    /// Log seviyesi
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Bilgilendirme
        /// </summary>
        Information = 0,
        
        /// <summary>
        /// Uyarı
        /// </summary>
        Warning = 1,
        
        /// <summary>
        /// Hata
        /// </summary>
        Error = 2,
        
        /// <summary>
        /// Kritik hata
        /// </summary>
        Critical = 3
    }
} 