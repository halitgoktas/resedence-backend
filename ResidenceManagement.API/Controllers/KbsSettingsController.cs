using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.DTOs.KbsIntegration;
using ResidenceManagement.Core.Interfaces.Services;

namespace ResidenceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class KbsSettingsController : ControllerBase
    {
        private readonly IKbsSettingsService _kbsSettingsService;
        private readonly ILogger<KbsSettingsController> _logger;

        public KbsSettingsController(
            IKbsSettingsService kbsSettingsService,
            ILogger<KbsSettingsController> logger)
        {
            _kbsSettingsService = kbsSettingsService;
            _logger = logger;
        }

        /// <summary>
        /// KBS ayarlarını getirir
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>KBS ayarları</returns>
        [HttpGet]
        public async Task<IActionResult> GetKbsSettings([FromQuery] int firmaId, [FromQuery] int subeId)
        {
            _logger.LogInformation("KBS ayarları getiriliyor. FirmaId: {FirmaId}, SubeId: {SubeId}", firmaId, subeId);
            var result = await _kbsSettingsService.GetKbsSettingsAsync(firmaId, subeId);
            return Ok(result);
        }

        /// <summary>
        /// KBS ayarlarını kaydeder
        /// </summary>
        /// <param name="kbsSettingsDto">KBS ayarları</param>
        /// <returns>Sonuç</returns>
        [HttpPost]
        public async Task<IActionResult> SaveKbsSettings([FromBody] KbsSettingsDto kbsSettingsDto)
        {
            _logger.LogInformation("KBS ayarları kaydediliyor. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);
            var result = await _kbsSettingsService.SaveKbsSettingsAsync(kbsSettingsDto);
            return Ok(result);
        }

        /// <summary>
        /// KBS bağlantısını test eder
        /// </summary>
        /// <param name="kbsSettingsDto">Test edilecek KBS ayarları</param>
        /// <returns>Test sonucu</returns>
        [HttpPost("test-connection")]
        public async Task<IActionResult> TestKbsConnection([FromBody] KbsSettingsDto kbsSettingsDto)
        {
            _logger.LogInformation("KBS bağlantısı test ediliyor. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);
            var result = await _kbsSettingsService.TestKbsConnectionAsync(kbsSettingsDto);
            return Ok(result);
        }

        /// <summary>
        /// KBS ayarlarını varsayılan değerlere sıfırlar
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>Sıfırlama sonucu</returns>
        [HttpPost("reset")]
        public async Task<IActionResult> ResetKbsSettings([FromQuery] int firmaId, [FromQuery] int subeId)
        {
            _logger.LogInformation("KBS ayarları sıfırlanıyor. FirmaId: {FirmaId}, SubeId: {SubeId}", firmaId, subeId);
            var result = await _kbsSettingsService.ResetKbsSettingsToDefaultAsync(firmaId, subeId);
            return Ok(result);
        }

        /// <summary>
        /// Otomatik gönderim ayarlarını günceller
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <param name="enabled">Aktif mi?</param>
        /// <param name="hour">Gönderim saati (0-23)</param>
        /// <returns>Güncelleme sonucu</returns>
        [HttpPut("auto-send")]
        public async Task<IActionResult> UpdateAutoSendSettings(
            [FromQuery] int firmaId, 
            [FromQuery] int subeId, 
            [FromQuery] bool enabled, 
            [FromQuery] int hour)
        {
            _logger.LogInformation("KBS otomatik gönderim ayarları güncelleniyor. FirmaId: {FirmaId}, SubeId: {SubeId}, Enabled: {Enabled}, Hour: {Hour}", 
                firmaId, subeId, enabled, hour);
            var result = await _kbsSettingsService.UpdateAutoSendSettingsAsync(firmaId, subeId, enabled, hour);
            return Ok(result);
        }
    }
} 