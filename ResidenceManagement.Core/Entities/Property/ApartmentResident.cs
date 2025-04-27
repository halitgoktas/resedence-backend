using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Entities.Property
{
    // Daire sakini modeli, dairede ikamet eden kişi bilgilerini temsil eder
    public class ApartmentResident : BaseTransactionEntity
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public ResidenceType ResidenceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NumberOfResidents { get; set; }
        public decimal? MonthlyRent { get; set; }
        public string ContractReference { get; set; }
        
        // Navigation Properties
        public virtual Apartment Apartment { get; set; }
        public virtual User User { get; set; }
    }
} 