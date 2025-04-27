using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Interfaces;

namespace ResidenceManagement.Core.Entities.Identity
{
    /// <summary>
    /// Kullanıcı refresh token bilgilerini saklayan entity
    /// </summary>
    public class RefreshToken : BaseEntity, ITenant
    {
        /// <summary>
        /// Token değeri
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Son geçerlilik tarihi
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        
        /// <summary>
        /// Oluşturulduğu IP adresi
        /// </summary>
        public string CreatedByIp { get; set; }
        
        /// <summary>
        /// Geçersiz kılınma tarihi
        /// </summary>
        public DateTime? RevokedDate { get; set; }
        
        /// <summary>
        /// Geçersiz kılan IP adresi
        /// </summary>
        public string RevokedByIp { get; set; }
        
        /// <summary>
        /// Yeni token ile değiştirildi mi
        /// </summary>
        public string ReplacedByToken { get; set; }
        
        /// <summary>
        /// Geçersiz kılınma sebebi
        /// </summary>
        public string ReasonRevoked { get; set; }
        
        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public int KullaniciId { get; set; }
        
        /// <summary>
        /// Multi-tenant için firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Multi-tenant için şube ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Token aktif mi
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Token süresi doldu mu
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        
        /// <summary>
        /// İlişkili kullanıcı
        /// </summary>
        public virtual Kullanici Kullanici { get; set; }
        
        /// <summary>
        /// Varsayılan constructor
        /// </summary>
        public RefreshToken()
        {
            IsActive = true;
        }
        
        /// <summary>
        /// Token'ın aktif olup olmadığını hesaplar
        /// </summary>
        /// <returns>Token aktif ise true, değilse false</returns>
        public bool CalculateIsActive()
        {
            return RevokedDate == null && !IsExpired;
        }
    }
} 