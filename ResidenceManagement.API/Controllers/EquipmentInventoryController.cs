using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.DTOs.Equipment;
using ResidenceManagement.Core.Enums;
using ResidenceManagement.Core.Models.Services;
using ResidenceManagement.Core.Services;

// Alias tanımlamaları
using EquipmentFilterDTO = ResidenceManagement.Core.DTOs.Equipment.EquipmentFilterDto;

namespace ResidenceManagement.API.Controllers
{
    // Demirbaş ve ekipman takip sistemi için API kontrolcüsü
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipmentInventoryController : ControllerBase
    {
        private readonly IEquipmentInventoryService _equipmentService;
        private readonly ILogger<EquipmentInventoryController> _logger;

        public EquipmentInventoryController(
            IEquipmentInventoryService equipmentService,
            ILogger<EquipmentInventoryController> logger)
        {
            _equipmentService = equipmentService;
            _logger = logger;
        }

        // Tüm ekipmanları listeler ve filtreleme sunar
        [HttpGet]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> GetEquipments([FromQuery] EquipmentFilterDTO filter)
        {
            try
            {
                var result = await _equipmentService.GetAllEquipmentsAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ekipman listesini alırken hata oluştu");
                return StatusCode(500, "Ekipman listesi alınamadı. Detay: " + ex.Message);
            }
        }

        // Belirli bir ekipmanı getirir
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            try
            {
                var equipment = await _equipmentService.GetEquipmentByIdAsync(id);
                if (equipment == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmanı alırken hata oluştu");
                return StatusCode(500, "Ekipman bilgisi alınamadı. Detay: " + ex.Message);
            }
        }

        // Yeni ekipman ekler
        [HttpPost]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> CreateEquipment([FromBody] EquipmentCreateDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdEquipment = await _equipmentService.CreateEquipmentAsync(equipmentDto);
                return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yeni ekipman eklenirken hata oluştu");
                return StatusCode(500, "Ekipman eklenemedi. Detay: " + ex.Message);
            }
        }

        // Ekipman bilgilerini günceller
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> UpdateEquipment(int id, [FromBody] EquipmentUpdateDto equipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != equipmentDto.Id)
                return BadRequest("URL'deki ID ile request body'deki ID eşleşmiyor");

            try
            {
                var updatedEquipment = await _equipmentService.UpdateEquipmentAsync(equipmentDto);
                if (updatedEquipment == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(updatedEquipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipman güncellenirken hata oluştu");
                return StatusCode(500, "Ekipman güncellenemedi. Detay: " + ex.Message);
            }
        }

        // Ekipman siler (yumuşak silme - IsDeleted=true)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            try
            {
                var result = await _equipmentService.DeleteEquipmentAsync(id);
                if (!result)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipman silinirken hata oluştu");
                return StatusCode(500, "Ekipman silinemedi. Detay: " + ex.Message);
            }
        }

        // Ekipman durumunu günceller
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> UpdateEquipmentStatus(int id, [FromBody] StatusUpdateDto statusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.UpdateEquipmentStatusAsync(id, statusDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmanın durumu güncellenirken hata oluştu");
                return StatusCode(500, "Ekipman durumu güncellenemedi. Detay: " + ex.Message);
            }
        }

