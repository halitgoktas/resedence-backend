using System;

namespace ResidenceManagement.Core.Common
{
    // Multi-tenant yapısı için hem firma hem de şube bazlı filtreleme arayüzü
    public interface ITenant : IFirmaTenant, ISubeTenant
    {
        // IFirmaTenant ve ISubeTenant arayüzlerinden gelen özellikleri içerir
    }
} 