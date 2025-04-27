using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs.Maintenance;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Interfaces.Services
{
    // Bakım planlaması için servis arayüzü
    public interface IMaintenanceScheduleService
    {
        // Bakım planlarını filtreleme ve listeleme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? residenceId = null,
            int? blockId = null,
            string maintenanceType = null,
            string status = null);

        // Belirli bir bakım planını getirme
        Task<MaintenanceScheduleDetailDTO> GetMaintenanceScheduleByIdAsync(int id);

        // Yeni bakım planı oluşturma
        Task<int> CreateMaintenanceScheduleAsync(MaintenanceScheduleCreateDTO createDto);

        // Mevcut bakım planını güncelleme
        Task<bool> UpdateMaintenanceScheduleAsync(MaintenanceScheduleUpdateDTO updateDto);

        // Bakım planını silme
        Task<bool> DeleteMaintenanceScheduleAsync(int id);

        // Bakım için kontrol listesini getirme
        Task<List<MaintenanceChecklistItemDTO>> GetMaintenanceChecklistAsync(int maintenanceId);

        // Bakım için kontrol listesini güncelleme
        Task<bool> UpdateChecklistAsync(int maintenanceId, List<MaintenanceChecklistItemDTO> checklistItems);

        // Bakım planına belge ekleme
        Task<int> AddDocumentAsync(int maintenanceId, MaintenanceDocumentDTO documentDto);

        // Bakım loglarını getirme
        Task<List<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int maintenanceId);

        // Bakım raporunu getirme
        Task<MaintenanceReportDTO> GetMaintenanceReportAsync(int maintenanceId);

        // Bakım maliyet raporunu getirme
        Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(DateTime startDate, DateTime endDate, int? residenceId);

        // Bakım durum güncellemesi
        Task<bool> UpdateMaintenanceStatusAsync(int id, string status, string notes = null);

        // Bakımı çalışana atama
        Task<bool> AssignMaintenanceAsync(int id, int assignedToUserId, string assignedToUserName, string notes);

        // Bakım tamamlama
        Task<bool> CompleteMaintenanceAsync(int id, string completionNotes, decimal? actualCost, int? actualDurationMinutes);

        // Bakım iptal etme
        Task<bool> CancelMaintenanceAsync(int id, string reason);

        // Ekipmana göre bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByEquipmentAsync(int equipmentId);

        // Daireye göre bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId);

        // Kullanıcıya göre bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByUserAsync(int userId);

        // Tekrarlayan bakım için yeni örnek oluşturma
        Task<int> CreateRecurrenceInstanceAsync(int maintenanceId);

        // Yaklaşan bakım raporunu getirme
        Task<MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days);

        // Bakım maliyet raporunu getir (tekil bakım planı için)
        Task<MaintenanceCostReportDTO> GetMaintenanceCostReportByIdAsync(int maintenanceId);

        // Bakım kontrol listesi güncelleme metodu
        Task<ApiResponse> UpdateMaintenanceChecklistAsync(int scheduleId, MaintenanceChecklistUpdateDTO model);

        // Bakım dokümanı ekleme metodu
        Task<ApiResponse> AddMaintenanceDocumentAsync(int scheduleId, MaintenanceDocumentCreateDTO model);

        // Atanmış kullanıcıya göre bakım planlarını getirme
        Task<ApiResponse<List<MaintenanceScheduleListDTO>>> GetMaintenanceSchedulesByAssignedUserAsync(int userId);
    }
} 