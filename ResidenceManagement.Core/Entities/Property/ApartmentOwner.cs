using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Entities.Property
{
    // Daire sahibi modeli, daire sahipliği bilgilerini temsil eder
    public class ApartmentOwner : BaseTransactionEntity
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public decimal OwnershipPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DeedInformation { get; set; }
        
        // Navigation Properties
        public virtual Apartment Apartment { get; set; }
        public virtual User User { get; set; }
    }
} 