using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.User;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Kullanıcı yönetimi için servis arayüzü
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Kullanıcı adı veya email ile kullanıcı arar
        /// </summary>
        /// <param name="usernameOrEmail">Kullanıcı adı veya email</param>
        /// <returns>Bulunan kullanıcı</returns>
        Task<Kullanici> FindByUsernameOrEmailAsync(string usernameOrEmail);
        
        /// <summary>
        /// Şifre doğrulaması yapar
        /// </summary>
        /// <param name="kullanici">Kullanıcı</param>
        /// <param name="password">Kontrol edilecek şifre</param>
        /// <returns>Şifre doğru ise true</returns>
        Task<bool> ValidatePasswordAsync(Kullanici kullanici, string password);
        
        /// <summary>
        /// Tüm kullanıcıları listeler
        /// </summary>
        /// <returns>Kullanıcı listesi</returns>
        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();
        
        /// <summary>
        /// ID ile kullanıcı getirir
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <returns>Kullanıcı bilgisi</returns>
        Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);
        
        /// <summary>
        /// Yeni kullanıcı kaydı yapar
        /// </summary>
        /// <param name="createUserDto">Kullanıcı bilgileri</param>
        /// <returns>Oluşturulan kullanıcı</returns>
        Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        
        /// <summary>
        /// Kullanıcı bilgilerini günceller
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <param name="updateUserDto">Güncellenecek bilgiler</param>
        /// <returns>Güncellenen kullanıcı</returns>
        Task<ApiResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        
        /// <summary>
        /// Kullanıcıyı siler (soft delete)
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> DeleteUserAsync(int id);
        
        /// <summary>
        /// Kullanıcının şifresini değiştirir
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <param name="changePasswordDto">Şifre değiştirme bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);
        
        /// <summary>
        /// Kullanıcının rollerini getirir
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <returns>Kullanıcı rolleri</returns>
        Task<ApiResponse<List<string>>> GetUserRolesAsync(int id);
        
        /// <summary>
        /// Kullanıcıya rol atar
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <param name="roleName">Rol adı</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> AssignRoleToUserAsync(int id, string roleName);
        
        /// <summary>
        /// Kullanıcıdan rol kaldırır
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <param name="roleName">Rol adı</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> RemoveRoleFromUserAsync(int id, string roleName);
    }
} 