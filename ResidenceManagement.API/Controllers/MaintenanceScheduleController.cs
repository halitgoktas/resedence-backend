using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.DTOs.Maintenance;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Interfaces.Services;

// Alias tanımlamaları
using MaintenanceDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDTO;
using MaintenanceDetailDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleDetailDTO;
using MaintenanceCreateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleCreateDTO;
using MaintenanceUpdateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceScheduleUpdateDTO;
using MaintenanceChecklistDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceChecklistItemDTO;
using MaintenanceDocDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceDocumentDTO;
using MaintenanceLogDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceLogDTO;
using MaintenanceRptDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceReportDTO;
using MaintenanceCostRptDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceCostReportDTO;
using MaintenanceChecklistUpdateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceChecklistUpdateDTO;
using MaintenanceDocumentCreateDTO = ResidenceManagement.Core.DTOs.Maintenance.MaintenanceDocumentCreateDTO;

namespace ResidenceManagement.API.Controllers
{
    // Bakım takvimi işlemleri için controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MaintenanceScheduleController : ControllerBase
    {
        private readonly IMaintenanceScheduleService _maintenanceService;
        private readonly ILogger<MaintenanceScheduleController> _logger;

        // Constructor
        public MaintenanceScheduleController(
            IMaintenanceScheduleService maintenanceService,
            ILogger<MaintenanceScheduleController> logger)
        {
            _maintenanceService = maintenanceService;
            _logger = logger;
        }

        // Bakım takvimini listeleme
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetMaintenanceSchedules(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? residenceId,
            [FromQuery] int? blockId,
            [FromQuery] string maintenanceType,
            [FromQuery] string status)
        {
            try
            {
                var schedules = await _maintenanceService.GetMaintenanceSchedulesAsync(
                    startDate, endDate, residenceId, blockId, maintenanceType, status);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım takvimi listelenirken hata oluştu");
                return StatusCode(500, new { message = "Bakım takvimi listelenirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım planı detayı getirme
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<MaintenanceDetailDTO>> GetMaintenanceScheduleById(int id)
        {
            try
            {
                var schedule = await _maintenanceService.GetMaintenanceScheduleByIdAsync(id);
                
                if (schedule == null)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı detayı alınırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım planı detayı alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Yeni bakım planı oluşturma
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MaintenanceDetailDTO>> CreateMaintenanceSchedule(
            [FromBody] MaintenanceCreateDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var scheduleId = await _maintenanceService.CreateMaintenanceScheduleAsync(model);
                var createdSchedule = await _maintenanceService.GetMaintenanceScheduleByIdAsync(scheduleId);
                
                return CreatedAtAction(nameof(GetMaintenanceScheduleById), new { id = scheduleId }, createdSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı oluşturulurken hata oluştu");
                return StatusCode(500, new { message = "Bakım planı oluşturulurken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım planı güncelleme
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MaintenanceDetailDTO>> UpdateMaintenanceSchedule(
            int id, [FromBody] MaintenanceUpdateDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                if (id != model.Id)
                    return BadRequest(new { message = "ID değerleri uyuşmuyor" });
                
                var success = await _maintenanceService.UpdateMaintenanceScheduleAsync(model);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                var updatedSchedule = await _maintenanceService.GetMaintenanceScheduleByIdAsync(id);
                return Ok(updatedSchedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı güncellenirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım planı güncellenirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım planı silme
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteMaintenanceSchedule(int id)
        {
            try
            {
                var success = await _maintenanceService.DeleteMaintenanceScheduleAsync(id);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım planı silinirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım planı silinirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım durumu güncelleme
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult> UpdateMaintenanceStatus(
            int id, [FromBody] StatusUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var success = await _maintenanceService.UpdateMaintenanceStatusAsync(id, request.Status, request.Notes);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(new { message = "Bakım durumu başarıyla güncellendi" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım durumu güncellenirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım durumu güncellenirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım görevini atama
        [HttpPut("{id}/assign")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> AssignMaintenance(
            int id, [FromBody] AssignmentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var success = await _maintenanceService.AssignMaintenanceAsync(
                    id, request.AssignedToUserId, request.AssignedToUserName, request.Notes);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(new { message = "Bakım görevi başarıyla atandı" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım görevi atanırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım görevi atanırken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım tamamlama
        [HttpPut("{id}/complete")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult> CompleteMaintenance(
            int id, [FromBody] CompletionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var success = await _maintenanceService.CompleteMaintenanceAsync(
                    id, request.CompletionNotes, request.ActualCost, request.ActualDurationMinutes);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(new { message = "Bakım başarıyla tamamlandı" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım tamamlanırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım tamamlanırken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım iptal etme
        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> CancelMaintenance(
            int id, [FromBody] CancellationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var success = await _maintenanceService.CancelMaintenanceAsync(id, request.CancellationReason);
                
                if (!success)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(new { message = "Bakım başarıyla iptal edildi" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım iptal edilirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım iptal edilirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım kontrol listesi güncelleme
        [HttpPut("{id}/checklist")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult> UpdateChecklist(
            int id, [FromBody] MaintenanceChecklistUpdateDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var response = await _maintenanceService.UpdateMaintenanceChecklistAsync(id, model);
                
                if (!response.IsSuccess)
                    return StatusCode((int)response.StatusCode, response);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kontrol listesi güncellenirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Kontrol listesi güncellenirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım belgesi ekleme
        [HttpPost("{id}/documents")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<int>> AddDocument(
            int id, [FromBody] MaintenanceDocumentCreateDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var response = await _maintenanceService.AddMaintenanceDocumentAsync(id, model);
                
                if (!response.IsSuccess)
                    return StatusCode((int)response.StatusCode, response);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım belgesi eklenirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım belgesi eklenirken bir hata oluştu", error = ex.Message });
            }
        }

        // Bakım loglarını getirme
        [HttpGet("{id}/logs")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<List<MaintenanceLogDTO>>> GetMaintenanceLogs(int id)
        {
            try
            {
                var logs = await _maintenanceService.GetMaintenanceLogsAsync(id);
                
                if (logs == null)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı" });
                
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım logları alınırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "Bakım logları alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Ekipmana göre bakım planları
        [HttpGet("by-equipment/{equipmentId}")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetMaintenanceSchedulesByEquipment(int equipmentId)
        {
            try
            {
                var schedules = await _maintenanceService.GetMaintenanceSchedulesByEquipmentAsync(equipmentId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ekipman bakım planları alınırken hata oluştu. EquipmentID: {EquipmentId}", equipmentId);
                return StatusCode(500, new { message = "Ekipman bakım planları alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Daireye göre bakım planları
        [HttpGet("by-apartment/{apartmentId}")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetMaintenanceSchedulesByApartment(int apartmentId)
        {
            try
            {
                var schedules = await _maintenanceService.GetMaintenanceSchedulesByApartmentAsync(apartmentId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Daire bakım planları alınırken hata oluştu. ApartmentID: {ApartmentId}", apartmentId);
                return StatusCode(500, new { message = "Daire bakım planları alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Kullanıcıya atanan bakım planları
        [HttpGet("by-user/{userId}")]
        [Authorize(Roles = "Admin,Manager,TechnicalStaff")]
        public async Task<ActionResult<List<MaintenanceDTO>>> GetMaintenanceSchedulesByUser(int userId)
        {
            try
            {
                var schedules = await _maintenanceService.GetMaintenanceSchedulesByAssignedUserAsync(userId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bakım planları alınırken hata oluştu. UserID: {UserId}", userId);
                return StatusCode(500, new { message = "Kullanıcı bakım planları alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Tekrarlanan bakımdan yeni örnek oluşturma
        [HttpPost("{id}/recurrence-instance")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<int>> CreateRecurrenceInstance(int id)
        {
            try
            {
                var newScheduleId = await _maintenanceService.CreateRecurrenceInstanceAsync(id);
                
                if (newScheduleId <= 0)
                    return NotFound(new { message = $"ID: {id} numaralı bakım planı bulunamadı veya tekrarlanabilir değil" });
                
                return Ok(new { 
                    scheduleId = newScheduleId, 
                    message = "Tekrarlanan bakımdan yeni örnek oluşturuldu" 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tekrarlanan bakımdan yeni örnek oluşturulurken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { 
                    message = "Tekrarlanan bakımdan yeni örnek oluşturulurken bir hata oluştu", 
                    error = ex.Message 
                });
            }
        }

        // Yaklaşan bakım planları raporu
        [HttpGet("upcoming-report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MaintenanceRptDTO>> GetUpcomingMaintenanceReport(
            [FromQuery] int? residenceId, [FromQuery] int days = 30)
        {
            try
            {
                var report = await _maintenanceService.GetUpcomingMaintenanceReportAsync(residenceId, days);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yaklaşan bakım planları raporu alınırken hata oluştu");
                return StatusCode(500, new { 
                    message = "Yaklaşan bakım planları raporu alınırken bir hata oluştu", 
                    error = ex.Message 
                });
            }
        }

        // Bakım maliyet raporu
        [HttpGet("cost-report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MaintenanceCostRptDTO>> GetMaintenanceCostReport(
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? residenceId)
        {
            try
            {
                var report = await _maintenanceService.GetMaintenanceCostReportAsync(startDate, endDate, residenceId);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım maliyet raporu alınırken hata oluştu");
                return StatusCode(500, new { 
                    message = "Bakım maliyet raporu alınırken bir hata oluştu", 
                    error = ex.Message 
                });
            }
        }

        // Durum güncelleme isteği sınıfı
        public class StatusUpdateRequest
        {
            public string Status { get; set; }
            public string Notes { get; set; }
        }

        // Atama isteği sınıfı
        public class AssignmentRequest
        {
            public int AssignedToUserId { get; set; }
            public string AssignedToUserName { get; set; }
            public string Notes { get; set; }
        }

        // Tamamlama isteği sınıfı
        public class CompletionRequest
        {
            public string CompletionNotes { get; set; }
            public decimal? ActualCost { get; set; }
            public int? ActualDurationMinutes { get; set; }
        }

        // İptal isteği sınıfı
        public class CancellationRequest
        {
            public string CancellationReason { get; set; }
        }
    }
} 