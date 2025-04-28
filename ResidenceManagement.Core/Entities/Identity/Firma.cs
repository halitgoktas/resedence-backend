using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Site yönetim sistemindeki firmaları temsil eden sınıf.
    /// Rezidans yönetim sistemini kullanan firmaların bilgilerini saklar.
    /// Multi-tenant yapının temel firma bileşenidir.
    /// </summary>
    public class Firma : BaseEntity, ResidenceManagement.Core.Common.ITenant
    {
        /// <summary>
        /// Ana kimlik - BasenEntity'den geliyor
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// FirmaId - kendi Id'si ile aynı olur
        /// </summary>
        public override int FirmaId { get; set; }

        /// <summary>
        /// Firma adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Firma vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Firma adresi
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Firma telefon numarası
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Firma e-posta adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Firma web sitesi
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// Firma logosu URL'si
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Abonelik başlangıç tarihi
        /// </summary>
        public DateTime SubscriptionStartDate { get; set; }

        /// <summary>
        /// Abonelik bitiş tarihi
        /// </summary>
        public DateTime? SubscriptionEndDate { get; set; }

        /// <summary>
        /// Firmaya bağlı şubeler
        /// </summary>
        public virtual ICollection<Sube> Subeler { get; set; }

        /// <summary>
        /// Firmaya bağlı kullanıcılar
        /// </summary>
        public virtual ICollection<Kullanici> Users { get; set; }

        public Firma()
        {
            Subeler = new HashSet<Sube>();
            Users = new HashSet<Kullanici>();
            IsActive = true;
            // FirmaId, kendi Id'si ile aynı olur
            FirmaId = Id;
        }
    }
} 