using System;

namespace ResidenceManagement.Core.Entities.Base
{
    // Sistem genelinde kullanılan ve tenant bilgisi gerektirmeyen tablolar için temel sınıf
    public abstract class BaseSystemEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
} 