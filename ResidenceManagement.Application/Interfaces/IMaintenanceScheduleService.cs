using ResidenceManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidenceManagement.Application.Interfaces
{
    /// <summary>
    /// Bakım çizelgesi servisi interface
    /// </summary>
    public interface IMaintenanceScheduleService
    {
        /// <summary>
        /// Bakım çizelgesi ekler
        /// </summary>
        /// <param name="maintenanceSchedule">Eklenecek bakım çizelgesi</param>
        /// <returns>Eklenen bakım çizelgesinin ID'si</returns>
        Task<int> AddScheduleAsync(MaintenanceScheduleDTO maintenanceSchedule);
        
        /// <summary>
        /// Bakım çizelgesini günceller
        /// </summary>
        /// <param name="maintenanceSchedule">Güncellenecek bakım çizelgesi</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> UpdateScheduleAsync(MaintenanceScheduleDTO maintenanceSchedule);
        
        /// <summary>
        /// Bakım çizelgesini siler
        /// </summary>
        /// <param name="id">Silinecek bakım çizelgesi ID'si</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> DeleteScheduleAsync(int id);
        
        /// <summary>
        /// Tüm bakım çizelgelerini getirir
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>Bakım çizelgeleri listesi</returns>
        Task<IEnumerable<MaintenanceScheduleDTO>> GetAllSchedulesAsync(int firmaId, int subeId);
        
        /// <summary>
        /// ID'ye göre bakım çizelgesi getirir
        /// </summary>
        /// <param name="id">Bakım çizelgesi ID'si</param>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>Bakım çizelgesi</returns>
        Task<MaintenanceScheduleDTO> GetScheduleByIdAsync(int id, int firmaId, int subeId);
        
        /// <summary>
        /// Bakım çizelgesini tamamlar
        /// </summary>
        /// <param name="id">Bakım çizelgesi ID'si</param>
        /// <param name="completionDate">Tamamlanma tarihi</param>
        /// <param name="notes">Notlar</param>
        /// <param name="actualCost">Gerçek maliyet</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> CompleteScheduleAsync(int id, DateTime completionDate, string notes, decimal? actualCost);
        
        /// <summary>
        /// Bakım çizelgesini iptal eder
        /// </summary>
        /// <param name="id">Bakım çizelgesi ID'si</param>
        /// <param name="cancelReason">İptal sebebi</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> CancelScheduleAsync(int id, string cancelReason);
        
        /// <summary>
        /// Bakım çizelgesinin ilerleme durumunu günceller
        /// </summary>
        /// <param name="id">Bakım çizelgesi ID'si</param>
        /// <param name="completionPercentage">Tamamlanma yüzdesi</param>
        /// <param name="notes">Notlar</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> UpdateProgressAsync(int id, int completionPercentage, string notes);
    }
} 