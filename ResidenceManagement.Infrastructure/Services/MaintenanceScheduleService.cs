using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs.Maintenance;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Interfaces.Services;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Infrastructure.Services
{
    // Bakım planlaması için servis sınıfı
    public class MaintenanceScheduleService : IMaintenanceScheduleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MaintenanceScheduleService> _logger;

        // Constructor
        public MaintenanceScheduleService(ApplicationDbContext context, ILogger<MaintenanceScheduleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Bakım planlarını filtreleme ve listeleme
        public async Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? residenceId = null,
            int? blockId = null,
            string maintenanceType = null,
            string status = null)
        {
            try
            {
                var query = _context.MaintenanceSchedules.AsQueryable();

                // Filtreleri uygula
                if (startDate.HasValue)
                {
                    query = query.Where(m => m.ScheduledDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(m => m.ScheduledDate <= endDate.Value);
                }

                if (residenceId.HasValue)
                {
                    query = query.Where(m => m.ResidenceId == residenceId.Value);
                }

                if (blockId.HasValue)
                {
                    query = query.Where(m => m.BlockId == blockId.Value);
                }

                if (!string.IsNullOrEmpty(maintenanceType))
                {
                    query = query.Where(m => m.MaintenanceType == maintenanceType);
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(m => m.Status == status);
                }

                // Sonuçları DTO'ya dönüştür
                var maintenanceSchedules = await query
                    .Select(m => new MaintenanceScheduleDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MaintenanceType = m.MaintenanceType,
                        Priority = m.Priority,
                        ScheduledDate = m.ScheduledDate,
                        EstimatedDurationMinutes = m.EstimatedDuration,
                        Status = m.Status,
                        ResidenceId = m.ResidenceId,
                        BlockId = m.BlockId,
                        ApartmentId = m.ApartmentId,
                        EquipmentId = m.EquipmentId,
                        AssignedToUserId = m.AssignedToUserId,
                        IsRecurring = !string.IsNullOrEmpty(m.RecurrencePattern),
                        EstimatedCost = m.EstimatedCost ?? 0,
                        FirmaId = m.FirmaId,
                        SubeId = m.SubeId
                    })
                    .ToListAsync();

                return maintenanceSchedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planları getirilirken bir hata oluştu");
                throw;
            }
        }

        // Belirli bir bakım planını getirme
        public async Task<MaintenanceScheduleDetailDTO> GetMaintenanceScheduleByIdAsync(int id)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);

                if (maintenance == null)
                {
                    return null;
                }

                var documents = await _context.MaintenanceDocuments
                    .Where(d => d.MaintenanceScheduleId == id)
                    .Select(d => new MaintenanceDocumentDTO
                    {
                        Id = d.Id,
                        MaintenanceScheduleId = d.MaintenanceScheduleId,
                        Title = d.Title,
                        Description = d.Description,
                        DocumentType = d.FileType,
                        FilePath = d.FilePath,
                        UploadDate = d.UploadDate,
                        UploadedBy = d.UploadedBy,
                        FirmaId = d.FirmaId,
                        SubeId = d.SubeId
                    })
                    .ToListAsync();

                var checklists = await _context.MaintenanceChecklistItems
                    .Where(c => c.MaintenanceScheduleId == id)
                    .Select(c => new MaintenanceChecklistItemDTO
                    {
                        Id = c.Id,
                        MaintenanceScheduleId = c.MaintenanceScheduleId,
                        Title = c.Title,
                        Description = c.Description,
                        IsRequired = c.IsRequired,
                        IsCompleted = c.IsCompleted,
                        CompletedAt = c.CompletedDate,
                        CompletedByUserId = c.CompletedByUserId,
                        CompletionNotes = c.Notes,
                        FirmaId = c.FirmaId,
                        SubeId = c.SubeId
                    })
                    .ToListAsync();

                var logs = await _context.MaintenanceLogs
                    .Where(l => l.MaintenanceScheduleId == id)
                    .Select(l => new MaintenanceLogDTO
                    {
                        Id = l.Id,
                        MaintenanceScheduleId = l.MaintenanceScheduleId,
                        LogDate = l.CreatedDate,
                        Activity = l.LogType,
                        Description = l.Description,
                        Notes = l.Notes,
                        PerformedByUserId = l.CreatedBy ?? 0,
                        FirmaId = l.FirmaId,
                        SubeId = l.SubeId
                    })
                    .ToListAsync();

                var detailDto = new MaintenanceScheduleDetailDTO
                {
                    Id = maintenance.Id,
                    Title = maintenance.Title,
                    Description = maintenance.Description,
                    MaintenanceType = maintenance.MaintenanceType,
                    Priority = maintenance.Priority,
                    ScheduledDate = maintenance.ScheduledDate,
                    ScheduledTime = maintenance.ScheduledTime,
                    EndDate = maintenance.EndDate,
                    EstimatedDurationMinutes = maintenance.EstimatedDuration,
                    Status = maintenance.Status,
                    ResidenceId = maintenance.ResidenceId,
                    BlockId = maintenance.BlockId,
                    ApartmentId = maintenance.ApartmentId,
                    EquipmentId = maintenance.EquipmentId,
                    AssignedToUserId = maintenance.AssignedToUserId,
                    ServiceProviderId = maintenance.ServiceProviderId,
                    ServiceProviderName = maintenance.ServiceProviderName,
                    RecurrencePattern = maintenance.RecurrencePattern,
                    RecurrenceFrequency = maintenance.RecurrenceInterval,
                    RecurrenceEndDate = maintenance.RecurrenceEndDate,
                    SendNotification = maintenance.SendReminders,
                    NotificationDaysInAdvance = maintenance.ReminderDaysBefore,
                    CompletionDate = maintenance.CompletionDate ?? DateTime.Now,
                    CompletedBy = maintenance.CompletedByUserName,
                    CompletionNotes = maintenance.CompletionNotes,
                    ActualCost = maintenance.ActualCost ?? 0,
                    ActualDurationMinutes = maintenance.ActualDuration ?? 0,
                    CancellationReason = maintenance.CancellationReason,
                    RequiredMaterials = maintenance.RequiredMaterials,
                    Documents = documents,
                    ChecklistItems = checklists,
                    Logs = logs,
                    IsRecurring = !string.IsNullOrEmpty(maintenance.RecurrencePattern),
                    EstimatedCost = maintenance.EstimatedCost ?? 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                return detailDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı getirilirken bir hata oluştu", id);
                throw;
            }
        }

        // Yeni bakım planı oluşturma
        public async Task<int> CreateMaintenanceScheduleAsync(MaintenanceScheduleCreateDTO createDto)
        {
            try
            {
                var newMaintenance = new MaintenanceSchedule
                {
                    Title = createDto.Title,
                    Description = createDto.Description,
                    MaintenanceType = createDto.MaintenanceType,
                    Priority = createDto.Priority,
                    ScheduledDate = createDto.ScheduledDate,
                    ScheduledTime = createDto.ScheduledTime,
                    EndDate = createDto.EndDate,
                    EstimatedDuration = createDto.EstimatedDurationMinutes,
                    Status = "Planlandı", // Default status
                    ResidenceId = createDto.ResidenceId,
                    BlockId = createDto.BlockId,
                    ApartmentId = createDto.ApartmentId,
                    EquipmentId = createDto.EquipmentId,
                    AssignedToUserId = createDto.AssignedToUserId,
                    RecurrencePattern = createDto.IsRecurring ? createDto.RecurrencePattern : null,
                    RecurrenceInterval = createDto.RecurrenceInterval,
                    RecurrenceEndDate = createDto.RecurrenceEndDate,
                    EstimatedCost = createDto.EstimatedCost,
                    SendReminders = createDto.SendReminders,
                    ReminderDaysBefore = createDto.ReminderDaysBefore,
                    CreatedByUserId = createDto.CreatedBy,
                    CreatedDate = DateTime.Now,
                    FirmaId = createDto.FirmaId,
                    SubeId = createDto.SubeId
                };

                _context.MaintenanceSchedules.Add(newMaintenance);
                await _context.SaveChangesAsync();

                // Eğer kontrol listesi öğeleri verilmişse onları da ekle
                if (createDto.ChecklistItems != null && createDto.ChecklistItems.Any())
                {
                    foreach (var item in createDto.ChecklistItems)
                    {
                        var checklistItem = new MaintenanceChecklistItem
                        {
                            MaintenanceScheduleId = newMaintenance.Id,
                            Title = "Kontrol Maddesi",
                            Description = item,
                            IsRequired = true,
                            IsCompleted = false,
                            OrderNumber = 0,
                            CreatedDate = DateTime.Now,
                            FirmaId = createDto.FirmaId,
                            SubeId = createDto.SubeId
                        };

                        _context.MaintenanceChecklistItems.Add(checklistItem);
                    }

                    await _context.SaveChangesAsync();
                }

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = newMaintenance.Id,
                    LogType = "Oluşturma",
                    Description = "Bakım planı oluşturuldu",
                    CreatedDate = DateTime.Now,
                    CreatedBy = createDto.CreatedBy,
                    FirmaId = createDto.FirmaId,
                    SubeId = createDto.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return newMaintenance.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı oluşturulurken bir hata oluştu");
                throw;
            }
        }

        // Mevcut bakım planını güncelleme
        public async Task<bool> UpdateMaintenanceScheduleAsync(MaintenanceScheduleUpdateDTO updateDto)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(updateDto.Id);
                if (maintenance == null)
                {
                    return false;
                }

                maintenance.Title = updateDto.Title;
                maintenance.Description = updateDto.Description;
                maintenance.MaintenanceType = updateDto.MaintenanceType;
                maintenance.Priority = updateDto.Priority;
                maintenance.ScheduledDate = updateDto.ScheduledDate;
                maintenance.ScheduledTime = updateDto.ScheduledTime;
                maintenance.EndDate = updateDto.EndDate;
                maintenance.EstimatedDuration = updateDto.EstimatedDurationMinutes;
                maintenance.Status = updateDto.Status;
                maintenance.ResidenceId = updateDto.ResidenceId;
                maintenance.BlockId = updateDto.BlockId;
                maintenance.ApartmentId = updateDto.ApartmentId;
                maintenance.EquipmentId = updateDto.EquipmentId;
                maintenance.AssignedToUserId = updateDto.AssignedToUserId;
                maintenance.RecurrencePattern = updateDto.IsRecurring ? updateDto.RecurrencePattern : null;
                maintenance.RecurrenceInterval = updateDto.RecurrenceInterval;
                maintenance.RecurrenceEndDate = updateDto.RecurrenceEndDate;
                maintenance.EstimatedCost = updateDto.EstimatedCost;
                maintenance.ActualCost = updateDto.ActualCost;
                maintenance.SendReminders = updateDto.SendReminders;
                maintenance.ReminderDaysBefore = updateDto.ReminderDaysBefore;
                maintenance.UpdatedByUserId = updateDto.UpdatedBy;
                maintenance.UpdatedDate = DateTime.Now;
                maintenance.FirmaId = updateDto.FirmaId;
                maintenance.SubeId = updateDto.SubeId;

                _context.MaintenanceSchedules.Update(maintenance);

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = maintenance.Id,
                    LogType = "Güncelleme",
                    Description = "Bakım planı güncellendi",
                    CreatedDate = DateTime.Now,
                    CreatedBy = updateDto.UpdatedBy,
                    FirmaId = updateDto.FirmaId,
                    SubeId = updateDto.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı güncellenirken bir hata oluştu", updateDto.Id);
                throw;
            }
        }

        // Bakım planını silme
        public async Task<bool> DeleteMaintenanceScheduleAsync(int id)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);
                if (maintenance == null)
                {
                    return false;
                }

                _context.MaintenanceSchedules.Remove(maintenance);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı silinirken bir hata oluştu", id);
                throw;
            }
        }

        // Bakım için kontrol listesini getirme
        public async Task<List<MaintenanceChecklistItemDTO>> GetMaintenanceChecklistAsync(int maintenanceId)
        {
            try
            {
                _logger.LogInformation("Bakım kontrol listesi getiriliyor: MaintenanceId={MaintenanceId}", maintenanceId);

                var checklistItems = await _context.MaintenanceChecklistItems
                    .Where(c => c.MaintenanceScheduleId == maintenanceId)
                    .OrderBy(c => c.OrderNumber)
                    .ToListAsync();

                if (checklistItems == null || !checklistItems.Any())
                {
                    _logger.LogWarning("Bakım için kontrol listesi bulunamadı: MaintenanceId={MaintenanceId}", maintenanceId);
                    return new List<MaintenanceChecklistItemDTO>();
                }

                var result = checklistItems.Select(item => new MaintenanceChecklistItemDTO
                {
                    Id = item.Id,
                    Description = item.Description,
                    Notes = item.Notes,
                    IsCompleted = item.IsCompleted,
                    CompletedDate = item.CompletedDate,
                    CompletedByUserId = item.CompletedByUserId,
                    OrderNumber = item.OrderNumber,
                    MaintenanceScheduleId = item.MaintenanceScheduleId,
                    FirmaId = item.FirmaId,
                    SubeId = item.SubeId
                }).ToList();

                _logger.LogInformation("Bakım kontrol listesi başarıyla getirildi: MaintenanceId={MaintenanceId}, ItemCount={ItemCount}", 
                    maintenanceId, result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım kontrol listesi getirilirken hata oluştu: MaintenanceId={MaintenanceId}", maintenanceId);
                throw;
            }
        }

        // Bakım için kontrol listesini güncelleme
        public async Task<bool> UpdateChecklistAsync(int maintenanceId, List<MaintenanceChecklistItemDTO> checklistItems)
        {
            try
            {
                _logger.LogInformation("Bakım kontrol listesi güncelleniyor: MaintenanceId={MaintenanceId}, ItemCount={ItemCount}", 
                    maintenanceId, checklistItems?.Count ?? 0);

                if (checklistItems == null)
                {
                    _logger.LogWarning("Bakım kontrol listesi güncellenemedi: Boş kontrol listesi gönderildi");
                    return false;
                }

                // Mevcut kontrol öğelerini al
                var existingItems = await _context.MaintenanceChecklistItems
                    .Where(c => c.MaintenanceScheduleId == maintenanceId)
                    .ToListAsync();

                // Silinecek öğeleri belirle (gelen listede olmayanlar)
                var itemsToDelete = existingItems
                    .Where(existing => !checklistItems.Any(item => item.Id == existing.Id))
                    .ToList();

                // Silinecek öğeleri sil
                if (itemsToDelete.Any())
                {
                    _context.MaintenanceChecklistItems.RemoveRange(itemsToDelete);
                    _logger.LogInformation("Bakım kontrol listesinden {Count} öğe silindi", itemsToDelete.Count);
                }

                // Her bir kontrol öğesini güncelle veya ekle
                foreach (var itemDto in checklistItems)
                {
                    if (itemDto.Id > 0)
                    {
                        // Mevcut öğeyi güncelle
                        var existingItem = existingItems.FirstOrDefault(e => e.Id == itemDto.Id);
                        if (existingItem != null)
                        {
                            existingItem.Description = itemDto.Description;
                            existingItem.Notes = itemDto.Notes;
                            existingItem.IsCompleted = itemDto.IsCompleted;
                            existingItem.CompletedDate = itemDto.CompletedDate;
                            existingItem.CompletedByUserId = itemDto.CompletedByUserId;
                            existingItem.OrderNumber = itemDto.OrderNumber;

                            _context.MaintenanceChecklistItems.Update(existingItem);
                            _logger.LogDebug("Bakım kontrol listesi öğesi güncellendi: ItemId={ItemId}", existingItem.Id);
                        }
                    }
                    else
                    {
                        // Yeni öğe ekle
                        var newItem = new MaintenanceChecklistItem
                        {
                            Description = itemDto.Description,
                            Notes = itemDto.Notes,
                            IsCompleted = itemDto.IsCompleted,
                            CompletedDate = itemDto.CompletedDate,
                            CompletedByUserId = itemDto.CompletedByUserId,
                            OrderNumber = itemDto.OrderNumber,
                            MaintenanceScheduleId = maintenanceId,
                            FirmaId = itemDto.FirmaId,
                            SubeId = itemDto.SubeId
                        };

                        _context.MaintenanceChecklistItems.Add(newItem);
                        _logger.LogDebug("Bakım kontrol listesine yeni öğe eklendi: Description={Description}", newItem.Description);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Bakım kontrol listesi başarıyla güncellendi: MaintenanceId={MaintenanceId}", maintenanceId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım kontrol listesi güncellenirken hata oluştu: MaintenanceId={MaintenanceId}", maintenanceId);
                throw;
            }
        }

        // Bakım planına belge ekleme
        public async Task<int> AddDocumentAsync(int maintenanceId, MaintenanceDocumentDTO documentDto)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
                if (maintenance == null)
                {
                    return 0;
                }

                var document = new MaintenanceDocument
                {
                    MaintenanceScheduleId = maintenanceId,
                    Title = documentDto.Title,
                    Description = documentDto.Description,
                    FileType = documentDto.DocumentType,
                    FilePath = documentDto.FilePath,
                    UploadDate = DateTime.Now,
                    UploadedBy = documentDto.UploadedBy,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceDocuments.Add(document);
                await _context.SaveChangesAsync();

                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = maintenanceId,
                    LogType = "Doküman Ekleme",
                    Description = $"{documentDto.Title} başlıklı doküman eklendi",
                    CreatedDate = DateTime.Now,
                    CreatedBy = documentDto.UploadedBy != null ? int.Parse(documentDto.UploadedBy) : 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return document.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {Id} olan bakım planına doküman eklenirken bir hata oluştu", maintenanceId);
                throw;
            }
        }

        // Bakım loglarını getirme
        public async Task<List<MaintenanceLogDTO>> GetMaintenanceLogsAsync(int maintenanceId)
        {
            try
            {
                var logs = await _context.MaintenanceLogs
                    .Where(l => l.MaintenanceScheduleId == maintenanceId)
                    .OrderByDescending(l => l.CreatedDate)
                    .Select(l => new MaintenanceLogDTO
                    {
                        Id = l.Id,
                        MaintenanceScheduleId = l.MaintenanceScheduleId,
                        LogDate = l.CreatedDate,
                        Activity = l.LogType,
                        Description = l.Description,
                        Notes = l.Notes,
                        PerformedByUserId = l.CreatedBy ?? 0,
                        FirmaId = l.FirmaId,
                        SubeId = l.SubeId
                    })
                    .ToListAsync();

                return logs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planının logları getirilirken bir hata oluştu", maintenanceId);
                throw;
            }
        }

        // Bakım raporunu getir
        public async Task<MaintenanceReportDTO> GetMaintenanceReportAsync(int maintenanceId)
        {
            try
            {
                // İlgili bakım planını getir
                var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);

                if (maintenance == null)
                {
                    return null;
                }

                // İlgili bakım planı için kontrol listesini getir
                var checklistItems = await _context.MaintenanceChecklists.Where(c => c.MaintenanceScheduleId == maintenanceId).ToListAsync();
                
                // İlgili bakım planı için logları getir
                var logs = await _context.MaintenanceLogs.Where(l => l.MaintenanceScheduleId == maintenanceId).ToListAsync();
                
                // İlgili bakım planı için dokümanları getir
                var documents = await _context.MaintenanceDocuments.Where(d => d.MaintenanceScheduleId == maintenanceId).ToListAsync();

                // Rapor DTO'sunu oluştur
                var report = new MaintenanceReportDTO
                {
                    MaintenanceScheduleId = maintenanceId,
                    MaintenanceTitle = maintenance.Title,
                    MaintenanceDescription = maintenance.Description,
                    MaintenanceType = maintenance.MaintenanceType,
                    ScheduledDate = maintenance.ScheduledDate,
                    CompletionDate = maintenance.CompletionDate,
                    Status = maintenance.Status,
                    AssignedToUserId = maintenance.AssignedToUserId,
                    CompletedTasks = checklistItems?.Count(c => c.IsCompleted) ?? 0,
                    TotalTasks = checklistItems?.Count() ?? 0,
                    LogEntries = logs?.Select(l => new MaintenanceLogDTO
                    {
                        Id = l.Id,
                        MaintenanceScheduleId = l.MaintenanceScheduleId,
                        ActionType = l.ActionType,
                        Description = l.Description,
                        PerformedByUserId = l.CreatedBy ?? 0,
                        ActionDate = l.ActionDate ?? DateTime.Now,
                        Notes = l.Notes
                    }).ToList() ?? new List<MaintenanceLogDTO>(),
                    Attachments = documents?.Select(d => new MaintenanceDocumentDTO
                    {
                        Id = d.Id,
                        MaintenanceScheduleId = d.MaintenanceScheduleId,
                        DocumentName = d.DocumentName,
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath,
                        Description = d.Description
                    }).ToList() ?? new List<MaintenanceDocumentDTO>(),
                    Notes = maintenance.Notes,
                    GeneratedDate = DateTime.Now,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {Id} olan bakım planı için rapor getirilirken bir hata oluştu", maintenanceId);
                throw;
            }
        }

        // Bakım maliyet raporunu getirme
        public async Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(DateTime startDate, DateTime endDate, int? residenceId)
        {
            try
            {
                var query = _context.MaintenanceSchedules
                    .Where(m => m.CompletionDate >= startDate && m.CompletionDate <= endDate)
                    .AsQueryable();

                if (residenceId.HasValue)
                {
                    query = query.Where(m => m.ResidenceId == residenceId.Value);
                }

                // Tamamlanan bakımları getir
                var completedMaintenances = await query
                    .Where(m => m.Status == "Tamamlandı" && m.ActualCost.HasValue)
                    .ToListAsync();

                // Toplam ve ortalama maliyet hesapla
                int totalMaintenanceCount = completedMaintenances.Count;
                int completedMaintenanceCount = completedMaintenances.Count;
                decimal totalEstimatedCost = completedMaintenances.Sum(m => m.EstimatedCost ?? 0);
                decimal totalActualCost = completedMaintenances.Sum(m => m.ActualCost ?? 0);
                decimal costDifferencePercentage = totalEstimatedCost > 0 
                    ? (totalActualCost - totalEstimatedCost) / totalEstimatedCost * 100 
                    : 0;

                // Tip bazında maliyet
                var costByType = completedMaintenances
                    .GroupBy(m => m.MaintenanceType)
                    .Select(g => new CostByTypeReportDTO
                    {
                        MaintenanceType = g.Key,
                        Count = g.Count(),
                        TotalCost = g.Sum(m => m.ActualCost ?? 0),
                        Percentage = totalActualCost > 0 
                            ? g.Sum(m => m.ActualCost ?? 0) / totalActualCost * 100 
                            : 0
                    })
                    .ToList();

                // Lokasyon bazında maliyet
                var costByLocation = completedMaintenances
                    .GroupBy(m => new { m.ResidenceId, m.BlockId, m.ApartmentId })
                    .Select(g => new CostByLocationReportDTO
                    {
                        LocationType = g.Key.ApartmentId.HasValue ? "Daire" : (g.Key.BlockId.HasValue ? "Blok" : "Rezidans"),
                        LocationName = g.Key.ApartmentId.HasValue ? "Daire " + g.Key.ApartmentId.Value.ToString() :
                                       (g.Key.BlockId.HasValue ? "Blok " + g.Key.BlockId.Value.ToString() :
                                       (g.Key.ResidenceId.HasValue ? "Rezidans " + g.Key.ResidenceId.Value.ToString() : "Bilinmiyor")),
                        Count = g.Count(),
                        TotalCost = g.Sum(m => m.ActualCost ?? 0),
                        Percentage = totalActualCost > 0 
                            ? g.Sum(m => m.ActualCost ?? 0) / totalActualCost * 100 
                            : 0
                    })
                    .ToList();

                // Ay bazında maliyet
                var costByMonth = completedMaintenances
                    .GroupBy(m => new { 
                        Month = m.CompletionDate != null ? m.CompletionDate.Value.Month : DateTime.Now.Month, 
                        Year = m.CompletionDate != null ? m.CompletionDate.Value.Year : DateTime.Now.Year 
                    })
                    .Select(g => new CostByMonthReportDTO
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        MonthName = System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.GetMonthName(g.Key.Month),
                        Count = g.Count(),
                        EstimatedCost = g.Sum(m => m.EstimatedCost ?? 0),
                        ActualCost = g.Sum(m => m.ActualCost ?? 0)
                    })
                    .OrderBy(m => m.Year)
                    .ThenBy(m => m.Month)
                    .ToList();

                // Yüksek maliyetli bakımlar
                var highCostMaintenances = completedMaintenances
                    .OrderByDescending(m => m.ActualCost)
                    .Take(5)
                    .Select(m => new HighCostMaintenanceReportDTO
                    {
                        MaintenanceId = m.Id,
                        Title = m.Title,
                        MaintenanceType = m.MaintenanceType,
                        Location = m.ApartmentId.HasValue ? "Daire " + m.ApartmentId.Value :
                                  (m.BlockId.HasValue ? "Blok " + m.BlockId.Value :
                                  (m.ResidenceId.HasValue ? "Rezidans " + m.ResidenceId.Value : "Bilinmiyor")),
                        ActualCost = m.ActualCost ?? 0,
                        PercentageOfTotal = totalActualCost > 0 
                            ? (m.ActualCost ?? 0) / totalActualCost * 100 
                            : 0
                    })
                    .ToList();

                // Büyük fark olan bakımlar
                var highVarianceMaintenances = completedMaintenances
                    .Where(m => m.EstimatedCost > 0)
                    .OrderByDescending(m => Math.Abs(((m.ActualCost ?? 0) - (m.EstimatedCost ?? 0)) / (m.EstimatedCost ?? 1) * 100))
                    .Take(5)
                    .Select(m => new CostVarianceMaintenanceReportDTO
                    {
                        MaintenanceId = m.Id,
                        Title = m.Title,
                        MaintenanceType = m.MaintenanceType,
                        EstimatedCost = m.EstimatedCost ?? 0,
                        ActualCost = m.ActualCost ?? 0,
                        Variance = (m.ActualCost ?? 0) - (m.EstimatedCost ?? 0),
                        VariancePercentage = m.EstimatedCost > 0 
                            ? ((m.ActualCost ?? 0) - (m.EstimatedCost ?? 0)) / (m.EstimatedCost ?? 1) * 100 
                            : 0
                    })
                    .ToList();

                var report = new MaintenanceCostReportDTO
                {
                    ReportDate = DateTime.Now,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalMaintenanceCount = totalMaintenanceCount,
                    CompletedMaintenanceCount = completedMaintenanceCount,
                    TotalEstimatedCost = totalEstimatedCost,
                    TotalActualCost = totalActualCost,
                    CostDifferencePercentage = costDifferencePercentage,
                    CostByType = costByType,
                    CostByLocation = costByLocation,
                    CostByMonth = costByMonth,
                    HighestCostItems = highCostMaintenances,
                    HighestVarianceItems = highVarianceMaintenances,
                    FirmaId = completedMaintenances.FirstOrDefault()?.FirmaId ?? 0,
                    SubeId = completedMaintenances.FirstOrDefault()?.SubeId ?? 0
                };

                _logger.LogInformation("Bakım maliyet raporu oluşturuldu: Başlangıç={StartDate}, Bitiş={EndDate}, Toplam Bakım={TotalCount}", 
                    startDate, endDate, completedMaintenances.Count);

                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım maliyet raporu getirilirken bir hata oluştu");
                throw;
            }
        }

        // Bakım durum güncellemesi
        public async Task<bool> UpdateMaintenanceStatusAsync(int id, string status, string notes = null)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);
                if (maintenance == null)
                {
                    return false;
                }

                var oldStatus = maintenance.Status;
                maintenance.Status = status;
                maintenance.UpdatedDate = DateTime.Now;

                if (status == "Tamamlandı" && maintenance.CompletionDate == null)
                {
                    maintenance.CompletionDate = DateTime.Now;
                }

                _context.MaintenanceSchedules.Update(maintenance);

                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = id,
                    LogType = "Durum Değişikliği",
                    Description = $"Bakım durumu '{oldStatus}' durumundan '{status}' durumuna değiştirildi",
                    Notes = notes,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {Id} olan bakım planının durumu güncellenirken bir hata oluştu", id);
                throw;
            }
        }

        // Bakımı çalışana ata
        public async Task<bool> AssignMaintenanceAsync(int id, int assignedToUserId, string assignedToUserName, string notes)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);
                if (maintenance == null)
                {
                    return false;
                }

                maintenance.AssignedToUserId = assignedToUserId;
                maintenance.AssignedToUserName = assignedToUserName;
                maintenance.Status = "Atandı";
                maintenance.UpdatedDate = DateTime.Now;

                _context.MaintenanceSchedules.Update(maintenance);

                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = id,
                    LogType = "Atama",
                    Description = $"Bakım {assignedToUserName} kişisine atandı",
                    Notes = notes,
                    CreatedDate = DateTime.Now,
                    CreatedBy = assignedToUserId,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {Id} olan bakım planı atanırken bir hata oluştu", id);
                throw;
            }
        }

        // Bakım tamamlama
        public async Task<bool> CompleteMaintenanceAsync(int id, string completionNotes, decimal? actualCost, int? actualDurationMinutes)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);
                if (maintenance == null)
                {
                    return false;
                }

                maintenance.Status = "Tamamlandı";
                maintenance.CompletionDate = DateTime.Now;
                maintenance.CompletionNotes = completionNotes;
                maintenance.ActualCost = actualCost;
                maintenance.ActualDuration = actualDurationMinutes;
                maintenance.UpdatedDate = DateTime.Now;

                _context.MaintenanceSchedules.Update(maintenance);

                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = id,
                    LogType = "Tamamlama",
                    Description = "Bakım görevi tamamlandı",
                    Notes = completionNotes,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(maintenance.RecurrencePattern) && 
                    maintenance.RecurrenceInterval.HasValue && 
                    maintenance.RecurrenceInterval.Value > 0)
                {
                    await CreateRecurrenceInstanceAsync(id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {Id} olan bakım planı tamamlanırken bir hata oluştu", id);
                throw;
            }
        }

        // Bakım iptal etme
        public async Task<bool> CancelMaintenanceAsync(int id, string reason)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(id);
                if (maintenance == null)
                {
                    return false;
                }

                maintenance.Status = "İptal Edildi";
                maintenance.CancellationReason = reason;
                maintenance.UpdatedDate = DateTime.Now;

                _context.MaintenanceSchedules.Update(maintenance);

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = id,
                    LogType = "İptal",
                    Description = "Bakım görevi iptal edildi",
                    Notes = reason,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı iptal edilirken bir hata oluştu", id);
                throw;
            }
        }

        // Ekipmana göre bakım planlarını getirme
        public async Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByEquipmentAsync(int equipmentId)
        {
            try
            {
                var maintenanceSchedules = await _context.MaintenanceSchedules
                    .Where(m => m.EquipmentId == equipmentId)
                    .Select(m => new MaintenanceScheduleDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MaintenanceType = m.MaintenanceType,
                        Priority = m.Priority,
                        ScheduledDate = m.ScheduledDate,
                        EstimatedDurationMinutes = m.EstimatedDuration,
                        Status = m.Status,
                        IsRecurring = !string.IsNullOrEmpty(m.RecurrencePattern),
                        EstimatedCost = m.EstimatedCost ?? 0,
                        FirmaId = m.FirmaId,
                        SubeId = m.SubeId
                    })
                    .ToListAsync();

                return maintenanceSchedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ekipman ID: {id} için bakım planları getirilirken bir hata oluştu", equipmentId);
                throw;
            }
        }

        // Daireye göre bakım planlarını getirme
        public async Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId)
        {
            try
            {
                var maintenanceSchedules = await _context.MaintenanceSchedules
                    .Where(m => m.ApartmentId == apartmentId)
                    .Select(m => new MaintenanceScheduleDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MaintenanceType = m.MaintenanceType,
                        Priority = m.Priority,
                        ScheduledDate = m.ScheduledDate,
                        EstimatedDurationMinutes = m.EstimatedDuration,
                        Status = m.Status,
                        IsRecurring = !string.IsNullOrEmpty(m.RecurrencePattern),
                        EstimatedCost = m.EstimatedCost ?? 0,
                        FirmaId = m.FirmaId,
                        SubeId = m.SubeId
                    })
                    .ToListAsync();

                return maintenanceSchedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Daire ID: {id} için bakım planları getirilirken bir hata oluştu", apartmentId);
                throw;
            }
        }

        // Kullanıcıya göre bakım planlarını getirme
        public async Task<List<MaintenanceScheduleDTO>> GetMaintenanceSchedulesByUserAsync(int userId)
        {
            try
            {
                var maintenanceSchedules = await _context.MaintenanceSchedules
                    .Where(m => m.AssignedToUserId == userId)
                    .Select(m => new MaintenanceScheduleDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MaintenanceType = m.MaintenanceType,
                        Priority = m.Priority,
                        ScheduledDate = m.ScheduledDate,
                        EstimatedDurationMinutes = m.EstimatedDuration,
                        Status = m.Status,
                        IsRecurring = !string.IsNullOrEmpty(m.RecurrencePattern),
                        EstimatedCost = m.EstimatedCost ?? 0,
                        FirmaId = m.FirmaId,
                        SubeId = m.SubeId
                    })
                    .ToListAsync();

                return maintenanceSchedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı ID: {id} için bakım planları getirilirken bir hata oluştu", userId);
                throw;
            }
        }

        // Tekrarlayan bakım için yeni örnek oluşturma
        public async Task<int> CreateRecurrenceInstanceAsync(int maintenanceId)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
                if (maintenance == null || string.IsNullOrEmpty(maintenance.RecurrencePattern) || !maintenance.RecurrenceInterval.HasValue)
                {
                    return 0;
                }

                // Bir sonraki bakım tarihini hesapla
                DateTime nextScheduledDate = maintenance.ScheduledDate;
                
                switch (maintenance.RecurrencePattern.ToLower())
                {
                    case "günlük":
                        nextScheduledDate = maintenance.ScheduledDate.AddDays(maintenance.RecurrenceInterval.Value);
                        break;
                    case "haftalık":
                        nextScheduledDate = maintenance.ScheduledDate.AddDays(7 * maintenance.RecurrenceInterval.Value);
                        break;
                    case "aylık":
                        nextScheduledDate = maintenance.ScheduledDate.AddMonths(maintenance.RecurrenceInterval.Value);
                        break;
                    case "yıllık":
                        nextScheduledDate = maintenance.ScheduledDate.AddYears(maintenance.RecurrenceInterval.Value);
                        break;
                    default:
                        return 0;
                }

                // Yeni bakım planı oluştur
                var newMaintenance = new MaintenanceSchedule
                {
                    Title = maintenance.Title,
                    Description = maintenance.Description,
                    MaintenanceType = maintenance.MaintenanceType,
                    Priority = maintenance.Priority,
                    ScheduledDate = nextScheduledDate,
                    ScheduledTime = maintenance.ScheduledTime,
                    EndDate = maintenance.EndDate.HasValue ? maintenance.EndDate.Value.AddDays((nextScheduledDate - maintenance.ScheduledDate).Days) : null,
                    EstimatedDuration = maintenance.EstimatedDuration,
                    Status = "Planlandı",
                    ResidenceId = maintenance.ResidenceId,
                    BlockId = maintenance.BlockId,
                    ApartmentId = maintenance.ApartmentId,
                    EquipmentId = maintenance.EquipmentId,
                    EstimatedCost = maintenance.EstimatedCost,
                    RecurrencePattern = maintenance.RecurrencePattern,
                    RecurrenceInterval = maintenance.RecurrenceInterval,
                    RecurrenceEndDate = maintenance.RecurrenceEndDate,
                    ParentMaintenanceId = maintenance.Id,
                    CreatedDate = DateTime.Now,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceSchedules.Add(newMaintenance);
                await _context.SaveChangesAsync();

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = newMaintenance.Id,
                    LogType = "Tekrarlama",
                    Description = "Tekrarlayan bakım planı oluşturuldu",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                return newMaintenance.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı için yeni tekrarlama örneği oluşturulurken bir hata oluştu", maintenanceId);
                throw;
            }
        }

        // Yaklaşan bakım raporunu getirme
        public async Task<MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days)
        {
            try
            {
                DateTime startDate = DateTime.Now;
                DateTime endDate = startDate.AddDays(days);

                var query = _context.MaintenanceSchedules
                    .Where(m => m.ScheduledDate >= startDate && m.ScheduledDate <= endDate)
                    .AsQueryable();

                if (residenceId.HasValue)
                {
                    query = query.Where(m => m.ResidenceId == residenceId.Value);
                }

                // Toplam ve durum sayıları
                int totalMaintenanceCount = await query.CountAsync();
                int upcomingCount = await query.CountAsync();
                int pendingCount = await query.Where(m => m.Status == "Planlandı").CountAsync();
                int inProgressCount = await query.Where(m => m.Status == "Devam Ediyor").CountAsync();
                int completedCount = await query.Where(m => m.Status == "Tamamlandı").CountAsync();
                int overdueCount = await _context.MaintenanceSchedules
                    .Where(m => m.ScheduledDate < startDate && m.Status != "Tamamlandı" && m.Status != "İptal Edildi")
                    .CountAsync();
                int cancelledCount = await query.Where(m => m.Status == "İptal Edildi").CountAsync();

                // Bakım tipine göre dağılım
                var byType = await query
                    .GroupBy(m => m.MaintenanceType)
                    .Select(g => new MaintenanceByTypeDTO
                    {
                        MaintenanceType = g.Key,
                        Count = g.Count(),
                        Percentage = (decimal)g.Count() / (totalMaintenanceCount > 0 ? totalMaintenanceCount : 1) * 100
                    })
                    .ToListAsync();

                // Lokasyona göre dağılım
                var byLocation = await query
                    .GroupBy(m => new { m.ResidenceId, m.BlockId, m.ApartmentId })
                    .Select(g => new MaintenanceByLocationDTO
                    {
                        LocationType = g.Key.ApartmentId.HasValue ? "Daire" : (g.Key.BlockId.HasValue ? "Blok" : "Rezidans"),
                        LocationName = g.Key.ApartmentId.HasValue ? "Daire " + g.Key.ApartmentId.Value.ToString() :
                                       (g.Key.BlockId.HasValue ? "Blok " + g.Key.BlockId.Value.ToString() :
                                       (g.Key.ResidenceId.HasValue ? "Rezidans " + g.Key.ResidenceId.Value.ToString() : "Bilinmiyor")),
                        Count = g.Count(),
                        Percentage = (decimal)g.Count() / (totalMaintenanceCount > 0 ? totalMaintenanceCount : 1) * 100
                    })
                    .ToListAsync();

                // Duruma göre dağılım
                var byStatus = await query
                    .GroupBy(m => m.Status)
                    .Select(g => new MaintenanceByStatusDTO
                    {
                        Status = g.Key,
                        Count = g.Count(),
                        Percentage = (decimal)g.Count() / (totalMaintenanceCount > 0 ? totalMaintenanceCount : 1) * 100
                    })
                    .ToListAsync();

                var report = new MaintenanceReportDTO
                {
                    ReportDate = DateTime.Now,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalMaintenanceCount = totalMaintenanceCount,
                    UpcomingMaintenanceCount = upcomingCount,
                    PendingMaintenanceCount = pendingCount,
                    InProgressMaintenanceCount = inProgressCount,
                    CompletedMaintenanceCount = completedCount,
                    OverdueMaintenanceCount = overdueCount,
                    CancelledMaintenanceCount = cancelledCount,
                    MaintenanceByType = byType,
                    MaintenanceByLocation = byLocation,
                    MaintenanceByStatus = byStatus,
                    FirmaId = residenceId.HasValue ? query.FirstOrDefault()?.FirmaId ?? 0 : 0,
                    SubeId = residenceId.HasValue ? query.FirstOrDefault()?.SubeId ?? 0 : 0
                };

                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yaklaşan bakım raporu getirilirken bir hata oluştu");
                throw;
            }
        }

        // Bakım maliyet raporunu getir (tekil bakım planı için)
        public async Task<MaintenanceCostReportDTO> GetMaintenanceCostReportByIdAsync(int maintenanceId)
        {
            try
            {
                // İlgili bakım planını getir
                var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);

                if (maintenance == null)
                {
                    _logger.LogWarning("ID: {Id} olan bakım planı bulunamadı", maintenanceId);
                    return null;
                }

                // İlgili bakım planı için maliyetleri getir
                var costs = await _context.MaintenanceCosts
                    .Where(c => c.MaintenanceScheduleId == maintenanceId)
                    .ToListAsync();

                // Ek bilgileri getir
                bool hasCompletionDate = maintenance.CompletionDate.HasValue;
                decimal estimatedCost = maintenance.EstimatedCost ?? 0;
                decimal actualCost = maintenance.ActualCost ?? 0;
                decimal costDifference = actualCost - estimatedCost;
                decimal costDifferencePercentage = estimatedCost > 0 
                    ? (costDifference / estimatedCost) * 100 
                    : 0;

                // Maliyet türüne göre dağılım
                var costTypeBreakdown = costs
                    .GroupBy(c => c.CostType)
                    .Select(g => new CostByTypeReportDTO
                    {
                        MaintenanceType = g.Key,
                        Count = g.Count(),
                        TotalCost = g.Sum(c => c.Amount),
                        Percentage = costs.Sum(c => c.Amount) > 0 
                            ? g.Sum(c => c.Amount) / costs.Sum(c => c.Amount) * 100 
                            : 0
                    })
                    .ToList();

                // Maliyet raporu DTO'sunu oluştur
                var costReport = new MaintenanceCostReportDTO
                {
                    // Genel bilgiler
                    ReportDate = DateTime.Now,
                    StartDate = maintenance.ScheduledDate,
                    EndDate = maintenance.CompletionDate ?? DateTime.Now,
                    TotalMaintenanceCount = 1,
                    CompletedMaintenanceCount = hasCompletionDate ? 1 : 0,
                    
                    // Maliyet bilgileri
                    TotalEstimatedCost = estimatedCost,
                    TotalActualCost = actualCost,
                    CostDifferencePercentage = costDifferencePercentage,
                    
                    // Bakıma özgü maliyetler
                    CostByType = costTypeBreakdown,
                    
                    // Firma ve şube bilgileri
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                // Bakım maliyeti kırılımını ekle
                if (costs.Any())
                {
                    // Yüksek maliyet kalemi varsa raporla
                    var highestCostItem = costs.OrderByDescending(c => c.Amount).FirstOrDefault();
                    if (highestCostItem != null)
                    {
                        costReport.HighestCostItems = new List<HighCostMaintenanceReportDTO>
                        {
                            new HighCostMaintenanceReportDTO
                            {
                                MaintenanceId = maintenance.Id,
                                Title = maintenance.Title,
                                MaintenanceType = maintenance.MaintenanceType,
                                Location = "Bakım yeri",
                                ActualCost = actualCost,
                                PercentageOfTotal = 100
                            }
                        };
                    }

                    // Maliyet farkı varsa raporla
                    if (Math.Abs(costDifferencePercentage) > 10)
                    {
                        costReport.HighestVarianceItems = new List<CostVarianceMaintenanceReportDTO>
                        {
                            new CostVarianceMaintenanceReportDTO
                            {
                                MaintenanceId = maintenance.Id,
                                Title = maintenance.Title,
                                MaintenanceType = maintenance.MaintenanceType,
                                EstimatedCost = estimatedCost,
                                ActualCost = actualCost,
                                Variance = costDifference,
                                VariancePercentage = costDifferencePercentage
                            }
                        };
                    }
                }

                _logger.LogInformation("ID: {Id} olan bakım planı için maliyet raporu oluşturuldu", maintenanceId);
                return costReport;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {Id} olan bakım planı için maliyet raporu getirilirken bir hata oluştu", maintenanceId);
                throw;
            }
        }

        // Bakım kontrol listesi güncelleme metodu
        public async Task<ApiResponse> UpdateMaintenanceChecklistAsync(int scheduleId, MaintenanceChecklistUpdateDTO model)
        {
            try
            {
                _logger.LogInformation("Bakım kontrol listesi güncelleniyor: ScheduleId={ScheduleId}", scheduleId);

                var maintenance = await _context.MaintenanceSchedules.FindAsync(scheduleId);
                if (maintenance == null)
                {
                    _logger.LogWarning("Bakım planı bulunamadı: ScheduleId={ScheduleId}", scheduleId);
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Bakım planı bulunamadı",
                        StatusCode = 404
                    };
                }

                // Mevcut kontrol listesi öğelerini al
                var existingItems = await _context.MaintenanceChecklistItems
                    .Where(c => c.MaintenanceScheduleId == scheduleId)
                    .ToListAsync();

                // Silinecek öğeleri belirle
                var itemsToDelete = existingItems
                    .Where(existing => !model.Items.Any(item => item.Id == existing.Id))
                    .ToList();

                // Silinecek öğeleri sil
                if (itemsToDelete.Any())
                {
                    _context.MaintenanceChecklistItems.RemoveRange(itemsToDelete);
                    _logger.LogInformation("Bakım kontrol listesinden {Count} öğe silindi", itemsToDelete.Count);
                }

                // Her bir kontrol öğesini güncelle veya ekle
                foreach (var itemDto in model.Items)
                {
                    if (itemDto.Id > 0)
                    {
                        // Mevcut öğeyi güncelle
                        var existingItem = existingItems.FirstOrDefault(e => e.Id == itemDto.Id);
                        if (existingItem != null)
                        {
                            existingItem.Title = itemDto.Title;
                            existingItem.Description = itemDto.Description;
                            existingItem.IsRequired = itemDto.IsRequired;
                            existingItem.IsCompleted = itemDto.IsCompleted;
                            existingItem.CompletedDate = itemDto.CompletedDate;
                            existingItem.CompletedByUserId = itemDto.CompletedByUserId;
                            existingItem.Notes = itemDto.Notes;
                            existingItem.OrderNumber = itemDto.OrderNumber;
                            existingItem.UpdatedDate = DateTime.Now;

                            _context.MaintenanceChecklistItems.Update(existingItem);
                            _logger.LogDebug("Bakım kontrol listesi öğesi güncellendi: ItemId={ItemId}", existingItem.Id);
                        }
                    }
                    else
                    {
                        // Yeni öğe ekle
                        var newItem = new MaintenanceChecklistItem
                        {
                            MaintenanceScheduleId = scheduleId,
                            Title = itemDto.Title,
                            Description = itemDto.Description,
                            IsRequired = itemDto.IsRequired,
                            IsCompleted = itemDto.IsCompleted,
                            CompletedDate = itemDto.CompletedDate,
                            CompletedByUserId = itemDto.CompletedByUserId,
                            Notes = itemDto.Notes,
                            OrderNumber = itemDto.OrderNumber,
                            CreatedDate = DateTime.Now,
                            FirmaId = maintenance.FirmaId,
                            SubeId = maintenance.SubeId
                        };

                        _context.MaintenanceChecklistItems.Add(newItem);
                        _logger.LogDebug("Bakım kontrol listesine yeni öğe eklendi: Title={Title}", newItem.Title);
                    }
                }

                await _context.SaveChangesAsync();

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = scheduleId,
                    LogType = "Kontrol Listesi Güncelleme",
                    Description = "Bakım kontrol listesi güncellendi",
                    CreatedDate = DateTime.Now,
                    CreatedBy = model.UpdatedByUserId,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Bakım kontrol listesi başarıyla güncellendi: ScheduleId={ScheduleId}", scheduleId);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Bakım kontrol listesi başarıyla güncellendi",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım kontrol listesi güncellenirken hata oluştu: ScheduleId={ScheduleId}", scheduleId);
                return new ApiResponse
                {
                    Success = false,
                    Message = "Bakım kontrol listesi güncellenirken bir hata oluştu",
                    StatusCode = 500
                };
            }
        }

        // Bakım dokümanı ekleme metodu
        public async Task<ApiResponse> AddMaintenanceDocumentAsync(int scheduleId, MaintenanceDocumentCreateDTO model)
        {
            try
            {
                _logger.LogInformation("Bakım dokümanı ekleniyor: ScheduleId={ScheduleId}", scheduleId);

                var maintenance = await _context.MaintenanceSchedules.FindAsync(scheduleId);
                if (maintenance == null)
                {
                    _logger.LogWarning("Bakım planı bulunamadı: ScheduleId={ScheduleId}", scheduleId);
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Bakım planı bulunamadı",
                        StatusCode = 404
                    };
                }

                var document = new MaintenanceDocument
                {
                    MaintenanceScheduleId = scheduleId,
                    Title = model.Title,
                    Description = model.Description,
                    FileType = model.FileType,
                    FilePath = model.FilePath,
                    FileSize = model.FileSize,
                    UploadDate = DateTime.Now,
                    UploadedBy = model.UploadedByUserId.ToString(),
                    CreatedDate = DateTime.Now,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceDocuments.Add(document);
                await _context.SaveChangesAsync();

                // Log kaydı oluştur
                var log = new MaintenanceLog
                {
                    MaintenanceScheduleId = scheduleId,
                    LogType = "Doküman Ekleme",
                    Description = $"{model.Title} başlıklı doküman eklendi",
                    CreatedDate = DateTime.Now,
                    CreatedBy = model.UploadedByUserId,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(log);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Bakım dokümanı başarıyla eklendi: ScheduleId={ScheduleId}, DocumentId={DocumentId}", 
                    scheduleId, document.Id);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Bakım dokümanı başarıyla eklendi",
                    Data = new { DocumentId = document.Id },
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım dokümanı eklenirken hata oluştu: ScheduleId={ScheduleId}", scheduleId);
                return new ApiResponse
                {
                    Success = false,
                    Message = "Bakım dokümanı eklenirken bir hata oluştu",
                    StatusCode = 500
                };
            }
        }

        // Atanmış kullanıcıya göre bakım planlarını getirme
        public async Task<ApiResponse<List<MaintenanceScheduleListDTO>>> GetMaintenanceSchedulesByAssignedUserAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"Kullanıcıya atanmış bakım planları getiriliyor: UserId={userId}");

                var schedules = await _context.MaintenanceSchedules
                    .Where(m => m.AssignedToUserId == userId)
                    .Select(m => new MaintenanceScheduleListDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MaintenanceType = m.MaintenanceType,
                        Priority = m.Priority,
                        ScheduledDate = m.ScheduledDate,
                        ScheduledTime = m.ScheduledTime,
                        EstimatedDurationMinutes = m.EstimatedDuration ?? 0,
                        Status = m.Status,
                        AssignedToUserId = m.AssignedToUserId,
                        AssignedToUserName = m.AssignedToUserName,
                        ResidenceId = m.ResidenceId,
                        BlockId = m.BlockId,
                        ApartmentId = m.ApartmentId,
                        IsRecurring = !string.IsNullOrEmpty(m.RecurrencePattern),
                        EstimatedCost = m.EstimatedCost ?? 0,
                        FirmaId = m.FirmaId,
                        SubeId = m.SubeId,
                        CreatedDate = m.CreatedDate,
                        UpdatedDate = m.UpdatedDate
                    })
                    .ToListAsync();

                _logger.LogInformation($"Kullanıcıya atanmış {schedules.Count} bakım planı bulundu: UserId={userId}");

                return new ApiResponse<List<MaintenanceScheduleListDTO>>
                {
                    Success = true,
                    Message = "Bakım planları başarıyla getirildi",
                    Data = schedules,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcıya atanmış bakım planları getirilirken hata oluştu: UserId={userId}");
                return new ApiResponse<List<MaintenanceScheduleListDTO>>
                {
                    Success = false,
                    Message = "Bakım planları getirilirken bir hata oluştu",
                    StatusCode = 500
                };
            }
        }
    }
} 