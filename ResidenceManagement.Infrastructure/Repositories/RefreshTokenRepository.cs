using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> GetActiveTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            return await _dbContext.RefreshTokenlar
                .FirstOrDefaultAsync(rt => rt.Token == token && rt.IsActive && !rt.IsRevoked);
        }
        
        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            return await _dbContext.RefreshTokenlar
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }
        
        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokenlar.AddAsync(refreshToken);
        }
        
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
} 