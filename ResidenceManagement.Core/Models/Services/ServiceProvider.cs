using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Hizmet sağlayıcıları (servis firmaları) modelleyen sınıf
    /// </summary>
    public class ServiceProvider : BaseEntity
    {
        /// <summary>
        /// Hizmet sağlayıcı kodu
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Hizmet sağlayıcı adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// İletişim kişisi
        /// </summary>
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Adres
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Vergi dairesi
        /// </summary>
        public string TaxOffice { get; set; }
        
        /// <summary>
        /// Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }
        
        /// <summary>
        /// Anlaşma başlangıç tarihi
        /// </summary>
        public DateTime? ContractStartDate { get; set; }
        
        /// <summary>
        /// Anlaşma bitiş tarihi
        /// </summary>
        public DateTime? ContractEndDate { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Sağladıkları hizmet kategorileri (virgülle ayrılmış kategori ID'leri)
        /// </summary>
        public string ServiceCategoryIds { get; set; }
        
        /// <summary>
        /// Çalışma saatleri
        /// </summary>
        public string WorkingHours { get; set; }
        
        /// <summary>
        /// Anlaşma notları
        /// </summary>
        public string ContractNotes { get; set; }
        
        /// <summary>
        /// Anlaşma dosya yolu (sözleşme dosyası)
        /// </summary>
        public string ContractFilePath { get; set; }
        
        /// <summary>
        /// Ödeme şekli
        /// </summary>
        public string PaymentTerms { get; set; }
        
        /// <summary>
        /// Varsayılan para birimi
        /// </summary>
        public string DefaultCurrency { get; set; }
        
        /// <summary>
        /// Hizmet sağlayıcı logosu
        /// </summary>
        public string LogoUrl { get; set; }
        
        /// <summary>
        /// Web sitesi
        /// </summary>
        public string Website { get; set; }
        
        /// <summary>
        /// Bu sağlayıcıya ait servis istekleri
        /// </summary>
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
        
        /// <summary>
        /// Bu sağlayıcının uzman personelleri
        /// </summary>
        public virtual ICollection<ServiceExpert> ServiceExperts { get; set; }
        
        /// <summary>
        /// Bu sağlayıcının sağladığı servis tanımları
        /// </summary>
        public virtual ICollection<ServiceDefinition> ServiceDefinitions { get; set; }
        
        /// <summary>
        /// Bu sağlayıcının fatura bilgileri
        /// </summary>
        public virtual ICollection<ServiceInvoice> Invoices { get; set; }
        
        /// <summary>
        /// Aktif mi
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Bu sağlayıcıya ait bakım planları
        /// </summary>
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        
        /// <summary>
        /// Kalite puanı (1-5 arası)
        /// </summary>
        public decimal QualityRating { get; set; }
        
        /// <summary>
        /// Fiyat performans puanı (1-5 arası)
        /// </summary>
        public decimal PricePerformanceRating { get; set; }
        
        /// <summary>
        /// Tepki süresi puanı (1-5 arası)
        /// </summary>
        public decimal ResponseTimeRating { get; set; }
        
        /// <summary>
        /// Değerlendirme sayısı
        /// </summary>
        public int RatingCount { get; set; }
        
        public ServiceProvider()
        {
            ServiceRequests = new List<ServiceRequest>();
            ServiceExperts = new List<ServiceExpert>();
            ServiceDefinitions = new List<ServiceDefinition>();
            Invoices = new List<ServiceInvoice>();
            MaintenancePlans = new List<MaintenancePlan>();
            CreatedDate = DateTime.Now;
            IsActive = true;
            QualityRating = 0;
            PricePerformanceRating = 0;
            ResponseTimeRating = 0;
            RatingCount = 0;
            DefaultCurrency = "TRY";
        }
        
        /// <summary>
        /// Sağlayıcıyı aktifleştirir
        /// </summary>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void Activate(int userId)
        {
            this.IsActive = true;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Sağlayıcıyı deaktif eder
        /// </summary>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void Deactivate(int userId)
        {
            this.IsActive = false;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Anlaşmayı yeniler
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="notes">Anlaşma notları</param>
        /// <param name="contractFilePath">Anlaşma dosya yolu</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void RenewContract(DateTime startDate, DateTime endDate, string notes, string contractFilePath, int userId)
        {
            this.ContractStartDate = startDate;
            this.ContractEndDate = endDate;
            this.ContractNotes = notes;
            
            if (!string.IsNullOrEmpty(contractFilePath))
            {
                this.ContractFilePath = contractFilePath;
            }
            
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Sağlayıcıya ait yeni bir uzman ekler
        /// </summary>
        /// <param name="expert">Eklenecek uzman</param>
        public void AddExpert(ServiceExpert expert)
        {
            expert.ServiceProviderId = this.Id;
            this.ServiceExperts.Add(expert);
        }
        
        /// <summary>
        /// Sağlayıcının puanlarını günceller
        /// </summary>
        /// <param name="quality">Kalite puanı (1-5)</param>
        /// <param name="pricePerformance">Fiyat performans puanı (1-5)</param>
        /// <param name="responseTime">Tepki süresi puanı (1-5)</param>
        public void UpdateRatings(decimal quality, decimal pricePerformance, decimal responseTime)
        {
            decimal totalQuality = (this.QualityRating * this.RatingCount) + quality;
            decimal totalPricePerformance = (this.PricePerformanceRating * this.RatingCount) + pricePerformance;
            decimal totalResponseTime = (this.ResponseTimeRating * this.RatingCount) + responseTime;
            
            this.RatingCount++;
            
            this.QualityRating = Math.Round(totalQuality / this.RatingCount, 1);
            this.PricePerformanceRating = Math.Round(totalPricePerformance / this.RatingCount, 1);
            this.ResponseTimeRating = Math.Round(totalResponseTime / this.RatingCount, 1);
        }
    }
} 