using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidenceManagement.Core.Services
{
    /// <summary>
    /// Kullanıcı işlemlerini denetleme (audit) loglama servisi
    /// </summary>
    public interface IAuditLogService
    {
        /// <summary>
        /// Kullanıcı işlemini loglar
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="controller">Controller adı</param>
        /// <param name="action">Action adı</param>
        /// <param name="parameters">İşlem parametreleri</param>
        /// <param name="result">İşlem sonucu</param>
        /// <param name="path">İstek yolu</param>
        Task LogAsync(string userId, string controller, string action, object parameters, object result, string path);

        /// <summary>
        /// Belirli bir kullanıcının işlem loglarını getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="page">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa büyüklüğü</param>
        Task<(IEnumerable<AuditLogDto> Logs, int TotalCount)> GetUserLogsAsync(
            string userId, 
            DateTime startDate, 
            DateTime endDate, 
            int page = 1, 
            int pageSize = 20);

        /// <summary>
        /// Belirli bir action için işlem loglarını getirir
        /// </summary>
        /// <param name="controller">Controller adı</param>
        /// <param name="action">Action adı</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="page">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa büyüklüğü</param>
        Task<(IEnumerable<AuditLogDto> Logs, int TotalCount)> GetActionLogsAsync(
            string controller,
            string action,
            DateTime startDate,
            DateTime endDate,
            int page = 1,
            int pageSize = 20);
    }

    /// <summary>
    /// Denetim logu için DTO
    /// </summary>
    public class AuditLogDto
    {
        /// <summary>
        /// Log ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string UserName { get; set; }

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
    }
} 