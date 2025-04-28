using ResidenceManagement.Core.Entities.Identity;
using System.Threading.Tasks;

namespace ResidenceManagement.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetActiveTokenAsync(string token);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
} 