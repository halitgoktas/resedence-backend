using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Teknik servis ve bakım işlemlerinde kullanılan malzemeleri temsil eden sınıf
    /// </summary>
    public class ServiceMaterial : BaseEntity
    {
        /// <summary>
        /// Malzeme ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Bağlı servis talebi ID
        /// </summary>
        public int ServiceRequestId { get; set; }

        /// <summary>
        /// Malzeme adı
        /// </summary>
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>
        /// Malzeme kodu
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// Malzeme açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Malzeme miktarı
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Miktar birimi (adet, kg, metre vb.)
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Toplam fiyat (miktar * birim fiyat)
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Toplam tutar (alternatif olarak kullanılabilecek toplam değer)
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Tedarikçi bilgisi
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// Tedarik tarihi
        /// </summary>
        public DateTime? SupplyDate { get; set; }

        /// <summary>
        /// Eklenen tarih
        /// </summary>
        public DateTime AddedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Ekleyen kullanıcı ID
        /// </summary>
        public int AddedById { get; set; }

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }

        /// <summary>
        /// Malzeme kategorisi ID
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Malzeme kategorisi adı
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;
        
        /// <summary>
        /// Mevcut stok miktarı
        /// </summary>
        public decimal StockQuantity { get; set; }
        
        /// <summary>
        /// Minimum stok seviyesi
        /// </summary>
        public decimal MinimumStockLevel { get; set; }
        
        /// <summary>
        /// Kritik stok seviyesi
        /// </summary>
        public decimal CriticalStockLevel { get; set; }
        
        /// <summary>
        /// Son alım tarihi
        /// </summary>
        public DateTime? LastPurchaseDate { get; set; }
        
        /// <summary>
        /// Son alım fiyatı
        /// </summary>
        public decimal? LastPurchasePrice { get; set; }
        
        /// <summary>
        /// Tedarikçi ID
        /// </summary>
        public int? SupplierId { get; set; }
        
        /// <summary>
        /// Tedarikçi adı
        /// </summary>
        public string SupplierName { get; set; } = string.Empty;
        
        /// <summary>
        /// Malzeme resim URL'si
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Barkod
        /// </summary>
        public string Barcode { get; set; } = string.Empty;
        
        /// <summary>
        /// Üretici
        /// </summary>
        public string Manufacturer { get; set; } = string.Empty;
        
        /// <summary>
        /// Üretici parça numarası
        /// </summary>
        public string ManufacturerPartNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Garanti süresi (ay)
        /// </summary>
        public int? WarrantyPeriod { get; set; }
        
        /// <summary>
        /// Etiketler (virgülle ayrılmış)
        /// </summary>
        public string Tags { get; set; } = string.Empty;
        
        /// <summary>
        /// Depo konumu
        /// </summary>
        public string StorageLocation { get; set; } = string.Empty;
        
        /// <summary>
        /// Depo rafı
        /// </summary>
        public string StorageShelf { get; set; } = string.Empty;
        
        /// <summary>
        /// Satın alma durumu (Aktif, Pasif)
        /// </summary>
        public string PurchaseStatus { get; set; } = string.Empty;
        
        /// <summary>
        /// Malzeme türü (Sarf Malzeme, Demirbaş, Yedek Parça)
        /// </summary>
        public string MaterialType { get; set; } = string.Empty;
        
        /// <summary>
        /// Son sayım tarihi
        /// </summary>
        public DateTime? LastInventoryDate { get; set; }
        
        /// <summary>
        /// Ortalama tedarik süresi (gün)
        /// </summary>
        public int? AverageProcurementTime { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        
        /// <summary>
        /// Teknik özellikler (JSON formatında)
        /// </summary>
        public string TechnicalProperties { get; set; } = string.Empty;
        
        /// <summary>
        /// QR kod URL'si
        /// </summary>
        public string QrCodeUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// KDV oranı
        /// </summary>
        public decimal VatRate { get; set; }
        
        /// <summary>
        /// KDV dahil mi?
        /// </summary>
        public bool IsVatIncluded { get; set; }
        
        /// <summary>
        /// Malzeme durumu (Yeni, İkinci El, Yenilenmiş)
        /// </summary>
        public string Condition { get; set; } = string.Empty;
        
        /// <summary>
        /// Hizmet tanımlarında kullanımı
        /// </summary>
        public virtual ICollection<ServiceDefinitionMaterial> ServiceDefinitions { get; set; } = new List<ServiceDefinitionMaterial>();
        
        /// <summary>
        /// Servis taleplerinde kullanımı
        /// </summary>
        public virtual ICollection<ServiceRequestMaterial> ServiceRequests { get; set; } = new List<ServiceRequestMaterial>();
        
        /// <summary>
        /// Stok hareketleri
        /// </summary>
        public virtual ICollection<MaterialInventoryMovement> InventoryMovements { get; set; } = new List<MaterialInventoryMovement>();
        
        public ServiceMaterial()
        {
            MaterialCode = GenerateMaterialCode();
            Currency = "TRY";
            StockQuantity = 0;
            MinimumStockLevel = 0;
            CriticalStockLevel = 0;
            VatRate = 18; // Varsayılan KDV oranı
            IsVatIncluded = false;
            PurchaseStatus = "Aktif";
            MaterialType = "Sarf Malzeme";
            Condition = "Yeni";
            ServiceDefinitions = new List<ServiceDefinitionMaterial>();
            ServiceRequests = new List<ServiceRequestMaterial>();
            InventoryMovements = new List<MaterialInventoryMovement>();
            CreatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Stok miktarını günceller
        /// </summary>
        /// <param name="quantity">Miktar değişimi (pozitif ekler, negatif azaltır)</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="notes">İşlem notu</param>
        /// <returns>Güncelleme sonrası stok miktarı</returns>
        public decimal UpdateStock(decimal quantity, int userId, string notes = null)
        {
            this.StockQuantity += quantity;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
            
            if (!string.IsNullOrEmpty(notes))
            {
                this.Notes = string.IsNullOrEmpty(this.Notes)
                    ? $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm")} - {notes}"
                    : $"{this.Notes}\n{DateTime.Now.ToString("dd.MM.yyyy HH:mm")} - {notes}";
            }
            
            return this.StockQuantity;
        }
        
        /// <summary>
        /// Satın alma bilgilerini günceller
        /// </summary>
        /// <param name="price">Alım fiyatı</param>
        /// <param name="supplierId">Tedarikçi ID'si</param>
        /// <param name="supplierName">Tedarikçi adı</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void UpdatePurchaseInfo(decimal price, int? supplierId, string supplierName, int userId)
        {
            this.LastPurchaseDate = DateTime.Now;
            this.LastPurchasePrice = price;
            this.SupplierId = supplierId;
            this.SupplierName = supplierName;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Fiyat bilgisini günceller
        /// </summary>
        /// <param name="newPrice">Yeni birim fiyat</param>
        /// <param name="newCurrency">Yeni para birimi</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void UpdatePrice(decimal newPrice, string newCurrency, int userId)
        {
            this.UnitPrice = newPrice;
            this.Currency = newCurrency;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Stok seviyelerini günceller
        /// </summary>
        /// <param name="minimumLevel">Minimum stok seviyesi</param>
        /// <param name="criticalLevel">Kritik stok seviyesi</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void UpdateStockLevels(decimal minimumLevel, decimal criticalLevel, int userId)
        {
            this.MinimumStockLevel = minimumLevel;
            this.CriticalStockLevel = criticalLevel;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Depo konumunu günceller
        /// </summary>
        /// <param name="location">Depo konumu</param>
        /// <param name="shelf">Raf bilgisi</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void UpdateStorageLocation(string location, string shelf, int userId)
        {
            this.StorageLocation = location;
            this.StorageShelf = shelf;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Sayım bilgisini günceller
        /// </summary>
        /// <param name="actualQuantity">Sayılan miktar</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="notes">Sayım notu</param>
        /// <returns>Stok farkı (pozitif: fazla, negatif: eksik)</returns>
        public decimal UpdateInventory(decimal actualQuantity, int userId, string notes = null)
        {
            decimal difference = actualQuantity - this.StockQuantity;
            this.StockQuantity = actualQuantity;
            this.LastInventoryDate = DateTime.Now;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
            
            string inventoryNote = $"Sayım: Önceki {this.StockQuantity - difference}, Sayılan {actualQuantity}, Fark {difference}";
            if (!string.IsNullOrEmpty(notes))
            {
                inventoryNote += $" - Not: {notes}";
            }
            
            this.Notes = string.IsNullOrEmpty(this.Notes)
                ? $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm")} - {inventoryNote}"
                : $"{this.Notes}\n{DateTime.Now.ToString("dd.MM.yyyy HH:mm")} - {inventoryNote}";
            
            return difference;
        }
        
        /// <summary>
        /// Malzeme kodu oluşturur
        /// </summary>
        /// <returns>Malzeme kodu</returns>
        private string GenerateMaterialCode()
        {
            return $"MAT-{DateTime.Now.Year.ToString().Substring(2, 2)}{DateTime.Now.Month:D2}-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
        }
        
        /// <summary>
        /// KDV dahil satış fiyatını hesaplar
        /// </summary>
        /// <returns>KDV dahil satış fiyatı</returns>
        public decimal CalculatePriceWithVAT()
        {
            return UnitPrice + (UnitPrice * VatRate / 100);
        }
        
        /// <summary>
        /// KDV dahil maliyet fiyatını hesaplar
        /// </summary>
        /// <returns>KDV dahil maliyet</returns>
        public decimal CalculateCostWithVAT()
        {
            return LastPurchasePrice ?? 0 + ((LastPurchasePrice ?? 0) * VatRate / 100);
        }
        
        /// <summary>
        /// Kar marjını hesaplar
        /// </summary>
        /// <returns>Kar marjı yüzdesi</returns>
        public decimal CalculateMargin()
        {
            if (LastPurchasePrice == null)
                return 0;
                
            return ((UnitPrice - (LastPurchasePrice ?? 0)) / (LastPurchasePrice ?? 0)) * 100;
        }
        
        /// <summary>
        /// Kritik veya minimum stok seviyesinin altında mı kontrol eder
        /// </summary>
        /// <returns>Stok durumu</returns>
        public string CheckStockStatus()
        {
            if (StockQuantity <= CriticalStockLevel)
            {
                return "Kritik";
            }
            else if (StockQuantity <= MinimumStockLevel)
            {
                return "Düşük";
            }
            else
            {
                return "Normal";
            }
        }
    }
    
    /// <summary>
    /// Malzeme stok hareketlerini takip eden sınıf
    /// </summary>
    public class MaterialInventoryMovement : BaseEntity
    {
        /// <summary>
        /// Malzeme ID'si
        /// </summary>
        public int MaterialId { get; set; }
        
        /// <summary>
        /// İlgili malzeme
        /// </summary>
        public virtual ServiceMaterial Material { get; set; }
        
        /// <summary>
        /// Hareket tarihi
        /// </summary>
        public DateTime MovementDate { get; set; }
        
        /// <summary>
        /// Hareket tipi (Giriş, Çıkış, Ayarlama, Transfer)
        /// </summary>
        public string MovementType { get; set; }
        
        /// <summary>
        /// Hareket miktarı
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Eski stok miktarı
        /// </summary>
        public decimal OldStockQuantity { get; set; }
        
        /// <summary>
        /// Yeni stok miktarı
        /// </summary>
        public decimal NewStockQuantity { get; set; }
        
        /// <summary>
        /// Birim maliyet
        /// </summary>
        public decimal UnitCost { get; set; }
        
        /// <summary>
        /// Birim satış fiyatı
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// İlgili servis talebi ID'si
        /// </summary>
        public int? ServiceRequestId { get; set; }
        
        /// <summary>
        /// İlgili satın alma siparişi ID'si
        /// </summary>
        public int? PurchaseOrderId { get; set; }
        
        /// <summary>
        /// Depo ID'si
        /// </summary>
        public int? WarehouseId { get; set; }
        
        /// <summary>
        /// Kaynak depo ID'si (transfer işlemleri için)
        /// </summary>
        public int? SourceWarehouseId { get; set; }
        
        /// <summary>
        /// Hedef depo ID'si (transfer işlemleri için)
        /// </summary>
        public int? DestinationWarehouseId { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Belge numarası (fatura, irsaliye vb.)
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Onaylandı mı?
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// Onaylayan ID'si
        /// </summary>
        public int? ApprovedBy { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        public MaterialInventoryMovement()
        {
            MovementDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            IsApproved = false;
        }
        
        /// <summary>
        /// Hareketi onayla
        /// </summary>
        /// <param name="approverId">Onaylayan ID'si</param>
        public void Approve(int approverId)
        {
            this.IsApproved = true;
            this.ApprovedBy = approverId;
            this.ApprovalDate = DateTime.Now;
        }
        
        /// <summary>
        /// Toplam maliyeti hesaplar
        /// </summary>
        /// <returns>Toplam maliyet</returns>
        public decimal CalculateTotalCost()
        {
            return UnitCost * Quantity;
        }
        
        /// <summary>
        /// Toplam satış değerini hesaplar
        /// </summary>
        /// <returns>Toplam satış değeri</returns>
        public decimal CalculateTotalPrice()
        {
            return UnitPrice * Quantity;
        }
    }
    
    /// <summary>
    /// Servis taleplerinde kullanılan malzemeleri takip eden sınıf
    /// </summary>
    public class ServiceRequestMaterial : BaseEntity
    {
        /// <summary>
        /// Servis talebi ID'si
        /// </summary>
        public int ServiceRequestId { get; set; }
        
        /// <summary>
        /// İlgili servis talebi
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; } = null!;
        
        /// <summary>
        /// Malzeme ID'si
        /// </summary>
        public int ServiceMaterialId { get; set; }
        
        /// <summary>
        /// İlgili malzeme
        /// </summary>
        public virtual ServiceMaterial ServiceMaterial { get; set; }
        
        /// <summary>
        /// Kullanılan miktar
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Birim maliyet
        /// </summary>
        public decimal UnitCost { get; set; }
        
        /// <summary>
        /// Birim satış fiyatı
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// İndirim oranı (%)
        /// </summary>
        public decimal DiscountRate { get; set; }
        
        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }
        
        /// <summary>
        /// KDV oranı (%)
        /// </summary>
        public decimal VATRate { get; set; }
        
        /// <summary>
        /// Stoktan düşüldü mü?
        /// </summary>
        public bool IsRemovedFromInventory { get; set; }
        
        /// <summary>
        /// Stok hareketi ID'si
        /// </summary>
        public int? InventoryMovementId { get; set; }
        
        /// <summary>
        /// İlgili stok hareketi
        /// </summary>
        public virtual MaterialInventoryMovement InventoryMovement { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Yönetici onayı gerekiyor mu?
        /// </summary>
        public bool RequiresManagerApproval { get; set; }
        
        /// <summary>
        /// Onaylandı mı?
        /// </summary>
        public bool IsApproved { get; set; }
        
        /// <summary>
        /// Onaylayan kullanıcı ID'si
        /// </summary>
        public int? ApprovedBy { get; set; }
        
        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        
        public ServiceRequestMaterial()
        {
            CreatedDate = DateTime.Now;
            Quantity = 1;
            DiscountRate = 0;
            DiscountAmount = 0;
            IsRemovedFromInventory = false;
            RequiresManagerApproval = false;
            IsApproved = false;
        }
        
        /// <summary>
        /// Toplam maliyeti hesaplar
        /// </summary>
        /// <returns>Toplam maliyet</returns>
        public decimal CalculateTotalCost()
        {
            return UnitCost * Quantity;
        }
        
        /// <summary>
        /// İndirim öncesi toplam fiyatı hesaplar
        /// </summary>
        /// <returns>İndirim öncesi toplam fiyat</returns>
        public decimal CalculateSubtotal()
        {
            return UnitPrice * Quantity;
        }
        
        /// <summary>
        /// İndirim tutarını hesaplar
        /// </summary>
        /// <returns>İndirim tutarı</returns>
        public decimal CalculateDiscount()
        {
            if (DiscountAmount > 0)
            {
                return DiscountAmount;
            }
            
            return CalculateSubtotal() * DiscountRate / 100;
        }
        
        /// <summary>
        /// İndirim sonrası toplam fiyatı hesaplar
        /// </summary>
        /// <returns>İndirim sonrası toplam fiyat</returns>
        public decimal CalculateTotalAfterDiscount()
        {
            return CalculateSubtotal() - CalculateDiscount();
        }
        
        /// <summary>
        /// KDV tutarını hesaplar
        /// </summary>
        /// <returns>KDV tutarı</returns>
        public decimal CalculateVAT()
        {
            return CalculateTotalAfterDiscount() * VATRate / 100;
        }
        
        /// <summary>
        /// KDV dahil toplam fiyatı hesaplar
        /// </summary>
        /// <returns>KDV dahil toplam fiyat</returns>
        public decimal CalculateTotal()
        {
            return CalculateTotalAfterDiscount() + CalculateVAT();
        }
        
        /// <summary>
        /// Malzeme kullanımını onayla
        /// </summary>
        /// <param name="approverId">Onaylayan kullanıcı ID'si</param>
        public void Approve(int approverId)
        {
            this.IsApproved = true;
            this.ApprovedBy = approverId;
            this.ApprovalDate = DateTime.Now;
        }
    }
} 