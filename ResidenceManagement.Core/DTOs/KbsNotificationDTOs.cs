using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Models.KBS;
using ResidenceManagement.Core.Entities.Kbs;

namespace ResidenceManagement.Core.DTOs
{
    // Sayfalama için kullanılan genel DTO
    public class PagedResultDto<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

    /// <summary>
    /// KBS bildirim filtreleme DTO'su
    /// </summary>
    public class KbsNotificationFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public KbsNotifyStatus? Status { get; set; }
        public string SearchText { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// KBS bildirim yanıt DTO'su
    /// </summary>
    public class KbsNotificationResponseDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public string ResponseMessage { get; set; }
        public string ReferenceNumber { get; set; }
    }

    /// <summary>
    /// KBS bildirim DTO'su
    /// </summary>
    public class KbsNotificationDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public KbsNotificationType NotificationType { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime PlannedCheckOutDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
        public int GuestCount { get; set; }
        public string ApartmentNumber { get; set; }
        public string BlockName { get; set; }
        public StayPurpose StayPurpose { get; set; }
        public bool IsPaid { get; set; }
        public List<KbsGuestInfoDto> Guests { get; set; }
    }

    /// <summary>
    /// KBS misafir bilgisi DTO'su
    /// </summary>
    public class KbsGuestInfoDto
    {
        public int Id { get; set; }
        public GuestType GuestType { get; set; }
        public IdentityType IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string Nationality { get; set; }
    }

    /// <summary>
    /// KBS bildirim oluşturma DTO'su
    /// </summary>
    public class CreateKbsNotificationDto
    {
        public int ApartmentId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime PlannedCheckOutDate { get; set; }
        public StayPurpose StayPurpose { get; set; }
        public List<CreateKbsGuestInfoDto> Guests { get; set; }
    }

    /// <summary>
    /// KBS log DTO'su
    /// </summary>
    public class KbsNotificationLogDto
    {
        public int Id { get; set; }
        public KbsLogActionType ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }

    /// <summary>
    /// KBS bildirim durum güncelleme DTO'su
    /// </summary>
    public class UpdateKbsNotificationStatusDto
    {
        public KbsNotifyStatus OldStatus { get; set; }
        public KbsNotifyStatus NewStatus { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// KBS bildirim iptal DTO'su
    /// </summary>
    public class CancelKbsNotificationDto
    {
        public KbsNotifyStatus Status { get; set; }
        public string CancellationReason { get; set; }
    }

    /// <summary>
    /// KBS bildirim durum sorgu yanıt DTO'su
    /// </summary>
    public class KbsNotificationStatusResponseDto
    {
        public string NotificationNumber { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public string ResponseMessage { get; set; }
    }

    // KBS detaylı bildirim DTO
    public class KbsNotificationDetailDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public int? ReservationId { get; set; }
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public KbsNotificationType NotificationType { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime PlannedCheckOutDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
        public int GuestCount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public int? LastUpdatedByUserId { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string CancellationReason { get; set; }
        public int? CancelledByUserId { get; set; }
        public string CancelledByUserName { get; set; }
        public string KbsReferenceId { get; set; }
        public string LastResponseCode { get; set; }
        public string LastResponseMessage { get; set; }
        public StayPurpose StayPurpose { get; set; }
        public bool IsPaid { get; set; }
        public string Notes { get; set; }
        public string ErrorDescription { get; set; }
        public List<string> NotificationUrls { get; set; }
        public List<KbsGuestInfoDto> Guests { get; set; } = new List<KbsGuestInfoDto>();
        public List<KbsNotificationLogDto> Logs { get; set; } = new List<KbsNotificationLogDto>();
        public List<KbsNotificationHistoryDto> History { get; set; } = new List<KbsNotificationHistoryDto>();
    }

    // KBS bildirim güncelleme DTO
    public class KbsNotificationUpdateDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime PlannedCheckOutDate { get; set; }
        public StayPurpose StayPurpose { get; set; }
        public bool IsPaid { get; set; }
        public string Notes { get; set; }
        public List<KbsGuestInfoDto> Guests { get; set; } = new List<KbsGuestInfoDto>();
    }

    // KBS bildirim geçmişi DTO
    public class KbsNotificationHistoryDto
    {
        public int Id { get; set; }
        public int KbsNotificationId { get; set; }
        public KbsNotifyStatus OldStatus { get; set; }
        public string OldStatusText => OldStatus.ToString();
        public KbsNotifyStatus NewStatus { get; set; }
        public string NewStatusText => NewStatus.ToString();
        public DateTime ChangeDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string AdditionalInfo { get; set; }
    }

    // KBS bildirim durumu DTO
    public class KbsNotificationStatusDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public string StatusText => Status.ToString();
        public string KbsReferenceId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }

    // KBS durum raporu DTO
    public class KbsStatusReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalNotifications { get; set; }
        public int SubmittedCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public int ErrorCount { get; set; }
        public int CancelledCount { get; set; }
        public int PendingCount { get; set; }
        public int CheckedOutCount { get; set; }
        public List<KbsStatusReportItemDto> DailyBreakdown { get; set; } = new List<KbsStatusReportItemDto>();
    }

