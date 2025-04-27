using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Rol ve Yetki arasındaki ilişkiyi tanımlayan sınıf
    /// </summary>
    public class RolYetki : BaseEntity
    {
        /// <summary>
        /// RolYetki ID
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Firma ID - BaseEntity'den override ediliyor
        /// </summary>
        public override int FirmaId { get; set; }

        /// <summary>
        /// Şube ID - BaseEntity'den override ediliyor
        /// </summary>
        public override int SubeId { get; set; }

        /// <summary>
        /// Rol ID
        /// </summary>
        public int RolId { get; set; }

        /// <summary>
        /// Yetki ID
        /// </summary>
        public int YetkiId { get; set; }

        /// <summary>
        /// İlişkili Rol nesnesi
        /// </summary>
        public virtual Rol Rol { get; set; }

        /// <summary>
        /// İlişkili Yetki nesnesi
        /// </summary>
        public virtual Yetki Yetki { get; set; }

        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public RolYetki()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
} 