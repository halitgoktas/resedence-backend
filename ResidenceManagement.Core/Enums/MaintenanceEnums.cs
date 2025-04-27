using System;

namespace ResidenceManagement.Core.Enums
{
    /// <summary>
    /// Bakım tiplerini tanımlayan enum
    /// </summary>
    public enum MaintenanceType
    {
        /// <summary>
        /// Önleyici bakım
        /// </summary>
        Preventive = 1,
        
        /// <summary>
        /// Düzeltici bakım
        /// </summary>
        Corrective = 2,
        
        /// <summary>
        /// Kestirimci bakım
        /// </summary>
        Predictive = 3,
        
        /// <summary>
        /// Periyodik bakım
        /// </summary>
        Periodic = 4,
        
        /// <summary>
        /// Acil bakım
        /// </summary>
        Emergency = 5
    }
    
    /// <summary>
    /// Bakım sıklık tiplerini tanımlayan enum
    /// </summary>
    public enum MaintenanceFrequency
    {
        /// <summary>
        /// Günlük
        /// </summary>
        Daily = 1,
        
        /// <summary>
        /// Haftalık
        /// </summary>
        Weekly = 2,
        
        /// <summary>
        /// Aylık
        /// </summary>
        Monthly = 3,
        
        /// <summary>
        /// Üç aylık
        /// </summary>
        Quarterly = 4,
        
        /// <summary>
        /// Altı aylık
        /// </summary>
        SemiAnnually = 5,
        
        /// <summary>
        /// Yıllık
        /// </summary>
        Annually = 6,
        
        /// <summary>
        /// Özel sıklık
        /// </summary>
        Custom = 7
    }
    
    /// <summary>
    /// Bakım durumlarını tanımlayan enum
    /// </summary>
    public enum MaintenanceStatus
    {
        /// <summary>
        /// Planlandı
        /// </summary>
        Planned = 1,
        
        /// <summary>
        /// Zamanlandı
        /// </summary>
        Scheduled = 2,
        
        /// <summary>
        /// Devam ediyor
        /// </summary>
        InProgress = 3,
        
        /// <summary>
        /// Tamamlandı
        /// </summary>
        Completed = 4,
        
        /// <summary>
        /// Ertelendi
        /// </summary>
        Postponed = 5,
        
        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 6,
        
        /// <summary>
        /// Gecikti
        /// </summary>
        Delayed = 7
    }
    
    /// <summary>
    /// Bakım öncelik seviyelerini tanımlayan enum
    /// </summary>
    public enum MaintenancePriority
    {
        /// <summary>
        /// Düşük
        /// </summary>
        Low = 1,
        
        /// <summary>
        /// Normal
        /// </summary>
        Normal = 2,
        
        /// <summary>
        /// Yüksek
        /// </summary>
        High = 3,
        
        /// <summary>
        /// Acil
        /// </summary>
        Critical = 4
    }
} 