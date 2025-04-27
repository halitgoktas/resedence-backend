using System;

namespace ResidenceManagement.Core.Enums
{
    // Ödeme durumlarını tanımlayan enum sınıfı
    public enum PaymentStatus
    {
        // Ödeme beklemede
        Beklemede = 1,
        
        // Ödeme kısmen yapılmış
        KismiOdeme = 2,
        
        // Ödeme tamamlanmış
        Tamamlandi = 3,
        
        // Ödeme iptal edilmiş
        Iptal = 4,
        
        // Ödeme gecikmiş
        Gecikti = 5
    }
} 