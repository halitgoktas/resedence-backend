using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs.Maintenance;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Services
{
    // Bakım servisi implementasyonu
    public class MaintenanceService : IMaintenanceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MaintenanceService> _logger;

        public MaintenanceService(ApplicationDbContext context, ILogger<MaintenanceService> logger)
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
                        EstimatedCost = m.EstimatedCost ?? 0M,
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

        public async Task<MaintenanceScheduleDetailDTO> GetMaintenanceScheduleByIdAsync(int id)
        {
            try
            {
                var maintenanceSchedule = await _context.MaintenanceSchedules
                    .Include(m => m.Documents)
                    .Include(m => m.ChecklistItems)
                    .Include(m => m.Logs)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (maintenanceSchedule == null)
                {
                    _logger.LogWarning($"Bakım programı bulunamadı. ID: {id}");
                    return null;
                }

                var completionRate = maintenanceSchedule.ChecklistItems != null && maintenanceSchedule.ChecklistItems.Any() 
                    ? (double)maintenanceSchedule.ChecklistItems.Count(c => c.IsCompleted) / maintenanceSchedule.ChecklistItems.Count * 100 
                    : 0;

                var totalCost = maintenanceSchedule.ActualCost ?? 0m;

                var maintenanceLog = new MaintenanceLog
                {
                    MaintenanceScheduleId = maintenanceSchedule.Id,
                    Description = $"Bakım programı görüntülendi: {maintenanceSchedule.Title}",
                    CreatedBy = maintenanceSchedule.CreatedBy ?? 0,
                    LogDate = DateTime.Now
                };

                var documents = await _context.MaintenanceDocuments
                    .Where(d => d.MaintenanceScheduleId == id)
                    .Select(d => new MaintenanceDocumentDTO
                    {
                        Id = d.Id,
                        MaintenanceScheduleId = d.MaintenanceScheduleId,
                        Title = d.Title,
                        Description = d.Description,
                        FilePath = d.FilePath,
                        FileType = d.FileType,
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
                    Id = maintenanceSchedule.Id,
                    Title = maintenanceSchedule.Title,
                    Description = maintenanceSchedule.Description,
                    MaintenanceType = maintenanceSchedule.MaintenanceType,
                    Priority = maintenanceSchedule.Priority,
                    ScheduledDate = maintenanceSchedule.ScheduledDate,
                    ScheduledTime = maintenanceSchedule.ScheduledTime,
                    EndDate = maintenanceSchedule.EndDate,
                    EstimatedDurationMinutes = maintenanceSchedule.EstimatedDuration,
                    Status = maintenanceSchedule.Status,
                    ResidenceId = maintenanceSchedule.ResidenceId,
                    BlockId = maintenanceSchedule.BlockId,
                    ApartmentId = maintenanceSchedule.ApartmentId,
                    EquipmentId = maintenanceSchedule.EquipmentId,
                    AssignedToUserId = maintenanceSchedule.AssignedToUserId,
                    ServiceProviderId = maintenanceSchedule.ServiceProviderId,
                    ServiceProviderName = maintenanceSchedule.ServiceProviderName,
                    RecurrencePattern = maintenanceSchedule.RecurrencePattern,
                    RecurrenceFrequency = maintenanceSchedule.RecurrenceInterval,
                    RecurrenceEndDate = maintenanceSchedule.RecurrenceEndDate,
                    SendNotification = maintenanceSchedule.SendReminders,
                    NotificationDaysInAdvance = maintenanceSchedule.ReminderDaysBefore,
                    CompletionDate = maintenanceSchedule.CompletionDate,
                    CompletedBy = maintenanceSchedule.CompletedByUserName,
                    CompletionNotes = maintenanceSchedule.CompletionNotes,
                    ActualCost = maintenanceSchedule.ActualCost ?? 0,
                    ActualDurationMinutes = maintenanceSchedule.ActualDuration ?? 0,
                    CancellationReason = maintenanceSchedule.CancellationReason,
                    RequiredMaterials = maintenanceSchedule.RequiredMaterials,
                    Documents = documents,
                    ChecklistItems = checklists,
                    Logs = logs,
                    IsRecurring = !string.IsNullOrEmpty(maintenanceSchedule.RecurrencePattern),
                    EstimatedCost = maintenanceSchedule.EstimatedCost ?? 0M,
                    FirmaId = maintenanceSchedule.FirmaId,
                    SubeId = maintenanceSchedule.SubeId
                };

                return detailDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı getirilirken bir hata oluştu", id);
                throw;
            }
        }

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
                    Status = "Planlandı",
                    ResidenceId = createDto.ResidenceId,
                    BlockId = createDto.BlockId,
                    ApartmentId = createDto.ApartmentId,
                    EquipmentId = createDto.EquipmentId,
                    AssignedToUserId = createDto.AssignedToUserId,
                    RecurrencePattern = createDto.IsRecurring ? createDto.RecurrencePattern : null,
                    RecurrenceInterval = createDto.RecurrenceInterval ?? 0,
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

                var maintenanceLog = new MaintenanceLog
                {
                    MaintenanceScheduleId = newMaintenance.Id,
                    LogType = "Oluşturma",
                    Description = $"Bakım planı oluşturuldu: {newMaintenance.Title}",
                    CreatedBy = int.TryParse(createDto.CreatedBy.ToString(), out int result1) ? result1 : 0,
                    CreatedDate = DateTime.Now
                };

                _context.MaintenanceLogs.Add(maintenanceLog);
                await _context.SaveChangesAsync();

                return newMaintenance.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı oluşturulurken bir hata oluştu");
                throw;
            }
        }

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

                var maintenanceLog = new MaintenanceLog
                {
                    MaintenanceScheduleId = maintenance.Id,
                    LogType = "Güncelleme",
                    Description = "Bakım planı güncellendi",
                    CreatedBy = int.TryParse(updateDto.UpdatedBy.ToString(), out int result2) ? result2 : 0,
                    CreatedDate = DateTime.Now
                };

                _context.MaintenanceLogs.Add(maintenanceLog);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID: {id} olan bakım planı güncellenirken bir hata oluştu", updateDto.Id);
                throw;
            }
        }

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

        // Bakım görevini çalışana atama
        public async Task<bool> AssignMaintenanceAsync(
            int maintenanceId, int assignedToUserId, string assignedToUserName, string notes)
        {
            var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
            if (maintenance == null)
            {
                return false;
            }

            // Bakım görevini atanan kişiye güncelle
            maintenance.AssignedToUserId = assignedToUserId;
            maintenance.AssignedToUserName = assignedToUserName;
            maintenance.Status = "Atandı";

            // Log kaydı oluştur
            var maintenanceLog = new MaintenanceLog
            {
                MaintenanceScheduleId = maintenanceId,
                LogType = "Atama",
                Description = $"Bakım {assignedToUserName} kişisine atandı",
                Notes = notes,
                CreatedDate = DateTime.Now,
                CreatedBy = int.TryParse(assignedToUserId.ToString(), out int result3) ? result3 : 0,
                FirmaId = maintenance.FirmaId,
                SubeId = maintenance.SubeId
            };

            _context.MaintenanceLogs.Add(maintenanceLog);
            await _context.SaveChangesAsync();

            return true;
        }

        // Bakım tamamlama işlemi
        public async Task<bool> CompleteMaintenanceAsync(
            int maintenanceId, string completionNotes, decimal? actualCost, int? actualDurationMinutes)
        {
            var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
            if (maintenance == null)
            {
                return false;
            }

            // Bakım görevini tamamlandı olarak işaretle
            maintenance.Status = "Tamamlandı";
            maintenance.CompletionDate = DateTime.Now;
            maintenance.CompletionNotes = completionNotes;
            maintenance.ActualCost = actualCost;
            maintenance.ActualDuration = actualDurationMinutes;

            // Log kaydı oluştur
            var maintenanceLog = new MaintenanceLog
            {
                MaintenanceScheduleId = maintenanceId,
                LogType = "Tamamlama",
                Description = "Bakım görevi tamamlandı",
                Notes = completionNotes,
                CreatedDate = DateTime.Now,
                CreatedBy = 0,
                FirmaId = maintenance.FirmaId,
                SubeId = maintenance.SubeId
            };

            _context.MaintenanceLogs.Add(maintenanceLog);
            await _context.SaveChangesAsync();

            // Tekrarlanan bakım ise bir sonraki örneği oluştur
            if (!string.IsNullOrEmpty(maintenance.RecurrencePattern) && maintenance.RecurrenceInterval > 0)
            {
                await CreateRecurrenceInstanceAsync(maintenanceId);
            }

            return true;
        }

        // Bakım iptal etme işlemi
        public async Task<bool> CancelMaintenanceAsync(int maintenanceId, string cancellationReason)
        {
            var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
            if (maintenance == null)
            {
                return false;
            }

            // Bakım görevini iptal et
            maintenance.Status = "İptal Edildi";
            maintenance.CancellationReason = cancellationReason;

            // Log kaydı oluştur
            var maintenanceLog = new MaintenanceLog
            {
                MaintenanceScheduleId = maintenanceId,
                LogType = "İptal",
                Description = "Bakım görevi iptal edildi",
                Notes = cancellationReason,
                CreatedDate = DateTime.Now,
                CreatedBy = 0,
                FirmaId = maintenance.FirmaId,
                SubeId = maintenance.SubeId
            };

            _context.MaintenanceLogs.Add(maintenanceLog);
            await _context.SaveChangesAsync();

            return true;
        }

        // Bakım kontrol listesini güncelleme
        public async Task<bool> UpdateMaintenanceChecklistAsync(
            int maintenanceId, List<ResidenceManagement.Core.DTOs.Maintenance.MaintenanceChecklistItemDTO> checklistItems)
        {
            try
            {
                var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
                if (maintenance == null)
                {
                    _logger.LogWarning("Kontrol listesi güncellenirken bakım planı bulunamadı. ID: {Id}", maintenanceId);
                    return false;
                }

                // Mevcut kontrol listesi öğelerini sil
                var existingItems = await _context.MaintenanceChecklistItems
                    .Where(c => c.MaintenanceScheduleId == maintenanceId)
                    .ToListAsync();

                _context.MaintenanceChecklistItems.RemoveRange(existingItems);

                // Yeni kontrol listesi öğelerini ekle
                foreach (var item in checklistItems)
                {
                    var checklistItem = new MaintenanceChecklistItem
                    {
                        MaintenanceScheduleId = maintenanceId,
                        Title = item.Title ?? "Kontrol Maddesi",
                        Description = item.Description ?? string.Empty,
                        IsRequired = item.IsRequired,
                        IsCompleted = item.IsCompleted,
                        CompletedDate = item.IsCompleted ? item.CompletedDate ?? DateTime.Now : null,
                        CompletedByUserId = item.CompletedByUserId,
                        Notes = item.Notes,
                        OrderNumber = item.OrderNumber,
                        FirmaId = maintenance.FirmaId,
                        SubeId = maintenance.SubeId
                    };

                    _context.MaintenanceChecklistItems.Add(checklistItem);
                }

                var maintenanceLog = new MaintenanceLog
                {
                    MaintenanceScheduleId = maintenanceId,
                    LogType = "Güncelleme",
                    Description = "Kontrol listesi güncellendi",
                    CreatedDate = DateTime.Now,
                    FirmaId = maintenance.FirmaId,
                    SubeId = maintenance.SubeId
                };

                _context.MaintenanceLogs.Add(maintenanceLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kontrol listesi güncellenirken hata oluştu. ID: {Id}", maintenanceId);
                return false;
            }
        }

        // Tekrarlanan bakım için yeni instance oluşturma
        public async Task<int> CreateRecurrenceInstanceAsync(int maintenanceId)
        {
            var maintenance = await _context.MaintenanceSchedules.FindAsync(maintenanceId);
            if (maintenance == null)
            {
                return 0;
            }

            // Bakım tekrarlama değerleri yoksa işlem yapma
            if (string.IsNullOrEmpty(maintenance.RecurrencePattern) || maintenance.RecurrenceInterval <= 0)
            {
                return 0;
            }

            // Bir sonraki bakım tarihini hesapla
            DateTime nextScheduledDate = maintenance.ScheduledDate;
            
            switch (maintenance.RecurrencePattern.ToLower())
            {
                case "günlük":
                    nextScheduledDate = maintenance.ScheduledDate.AddDays(maintenance.RecurrenceInterval ?? 0);
                    break;
                case "haftalık":
                    nextScheduledDate = maintenance.ScheduledDate.AddDays((7 * (maintenance.RecurrenceInterval ?? 0)));
                    break;
                case "aylık":
                    nextScheduledDate = maintenance.ScheduledDate.AddMonths(maintenance.RecurrenceInterval ?? 0);
                    break;
                case "yıllık":
                    nextScheduledDate = maintenance.ScheduledDate.AddYears(maintenance.RecurrenceInterval ?? 0);
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
                EstimatedDuration = maintenance.EstimatedDuration,
                Status = "Planlandı",
                ResidenceId = maintenance.ResidenceId,
                BlockId = maintenance.BlockId,
                ApartmentId = maintenance.ApartmentId,
                EquipmentId = maintenance.EquipmentId,
                EstimatedCost = maintenance.EstimatedCost,
                RecurrencePattern = maintenance.RecurrencePattern,
                RecurrenceInterval = maintenance.RecurrenceInterval,
                ParentMaintenanceId = maintenance.Id,
                CreatedDate = DateTime.Now,
                FirmaId = maintenance.FirmaId,
                SubeId = maintenance.SubeId
            };

            _context.MaintenanceSchedules.Add(newMaintenance);
            await _context.SaveChangesAsync();

            return newMaintenance.Id;
        }

        // Daireye göre bakım planlarını getirme
        public async Task<List<ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO>> GetMaintenanceSchedulesByApartmentAsync(int apartmentId)
        {
            var query = _context.MaintenanceSchedules
                .Where(m => m.ApartmentId == apartmentId);

            // Sonuçları DTO'ya dönüştür
            var maintenanceSchedules = await query
                .Select(m => new ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO
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
                    EstimatedCost = m.EstimatedCost ?? 0M,
                    FirmaId = m.FirmaId,
                    SubeId = m.SubeId
                })
                .ToListAsync();

            return maintenanceSchedules;
        }

        // Kullanıcıya göre bakım planlarını getirme
        public async Task<List<ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO>> GetMaintenanceSchedulesByAssignedUserAsync(int userId)
        {
            var query = _context.MaintenanceSchedules
                .Where(m => m.AssignedToUserId == userId);

            // Sonuçları DTO'ya dönüştür
            var maintenanceSchedules = await query
                .Select(m => new ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO
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
                    EstimatedCost = m.EstimatedCost ?? 0M,
                    FirmaId = m.FirmaId,
                    SubeId = m.SubeId
                })
                .ToListAsync();

            return maintenanceSchedules;
        }

        // Yaklaşan bakım raporunu getirme
        public async Task<ResidenceManagement.Core.DTOs.Maintenance.MaintenanceReportDTO> GetUpcomingMaintenanceReportAsync(int? residenceId, int days = 30)
        {
            DateTime endDate = DateTime.Now.AddDays(days);
            
            var query = _context.MaintenanceSchedules
                .Where(m => m.ScheduledDate >= DateTime.Now && m.ScheduledDate <= endDate)
                .AsQueryable();

            if (residenceId.HasValue)
            {
                query = query.Where(m => m.ResidenceId == residenceId.Value);
            }

            // Yaklaşan bakım istatistiklerini getir
            var upcomingCount = await query.CountAsync();
            var pendingCount = await query.Where(m => m.Status == "Planlandı").CountAsync();
            var assignedCount = await query.Where(m => m.Status == "Atandı").CountAsync();
            var inProgressCount = await query.Where(m => m.Status == "İşlemde").CountAsync();
            var completedCount = await query.Where(m => m.Status == "Tamamlandı").CountAsync();
            var overdueCount = await _context.MaintenanceSchedules
                .Where(m => m.ScheduledDate < DateTime.Now && m.Status != "Tamamlandı" && m.Status != "İptal Edildi")
                .CountAsync();

            // Bakım türlerine göre dağılım
            var byTypeQuery = await query
                .GroupBy(m => m.MaintenanceType)
                .Select(g => new MaintenanceByTypeDTO
                {
                    MaintenanceType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // Konumlara göre dağılım
            var byLocationQuery = await query
                .GroupBy(m => m.BlockId)
                .Select(g => new MaintenanceByLocationDTO
                {
                    LocationId = g.Key.HasValue ? g.Key.Value : 0,
                    LocationName = "Blok " + g.Key.ToString(),
                    Count = g.Count()
                })
                .ToListAsync();

            // Statülere göre dağılım
            var byStatusQuery = await query
                .GroupBy(m => m.Status)
                .Select(g => new MaintenanceByStatusDTO
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var report = new ResidenceManagement.Core.DTOs.Maintenance.MaintenanceReportDTO
            {
                TotalUpcoming = upcomingCount,
                TotalPending = pendingCount,
                TotalInProgress = inProgressCount,
                TotalCompleted = completedCount,
                TotalOverdue = overdueCount,
                ByType = byTypeQuery,
                ByLocation = byLocationQuery,
                ByStatus = byStatusQuery,
                ReportGeneratedDate = DateTime.Now
            };

            return report;
        }

        // Bakım maliyet raporunu getirme
        public async Task<MaintenanceCostReportDTO> GetMaintenanceCostReportAsync(
            DateTime startDate, DateTime endDate, int? residenceId)
        {
            try
            {
                // Tarih aralığına ve varsa rezidans ID'sine göre bakımları filtrele
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

                // Ekipman tipi bazında maliyet
                var costByEquipment = completedMaintenances
                    .Where(m => m.EquipmentId.HasValue)
                    .GroupBy(m => m.EquipmentId.Value)
                    .Select(g => new CostByEquipmentReportDTO
                    {
                        EquipmentId = g.Key,
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
                        LocationName = g.Key.ApartmentId.HasValue ? "Daire " + g.Key.ApartmentId.Value :
                                      (g.Key.BlockId.HasValue ? "Blok " + g.Key.BlockId.Value :
                                      (g.Key.ResidenceId.HasValue ? "Rezidans " + g.Key.ResidenceId.Value : "Bilinmiyor")),
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
                        Month = m.CompletionDate.HasValue ? m.CompletionDate.Value.Month : DateTime.Now.Month, 
                        Year = m.CompletionDate.HasValue ? m.CompletionDate.Value.Year : DateTime.Now.Year 
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
                        Location = m.BlockId.HasValue ? $"Blok {m.BlockId}" : (m.ApartmentId.HasValue ? $"Daire {m.ApartmentId}" : "Genel Alan"),
                        ActualCost = m.ActualCost ?? 0,
                        PercentageOfTotal = totalActualCost > 0 ? (m.ActualCost ?? 0) / totalActualCost * 100 : 0
                    })
                    .ToList();

                // Büyük fark olan bakımlar
                var highVarianceMaintenances = completedMaintenances
                    .Where(m => m.EstimatedCost.HasValue && m.EstimatedCost.Value > 0)
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
                    CostByEquipment = costByEquipment,
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
    }
} 