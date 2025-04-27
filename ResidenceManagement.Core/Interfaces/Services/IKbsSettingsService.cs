using System.Threading.Tasks;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.KbsIntegration;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// KBS ayarları için servis arayüzü
    /// </summary>
    public interface IKbsSettingsService
    {
        /// <summary>
        /// KBS ayarlarını getirir
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>KBS ayarları</returns>
        Task<ApiResponse<KbsSettingsDto>> GetKbsSettingsAsync(int firmaId, int subeId);
        
        /// <summary>
        /// KBS ayarlarını günceller veya oluşturur
        /// </summary>
        /// <param name="kbsSettingsDto">Güncellenecek ayarlar</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<KbsSettingsDto>> SaveKbsSettingsAsync(KbsSettingsDto kbsSettingsDto);
        
        /// <summary>
        /// KBS bağlantı testi yapar
        /// </summary>
        /// <param name="kbsSettingsDto">Test edilecek ayarlar</param>
        /// <returns>Bağlantı test sonucu</returns>
        Task<ApiResponse<bool>> TestKbsConnectionAsync(KbsSettingsDto kbsSettingsDto);
        
        /// <summary>
        /// KBS ayarlarını default değerlere sıfırlar
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<KbsSettingsDto>> ResetKbsSettingsToDefaultAsync(int firmaId, int subeId);
        
        /// <summary>
        /// Otomatik gönderim ayarlarını günceller
        /// </summary>
        /// <param name="firmaId">Firma ID</param>
        /// <param name="subeId">Şube ID</param>
        /// <param name="enabled">Aktif mi?</param>
        /// <param name="hour">Gönderim saati (0-23)</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> UpdateAutoSendSettingsAsync(int firmaId, int subeId, bool enabled, int hour);
    }
} 