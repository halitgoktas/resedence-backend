using System;

namespace ResidenceManagement.Core.Models.Common
{
    // Tüm entity sınıflarının miras alacağı temel sınıf
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Yeni oluşturulan kayıtlar için default değerleri ayarlar
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
} 