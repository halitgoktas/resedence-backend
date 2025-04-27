using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Services;

namespace ResidenceManagement.Core.Entities.Staff
{
    /// <summary>
    /// Personel bilgilerini içeren sınıf
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Personel kimlik numarası (TC No)
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Personel adı
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Personel soyadı
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tam adı
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Doğum tarihi
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Doğum yeri
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Cinsiyet (1: Erkek, 2: Kadın, 3: Diğer)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// İşe başlama tarihi
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// İşten ayrılma tarihi
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Departman ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Pozisyon
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Maaş
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Maaş para birimi
        /// </summary>
        public string SalaryCurrency { get; set; } = "TRY";

        /// <summary>
        /// Maaş ödeme periyodu (1: Haftalık, 2: İki Haftalık, 3: Aylık)
        /// </summary>
        public int PaymentPeriod { get; set; } = 3;

        /// <summary>
        /// Maaş ödeme günü (ayın kaçında)
        /// </summary>
        public int PaymentDayOfMonth { get; set; } = 1;

        /// <summary>
        /// Çalışma şekli (1: Tam Zamanlı, 2: Yarı Zamanlı, 3: Sözleşmeli)
        /// </summary>
        public int EmploymentType { get; set; } = 1;

        /// <summary>
        /// Çalışma saatleri (JSON formatında)
        /// </summary>
        public string WorkingHours { get; set; }

        /// <summary>
        /// Haftalık çalışma saati
        /// </summary>
        public int WeeklyWorkHours { get; set; } = 45;

        /// <summary>
        /// SGK numarası
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Banka adı
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// IBAN numarası
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// KBS'ye bildirildi mi?
        /// </summary>
        public bool IsReportedToKBS { get; set; }

        /// <summary>
        /// KBS'ye bildirim tarihi
        /// </summary>
        public DateTime? KBSReportDate { get; set; }

        /// <summary>
        /// KBS referans numarası
        /// </summary>
        public string KBSReferenceNumber { get; set; }

        /// <summary>
        /// Son sağlık raporu tarihi
        /// </summary>
        public DateTime? LastMedicalReportDate { get; set; }

        /// <summary>
        /// Acil durumda aranacak kişi
        /// </summary>
        public string EmergencyContactName { get; set; }

        /// <summary>
        /// Acil durumda aranacak kişi telefonu
        /// </summary>
        public string EmergencyContactPhone { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Profil fotoğrafı URL
        /// </summary>
        public string ProfilePhotoUrl { get; set; }

        /// <summary>
        /// Belgeler (JSON formatında)
        /// </summary>
        public string Documents { get; set; }

        /// <summary>
        /// Beceriler (JSON formatında)
        /// </summary>
        public string Skills { get; set; }

        // Navigation Properties
        /// <summary>
        /// Departman
        /// </summary>
        public virtual EmployeeDepartment Department { get; set; }

        /// <summary>
        /// Personel maaş hareketleri
        /// </summary>
        public virtual ICollection<EmployeeSalaryPayment> SalaryPayments { get; set; }

        /// <summary>
        /// Atanan servis görevleri
        /// </summary>
        public virtual ICollection<ServiceTask> AssignedTasks { get; set; }
    }
} 