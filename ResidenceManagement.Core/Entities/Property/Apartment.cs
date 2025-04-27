using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Entities.Property
{
    // Daire modeli, bir konut birimini temsil eder
    public class Apartment : BaseEntity
    {
        public int BlockId { get; set; }
        public string ApartmentNumber { get; set; }
        public int Floor { get; set; }
        public string ApartmentType { get; set; } // 1+1, 2+1, vb.
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfLivingRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBalconies { get; set; }
        public ApartmentStatus Status { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public int? OwnerId { get; set; }
        public int? TenantId { get; set; }
        public string Description { get; set; }
        public decimal DuesAmount { get; set; }
        public DateTime? LastDuesPaymentDate { get; set; }
        public decimal DuesCoefficient { get; set; }
        public string HeatingType { get; set; }
        public string Notes { get; set; }
        
        // Navigation Properties
        public virtual Block Block { get; set; }
        public virtual ICollection<ApartmentOwner> Owners { get; set; }
        public virtual ICollection<ApartmentResident> Residents { get; set; }
    }
} 