using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Core.Entities.Identity
{
    // Kullanıcı modeli, sistem kullanıcılarını temsil eder
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string PreferredLanguage { get; set; } // tr, en, de, ar, ru, fa
        public string PreferredCurrency { get; set; } // TRY, USD, EUR, GBP
        public bool TwoFactorEnabled { get; set; }
        public string TwoFactorKey { get; set; }
        
        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<ApartmentOwner> OwnedApartments { get; set; }
        public virtual ICollection<ApartmentResident> ResidedApartments { get; set; }
        public virtual ICollection<MaintenanceSchedule> AssignedMaintenanceSchedules { get; set; }
        
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            OwnedApartments = new HashSet<ApartmentOwner>();
            ResidedApartments = new HashSet<ApartmentResident>();
            AssignedMaintenanceSchedules = new HashSet<MaintenanceSchedule>();
        }
    }
} 