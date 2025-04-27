using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.DTOs.Maintenance;

// Alias tanımlamaları
using MaintenanceDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO;
using MaintenanceDetailDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDetailDTO;
using MaintenanceCreateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleCreateDTO;
using MaintenanceUpdateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleUpdateDTO;
using MaintenanceDocDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceDocumentDTO;
using MaintenanceChkListDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceChecklistItemDTO;
using MaintenanceReportDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceReportDTO;
using MaintenanceCostRptDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceCostReportDTO;
using MaintenanceLogDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceLogDTO;

namespace ResidenceManagement.Core.Services.Interfaces
{
    // Bakım zamanlaması için servis arayüzü
    public interface IMaintenanceScheduleService
    {
        // Bakım planlarını getirme metotları
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesAsync(
            DateTime? startDate, DateTime? endDate, int? residenceId, int? blockId, 
            string maintenanceType, string status);
        
        Task<MaintenanceDetailDTO> GetMaintenanceScheduleByIdAsync(int id);
        
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByEquipmentAsync(int equipmentId);
        
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId);
        
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByAssignedUserAsync(int userId);

        // Bakım planı CRUD işlemleri
        Task<int> CreateMaintenanceScheduleAsync(MaintenanceCreateDTO model);
        
        Task<bool> UpdateMaintenanceScheduleAsync(MaintenanceUpdateDTO model);
        
        Task<bool> DeleteMaintenanceScheduleAsync(int id);

        // Bakım planı durum işlemleri
        Task<bool> UpdateMaintenanceStatusAsync(int id, string status, string notes);
        
        Task<bool> AssignMaintenanceAsync(int id, int assignedToUserId, string assignedToUserName, string notes);
        
        Task<bool> CompleteMaintenanceAsync(
            int id, string completionNotes, decimal? actualCost, int? actualDurationMinutes);
        
        Task<bool> CancelMaintenanceAsync(int id, string cancellationReason);

        // Bakım planı kontrol listesi işlemleri
        Task<bool> UpdateMaintenanceChecklistAsync(int id, List<MaintenanceChkListDTO> checklistItems);

        // Bakım planı belge işlemleri
        Task<int> AddMaintenanceDocumentAsync(int id, MaintenanceDocDTO document);

        // Bakım log işlemleri
        Task<List<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int maintenanceId);

        // Tekrarlı bakım planlama işlemleri
        Task<int> CreateRecurrenceInstanceAsync(int maintenanceId);

        // Bakım planları raporlama
        Task<MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days);
        
        Task<MaintenanceCostRptDTO> GetMaintenanceCostReportAsync(
            DateTime startDate, DateTime endDate, int? residenceId);
    }
} 