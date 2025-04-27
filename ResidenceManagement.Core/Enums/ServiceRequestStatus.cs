namespace ResidenceManagement.Core.Enums
{
    /// <summary>
    /// Hizmet taleplerinin durumlarını temsil eden enum
    /// </summary>
    public enum ServiceRequestStatus
    {
        /// <summary>
        /// Açık/Yeni talep
        /// </summary>
        Open = 1,
        
        /// <summary>
        /// Atandı
        /// </summary>
        Assigned = 2,
        
        /// <summary>
        /// İşlemde
        /// </summary>
        InProgress = 3,
        
        /// <summary>
        /// Beklemede
        /// </summary>
        OnHold = 4,
        
        /// <summary>
        /// Çözüldü
        /// </summary>
        Resolved = 5,
        
        /// <summary>
        /// Kapatıldı
        /// </summary>
        Closed = 6,
        
        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 7,
        
        /// <summary>
        /// Yeniden açıldı
        /// </summary>
        Reopened = 8
    }
} 