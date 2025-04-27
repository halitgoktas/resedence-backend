using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Models.KBS;

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

    // KBS bildirimleri için filtreleme DTO
    public class KbsNotificationFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string NotificationNumber { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public int? BlockId { get; set; }
        public int? ResidenceId { get; set; }
        public KbsNotifyStatus? Status { get; set; }
        public bool? IsSubmitted { get; set; }
        public bool? IsCancelled { get; set; }
        public string GuestName { get; set; }
        public string GuestIdentityNumber { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; } = "CreatedDate";
        public bool SortDesc { get; set; } = true;
    }

    // KBS bildirim listeleme DTO
    public class KbsNotificationDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime PlannedCheckOutDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
        public KbsNotifyStatus Status { get; set; }
        public string StatusText => Status.ToString();
        public string ApartmentNumber { get; set; }
        public string BlockName { get; set; }
        public string ResidenceName { get; set; }
        public int GuestCount { get; set; }
        public string PrimaryGuestName { get; set; }
        public string PrimaryGuestIdentityNumber { get; set; }
        public string KbsReferenceId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public bool IsCancelled { get; set; }
        public string CreatedByUserName { get; set; }
        public string LastResponseMessage { get; set; }
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

    // KBS misafir bilgisi DTO
    public class KbsGuestInfoDto
    {
        public int Id { get; set; }
        public int KbsNotificationId { get; set; }
        public GuestType GuestType { get; set; }
        public IdentityType IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string IssuingCountry { get; set; }
        public DateTime? DocumentExpiryDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public Gender? Gender { get; set; }
        [Required]
        public string Nationality { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string VehiclePlate { get; set; }
        public string Notes { get; set; }
        public bool IsRegisteredInKbs { get; set; }
        public bool IsRegisteredGuest { get; set; }
        public bool IsGroupLeader { get; set; }
        public string PhotoUrl { get; set; }
        public string IdentityFrontPhotoUrl { get; set; }
        public string IdentityBackPhotoUrl { get; set; }
        public string KbsReferenceNumber { get; set; }
        public bool IsVerifiedByKbs { get; set; }
        public DateTime? KbsVerificationDate { get; set; }
        public string KbsErrorMessage { get; set; }
    }

    // KBS bildirim oluşturma DTO
    public class KbsNotificationCreateDto
    {
        [Required]
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public int? BlockId { get; set; }
        public int ResidenceId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime PlannedCheckOutDate { get; set; }
        public StayPurpose StayPurpose { get; set; } = StayPurpose.Tourism;
        public bool IsPaid { get; set; } = true;
        public string Notes { get; set; }
        public int? ReservationId { get; set; }
        public List<KbsGuestInfoDto> Guests { get; set; } = new List<KbsGuestInfoDto>();
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

    // KBS bildirim log DTO
    public class KbsNotificationLogDto
    {
        public int Id { get; set; }
        public int KbsNotificationId { get; set; }
        public KbsLogActionType ActionType { get; set; }
        public string ActionTypeText => ActionType.ToString();
        public DateTime ActionDate { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string ResultStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSuccess { get; set; }
        public int? ProcessDuration { get; set; }
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

    // KBS bildirim durumu DTO
    public class KbsNotificationStatusResultDto
    {
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public DateTime LastUpdated { get; set; }
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