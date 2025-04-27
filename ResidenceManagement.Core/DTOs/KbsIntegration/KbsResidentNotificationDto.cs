using System;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.KbsIntegration
{
    /// <summary>
    /// KBS'ye bildirilecek daire sakini bilgilerini içeren DTO
    /// </summary>
    public class KbsResidentNotificationDto
    {
        /// <summary>
        /// TC Kimlik / Pasaport Numarası
        /// </summary>
        [Required(ErrorMessage = "Kimlik numarası gereklidir")]
        [MaxLength(20, ErrorMessage = "Kimlik numarası en fazla 20 karakter olabilir")]
        public string KimlikNo { get; set; }
        
        /// <summary>
        /// Belge Tipi (NUFUS, PASAPORT, vb.)
        /// </summary>
        [Required(ErrorMessage = "Belge tipi gereklidir")]
        [MaxLength(50, ErrorMessage = "Belge tipi en fazla 50 karakter olabilir")]
        public string BelgeTipi { get; set; }
        
        /// <summary>
        /// Adı
        /// </summary>
        [Required(ErrorMessage = "Ad gereklidir")]
        [MaxLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string Adi { get; set; }
        
        /// <summary>
        /// Soyadı
        /// </summary>
        [Required(ErrorMessage = "Soyad gereklidir")]
        [MaxLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string Soyadi { get; set; }
        
        /// <summary>
        /// Baba Adı
        /// </summary>
        [MaxLength(100, ErrorMessage = "Baba adı en fazla 100 karakter olabilir")]
        public string BabaAdi { get; set; }
        
        /// <summary>
        /// Anne Adı
        /// </summary>
        [MaxLength(100, ErrorMessage = "Anne adı en fazla 100 karakter olabilir")]
        public string AnneAdi { get; set; }
        
        /// <summary>
        /// Doğum Tarihi
        /// </summary>
        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        public DateTime DogumTarihi { get; set; }
        
        /// <summary>
        /// Doğum Yeri
        /// </summary>
        [MaxLength(100, ErrorMessage = "Doğum yeri en fazla 100 karakter olabilir")]
        public string DogumYeri { get; set; }
        
        /// <summary>
        /// Uyruğu
        /// </summary>
        [Required(ErrorMessage = "Uyruğu gereklidir")]
        [MaxLength(50, ErrorMessage = "Uyruk en fazla 50 karakter olabilir")]
        public string Uyruk { get; set; }
        
        /// <summary>
        /// Cinsiyet (E/K)
        /// </summary>
        [Required(ErrorMessage = "Cinsiyet gereklidir")]
        [MaxLength(10, ErrorMessage = "Cinsiyet en fazla 10 karakter olabilir")]
        public string Cinsiyet { get; set; }
        
        /// <summary>
        /// Medeni Hali (Evli/Bekar)
        /// </summary>
        [MaxLength(20, ErrorMessage = "Medeni hal en fazla 20 karakter olabilir")]
        public string MedeniHali { get; set; }
        
        /// <summary>
        /// Telefon Numarası
        /// </summary>
        [MaxLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string Telefon { get; set; }
        
        /// <summary>
        /// E-posta Adresi
        /// </summary>
        [MaxLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Eposta { get; set; }
        
        /// <summary>
        /// Önceki Adres
        /// </summary>
        [MaxLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string OncekiAdres { get; set; }
        
        /// <summary>
        /// Şehir / İlçe
        /// </summary>
        [MaxLength(50, ErrorMessage = "Şehir/İlçe en fazla 50 karakter olabilir")]
        public string SehirIlce { get; set; }
        
        /// <summary>
        /// Diğer adres bilgileri (fatura adresi vb.)
        /// </summary>
        [MaxLength(500, ErrorMessage = "Diğer adres bilgileri en fazla 500 karakter olabilir")]
        public string DigerAdresBilgileri { get; set; }
        
        /// <summary>
        /// Daire / Oda Numarası
        /// </summary>
        [Required(ErrorMessage = "Daire/Oda numarası gereklidir")]
        [MaxLength(20, ErrorMessage = "Daire/Oda numarası en fazla 20 karakter olabilir")]
        public string DaireNo { get; set; }
        
        /// <summary>
        /// Blok / Bina
        /// </summary>
        [MaxLength(50, ErrorMessage = "Blok/Bina en fazla 50 karakter olabilir")]
        public string Blok { get; set; }
        
        /// <summary>
        /// Site / Adres
        /// </summary>
        [Required(ErrorMessage = "Site/Adres gereklidir")]
        [MaxLength(200, ErrorMessage = "Site/Adres en fazla 200 karakter olabilir")]
        public string SiteAdres { get; set; }
        
        /// <summary>
        /// Bildirim Tipi (Giriş veya Çıkış)
        /// </summary>
        [Required(ErrorMessage = "Bildirim tipi gereklidir")]
        [MaxLength(20, ErrorMessage = "Bildirim tipi en fazla 20 karakter olabilir")]
        public string BildirimTipi { get; set; }
        
        /// <summary>
        /// Giriş Tarihi (Taşınma)
        /// </summary>
        [Required(ErrorMessage = "Giriş tarihi gereklidir")]
        public DateTime GirisTarihi { get; set; }
        
        /// <summary>
        /// Çıkış Tarihi (Ayrılış)
        /// </summary>
        public DateTime? CikisTarihi { get; set; }
        
        /// <summary>
        /// Kimlik belgesi resminin Base64 kodlanmış verisi
        /// </summary>
        public string KimlikBelgesiResmi { get; set; }
        
        /// <summary>
        /// Kişi tipi (Mülk Sahibi, Kiracı, Aile Üyesi)
        /// </summary>
        [Required(ErrorMessage = "Kişi tipi gereklidir")]
        [MaxLength(50, ErrorMessage = "Kişi tipi en fazla 50 karakter olabilir")]
        public string KisiTipi { get; set; }
        
        /// <summary>
        /// Aile üyesi ise, bağlı olduğu kişinin ID'si
        /// </summary>
        public int? BagliKisiId { get; set; }
        
        /// <summary>
        /// Konaklama referans numarası (varsa)
        /// </summary>
        public string KonaklamaReferansNo { get; set; }
    }
} 