using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Kullanıcı-Rol ilişkisini tanımlayan sınıf
    /// </summary>
    public class UserRole : BaseEntity, ResidenceManagement.Core.Common.ITenant
    {
        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public int KullaniciId { get; set; }
        
        /// <summary>
        /// Rol ID
        /// </summary>
        public int RolId { get; set; }
        
        /// <summary>
        /// Kullanıcıya rolün atanma tarihi
        /// </summary>
        public DateTime AtamaTarihi { get; set; }
        
        /// <summary>
        /// Kullanıcının bağlı olduğu firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Kullanıcının bağlı olduğu şube ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Rolün geçerlilik bitiş tarihi (opsiyonel)
        /// </summary>
        public DateTime? BitisTarihi { get; set; }
        
        /// <summary>
        /// Rolün durumu (aktif/pasif)
        /// </summary>
        public bool AktifMi { get; set; }
        
        /// <summary>
        /// İlişkili kullanıcı
        /// </summary>
        public virtual Kullanici Kullanici { get; set; }
        
        /// <summary>
        /// İlişkili rol
        /// </summary>
        public virtual Rol Rol { get; set; }
        
        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public UserRole()
        {
            AtamaTarihi = DateTime.Now;
            AktifMi = true;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
} 