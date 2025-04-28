using System;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Yenileme token'ları için entity sınıfı
    /// </summary>
    public class RefreshToken : ResidenceManagement.Core.Entities.Base.BaseEntity, ResidenceManagement.Core.Common.ITenant
    {
        /// <summary>
        /// Token ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Token değeri
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Token'ın ait olduğu kullanıcı ID
        /// </summary>
        public int KullaniciId { get; set; }
        
        /// <summary>
        /// Token oluşturulma tarihi
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// Token geçerlilik bitiş tarihi
        /// </summary>
        public DateTime ExpiryDate { get; set; }
        
        /// <summary>
        /// Token son geçerlilik tarihi (alternatif isim)
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        
        /// <summary>
        /// Son kullanılan IP adresi
        /// </summary>
        public string LastUsedIp { get; set; }
        
        /// <summary>
        /// Son kullanım tarihi
        /// </summary>
        public DateTime? LastUsedDate { get; set; }
        
        /// <summary>
        /// Token aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Token süresi doldu mu?
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        
        /// <summary>
        /// Token iptal edildi mi?
        /// </summary>
        public bool IsRevoked { get; set; }
        
        /// <summary>
        /// Token iptal tarihi
        /// </summary>
        public DateTime? RevokedDate { get; set; }
        
        /// <summary>
        /// Token'ı oluşturan IP adresi
        /// </summary>
        public string CreatedByIp { get; set; }
        
        /// <summary>
        /// Token'ı iptal eden IP adresi
        /// </summary>
        public string RevokedByIp { get; set; }
        
        /// <summary>
        /// Bu token yerine kullanılacak yeni token
        /// </summary>
        public string ReplacedByToken { get; set; }
        
        /// <summary>
        /// Token'ın iptal edilme nedeni
        /// </summary>
        public string ReasonRevoked { get; set; }
        
        /// <summary>
        /// Oluşturulan cihaz bilgisi
        /// </summary>
        public string DeviceInfo { get; set; }
        
        /// <summary>
        /// Kullanıcı navigasyon özelliği
        /// </summary>
        public virtual Kullanici Kullanici { get; set; }
        
        /// <summary>
        /// Firma ID - Multi-tenant yapı için
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID - Multi-tenant yapı için
        /// </summary>
        public int SubeId { get; set; }
    }
} 