using System;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Staff
{
    // Personel bilgilerini temsil eden entity sınıfı
    public class Staff : BaseEntity
    {
        // Kişisel bilgiler
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        
        // İletişim bilgileri
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        
        // İş bilgileri
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public bool IsActive { get; set; }
        
        // Acil durum bilgileri
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactRelation { get; set; }
        
        // Maaş bilgileri
        public decimal Salary { get; set; }
        public string SalaryType { get; set; } // Saatlik, Günlük, Aylık
        
        // Çalışma bilgileri
        public string WorkType { get; set; } // Tam zamanlı, Yarı zamanlı, Sözleşmeli
        public string WorkShift { get; set; } // Gündüz, Gece, Vardiyalı
        public string WorkHours { get; set; }
        
        // Nitelikler
        public string Skills { get; set; }
        public string Certifications { get; set; }
        public string Education { get; set; }
        
        // Notlar
        public string Notes { get; set; }
        
        // Atandığı yer
        public int? ResidenceComplexId { get; set; }
        public int? BuildingId { get; set; }
        
        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 