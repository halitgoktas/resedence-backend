using System.Threading.Tasks;
using ResidenceManagement.Core.DTOs.Authentication;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Kimlik doğrulama ve yetkilendirme servis arayüzü
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştirir
        /// </summary>
        /// <param name="request">Giriş bilgileri</param>
        /// <returns>Token içeren bir ApiResponse</returns>
        Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest request);

        /// <summary>
        /// Token yenileme işlemini gerçekleştirir
        /// </summary>
        /// <param name="request">Yenileme isteği bilgileri</param>
        /// <returns>Token içeren bir ApiResponse</returns>
        Task<ApiResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);

        /// <summary>
        /// Çıkış işlemini gerçekleştirir, refresh token'ı iptal eder
        /// </summary>
        /// <param name="token">İptal edilecek refresh token</param>
        /// <returns>İşlemin başarı durumunu bildiren ApiResponse</returns>
        Task<ApiResponse<bool>> RevokeTokenAsync(string token);

        /// <summary>
        /// Kullanıcı bilgilerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <returns>Kullanıcı bilgilerini içeren yanıt</returns>
        Task<ApiResponse<UserInfoResponse>> GetUserInfoAsync(int userId);
    }
} 