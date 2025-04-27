using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Entities.Person
{
    /// <summary>
    /// Aile üyesi bilgilerini temsil eden entity sınıfı
    /// </summary>
    public class FamilyMember : BaseEntity, ITenant
    {
        /// <summary>
        /// İlişkili birincil kullanıcı (daire sakini veya daire sahibi) ID'si
        /// </summary>
        public int RelatedUserId { get; set; }
        
        /// <summary>
        /// İlişkili apartman/daire ID'si
        /// </summary>
        public int ApartmentId { get; set; }
        
        /// <summary>
        /// İlişki türü (Eş, Çocuk, Ebeveyn, vb.)
        /// </summary>
        public string RelationType { get; set; }
        
        /// <summary>
        /// Ad
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Soyad
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Tam ad bilgisi (salt okunur)
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
        
        /// <summary>
        /// TC Kimlik Numarası / Pasaport Numarası
        /// </summary>
        public string IdentityNumber { get; set; }
        
        /// <summary>
        /// Doğum tarihi
        /// </summary>
        public DateTime BirthDate { get; set; }
        
        /// <summary>
        /// Doğum yeri
        /// </summary>
        public string BirthPlace { get; set; }
        
        /// <summary>
        /// Baba adı
        /// </summary>
        public string FatherName { get; set; }
        
        /// <summary>
        /// Anne adı
        /// </summary>
        public string MotherName { get; set; }
        
        /// <summary>
        /// Cinsiyet (E/K)
        /// </summary>
        public string Gender { get; set; }
        
        /// <summary>
        /// Uyruk
        /// </summary>
        public string Nationality { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Adres bilgisi
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Kimlik belge türü (TC Kimlik, Pasaport, vb.)
        /// </summary>
        public string DocumentType { get; set; }
        
        /// <summary>
        /// Kimlik belgesi numarası
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin verildiği ülke
        /// </summary>
        public string DocumentIssuingCountry { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin verildiği tarih
        /// </summary>
        public DateTime? DocumentIssueDate { get; set; }
        
        /// <summary>
        /// Kimlik belgesinin geçerlilik tarihi
        /// </summary>
        public DateTime? DocumentExpiryDate { get; set; }
        
        /// <summary>
        /// KBS'ye bildirildi mi?
        /// </summary>
        public bool IsReportedToKbs { get; set; }
        
        /// <summary>
        /// KBS'ye bildirim tarihi
        /// </summary>
        public DateTime? KbsReportDate { get; set; }
        
        /// <summary>
        /// KBS referans numarası
        /// </summary>
        public string KbsReferenceNumber { get; set; }
        
        /// <summary>
        /// Giriş (taşınma) tarihi
        /// </summary>
        public DateTime EntryDate { get; set; }
        
        /// <summary>
        /// Çıkış (taşınma) tarihi
        /// </summary>
        public DateTime? ExitDate { get; set; }
        
        /// <summary>
        /// İkamet etme durumu aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Firma ID (Multi-tenancy için)
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID (Multi-tenancy için)
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// İlişkili kullanıcı
        /// </summary>
        public virtual Kullanici RelatedUser { get; set; }
        
        /// <summary>
        /// İlişkili apartman/daire
        /// </summary>
        public virtual Property.Apartment Apartment { get; set; }
        
        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public FamilyMember()
        {
            IsActive = true;
            IsReportedToKbs = false;
            CreatedDate = DateTime.Now;
        }
    }
} 