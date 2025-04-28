// -----------------------------------------------------------------------
// <copyright file="KbsNotificationHistory.cs" company="ResidenceManager">
// Copyright (c) ResidenceManager. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Purpose: Represents a history record for KBS notification status changes
// Dependencies: BaseEntity, KbsNotification
// Usage: Used for tracking status changes of KBS notifications
// Author: ResidenceManager Team
// Created: 2024-04-27
// Modified: 2024-04-27
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using ResidenceManagement.Core.Entities.Common;

namespace ResidenceManagement.Core.Entities.Kbs
{
    /// <summary>
    /// Represents a history record for KBS notification status changes
    /// </summary>
    public class KbsNotificationHistory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the related KBS notification ID
        /// </summary>
        public int KbsNotificationId { get; set; }

        /// <summary>
        /// Gets or sets the related KBS notification
        /// </summary>
        public virtual KbsNotification KbsNotification { get; set; }

        /// <summary>
        /// Gets or sets the old status
        /// </summary>
        public KbsNotificationStatus OldStatus { get; set; }

        /// <summary>
        /// Gets or sets the new status
        /// </summary>
        public KbsNotificationStatus NewStatus { get; set; }

        /// <summary>
        /// Gets or sets the change date
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the user ID who made the change
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the username who made the change
        /// </summary>
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the description of the change
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets additional information in JSON format
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KbsNotificationHistory"/> class
        /// </summary>
        public KbsNotificationHistory()
        {
            ChangeDate = DateTime.Now;
        }
    }
} 