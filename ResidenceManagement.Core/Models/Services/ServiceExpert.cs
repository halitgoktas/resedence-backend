using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Teknik servis ve diğer hizmetleri sağlayan uzman personelleri modelleyen sınıf
    /// </summary>
    public class ServiceExpert : BaseEntity
    {
        /// <summary>
        /// Uzmanın bağlı olduğu servis sağlayıcı ID'si
        /// </summary>
        public int ServiceProviderId { get; set; }
        
        /// <summary>
        /// Bağlı olduğu servis sağlayıcı
        /// </summary>
        public virtual ServiceProvider ServiceProvider { get; set; }
        
        /// <summary>
        /// Uzman adı
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Uzman soyadı
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Uzmanın tam adı (Sadece okuma)
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
        
        /// <summary>
        /// Telefon numarası
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// E-posta adresi
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Kimlik numarası
        /// </summary>
        public string IdentityNumber { get; set; }
        
        /// <summary>
        /// Profil fotoğrafı URL'si
        /// </summary>
        public string ProfileImageUrl { get; set; }
        
        /// <summary>
        /// Uzmanın uzmanlık alanları (virgülle ayrılmış servis kategori ID'leri)
        /// </summary>
        public string ExpertiseAreas { get; set; }
        
        /// <summary>
        /// Uzmanın uzmanlık seviyesi (1-5 arası)
        /// </summary>
        public int ExpertiseLevel { get; set; }
        
        /// <summary>
        /// İşe başlama tarihi
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// İşten ayrılma tarihi (eğer ayrıldıysa)
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Çalışma durumu (Aktif, İzinli, Ayrılmış vs.)
        /// </summary>
        public string WorkStatus { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Sertifika bilgileri (JSON formatında)
        /// </summary>
        public string Certifications { get; set; }
        
        /// <summary>
        /// Eğitim bilgileri (JSON formatında)
        /// </summary>
        public string Education { get; set; }
        
        /// <summary>
        /// Ortalama müdahale süresi (dakika)
        /// </summary>
        public int? AverageResponseTime { get; set; }
        
        /// <summary>
        /// Uzmanın sahip olduğu yetenekler (virgülle ayrılmış)
        /// </summary>
        public string Skills { get; set; }
        
        /// <summary>
        /// Günlük maksimum servis kapasitesi
        /// </summary>
        public int? DailyCapacity { get; set; }
        
        /// <summary>
        /// Uzmanın atandığı servis istekleri
        /// </summary>
        public virtual ICollection<ServiceRequest> AssignedRequests { get; set; }
        
        /// <summary>
        /// Uzmanın düzenlediği raporlar
        /// </summary>
        public virtual ICollection<ServiceReport> ServiceReports { get; set; }
        
        /// <summary>
        /// Uzmanın katıldığı bakım planları
        /// </summary>
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        
        /// <summary>
        /// Uzmanın çalışma saatleri (JSON formatında)
        /// </summary>
        public string WorkingHours { get; set; }
        
        /// <summary>
        /// Uzmanın mevcut lokasyonu (GPS koordinatları)
        /// </summary>
        public string CurrentLocation { get; set; }
        
        /// <summary>
        /// Son konum güncelleme zamanı
        /// </summary>
        public DateTime? LastLocationUpdate { get; set; }
        
        /// <summary>
        /// Müşteri memnuniyet puanı (1-5 arası)
        /// </summary>
        public decimal CustomerSatisfactionRating { get; set; }
        
        /// <summary>
        /// Değerlendirme sayısı
        /// </summary>
        public int RatingCount { get; set; }
        
        /// <summary>
        /// Tamamlanan servis isteği sayısı
        /// </summary>
        public int CompletedRequestCount { get; set; }
        
        /// <summary>
        /// Acil durum servisi için uygun mu?
        /// </summary>
        public bool IsAvailableForEmergency { get; set; }
        
        /// <summary>
        /// Mobil uygulamada görünür mü?
        /// </summary>
        public bool IsVisibleInMobileApp { get; set; }
        
        public ServiceExpert()
        {
            AssignedRequests = new List<ServiceRequest>();
            ServiceReports = new List<ServiceReport>();
            MaintenancePlans = new List<MaintenancePlan>();
            CreatedDate = DateTime.Now;
            WorkStatus = "Aktif";
            ExpertiseLevel = 3;
            CustomerSatisfactionRating = 0;
            RatingCount = 0;
            CompletedRequestCount = 0;
            IsAvailableForEmergency = false;
            IsVisibleInMobileApp = true;
        }
        
        /// <summary>
        /// Uzmanın durumunu günceller
        /// </summary>
        /// <param name="status">Yeni durum</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="notes">Ek notlar</param>
        public void UpdateStatus(string status, int userId, string notes = null)
        {
            this.WorkStatus = status;
            
            if (!string.IsNullOrEmpty(notes))
            {
                this.Notes = string.IsNullOrEmpty(this.Notes) 
                    ? notes 
                    : $"{this.Notes}\n{DateTime.Now.ToString("dd.MM.yyyy HH:mm")} - {notes}";
            }
            
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Uzmanın konumunu günceller
        /// </summary>
        /// <param name="location">Yeni konum (GPS koordinatları)</param>
        public void UpdateLocation(string location)
        {
            this.CurrentLocation = location;
            this.LastLocationUpdate = DateTime.Now;
        }
        
        /// <summary>
        /// Uzmanın memnuniyet puanını günceller
        /// </summary>
        /// <param name="rating">Yeni değerlendirme puanı (1-5)</param>
        public void AddRating(decimal rating)
        {
            decimal totalRating = (this.CustomerSatisfactionRating * this.RatingCount) + rating;
            this.RatingCount++;
            this.CustomerSatisfactionRating = Math.Round(totalRating / this.RatingCount, 1);
        }
        
        /// <summary>
        /// Tamamlanan servis isteği sayısını artırır
        /// </summary>
        public void IncrementCompletedRequests()
        {
            this.CompletedRequestCount++;
        }
        
        /// <summary>
        /// Uzmanın acil durum servis uygunluğunu değiştirir
        /// </summary>
        /// <param name="isAvailable">Uygunluk durumu</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        public void SetEmergencyAvailability(bool isAvailable, int userId)
        {
            this.IsAvailableForEmergency = isAvailable;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
        }
    }
} 