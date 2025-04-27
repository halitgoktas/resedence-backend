using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Data.Repositories
{
    /// <summary>
    /// RefreshToken repository implementasyonu
    /// </summary>
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        /// <summary>
        /// RefreshTokenRepository constructor
        /// </summary>
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        /// <inheritdoc/>
        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _dbContext.Set<RefreshToken>()
                .Include(rt => rt.Kullanici)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }
        
        /// <inheritdoc/>
        public async Task<RefreshToken[]> GetActiveByUserIdAsync(int kullaniciId)
        {
            return await _dbContext.Set<RefreshToken>()
                .Where(rt => rt.KullaniciId == kullaniciId && rt.RevokedDate == null && rt.ExpirationDate > DateTime.UtcNow)
                .ToArrayAsync();
        }
        
        /// <inheritdoc/>
        public async Task<bool> RevokeAllUserTokensAsync(int kullaniciId, string ipAddress, string reason)
        {
            var activeTokens = await GetActiveByUserIdAsync(kullaniciId);
            
            if (activeTokens == null || activeTokens.Length == 0)
                return false;
            
            foreach (var token in activeTokens)
            {
                token.RevokedDate = DateTime.UtcNow;
                token.RevokedByIp = ipAddress;
                token.ReasonRevoked = reason;
            }
            
            return true;
        }
    }
} 