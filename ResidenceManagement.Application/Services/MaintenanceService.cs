using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResidenceManagement.Application.Services
{
    /// <summary>
    /// Bakım işlemleri servisi
    /// </summary>
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaintenanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Bakım kaydı ekler
        /// </summary>
        /// <param name="maintenance">Bakım bilgisi</param>
        /// <returns>Eklenen bakım kaydının ID'si</returns>
        public async Task<int> AddMaintenanceAsync(Maintenance maintenance)
        {
            if (maintenance == null)
                throw new ArgumentNullException(nameof(maintenance));

            _unitOfWork.MaintenanceRepository.Add(maintenance);
            await _unitOfWork.SaveChangesAsync();

            return maintenance.Id;
        }

        /// <summary>
        /// Bakım kaydını günceller
        /// </summary>
        /// <param name="maintenance">Güncellenecek bakım bilgisi</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<bool> UpdateMaintenanceAsync(Maintenance maintenance)
        {
            if (maintenance == null)
                throw new ArgumentNullException(nameof(maintenance));

            _unitOfWork.MaintenanceRepository.Update(maintenance);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Bakım kaydını siler
        /// </summary>
        /// <param name="id">Silinecek bakım ID'si</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<bool> DeleteMaintenanceAsync(int id)
        {
            var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
            if (maintenance == null)
                return false;

            _unitOfWork.MaintenanceRepository.Delete(maintenance);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Tüm bakım kayıtlarını getirir
        /// </summary>
        /// <returns>Bakım kayıtları listesi</returns>
        public async Task<IEnumerable<Maintenance>> GetAllMaintenanceAsync()
        {
            return await _unitOfWork.MaintenanceRepository.GetAllAsync();
        }

        /// <summary>
        /// ID'ye göre bakım kaydı getirir
        /// </summary>
        /// <param name="id">Bakım ID'si</param>
        /// <returns>Bakım kaydı</returns>
        public async Task<Maintenance> GetMaintenanceByIdAsync(int id)
        {
            return await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        }
    }
} 