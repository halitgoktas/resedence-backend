using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    // Rol modeli, sistem içerisindeki rolleri tanımlar
    public class Role : BaseLookupEntity
    {
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public bool IsSystemDefined { get; set; }
        
        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
} 