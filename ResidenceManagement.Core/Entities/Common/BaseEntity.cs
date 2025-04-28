using System;
using ResidenceManagement.Core.Interfaces.Common;

namespace ResidenceManagement.Core.Entities.Common
{
    /// <summary>
    /// Tüm entity'ler için temel sınıf
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Benzersiz tanımlayıcı
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı ID
        /// </summary>
        public int? LastModifiedBy { get; set; }

        /// <summary>
        /// Silindi mi?
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Yeni bir BaseEntity örneği oluşturur
        /// </summary>
        protected BaseEntity()
        {
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
    }
} 