// -----------------------------------------------------------------------
// <copyright file="KbsNotification.cs" company="ResidenceManager">
// Copyright (c) ResidenceManager. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Purpose: Represents a notification record for the Identity Notification System (KBS)
// Dependencies: BaseEntity, ITenant, Apartment, KbsGuestInfo, KbsNotificationLog, KbsNotificationHistory
// Usage: Used for managing and tracking KBS notifications for residents and guests
// Author: ResidenceManager Team
// Created: 2024-04-27
// Modified: 2024-04-27
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.Entities.Property;
using ResidenceManagement.Core.Interfaces.Common;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS bildirimlerini temsil eden sınıf
    /// </summary>
    public class KbsNotification : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// Referans numarası
        /// </summary>
        public string ReferenceId { get; set; }
        
        /// <summary>
        /// KBS Tesisi kodu
        /// </summary>
        public string FacilityCode { get; set; }
        
        /// <summary>
        /// Bildirim tipi (Giriş, Çıkış, vb.)
        /// </summary>
        public string NotificationType { get; set; }
        
        /// <summary>
        /// Bildirim açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Bildirim içeriği (JSON formatında)
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// KBS'den dönen durum kodu
        /// </summary>
        public string StatusCode { get; set; }
        
        /// <summary>
        /// KBS'den dönen durum mesajı
        /// </summary>
        public string StatusMessage { get; set; }
        
        /// <summary>
        /// Bildirim gönderim tarihi
        /// </summary>
        public DateTime? NotificationDate { get; set; }
        
        /// <summary>
        /// Durum güncelleme tarihi
        /// </summary>
        public DateTime? StatusUpdateDate { get; set; }
        
        /// <summary>
        /// Daire/Oda numarası
        /// </summary>
        public string RoomNumber { get; set; }
        
        /// <summary>
        /// Tahmini çıkış tarihi
        /// </summary>
        public DateTime ExpectedCheckOutDate { get; set; }
        
        /// <summary>
        /// Misafir bilgileri (JSON formatında)
        /// </summary>
        public string GuestInfo { get; set; }
        
        /// <summary>
        /// Bildirim önceliği
        /// </summary>
        public string Priority { get; set; } = "Normal";
        
        /// <summary>
        /// Bildirim işlendi mi?
        /// </summary>
        public bool IsProcessed { get; set; }
        
        /// <summary>
        /// İşlenme tarihi
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
        
        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Yeniden deneme sayısı
        /// </summary>
        public int RetryCount { get; set; }
        
        /// <summary>
        /// Son deneme tarihi
        /// </summary>
        public DateTime? LastRetryDate { get; set; }
        
        /// <summary>
        /// Otomatik bildirim mi?
        /// </summary>
        public bool IsAutomated { get; set; }
        
        /// <summary>
        /// Bildirim işlem ID'si
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// İlişkili konuk ID'si
        /// </summary>
        public int? GuestId { get; set; }

        /// <summary>
        /// Gets or sets the company ID for multi-tenancy
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the branch ID for multi-tenancy
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the unique notification number
        /// </summary>
        [Required]
        [StringLength(50)]
        public string NotificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the related reservation ID (if any)
        /// </summary>
        public int? ReservationId { get; set; }

        /// <summary>
        /// Gets or sets the related apartment ID
        /// </summary>
        public int ApartmentId { get; set; }

        /// <summary>
        /// Gets or sets the apartment number
        /// </summary>
        [Required]
        [StringLength(20)]
        public string ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the related block ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Gets or sets the block name
        /// </summary>
        [StringLength(50)]
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or sets the notification status
        /// </summary>
        public KbsNotificationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the check-in date
        /// </summary>
        [Required]
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets the planned check-out date
        /// </summary>
        public DateTime PlannedCheckOutDate { get; set; }

        /// <summary>
        /// Gets or sets the actual check-out date
        /// </summary>
        public DateTime? ActualCheckOutDate { get; set; }

        /// <summary>
        /// Gets the total number of guests
        /// </summary>
        public int GuestCount => Guests?.Count ?? 0;

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the submission date to KBS
        /// </summary>
        public DateTime? SubmissionDate { get; set; }

        /// <summary>
        /// Gets or sets the last update date
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the KBS reference ID
        /// </summary>
        [StringLength(50)]
        public string KbsReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the last response code from KBS
        /// </summary>
        [StringLength(20)]
        public string LastResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the last response message from KBS
        /// </summary>
        [StringLength(500)]
        public string LastResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notification is cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the cancellation date
        /// </summary>
        public DateTime? CancellationDate { get; set; }

        /// <summary>
        /// Gets or sets the cancellation reason
        /// </summary>
        [StringLength(500)]
        public string CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets the stay purpose
        /// </summary>
        public StayPurpose StayPurpose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the stay is paid
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Gets or sets additional notes
        /// </summary>
        [StringLength(500)]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets error description if any
        /// </summary>
        [StringLength(500)]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets notification URLs in JSON format
        /// </summary>
        public string NotificationUrls { get; set; }

        /// <summary>
        /// Gets or sets the related apartment
        /// </summary>
        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }

        /// <summary>
        /// Gets or sets the guest information collection
        /// </summary>
        public virtual ICollection<KbsGuestInfo> Guests { get; set; }

        /// <summary>
        /// Gets or sets the notification logs
        /// </summary>
        public virtual ICollection<KbsNotificationLog> Logs { get; set; }

        /// <summary>
        /// Gets or sets the notification history
        /// </summary>
        public virtual ICollection<KbsNotificationHistory> History { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KbsNotification"/> class
        /// </summary>
        public KbsNotification()
        {
            Guests = new List<KbsGuestInfo>();
            Logs = new List<KbsNotificationLog>();
            History = new List<KbsNotificationHistory>();
            CreationDate = DateTime.Now;
            Status = KbsNotificationStatus.Draft;
            NotificationType = KbsNotificationType.CheckIn.ToString();
            IsCancelled = false;
            StayPurpose = StayPurpose.Tourism;
            NotificationNumber = GenerateNotificationNumber();
        }

        /// <summary>
        /// Generates a unique notification number
        /// </summary>
        /// <returns>Generated notification number</returns>
        private string GenerateNotificationNumber()
        {
            string prefix = "KBS";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string random = new Random().Next(1000, 9999).ToString();
            return $"{prefix}-{timestamp}-{random}";
        }

        /// <summary>
        /// Updates the notification status
        /// </summary>
        /// <param name="newStatus">New status to set</param>
        /// <param name="description">Optional description</param>
        /// <param name="userId">User ID performing the update</param>
        /// <param name="userName">Username performing the update</param>
        public void UpdateStatus(KbsNotificationStatus newStatus, string description = null, int? userId = null, string userName = null)
        {
            var oldStatus = Status;
            Status = newStatus;
            LastUpdateDate = DateTime.Now;

            History.Add(new KbsNotificationHistory
            {
                KbsNotificationId = Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Description = description,
                UserId = userId,
                UserName = userName
            });
        }
    }
} 