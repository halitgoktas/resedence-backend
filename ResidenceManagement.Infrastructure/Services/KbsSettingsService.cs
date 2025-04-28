using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.KbsIntegration;
using ResidenceManagement.Core.Entities.Kbs;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Interfaces.Services;

namespace ResidenceManagement.Infrastructure.Services
{
    /// <summary>
    /// KBS ayarları için servis implementasyonu
    /// </summary>
    public class KbsSettingsService : IKbsSettingsService
    {
        private readonly IRepository<KbsSettings> _kbsSettingsRepository;
        private readonly ResidenceManagement.Core.Interfaces.Services.IKbsIntegrationService _kbsIntegrationService;
        private readonly ILogger<KbsSettingsService> _logger;

        /// <summary>
        /// KbsSettingsService constructor
        /// </summary>
        public KbsSettingsService(
            IRepository<KbsSettings> kbsSettingsRepository,
            ResidenceManagement.Core.Interfaces.Services.IKbsIntegrationService kbsIntegrationService,
            ILogger<KbsSettingsService> logger)
        {
            _kbsSettingsRepository = kbsSettingsRepository ?? throw new ArgumentNullException(nameof(kbsSettingsRepository));
            _kbsIntegrationService = kbsIntegrationService ?? throw new ArgumentNullException(nameof(kbsIntegrationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// KBS ayarlarını getirir
        /// </summary>
        public async Task<ApiResponse<KbsSettingsDto>> GetKbsSettingsAsync(int firmaId, int subeId)
        {
            try
            {
                _logger.LogInformation("KBS ayarları getiriliyor. FirmaId: {FirmaId}, SubeId: {SubeId}", firmaId, subeId);
                
                var settings = await _kbsSettingsRepository.GetFirstOrDefaultAsync(
                    x => x.FirmaId == firmaId && x.SubeId == subeId && x.IsActive && !x.IsDeleted);

                if (settings == null)
                {
                    _logger.LogInformation("KBS ayarları bulunamadı. Varsayılan değerler döndürülüyor.");
                    return ApiResponse<KbsSettingsDto>.Success(new KbsSettingsDto
                    {
                        FirmaId = firmaId,
                        SubeId = subeId,
                        IsActive = true,
                        TimeoutSeconds = 30,
                        MaxRetryCount = 3
                    });
                }

                var settingsDto = new KbsSettingsDto
                {
                    ServiceUrl = settings.ServiceUrl,
                    Username = settings.Username,
                    Password = settings.Password,
                    TesisKodu = settings.TesisKodu,
                    IsletmeAdi = settings.IsletmeAdi,
                    MersisNo = settings.MersisNo,
                    TimeoutSeconds = settings.TimeoutSeconds,
                    MaxRetryCount = settings.MaxRetryCount,
                    IsActive = settings.IsActive,
                    FirmaId = settings.FirmaId,
                    SubeId = settings.SubeId
                };

                return ApiResponse<KbsSettingsDto>.Success(settingsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS ayarları getirilirken hata oluştu. FirmaId: {FirmaId}, SubeId: {SubeId}", firmaId, subeId);
                return ApiResponse<KbsSettingsDto>.Failure("KBS ayarları getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// KBS ayarlarını günceller veya oluşturur
        /// </summary>
        public async Task<ApiResponse<KbsSettingsDto>> SaveKbsSettingsAsync(KbsSettingsDto kbsSettingsDto)
        {
            try
            {
                _logger.LogInformation("KBS ayarları kaydediliyor. FirmaId: {FirmaId}, SubeId: {SubeId}", kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);

                var existingSettings = await _kbsSettingsRepository.GetFirstOrDefaultAsync(
                    x => x.FirmaId == kbsSettingsDto.FirmaId && x.SubeId == kbsSettingsDto.SubeId && !x.IsDeleted);

                if (existingSettings == null)
                {
                    // Yeni ayarlar oluştur
                    var newSettings = new KbsSettings
                    {
                        ServiceUrl = kbsSettingsDto.ServiceUrl,
                        Username = kbsSettingsDto.Username,
                        Password = kbsSettingsDto.Password,
                        TesisKodu = kbsSettingsDto.TesisKodu,
                        IsletmeAdi = kbsSettingsDto.IsletmeAdi,
                        MersisNo = kbsSettingsDto.MersisNo,
                        TimeoutSeconds = kbsSettingsDto.TimeoutSeconds,
                        MaxRetryCount = kbsSettingsDto.MaxRetryCount,
                        IsActive = kbsSettingsDto.IsActive,
                        FirmaId = kbsSettingsDto.FirmaId,
                        SubeId = kbsSettingsDto.SubeId,
                        CreatedDate = DateTime.Now
                    };

                    await _kbsSettingsRepository.AddAsync(newSettings);
                    await _kbsSettingsRepository.SaveChangesAsync();
                    
                    _logger.LogInformation("Yeni KBS ayarları oluşturuldu. ID: {Id}", newSettings.Id);
                }
                else
                {
                    // Mevcut ayarları güncelle
                    existingSettings.ServiceUrl = kbsSettingsDto.ServiceUrl;
                    existingSettings.Username = kbsSettingsDto.Username;
                    existingSettings.Password = kbsSettingsDto.Password;
                    existingSettings.TesisKodu = kbsSettingsDto.TesisKodu;
                    existingSettings.IsletmeAdi = kbsSettingsDto.IsletmeAdi;
                    existingSettings.MersisNo = kbsSettingsDto.MersisNo;
                    existingSettings.TimeoutSeconds = kbsSettingsDto.TimeoutSeconds;
                    existingSettings.MaxRetryCount = kbsSettingsDto.MaxRetryCount;
                    existingSettings.IsActive = kbsSettingsDto.IsActive;
                    existingSettings.UpdatedDate = DateTime.Now;

                    await _kbsSettingsRepository.UpdateAsync(existingSettings);
                    await _kbsSettingsRepository.SaveChangesAsync();
                    
                    _logger.LogInformation("KBS ayarları güncellendi. ID: {Id}", existingSettings.Id);
                }

                return ApiResponse<KbsSettingsDto>.Success(kbsSettingsDto, "KBS ayarları başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS ayarları kaydedilirken hata oluştu. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);
                return ApiResponse<KbsSettingsDto>.Failure("KBS ayarları kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// KBS bağlantı testi yapar
        /// </summary>
        public async Task<ApiResponse<bool>> TestKbsConnectionAsync(KbsSettingsDto kbsSettingsDto)
        {
            try
            {
                _logger.LogInformation("KBS bağlantı testi yapılıyor. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);
                
                // KBS entegrasyon servisini kullanarak bağlantı testi yap
                var loginResponse = await _kbsIntegrationService.LoginToKbsAsync(
                    kbsSettingsDto.Username, 
                    kbsSettingsDto.Password, 
                    kbsSettingsDto.TesisKodu);
                
                if (loginResponse.IsSuccess)
                {
                    return ApiResponse<bool>.Success(true, "KBS bağlantı testi başarılı.");
                }
                else
                {
                    return ApiResponse<bool>.Failure("KBS bağlantı testi başarısız: " + loginResponse.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bağlantı testi sırasında hata oluştu. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    kbsSettingsDto.FirmaId, kbsSettingsDto.SubeId);
                return ApiResponse<bool>.Failure("KBS bağlantı testi sırasında bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// KBS ayarlarını default değerlere sıfırlar
        /// </summary>
        public async Task<ApiResponse<KbsSettingsDto>> ResetKbsSettingsToDefaultAsync(int firmaId, int subeId)
        {
            try
            {
                _logger.LogInformation("KBS ayarları varsayılan değerlere sıfırlanıyor. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    firmaId, subeId);
                
                var existingSettings = await _kbsSettingsRepository.GetFirstOrDefaultAsync(
                    x => x.FirmaId == firmaId && x.SubeId == subeId && !x.IsDeleted);
                
                if (existingSettings != null)
                {
                    // Mevcut ayarları devre dışı bırak, silme
                    existingSettings.IsActive = false;
                    existingSettings.UpdatedDate = DateTime.Now;
                    
                    await _kbsSettingsRepository.UpdateAsync(existingSettings);
                    await _kbsSettingsRepository.SaveChangesAsync();
                    
                    _logger.LogInformation("Eski KBS ayarları devre dışı bırakıldı. ID: {Id}", existingSettings.Id);
                }
                
                // Varsayılan ayarları oluştur
                var defaultSettings = new KbsSettingsDto
                {
                    ServiceUrl = "https://kbs.gov.tr/api",
                    Username = "",
                    Password = "",
                    TesisKodu = "",
                    IsletmeAdi = "",
                    MersisNo = "",
                    TimeoutSeconds = 30,
                    MaxRetryCount = 3,
                    IsActive = true,
                    FirmaId = firmaId,
                    SubeId = subeId
                };
                
                // Yeni varsayılan ayarları kaydet
                var saveResponse = await SaveKbsSettingsAsync(defaultSettings);
                
                if (saveResponse.IsSuccess)
                {
                    return ApiResponse<KbsSettingsDto>.Success(defaultSettings, "KBS ayarları varsayılan değerlere sıfırlandı.");
                }
                else
                {
                    return ApiResponse<KbsSettingsDto>.Failure(saveResponse.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS ayarları sıfırlanırken hata oluştu. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    firmaId, subeId);
                return ApiResponse<KbsSettingsDto>.Failure("KBS ayarları sıfırlanırken bir hata oluştu: " + ex.Message);
            }
        }

        /// <summary>
        /// Otomatik gönderim ayarlarını günceller
        /// </summary>
        public async Task<ApiResponse<bool>> UpdateAutoSendSettingsAsync(int firmaId, int subeId, bool enabled, int hour)
        {
            try
            {
                _logger.LogInformation("KBS otomatik gönderim ayarları güncelleniyor. FirmaId: {FirmaId}, SubeId: {SubeId}, Enabled: {Enabled}, Hour: {Hour}", 
                    firmaId, subeId, enabled, hour);
                
                if (hour < 0 || hour > 23)
                {
                    return ApiResponse<bool>.Failure("Geçersiz saat değeri. Saat 0-23 arasında olmalıdır.");
                }
                
                var settings = await _kbsSettingsRepository.GetFirstOrDefaultAsync(
                    x => x.FirmaId == firmaId && x.SubeId == subeId && x.IsActive && !x.IsDeleted);
                
                if (settings == null)
                {
                    return ApiResponse<bool>.Failure("KBS ayarları bulunamadı.");
                }
                
                settings.AutoSendEnabled = enabled;
                settings.AutoSendHour = hour;
                settings.UpdatedDate = DateTime.Now;
                
                await _kbsSettingsRepository.UpdateAsync(settings);
                await _kbsSettingsRepository.SaveChangesAsync();
                
                _logger.LogInformation("KBS otomatik gönderim ayarları güncellendi. ID: {Id}", settings.Id);
                
                return ApiResponse<bool>.Success(true, "KBS otomatik gönderim ayarları güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS otomatik gönderim ayarları güncellenirken hata oluştu. FirmaId: {FirmaId}, SubeId: {SubeId}", 
                    firmaId, subeId);
                return ApiResponse<bool>.Failure("KBS otomatik gönderim ayarları güncellenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
} 