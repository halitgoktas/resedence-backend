using System;

namespace ResidenceManagement.Core.Entities.Base
{
    // Referans/tanım tabloları için kullanılan temel sınıf
    public abstract class BaseLookupEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
} 