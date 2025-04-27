using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.Person
{
    /// <summary>
    /// Aile üyesi veri transfer nesnesi
    /// </summary>
    public class FamilyMemberDto
    {
        /// <summary>
        /// Aile üyesi ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// İlişkili birincil kullanıcı ID'si
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
        /// Tam ad
        /// </summary>
        public string FullName { get; set; }
        
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
        /// Kimlik belge türü
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
    }
} 