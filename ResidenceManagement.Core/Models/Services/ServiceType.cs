using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Services
{
    // Servis taleplerinde kullanılacak olan tüm servis tiplerini tanımlayan sınıf
    public class ServiceType
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Kategori bilgileri
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        // Temel bilgiler
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Görsel
        public string IconUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Color { get; set; }
        
        // Durum
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }  // Sakinlerin görebileceği/talep edebileceği bir servis mi?
        public bool IsUrgent { get; set; }  // Acil durum servisi mi?
        public bool IsExternalService { get; set; }  // Dış hizmet alımı gerektiren servis mi?
        
        // Yetki ve atama bilgileri
        public bool RequiresManagerApproval { get; set; }
        public string DefaultAssignedDepartment { get; set; }
        public string DefaultAssignedRole { get; set; }
        public int? DefaultAssignedUserId { get; set; }
        public string DefaultAssignedUserName { get; set; }
        
        // Varsayılan parametreler
        public string DefaultPriorityLevel { get; set; }
        public int? EstimatedCompletionTime { get; set; }  // Dakika cinsinden
        public int? ResponseTimeTarget { get; set; }  // Dakika cinsinden
        public string WorkingHours { get; set; }  // JSON formatında (gün/saat bilgileri)
        
        // Fiyatlandırma bilgileri
        public bool IsChargeable { get; set; }
        public decimal? DefaultPrice { get; set; }
        public string CurrencyCode { get; set; }
        public string PricingType { get; set; }  // Sabit, Saatlik, Malzemeye göre vs.
        public decimal? MinimumCharge { get; set; }
        public bool IncludesMaterials { get; set; }
        public decimal? HourlyRate { get; set; }
        
        // Form ve adım yapılandırmaları
        public string RequiredFields { get; set; }  // JSON formatında zorunlu alanlar
        public string FormFields { get; set; }  // JSON formatında form elemanları
        public string Workflows { get; set; }  // JSON formatında iş akışı tanımları
        public string ChecklistItems { get; set; }  // JSON formatında kontrol listesi
        
        // İlişkili servis tipleri
        public string RelatedServiceTypes { get; set; }  // JSON formatında
        
        // Bildirim yapılandırması
        public bool NotifyPropertyManager { get; set; }
        public bool NotifyTechnicalStaff { get; set; }
        public bool NotifyResident { get; set; }
        public bool NotifyOwner { get; set; }
        public string CustomNotificationEmails { get; set; }
        public string NotificationTemplateId { get; set; }
        
        // Raporlama bilgileri
        public string ReportingCategory { get; set; }
        public string ReportingTags { get; set; }  // JSON formatında
        
        // Alan yönetimi
        public bool RequiresLocationInfo { get; set; }
        public string ApplicableLocations { get; set; }  // JSON formatında (daire, ortak alan, teknik alan vs.)
        
        // Malzeme yönetimi
        public bool RequiresMaterials { get; set; }
        public string DefaultMaterials { get; set; }  // JSON formatında varsayılan malzemeler
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // İçerik dil desteği
        public Dictionary<string, string> TranslatedNames { get; set; }
        public Dictionary<string, string> TranslatedDescriptions { get; set; }
        
        // Yapıcı metot
        public ServiceType()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsPublic = true;
            CurrencyCode = "TRY";
            DefaultPriorityLevel = "Normal";
            TranslatedNames = new Dictionary<string, string>();
            TranslatedDescriptions = new Dictionary<string, string>();
        }
        
        // Çeviri ekle
        public void AddTranslation(string languageCode, string translatedName, string translatedDescription = null)
        {
            if (!string.IsNullOrEmpty(languageCode) && !string.IsNullOrEmpty(translatedName))
            {
                TranslatedNames[languageCode] = translatedName;
                
                if (!string.IsNullOrEmpty(translatedDescription))
                {
                    TranslatedDescriptions[languageCode] = translatedDescription;
                }
            }
        }
        
        // Belirli bir dildeki çeviriyi al
        public string GetTranslatedName(string languageCode)
        {
            if (!string.IsNullOrEmpty(languageCode) && TranslatedNames.ContainsKey(languageCode))
            {
                return TranslatedNames[languageCode];
            }
            
            return Name;
        }
        
        public string GetTranslatedDescription(string languageCode)
        {
            if (!string.IsNullOrEmpty(languageCode) && TranslatedDescriptions.ContainsKey(languageCode))
            {
                return TranslatedDescriptions[languageCode];
            }
            
            return Description;
        }
        
        // Form alanı ekle
        public void AddFormField(string fieldName, string fieldType, bool isRequired = false, string defaultValue = null)
        {
            // Bu metot gerçek uygulamada JSON işleme mantığıyla çalışır
            // Burada sadece konsept olarak yer almaktadır
            // FormFields bir JSON dizisi olarak saklanacaktır
            
            // Örnek bir JSON formatı:
            // [{"name":"description","type":"textarea","required":true,"default":""},
            //  {"name":"location","type":"select","required":true,"options":["Salon","Mutfak","Banyo"]}]
        }
        
        // İş akışı adımı ekle
        public void AddWorkflowStep(string stepName, string description, string assignedRole, int order)
        {
            // Bu metot gerçek uygulamada JSON işleme mantığıyla çalışır
            // Burada sadece konsept olarak yer almaktadır
            // Workflows bir JSON dizisi olarak saklanacaktır
        }
        
        // Kontrol listesi maddesi ekle
        public void AddChecklistItem(string itemText, bool isRequired = true, int order = 0)
        {
            // Bu metot gerçek uygulamada JSON işleme mantığıyla çalışır
            // Burada sadece konsept olarak yer almaktadır
            // ChecklistItems bir JSON dizisi olarak saklanacaktır
        }
        
        // İlişkili servis tipi ekle
        public void AddRelatedServiceType(int serviceTypeId, string relationshipType)
        {
            // Bu metot gerçek uygulamada JSON işleme mantığıyla çalışır
            // Burada sadece konsept olarak yer almaktadır
            // RelatedServiceTypes bir JSON dizisi olarak saklanacaktır
        }
    }
} 