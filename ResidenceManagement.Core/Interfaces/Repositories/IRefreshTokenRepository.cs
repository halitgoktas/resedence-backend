using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Interfaces.Repositories
{
    /// <summary>
    /// RefreshToken entity'si için veritabanı işlemlerini yöneten repository arayüzü
    /// </summary>
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /// <summary>
        /// Token değerine göre refresh token getirir
        /// </summary>
        /// <param name="token">Refresh token değeri</param>
        /// <returns>Bulunan refresh token veya null</returns>
        Task<RefreshToken> GetByTokenAsync(string token);
        
        /// <summary>
        /// Kullanıcıya ait aktif refresh token'ları getirir
        /// </summary>
        /// <param name="kullaniciId">Kullanıcı kimliği</param>
        /// <returns>Kullanıcının aktif refresh token'ları</returns>
        Task<RefreshToken[]> GetActiveByUserIdAsync(int kullaniciId);
        
        /// <summary>
        /// Kullanıcının tüm refresh token'larını iptal eder
        /// </summary>
        /// <param name="kullaniciId">Kullanıcı kimliği</param>
        /// <param name="ipAddress">İşlemi yapan IP adresi</param>
        /// <param name="reason">İptal sebebi</param>
        /// <returns>İşlem sonucu</returns>
        Task<bool> RevokeAllUserTokensAsync(int kullaniciId, string ipAddress, string reason);
    }
} 