    // KBS durum raporu öğesi DTO
    public class KbsStatusReportItemDto
    {
        public DateTime Date { get; set; }
        public int TotalCount { get; set; }
        public int SubmittedCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public int ErrorCount { get; set; }
    }

    // KBS otomasyon sonucu DTO
    public class KbsAutomationResultDto
    {
        public DateTime ProcessTime { get; set; }
        public int ProcessedCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<string> SuccessMessages { get; set; } = new List<string>();
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }

    // KBS konfigürasyonu DTO
    public class KbsConfigurationDto
    {
        public int Id { get; set; }
        public string ServiceUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string FacilityCode { get; set; }
        public string FacilityType { get; set; }
        public string RegisteredFacilityName { get; set; }
        public string RegisteredAddress { get; set; }
        public string RegisteredDistrict { get; set; }
        public string RegisteredCity { get; set; }
        public string LawEnforcementType { get; set; }
        public string LawEnforcementBranch { get; set; }
        public int ConnectionTimeout { get; set; } = 60;
        public bool AutoSubmitEnabled { get; set; } = true;
        public int AutoSubmitDelayMinutes { get; set; } = 15;
        public DateTime? LastDailySubmissionTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool TestModeEnabled { get; set; } = true;
        public string CertificateThumbprint { get; set; }
        public int MaxDailySubmissions { get; set; } = 1000;
        public string Notes { get; set; }
    }

    // KBS bağlantı testi sonucu DTO
    public class KbsConnectionTestResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int ResponseTimeMs { get; set; }
        public string ServerVersion { get; set; }
        public bool IsTestMode { get; set; }
        public DateTime TestDate { get; set; } = DateTime.Now;
    }

    // KBS iptal sonucu DTO
    public class KbsCancellationResultDto
    {
        public bool Success { get; set; }
        public string ReferenceNumber { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }

    // KBS gönderim sonucu DTO
    public class KbsSubmissionResultDto
    {
        public bool Success { get; set; }
        public string ReferenceNumber { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }

    // KBS günlük rapor DTO
    public class KbsDailyReportDto
    {
        public DateTime ReportDate { get; set; }
        public int TotalSubmissions { get; set; }
        public int SuccessfulSubmissions { get; set; }
        public int FailedSubmissions { get; set; }
        public int TotalCancellations { get; set; }
        public List<KbsReportItemDto> ReportItems { get; set; } = new List<KbsReportItemDto>();
    }

    // KBS rapor öğesi DTO
    public class KbsReportItemDto
    {
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string GuestName { get; set; }
        public string GuestIdentity { get; set; }
        public string ApartmentNumber { get; set; }
    }
} 