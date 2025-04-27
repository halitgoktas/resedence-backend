using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs.Maintenance;

namespace ResidenceManagement.Core.Interfaces
{
    // Bakım süreci için servis arayüzü
    public interface IMaintenanceService
    {
        // Bakım takvimi listeleme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesAsync(
            DateTime? startDate = null, 
            DateTime? endDate = null,
            int? residenceId = null,
            int? blockId = null,
            string maintenanceType = null,
            string status = null);
            
        // Bakım görevi atama
        Task<bool> AssignMaintenanceAsync(
            int maintenanceId, int assignedToUserId, string assignedToUserName, string notes);
            
        // Bakım tamamlama
        Task<bool> CompleteMaintenanceAsync(
            int maintenanceId, string completionNotes, decimal? actualCost, int? actualDurationMinutes);
            
        // Bakım iptal etme
        Task<bool> CancelMaintenanceAsync(int maintenanceId, string cancellationReason);
            
        // Bakım kontrol listesi öğelerini güncelleme
        Task<bool> UpdateMaintenanceChecklistAsync(
            int maintenanceId, List<MaintenanceChecklistItemDTO> checklistItems);
            
        // Tekrarlanan bir bakım planından yeni örnek oluşturma
        Task<int> CreateRecurrenceInstanceAsync(int maintenanceId);
            
        // Apartmana bağlı bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId);
            
        // Kullanıcıya atanmış bakım planlarını getirme
        Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByAssignedUserAsync(int userId);
            
        // Yaklaşan bakım planları raporunu getirme
        Task<MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days = 30);
            
        // Bakım maliyet raporunu getirme
        Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(
            DateTime startDate, DateTime endDate, int? residenceId);
    }
} 