        // Ekipmanı başka bir lokasyon veya birime taşır
        [HttpPatch("{id}/relocate")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> RelocateEquipment(int id, [FromBody] EquipmentRelocationDto relocationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.RelocateEquipmentAsync(id, relocationDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipman taşınırken hata oluştu");
                return StatusCode(500, "Ekipman taşınamadı. Detay: " + ex.Message);
            }
        }

        // Bakım kaydı ekler
        [HttpPost("{id}/maintenance")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> AddMaintenanceRecord(int id, [FromBody] MaintenanceRecordDto maintenanceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.AddMaintenanceRecordAsync(id, maintenanceDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmana bakım kaydı eklenirken hata oluştu");
                return StatusCode(500, "Bakım kaydı eklenemedi. Detay: " + ex.Message);
            }
        }

        // Bakım geçmişini getirir
        [HttpGet("{id}/maintenance-history")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> GetMaintenanceHistory(int id)
        {
            try
            {
                var history = await _equipmentService.GetMaintenanceHistoryAsync(id);
                if (history == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmanın bakım geçmişi alınırken hata oluştu");
                return StatusCode(500, "Bakım geçmişi alınamadı. Detay: " + ex.Message);
            }
        }

        // Doküman ekler
        [HttpPost("{id}/documents")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> AddDocument(int id, [FromBody] DocumentDto documentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.AddDocumentAsync(id, documentDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmana doküman eklenirken hata oluştu");
                return StatusCode(500, "Doküman eklenemedi. Detay: " + ex.Message);
            }
        }

        // Yedek parça ekler
        [HttpPost("{id}/parts")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> AddPart(int id, [FromBody] PartDto partDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.AddPartAsync(id, partDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmana yedek parça eklenirken hata oluştu");
                return StatusCode(500, "Yedek parça eklenemedi. Detay: " + ex.Message);
            }
        }

        // Metrik veya ölçüm değerini ekler/günceller
        [HttpPost("{id}/metrics")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> UpdateMetric(int id, [FromBody] MetricUpdateDto metricDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.UpdateMetricAsync(id, metricDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmanın metriği güncellenirken hata oluştu");
                return StatusCode(500, "Metrik güncellenemedi. Detay: " + ex.Message);
            }
        }

        // Aktivite logu getirir
        [HttpGet("{id}/activity-log")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> GetActivityLog(int id)
        {
            try
            {
                var logEntries = await _equipmentService.GetActivityLogAsync(id);
                if (logEntries == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(logEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmanın aktivite logu alınırken hata oluştu");
                return StatusCode(500, "Aktivite logu alınamadı. Detay: " + ex.Message);
            }
        }

        // Fotoğraf yükler
        [HttpPost("{id}/photos")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] PhotoDto photoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.AddPhotoAsync(id, photoDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipmana fotoğraf eklenirken hata oluştu");
                return StatusCode(500, "Fotoğraf eklenemedi. Detay: " + ex.Message);
            }
        }

        // Kategoriye göre ekipman sayısı
        [HttpGet("reports/by-category")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> GetEquipmentsByCategory()
        {
            try
            {
                var report = await _equipmentService.GetEquipmentsByCategoryAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriye göre ekipman sayısı raporlanırken hata oluştu");
                return StatusCode(500, "Rapor alınamadı. Detay: " + ex.Message);
            }
        }

        // Binaya göre ekipman durumu raporu
        [HttpGet("reports/by-building")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> GetEquipmentsByBuilding()
        {
            try
            {
                var report = await _equipmentService.GetEquipmentsByBuildingAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Binaya göre ekipman raporu alınırken hata oluştu");
                return StatusCode(500, "Rapor alınamadı. Detay: " + ex.Message);
            }
        }

        // Yaklaşan bakımları getirir
        [HttpGet("reports/upcoming-maintenances")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> GetUpcomingMaintenances([FromQuery] DateTime? endDate)
        {
            try
            {
                var report = await _equipmentService.GetUpcomingMaintenancesAsync(endDate ?? DateTime.Now.AddDays(30));
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yaklaşan bakımlar raporlanırken hata oluştu");
                return StatusCode(500, "Rapor alınamadı. Detay: " + ex.Message);
            }
        }

        // Bakım maliyeti raporu
        [HttpGet("reports/maintenance-costs")]
        [Authorize(Roles = "Admin,SiteYonetici")]
        public async Task<IActionResult> GetMaintenanceCosts([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var report = await _equipmentService.GetMaintenanceCostsAsync(
                    startDate ?? DateTime.Now.AddMonths(-6), 
                    endDate ?? DateTime.Now);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bakım maliyeti raporu alınırken hata oluştu");
                return StatusCode(500, "Rapor alınamadı. Detay: " + ex.Message);
            }
        }

        // QR Kod veya Barkod ile ekipman arama
        [HttpGet("search/barcode/{barcode}")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> GetEquipmentByBarcode(string barcode)
        {
            try
            {
                var equipment = await _equipmentService.GetEquipmentByBarcodeAsync(barcode);
                if (equipment == null)
                    return NotFound($"Barkod: {barcode} olan ekipman bulunamadı");

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Barkod: {barcode} olan ekipmana erişilirken hata oluştu");
                return StatusCode(500, "Ekipman bilgisi alınamadı. Detay: " + ex.Message);
            }
        }
        
        // Bakım takvimine ekipman ekleme
        [HttpPost("{id}/schedule-maintenance")]
        [Authorize(Roles = "Admin,SiteYonetici,TeknikServis")]
        public async Task<IActionResult> ScheduleMaintenance(int id, [FromBody] MaintenanceScheduleDto scheduleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _equipmentService.ScheduleMaintenanceAsync(id, scheduleDto);
                if (result == null)
                    return NotFound($"ID: {id} olan ekipman bulunamadı");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID: {id} olan ekipman için bakım planlanırken hata oluştu");
                return StatusCode(500, "Bakım planlanamadı. Detay: " + ex.Message);
            }
        }
    }
} 