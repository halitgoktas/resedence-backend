using System.Collections.Generic;
using System.Threading.Tasks;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Person;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Core.Interfaces.Services
{
    /// <summary>
    /// KBS (Kimlik Bildirme Sistemi) entegrasyonu için servis arayüzü
    /// </summary>
    public interface IKbsIntegrationService
    {
        /// <summary>
        /// KBS sistemine login olur ve oturum bilgilerini döner
        /// </summary>
        /// <param name="username">KBS kullanıcı adı</param>
        /// <param name="password">KBS şifresi</param>
        /// <param name="tesisKodu">Tesis kodu</param>
        /// <returns>Oturum bilgileri</returns>
        Task<ApiResponse<string>> LoginToKbsAsync(string username, string password, string tesisKodu);
        
        /// <summary>
        /// Daire sahibini KBS'ye bildirir
        /// </summary>
        /// <param name="user">Daire sahibi kullanıcı</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportOwnerToKbsAsync(Kullanici user, Apartment apartment);
        
        /// <summary>
        /// Kiracıyı KBS'ye bildirir
        /// </summary>
        /// <param name="user">Kiracı kullanıcı</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <param name="startDate">Kiralama başlangıç tarihi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportTenantToKbsAsync(Kullanici user, Apartment apartment, System.DateTime startDate);
        
        /// <summary>
        /// Aile üyesini KBS'ye bildirir
        /// </summary>
        /// <param name="familyMember">Aile üyesi bilgisi</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportFamilyMemberToKbsAsync(FamilyMember familyMember, Apartment apartment);

        /// <summary>
        /// Daire sahibinin ayrılışını KBS'ye bildirir
        /// </summary>
        /// <param name="user">Daire sahibi kullanıcı</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <param name="exitDate">Ayrılış tarihi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportOwnerExitToKbsAsync(Kullanici user, Apartment apartment, System.DateTime exitDate);
        
        /// <summary>
        /// Kiracının ayrılışını KBS'ye bildirir
        /// </summary>
        /// <param name="user">Kiracı kullanıcı</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <param name="exitDate">Ayrılış tarihi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportTenantExitToKbsAsync(Kullanici user, Apartment apartment, System.DateTime exitDate);
        
        /// <summary>
        /// Aile üyesinin ayrılışını KBS'ye bildirir
        /// </summary>
        /// <param name="familyMember">Aile üyesi bilgisi</param>
        /// <param name="apartment">Daire bilgisi</param>
        /// <param name="exitDate">Ayrılış tarihi</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportFamilyMemberExitToKbsAsync(FamilyMember familyMember, Apartment apartment, System.DateTime exitDate);
        
        /// <summary>
        /// KBS için bildirim kaydı oluşturur
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        /// <param name="apartment">Daire</param>
        /// <param name="notificationType">Bildirim tipi (giriş/çıkış)</param>
        /// <returns>Bildirim kaydı ID</returns>
        Task<ApiResponse<int>> CreateKbsNotificationAsync(Kullanici user, Apartment apartment, string notificationType);
        
        /// <summary>
        /// KBS bildiriminin durumunu günceller
        /// </summary>
        /// <param name="notificationId">Bildirim ID</param>
        /// <param name="status">Yeni durum</param>
        /// <param name="responseDetails">Yanıt detayları</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> UpdateNotificationStatusAsync(int notificationId, string status, string responseDetails);
        
        /// <summary>
        /// Bildirimi KBS sistemine gönderir
        /// </summary>
        /// <param name="notificationId">Bildirim ID</param>
        /// <returns>Gönderim sonucu</returns>
        Task<ApiResponse<bool>> SendNotificationToKbsAsync(int notificationId);
        
        /// <summary>
        /// Tüm bekleyen bildirimleri KBS'ye gönderir
        /// </summary>
        /// <returns>Gönderim sonuçları</returns>
        Task<ApiResponse<List<string>>> SendPendingNotificationsAsync();
        
        /// <summary>
        /// Belirli bir apartmanda ikamet eden tüm kişileri (daire sahibi, kiracı, aile üyeleri) KBS'ye bildirir
        /// </summary>
        /// <param name="apartmentId">Daire ID</param>
        /// <returns>İşlem sonucu</returns>
        Task<ApiResponse<bool>> ReportAllResidentsForApartmentAsync(int apartmentId);
    }
} 