using System;
using System.Collections.Generic;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Services
{
    // MaintenanceTask sınıfı, periyodik bakım görevlerini ve özel bakım işlemlerini temsil eder
    public class MaintenanceTask : BaseEntity
    {
        // Görev kodu
        public string TaskCode { get; private set; }
        
        // Görev adı
        public string TaskName { get; set; }
        
        // Görev açıklaması
        public string Description { get; set; }
        
        // Bakım kategorisi (örn: Elektrik, Mekanik, Sıhhi Tesisat vb.)
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        // Bakım tipi ve durumu
        public MaintenanceTaskType TaskType { get; set; }
        public MaintenanceTaskStatus Status { get; private set; }
        
        // Öncelik seviyesi
        public TaskPriority Priority { get; set; }
        
        // İlişkili ekipmanlar
        public int? EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        
        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public string LocationDetails { get; set; }
        
        // Periyodik bakım ayarları
        public bool IsRecurring { get; set; }
        public RecurrenceType? RecurrenceType { get; set; }
        public int? RecurrenceInterval { get; set; }
        public DateTime? LastPerformedDate { get; set; }
        public DateTime? NextScheduledDate { get; set; }
        
        // Planlama bilgileri
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public TimeSpan? EstimatedDuration { get; set; }
        
        // Gerçekleştirme bilgileri
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public TimeSpan? ActualDuration { get; private set; }
        
        // Atama bilgileri
        public int? AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        
        // Kontrol listeleri (JSON)
        public string ChecklistsJson { get; set; }
        public List<Dictionary<string, object>> Checklists 
        { 
            get => string.IsNullOrEmpty(ChecklistsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(ChecklistsJson); 
            set => ChecklistsJson = JsonSerializer.Serialize(value); 
        }
        
        // Adımlar (JSON)
        public string StepsJson { get; set; }
        public List<Dictionary<string, object>> Steps 
        { 
            get => string.IsNullOrEmpty(StepsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(StepsJson); 
            set => StepsJson = JsonSerializer.Serialize(value); 
        }
        
        // Tamamlanma yüzdesi
        public int CompletionPercentage { get; private set; }
        
        // Güvenlik notları
        public string SafetyNotes { get; set; }
        
        // Gerekli araç ve ekipmanlar
        public string RequiredToolsJson { get; set; }
        public List<string> RequiredTools 
        { 
            get => string.IsNullOrEmpty(RequiredToolsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(RequiredToolsJson); 
            set => RequiredToolsJson = JsonSerializer.Serialize(value); 
        }
        
        // Gerekli yedek parçalar ve malzemeler
        public string RequiredPartsJson { get; set; }
        public List<Dictionary<string, object>> RequiredParts 
        { 
            get => string.IsNullOrEmpty(RequiredPartsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(RequiredPartsJson); 
            set => RequiredPartsJson = JsonSerializer.Serialize(value); 
        }
        
        // Maliyet bilgileri
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; private set; }
        public string Currency { get; set; } = "TRY";
        
        // Kullanılan malzemeler (JSON)
        public string MaterialsUsedJson { get; set; }
        public List<Dictionary<string, object>> MaterialsUsed 
        { 
            get => string.IsNullOrEmpty(MaterialsUsedJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(MaterialsUsedJson); 
            set => MaterialsUsedJson = JsonSerializer.Serialize(value); 
        }
        
        // İlişkili çalışma izinleri (varsa)
        public string WorkPermitNumber { get; set; }
        
        // Talimat ve dokümanlar
        public string DocumentsJson { get; set; }
        public List<string> Documents 
        { 
            get => string.IsNullOrEmpty(DocumentsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(DocumentsJson); 
            set => DocumentsJson = JsonSerializer.Serialize(value); 
        }
        
        // Tespit ve notlar
        public string Findings { get; set; }
        public string Recommendations { get; set; }
        
        // Fotoğraflar
        public string PhotoUrlsJson { get; set; }
        public List<string> PhotoUrls 
        { 
            get => string.IsNullOrEmpty(PhotoUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(PhotoUrlsJson); 
            set => PhotoUrlsJson = JsonSerializer.Serialize(value); 
        }
        
        // Bağlantılı servis talebi (varsa)
        public int? ServiceRequestId { get; set; }
        
        // Onay bilgileri
        public bool RequiresApproval { get; set; }
        public bool IsApproved { get; private set; }
        public int? ApprovedById { get; set; }
        public DateTime? ApprovalDate { get; private set; }
        
        // İç notlar ve aktivite kayıtları
        public string InternalNotes { get; set; }
        
        // İzlenebilirlik için log
        public string ActivityLogJson { get; set; }
        public List<Dictionary<string, object>> ActivityLog 
        { 
            get => string.IsNullOrEmpty(ActivityLogJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(ActivityLogJson); 
            set => ActivityLogJson = JsonSerializer.Serialize(value); 
        }
        
        public MaintenanceTask()
        {
            TaskCode = GenerateTaskCode();
            Status = MaintenanceTaskStatus.Planned;
            CompletionPercentage = 0;
            Currency = "TRY";
            IsRecurring = false;
            RequiresApproval = false;
            IsApproved = false;
        }
        
        // Görev kodu oluşturma
        private string GenerateTaskCode()
        {
            return $"MT-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
        
        // Görev durumunu güncelleme
        public void UpdateStatus(MaintenanceTaskStatus newStatus, int userId, string note = null)
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
            
            // İlgili durum güncellemeleri
            if (newStatus == MaintenanceTaskStatus.InProgress && !ActualStartDate.HasValue)
            {
                ActualStartDate = DateTime.Now;
            }
            else if (newStatus == MaintenanceTaskStatus.Completed && !ActualEndDate.HasValue)
            {
                ActualEndDate = DateTime.Now;
                CompletionPercentage = 100;
                CalculateActualDuration();
                
                if (IsRecurring && RecurrenceType.HasValue && RecurrenceInterval.HasValue)
                {
                    LastPerformedDate = DateTime.Now;
                    ScheduleNextRecurrence();
                }
            }
        }
        
        // Gerçek süreyi hesaplama
        private void CalculateActualDuration()
        {
            if (ActualStartDate.HasValue && ActualEndDate.HasValue)
            {
                ActualDuration = ActualEndDate.Value - ActualStartDate.Value;
            }
        }
        
        // Bir sonraki periyodik bakımı planlama
        private void ScheduleNextRecurrence()
        {
            if (!LastPerformedDate.HasValue || !RecurrenceType.HasValue || !RecurrenceInterval.HasValue)
                return;
                
            switch (RecurrenceType.Value)
            {
                case (RecurrenceType)1: // Daily
                    NextScheduledDate = LastPerformedDate.Value.AddDays(RecurrenceInterval.Value);
                    break;
                case (RecurrenceType)2: // Weekly
                    NextScheduledDate = LastPerformedDate.Value.AddDays(RecurrenceInterval.Value * 7);
                    break;
                case (RecurrenceType)3: // Monthly
                    NextScheduledDate = LastPerformedDate.Value.AddMonths(RecurrenceInterval.Value);
                    break;
                case (RecurrenceType)4: // Quarterly
                    NextScheduledDate = LastPerformedDate.Value.AddMonths(RecurrenceInterval.Value * 3);
                    break;
                case (RecurrenceType)5: // Biannually
                    NextScheduledDate = LastPerformedDate.Value.AddMonths(RecurrenceInterval.Value * 6);
                    break;
                case (RecurrenceType)6: // Annually
                    NextScheduledDate = LastPerformedDate.Value.AddYears(RecurrenceInterval.Value);
                    break;
                default:
                    NextScheduledDate = null;
                    break;
            }
        }
        
        // Tamamlanma yüzdesini güncelleme
        public void UpdateCompletionPercentage(int percentage, int userId, string note = null)
        {
            if (percentage < 0)
                percentage = 0;
            else if (percentage > 100)
                percentage = 100;
                
            var oldPercentage = CompletionPercentage;
            CompletionPercentage = percentage;
            
            // Aktivite logu tutma
            var logEntry = new Dictionary<string, object>
            {
                { "timestamp", DateTime.Now },
                { "action", "CompletionUpdate" },
                { "userId", userId },
                { "oldPercentage", oldPercentage },
                { "newPercentage", percentage },
                { "note", note ?? string.Empty }
            };
            
            var currentLog = ActivityLog ?? new List<Dictionary<string, object>>();
            currentLog.Add(logEntry);
            ActivityLog = currentLog;
            
            // Otomatik durum güncellemeleri
            if (percentage == 100 && Status != MaintenanceTaskStatus.Completed)
            {
                UpdateStatus(MaintenanceTaskStatus.Completed, userId, "Otomatik durum güncellemesi: Tamamlanma %100");
            }
        }
        
        // Malzeme ekleme
        public void AddMaterial(string name, int quantity, decimal unitPrice, string description = null)
        {
            var materials = MaterialsUsed ?? new List<Dictionary<string, object>>();
            
            var materialEntry = new Dictionary<string, object>
            {
                { "name", name },
                { "quantity", quantity },
                { "unitPrice", unitPrice },
                { "totalPrice", quantity * unitPrice },
                { "description", description ?? string.Empty },
                { "addedAt", DateTime.Now }
            };
            
            materials.Add(materialEntry);
            MaterialsUsed = materials;
            
            // Gerçek maliyeti güncelle
            ActualCost += quantity * unitPrice;
        }
        
        // Onay işlemi
        public void SetApproval(bool isApproved, int approvedById)
        {
            IsApproved = isApproved;
            ApprovedById = isApproved ? approvedById : (int?)null;
            ApprovalDate = isApproved ? DateTime.Now : (DateTime?)null;
            
            // Aktivite logu tutma
            var logEntry = new Dictionary<string, object>
            {
                { "timestamp", DateTime.Now },
                { "action", isApproved ? "Approved" : "Rejected" },
                { "userId", approvedById }
            };
            
            var currentLog = ActivityLog ?? new List<Dictionary<string, object>>();
            currentLog.Add(logEntry);
            ActivityLog = currentLog;
        }
        
        // Adım ekleme
        public void AddStep(string stepName, string description, int orderIndex)
        {
            var steps = Steps ?? new List<Dictionary<string, object>>();
            
            var step = new Dictionary<string, object>
            {
                { "stepName", stepName },
                { "description", description },
                { "orderIndex", orderIndex },
                { "isCompleted", false },
                { "completedById", null },
                { "completedDate", null }
            };
            
            steps.Add(step);
            Steps = steps;
        }
        
        // Adım tamamlama
        public void CompleteStep(int stepIndex, int userId, string notes = null)
        {
            var steps = Steps;
            if (steps == null || stepIndex >= steps.Count)
                return;
                
            var step = steps[stepIndex];
            step["isCompleted"] = true;
            step["completedById"] = userId;
            step["completedDate"] = DateTime.Now;
            
            if (!string.IsNullOrEmpty(notes))
            {
                step["notes"] = notes;
            }
            
            steps[stepIndex] = step;
            Steps = steps;
            
            // Tamamlanma yüzdesini güncelle
            int completedSteps = steps.Count(s => (bool)s["isCompleted"]);
            int newPercentage = (int)Math.Round((double)completedSteps / steps.Count * 100);
            UpdateCompletionPercentage(newPercentage, userId, "Adım tamamlama ile otomatik güncelleme");
        }
        
        // Fotoğraf ekleme
        public void AddPhoto(string photoUrl)
        {
            var photos = PhotoUrls ?? new List<string>();
            photos.Add(photoUrl);
            PhotoUrls = photos;
        }
    }
    
    // Bakım görev türleri için enum
    public enum MaintenanceTaskType
    {
        PreventiveMaintenance = 1,
        CorrectiveMaintenance = 2,
        PredictiveMaintenance = 3,
        ConditionBasedMaintenance = 4,
        Emergency = 5,
        Inspection = 6,
        Overhaul = 7,
        Other = 99
    }
    
    // Bakım görev durumları için enum
    public enum MaintenanceTaskStatus
    {
        Planned = 1,
        Scheduled = 2,
        WaitingForParts = 3,
        WaitingForApproval = 4,
        InProgress = 5,
        OnHold = 6,
        Completed = 7,
        Cancelled = 8,
        Deferred = 9
    }
    
    // Öncelik seviyeleri için enum
    public enum TaskPriority
    {
        Low = 1,
        Normal = 2,
        High = 3,
        Critical = 4,
        Emergency = 5
    }
    
    // Periyodik bakım için tekrarlama türleri
    public enum RecurrenceType
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Quarterly = 4,
        Biannually = 5,
        Annually = 6
    }
} 