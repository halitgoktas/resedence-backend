using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Enums;
using ResidenceManagement.Core.Models.Genel;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Periyodik bakım planını temsil eden sınıf
    /// </summary>
    public class MaintenancePlan : BaseEntity
    {
        /// <summary>
        /// Plan ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Plan kodu
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Plan adı
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Plan açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Bakım frekansı (Günlük, Haftalık, Aylık, 3 Aylık, 6 Aylık, Yıllık)
        /// </summary>
        public string Frequency { get; set; } = string.Empty;

        /// <summary>
        /// Frekans aralığı (sayısal değer)
        /// </summary>
        public int FrequencyInterval { get; set; }

        /// <summary>
        /// Frekans birimi (Gün, Hafta, Ay, Yıl)
        /// </summary>
        public string FrequencyUnit { get; set; } = string.Empty;

        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Son bakım tarihi
        /// </summary>
        public DateTime? LastMaintenanceDate { get; set; }

        /// <summary>
        /// Bir sonraki bakım tarihi
        /// </summary>
        public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>
        /// Plan durumu (Aktif, Tamamlandı, İptal Edildi)
        /// </summary>
        public string Status { get; set; } = "Aktif";

        /// <summary>
        /// Bakım önceliği (Düşük, Normal, Yüksek, Acil)
        /// </summary>
        public string Priority { get; set; } = "Normal";

        /// <summary>
        /// Tahmini bakım süresi (saat)
        /// </summary>
        public decimal? EstimatedDuration { get; set; }

        /// <summary>
        /// Bakım tipi (Önleyici, Düzeltici, Kestirimci)
        /// </summary>
        public string MaintenanceType { get; set; } = string.Empty;

        /// <summary>
        /// Bakım kategorisi
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// İlgili ekipman ID
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// İlgili ekipman adı
        /// </summary>
        public string? EquipmentName { get; set; }

        /// <summary>
        /// Bakım sözleşme numarası
        /// </summary>
        public string MaintenanceContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Bakım sağlayıcı firma
        /// </summary>
        public string MaintenanceProvider { get; set; } = string.Empty;

        /// <summary>
        /// Son bakımı yapan
        /// </summary>
        public string LastMaintenanceBy { get; set; } = string.Empty;

        /// <summary>
        /// Bakım maliyeti
        /// </summary>
        public decimal? MaintenanceCost { get; set; }

        /// <summary>
        /// Bakım talimatları
        /// </summary>
        public string Instructions { get; set; } = string.Empty;

        /// <summary>
        /// Gerekli araç ve ekipmanlar
        /// </summary>
        public string RequiredTools { get; set; } = string.Empty;

        /// <summary>
        /// Güvenlik önlemleri
        /// </summary>
        public string SafetyMeasures { get; set; } = string.Empty;

        /// <summary>
        /// İlgili dokümantasyon
        /// </summary>
        public string? Documentation { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı ID
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Son güncelleyen kullanıcı ID
        /// </summary>
        public int? LastUpdatedById { get; set; }

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// Bakım kontrol listesi
        /// </summary>
        public string ChecklistItems { get; set; } = string.Empty;

        /// <summary>
        /// Bakım hatırlatıcı gün sayısı (Bakım tarihinden kaç gün önce hatırlatma yapılacak)
        /// </summary>
        public int? ReminderDays { get; set; }

        /// <summary>
        /// İlgili bakım görevleri
        /// </summary>
        public virtual ICollection<MaintenanceTask> Tasks { get; set; } = new List<MaintenanceTask>();

        /// <summary>
        /// Bakım geçmişi
        /// </summary>
        public virtual ICollection<MaintenanceHistory> History { get; set; } = new List<MaintenanceHistory>();

        /// <summary>
        /// Bakımda kullanılacak malzemeler
        /// </summary>
        public virtual ICollection<MaintenanceMaterial> Materials { get; set; } = new List<MaintenanceMaterial>();

        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }

        /// <summary>
        /// Tesis/Bina ID
        /// </summary>
        public int? ResidenceId { get; set; }

        /// <summary>
        /// Tesis/Bina adı
        /// </summary>
        public string? ResidenceName { get; set; }

        /// <summary>
        /// Blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Blok adı
        /// </summary>
        public string? BlockName { get; set; }

        /// <summary>
        /// Daire/Birim ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// Daire/Birim adı
        /// </summary>
        public string? ApartmentName { get; set; }

        /// <summary>
        /// Ortak alan ID
        /// </summary>
        public int? CommonAreaId { get; set; }

        /// <summary>
        /// Ortak alan adı
        /// </summary>
        public string? CommonAreaName { get; set; }
        
        /// <summary>
        /// Bir sonraki bakım tarihini hesaplar
        /// </summary>
        /// <param name="lastMaintenanceDate">Son bakım tarihi</param>
        /// <returns>Bir sonraki bakım tarihi</returns>
        public DateTime CalculateNextMaintenanceDate(DateTime lastMaintenanceDate)
        {
            DateTime nextDate = lastMaintenanceDate;
            
            switch (FrequencyUnit.ToLower())
            {
                case "gün":
                    nextDate = lastMaintenanceDate.AddDays(FrequencyInterval);
                    break;
                case "hafta":
                    nextDate = lastMaintenanceDate.AddDays(FrequencyInterval * 7);
                    break;
                case "ay":
                    nextDate = lastMaintenanceDate.AddMonths(FrequencyInterval);
                    break;
                case "yıl":
                    nextDate = lastMaintenanceDate.AddYears(FrequencyInterval);
                    break;
                default:
                    nextDate = lastMaintenanceDate.AddMonths(1); // Varsayılan olarak 1 ay
                    break;
            }
            
            return nextDate;
        }
        
        /// <summary>
        /// Bakım planını günceller
        /// </summary>
        /// <param name="lastMaintenanceDate">Son bakım tarihi</param>
        /// <param name="lastMaintenanceBy">Son bakımı yapan</param>
        public void UpdateMaintenanceStatus(DateTime lastMaintenanceDate, string lastMaintenanceBy)
        {
            LastMaintenanceDate = lastMaintenanceDate;
            LastMaintenanceBy = lastMaintenanceBy;
            NextMaintenanceDate = CalculateNextMaintenanceDate(lastMaintenanceDate);
            LastUpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Bakım planından servis talebi oluşturur
        /// </summary>
        /// <param name="userId">Oluşturan kullanıcı ID</param>
        /// <returns>Oluşturulan servis talebi</returns>
        public ServiceRequest CreateServiceRequest(int userId)
        {
            var serviceRequest = new ServiceRequest
            {
                FirmaId = this.FirmaId,
                SubeId = this.SubeId,
                ServiceTypeId = 2, // Bakım tipi servis
                ServiceTypeName = "Bakım",
                Title = $"Periyodik Bakım: {this.Name}",
                Description = $"Periyodik bakım talebi: {this.Description}",
                PriorityLevel = this.Priority,
                Status = "Yeni",
                RequestedById = userId,
                ResidenceId = this.ResidenceId,
                ResidenceName = this.ResidenceName,
                BlockId = this.BlockId,
                BlockName = this.BlockName,
                ApartmentId = this.ApartmentId,
                ApartmentNumber = this.ApartmentName,
                RequestDate = DateTime.Now,
                IsAutomated = true,
                IsActive = true,
                CreatedById = userId,
                CreatedBy = "Sistem",
                CreatedDate = DateTime.Now
            };

            // Bakım planındaki talimatları ekle
            if (!string.IsNullOrEmpty(this.Instructions))
            {
                serviceRequest.AddTask(new ServiceTask
                {
                    TaskName = "Bakım Talimatları",
                    Description = this.Instructions,
                    EstimatedMinutes = this.EstimatedDuration.HasValue ? (int)(this.EstimatedDuration.Value * 60) : 60,
                    IsRequired = true,
                    CreatedBy = "Sistem"
                });
            }

            // Bakım planındaki malzemeleri ekle
            foreach (var material in this.Materials)
            {
                serviceRequest.AddMaterial(new ServiceMaterial
                {
                    MaterialName = material.MaterialName,
                    MaterialCode = material.MaterialCode,
                    Quantity = material.Quantity,
                    Unit = material.Unit,
                    UnitPrice = material.UnitPrice,
                    TotalPrice = material.TotalPrice,
                    Notes = material.Notes
                });
            }

            return serviceRequest;
        }
        
        public MaintenancePlan()
        {
            Tasks = new List<MaintenanceTask>();
            History = new List<MaintenanceHistory>();
            Materials = new List<MaintenanceMaterial>();
            CreatedDate = DateTime.Now;
            Status = "Aktif";
            Priority = "Normal";
        }
    }
    
    /// <summary>
    /// Bakım planı geçmişini takip eden sınıf
    /// </summary>
    public class MaintenancePlanHistory : BaseEntity
    {
        /// <summary>
        /// Bakım planı ID'si
        /// </summary>
        public int MaintenancePlanId { get; set; }
        
        /// <summary>
        /// İlgili bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime ActionDate { get; set; }
        
        /// <summary>
        /// İşlem tipi (Oluşturma, Güncelleme, Tamamlandı, İptal, Talep Oluşturma)
        /// </summary>
        public string ActionType { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// İlgili servis talebi ID'si
        /// </summary>
        public int? ServiceRequestId { get; set; }
        
        /// <summary>
        /// İlgili servis talebi
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; }
        
        public MaintenancePlanHistory()
        {
            ActionDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }
    
    /// <summary>
    /// Bakım planı takvimini takip eden sınıf
    /// </summary>
    public class MaintenanceCalendar
    {
        /// <summary>
        /// Bakım planı ID'si
        /// </summary>
        public int MaintenancePlanId { get; set; }
        
        /// <summary>
        /// İlgili bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
        
        /// <summary>
        /// Plan adı
        /// </summary>
        public string PlanName { get; set; }
        
        /// <summary>
        /// Bakım tarihi
        /// </summary>
        public DateTime MaintenanceDate { get; set; }
        
        /// <summary>
        /// Durum (Beklemede, Tamamlandı, İptal, Gecikti)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Atanan teknisyen/ekip ID'si
        /// </summary>
        public int? AssignedToId { get; set; }
        
        /// <summary>
        /// Atanan teknisyen/ekip adı
        /// </summary>
        public string AssignedToName { get; set; }
        
        /// <summary>
        /// İlgili servis talebi ID'si
        /// </summary>
        public int? ServiceRequestId { get; set; }
        
        /// <summary>
        /// Rezidans ID'si
        /// </summary>
        public int? ResidenceId { get; set; }
        
        /// <summary>
        /// Rezidans adı
        /// </summary>
        public string ResidenceName { get; set; }
        
        /// <summary>
        /// Blok ID'si
        /// </summary>
        public int? BlockId { get; set; }
        
        /// <summary>
        /// Blok adı
        /// </summary>
        public string BlockName { get; set; }
        
        /// <summary>
        /// Daire ID'si
        /// </summary>
        public int? ApartmentId { get; set; }
        
        /// <summary>
        /// Daire numarası
        /// </summary>
        public string ApartmentNumber { get; set; }
        
        /// <summary>
        /// Öncelik seviyesi
        /// </summary>
        public int PriorityLevel { get; set; }
        
        /// <summary>
        /// Renk kodu (takvim görünümü için)
        /// </summary>
        public string ColorCode { get; set; }
        
        /// <summary>
        /// Tamamlanma tarihi
        /// </summary>
        public DateTime? CompletionDate { get; set; }
        
        /// <summary>
        /// Tahmini tamamlanma süresi (dakika)
        /// </summary>
        public int EstimatedDuration { get; set; }
        
        /// <summary>
        /// Firma ID'si
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID'si
        /// </summary>
        public int SubeId { get; set; }
    }

    /// <summary>
    /// Bakım geçmişi sınıfı
    /// </summary>
    public class MaintenanceHistory : BaseEntity
    {
        /// <summary>
        /// Bakım planı ID'si
        /// </summary>
        public int MaintenancePlanId { get; set; }
        
        /// <summary>
        /// İlgili bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime ActionDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// İşlem tipi (Oluşturuldu, Güncellendi, Tamamlandı, İptal Edildi)
        /// </summary>
        public string ActionType { get; set; } = string.Empty;
        
        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        
        /// <summary>
        /// İşlemi yapan kullanıcı ID
        /// </summary>
        public int PerformedById { get; set; }
        
        /// <summary>
        /// İşlemi yapan kullanıcı adı
        /// </summary>
        public string PerformedByName { get; set; } = string.Empty;
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
    }
    
    /// <summary>
    /// Bakım malzemeleri sınıfı
    /// </summary>
    public class MaintenanceMaterial : BaseEntity
    {
        /// <summary>
        /// Bakım planı ID'si
        /// </summary>
        public int MaintenancePlanId { get; set; }
        
        /// <summary>
        /// İlgili bakım planı
        /// </summary>
        public virtual MaintenancePlan MaintenancePlan { get; set; }
        
        /// <summary>
        /// Malzeme ID'si
        /// </summary>
        public int MaterialId { get; set; }
        
        /// <summary>
        /// Malzeme adı
        /// </summary>
        public string MaterialName { get; set; } = string.Empty;
        
        /// <summary>
        /// Malzeme kodu
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Birim
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        
        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Toplam fiyat
        /// </summary>
        public decimal TotalPrice { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        
        /// <summary>
        /// Firma ID
        /// </summary>
        public int FirmaId { get; set; }
        
        /// <summary>
        /// Şube ID
        /// </summary>
        public int SubeId { get; set; }
    }
} 