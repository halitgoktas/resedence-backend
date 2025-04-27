using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Services;

namespace ResidenceManagement.Core.Entities.Inventory
{
    /// <summary>
    /// Rezidans ve sitelerde kullanılan ekipman ve demirbaşların envanterini temsil eden sınıf
    /// </summary>
    public class EquipmentInventory : BaseEntity
    {
        // Temel bilgiler
        /// <summary>
        /// Demirbaş/Ekipman kodu
        /// </summary>
        public string InventoryCode { get; set; }
        
        /// <summary>
        /// Demirbaş/Ekipman adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Demirbaş/Ekipman açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Kategori (Elektronik, Mekanik, Mobilya, vb.)
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Alt kategori
        /// </summary>
        public string SubCategory { get; set; }
        
        /// <summary>
        /// Ekipman tipi (Asansör, Klima, Jeneratör, vb.)
        /// </summary>
        public string EquipmentType { get; set; }
        
        /// <summary>
        /// Marka
        /// </summary>
        public string Brand { get; set; }
        
        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// Seri numarası
        /// </summary>
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// Barkod numarası
        /// </summary>
        public string BarCode { get; set; }
        
        /// <summary>
        /// Ekipman durumu (Aktif, Pasif, Bakımda, Arızalı, vb.)
        /// </summary>
        public string Status { get; set; } = "Aktif";
        
        // Lokasyon bilgileri
        /// <summary>
        /// Ekipmanın bulunduğu rezidans ID'si
        /// </summary>
        public int? ResidenceId { get; set; }
        
        /// <summary>
        /// Ekipmanın bulunduğu rezidans
        /// </summary>
        [ForeignKey("ResidenceId")]
        public virtual Residence Residence { get; set; }
        
        /// <summary>
        /// Ekipmanın bulunduğu blok ID'si
        /// </summary>
        public int? BlockId { get; set; }
        
        /// <summary>
        /// Ekipmanın bulunduğu blok
        /// </summary>
        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; }
        
        /// <summary>
        /// Ekipmanın bulunduğu daire ID'si
        /// </summary>
        public int? ApartmentId { get; set; }
        
