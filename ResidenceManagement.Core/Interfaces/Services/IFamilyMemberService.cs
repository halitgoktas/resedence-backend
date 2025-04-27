using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.Person;
using ResidenceManagement.Core.Entities.Person;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// Aile üyesi yönetimi için servis arayüzü
    /// </summary>
    public interface IFamilyMemberService
    {
        /// <summary>
        /// Tüm aile üyelerini getirir
        /// </summary>
        /// <returns>Aile üyeleri listesi</returns>
        Task<ApiResponse<List<FamilyMemberDto>>> GetAllFamilyMembersAsync();
        
        /// <summary>
        /// Belirli bir kullanıcının aile üyelerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <returns>Aile üyeleri listesi</returns>
        Task<ApiResponse<List<FamilyMemberDto>>> GetFamilyMembersByUserIdAsync(int userId);
        
        /// <summary>
        /// Belirli bir dairenin aile üyelerini getirir
        /// </summary>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Aile üyeleri listesi</returns>
        Task<ApiResponse<List<FamilyMemberDto>>> GetFamilyMembersByApartmentIdAsync(int apartmentId);
        
        /// <summary>
        /// ID'ye göre aile üyesi getirir
        /// </summary>
        /// <param name="id">Aile üyesi ID</param>
        /// <returns>Aile üyesi bilgisi</returns>
        Task<ApiResponse<FamilyMemberDto>> GetFamilyMemberByIdAsync(int id);
        
        /// <summary>
        /// Yeni aile üyesi ekler
        /// </summary>
        /// <param name="createFamilyMemberDto">Aile üyesi bilgileri</param>
        /// <returns>Eklenen aile üyesi</returns>
        Task<ApiResponse<FamilyMemberDto>> CreateFamilyMemberAsync(CreateFamilyMemberDto createFamilyMemberDto);
        
        /// <summary>
        /// Aile üyesi bilgilerini günceller
        /// </summary>
        /// <param name="id">Aile üyesi ID</param>
        /// <param name="updateFamilyMemberDto">Güncellenecek bilgiler</param>
        /// <returns>Güncellenen aile üyesi</returns>
        Task<ApiResponse<FamilyMemberDto>> UpdateFamilyMemberAsync(int id, CreateFamilyMemberDto updateFamilyMemberDto);
        
        /// <summary>
        /// Aile üyesini siler
        /// </summary>
        /// <param name="id">Aile üyesi ID</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> DeleteFamilyMemberAsync(int id);
        
        /// <summary>
        /// Aile üyesini KBS'ye bildirir
        /// </summary>
        /// <param name="id">Aile üyesi ID</param>
        /// <returns>Bildirim sonucu</returns>
        Task<ApiResponse<bool>> ReportToKbsAsync(int id);
        
        /// <summary>
        /// Dairede oturanların tümünü (daire sahibi, kiracı, aile üyeleri) KBS'ye bildirir
        /// </summary>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>Bildirim sonucu</returns>
        Task<ApiResponse<bool>> ReportAllResidentsToKbsAsync(int apartmentId);
        
        /// <summary>
        /// TC kimlik numarasıyla aile üyesi arar
        /// </summary>
        /// <param name="identityNumber">TC Kimlik / Pasaport numarası</param>
        /// <returns>Bulunan aile üyesi</returns>
        Task<ApiResponse<FamilyMemberDto>> FindByIdentityNumberAsync(string identityNumber);
    }
} 