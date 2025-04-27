using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    /// <summary>
    /// Hizmet kategorilerini temsil eden entity
    /// </summary>
    public class ServiceCategory : BaseLookupEntity
    {
        /// <summary>
        /// Üst kategori ID (hiyerarşik yapı için)
        /// </summary>
        public int? ParentCategoryId { get; set; }
        
        /// <summary>
        /// Kategori renk kodu (UI gösterimi için)
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Kategori ikonu (UI gösterimi için)
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// Tahmini çözüm süresi (saat)
        /// </summary>
        public int? EstimatedResolutionHours { get; set; }
        
        /// <summary>
        /// İlişkiler
        /// </summary>
        public virtual ServiceCategory ParentCategory { get; set; }
        public virtual ICollection<ServiceCategory> SubCategories { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        
        public ServiceCategory()
        {
            SubCategories = new HashSet<ServiceCategory>();
            ServiceRequests = new HashSet<ServiceRequest>();
        }
    }
} 