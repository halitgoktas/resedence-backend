using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Genel
{
    /// <summary>
    /// Bina/blok bilgilerini tutan sınıf
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Bina ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Bina adı
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Bina kodu
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Bina tipi
        /// </summary>
        public string Type { get; set; } = string.Empty;
        
        /// <summary>
        /// Bağlı olduğu site/rezidans ID
        /// </summary>
        public int? ResidenceId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu site/rezidans adı
        /// </summary>
        public string ResidenceName { get; set; } = string.Empty;
        
        /// <summary>
        /// Adres bilgisi
        /// </summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Kat sayısı
        /// </summary>
        public int? NumberOfFloors { get; set; }
        
        /// <summary>
        /// Daire sayısı
        /// </summary>
        public int? NumberOfApartments { get; set; }
        
        /// <summary>
        /// Toplam alan (m²)
        /// </summary>
        public decimal? TotalArea { get; set; }
        
        /// <summary>
        /// Bina açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// İnşa tarihi
        /// </summary>
        public DateTime? ConstructionDate { get; set; }
        
        /// <summary>
        /// Son bakım tarihi
        /// </summary>
        public DateTime? LastMaintenanceDate { get; set; }
        
        /// <summary>
        /// Bina durumu (Aktif, Pasif, Bakımda vb.)
        /// </summary>
        public string Status { get; set; } = "Aktif";
        
        /// <summary>
        /// Bina içindeki dairelerin listesi
        /// </summary>
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
        
        /// <summary>
        /// Firmaya ait ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şubeye ait ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int? CreatedById { get; set; }
        
        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedById { get; set; }
    }
    
    /// <summary>
    /// Daire bilgilerini tutan sınıf
    /// </summary>
    public class Apartment
    {
        /// <summary>
        /// Daire ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Daire numarası/adı
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Daire kodu
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Bağlı olduğu bina ID
        /// </summary>
        public int? BuildingId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu bina adı
        /// </summary>
        public string BuildingName { get; set; } = string.Empty;
        
        /// <summary>
        /// Kat bilgisi
        /// </summary>
        public int? Floor { get; set; }
        
        /// <summary>
        /// Daire tipi
        /// </summary>
        public string Type { get; set; } = string.Empty;
        
        /// <summary>
        /// Daire alanı (m²)
        /// </summary>
        public decimal? Area { get; set; }
        
        /// <summary>
        /// Oda sayısı
        /// </summary>
        public int? NumberOfRooms { get; set; }
        
        /// <summary>
        /// Daire durumu (Boş, Dolu, Kirada vb.)
        /// </summary>
        public string Status { get; set; } = "Boş";
        
        /// <summary>
        /// Daire açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Firmaya ait ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şubeye ait ID
        /// </summary>
        public int SubeId { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int? CreatedById { get; set; }
        
        /// <summary>
        /// Güncelleyen kullanıcı ID
        /// </summary>
        public int? UpdatedById { get; set; }
    }
} 