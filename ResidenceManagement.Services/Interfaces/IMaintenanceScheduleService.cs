using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs;

namespace ResidenceManagement.Services.Interfaces
{
    public interface IMaintenanceScheduleService
    {
        // Bakım takvimi listeleme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesAsync(
            DateTime? startDate = null, 
            DateTime? endDate = null,
            int? residenceId = null,
            int? blockId = null,
            string maintenanceType = null,
            string status = null);

        // Bakım planı detayını alma
        Task<MaintenanceScheduleDetailDTO> GetMaintenanceScheduleByIdAsync(int id);

        // Yeni bakım planı oluşturma
        Task<int> CreateMaintenanceScheduleAsync(MaintenanceScheduleCreateDTO model);

        // Bakım planı güncelleme
        Task<bool> UpdateMaintenanceScheduleAsync(MaintenanceScheduleUpdateDTO model);

        // Bakım planı silme
        Task<bool> DeleteMaintenanceScheduleAsync(int id);

        // Bakım durumu güncelleme
        Task<bool> UpdateMaintenanceStatusAsync(int id, string status, string notes);

        // Bakım görevini atama
        Task<bool> AssignMaintenanceAsync(int id, int assignedToUserId, string assignedToUserName, string notes);

        // Bakım tamamlama
        Task<bool> CompleteMaintenanceAsync(int id, string completionNotes, decimal? actualCost, int? actualDurationMinutes);

        // Bakım iptal etme
        Task<bool> CancelMaintenanceAsync(int id, string cancellationReason);

        // Bakım kontrol listesi öğelerini güncelleme
        Task<bool> UpdateMaintenanceChecklistAsync(int id, List<MaintenanceChecklistItemDTO> checklistItems);

        // Bakım belgeleri ekleme
        Task<int> AddMaintenanceDocumentAsync(int id, MaintenanceDocumentCreateDTO model);

        // Bakım aktivite loglarını alma
        Task<List<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int id);

        // Tekrarlanan bir bakım planından yeni örnek oluşturma
        Task<int> CreateRecurrenceInstanceAsync(int id);

        // Ekipmana bağlı bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByEquipmentAsync(int equipmentId);

        // Apartmana bağlı bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId);

        // Kullanıcıya atanmış bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByAssignedUserAsync(int userId);

        // Yaklaşan bakım planları raporunu getirme
        Task<MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days = 30);

        // Bakım maliyet raporunu getirme
        Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(DateTime startDate, DateTime endDate, int? residenceId);
    }
} 