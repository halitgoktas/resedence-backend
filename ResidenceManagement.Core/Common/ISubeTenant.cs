using System;

namespace ResidenceManagement.Core.Common
{
    // Şube bazlı multi-tenant yapısı için filtreleme arayüzü
    public interface ISubeTenant
    {
        int SubeId { get; set; }
    }
} 