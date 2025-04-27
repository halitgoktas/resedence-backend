using System;

namespace ResidenceManagement.Core.Enums
{
    // Ödeme yöntemlerini tanımlayan enum sınıfı
    public enum PaymentMethod
    {
        // Nakit ödeme
        Nakit = 1,
        
        // Kredi kartı ile ödeme
        KrediKarti = 2,
        
        // Banka havalesi
        BankaHavalesi = 3,
        
        // Otomatik ödeme talimatı
        OtomatikOdeme = 4,
        
        // Çek ile ödeme
        Cek = 5,
        
        // Senet ile ödeme
        Senet = 6,
        
        // Diğer ödeme yöntemleri
        Diger = 7
    }
} 