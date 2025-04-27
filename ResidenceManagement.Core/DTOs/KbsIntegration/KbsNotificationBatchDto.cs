using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResidenceManagement.Core.DTOs.KbsIntegration
{
    /// <summary>
    /// KBS toplu bildirim işlemleri için DTO
    /// </summary>
    public class KbsNotificationBatchDto
    {
        /// <summary>
        /// Toplu işlem ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Bildirim türü (Giriş, Çıkış, vb.)
        /// </summary>
        [Required(ErrorMessage = "Bildirim türü gereklidir")]
        public string BildirimTuru { get; set; }
        
        /// <summary>
        /// Toplu bildirim açıklaması/başlığı
        /// </summary>
        [MaxLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir")]
        public string Aciklama { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime IslemTarihi { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Bildirilecek kişilerin listesi
        /// </summary>
        public List<int> KisiIdListesi { get; set; }
        
        /// <summary>
        /// Bildirilecek dairelerin listesi
        /// </summary>
        public List<int> DaireIdListesi { get; set; }
        
        /// <summary>
        /// Sakin türü filtresi (Daire Sahibi, Kiracı, Aile Üyesi, vb.)
        /// </summary>
        public string SakinTuruFiltresi { get; set; }
        
        /// <summary>
        /// Bildirim durumu (İşleniyor, Tamamlandı, Hata, vb.)
        /// </summary>
        public string BildirimDurumu { get; set; }
        
        /// <summary>
        /// Toplam bildirim sayısı
        /// </summary>
        public int ToplamBildirimSayisi { get; set; }
        
        /// <summary>
        /// Başarılı bildirim sayısı
        /// </summary>
        public int BasariliBildirimSayisi { get; set; }
        
        /// <summary>
        /// Başarısız bildirim sayısı
        /// </summary>
        public int BasarisizBildirimSayisi { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı ID
        /// </summary>
        public int IslemYapanKullaniciId { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string IslemYapanKullaniciAdi { get; set; }
        
        /// <summary>
        /// İşlem tamamlanma tarihi
        /// </summary>
        public DateTime? TamamlanmaTarihi { get; set; }
        
        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string HataMesaji { get; set; }
        
        /// <summary>
        /// Otomatik olarak mı oluşturuldu?
        /// </summary>
        public bool IsAutomatic { get; set; }
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
    }
} 