using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.DTOs.Maintenance;
using ResidenceManagement.Core.Filters;

// Alias tanımlamaları
using MaintenanceDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO;
using MaintenanceDetailDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDetailDTO;
using MaintenanceCreateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleCreateDTO;
using MaintenanceUpdateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleUpdateDTO;
using MaintenanceDocDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceDocumentDTO;
using MaintenanceLogDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceLogDTO;
using MaintenanceRptDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceReportDTO;
using MaintenanceCostRptDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceCostReportDTO;

namespace ResidenceManagement.Core.Services
{
    // Bakım çizelgesi servis arayüzü
    public interface IMaintenanceScheduleService
    {
        // Tüm bakım çizelgelerini filtreleyerek getirir
        Task<PaginatedResultDto<MaintenanceDTO>> GetAllMaintenanceSchedulesAsync(
            MaintenanceScheduleFilterDTO filter);
            
        // Belirli bir bakım çizelgesini ID'ye göre getirir
        Task<MaintenanceDetailDTO> GetMaintenanceScheduleByIdAsync(int id);
        
        // Ekipman ID'sine göre bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByEquipmentAsync(int equipmentId);
        
        // Konuma göre bakım çizelgelerini getirir (Site, Blok, Daire)
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByLocationAsync(
            int? residenceId = null, int? blockId = null, int? apartmentId = null);
            
        // Belirli bir tarih aralığındaki bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByDateRangeAsync(
            DateTime startDate, DateTime endDate);
            
        // Duruma göre bakım çizelgelerini getirir (Planlandı, Devam Ediyor, Tamamlandı, vb.)
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByStatusAsync(string status);
        
        // Görevlendirilen kişiye göre bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByAssignedUserAsync(int userId);
        
        // Yeni bir bakım çizelgesi oluşturur
        Task<int> CreateMaintenanceScheduleAsync(MaintenanceCreateDTO scheduleDto, int userId);
        
        // Mevcut bakım çizelgesini günceller
        Task<bool> UpdateMaintenanceScheduleAsync(MaintenanceUpdateDTO scheduleDto);
        
        // Bakım çizelgesini siler (genellikle soft delete)
        Task<bool> DeleteMaintenanceScheduleAsync(int id, int userId);
        
        // Bakım çizelgesinin durumunu günceller
        Task<bool> UpdateMaintenanceStatusAsync(int id, string newStatus, int userId, string notes = "");
        
        // Bakım çizelgesinin kontrol listesi öğesini günceller
        Task<bool> UpdateChecklistItemAsync(int itemId, bool isCompleted, int userId, string notes = "");
        
        // Bakım çizelgesine kontrol listesi öğesi ekler
        Task<int> AddChecklistItemAsync(int scheduleId, string description);
        
        // Bakım çizelgesinden kontrol listesi öğesini siler
        Task<bool> RemoveChecklistItemAsync(int itemId);
        
        // Bakım çizelgesine döküman ekler
        Task<int> AddDocumentAsync(int scheduleId, MaintenanceDocDTO document, int userId);
        
        // Bakım çizelgesinden döküman siler
        Task<bool> RemoveDocumentAsync(int documentId, int userId);
        
        // Bakım çizelgesini başka bir kullanıcıya atar
        Task<bool> AssignMaintenanceToUserAsync(int scheduleId, int userId, int assignedByUserId);
        
        // Bakım çizelgesinin tamamlanmasını işler
        Task<bool> CompleteMaintenanceAsync(
            int scheduleId, 
            int userId, 
            string notes, 
            int? actualDurationMinutes = null, 
            decimal? actualCost = null);
            
        // Bakım çizelgesinin iptalini işler
        Task<bool> CancelMaintenanceAsync(int scheduleId, int userId, string reason);
        
        // Yinelenen bakım çizelgeleri oluşturur
        Task<List<int>> CreateRecurringMaintenanceSchedulesAsync(
            MaintenanceCreateDTO baseSchedule, 
            int userId);
            
        // Belirli tarihte yaklaşan bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetUpcomingMaintenanceSchedulesAsync(
            DateTime date, int daysAhead);
            
        // Geçmiş bakım çizelgelerini ekipman için getirir
        Task<List<MaintenanceDTO>> GetMaintenanceHistoryForEquipmentAsync(
            int equipmentId, DateTime? startDate = null, DateTime? endDate = null);
            
        // Bakım çizelgesi raporunu oluşturur
        Task<MaintenanceRptDTO> GenerateMaintenanceReportAsync(
            DateTime startDate, DateTime endDate, string reportType = "all");
            
        // Bakım maliyet raporunu oluşturur
        Task<MaintenanceCostRptDTO> GenerateMaintenanceCostReportAsync(
            DateTime startDate, DateTime endDate);
            
        // Gecikmiş bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetOverdueMaintenanceSchedulesAsync();
        
        // Ekipman grubu için bakım çizelgelerini getirir
        Task<List<MaintenanceDTO>> GetMaintenanceSchedulesByEquipmentCategoryAsync(
            string categoryName);
    }
} 