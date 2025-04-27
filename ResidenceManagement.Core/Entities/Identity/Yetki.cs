using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Sistem yetkilerini tanımlayan sınıf
    /// </summary>
    public class Yetki : BaseEntity
    {
        /// <summary>
        /// Yetki ID
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
        /// Yetki adı
        /// </summary>
        public string Ad { get; set; }

        /// <summary>
        /// Yetki açıklaması
        /// </summary>
        public string Aciklama { get; set; }

        /// <summary>
        /// Yetki kodu (benzersiz tanımlayıcı)
        /// </summary>
        public string Kod { get; set; }

        /// <summary>
        /// Yetki kategorisi (Menü, İşlem, Rapor vb.)
        /// </summary>
        public string Kategori { get; set; }

        /// <summary>
        /// Sistem yetkisi mi? (Önceden tanımlı ve değiştirilemez yetkiler)
        /// </summary>
        public bool SistemYetkisi { get; set; }

        /// <summary>
        /// Bu yetkiye sahip rol yetki ilişkileri
        /// </summary>
        public virtual ICollection<RolYetki> RolYetkiler { get; set; }

        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public Yetki()
        {
            RolYetkiler = new HashSet<RolYetki>();
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
} 