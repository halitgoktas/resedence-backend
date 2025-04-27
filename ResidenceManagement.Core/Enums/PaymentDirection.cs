using System;

namespace ResidenceManagement.Core.Enums
{
    // Ödeme yönünü tanımlayan enum sınıfı
    public enum PaymentDirection
    {
        // Gelen ödeme (tahsilat)
        Gelir = 1,
        
        // Giden ödeme (ödeme)
        Gider = 2
    }
} 