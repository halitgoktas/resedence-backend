using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Kullanıcı rollerini tanımlayan sınıf
    /// </summary>
    public class Rol : BaseEntity
    {
        /// <summary>
        /// Rol ID
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
        /// Rol adı
        /// </summary>
        public string Ad { get; set; }

        /// <summary>
        /// Rol açıklaması
        /// </summary>
        public string Aciklama { get; set; }

        /// <summary>
        /// Sistem rolü mü? (Önceden tanımlı ve değiştirilemez roller)
        /// </summary>
        public bool SistemRolu { get; set; }

        /// <summary>
        /// Bu role atanmış kullanıcılar
        /// </summary>
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }

        /// <summary>
        /// Bu role verilen yetkiler
        /// </summary>
        public virtual ICollection<RolYetki> RolYetkiler { get; set; }

        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public Rol()
        {
            Kullanicilar = new HashSet<Kullanici>();
            RolYetkiler = new HashSet<RolYetki>();
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
} 