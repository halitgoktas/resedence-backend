using System.Collections.Generic;

namespace ResidenceManagement.Core.DTOs.Authentication
{
    /// <summary>
    /// Kullanıcı bilgilerini içeren yanıt modeli
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// Kullanıcı kimliği
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Kullanıcının adı
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Kullanıcının soyadı
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Kullanıcının e-posta adresi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Kullanıcının telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Kullanıcının rolleri
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// Kullanıcının bağlı olduğu firma kimliği
        /// </summary>
        public int? FirmaId { get; set; }

        /// <summary>
        /// Kullanıcının bağlı olduğu şube kimliği
        /// </summary>
        public int? SubeId { get; set; }

        /// <summary>
        /// Firma ID (CompanyId ile aynı, uyumluluk için)
        /// </summary>
        public int? CompanyId
        {
            get => FirmaId;
            set => FirmaId = value;
        }

        /// <summary>
        /// Şube ID (BranchId ile aynı, uyumluluk için)
        /// </summary>
        public int? BranchId
        {
            get => SubeId;
            set => SubeId = value;
        }
    }
} 