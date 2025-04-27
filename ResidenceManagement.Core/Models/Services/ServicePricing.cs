using System;

namespace ResidenceManagement.Core.Models.Services
{
    // Hizmet fiyatlandırma sınıfı
    public class ServicePricing
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Hangi hizmet tanımına ait
        public int ServiceDefinitionId { get; set; }
        public ServiceDefinition ServiceDefinition { get; set; }
        
        // Fiyat adı (örn: "Standart", "Premium", "Hafta Sonu")
        public string Name { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Para birimi
        public string CurrencyCode { get; set; }
        
        // Birim fiyat
        public decimal UnitPrice { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // Minimum ücret
        public decimal MinimumCharge { get; set; }
        
        // Saatlik ücret (saatlik hizmetler için)
        public decimal? HourlyRate { get; set; }
        
        // Metrekare başına ücret (alan bazlı hizmetler için)
        public decimal? PricePerSquareMeter { get; set; }
        
        // Malzeme dahil mi?
        public bool IncludesMaterials { get; set; }
        
        // Malzeme fiyatı (dahil değilse)
        public decimal? MaterialCost { get; set; }
        
        // Geçerlilik başlangıç tarihi
        public DateTime EffectiveFrom { get; set; }
        
        // Geçerlilik bitiş tarihi (null ise süresiz)
        public DateTime? EffectiveTo { get; set; }
        
        // Özel fiyat mı? (Belirli müşteriler için)
        public bool IsCustomPrice { get; set; }
        
        // Özel fiyat ise hangi müşteri/müşteri grubu için
        public int? CustomerGroupId { get; set; }
        
        // Aktif mi?
        public bool IsActive { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public ServicePricing()
        {
            EffectiveFrom = DateTime.Now;
            IsActive = true;
            CurrencyCode = "TRY";
            VATRate = 20; // Varsayılan %20 KDV
            CreatedDate = DateTime.Now;
            MinimumCharge = 0;
            IncludesMaterials = false;
        }
    }
} 