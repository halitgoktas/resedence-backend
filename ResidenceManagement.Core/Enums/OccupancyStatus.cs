namespace ResidenceManagement.Core.Enums
{
    // Dairenin doluluk durumunu belirten enum
    public enum OccupancyStatus
    {
        // Boş daire
        Empty = 0,
        
        // Sahibi tarafından kullanılan
        OccupiedByOwner = 1,
        
        // Kiracı tarafından kullanılan
        OccupiedByTenant = 2,
        
        // Geçici olarak boş (örn. tadilat, taşınma süreci)
        TemporarilyEmpty = 3
    }
} 