// -----------------------------------------------------------------------
// <copyright file="KbsNotificationLog.cs" company="ResidenceManager">
// Copyright (c) ResidenceManager. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Purpose: Represents a log entry for KBS notification operations
// Dependencies: BaseEntity, KbsNotification
// Usage: Used for tracking and auditing KBS notification operations
// Author: ResidenceManager Team
// Created: 2024-04-27
// Modified: 2024-04-27
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// KBS bildirim loglarını tutan sınıf
    /// </summary>
    public class KbsNotificationLog : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// İlişkili KBS bildirim ID'si
        /// </summary>
        public int KbsNotificationId { get; set; }
        
        /// <summary>
        /// İşlem tipi
        /// </summary>
        public KbsLogActionType ActionType { get; set; }
        
        /// <summary>
        /// Referans numarası
        /// </summary>
        [StringLength(100)]
        public string ReferenceId { get; set; }
        
        /// <summary>
        /// Log mesajı
        /// </summary>
        [StringLength(500)]
        public string Message { get; set; }
        
        /// <summary>
        /// Log düzeyi (Info, Warning, Error)
        /// </summary>
        [StringLength(20)]
        public string LogLevel { get; set; }
        
        /// <summary>
        /// Log tarihi
        /// </summary>
        public DateTime LogDate { get; set; }
        
        /// <summary>
        /// Durum kodu
        /// </summary>
        [StringLength(20)]
        public string StatusCode { get; set; }
        
        /// <summary>
        /// HTTP yanıt içeriği
        /// </summary>
        public string ResponseContent { get; set; }
        
        /// <summary>
        /// HTTP istek içeriği
        /// </summary>
        public string RequestContent { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı ID'si
        /// </summary>
        public int? UserId { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        [StringLength(100)]
        public string UserName { get; set; }
        
        /// <summary>
        /// IP adresi
        /// </summary>
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// İşlem süresi (milisaniye)
        /// </summary>
        public int? ProcessDuration { get; set; }
        
        /// <summary>
        /// İlişkili KBS bildirimi
        /// </summary>
        public virtual KbsNotification KbsNotification { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="KbsNotificationLog"/> class
        /// </summary>
        public KbsNotificationLog()
        {
            LogDate = DateTime.Now;
            IsSuccess = false;
        }
    }
} 