        /// <summary>
        /// Ekipmanın bulunduğu daire
        /// </summary>
        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }
        
        /// <summary>
        /// Bulunduğu odanın adı/numarası
        /// </summary>
        public string RoomName { get; set; }
        
        /// <summary>
        /// Detaylı konum bilgisi
        /// </summary>
        public string LocationDetails { get; set; }
        
        // Finansal bilgiler
        /// <summary>
        /// Satın alma tarihi
        /// </summary>
        public DateTime? PurchaseDate { get; set; }
        
        /// <summary>
        /// Satın alma fiyatı
        /// </summary>
        public decimal? PurchasePrice { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";
        
        /// <summary>
        /// Tahmini değer
        /// </summary>
        public decimal? EstimatedValue { get; set; }
        
        /// <summary>
        /// Değerleme tarihi
        /// </summary>
        public DateTime? ValuationDate { get; set; }
        
        /// <summary>
        /// Satıcı/Tedarikçi
        /// </summary>
        public string Vendor { get; set; }
        
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// Amortisman süresi (ay)
        /// </summary>
        public int? DepreciationPeriodMonths { get; set; }
        
        /// <summary>
        /// Amortisman yöntemi
        /// </summary>
        public string DepreciationMethod { get; set; }
        
        /// <summary>
        /// Maliyet merkezi kodu
        /// </summary>
        public string CostCenter { get; set; }
        
        /// <summary>
        /// Bütçe kodu
        /// </summary>
        public string BudgetCode { get; set; }
        
        // Teknik bilgiler
        /// <summary>
        /// Üretim tarihi
        /// </summary>
        public DateTime? ManufacturingDate { get; set; }
        
        /// <summary>
        /// Çalışma süresi (saat)
        /// </summary>
        public decimal? OperatingHours { get; set; }
        
        /// <summary>
        /// Kurulum tarihi
        /// </summary>
        public DateTime? InstallationDate { get; set; }
        
        /// <summary>
        /// Elektrik tüketimi (kW)
        /// </summary>
        public decimal? PowerConsumption { get; set; }
        
        /// <summary>
        /// Voltaj (V)
        /// </summary>
        public string Voltage { get; set; }
        
        /// <summary>
        /// Ampere değeri (A)
        /// </summary>
        public string Ampere { get; set; }
        
        /// <summary>
        /// Frekans (Hz)
        /// </summary>
        public string Frequency { get; set; }
        
        /// <summary>
        /// IP adresi
        /// </summary>
        public string IPAddress { get; set; }
        
        /// <summary>
        /// MAC adresi
        /// </summary>
        public string MACAddress { get; set; }
        
        /// <summary>
        /// Firmware/Yazılım versiyonu
        /// </summary>
        public string FirmwareVersion { get; set; }
        
        /// <summary>
        /// Boyutlar (cm)
        /// </summary>
        public string Dimensions { get; set; }
        
        /// <summary>
        /// Ağırlık (kg)
        /// </summary>
        public decimal? Weight { get; set; }
        
        /// <summary>
        /// Teknik özellikler (JSON olarak saklanır)
        /// </summary>
        public string TechnicalSpecifications { get; set; }
        
        // Garanti ve sözleşme bilgileri
        /// <summary>
        /// Garanti başlangıç tarihi
        /// </summary>
        public DateTime? WarrantyStartDate { get; set; }
        
        /// <summary>
        /// Garanti bitiş tarihi
        /// </summary>
        public DateTime? WarrantyEndDate { get; set; }
        
        /// <summary>
        /// Garanti süresinin ay cinsinden değeri
        /// </summary>
        public int? WarrantyDurationMonths { get; set; }
        
        /// <summary>
        /// Garanti belge numarası
        /// </summary>
        public string WarrantyDocumentNumber { get; set; }
        
        /// <summary>
        /// Bakım anlaşması var mı?
        /// </summary>
        public bool HasMaintenanceContract { get; set; } = false;
        
        /// <summary>
        /// Bakım anlaşması başlangıç tarihi
        /// </summary>
        public DateTime? MaintenanceContractStartDate { get; set; }
        
        /// <summary>
        /// Bakım anlaşması bitiş tarihi
        /// </summary>
        public DateTime? MaintenanceContractEndDate { get; set; }
        
        /// <summary>
        /// Bakım anlaşması numarası
        /// </summary>
        public string MaintenanceContractNumber { get; set; }
        
        /// <summary>
        /// Bakım anlaşması detayları
        /// </summary>
        public string MaintenanceContractDetails { get; set; }
        
        /// <summary>
        /// Bakım anlaşması sağlayıcısı
        /// </summary>
        public string MaintenanceProvider { get; set; }
        
        /// <summary>
        /// Servis sıklığı (her X ay)
        /// </summary>
        public int? ServiceFrequencyMonths { get; set; }
        
        // Bakım bilgileri
        /// <summary>
        /// Son bakım tarihi
        /// </summary>
        public DateTime? LastMaintenanceDate { get; set; }
        
        /// <summary>
        /// Bir sonraki bakım tarihi
        /// </summary>
        public DateTime? NextMaintenanceDate { get; set; }
        
        /// <summary>
        /// Son bakımı yapan
        /// </summary>
        public string LastMaintenanceBy { get; set; }
        
        /// <summary>
        /// Bakım talimatları (JSON olarak saklanır)
        /// </summary>
        public string MaintenanceInstructions { get; set; }
        
        /// <summary>
        /// Toplam bakım sayısı
        /// </summary>
        public int MaintenanceCount { get; set; } = 0;
        
        /// <summary>
        /// Son bakım maliyeti
        /// </summary>
        public decimal? LastMaintenanceCost { get; set; }
        
        /// <summary>
        /// Toplam bakım maliyeti
        /// </summary>
        public decimal? TotalMaintenanceCost { get; set; } = 0;
        
        // Arıza bilgileri
        /// <summary>
        /// Son arıza tarihi
        /// </summary>
        public DateTime? LastFailureDate { get; set; }
        
        /// <summary>
        /// Son arıza açıklaması
        /// </summary>
        public string LastFailureDescription { get; set; }
        
        /// <summary>
        /// Toplam arıza sayısı
        /// </summary>
        public int FailureCount { get; set; } = 0;
        
        /// <summary>
        /// Son arıza onarım maliyeti
        /// </summary>
        public decimal? LastRepairCost { get; set; }
        
        /// <summary>
        /// Toplam onarım maliyeti
        /// </summary>
        public decimal? TotalRepairCost { get; set; } = 0;
        
        // Belge ve dosya bilgileri
        /// <summary>
        /// Kullanım kılavuzu dosya yolu
        /// </summary>
        public string ManualFilePath { get; set; }
        
        /// <summary>
        /// Resim dosya yolları (JSON dizisi olarak saklanır)
        /// </summary>
        public string ImageUrls { get; set; }
        
        /// <summary>
        /// Ekli belge dosyaları (JSON dizisi olarak saklanır)
        /// </summary>
        public string AttachmentUrls { get; set; }
        
        /// <summary>
        /// QR kod URL'si
        /// </summary>
        public string QRCodeUrl { get; set; }
        
        // İlişkili koleksiyonlar
        /// <summary>
        /// Bu ekipmana bağlı bakım planları
        /// </summary>
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; } = new List<MaintenanceSchedule>();
        
        /// <summary>
        /// Bu ekipmana bağlı teknik servis istekleri
        /// </summary>
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        
        /// <summary>
        /// Bu ekipmana bağlı yedek parçalar
        /// </summary>
        public virtual ICollection<SparePart> SpareParts { get; set; } = new List<SparePart>();
        
        /// <summary>
        /// Teknik özellikler sözlüğünü alır
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> TechnicalSpecificationsDictionary
        {
            get => string.IsNullOrEmpty(TechnicalSpecifications) 
                ? new Dictionary<string, string>() 
                : JsonConvert.DeserializeObject<Dictionary<string, string>>(TechnicalSpecifications);
            set => TechnicalSpecifications = JsonConvert.SerializeObject(value);
        }
        
        /// <summary>
        /// Bakım talimatları listesini alır
        /// </summary>
        [NotMapped]
        public List<string> MaintenanceInstructionsList
        {
            get => string.IsNullOrEmpty(MaintenanceInstructions) 
                ? new List<string>() 
                : JsonConvert.DeserializeObject<List<string>>(MaintenanceInstructions);
            set => MaintenanceInstructions = JsonConvert.SerializeObject(value);
        }
        
        /// <summary>
        /// Resim URL'leri listesini alır
        /// </summary>
        [NotMapped]
        public List<string> ImageUrlsList
        {
            get => string.IsNullOrEmpty(ImageUrls) 
                ? new List<string>() 
                : JsonConvert.DeserializeObject<List<string>>(ImageUrls);
            set => ImageUrls = JsonConvert.SerializeObject(value);
        }
        
        /// <summary>
        /// Ekli belge URL'leri listesini alır
        /// </summary>
        [NotMapped]
        public List<string> AttachmentUrlsList
        {
            get => string.IsNullOrEmpty(AttachmentUrls) 
                ? new List<string>() 
                : JsonConvert.DeserializeObject<List<string>>(AttachmentUrls);
            set => AttachmentUrls = JsonConvert.SerializeObject(value);
        }
        
        /// <summary>
        /// Bir sonraki bakım tarihini hesaplar
        /// </summary>
        public void CalculateNextMaintenanceDate()
        {
            if (LastMaintenanceDate.HasValue && ServiceFrequencyMonths.HasValue && ServiceFrequencyMonths.Value > 0)
            {
                NextMaintenanceDate = LastMaintenanceDate.Value.AddMonths(ServiceFrequencyMonths.Value);
            }
        }
        
        /// <summary>
        /// Bakım kaydı ekler
        /// </summary>
        /// <param name="maintenanceDate">Bakım tarihi</param>
        /// <param name="maintenanceBy">Bakımı yapan</param>
        /// <param name="cost">Bakım maliyeti</param>
        /// <param name="notes">Notlar</param>
        public void AddMaintenanceRecord(DateTime maintenanceDate, string maintenanceBy, decimal cost, string notes = null)
        {
            LastMaintenanceDate = maintenanceDate;
            LastMaintenanceBy = maintenanceBy;
            LastMaintenanceCost = cost;
            MaintenanceCount++;
            
            if (TotalMaintenanceCost.HasValue)
            {
                TotalMaintenanceCost += cost;
            }
            else
            {
                TotalMaintenanceCost = cost;
            }
            
            CalculateNextMaintenanceDate();
        }
        
        /// <summary>
        /// Arıza kaydı ekler
        /// </summary>
        /// <param name="failureDate">Arıza tarihi</param>
        /// <param name="description">Arıza açıklaması</param>
        /// <param name="repairCost">Onarım maliyeti</param>
        public void AddFailureRecord(DateTime failureDate, string description, decimal repairCost)
        {
            LastFailureDate = failureDate;
            LastFailureDescription = description;
            LastRepairCost = repairCost;
            FailureCount++;
            
            if (TotalRepairCost.HasValue)
            {
                TotalRepairCost += repairCost;
            }
            else
            {
                TotalRepairCost = repairCost;
            }
        }
        
        /// <summary>
        /// Ekipman durumunu günceller
        /// </summary>
        /// <param name="newStatus">Yeni durum</param>
        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
        
        /// <summary>
        /// Garanti durumunu kontrol eder
        /// </summary>
        /// <returns>Garanti aktif mi?</returns>
        public bool IsUnderWarranty()
        {
            if (!WarrantyEndDate.HasValue) return false;
            return DateTime.Now <= WarrantyEndDate.Value;
        }
        
        /// <summary>
        /// Bakım anlaşması durumunu kontrol eder
        /// </summary>
        /// <returns>Bakım anlaşması aktif mi?</returns>
        public bool HasActiveMaintenanceContract()
        {
            if (!HasMaintenanceContract || !MaintenanceContractEndDate.HasValue) return false;
            return DateTime.Now <= MaintenanceContractEndDate.Value;
        }
        
        /// <summary>
        /// Bakım gerekiyor mu?
        /// </summary>
        /// <returns>Bakım gerekiyor mu?</returns>
        public bool NeedsMaintenance()
        {
            if (!NextMaintenanceDate.HasValue) return false;
            return DateTime.Now >= NextMaintenanceDate.Value;
        }
        
        /// <summary>
        /// Envanter kodunu oluşturur
        /// </summary>
        /// <param name="categoryCode">Kategori kodu</param>
        /// <param name="sequenceNumber">Sıra numarası</param>
        public void GenerateInventoryCode(string categoryCode, int sequenceNumber)
        {
            InventoryCode = $"{categoryCode}-{sequenceNumber.ToString("D6")}";
        }
    }
    
    /// <summary>
    /// Yedek parçaları temsil eden sınıf
    /// </summary>
    public class SparePart : BaseEntity
    {
        /// <summary>
        /// Yedek parça kodu
        /// </summary>
        public string PartCode { get; set; }
        
        /// <summary>
        /// Yedek parça adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Yedek parça açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// İlişkili ekipman ID'si
        /// </summary>
        public int? EquipmentInventoryId { get; set; }
        
        /// <summary>
        /// İlişkili ekipman
        /// </summary>
        [ForeignKey("EquipmentInventoryId")]
        public virtual EquipmentInventory EquipmentInventory { get; set; }
        
        /// <summary>
        /// Kategori
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Marka
        /// </summary>
        public string Brand { get; set; }
        
        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// Stok miktarı
        /// </summary>
        public int StockQuantity { get; set; } = 0;
        
        /// <summary>
        /// Minimum stok seviyesi
        /// </summary>
        public int MinimumStockLevel { get; set; } = 1;
        
        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";
        
        /// <summary>
        /// Tedarikçi
        /// </summary>
        public string Vendor { get; set; }
        
        /// <summary>
        /// Son satın alma tarihi
        /// </summary>
        public DateTime? LastPurchaseDate { get; set; }
        
        /// <summary>
        /// Son kullanma tarihi
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        
        /// <summary>
        /// Konum
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Resim URL'si
        /// </summary>
        public string ImageUrl { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Stok ekle
        /// </summary>
        /// <param name="quantity">Eklenecek miktar</param>
        /// <param name="purchaseDate">Satın alma tarihi</param>
        /// <param name="unitPrice">Birim fiyat</param>
        public void AddStock(int quantity, DateTime? purchaseDate = null, decimal? unitPrice = null)
        {
            StockQuantity += quantity;
            
            if (purchaseDate.HasValue)
            {
                LastPurchaseDate = purchaseDate;
            }
            
            if (unitPrice.HasValue)
            {
                UnitPrice = unitPrice.Value;
            }
        }
        
        /// <summary>
        /// Stok çıkar
        /// </summary>
        /// <param name="quantity">Çıkarılacak miktar</param>
        /// <returns>İşlem başarılı mı?</returns>
        public bool RemoveStock(int quantity)
        {
            if (StockQuantity < quantity)
            {
                return false;
            }
            
            StockQuantity -= quantity;
            return true;
        }
        
        /// <summary>
        /// Stok yeterli mi?
        /// </summary>
        /// <returns>Stok yeterli mi?</returns>
        public bool IsStockSufficient()
        {
            return StockQuantity >= MinimumStockLevel;
        }
    }
} 