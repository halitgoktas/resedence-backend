namespace ResidenceManagement.Core.Enums
{
    /// <summary>
    /// Misafir tiplerini tanımlayan enum
    /// </summary>
    public enum GuestType
    {
        /// <summary>
        /// Ana misafir (rezervasyon sahibi)
        /// </summary>
        PrimaryGuest = 0,
        
        /// <summary>
        /// Ek misafir
        /// </summary>
        AdditionalGuest = 1,
        
        /// <summary>
        /// Aile üyesi
        /// </summary>
        FamilyMember = 2,
        
        /// <summary>
        /// İş arkadaşı
        /// </summary>
        Colleague = 3,
        
        /// <summary>
        /// Grup üyesi
        /// </summary>
        GroupMember = 4,
        
        /// <summary>
        /// Çocuk misafir
        /// </summary>
        Child = 5,
        
        /// <summary>
        /// Diğer
        /// </summary>
        Other = 6
    }
} 