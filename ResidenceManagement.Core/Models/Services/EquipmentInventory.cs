using System;
using System.Collections.Generic;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Services
{
    // EquipmentInventory sınıfı, binalardaki ekipman ve demirbaşların takibini yapar
    public class EquipmentInventory : BaseEntity
    {
        // Ekipman kodu ve barkodu
        public string EquipmentCode { get; private set; }
        public string Barcode { get; set; }
        
        // Ekipman adı ve açıklaması
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Kategori bilgileri
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        // Tipi ve durumu
        public EquipmentType Type { get; set; }
        public EquipmentStatus Status { get; set; }
        
        // Marka ve model bilgileri
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        
        // Temel özellikler (JSON formatında)
        public string SpecificationsJson { get; set; }
        public Dictionary<string, object> Specifications 
        { 
            get => string.IsNullOrEmpty(SpecificationsJson) ? new Dictionary<string, object>() : JsonSerializer.Deserialize<Dictionary<string, object>>(SpecificationsJson); 
            set => SpecificationsJson = JsonSerializer.Serialize(value); 
        }
        
        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public string LocationDetails { get; set; }
        
        // Satın alma bilgileri
        public DateTime? PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; } = "TRY";
        public string SupplierName { get; set; }
        public string SupplierContact { get; set; }
        public string PurchaseOrderNumber { get; set; }
        
        // Garanti bilgileri
        public bool HasWarranty { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string WarrantyDetails { get; set; }
        
        // Yaşam döngüsü bilgileri
        public DateTime? InstallationDate { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public DateTime? EndOfLifeDate { get; set; }
        
        // Bakım periyodu
        public bool HasMaintenanceSchedule { get; set; }
        public RecurrenceType? MaintenanceRecurrenceType { get; set; }
        public int? MaintenanceRecurrenceInterval { get; set; }
        
        // Kritiklik seviyesi
        public CriticalityLevel CriticalityLevel { get; set; }
        
        // Maliyet bilgileri
        public decimal? ReplacementCost { get; set; }
        public decimal? TotalMaintenanceCost { get; private set; }
        public decimal? AnnualOperatingCost { get; set; }
        
        // Envanter bilgileri
        public decimal? CurrentValue { get; set; }
        public string AssetTag { get; set; }
        public int? Quantity { get; set; }
        public string Unit { get; set; }
        
        // Dokümantasyon
        public string DocumentsJson { get; set; }
        public List<Dictionary<string, object>> Documents 
        { 
            get => string.IsNullOrEmpty(DocumentsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(DocumentsJson); 
            set => DocumentsJson = JsonSerializer.Serialize(value); 
        }
        
        // Parça, yedek parça ve sarf malzeme bilgileri
        public string PartsJson { get; set; }
        public List<Dictionary<string, object>> Parts 
        { 
            get => string.IsNullOrEmpty(PartsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(PartsJson); 
            set => PartsJson = JsonSerializer.Serialize(value); 
        }
        
        // Bakım geçmişi
        public string MaintenanceHistoryJson { get; set; }
        public List<Dictionary<string, object>> MaintenanceHistory 
        { 
            get => string.IsNullOrEmpty(MaintenanceHistoryJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(MaintenanceHistoryJson); 
            set => MaintenanceHistoryJson = JsonSerializer.Serialize(value); 
        }
        
        // Fotoğraflar
        public string PhotoUrlsJson { get; set; }
        public List<string> PhotoUrls 
        { 
            get => string.IsNullOrEmpty(PhotoUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(PhotoUrlsJson); 
            set => PhotoUrlsJson = JsonSerializer.Serialize(value); 
        }
        
        // Servis sağlayıcı bilgileri
        public string ServiceProviderName { get; set; }
        public string ServiceProviderContact { get; set; }
        
        // Metrik ve ölçüm verisi
        public string OperationalMetricsJson { get; set; }
        public Dictionary<string, object> OperationalMetrics 
        { 
            get => string.IsNullOrEmpty(OperationalMetricsJson) ? new Dictionary<string, object>() : JsonSerializer.Deserialize<Dictionary<string, object>>(OperationalMetricsJson); 
            set => OperationalMetricsJson = JsonSerializer.Serialize(value); 
        }
        
        // Demirbaş sahibi/sorumlusu
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        
        // Risk değerlendirmesi
        public string RiskAssessment { get; set; }
        
        // İç notlar
        public string InternalNotes { get; set; }
        
        // İzlenebilirlik için log
        public string ActivityLogJson { get; set; }
        public List<Dictionary<string, object>> ActivityLog 
        { 
            get => string.IsNullOrEmpty(ActivityLogJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(ActivityLogJson); 
            set => ActivityLogJson = JsonSerializer.Serialize(value); 
        }
        
        public EquipmentInventory()
        {
            EquipmentCode = GenerateEquipmentCode();
            Status = EquipmentStatus.Active;
            Currency = "TRY";
            CriticalityLevel = CriticalityLevel.Medium;
            TotalMaintenanceCost = 0;
            Quantity = 1;
        }
        
        // Ekipman kodu oluşturma
        private string GenerateEquipmentCode()
        {
            return $"EQ-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
        
        // Demirbaş durumunu güncelleme
        public void UpdateStatus(EquipmentStatus newStatus, int userId, string note = null)
        {
            var oldStatus = Status;
            Status = newStatus;
            
            // Aktivite logu tutma
            var logEntry = new Dictionary<string, object>
            {
                { "timestamp", DateTime.Now },
                { "action", "StatusChange" },
                { "userId", userId },
                { "oldStatus", oldStatus },
                { "newStatus", newStatus },
                { "note", note ?? string.Empty }
            };
            
            var currentLog = ActivityLog ?? new List<Dictionary<string, object>>();
            currentLog.Add(logEntry);
            ActivityLog = currentLog;
        }
        
        // Bakım kaydı ekleme
        public void AddMaintenanceRecord(string maintenanceType, string description, decimal cost, int performedById, DateTime performedDate, string outcome = null)
        {
            var history = MaintenanceHistory ?? new List<Dictionary<string, object>>();
            
            var record = new Dictionary<string, object>
            {
                { "maintenanceType", maintenanceType },
                { "description", description },
                { "cost", cost },
                { "performedById", performedById },
                { "performedDate", performedDate },
                { "outcome", outcome ?? string.Empty },
                { "recordedAt", DateTime.Now }
            };
            
            history.Add(record);
            MaintenanceHistory = history;
            
            // Toplam bakım maliyetini güncelle
            TotalMaintenanceCost = (TotalMaintenanceCost ?? 0) + cost;
            
            // Son bakım tarihini güncelle
            LastMaintenanceDate = performedDate;
            
            // Bir sonraki bakım tarihini hesapla (eğer bakım planı varsa)
            if (HasMaintenanceSchedule && MaintenanceRecurrenceType.HasValue && MaintenanceRecurrenceInterval.HasValue)
            {
                NextMaintenanceDate = CalculateNextMaintenanceDate(performedDate);
            }
        }
        
        // Bir sonraki bakım tarihini hesaplama
        private DateTime? CalculateNextMaintenanceDate(DateTime lastDate)
        {
            if (!MaintenanceRecurrenceType.HasValue || !MaintenanceRecurrenceInterval.HasValue)
                return null;
                
            switch (MaintenanceRecurrenceType.Value)
            {
                case RecurrenceType.Daily:
                    return lastDate.AddDays(MaintenanceRecurrenceInterval.Value);
                case RecurrenceType.Weekly:
                    return lastDate.AddDays(MaintenanceRecurrenceInterval.Value * 7);
                case RecurrenceType.Monthly:
                    return lastDate.AddMonths(MaintenanceRecurrenceInterval.Value);
                case RecurrenceType.Quarterly:
                    return lastDate.AddMonths(MaintenanceRecurrenceInterval.Value * 3);
                case RecurrenceType.Biannually:
                    return lastDate.AddMonths(MaintenanceRecurrenceInterval.Value * 6);
                case RecurrenceType.Annually:
                    return lastDate.AddYears(MaintenanceRecurrenceInterval.Value);
                default:
                    return null;
            }
        }
        
        // Demirbaşı taşıma/yer değiştirme
        public void Relocate(int? newResidenceId, string newResidenceName, int? newBlockId, string newBlockName, 
                            int? newApartmentId, string newApartmentNumber, string newLocationDetails, int userId, string note = null)
        {
            var oldLocation = new Dictionary<string, object>
            {
                { "residenceId", ResidenceId },
                { "blockId", BlockId },
                { "apartmentId", ApartmentId },
                { "locationDetails", LocationDetails }
            };
            
            // Lokasyon bilgilerini güncelle
            ResidenceId = newResidenceId;
            ResidenceName = newResidenceName;
            BlockId = newBlockId;
            BlockName = newBlockName;
            ApartmentId = newApartmentId;
            ApartmentNumber = newApartmentNumber;
            LocationDetails = newLocationDetails;
            
            // Aktivite logu tutma
            var logEntry = new Dictionary<string, object>
            {
                { "timestamp", DateTime.Now },
                { "action", "Relocation" },
                { "userId", userId },
                { "oldLocation", oldLocation },
                { "newLocation", new Dictionary<string, object>
                    {
                        { "residenceId", ResidenceId },
                        { "blockId", BlockId },
                        { "apartmentId", ApartmentId },
                        { "locationDetails", LocationDetails }
                    }
                },
                { "note", note ?? string.Empty }
            };
            
            var currentLog = ActivityLog ?? new List<Dictionary<string, object>>();
            currentLog.Add(logEntry);
            ActivityLog = currentLog;
        }
        
        // Doküman ekleme
        public void AddDocument(string documentType, string title, string documentUrl, string description = null)
        {
            var documents = Documents ?? new List<Dictionary<string, object>>();
            
            var document = new Dictionary<string, object>
            {
                { "documentType", documentType },
                { "title", title },
                { "documentUrl", documentUrl },
                { "description", description ?? string.Empty },
                { "uploadedAt", DateTime.Now }
            };
            
            documents.Add(document);
            Documents = documents;
        }
        
        // Yedek parça ekleme
        public void AddPart(string partName, string partNumber, int quantity, decimal unitPrice, string description = null)
        {
            var parts = Parts ?? new List<Dictionary<string, object>>();
            
            var part = new Dictionary<string, object>
            {
                { "partName", partName },
                { "partNumber", partNumber },
                { "quantity", quantity },
                { "unitPrice", unitPrice },
                { "totalPrice", quantity * unitPrice },
                { "description", description ?? string.Empty },
                { "addedAt", DateTime.Now }
            };
            
            parts.Add(part);
            Parts = parts;
        }
        
        // Fotoğraf ekleme
        public void AddPhoto(string photoUrl)
        {
            var photos = PhotoUrls ?? new List<string>();
            photos.Add(photoUrl);
            PhotoUrls = photos;
        }
        
        // Metrik değeri güncelleme/ekleme
        public void UpdateMetric(string metricName, object metricValue, DateTime? readingDate = null)
        {
            var metrics = OperationalMetrics ?? new Dictionary<string, object>();
            
            var metricData = new Dictionary<string, object>
            {
                { "value", metricValue },
                { "lastUpdated", readingDate ?? DateTime.Now },
                { "history", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "value", metricValue },
                            { "timestamp", readingDate ?? DateTime.Now }
                        }
                    }
                }
            };
            
            // Eğer metrik zaten varsa, tarihçesini güncelle
            if (metrics.ContainsKey(metricName))
            {
                var existingMetric = JsonSerializer.Deserialize<Dictionary<string, object>>(
                    JsonSerializer.Serialize(metrics[metricName]));
                
                var history = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(
                    JsonSerializer.Serialize(existingMetric["history"]));
                
                history.Add(new Dictionary<string, object>
                {
                    { "value", metricValue },
                    { "timestamp", readingDate ?? DateTime.Now }
                });
                
                metricData["history"] = history;
            }
            
            metrics[metricName] = metricData;
            OperationalMetrics = metrics;
        }
    }
    
    // Ekipman türleri için enum
    public enum EquipmentType
    {
        HVAC = 1,
        Electrical = 2,
        Plumbing = 3,
        Fire = 4,
        Security = 5,
        Elevator = 6,
        Generator = 7,
        Pool = 8,
        Gym = 9,
        CommonArea = 10,
        Kitchen = 11,
        IT = 12,
        Other = 99
    }
    
    // Ekipman durumları için enum
    public enum EquipmentStatus
    {
        Active = 1,
        InMaintenance = 2,
        OutOfOrder = 3,
        Reserved = 4,
        Retired = 5,
        Disposed = 6,
        Lost = 7,
        Stolen = 8
    }
    
    // Kritiklik seviyeleri
    public enum CriticalityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
} 