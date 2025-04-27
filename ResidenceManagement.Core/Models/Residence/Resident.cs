using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Residence
{
    // Site veya rezidansta oturan kişileri temsil eden entity sınıfı
    public class Resident : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string ResidentType { get; set; } // Mal Sahibi, Kiracı, Aile Üyesi vb.
        public DateTime MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public bool IsActive { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Notes { get; set; }
        public int ApartmentId { get; set; }
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // İlişkiler
        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public Resident()
        {
            Vehicles = new HashSet<Vehicle>();
            IsActive = true;
        }
    }
} 