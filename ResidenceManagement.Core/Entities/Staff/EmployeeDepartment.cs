using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Staff
{
    /// <summary>
    /// Personel departmanları sınıfı
    /// </summary>
    public class EmployeeDepartment : BaseLookupEntity
    {
        /// <summary>
        /// Departman kodu
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Departman açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Departman yöneticisi ID
        /// </summary>
        public int? ManagerId { get; set; }

        /// <summary>
        /// Departman bütçesi
        /// </summary>
        public decimal? Budget { get; set; }

        /// <summary>
        /// Bütçe para birimi
        /// </summary>
        public string BudgetCurrency { get; set; } = "TRY";

        /// <summary>
        /// Departman renk kodu
        /// </summary>
        public string ColorCode { get; set; }

        // Navigation Properties
        /// <summary>
        /// Departman çalışanları
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Departman yöneticisi
        /// </summary>
        public virtual Employee Manager { get; set; }
    }
} 