using System;
using System.Collections.Generic;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Services
{
    /// <summary>
    /// Servis talebi varlık sınıfı
    /// </summary>
    public class ServiceRequest : BaseEntity
    {
        // Temel bilgiler
        /// <summary>
        /// Servis talep numarası
        /// </summary>
        public string RequestNumber { get; set; }

        /// <summary>
        /// Servis tipi kimliği
        /// </summary>
        public int ServiceTypeId { get; set; }

        /// <summary>
        /// Servis tipi adı
        /// </summary>
        public string ServiceTypeName { get; set; }

        // Talep eden bilgileri
        /// <summary>
        /// Talep eden kullanıcı kimliği
        /// </summary>
        public int? RequestedById { get; set; }

        /// <summary>
        /// Talep eden kullanıcı adı
        /// </summary>
        public string RequestedByName { get; set; }

        /// <summary>
        /// Talep eden telefon
        /// </summary>
        public string RequestedByPhone { get; set; }

        /// <summary>
        /// Talep eden e-posta
        /// </summary>
        public string RequestedByEmail { get; set; }

        // Talep detayları
        /// <summary>
        /// Talep başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Talep açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Talep tarihi
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Öncelik seviyesi (1: Düşük, 2: Normal, 3: Yüksek, 4: Acil)
        /// </summary>
        public int PriorityLevel { get; set; }

        /// <summary>
        /// Talep durumu (1: Yeni, 2: Atandı, 3: İşleniyor, 4: Beklemede, 5: Tamamlandı, 6: İptal Edildi, 7: Reddedildi)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Talep kapatıldı mı?
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Talep otomatik olarak oluşturuldu mu?
        /// </summary>
        public bool IsAutomated { get; set; }

        // Konum bilgileri
        /// <summary>
        /// Site kimliği
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Blok kimliği
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Daire kimliği
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Daire numarası veya konum
        /// </summary>
        public string Location { get; set; }

        // Zamanlama bilgileri
        /// <summary>
        /// Tercih edilen servis tarihi
        /// </summary>
        public DateTime? PreferredServiceDate { get; set; }

        /// <summary>
        /// Randevu tarihi ve saati
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// Tahmini tamamlanma tarihi
        /// </summary>
        public DateTime? EstimatedCompletionDate { get; set; }

        /// <summary>
        /// Başlangıç zamanı
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Bitiş zamanı
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Toplam işlem süresi (dakika)
        /// </summary>
        public int? TotalMinutes { get; set; }

        // Atama bilgileri
        /// <summary>
        /// Atanan teknisyen kimliği
        /// </summary>
        public int? AssignedToId { get; set; }

        /// <summary>
        /// Atanan teknisyen adı
        /// </summary>
        public string AssignedToName { get; set; }

        /// <summary>
        /// Atanan ekip kimliği
        /// </summary>
        public int? AssignedTeamId { get; set; }

        /// <summary>
        /// Atanan ekip adı
        /// </summary>
        public string AssignedTeamName { get; set; }

        /// <summary>
        /// Atama tarihi
        /// </summary>
        public DateTime? AssignmentDate { get; set; }

        // Onay bilgileri
        /// <summary>
        /// Müşteri onay durumu
        /// </summary>
        public bool? CustomerApproved { get; set; }

        /// <summary>
        /// Müşteri onay tarihi
        /// </summary>
        public DateTime? CustomerApprovalDate { get; set; }

        /// <summary>
        /// Müşteri onay notu
        /// </summary>
        public string CustomerApprovalNote { get; set; }

        /// <summary>
        /// Yönetici onay durumu
        /// </summary>
        public bool? ManagerApproved { get; set; }

        /// <summary>
        /// Yönetici onay tarihi
        /// </summary>
        public DateTime? ManagerApprovalDate { get; set; }

        /// <summary>
        /// Yönetici onay notu
        /// </summary>
        public string ManagerApprovalNote { get; set; }

        // Maliyet ve fiyatlandırma bilgileri
        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Gerçek maliyet
        /// </summary>
        public decimal? ActualCost { get; set; }

        /// <summary>
        /// İşçilik maliyeti
        /// </summary>
        public decimal? LaborCost { get; set; }

        /// <summary>
        /// Malzeme maliyeti
        /// </summary>
        public decimal? MaterialCost { get; set; }

        /// <summary>
        /// Diğer maliyetler
        /// </summary>
        public decimal? OtherCosts { get; set; }

        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal? TotalCost { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Ödeme durumu (1: Bekliyor, 2: Kısmi Ödendi, 3: Ödendi, 4: İade Edildi)
        /// </summary>
        public int? PaymentStatus { get; set; }

        // Sonuç bilgileri
        /// <summary>
        /// Servis notu
        /// </summary>
        public string ServiceNote { get; set; }

        /// <summary>
        /// Çözüm açıklaması
        /// </summary>
        public string ResolutionDetails { get; set; }

        /// <summary>
        /// Çözüm tarihi
        /// </summary>
        public DateTime? ResolutionDate { get; set; }

        /// <summary>
        /// Çözüldü mü?
        /// </summary>
        public bool IsResolved { get; set; }

        // Geri bildirim
        /// <summary>
        /// Müşteri memnuniyet puanı (1-5)
        /// </summary>
        public int? CustomerRating { get; set; }

        /// <summary>
        /// Müşteri yorumu
        /// </summary>
        public string CustomerFeedback { get; set; }

        /// <summary>
        /// Geri bildirim tarihi
        /// </summary>
        public DateTime? FeedbackDate { get; set; }

        // Ek bilgiler
        /// <summary>
        /// Dahili notlar (yönetim için)
        /// </summary>
        public string InternalNotes { get; set; }

        /// <summary>
        /// Aktivite logları (JSON formatında)
        /// </summary>
        public string ActivityLog { get; set; }

        /// <summary>
        /// Fotoğraf URL'leri (JSON formatında)
        /// </summary>
        public string PhotoUrls { get; set; }

        /// <summary>
        /// Ek dosya URL'leri (JSON formatında)
        /// </summary>
        public string AttachmentUrls { get; set; }

        /// <summary>
        /// Malzeme listesi (JSON formatında)
        /// </summary>
        public string Materials { get; set; }

        /// <summary>
        /// Görev listesi (JSON formatında)
        /// </summary>
        public string Tasks { get; set; }

        /// <summary>
        /// Etiketler
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Benzer/İlgili servis talepleri
        /// </summary>
        public string RelatedRequestIds { get; set; }

        /// <summary>
        /// Yeni bir servis talebi oluşturur
        /// </summary>
        public ServiceRequest()
        {
            RequestDate = DateTime.Now;
            Status = 1; // Yeni
            PriorityLevel = 2; // Normal
            Currency = "TRY";
            IsClosed = false;
            IsResolved = false;
            IsAutomated = false;
            RequestNumber = GenerateRequestNumber();
        }

        /// <summary>
        /// Servis talep numarası oluşturur
        /// </summary>
        private string GenerateRequestNumber()
        {
            return $"SR-{DateTime.Now:yyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
        }

        /// <summary>
        /// Servis talebinin durumunu günceller
        /// </summary>
        public void UpdateStatus(int newStatus, string note = null)
        {
            Status = newStatus;
            
            var activityList = string.IsNullOrEmpty(ActivityLog) 
                ? new List<ActivityLogItem>() 
                : JsonSerializer.Deserialize<List<ActivityLogItem>>(ActivityLog);
            
            activityList.Add(new ActivityLogItem 
            { 
                Date = DateTime.Now,
                Action = $"Status changed to {newStatus}",
                Note = note,
                UserId = RequestedById
            });
            
            ActivityLog = JsonSerializer.Serialize(activityList);

            if (newStatus == 5) // Tamamlandı
            {
                IsClosed = true;
                ResolutionDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Servis talebini bir teknisyene atar
        /// </summary>
        public void AssignRequest(int technicianId, string technicianName, DateTime assignmentDate)
        {
            AssignedToId = technicianId;
            AssignedToName = technicianName;
            AssignmentDate = assignmentDate;
            Status = 2; // Atandı
            
            var activityList = string.IsNullOrEmpty(ActivityLog) 
                ? new List<ActivityLogItem>() 
                : JsonSerializer.Deserialize<List<ActivityLogItem>>(ActivityLog);
            
            activityList.Add(new ActivityLogItem 
            { 
                Date = DateTime.Now,
                Action = $"Assigned to {technicianName}",
                UserId = RequestedById
            });
            
            ActivityLog = JsonSerializer.Serialize(activityList);
        }

        /// <summary>
        /// Malzeme ekler
        /// </summary>
        public void AddMaterial(string materialName, int quantity, decimal unitPrice)
        {
            var materialList = string.IsNullOrEmpty(Materials) 
                ? new List<MaterialItem>() 
                : JsonSerializer.Deserialize<List<MaterialItem>>(Materials);
            
            materialList.Add(new MaterialItem 
            { 
                Name = materialName,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice
            });
            
            Materials = JsonSerializer.Serialize(materialList);
            CalculateTotalCost();
        }

        /// <summary>
        /// Görev ekler
        /// </summary>
        public void AddTask(string taskName, string description, int estimatedMinutes)
        {
            var taskList = string.IsNullOrEmpty(Tasks) 
                ? new List<TaskItem>() 
                : JsonSerializer.Deserialize<List<TaskItem>>(Tasks);
            
            taskList.Add(new TaskItem 
            { 
                Name = taskName,
                Description = description,
                EstimatedMinutes = estimatedMinutes,
                IsCompleted = false
            });
            
            Tasks = JsonSerializer.Serialize(taskList);
        }

        /// <summary>
        /// Toplam maliyeti hesaplar
        /// </summary>
        public void CalculateTotalCost()
        {
            MaterialCost = 0;
            
            if (!string.IsNullOrEmpty(Materials))
            {
                var materialList = JsonSerializer.Deserialize<List<MaterialItem>>(Materials);
                foreach (var material in materialList)
                {
                    MaterialCost += material.TotalPrice;
                }
            }
            
            TotalCost = (LaborCost ?? 0) + (MaterialCost ?? 0) + (OtherCosts ?? 0);
        }

        /// <summary>
        /// Toplam servis süresini hesaplar
        /// </summary>
        public void CalculateServiceTime()
        {
            if (StartTime.HasValue && EndTime.HasValue)
            {
                TimeSpan duration = EndTime.Value - StartTime.Value;
                TotalMinutes = (int)duration.TotalMinutes;
            }
        }

        // İç sınıflar
        /// <summary>
        /// Aktivite log öğesi
        /// </summary>
        private class ActivityLogItem
        {
            public DateTime Date { get; set; }
            public string Action { get; set; }
            public string Note { get; set; }
            public int? UserId { get; set; }
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
        }

        /// <summary>
        /// Görev öğesi
        /// </summary>
        private class TaskItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int EstimatedMinutes { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime? CompletionDate { get; set; }
        }
    }
} 