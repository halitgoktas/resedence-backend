using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    // Ekipman sınıfı - Site ve binalarda bulunan demirbaş ve ekipmanları takip etmek için
    public class Equipment : BaseEntity
    {
        // Temel Bilgiler
        public string EquipmentCode { get; set; } = string.Empty;    // Ekipman kodu
        public string Name { get; set; } = string.Empty;             // Ekipman adı
        public string Description { get; set; } = string.Empty;      // Ekipman açıklaması
        public string Category { get; set; } = string.Empty;         // Kategori (Mobilya, Elektronik, Teknik, Spor Ekipmanı, vb.)
        public string SerialNumber { get; set; } = string.Empty;     // Seri numarası
        public string Model { get; set; } = string.Empty;            // Model
        public string Manufacturer { get; set; } = string.Empty;     // Üretici
        public string Vendor { get; set; } = string.Empty;           // Tedarikçi
        public string PurchaseOrderNumber { get; set; } = string.Empty; // Satın alma sipariş numarası
        public DateTime AcquisitionDate { get; set; }                // Edinme tarihi
        public decimal PurchasePrice { get; set; }                   // Alım fiyatı
        public decimal CurrentValue { get; set; }                    // Güncel değeri
        public string WarrantyInfo { get; set; } = string.Empty;     // Garanti bilgisi
        public DateTime? WarrantyExpirationDate { get; set; }        // Garanti bitiş tarihi
        
        // Garanti bilgileri
        public DateTime? WarrantyStartDate { get; set; }             // Garanti başlangıç tarihi
        public DateTime? WarrantyEndDate { get; set; }               // Garanti bitiş tarihi
        public string WarrantyProvider { get; set; } = string.Empty; // Garanti sağlayıcı
        public string WarrantyContractNumber { get; set; } = string.Empty; // Garanti sözleşme numarası

        // Durum Bilgileri
        public string Status { get; set; } = string.Empty;           // Durum (Aktif, Bakımda, Arızalı, Hurdaya Ayrılmış)
        public DateTime? LastMaintenanceDate { get; set; }           // Son bakım tarihi
        public DateTime? NextMaintenanceDate { get; set; }           // Sonraki planlanan bakım tarihi

        // Konum Bilgileri
        public int? ResidenceId { get; set; }                        // Bağlı olduğu site ID'si
        public Residence Residence { get; set; }                     // Bağlı olduğu site
        public int? BlockId { get; set; }                            // Bağlı olduğu blok ID'si
        public Block Block { get; set; }                             // Bağlı olduğu blok
        public int? ApartmentId { get; set; }                        // Bağlı olduğu daire ID'si
        public Apartment Apartment { get; set; }                     // Bağlı olduğu daire
        public string Location { get; set; } = string.Empty;         // Detaylı konum bilgisi (ör. "Lobi", "Havuz Alanı", "Kat 2 Koridor")

        // Atama Bilgileri
        public string AssignedTo { get; set; } = string.Empty;       // Sorumlu kişi/departman
        public int? AssignedToUserId { get; set; }                   // Atanan kullanıcı ID'si
        public string AssignedToUserName { get; set; } = string.Empty; // Atanan kullanıcı adı

        // Barkod ve Takip
        public string Barcode { get; set; } = string.Empty;          // Barkod
        public string QRCode { get; set; } = string.Empty;           // QR Kod
        public string AssetTag { get; set; } = string.Empty;         // Demirbaş etiketi
        public string Notes { get; set; } = string.Empty;            // Notlar

        // Bağlantılı Kayıtlar
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>(); // Bakım kayıtları
        public ICollection<EquipmentDocument> Documents { get; set; } = new List<EquipmentDocument>();          // İlgili dokümanlar
        public ICollection<EquipmentPart> Parts { get; set; } = new List<EquipmentPart>();                      // Bileşen parçalar
        public ICollection<EquipmentMetric> Metrics { get; set; } = new List<EquipmentMetric>();                // Ölçüm metrikleri
        public ICollection<EquipmentPhoto> Photos { get; set; } = new List<EquipmentPhoto>();                   // Fotoğraflar
        public ICollection<EquipmentLog> ActivityLogs { get; set; } = new List<EquipmentLog>();                 // Aktivite geçmişi
        public ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; } = new List<MaintenanceSchedule>(); // Bakım planları

        // Yapılandırılmış Metotlar
        public void UpdateStatus(string newStatus, string reason = null, string changedBy = null)
        {
            string oldStatus = Status;
            Status = newStatus;
            UpdatedDate = DateTime.Now;
            UpdatedBy = changedBy != null ? int.Parse(changedBy) : UpdatedBy;

            // Aktivite logu ekle
            ActivityLogs.Add(new EquipmentLog
            {
                ActivityType = "StatusChange",
                Description = $"Durum değiştirildi: {oldStatus} -> {newStatus}",
                Details = reason,
                PerformedBy = changedBy ?? UpdatedBy?.ToString() ?? string.Empty,
                ActivityDate = DateTime.Now,
                CreatedBy = changedBy != null ? int.Parse(changedBy) : UpdatedBy,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            });
        }

        // AddActivityLog metodu eklendi
        private void AddActivityLog(string activityType, string description, string details = null)
        {
            ActivityLogs.Add(new EquipmentLog
            {
                ActivityType = activityType,
                Description = description,
                Details = details,
                PerformedBy = UpdatedBy?.ToString() ?? string.Empty,
                ActivityDate = DateTime.Now,
                CreatedBy = UpdatedBy,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            });
        }

        public void Relocate(int? newResidenceId, int? newBlockId, int? newApartmentId, string newLocation)
        {
            // Eski konum bilgileri için null değer kontrolü
            var oldLocation = $"Rezidans: {(ResidenceId.HasValue ? ResidenceId.ToString() : "Belirtilmemiş")}, " +
                             $"Blok: {(BlockId.HasValue ? BlockId.ToString() : "Belirtilmemiş")}, " +
                             $"Daire: {(ApartmentId.HasValue ? ApartmentId.ToString() : "Belirtilmemiş")}, " +
                             $"Konum: {Location ?? "Belirtilmemiş"}";

            // Yeni konum değerlerini atama
            ResidenceId = newResidenceId;
            BlockId = newBlockId;
            ApartmentId = newApartmentId;
            Location = newLocation;

            // İşlem logu için açıklama oluşturma
            var description = $"Ekipman taşındı. Önceki konum: {oldLocation}. " +
                              $"Yeni konum: Rezidans: {(newResidenceId.HasValue ? newResidenceId.ToString() : "Belirtilmemiş")}, " +
                              $"Blok: {(newBlockId.HasValue ? newBlockId.ToString() : "Belirtilmemiş")}, " +
                              $"Daire: {(newApartmentId.HasValue ? newApartmentId.ToString() : "Belirtilmemiş")}, " +
                              $"Konum: {newLocation ?? "Belirtilmemiş"}";

            // Aktivite kaydı ekleme
            AddActivityLog("Ekipman Taşıma", description);
        }

        public MaintenanceRecord AddMaintenanceRecord(string maintenanceType, string description, DateTime maintenanceDate, string performedBy, decimal cost = 0, string notes = null)
        {
            int? performedById = null;
            if (!string.IsNullOrEmpty(performedBy) && int.TryParse(performedBy, out int id))
            {
                performedById = id;
            }

            var record = new MaintenanceRecord
            {
                MaintenanceType = maintenanceType,
                Description = description,
                MaintenanceDate = maintenanceDate,
                PerformedBy = performedBy,
                Cost = cost,
                Notes = notes,
                Status = "Tamamlandı",
                CompletionDate = DateTime.Now,
                EquipmentId = Id,
                CreatedBy = performedById,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            };

            MaintenanceRecords.Add(record);
            LastMaintenanceDate = maintenanceDate;
            
            // Aktivite logu ekle
            ActivityLogs.Add(new EquipmentLog
            {
                ActivityType = "Maintenance",
                Description = $"{maintenanceType} bakımı eklendi",
                Details = description,
                PerformedBy = performedBy,
                ActivityDate = DateTime.Now,
                CreatedBy = performedById,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            });

            return record;
        }

        public MaintenanceRecord ScheduleMaintenance(string maintenanceType, DateTime scheduledDate, string assignedTo = null, string description = null)
        {
            var record = new MaintenanceRecord
            {
                MaintenanceType = maintenanceType,
                Description = description,
                MaintenanceDate = scheduledDate,
                PerformedBy = assignedTo,
                Status = "Planlandı",
                EquipmentId = Id,
                CreatedBy = UpdatedBy,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            };

            MaintenanceRecords.Add(record);
            NextMaintenanceDate = scheduledDate;
            
            // Aktivite logu ekle
            ActivityLogs.Add(new EquipmentLog
            {
                ActivityType = "MaintenanceScheduled",
                Description = $"{maintenanceType} bakımı planlandı: {scheduledDate.ToShortDateString()}",
                Details = description,
                PerformedBy = UpdatedBy?.ToString() ?? string.Empty,
                ActivityDate = DateTime.Now,
                CreatedBy = UpdatedBy,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            });

            return record;
        }

        public void UpdateValue(decimal newValue, string reason = null, string performedBy = null)
        {
            int? performedById = null;
            if (!string.IsNullOrEmpty(performedBy) && int.TryParse(performedBy, out int id))
            {
                performedById = id;
            }

            decimal oldValue = CurrentValue;
            CurrentValue = newValue;
            UpdatedDate = DateTime.Now;
            UpdatedBy = performedById ?? UpdatedBy;

            // Aktivite logu ekle
            ActivityLogs.Add(new EquipmentLog
            {
                ActivityType = "ValueUpdate",
                Description = $"Değer güncellendi: {oldValue} -> {newValue}",
                Details = reason,
                PerformedBy = performedBy ?? UpdatedBy?.ToString() ?? string.Empty,
                ActivityDate = DateTime.Now,
                CreatedBy = performedById ?? UpdatedBy,
                CreatedDate = DateTime.Now,
                FirmaId = FirmaId,
                SubeId = SubeId,
                IsActive = true
            });
        }

        public void SetWarranty(DateTime startDate, DateTime endDate, string provider, string contractNumber)
        {
            // Mevcut garanti bilgilerini kaydetme
            var oldWarranty = $"Başlangıç: {WarrantyStartDate}, Bitiş: {WarrantyEndDate}, " +
                             $"Sağlayıcı: {WarrantyProvider ?? "Belirtilmemiş"}, " +
                             $"Sözleşme No: {WarrantyContractNumber ?? "Belirtilmemiş"}";

            // Yeni garanti değerlerini atama
            WarrantyStartDate = startDate;
            WarrantyEndDate = endDate;
            WarrantyProvider = provider;
            WarrantyContractNumber = contractNumber;

            // İşlem logu için açıklama oluşturma
            var description = $"Garanti bilgileri güncellendi. Önceki bilgiler: {oldWarranty}. " +
                             $"Yeni bilgiler: Başlangıç: {startDate}, Bitiş: {endDate}, " +
                             $"Sağlayıcı: {provider ?? "Belirtilmemiş"}, " +
                             $"Sözleşme No: {contractNumber ?? "Belirtilmemiş"}";

            // Aktivite kaydı ekleme
            AddActivityLog("Garanti Bilgilerini Güncelleme", description);
        }

        public void AssignToUser(int? userId, string userName)
        {
            // Mevcut atanan kullanıcı bilgilerini kaydetme
            var oldAssignment = $"Kullanıcı ID: {(AssignedToUserId.HasValue ? AssignedToUserId.ToString() : "Atanmamış")}, " +
                               $"Kullanıcı Adı: {AssignedToUserName ?? "Belirtilmemiş"}";

            // Yeni kullanıcı değerlerini atama
            AssignedToUserId = userId;
            AssignedToUserName = userName;

            // İşlem logu için açıklama oluşturma
            var description = $"Ekipman kullanıcıya atandı. Önceki atama: {oldAssignment}. " +
                             $"Yeni atama: Kullanıcı ID: {(userId.HasValue ? userId.ToString() : "Atanmamış")}, " +
                             $"Kullanıcı Adı: {userName ?? "Belirtilmemiş"}";

            // Aktivite kaydı ekleme
            AddActivityLog("Kullanıcıya Atama", description);
        }

        public Dictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            
            // Temel özellikleri ekleme
            properties.Add("EquipmentCode", EquipmentCode ?? string.Empty);
            properties.Add("Name", Name ?? string.Empty);
            properties.Add("Description", Description ?? string.Empty);
            
            // ... existing code ...

            return properties;
        }
    }

    // Bakım Kaydı - Ekipmanlar için bakım geçmişi
    public class MaintenanceRecord : BaseEntity
    {
        public int EquipmentId { get; set; }                // İlişkili ekipman ID
        public Equipment Equipment { get; set; }            // İlişkili ekipman
        public string MaintenanceType { get; set; }         // Bakım tipi (Rutin, Koruyucu, Onarım, vb.)
        public string Description { get; set; }             // Bakım açıklaması
        public DateTime MaintenanceDate { get; set; }       // Bakım tarihi
        public DateTime? CompletionDate { get; set; }       // Tamamlanma tarihi
        public string Status { get; set; }                  // Durum (Planlandı, DevamEdiyor, Tamamlandı, İptalEdildi)
        public string PerformedBy { get; set; }             // Bakımı yapan kişi/firma
        public decimal Cost { get; set; }                   // Maliyet
        public string Notes { get; set; }                   // Notlar
        public ICollection<MaintenancePart> PartsReplaced { get; set; } = new List<MaintenancePart>(); // Değiştirilen parçalar
    }

    // Bakım Parçası - Bakım sırasında değiştirilen parçalar
    public class MaintenancePart : BaseEntity
    {
        public int MaintenanceRecordId { get; set; }     // İlişkili bakım kaydı ID
        public MaintenanceRecord MaintenanceRecord { get; set; } // İlişkili bakım kaydı
        public int? EquipmentPartId { get; set; }        // İlişkili ekipman parçası ID (opsiyonel)
        public string Name { get; set; }                 // Parça adı
        public string PartNumber { get; set; }           // Parça numarası
        public decimal Cost { get; set; }                // Maliyet
        public int Quantity { get; set; }                // Miktar
        public string Notes { get; set; }                // Notlar
    }

    // Ekipman Parçası - Ekipmanı oluşturan parçalar
    public class EquipmentPart : BaseEntity
    {
        public int EquipmentId { get; set; }             // İlişkili ekipman ID
        public Equipment Equipment { get; set; }         // İlişkili ekipman
        public string Name { get; set; }                 // Parça adı
        public string PartNumber { get; set; }           // Parça numarası
        public string Description { get; set; }          // Açıklama
        public string Manufacturer { get; set; }         // Üretici
        public string Vendor { get; set; }               // Tedarikçi
        public decimal Cost { get; set; }                // Maliyet
        public DateTime InstallationDate { get; set; }   // Kurulum tarihi
        public DateTime? ReplacementDate { get; set; }   // Değiştirilme tarihi
        public string Notes { get; set; }                // Notlar
    }

    // Ekipman Belgesi - Ekipmanla ilişkili belgeler
    public class EquipmentDocument : BaseEntity
    {
        public int EquipmentId { get; set; }             // İlişkili ekipman ID
        public Equipment Equipment { get; set; }         // İlişkili ekipman
        public string Name { get; set; }                 // Belge adı
        public string Description { get; set; }          // Açıklama
        public string DocumentType { get; set; }         // Belge tipi (Kullanım Kılavuzu, Garanti Belgesi, Sertifika, vb.)
        public string FilePath { get; set; }             // Dosya yolu
        public DateTime UploadDate { get; set; }         // Yükleme tarihi
        public string UploadedBy { get; set; }           // Yükleyen kişi
    }

    // Ekipman Metrik - Ekipmanla ilgili ölçüm ve metrikler
    public class EquipmentMetric : BaseEntity
    {
        public int EquipmentId { get; set; }             // İlişkili ekipman ID
        public Equipment Equipment { get; set; }         // İlişkili ekipman
        public string MetricName { get; set; }           // Metrik adı (ör. "Çalışma Saati", "Sıcaklık", "Basınç")
        public string MetricValue { get; set; }          // Metrik değeri
        public string Unit { get; set; }                 // Birim (ör. "Saat", "°C", "Bar")
        public DateTime MeasurementDate { get; set; }    // Ölçüm tarihi
        public string MeasuredBy { get; set; }           // Ölçümü yapan kişi
        public string Notes { get; set; }                // Notlar
    }

    // Ekipman Fotoğrafı - Ekipmana ait fotoğraflar
    public class EquipmentPhoto : BaseEntity
    {
        public int EquipmentId { get; set; }             // İlişkili ekipman ID
        public Equipment Equipment { get; set; }         // İlişkili ekipman
        public string Title { get; set; }                // Başlık
        public string Description { get; set; }          // Açıklama
        public string FilePath { get; set; }             // Dosya yolu
        public DateTime UploadDate { get; set; }         // Yükleme tarihi
        public string UploadedBy { get; set; }           // Yükleyen kişi
    }

    // Ekipman Aktivite Logu - Ekipman üzerinde yapılan tüm aktivitelerin kaydı
    public class EquipmentLog : BaseEntity
    {
        public int EquipmentId { get; set; }             // İlişkili ekipman ID
        public Equipment Equipment { get; set; }         // İlişkili ekipman
        public string ActivityType { get; set; }         // Aktivite tipi (StatusChange, Relocation, Maintenance, vb.)
        public string Description { get; set; }          // Açıklama
        public DateTime ActivityDate { get; set; }       // Aktivite tarihi
        public string PerformedBy { get; set; }          // İşlemi yapan kişi
        public string Details { get; set; }              // Detaylar
    }
} 