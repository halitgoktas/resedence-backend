using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.DTOs.Equipment;
using ResidenceManagement.Core.Models.Services;

// Alias tanımlamaları
using EquipmentListItemDTO = ResidenceManagement.Core.DTOs.Equipment.EquipmentListItemDto;
using EquipmentFilterDTO = ResidenceManagement.Core.DTOs.Equipment.EquipmentFilterDto;
using EquipmentMaintenanceResultDTO = ResidenceManagement.Core.DTOs.Equipment.MaintenanceResultDto;

namespace ResidenceManagement.Core.Services
{
    // Demirbaş ve ekipman takibi için servis arabirimi
    public interface IEquipmentInventoryService
    {
        // Temel CRUD işlemleri
        Task<PaginatedResultDto<EquipmentListItemDTO>> GetAllEquipmentsAsync(EquipmentFilterDTO filter);
        Task<EquipmentDetailDto> GetEquipmentByIdAsync(int id);
        Task<EquipmentDetailDto> CreateEquipmentAsync(EquipmentCreateDto equipmentDto);
        Task<EquipmentDetailDto> UpdateEquipmentAsync(EquipmentUpdateDto equipmentDto);
        Task<bool> DeleteEquipmentAsync(int id);
        
        // Barkod ile arama
        Task<EquipmentDetailDto> GetEquipmentByBarcodeAsync(string barcode);
        
        // Durum güncelleme
        Task<StatusResultDto> UpdateEquipmentStatusAsync(int id, StatusUpdateDto statusDto);
        
        // Ekipmanı taşıma (yer değiştirme)
        Task<RelocationResultDto> RelocateEquipmentAsync(int id, EquipmentRelocationDto relocationDto);
        
        // Bakım ve servis işlemleri
        Task<EquipmentMaintenanceResultDTO> AddMaintenanceRecordAsync(int id, MaintenanceRecordDto maintenanceDto);
        Task<List<MaintenanceRecordDto>> GetMaintenanceHistoryAsync(int id);
        Task<MaintenanceScheduleResultDto> ScheduleMaintenanceAsync(int id, MaintenanceScheduleDto scheduleDto);
        
        // Doküman ve parça ekleme
        Task<DocumentResultDto> AddDocumentAsync(int id, DocumentDto documentDto);
        Task<PartResultDto> AddPartAsync(int id, PartDto partDto);
        
        // Metrik ve ölçüm güncelleme
        Task<MetricResultDto> UpdateMetricAsync(int id, MetricUpdateDto metricDto);
        
        // Fotoğraf işlemleri
        Task<PhotoResultDto> AddPhotoAsync(int id, PhotoDto photoDto);
        
        // Aktivite logları
        Task<List<ActivityLogEntryDto>> GetActivityLogAsync(int id);
        
        // Raporlar
        Task<List<CategoryReportItemDto>> GetEquipmentsByCategoryAsync();
        Task<List<BuildingReportItemDto>> GetEquipmentsByBuildingAsync();
        Task<List<UpcomingMaintenanceDto>> GetUpcomingMaintenancesAsync(DateTime endDate);
        Task<MaintenanceCostReportDto> GetMaintenanceCostsAsync(DateTime startDate, DateTime endDate);
    }
} 