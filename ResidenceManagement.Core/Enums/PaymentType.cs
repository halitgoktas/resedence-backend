using System;

namespace ResidenceManagement.Core.Enums
{
    // Ödeme tiplerini tanımlayan enum sınıfı
    public enum PaymentType
    {
        // Aidat ödemesi
        Aidat = 1,
        
        // Su faturası ödemesi
        SuFaturasi = 2,
        
        // Elektrik faturası ödemesi
        ElektrikFaturasi = 3,
        
        // Doğalgaz faturası ödemesi
        DogalgazFaturasi = 4,
        
        // İnternet faturası ödemesi
        InternetFaturasi = 5,
        
        // Yönetim gideri ödemesi
        YonetimGideri = 6,
        
        // Bakım onarım gideri ödemesi
        BakimOnarim = 7,
        
        // Temizlik gideri ödemesi
        Temizlik = 8,
        
        // Güvenlik gideri ödemesi
        Guvenlik = 9,
        
        // Diğer ödemeler
        Diger = 10
    }
} 