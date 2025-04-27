using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Common
{
    // Tüm çoklu kiracı (multi-tenant) varlıkları için temel sınıf
    // BaseEntity zaten ITenant arayüzünü uyguladığı için doğrudan BaseEntity'den kalıtım alır
    public abstract class BaseMultiTenantEntity : BaseEntity
    {
        // BaseEntity sınıfı zaten FirmaId ve SubeId alanlarını içeriyor
        // Multi-tenant varlıklar için ek özellik ve davranışlar buraya eklenebilir
    }
} 