using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Entities.Identity
{
    // UserRole sınıfı, kullanıcılar ve roller arasındaki ilişkiyi tanımlar
    public class UserRole : BaseEntity, ITenant
    {
        // Kullanıcı ID'si
        public Guid KullaniciId { get; set; }
        
        // Rol ID'si
        public Guid RolId { get; set; }
        
        // Kullanıcıya rolün atanma tarihi
        public DateTime AtamaTarihi { get; set; }
        
        // Kullanıcının bağlı olduğu firma ID'si (multi-tenant yapı için)
        public Guid FirmaId { get; set; }
        
        // Kullanıcının bağlı olduğu şube ID'si (multi-tenant yapı için)
        public Guid SubeId { get; set; }
        
        // Rolün geçerlilik bitiş tarihi (opsiyonel)
        public DateTime? BitisTarihi { get; set; }
        
        // Rolün durumu (aktif/pasif)
        public bool AktifMi { get; set; }
        
        // Navigation Properties
        // İlişkili kullanıcı
        public virtual Kullanici Kullanici { get; set; }
        
        // İlişkili rol
        public virtual Rol Rol { get; set; }
        
        public UserRole()
        {
            AtamaTarihi = DateTime.Now;
            AktifMi = true;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
} 