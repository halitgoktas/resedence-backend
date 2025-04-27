using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs.Maintenance;

namespace ResidenceManagement.Core.Interfaces
{
    // Bakım planlaması servis arayüzü
    public interface IMaintenanceScheduleService
    {
        // Bakım planlarını listeleme
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
        
        // Bakım planı durumunu güncelleme
        Task<bool> UpdateMaintenanceStatusAsync(int id, string status, string notes);
        
        // Bakım planını atama
        Task<bool> AssignMaintenanceAsync(int id, int assignedToUserId, string assignedToUserName, string notes);
        
        // Bakım planını tamamlama
        Task<bool> CompleteMaintenanceAsync(int id, string completionNotes, decimal? actualCost, int? actualDurationMinutes);
        
        // Bakım planını iptal etme
        Task<bool> CancelMaintenanceAsync(int id, string reason);
        
        // Bakım kontrol listesini güncelleme
        Task<bool> UpdateChecklistAsync(int id, List<MaintenanceChecklistItemDTO> checklistItems);
        
        // Bakım planına belge ekleme
        Task<int> AddDocumentAsync(int maintenanceId, MaintenanceDocumentDTO documentDto);
        
        // Bakım loglarını getirme
        Task<List<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int maintenanceId);
        
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
        
        // Bakım maliyet raporunu getirme
        Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(DateTime startDate, DateTime endDate, int? residenceId);
    }
} 