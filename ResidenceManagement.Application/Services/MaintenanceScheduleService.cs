using ResidenceManagement.Application.DTOs;
using ResidenceManagement.Application.Interfaces;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ResidenceManagement.Application.Services
{
    /// <summary>
    /// Bakım çizelgesi servisi
    /// </summary>
    public class MaintenanceScheduleService : IMaintenanceScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaintenanceScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<int> AddScheduleAsync(MaintenanceScheduleDTO maintenanceScheduleDto)
        {
            if (maintenanceScheduleDto == null)
                throw new ArgumentNullException(nameof(maintenanceScheduleDto));

            var maintenanceSchedule = _mapper.Map<MaintenanceSchedule>(maintenanceScheduleDto);
            
            _unitOfWork.MaintenanceScheduleRepository.Add(maintenanceSchedule);
            await _unitOfWork.SaveChangesAsync();

            return maintenanceSchedule.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateScheduleAsync(MaintenanceScheduleDTO maintenanceScheduleDto)
        {
            if (maintenanceScheduleDto == null)
                throw new ArgumentNullException(nameof(maintenanceScheduleDto));

            var existingSchedule = await _unitOfWork.MaintenanceScheduleRepository.GetByIdAsync(maintenanceScheduleDto.Id);
            if (existingSchedule == null)
                return false;

            _mapper.Map(maintenanceScheduleDto, existingSchedule);
            
            _unitOfWork.MaintenanceScheduleRepository.Update(existingSchedule);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var maintenanceSchedule = await _unitOfWork.MaintenanceScheduleRepository.GetByIdAsync(id);
            if (maintenanceSchedule == null)
                return false;

            _unitOfWork.MaintenanceScheduleRepository.Delete(maintenanceSchedule);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MaintenanceScheduleDTO>> GetAllSchedulesAsync(int firmaId, int subeId)
        {
            var schedules = await _unitOfWork.MaintenanceScheduleRepository.GetAllAsync(
                predicate: ms => ms.FirmaId == firmaId && ms.SubeId == subeId);
            
            return _mapper.Map<IEnumerable<MaintenanceScheduleDTO>>(schedules);
        }

        /// <inheritdoc/>
        public async Task<MaintenanceScheduleDTO> GetScheduleByIdAsync(int id, int firmaId, int subeId)
        {
            var schedule = await _unitOfWork.MaintenanceScheduleRepository.GetFirstOrDefaultAsync(
                predicate: ms => ms.Id == id && ms.FirmaId == firmaId && ms.SubeId == subeId);
            
            return _mapper.Map<MaintenanceScheduleDTO>(schedule);
        }

        /// <inheritdoc/>
        public async Task<bool> CompleteScheduleAsync(int id, DateTime completionDate, string notes, decimal? actualCost)
        {
            var schedule = await _unitOfWork.MaintenanceScheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                return false;

            schedule.Status = "Tamamlandı";
            schedule.CompletionDate = completionDate;
            schedule.Notes = notes;
            schedule.ActualCost = actualCost;
            schedule.CompletionPercentage = 100;
            schedule.UpdatedAt = DateTime.Now;

            _unitOfWork.MaintenanceScheduleRepository.Update(schedule);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> CancelScheduleAsync(int id, string cancelReason)
        {
            var schedule = await _unitOfWork.MaintenanceScheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                return false;

            schedule.Status = "İptal Edildi";
            schedule.Notes = cancelReason;
            schedule.UpdatedAt = DateTime.Now;

            _unitOfWork.MaintenanceScheduleRepository.Update(schedule);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProgressAsync(int id, int completionPercentage, string notes)
        {
            if (completionPercentage < 0 || completionPercentage > 100)
                throw new ArgumentOutOfRangeException(nameof(completionPercentage), "Tamamlanma yüzdesi 0-100 arasında olmalıdır");

            var schedule = await _unitOfWork.MaintenanceScheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                return false;

            schedule.CompletionPercentage = completionPercentage;
            schedule.Notes = notes;
            schedule.UpdatedAt = DateTime.Now;
            
            if (completionPercentage == 100)
            {
                schedule.Status = "Tamamlandı";
                schedule.CompletionDate = DateTime.Now;
            }
            else if (completionPercentage > 0)
            {
                schedule.Status = "Devam Ediyor";
            }

            _unitOfWork.MaintenanceScheduleRepository.Update(schedule);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
} 