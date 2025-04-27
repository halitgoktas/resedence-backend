using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.Models.KBS;
using ResidenceManagement.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.API.Controllers
{
    // KBS (Kimlik Bildirme Sistemi) entegrasyonu için controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KbsIntegrationController : ControllerBase
    {
        // DI ile servis enjekte edilecek
        private readonly IKbsIntegrationService _kbsService;
        private readonly ILogger<KbsIntegrationController> _logger;

        // Yapıcı metot
        public KbsIntegrationController(IKbsIntegrationService kbsService, ILogger<KbsIntegrationController> logger)
        {
            _kbsService = kbsService;
            _logger = logger;
        }

        // KBS bildirimleri listele
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<PaginatedResultDto<KbsNotificationDto>>> GetNotifications([FromQuery] KbsNotificationFilterDto filter)
        {
            try
            {
                _logger.LogInformation("GetNotifications methodu çağrıldı");
                var result = await _kbsService.GetNotificationsAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimleri listelenirken hata oluştu");
                return StatusCode(500, new { message = "KBS bildirimleri listelenirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirim detayını getir
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<KbsNotificationDetailDto>> GetNotificationById(int id)
        {
            try
            {
                var notification = await _kbsService.GetNotificationByIdAsync(id);
                if (notification == null)
                    return NotFound(new { message = $"ID: {id} numaralı bildirim bulunamadı" });

                return Ok(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirim detayı alınırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirim detayı alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Yeni KBS bildirimi oluştur
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<KbsNotificationDto>> CreateNotification([FromBody] KbsNotificationCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _kbsService.CreateNotificationAsync(model);
                return CreatedAtAction(nameof(GetNotificationById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi oluşturulurken hata oluştu");
                return StatusCode(500, new { message = "KBS bildirimi oluşturulurken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi güncelle
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<KbsNotificationDto>> UpdateNotification(int id, [FromBody] KbsNotificationUpdateDto model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest(new { message = "ID değerleri uyuşmuyor" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _kbsService.UpdateNotificationAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi güncellenirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirimi güncellenirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi sil
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteNotification(int id)
        {
            try
            {
                var result = await _kbsService.DeleteNotificationAsync(id);
                if (!result)
                    return NotFound(new { message = $"ID: {id} numaralı bildirim bulunamadı" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi silinirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirimi silinirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi gönder
        [HttpPost("{id}/submit")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<KbsNotificationStatusDto>> SubmitToKbs(int id)
        {
            try
            {
                var result = await _kbsService.SubmitToKbsAsync(id, User.Identity.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi gönderilirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirimi gönderilirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi tekrar gönder
        [HttpPost("{id}/resubmit")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<KbsNotificationStatusDto>> ResubmitToKbs(int id)
        {
            try
            {
                var result = await _kbsService.ResubmitToKbsAsync(id, User.Identity.Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi tekrar gönderilirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirimi tekrar gönderilirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi iptal et
        [HttpPost("{id}/cancel")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<KbsNotificationStatusDto>> CancelNotification(int id, [FromBody] CancellationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.CancellationReason))
                    return BadRequest(new { message = "İptal nedeni belirtilmelidir" });

                var result = await _kbsService.CancelNotificationAsync(id, User.Identity.Name, request.CancellationReason);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi iptal edilirken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirimi iptal edilirken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS bildirimi loglarını getir
        [HttpGet("{id}/logs")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<List<KbsNotificationLogDto>>> GetNotificationLogs(int id)
        {
            try
            {
                var logs = await _kbsService.GetNotificationLogsAsync(id);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirim logları alınırken hata oluştu. ID: {Id}", id);
                return StatusCode(500, new { message = "KBS bildirim logları alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // Rezervasyondan KBS bildirimi oluştur
        [HttpPost("from-reservation/{reservationId}")]
        [Authorize(Roles = "Admin,Manager,Receptionist")]
        public async Task<ActionResult<KbsNotificationDto>> CreateFromReservation(int reservationId)
        {
            try
            {
                var result = await _kbsService.CreateFromReservationAsync(reservationId);
                return CreatedAtAction(nameof(GetNotificationById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rezervasyondan KBS bildirimi oluşturulurken hata oluştu. ReservationID: {ReservationId}", reservationId);
                return StatusCode(500, new { message = "Rezervasyondan KBS bildirimi oluşturulurken bir hata oluştu", error = ex.Message });
            }
        }

        // KBS durum raporu
        [HttpGet("status-report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<KbsStatusReportDto>> GetStatusReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var report = await _kbsService.GetStatusReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS durum raporu alınırken hata oluştu");
                return StatusCode(500, new { message = "KBS durum raporu alınırken bir hata oluştu", error = ex.Message });
            }
        }

        // İptal nedeni modeli
        public class CancellationRequest
        {
            [Required(ErrorMessage = "İptal nedeni zorunludur")]
            [StringLength(500, ErrorMessage = "İptal nedeni en fazla 500 karakter olabilir")]
            public string CancellationReason { get; set; }
        }
    }
} 