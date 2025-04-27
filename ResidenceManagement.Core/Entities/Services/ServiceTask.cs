using System;
using System.Collections.Generic;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Services
{
    /// <summary>
    /// Servis görevlerini temsil eden sınıf
    /// </summary>
    public class ServiceTask : BaseEntity
    {
        /// <summary>
        /// Görev kodu
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// Görev adı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Görev açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Servis talebi ID
        /// </summary>
        public int? ServiceRequestId { get; set; }

        /// <summary>
        /// Servis raporu ID
        /// </summary>
        public int? ServiceReportId { get; set; }

        /// <summary>
        /// Görev tipi (1: Kontrol, 2: Bakım, 3: Tamir, 4: Değişim, 5: Temizlik, 6: Kurulum)
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// Görev durumu (1: Bekliyor, 2: Devam Ediyor, 3: Tamamlandı, 4: İptal Edildi, 5: Ertelendi)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// Öncelik seviyesi (1: Düşük, 2: Normal, 3: Yüksek, 4: Acil)
        /// </summary>
        public int PriorityLevel { get; set; } = 2;

        /// <summary>
        /// Planlanan başlangıç tarihi
        /// </summary>
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>
        /// Planlanan bitiş tarihi
        /// </summary>
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>
        /// Fiili başlangıç tarihi
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// Fiili bitiş tarihi
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        public int? EstimatedDuration { get; set; }

        /// <summary>
        /// Fiili süre (dakika)
        /// </summary>
        public int? ActualDuration { get; set; }

        /// <summary>
        /// Atanan teknisyen ID
        /// </summary>
        public int? AssignedTechnicianId { get; set; }

        /// <summary>
        /// Atanan teknisyen adı
        /// </summary>
        public string AssignedTechnicianName { get; set; }

        /// <summary>
        /// Ekipman ID
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Ekipman adı
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// Site ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Daire ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Ortak alan ID
        /// </summary>
        public int? CommonAreaId { get; set; }

        /// <summary>
        /// Lokasyon açıklaması
        /// </summary>
        public string LocationDescription { get; set; }

        /// <summary>
        /// Talimatlar
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Görev notları
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gereken ekipmanlar (JSON formatında)
        /// </summary>
        public string RequiredEquipment { get; set; }

        /// <summary>
        /// Gereken malzemeler (JSON formatında)
        /// </summary>
        public string RequiredMaterials { get; set; }

        /// <summary>
        /// Kullanılan malzemeler (JSON formatında)
        /// </summary>
        public string UsedMaterials { get; set; }

        /// <summary>
        /// Yedek parçalar (JSON formatında)
        /// </summary>
        public string SpareParts { get; set; }

        /// <summary>
        /// Fotoğraf URLleri (JSON formatında)
        /// </summary>
        public string PhotoUrls { get; set; }

        /// <summary>
        /// Bulgular ve gözlemler
        /// </summary>
        public string Findings { get; set; }

        /// <summary>
        /// Yapılan işlemler
        /// </summary>
        public string ActionsPerformed { get; set; }

        /// <summary>
        /// Tavsiyeler
        /// </summary>
        public string Recommendations { get; set; }

        /// <summary>
        /// Kontrol listesi (JSON formatında)
        /// </summary>
        public string Checklist { get; set; }

        /// <summary>
        /// Tamamlanma oranı (%)
        /// </summary>
        public int CompletionPercentage { get; set; } = 0;

        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Fiili maliyet
        /// </summary>
        public decimal? ActualCost { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Faktör görevler (JSON formatında)
        /// </summary>
        public string ChildTasks { get; set; }

        /// <summary>
        /// Ebeveyn görev ID
        /// </summary>
        public int? ParentTaskId { get; set; }

        /// <summary>
        /// Etiketler
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcı ID
        /// </summary>
        public int? CompletedById { get; set; }

        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string CompletedByName { get; set; }

        /// <summary>
        /// Onaylayan kullanıcı ID
        /// </summary>
        public int? ApprovedById { get; set; }

        /// <summary>
        /// Onaylayan kullanıcı adı
        /// </summary>
        public string ApprovedByName { get; set; }

        /// <summary>
        /// Tekrarlama bilgisi (1: Tekrar yok, 2: Günlük, 3: Haftalık, 4: Aylık, 5: Yıllık)
        /// </summary>
        public int RecurrenceType { get; set; } = 1;

        /// <summary>
        /// Tekrarlama aralığı
        /// </summary>
        public int RecurrenceInterval { get; set; } = 0;

        /// <summary>
        /// Aktivite günlüğü (JSON formatında)
        /// </summary>
        public string ActivityLog { get; set; }

        /// <summary>
        /// ServiceTask sınıfının yapıcı metodu
        /// </summary>
        public ServiceTask()
        {
            Status = 1; // Bekliyor
            PriorityLevel = 2; // Normal
            CompletionPercentage = 0;
            Currency = "TRY";
            RecurrenceType = 1; // Tekrar yok
            
            // Aktivite günlüğünü başlat
            RecordActivity("Görev oluşturuldu");
        }

        /// <summary>
        /// Görevi oluşturmak için kullanılan yapıcı metot
        /// </summary>
        public ServiceTask(string title, string description, int taskType, int priorityLevel = 2)
            : this()
        {
            Title = title;
            Description = description;
            TaskType = taskType;
            PriorityLevel = priorityLevel;
            TaskCode = GenerateTaskCode();
        }

        /// <summary>
        /// Görev kodunu otomatik oluşturur
        /// </summary>
        private string GenerateTaskCode()
        {
            string typeCode = "";
            switch (TaskType)
            {
                case 1: typeCode = "CHK"; break; // Kontrol
                case 2: typeCode = "MNT"; break; // Bakım
                case 3: typeCode = "RPR"; break; // Tamir
                case 4: typeCode = "RPL"; break; // Değişim
                case 5: typeCode = "CLN"; break; // Temizlik
                case 6: typeCode = "INS"; break; // Kurulum
                default: typeCode = "TSK"; break; // Genel görev
            }
            
            return $"{typeCode}-{DateTime.Now:yyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        /// <summary>
        /// Görev durumunu günceller
        /// </summary>
        public void UpdateStatus(int newStatus, string notes = null)
        {
            int oldStatus = Status;
            Status = newStatus;

            string statusDesc = GetStatusDescription(newStatus);
            string activityNote = string.IsNullOrEmpty(notes) 
                ? $"Durum değiştirildi: {GetStatusDescription(oldStatus)} -> {statusDesc}" 
                : $"Durum değiştirildi: {GetStatusDescription(oldStatus)} -> {statusDesc}. Not: {notes}";
            
            RecordActivity(activityNote);

            // Durum değişikliğine göre ek işlemler
            switch (newStatus)
            {
                case 2: // Devam Ediyor
                    if (!ActualStartDate.HasValue)
                    {
                        ActualStartDate = DateTime.Now;
                        RecordActivity("Görev başlatıldı");
                    }
                    break;
                    
                case 3: // Tamamlandı
                    ActualEndDate = DateTime.Now;
                    CompletionPercentage = 100;
                    RecordActivity("Görev tamamlandı");
                    
                    // Fiili süreyi hesapla
                    if (ActualStartDate.HasValue)
                    {
                        TimeSpan duration = ActualEndDate.Value - ActualStartDate.Value;
                        ActualDuration = (int)duration.TotalMinutes;
                    }
                    break;
            }
        }

        /// <summary>
        /// Durum açıklamasını döndürür
        /// </summary>
        private string GetStatusDescription(int status)
        {
            switch (status)
            {
                case 1: return "Bekliyor";
                case 2: return "Devam Ediyor";
                case 3: return "Tamamlandı";
                case 4: return "İptal Edildi";
                case 5: return "Ertelendi";
                default: return "Bilinmiyor";
            }
        }

        /// <summary>
        /// Aktivite günlüğüne yeni bir kayıt ekler
        /// </summary>
        public void RecordActivity(string activityDescription)
        {
            var activities = string.IsNullOrEmpty(ActivityLog)
                ? new List<ActivityLogItem>()
                : JsonSerializer.Deserialize<List<ActivityLogItem>>(ActivityLog);

            activities.Add(new ActivityLogItem
            {
                Date = DateTime.Now,
                Description = activityDescription,
                UserId = CreatedBy // Varsayılan olarak işlemi yapan kullanıcı
            });

            ActivityLog = JsonSerializer.Serialize(activities);
        }

        /// <summary>
        /// Kontrol listesine yeni bir madde ekler
        /// </summary>
        public void AddChecklistItem(string itemName, string description = null, bool isRequired = true)
        {
            var checklistItems = string.IsNullOrEmpty(Checklist)
                ? new List<ChecklistItem>()
                : JsonSerializer.Deserialize<List<ChecklistItem>>(Checklist);

            checklistItems.Add(new ChecklistItem
            {
                Name = itemName,
                Description = description,
                IsRequired = isRequired
            });

            Checklist = JsonSerializer.Serialize(checklistItems);
        }

        /// <summary>
        /// Kontrol listesi maddesini günceller
        /// </summary>
        public void UpdateChecklistItem(int index, bool completed, string comment = null)
        {
            if (string.IsNullOrEmpty(Checklist))
                return;

            var checklistItems = JsonSerializer.Deserialize<List<ChecklistItem>>(Checklist);
            
            if (index < 0 || index >= checklistItems.Count)
                return;

            checklistItems[index].IsCompleted = completed;
            checklistItems[index].CompletionDate = completed ? DateTime.Now : (DateTime?)null;
            checklistItems[index].Comment = comment;

            Checklist = JsonSerializer.Serialize(checklistItems);
            
            // Tamamlanma yüzdesini hesapla
            int totalItems = checklistItems.Count;
            int completedItems = checklistItems.Count(i => i.IsCompleted);
            CompletionPercentage = totalItems > 0 ? (completedItems * 100) / totalItems : 0;
            
            RecordActivity($"Kontrol listesi güncellendi. Tamamlanma: %{CompletionPercentage}");
        }

        /// <summary>
        /// Malzeme ekler
        /// </summary>
        public void AddMaterial(string materialName, int quantity, decimal unitPrice, string note = null)
        {
            var materials = string.IsNullOrEmpty(UsedMaterials)
                ? new List<MaterialItem>()
                : JsonSerializer.Deserialize<List<MaterialItem>>(UsedMaterials);

            materials.Add(new MaterialItem
            {
                Name = materialName,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice,
                Note = note
            });

            UsedMaterials = JsonSerializer.Serialize(materials);
            CalculateActualCost();
            
            RecordActivity($"Malzeme eklendi: {materialName} ({quantity} adet)");
        }

        /// <summary>
        /// Fiili maliyeti hesaplar
        /// </summary>
        public void CalculateActualCost()
        {
            decimal totalCost = 0;

            if (!string.IsNullOrEmpty(UsedMaterials))
            {
                var materialList = JsonSerializer.Deserialize<List<MaterialItem>>(UsedMaterials);
                foreach (var material in materialList)
                {
                    totalCost += material.TotalPrice;
                }
            }

            if (!string.IsNullOrEmpty(SpareParts))
            {
                var sparePartsList = JsonSerializer.Deserialize<List<SparePartItem>>(SpareParts);
                foreach (var part in sparePartsList)
                {
                    totalCost += part.TotalPrice;
                }
            }

            // İşçilik maliyeti eklenebilir...

            ActualCost = totalCost;
        }

        /// <summary>
        /// Fotoğraf ekler
        /// </summary>
        public void AddPhoto(string photoUrl, string description = null)
        {
            var photos = string.IsNullOrEmpty(PhotoUrls)
                ? new List<PhotoItem>()
                : JsonSerializer.Deserialize<List<PhotoItem>>(PhotoUrls);

            photos.Add(new PhotoItem
            {
                Url = photoUrl,
                Description = description,
                UploadDate = DateTime.Now
            });

            PhotoUrls = JsonSerializer.Serialize(photos);
            RecordActivity("Fotoğraf eklendi");
        }

        /// <summary>
        /// Alt görev ekler
        /// </summary>
        public void AddChildTask(ServiceTask childTask)
        {
            childTask.ParentTaskId = Id;
            
            // Diğer alt görevlerin ID'lerini sakla
            var childTasks = string.IsNullOrEmpty(ChildTasks)
                ? new List<int>()
                : JsonSerializer.Deserialize<List<int>>(ChildTasks);

            childTasks.Add(childTask.Id);
            ChildTasks = JsonSerializer.Serialize(childTasks);
            
            RecordActivity($"Alt görev eklendi: {childTask.Title}");
        }

        // İç sınıflar
        /// <summary>
        /// Aktivite günlüğü öğesi
        /// </summary>
        private class ActivityLogItem
        {
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public int? UserId { get; set; }
            public string UserName { get; set; }
        }

        /// <summary>
        /// Kontrol listesi öğesi
        /// </summary>
        private class ChecklistItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsRequired { get; set; } = true;
            public bool IsCompleted { get; set; } = false;
            public DateTime? CompletionDate { get; set; }
            public string Comment { get; set; }
        }

        /// <summary>
        /// Malzeme öğesi
        /// </summary>
        private class MaterialItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public string Note { get; set; }
        }

        /// <summary>
        /// Yedek parça öğesi
        /// </summary>
        private class SparePartItem
        {
            public string Name { get; set; }
            public string PartNumber { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
        }

        /// <summary>
        /// Fotoğraf öğesi
        /// </summary>
        private class PhotoItem
        {
            public string Url { get; set; }
            public string Description { get; set; }
            public DateTime UploadDate { get; set; }
        }
    }
} 