using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidenceManagement.Core.Services
{
    /// <summary>
    /// Sistem operasyonları için loglama servisi
    /// </summary>
    public interface IOperationLogService
    {
        /// <summary>
        /// Operasyon logunu kaydeder
        /// </summary>
        /// <param name="operationType">Operasyon tipi</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="details">Operasyon detayları (opsiyonel)</param>
        Task LogAsync(string operationType, string message, object details = null);
        
        /// <summary>
        /// Operasyon logunu kullanıcı bilgisiyle kaydeder
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="operationType">Operasyon tipi</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="details">Operasyon detayları (opsiyonel)</param>
        Task LogAsync(int userId, string operationType, string message, object details = null);
        
        /// <summary>
        /// Operasyon loglarını belirli filtrelerle getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="operationType">Operasyon tipi (opsiyonel)</param>
        /// <param name="userId">Kullanıcı ID (opsiyonel)</param>
        /// <param name="page">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa büyüklüğü</param>
        Task<(IEnumerable<OperationLogDto> Logs, int TotalCount)> GetLogsAsync(
            DateTime startDate, 
            DateTime endDate, 
            string operationType = null, 
            int? userId = null, 
            int page = 1, 
            int pageSize = 20);
            
        /// <summary>
        /// Hata logunu kaydeder
        /// </summary>
        /// <param name="exception">Hata</param>
        /// <param name="message">Hata mesajı</param>
        /// <param name="userId">Kullanıcı ID (opsiyonel)</param>
        Task LogErrorAsync(Exception exception, string message, int? userId = null);
    }
    
    /// <summary>
    /// Operasyon logu için DTO
    /// </summary>
    public class OperationLogDto
    {
        /// <summary>
        /// Log ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Operasyon tarihi
        /// </summary>
        public DateTime Timestamp { get; set; }
        
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
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Operasyon detayları (JSON formatında)
        /// </summary>
        public string Details { get; set; }
    }
} 