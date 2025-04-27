using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    // İzin modeli, sistem içerisindeki izinleri tanımlar
    public class Permission : BaseLookupEntity
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Group { get; set; }
        public int DisplayOrder { get; set; }
        
        // Navigation Properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
} 