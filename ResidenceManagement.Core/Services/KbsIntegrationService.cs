using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Models.KBS;
using ResidenceManagement.Core.Exceptions;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.DTOs;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Person;
using ResidenceManagement.Core.Entities.Property;
using ResidenceManagement.Core.Interfaces.Services;
using ResidenceManagement.Core.Entities.Kbs;
using System.Linq;

namespace ResidenceManagement.Core.Services
{
    /// <summary>
    /// KBS (Kimlik Bildirme Sistemi) entegrasyonu için servis sınıfı
    /// </summary>
    public class KbsIntegrationService : IKbsIntegrationService, IMultiTenant
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<KbsIntegrationService> _logger;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        // Multi-tenant özellikler
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        /// <summary>
        /// KBS entegrasyon servisi constructor
        /// </summary>
        public KbsIntegrationService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<KbsIntegrationService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            
            // Konfigürasyon dosyasından KBS api bilgilerini al
            _baseUrl = _configuration["KbsIntegration:BaseUrl"];
            _apiKey = _configuration["KbsIntegration:ApiKey"];
            _apiSecret = _configuration["KbsIntegration:ApiSecret"];
            
            // HTTP istemcisini yapılandır
            ConfigureHttpClient();
        }

        /// <summary>
        /// HTTP istemci yapılandırması
        /// </summary>
        private void ConfigureHttpClient()
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                _logger.LogWarning("KBS API URL tanımlı değil.");
                return;
            }

            _httpClient.BaseAddress = new Uri(_baseUrl);
            
            if (!string.IsNullOrEmpty(_apiKey))
                _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
                
            if (!string.IsNullOrEmpty(_apiSecret))
                _httpClient.DefaultRequestHeaders.Add("X-Api-Secret", _apiSecret);
                
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// KBS'ye bildirim gönderir
        /// </summary>
        public async Task<KbsNotification> SendNotificationAsync(KbsNotification notification)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi gönderiliyor. Bildirim ID: {NotificationId}", notification.Id);
                
                // Bildirimi KBS formatına dönüştür
                var kbsRequest = MapToKbsRequest(notification);
                
                // KBS API'sine POST isteği gönder
                var response = await _httpClient.PostAsJsonAsync("/api/notifications", kbsRequest);
                
                // Yanıtı kontrol et
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    // KBS'den gelen referans numarasını kaydet
                    try 
                    {
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (jsonResponse.TryGetProperty("referenceNumber", out var refNumber))
                        {
                            notification.ReferenceId = refNumber.GetString();
                        }
                    }
                    catch {}
                    
                    // Bildirimin durumunu güncelle
                    notification.Status = KbsNotificationStatus.Submitted;
                    notification.NotificationDate = DateTime.Now;
                    notification.StatusMessage = "KBS bildirimi başarıyla gönderildi";
                    
                    _logger.LogInformation("KBS bildirimi başarıyla gönderildi. Referans No: {ReferenceNumber}", notification.ReferenceId);
                    return notification;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("KBS bildirimi gönderilemedi. Hata: {ErrorContent}", errorContent);
                    
                    // Bildirimin durumunu hata olarak güncelle
                    notification.Status = KbsNotificationStatus.Failed;
                    notification.StatusMessage = $"KBS bildirimi gönderilemedi: {errorContent}";
                    notification.RetryCount++;
                    
                    return notification;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi gönderilirken hata oluştu");
                
                // Bildirimin durumunu hata olarak güncelle
                notification.Status = KbsNotificationStatus.Failed;
                notification.StatusMessage = $"KBS bildirimi gönderilirken hata: {ex.Message}";
                notification.RetryCount++;
                
                return notification;
            }
        }

        /// <summary>
        /// KBS'ye toplu bildirim gönderir
        /// </summary>
        public async Task<List<KbsNotification>> SendBatchNotificationsAsync(List<KbsNotification> notifications)
        {
            try
            {
                _logger.LogInformation("KBS'ye {Count} adet toplu bildirim gönderiliyor", notifications.Count);
                var processedNotifications = new List<KbsNotification>();
                
                foreach (var notification in notifications)
                {
                    try
                    {
                        var result = await SendNotificationAsync(notification);
                        processedNotifications.Add(result);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Bildirim gönderilirken hata oluştu. Bildirim ID: {NotificationId}", notification.Id);
                        notification.Status = KbsNotificationStatus.Failed;
                        notification.StatusMessage = $"Bildirim işlenirken hata oluştu: {ex.Message}";
                        notification.RetryCount++;
                        processedNotifications.Add(notification);
                    }
                }
                
                _logger.LogInformation("Toplu bildirim işlemi tamamlandı. Toplam: {Total}, Başarılı: {Success}, Başarısız: {Failed}",
                    processedNotifications.Count,
                    processedNotifications.Count(n => n.Status == KbsNotificationStatus.Submitted),
                    processedNotifications.Count(n => n.Status == KbsNotificationStatus.Failed));
                
                return processedNotifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu bildirim işlemi sırasında beklenmeyen hata oluştu");
                throw new KbsIntegrationException("Toplu bildirim işlemi sırasında beklenmeyen hata oluştu", ex);
            }
        }

        /// <summary>
        /// KBS'ye bildirim gönderir
        /// </summary>
        public async Task<KbsSubmissionResultDto> SubmitNotificationAsync(KbsNotification notification)
        {
            try
            {
                _logger.LogInformation("KBS'ye bildirim gönderiliyor. Bildirim ID: {NotificationId}", notification.Id);
                
                // SendNotificationAsync metodunu kullanarak bildirim gönder
                var result = await SendNotificationAsync(notification);
                
                // Submission result oluştur
                var submissionResult = new KbsSubmissionResultDto
                {
                    Success = result.Status == KbsNotificationStatus.Submitted,
                    StatusCode = result.Status == KbsNotificationStatus.Submitted ? "200" : "400",
                    Message = result.StatusMessage,
                    ReferenceNumber = result.ReferenceId,
                    SubmissionDate = DateTime.Now
                };
                
                if (submissionResult.Success)
                {
                    _logger.LogInformation("KBS bildirimi başarıyla gönderildi. Bildirim ID: {NotificationId}", notification.Id);
                }
                else
                {
                    _logger.LogError("KBS bildirimi gönderilemedi. Bildirim ID: {NotificationId}, Hata: {ErrorMessage}", 
                        notification.Id, result.StatusMessage);
                }
                
                return submissionResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi gönderilirken hata oluştu. Bildirim ID: {NotificationId}", notification.Id);
                
                return new KbsSubmissionResultDto
                {
                    Success = false,
                    StatusCode = "500",
                    Message = $"KBS bildirimi gönderilirken hata oluştu: {ex.Message}",
                    SubmissionDate = DateTime.Now
                };
            }
        }

        /// <summary>
        /// KBS bildirim durumunu kontrol eder
        /// </summary>
        public async Task<KbsNotificationLog> CheckNotificationStatusAsync(string referenceNumber)
        {
            try
            {
                _logger.LogInformation("KBS bildirim durumu kontrol ediliyor. Referans No: {ReferenceNumber}", referenceNumber);
                
                // KBS API'sine GET isteği gönder
                var response = await _httpClient.GetAsync($"/api/notifications/status/{referenceNumber}");
                
                // Yeni bir log kaydı oluştur
                var log = new KbsNotificationLog
                {
                    ReferenceId = referenceNumber,
                    ActionType = KbsLogActionType.CheckStatus,
                    LogDate = DateTime.Now,
                    LogLevel = "Information",
                    Message = "KBS bildirim durumu sorgulandı"
                };
                
                // Yanıtı kontrol et
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    log.ResponseContent = responseContent;
                    log.StatusCode = ((int)response.StatusCode).ToString();
                    log.IsSuccess = true;
                    
                    try 
                    {
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (jsonResponse.TryGetProperty("status", out var status))
                        {
                            log.Message = $"Bildirim durumu: {status.GetString()}";
                        }
                    }
                    catch (Exception ex) 
                    {
                        _logger.LogWarning(ex, "KBS yanıtı JSON formatında değil");
                    }
                    
                    _logger.LogInformation("KBS bildirim durumu başarıyla alındı. Referans No: {ReferenceNumber}", referenceNumber);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    log.ResponseContent = errorContent;
                    log.StatusCode = ((int)response.StatusCode).ToString();
                    log.IsSuccess = false;
                    log.Message = $"KBS bildirim durumu alınamadı: {errorContent}";
                    
                    _logger.LogError("KBS bildirim durumu alınamadı. Hata: {ErrorContent}", errorContent);
                }
                
                return log;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirim durumu kontrol edilirken hata oluştu");
                
                // Hata durumunda log kaydı oluştur
                var log = new KbsNotificationLog
                {
                    ReferenceId = referenceNumber,
                    ActionType = KbsLogActionType.CheckStatus,
                    LogDate = DateTime.Now,
                    LogLevel = "Error",
                    IsSuccess = false,
                    Message = $"KBS bildirim durumu kontrol edilirken hata oluştu: {ex.Message}"
                };
                
                return log;
            }
        }

        /// <summary>
        /// KBS sistemine login olur ve oturum bilgilerini döner
        /// </summary>
        public async Task<ApiResponse<string>> LoginToKbsAsync(string username, string password, string tesisKodu)
        {
            try
            {
                _logger.LogInformation("KBS sistemine login işlemi yapılıyor");
                
                // Henüz implement edilmedi
                var response = new ApiResponse<string>();
                response.IsSuccess = false;
                response.Message = "KBS entegrasyonu henüz tamamlanmadı";
                response.Data = null;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS login işlemi sırasında hata oluştu");
                var response = new ApiResponse<string>();
                response.IsSuccess = false;
                response.Message = $"KBS login işlemi sırasında hata: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

        /// <summary>
        /// Daire sahibini KBS'ye bildirir
        /// </summary>
        public async Task<ApiResponse<bool>> ReportOwnerToKbsAsync(Kullanici user, Apartment apartment)
        {
            try
            {
                _logger.LogInformation("Daire sahibi KBS'ye bildiriliyor. Kullanıcı: {User}, Daire: {Apartment}", 
                    user.Id, apartment.Id);
                
                // Henüz implement edilmedi
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = "KBS entegrasyonu henüz tamamlanmadı";
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Daire sahibi KBS'ye bildirilirken hata oluştu. Kullanıcı: {User}, Daire: {Apartment}", 
                    user.Id, apartment.Id);
                
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = $"KBS bildirim işlemi sırasında hata: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        /// <summary>
        /// Kiracıyı KBS'ye bildirir
        /// </summary>
        public async Task<ApiResponse<bool>> ReportTenantToKbsAsync(Kullanici user, Apartment apartment, DateTime startDate)
        {
            try
            {
                _logger.LogInformation("Kiracı KBS'ye bildiriliyor. Kullanıcı: {User}, Daire: {Apartment}", 
                    user.Id, apartment.Id);
                
                // Henüz implement edilmedi
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = "KBS entegrasyonu henüz tamamlanmadı";
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kiracı KBS'ye bildirilirken hata oluştu. Kullanıcı: {User}, Daire: {Apartment}", 
                    user.Id, apartment.Id);
                
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = $"KBS bildirim işlemi sırasında hata: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        /// <summary>
        /// Aile üyesini KBS'ye bildirir
        /// </summary>
        public async Task<ApiResponse<bool>> ReportFamilyMemberToKbsAsync(FamilyMember familyMember, Apartment apartment)
        {
            try
            {
                _logger.LogInformation("Aile üyesi KBS'ye bildiriliyor. Üye: {Member}, Daire: {Apartment}", 
                    familyMember.Id, apartment.Id);
                
                // Henüz implement edilmedi
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = "KBS entegrasyonu henüz tamamlanmadı";
                response.Data = false;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aile üyesi KBS'ye bildirilirken hata oluştu. Üye: {Member}, Daire: {Apartment}", 
                    familyMember.Id, apartment.Id);
                
                var response = new ApiResponse<bool>();
                response.IsSuccess = false;
                response.Message = $"KBS bildirim işlemi sırasında hata: {ex.Message}";
                response.Data = false;
                return response;
            }
        }

        // KBS bildirimleri listele (filtreleme ve sayfalama ile)
        public async Task<PaginatedResultDto<KbsNotificationDto>> GetNotificationsAsync(KbsNotificationFilterDto filter)
        {
            try
            {
                _logger.LogInformation("KBS bildirimleri getiriliyor");
                // Burada bildirimleri getiren kod olacak
                throw new NotImplementedException("Bu metot henüz uygulanmadı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimleri getirilirken hata oluştu");
                throw new KbsIntegrationException("KBS bildirimleri getirilirken bir hata oluştu", ex);
            }
        }
        
        // Belirli ID'ye sahip bir KBS bildirimini getir
        public async Task<KbsNotificationDetailDto> GetNotificationByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi getiriliyor. ID: {Id}", id);
                // Burada bildirimi getiren kod olacak
                throw new NotImplementedException("Bu metot henüz uygulanmadı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi getirilirken hata oluştu. ID: {Id}", id);
                throw new KbsIntegrationException($"KBS bildirimi getirilirken bir hata oluştu. ID: {id}", ex);
            }
        }
        
        // Yeni bir KBS bildirimi oluştur
        public async Task<KbsNotificationDto> CreateNotificationAsync(KbsNotificationCreateDto model)
        {
            try
            {
                _logger.LogInformation("Yeni KBS bildirimi oluşturuluyor");
                // Burada bildirimi oluşturan kod olacak
                throw new NotImplementedException("Bu metot henüz uygulanmadı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi oluşturulurken hata oluştu");
                throw new KbsIntegrationException("KBS bildirimi oluşturulurken bir hata oluştu", ex);
            }
        }

        // KBS bildirimi iptal etme
        public async Task<KbsCancellationResultDto> CancelNotificationAsync(string kbsReferenceNumber, string cancellationReason)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi iptal ediliyor. Referans No: {ReferenceNumber}", kbsReferenceNumber);
                
                var cancellationRequest = new 
                {
                    ReferenceNumber = kbsReferenceNumber,
                    CancellationReason = cancellationReason
                };
                
                var response = await _httpClient.PostAsJsonAsync("/api/notifications/cancel", cancellationRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = new KbsCancellationResultDto
                    {
                        Success = true,
                        ReferenceNumber = kbsReferenceNumber,
                        StatusCode = response.StatusCode.ToString(),
                        Message = "KBS bildirimi başarıyla iptal edildi"
                    };
                    _logger.LogInformation("KBS bildirimi başarıyla iptal edildi. Referans No: {ReferenceNumber}", kbsReferenceNumber);
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("KBS bildirimi iptal edilemedi. Hata: {ErrorContent}", errorContent);
                    
                    return new KbsCancellationResultDto
                    {
                        Success = false,
                        ReferenceNumber = kbsReferenceNumber,
                        StatusCode = response.StatusCode.ToString(),
                        Message = $"KBS bildirimi iptal edilemedi: {errorContent}"
                    };
                }
            }
            catch (Exception ex) when (ex is not KbsIntegrationException)
            {
                _logger.LogError(ex, "KBS bildirimi iptal edilirken hata oluştu");
                return new KbsCancellationResultDto
                {
                    Success = false,
                    ReferenceNumber = kbsReferenceNumber,
                    StatusCode = "Error",
                    Message = $"KBS bildirimi iptal edilirken beklenmeyen bir hata oluştu: {ex.Message}"
                };
            }
        }

        // KBS bildirimi durumunu sorgulama
        public async Task<KbsNotificationStatusResultDto> GetNotificationStatusAsync(string kbsReferenceNumber)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi durumu sorgulanıyor. Referans No: {ReferenceNumber}", kbsReferenceNumber);
                
                var response = await _httpClient.GetAsync($"/api/notifications/status/{kbsReferenceNumber}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = new KbsNotificationStatusResultDto
                    {
                        ReferenceNumber = kbsReferenceNumber,
                        Status = "Success",
                        StatusCode = response.StatusCode.ToString(),
                        LastUpdated = DateTime.Now
                    };
                    
                    try 
                    {
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (jsonResponse.TryGetProperty("status", out var status))
                        {
                            result.Status = status.GetString();
                        }
                        if (jsonResponse.TryGetProperty("message", out var message))
                        {
                            result.StatusMessage = message.GetString();
                        }
                    }
                    catch {}
                    
                    _logger.LogInformation("KBS bildirimi durumu başarıyla alındı. Durum: {Status}", result.Status);
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("KBS bildirimi durumu alınamadı. Hata: {ErrorContent}", errorContent);
                    
                    return new KbsNotificationStatusResultDto
                    {
                        ReferenceNumber = kbsReferenceNumber,
                        Status = "Error",
                        StatusCode = response.StatusCode.ToString(),
                        StatusMessage = $"KBS bildirimi durumu alınamadı: {errorContent}",
                        LastUpdated = DateTime.Now
                    };
                }
            }
            catch (Exception ex) when (ex is not KbsIntegrationException)
            {
                _logger.LogError(ex, "KBS bildirimi durumu sorgulanırken hata oluştu");
                return new KbsNotificationStatusResultDto
                {
                    ReferenceNumber = kbsReferenceNumber,
                    Status = "Error",
                    StatusCode = "Exception",
                    StatusMessage = $"KBS bildirimi durumu sorgulanırken beklenmeyen bir hata oluştu: {ex.Message}",
                    LastUpdated = DateTime.Now
                };
            }
        }

        // KBS bildirim günlük kayıtlarını alma
        public async Task<IEnumerable<KbsNotificationLog>> GetNotificationLogsAsync(string kbsReferenceNumber)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi günlük kayıtları alınıyor. Referans No: {ReferenceNumber}", kbsReferenceNumber);
                
                var response = await _httpClient.GetAsync($"/api/notifications/logs/{kbsReferenceNumber}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<KbsNotificationLog>>();
                    _logger.LogInformation("KBS bildirimi günlük kayıtları başarıyla alındı");
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("KBS bildirimi günlük kayıtları alınamadı. Hata: {ErrorContent}", errorContent);
                    throw new KbsIntegrationException($"KBS bildirimi günlük kayıtları alınamadı. Durum kodu: {response.StatusCode}, Hata: {errorContent}");
                }
            }
            catch (Exception ex) when (ex is not KbsIntegrationException)
            {
                _logger.LogError(ex, "KBS bildirimi günlük kayıtları alınırken hata oluştu");
                throw new KbsIntegrationException("KBS bildirimi günlük kayıtları alınırken beklenmeyen bir hata oluştu", ex);
            }
        }

        // Günlük rapor alma
        public async Task<KbsDailyReportDto> GetDailyReportAsync(DateTime reportDate)
        {
            try
            {
                _logger.LogInformation("KBS günlük raporu alınıyor. Tarih: {ReportDate}", reportDate.ToShortDateString());
                
                var formattedDate = reportDate.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/api/reports/daily/{formattedDate}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = new KbsDailyReportDto
                    {
                        ReportDate = reportDate,
                        ReportItems = new List<KbsReportItemDto>()
                    };
                    
                    try
                    {
                        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (jsonResponse.TryGetProperty("totalSubmissions", out var totalSubmissions))
                        {
                            result.TotalSubmissions = totalSubmissions.GetInt32();
                        }
                        if (jsonResponse.TryGetProperty("successfulSubmissions", out var successfulSubmissions))
                        {
                            result.SuccessfulSubmissions = successfulSubmissions.GetInt32();
                        }
                        if (jsonResponse.TryGetProperty("failedSubmissions", out var failedSubmissions))
                        {
                            result.FailedSubmissions = failedSubmissions.GetInt32();
                        }
                        if (jsonResponse.TryGetProperty("totalCancellations", out var totalCancellations))
                        {
                            result.TotalCancellations = totalCancellations.GetInt32();
                        }
                    }
                    catch {}
                    
                    _logger.LogInformation("KBS günlük raporu başarıyla alındı");
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("KBS günlük raporu alınamadı. Hata: {ErrorContent}", errorContent);
                    throw new KbsIntegrationException($"KBS günlük raporu alınamadı. Durum kodu: {response.StatusCode}, Hata: {errorContent}");
                }
            }
            catch (Exception ex) when (ex is not KbsIntegrationException)
            {
                _logger.LogError(ex, "KBS günlük raporu alınırken hata oluştu");
                throw new KbsIntegrationException("KBS günlük raporu alınırken beklenmeyen bir hata oluştu", ex);
            }
        }

        // Diğer servis metotları için default implementasyonlar ekleyiniz.
        public Task<bool> DeleteNotificationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<KbsNotificationLogDto>> GetNotificationLogsAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<KbsNotificationDto> CreateFromReservationAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<KbsNotificationStatusDto> SubmitToKbsAsync(int notificationId, string userName)
        {
            throw new NotImplementedException();
        }

        public Task<KbsNotificationStatusDto> ResubmitToKbsAsync(int notificationId, string userName)
        {
            throw new NotImplementedException();
        }

        public Task<KbsNotificationStatusDto> CancelNotificationAsync(int notificationId, string userName, string cancellationReason)
        {
            throw new NotImplementedException();
        }

        public Task<KbsStatusReportDto> GetStatusReportAsync(DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GenerateKbsReportCsvAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<KbsAutomationResultDto> ProcessPendingNotificationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateKbsConfigurationAsync(KbsConfigurationDto model)
        {
            throw new NotImplementedException();
        }

        public Task<KbsConfigurationDto> GetKbsConfigurationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<KbsConnectionTestResultDto> TestKbsConnectionAsync(KbsConfigurationDto config = null)
        {
            throw new NotImplementedException();
        }

        // Mevcut bir KBS bildirimini güncelle
        public async Task<KbsNotificationDto> UpdateNotificationAsync(KbsNotificationUpdateDto model)
        {
            try
            {
                _logger.LogInformation("KBS bildirimi güncelleniyor. ID: {Id}", model.Id);
                // Burada bildirimi güncelleyen kod olacak
                throw new NotImplementedException("Bu metot henüz uygulanmadı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "KBS bildirimi güncellenirken hata oluştu. ID: {Id}", model.Id);
                throw new KbsIntegrationException($"KBS bildirimi güncellenirken bir hata oluştu. ID: {model.Id}", ex);
            }
        }

        // KBS isteği için model oluşturma
        private KbsRequestModel MapToKbsRequest(KbsNotification notification)
        {
            return new KbsRequestModel
            {
                FacilityCode = _configuration["KbsIntegration:FacilityCode"],
                CheckInDate = notification.CheckInDate,
                ExpectedCheckOutDate = notification.PlannedCheckOutDate,
                RoomNumber = notification.ApartmentNumber,
                Guest = notification.Guests?.FirstOrDefault() != null ? new KbsGuestModel
                {
                    IdType = notification.Guests.FirstOrDefault().IdentityType.ToString(),
                    IdNumber = notification.Guests.FirstOrDefault().IdentityNumber,
                    FirstName = notification.Guests.FirstOrDefault().FirstName,
                    LastName = notification.Guests.FirstOrDefault().LastName,
                    BirthDate = notification.Guests.FirstOrDefault().BirthDate,
                    Gender = notification.Guests.FirstOrDefault().Gender.ToString(),
                    Nationality = notification.Guests.FirstOrDefault().Nationality,
                    FatherName = notification.Guests.FirstOrDefault().FatherName,
                    MotherName = notification.Guests.FirstOrDefault().MotherName,
                    BirthPlace = notification.Guests.FirstOrDefault().BirthPlace,
                    Address = notification.Guests.FirstOrDefault().Address,
                    Phone = notification.Guests.FirstOrDefault().PhoneNumber,
                    Email = notification.Guests.FirstOrDefault().Email
                } : null
            };
        }
    }

    public class KbsRequestModel
    {
        public string FacilityCode { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime ExpectedCheckOutDate { get; set; }
        public string RoomNumber { get; set; }
        public KbsGuestModel Guest { get; set; }
    }
    
    public class KbsGuestModel
    {
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string BirthPlace { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
} 