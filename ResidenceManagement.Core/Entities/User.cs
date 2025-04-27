using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    // Kullanıcı sınıfı
    public class User : BaseEntity
    {
        // Constructor
        public User()
        {
            AssignedMaintenances = new HashSet<MaintenanceSchedule>();
            CompletedChecklistItems = new HashSet<MaintenanceChecklistItem>();
        }

        // Temel bilgiler
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLoginDate { get; set; }
        
        // Rol ve yetki bilgileri
        public string Role { get; set; }
        public string[] Permissions { get; set; }
        
        // İlişkili bakım görevleri
        public virtual ICollection<MaintenanceSchedule> AssignedMaintenances { get; set; }
        public virtual ICollection<MaintenanceChecklistItem> CompletedChecklistItems { get; set; }
        
        // Multi-tenant için gerekli alanlar
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Tam ad özelliği
        public string FullName => $"{FirstName} {LastName}";
    }
} 