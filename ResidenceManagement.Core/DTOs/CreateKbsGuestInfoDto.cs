using System;

namespace ResidenceManagement.Core.DTOs
{
    /// <summary>
    /// KBS'ye misafir bilgisi göndermek için DTO sınıfı
    /// </summary>
    public class CreateKbsGuestInfoDto
    {
        /// <summary>
        /// Misafir adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Misafir soyadı
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Kimlik/Pasaport türü
        /// </summary>
        public string IdType { get; set; }
        
        /// <summary>
        /// Kimlik/Pasaport numarası
        /// </summary>
        public string IdNumber { get; set; }
        
        /// <summary>
        /// Doğum tarihi
        /// </summary>
        public DateTime BirthDate { get; set; }
        
        /// <summary>
        /// Cinsiyet
        /// </summary>
        public string Gender { get; set; }
        
        /// <summary>
        /// Uyruk
        /// </summary>
        public string Nationality { get; set; }
        
        /// <summary>
        /// Baba adı
        /// </summary>
        public string FatherName { get; set; }
        
        /// <summary>
        /// Anne adı
        /// </summary>
        public string MotherName { get; set; }
        
        /// <summary>
        /// Doğum yeri
        /// </summary>
        public string BirthPlace { get; set; }
        
        /// <summary>
        /// Telefon
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// E-posta
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Giriş tarihi
        /// </summary>
        public DateTime CheckInDate { get; set; }
        
        /// <summary>
        /// Tahmini çıkış tarihi
        /// </summary>
        public DateTime ExpectedCheckOutDate { get; set; }
        
        /// <summary>
        /// İlgili daire/oda numarası
        /// </summary>
        public string RoomNumber { get; set; }
    }
} 