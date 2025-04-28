using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Firma altındaki şubeleri temsil eden sınıf.
    /// Her şube bir firmaya bağlıdır ve multi-tenant yapıda SubeId üzerinden filtrelenir.
    /// Şubeler, bir firmanın farklı bölgelerdeki/illerdeki lokasyonlarıdır.
    /// </summary>
    public class Sube : BaseEntity, ResidenceManagement.Core.Common.ITenant
    {
        /// <summary>
        /// Şubenin benzersiz kimliği
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Şubenin bağlı olduğu firma ID
        /// </summary>
        public override int FirmaId { get; set; }

        /// <summary>
        /// Şube Id - kendi Id'si ile aynı olur
        /// </summary>
        public override int SubeId { get; set; }

        /// <summary>
        /// Şube adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Şube adresi
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Şube telefon numarası
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Şube e-posta adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Şube sorumlusu
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// Şube açılış tarihi
        /// </summary>
        public DateTime OpeningDate { get; set; }

        /// <summary>
        /// Şube kapanış tarihi (eğer kapatıldıysa)
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Şube bölgesi/şehri
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Şubenin bağlı olduğu firma
        /// </summary>
        public virtual Firma Firma { get; set; }

        /// <summary>
        /// Şubeye bağlı kullanıcılar
        /// </summary>
        public virtual ICollection<Kullanici> Users { get; set; }

        public Sube()
        {
            Users = new HashSet<Kullanici>();
            IsActive = true;
            CreatedDate = DateTime.Now;
            // SubeId, kendi Id'si ile aynı olur
            SubeId = Id;
        }
    }
} 