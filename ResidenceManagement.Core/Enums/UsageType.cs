namespace ResidenceManagement.Core.Enums
{
    // Dairenin kullanım amacını belirten enum
    public enum UsageType
    {
        // Konut olarak kullanılan
        Residential = 0,
        
        // İşyeri/ofis olarak kullanılan
        Commercial = 1,
        
        // Hem konut hem işyeri olarak kullanılan (home-office)
        MixedUse = 2
    }
} 