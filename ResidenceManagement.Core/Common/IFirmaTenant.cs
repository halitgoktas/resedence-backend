using System;

namespace ResidenceManagement.Core.Common
{
    // Firma bazlı multi-tenant yapısı için filtreleme arayüzü
    public interface IFirmaTenant
    {
        int FirmaId { get; set; }
    }
} 