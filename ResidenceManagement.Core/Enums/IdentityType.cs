using System;

namespace ResidenceManagement.Core.Enums
{
    // Kimlik türlerini tanımlayan enum sınıfı
    public enum IdentityType
    {
        // Türkiye Cumhuriyeti kimlik numarası
        TCKimlik = 1,
        
        // Yabancı kimlik numarası
        YabanciKimlik = 2,
        
        // Pasaport numarası
        Pasaport = 3,
        
        // Diğer kimlik belgesi
        Diger = 4
    }
} 