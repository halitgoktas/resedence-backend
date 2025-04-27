using System;
using System.Collections.Generic;
using System.Linq;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Hizmet/servis tanımlarını içeren sınıf
    /// </summary>
    public class ServiceDefinition : BaseEntity
    {
        /// <summary>
        /// Hizmet kodu
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Hizmet adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Hizmet açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Hizmet kategori ID'si
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Hizmet kategori adı
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Hizmet servis kategori ID'si
        /// </summary>
        public int? ServiceCategoryId { get; set; }
        
        /// <summary>
        /// Hizmet alt kategori ID'si
        /// </summary>
        public int? SubCategoryId { get; set; }
        
        /// <summary>
        /// Hizmet alt kategori adı
        /// </summary>
        public string SubCategoryName { get; set; }
        
        /// <summary>
        /// Standart birim fiyat
        /// </summary>
        public decimal StandardPrice { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// KDV oranı
        /// </summary>
        public decimal VATRate { get; set; }
        
        /// <summary>
        /// Fiyat KDV dahil mi?
        /// </summary>
        public bool IsPriceIncludeVAT { get; set; }
        
        /// <summary>
        /// Fiyatlandırma tipi (Sabit, Saatlik, Metrekare vb)
        /// </summary>
        public string PricingType { get; set; }
        
        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        public int EstimatedDuration { get; set; }
        
        /// <summary>
        /// Varsayılan öncelik seviyesi
        /// </summary>
        public int DefaultPriorityLevel { get; set; }
        
        /// <summary>
        /// Hizmet türü (Arıza, Bakım, Temizlik, vb)
        /// </summary>
        public string ServiceType { get; set; }
        
        /// <summary>
        /// Varsayılan atanan teknisyen ID'si
        /// </summary>
        public int? DefaultAssignedToId { get; set; }
        
        /// <summary>
        /// Varsayılan atanan teknisyen adı
        /// </summary>
        public string DefaultAssignedToName { get; set; }
        
        /// <summary>
        /// Dış kaynaklı mı? (Dışarıdan firma hizmeti mi?)
        /// </summary>
        public bool IsOutsourced { get; set; }
        
        /// <summary>
        /// Tedarikçi firma ID'si
        /// </summary>
        public int? VendorId { get; set; }
        
        /// <summary>
        /// Tedarikçi firma adı
        /// </summary>
        public string VendorName { get; set; }
        
        /// <summary>
        /// Kontrol listesi/yapılacaklar JSON formatında
        /// </summary>
        public string Checklist { get; set; }
        
        /// <summary>
        /// Gerekli belgeler
        /// </summary>
        public string RequiredDocuments { get; set; }
        
        /// <summary>
        /// Güvenlik talimatları
        /// </summary>
        public string SafetyInstructions { get; set; }
        
        /// <summary>
        /// Hizmet görseli URL'i
        /// </summary>
        public string ImageUrl { get; set; }
        
        /// <summary>
        /// Hizmet aktif mi?
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Görüntüleme sırası
        /// </summary>
        public int DisplayOrder { get; set; }
        
        /// <summary>
        /// Acil servis mi?
        /// </summary>
        public bool IsEmergencyService { get; set; }
        
        /// <summary>
        /// Mobil uygulamadan talep edilebilir mi?
        /// </summary>
        public bool CanBeRequestedFromMobileApp { get; set; }
        
        /// <summary>
        /// Online ödeme gerektirir mi?
        /// </summary>
        public bool RequiresOnlinePayment { get; set; }
        
        /// <summary>
        /// Randevu gerektirir mi?
        /// </summary>
        public bool RequiresAppointment { get; set; }
        
        /// <summary>
        /// Toplam malzeme maliyeti
        /// </summary>
        public decimal MaterialCost { get; set; }
        
        /// <summary>
        /// Toplam servis maliyeti
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// Hizmetle ilgili malzemeler
        /// </summary>
        public virtual ICollection<ServiceDefinitionMaterial> Materials { get; set; }
        
        /// <summary>
        /// Bu hizmetle ilgili servis talepleri
        /// </summary>
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        
        /// <summary>
        /// Bu hizmetle ilgili bakım planları
        /// </summary>
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        
        public ServiceDefinition()
        {
            Materials = new List<ServiceDefinitionMaterial>();
            ServiceRequests = new List<ServiceRequest>();
            MaintenancePlans = new List<MaintenancePlan>();
            CreatedDate = DateTime.Now;
            IsActive = true;
            StandardPrice = 0;
            VATRate = 0.18M; // %18 KDV varsayılan
            IsPriceIncludeVAT = true;
            PricingType = "Sabit";
            EstimatedDuration = 60; // dakika
            DefaultPriorityLevel = 3; // Normal
            Currency = "TRY";
            DisplayOrder = 100;
            IsEmergencyService = false;
            CanBeRequestedFromMobileApp = true;
            RequiresOnlinePayment = false;
            RequiresAppointment = true;
            IsOutsourced = false;
            MaterialCost = 0;
            TotalCost = 0;
            CreatedBy = null; // String yerine null değer ata
        }
        
        /// <summary>
        /// KDV dahil fiyatı hesaplar
        /// </summary>
        /// <returns>KDV dahil fiyat</returns>
        public decimal CalculatePriceWithVAT()
        {
            if (IsPriceIncludeVAT)
            {
                return StandardPrice;
            }
            else
            {
                return StandardPrice * (1 + VATRate);
            }
        }
        
        /// <summary>
        /// KDV hariç fiyatı hesaplar
        /// </summary>
        /// <returns>KDV hariç fiyat</returns>
        public decimal CalculatePriceWithoutVAT()
        {
            if (IsPriceIncludeVAT)
            {
                return StandardPrice / (1 + VATRate);
            }
            else
            {
                return StandardPrice;
            }
        }
        
        /// <summary>
        /// KDV tutarını hesaplar
        /// </summary>
        /// <returns>KDV tutarı</returns>
        public decimal CalculateVATAmount()
        {
            if (IsPriceIncludeVAT)
            {
                return StandardPrice - (StandardPrice / (1 + VATRate));
            }
            else
            {
                return StandardPrice * VATRate;
            }
        }
        
        /// <summary>
        /// Hizmet tanımına malzeme ekler
        /// </summary>
        /// <param name="materialName">Malzeme adı</param>
        /// <param name="materialCode">Malzeme kodu</param>
        /// <param name="unitPrice">Birim fiyat</param>
        /// <param name="quantity">Miktar</param>
        /// <param name="unit">Birim</param>
        public void AddMaterial(string materialName, string materialCode, decimal unitPrice, int quantity, string unit)
        {
            // Servis tanımına malzeme ekler
            if (Materials == null)
            {
                Materials = new List<ServiceDefinitionMaterial>();
            }

            // ServiceDefinitionMaterial oluştur ve listeye ekle
            var material = new ServiceDefinitionMaterial
            {
                ServiceDefinitionId = this.Id,
                MaterialId = 0, // Daha sonra veritabanına kaydedilen ServiceMaterial ID'si atanmalı
                Material = new ServiceMaterial
                {
                    MaterialName = materialName,
                    MaterialCode = materialCode,
                    UnitPrice = unitPrice,
                    Unit = unit,
                    Quantity = quantity,
                    TotalPrice = unitPrice * quantity,
                    Currency = "TRY",
                    CategoryName = string.Empty,
                    SupplierName = string.Empty,
                    FirmaId = this.FirmaId,
                    SubeId = this.SubeId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = this.CreatedBy,
                    IsActive = true
                },
                Quantity = quantity,
                UnitPrice = unitPrice,
                Currency = "TRY",
                FirmaId = this.FirmaId,
                SubeId = this.SubeId,
                CreatedDate = DateTime.Now,
                CreatedBy = this.CreatedBy,
                IsActive = true
            };

            Materials.Add(material);
            
            // Toplam malzeme maliyetini güncelle
            this.MaterialCost = Materials.Where(m => m.IsActive).Sum(m => m.Quantity * (m.UnitPrice ?? m.Material?.UnitPrice ?? 0));
            
            // Toplam servis maliyetini güncelle
            UpdateTotalCost();
        }
        
        /// <summary>
        /// Toplam servis maliyetini günceller
        /// </summary>
        private void UpdateTotalCost()
        {
            // İşçilik maliyeti + malzeme maliyeti
            decimal laborCost = this.StandardPrice; // İşçilik maliyeti olarak standart fiyatı kullan
            TotalCost = laborCost + MaterialCost;
        }
        
        /// <summary>
        /// Hizmet tanımını aktifleştirir
        /// </summary>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void Activate(int userId)
        {
            this.IsActive = true;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Hizmet tanımını pasifleştirir
        /// </summary>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void Deactivate(int userId)
        {
            this.IsActive = false;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
    }
    
    /// <summary>
    /// Hizmet tanımında yer alan materyal bilgisi
    /// </summary>
    public class ServiceDefinitionMaterial : BaseEntity
    {
        /// <summary>
        /// Hizmet tanımı ID'si
        /// </summary>
        public int ServiceDefinitionId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu hizmet tanımı
        /// </summary>
        public virtual ServiceDefinition ServiceDefinition { get; set; }
        
        /// <summary>
        /// Materyal ID'si
        /// </summary>
        public int MaterialId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu materyal
        /// </summary>
        public virtual ServiceMaterial Material { get; set; }
        
        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Birim fiyat (varsayılan materyal fiyatından farklı olabilir)
        /// </summary>
        public decimal? UnitPrice { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Toplam fiyat
        /// </summary>
        public decimal? TotalPrice => Quantity * (UnitPrice ?? Material?.UnitPrice ?? 0);
        
        /// <summary>
        /// İsteğe bağlı mı
        /// </summary>
        public bool IsOptional { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
        
        public ServiceDefinitionMaterial()
        {
            CreatedDate = DateTime.Now;
            IsOptional = false;
        }
    }
} 