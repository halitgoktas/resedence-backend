using System;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Interfaces;

namespace ResidenceManagement.Core.Entities.Base
{
    // Tüm varlıklar için temel sınıf
    // Bu sınıf firma ve şube bazlı filtreleme için ITenant arayüzünü uygular
    public abstract class BaseEntity : ITenant
    {
        // Birincil anahtar
        public virtual int Id { get; set; }

        // Firma bazlı filtreleme için FirmaId (ITenant arayüzünden uygulanıyor)
        public virtual int FirmaId { get; set; }

        // Şube bazlı filtreleme için SubeId (ITenant arayüzünden uygulanıyor)
        public virtual int SubeId { get; set; }

        // Oluşturulma tarihi
        public DateTime CreatedDate { get; set; }

        // Oluşturan kullanıcı
        public int? CreatedBy { get; set; }

        // Güncelleme tarihi
        public DateTime? UpdatedDate { get; set; }

        // Güncelleyen kullanıcı
        public int? UpdatedBy { get; set; }

        // Silindi mi işareti
        public bool IsDeleted { get; set; }

        // Silinme tarihi
        public DateTime? DeletedDate { get; set; }

        // Silen kullanıcı
        public int? DeletedBy { get; set; }

        // Aktif mi işareti
        public bool IsActive { get; set; }

        // Yapıcı metot
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
        }
    }
} 