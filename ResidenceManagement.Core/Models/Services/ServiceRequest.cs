using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.DTOs;

namespace ResidenceManagement.Core.Models.Services
{
    // Servis taleplerini yönetmek için kullanılan sınıf
    public class ServiceRequest : BaseEntity
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Temel Bilgiler
        public string RequestNumber { get; set; } = string.Empty;
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PriorityLevel { get; set; } = "Normal";
        public string Status { get; set; } = "Yeni";
        public string? CurrencyCode { get; set; } = "TRY";
        
        // Müşteri/Talep Eden Bilgileri
        public int RequestedById { get; set; }
        public string RequestedByName { get; set; } = string.Empty;
        public string? RequestedByPhone { get; set; }
        public string? RequestedByEmail { get; set; }
        public string? RequestedByRole { get; set; }
        
        // Lokasyon Bilgileri
        public int? ResidenceId { get; set; }
        public string? ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string? BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string? ApartmentNumber { get; set; }
        public int? FloorId { get; set; }
        public string? FloorName { get; set; }
        public string? LocationType { get; set; }
        public string? LocationDetail { get; set; }
        
        // Atama Bilgileri
        public int? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }
        public string? AssignedToDepartment { get; set; }
        public string? AssignedBy { get; set; }
        public DateTime? AssignmentDate { get; set; }
        
        // Zamanlama Bilgileri
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? EstimatedCompletionDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? TotalDuration { get; set; } // Dakika cinsinden
        
        // Finansal Bilgiler
        public bool IsChargeable { get; set; }
        public string? PricingType { get; set; } // Sabit, Saatlik, Malzeme+İşçilik
        public decimal? FixedPrice { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? TotalMaterialCost { get; set; }
        public decimal? TotalLaborCost { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsBilled { get; set; }
        public DateTime? BillingDate { get; set; }
        public string? InvoiceNumber { get; set; }
        
        // Diğer Bilgiler
        public bool IsAutomated { get; set; }
        public bool IsScheduled { get; set; }
        public bool RequiresApproval { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedById { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ActivityLog { get; set; }
        
        // İlişkili Nesneler
        public virtual ICollection<ServiceMaterial> Materials { get; set; } = new List<ServiceMaterial>();
        public virtual ICollection<ServiceTask> Tasks { get; set; } = new List<ServiceTask>();
        public virtual ICollection<ServiceAttachment> Attachments { get; set; } = new List<ServiceAttachment>();
        public virtual ICollection<ServiceComment> Comments { get; set; } = new List<ServiceComment>();
        
        // Süreç ve takip
        public int? WorkflowId { get; set; }
        public string? CurrentWorkflowStep { get; set; } = string.Empty;
        public string? ChecklistProgress { get; set; } = string.Empty;  // JSON formatında kontrol listesi ilerlemesi
        public bool HasPendingActions { get; set; }
        public string? PendingActions { get; set; } = string.Empty;  // JSON formatında bekleyen aksiyonlar
        
        // Anlaşma ve termin
        public int? ScheduleId { get; set; }
        public bool IsRecurring { get; set; }
        public string? RecurrencePattern { get; set; } = string.Empty;  // JSON formatında tekrarlama bilgisi
        
        // Malzeme ve envanter
        public string? UsedMaterials { get; set; } = string.Empty;  // JSON formatında kullanılan malzemeler
        public bool RequiresInventory { get; set; }
        public string? InventoryItemsUsed { get; set; } = string.Empty;  // JSON formatında kullanılan envanter
        
        // Dış servis entegrasyonu
        public bool IsExternalService { get; set; }
        public int? ExternalServiceProviderId { get; set; }
        public string? ExternalServiceProviderName { get; set; } = string.Empty;
        public string? ExternalReferenceNumber { get; set; } = string.Empty;
        public decimal? ExternalCost { get; set; }
        
        // Değerlendirme ekle
        public int? Rating { get; set; }
        public string? FeedbackComments { get; set; }
        public DateTime? FeedbackDate { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // İlişkili servis malzemeleri
        public List<ServiceMaterial> MaterialsList { get; set; } = new List<ServiceMaterial>();
        
        // İlişkili servis görevleri
        public List<ServiceTask> TasksList { get; set; } = new List<ServiceTask>();
        
        // Yapıcı metot
        public ServiceRequest()
        {
            RequestDate = DateTime.Now;
            Status = "Yeni";
            PriorityLevel = "Normal";
            CurrencyCode = "TRY";
            IsActive = true;
            IsAutomated = false;
            CreatedDate = DateTime.Now;
            MaterialsList = new List<ServiceMaterial>();
            TasksList = new List<ServiceTask>();
            Materials = new List<ServiceMaterial>();
            Tasks = new List<ServiceTask>();
            Attachments = new List<ServiceAttachment>();
            Comments = new List<ServiceComment>();
        }
        
        // Durum özellikleri
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        // Durumu güncelle
        public void UpdateStatus(string newStatus, string updatedBy, string notes = null)
        {
            Status = newStatus;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
            
            // Duruma göre diğer alanları güncelle
            switch (newStatus)
            {
                case "Atandı":
                    AssignmentDate = DateTime.Now;
                    break;
                case "İşlemde":
                    ActualStartDate = DateTime.Now;
                    break;
                case "Tamamlandı":
                    ActualEndDate = DateTime.Now;
                    if (ActualStartDate.HasValue)
                    {
                        TotalDuration = (int)((DateTime)ActualEndDate - (DateTime)ActualStartDate).TotalMinutes;
                    }
                    break;
            }
            
            // Aktivite günlüğüne ekle
            AddToActivityLog(updatedBy, $"Durum {newStatus} olarak güncellendi", notes);
        }
        
        // Atama yap
        public void AssignTo(int userId, string userName, string assignedBy, string department = null)
        {
            AssignedToUserId = userId;
            AssignedToUserName = userName;
            AssignedToDepartment = department;
            AssignedBy = assignedBy;
            AssignmentDate = DateTime.Now;
            
            if (Status == "Yeni")
            {
                Status = "Atandı";
            }
            
            UpdatedBy = assignedBy;
            UpdatedDate = DateTime.Now;
            
            // Aktivite günlüğüne ekle
            AddToActivityLog(assignedBy, $"Talep {userName} kişisine atandı", null);
        }
        
        // Aktivite günlüğüne ekle
        public void AddToActivityLog(string actorName, string action, string notes = null)
        {
            // Gerçek uygulamada bu metot ActivityLog JSON dizisine yeni bir kayıt ekler
            // Burada sadece konsept olarak yer almaktadır
            
            // Örnek bir JSON kaydı:
            // {
            //   "timestamp": "2023-06-15T10:30:00",
            //   "actor": "Ahmet Yılmaz",
            //   "action": "Durum Tamamlandı olarak güncellendi",
            //   "notes": "Tüm işlemler sorunsuz tamamlandı."
            // }
        }
        
        // Malzeme ekle
        public void AddMaterial(ServiceMaterial material)
        {
            MaterialsList.Add(material);
            
            // Malzeme maliyetini toplam maliyete ekle
            TotalMaterialCost = (TotalMaterialCost ?? 0) + material.UnitPrice * material.Quantity;
            UpdateTotalCost();
        }
        
        // Görev ekle
        public void AddTask(ServiceTask task)
        {
            TasksList.Add(task);
        }
        
        // Toplam maliyeti güncelle
        public void UpdateTotalCost()
        {
            // Doğrudan toplam maliyeti hesapla
            TotalAmount = (TotalMaterialCost ?? 0) + (TotalLaborCost ?? 0) + (TotalTax ?? 0);
        }
        
        // Değerlendirme ekle
        public void AddFeedback(int rating, string comments, string submittedBy)
        {
            if (rating >= 1 && rating <= 5)
            {
                Rating = rating;
                FeedbackComments = comments;
                FeedbackDate = DateTime.Now;
                
                // Aktivite günlüğüne ekle
                AddToActivityLog(submittedBy, $"Değerlendirme eklendi: {rating}/5", comments);
            }
        }
        
        // Talep numarası oluştur
        public void GenerateRequestNumber()
        {
            if (string.IsNullOrEmpty(RequestNumber))
            {
                // Tarih-sıra numarası formatında talep numarası oluştur
                string datePrefix = DateTime.Now.ToString("yyyyMMdd");
                string random = new Random().Next(1000, 9999).ToString();
                RequestNumber = $"SR-{datePrefix}-{random}";
            }
        }
        
        // Ek belge URL'si ekle
        public void AddAttachment(string fileUrl, string fileName, string fileType, string uploadedBy)
        {
            // Gerçek uygulamada bu metot Attachments JSON dizisine yeni bir kayıt ekler
            // Burada sadece konsept olarak yer almaktadır
            
            AddToActivityLog(uploadedBy, $"{fileName} dosyası eklendi", null);
        }
        
        // Tahmini maliyeti hesapla
        public void CalculateEstimatedCost()
        {
            // Servis tipine göre maliyet hesaplanır
            // Örnek hesaplama:
            if (PricingType == "Sabit")
            {
                // Sabit bir fiyat
                FixedPrice = 100;
            }
            else if (PricingType == "Saatlik" && TotalDuration.HasValue)
            {
                // Saatlik bir fiyatlandırma (örneğin 50 TL/saat)
                FixedPrice = (TotalDuration.Value / 60.0m) * 50;
            }
            
            // Bu sadece basit bir örnek, gerçek uygulamada fiyatlandırma daha karmaşık olabilir
        }
    }
    
    /// <summary>
    /// Servis görevi sınıfı
    /// </summary>
    public class ServiceTask
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? CompletedBy { get; set; }
        public int EstimatedMinutes { get; set; }
        public int ActualMinutes { get; set; }
        public string Status { get; set; } = "Beklemede";
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        
        // İlişkili servis talebi
        public virtual ServiceRequest ServiceRequest { get; set; }
    }

    /// <summary>
    /// Servis ek dosyası sınıfı
    /// </summary>
    public class ServiceAttachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// Servis yorumu sınıfı
    /// </summary>
    public class ServiceComment
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentedBy { get; set; } = string.Empty;
        public string CommentedByRole { get; set; } = string.Empty;
    }
}