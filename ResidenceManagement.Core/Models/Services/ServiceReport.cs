using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Services
{
    /// <summary>
    /// Teknik servis ve bakım işlemlerinin raporlarını temsil eden sınıf
    /// </summary>
    public class ServiceReport : BaseEntity
    {
        /// <summary>
        /// Rapor numarası
        /// </summary>
        public string ReportNumber { get; private set; }
        
        /// <summary>
        /// Servis talebi ID
        /// </summary>
        public int? ServiceRequestId { get; set; }
        
        /// <summary>
        /// Rapor tipi (Arıza, Periyodik Bakım, Kontrol, İnceleme, vb.)
        /// </summary>
        public ServiceReportType ReportType { get; set; }
        
        /// <summary>
        /// Rapor başlığı
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Raporun durumu (Taslak, Tamamlandı, Onaylandı, İptal Edildi)
        /// </summary>
        public ServiceReportStatus Status { get; private set; }
        
        /// <summary>
        /// Servis tarihi
        /// </summary>
        public DateTime ServiceDate { get; set; }
        
        /// <summary>
        /// Raporun oluşturulma tarihi
        /// </summary>
        public DateTime ReportDate { get; set; }
        
        /// <summary>
        /// Servis teknisyeni ID'si
        /// </summary>
        public int TechnicianId { get; set; }
        
        /// <summary>
        /// Servis teknisyeni adı
        /// </summary>
        public string TechnicianName { get; set; }
        
        /// <summary>
        /// Servis saati (başlangıç)
        /// </summary>
        public DateTime ServiceStartTime { get; set; }
        
        /// <summary>
        /// Servis saati (bitiş)
        /// </summary>
        public DateTime ServiceEndTime { get; set; }
        
        /// <summary>
        /// Toplam servis süresi (dakika)
        /// </summary>
        public TimeSpan TotalServiceTime { get; private set; }
        
        /// <summary>
        /// İlgili site ID
        /// </summary>
        public int? ResidenceId { get; set; }
        
        /// <summary>
        /// İlgili site adı
        /// </summary>
        public string ResidenceName { get; set; }
        
        /// <summary>
        /// İlgili blok ID
        /// </summary>
        public int? BlockId { get; set; }
        
        /// <summary>
        /// İlgili blok adı
        /// </summary>
        public string BlockName { get; set; }
        
        /// <summary>
        /// İlgili daire ID
        /// </summary>
        public int? ApartmentId { get; set; }
        
        /// <summary>
        /// İlgili daire numarası
        /// </summary>
        public string ApartmentNumber { get; set; }
        
        /// <summary>
        /// İlgili ortak alan ID
        /// </summary>
        public int? CommonAreaId { get; set; }
        
        /// <summary>
        /// İlgili ortak alan adı
        /// </summary>
        public string CommonAreaName { get; set; }
        
        /// <summary>
        /// Cihaz veya ekipman bilgisi
        /// </summary>
        public string EquipmentInfo { get; set; }
        
        /// <summary>
        /// Marka
        /// </summary>
        public string Brand { get; set; }
        
        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// Seri numarası
        /// </summary>
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// Arıza/bakım açıklaması
        /// </summary>
        public string IssueDescription { get; set; }
        
        /// <summary>
        /// Yapılan işlemler
        /// </summary>
        public string WorkDescription { get; set; }
        
        /// <summary>
        /// Bulgular ve sonuçlar
        /// </summary>
        public string Findings { get; set; }
        
        /// <summary>
        /// Öneriler
        /// </summary>
        public string Recommendations { get; set; }
        
        /// <summary>
        /// Kullanılan malzemeler (JSON formatında ayrıntılı liste)
        /// </summary>
        public string MaterialsUsedJson { get; set; }
        
        /// <summary>
        /// Toplam malzeme maliyeti
        /// </summary>
        public decimal MaterialsCost { get; set; }
        
        /// <summary>
        /// İşçilik maliyeti
        /// </summary>
        public decimal LaborCost { get; set; }
        
        /// <summary>
        /// Diğer maliyetler
        /// </summary>
        public decimal AdditionalCosts { get; set; }
        
        /// <summary>
        /// Toplam maliyet
        /// </summary>
        public decimal TotalCost { get; private set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string Currency { get; set; } = "TRY";
        
        /// <summary>
        /// Müşteri adı (daire sahibi/kiracı)
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// Müşteri telefonu
        /// </summary>
        public string CustomerPhone { get; set; }
        
        /// <summary>
        /// Müşteri e-posta
        /// </summary>
        public string CustomerEmail { get; set; }
        
        /// <summary>
        /// Müşteri onayı durumu
        /// </summary>
        public bool RequiresCustomerApproval { get; set; }
        
        /// <summary>
        /// Müşteri onay tarihi
        /// </summary>
        public DateTime? CustomerApprovalDate { get; private set; }
        
        /// <summary>
        /// Müşteri notu/yorumu
        /// </summary>
        public string CustomerNotes { get; set; }
        
        /// <summary>
        /// Müşteri imza URL'si
        /// </summary>
        public string CustomerSignatureUrl { get; set; }
        
        /// <summary>
        /// Yönetici onayı durumu
        /// </summary>
        public bool RequiresManagerApproval { get; set; }
        
        /// <summary>
        /// Onaylayan yönetici ID'si
        /// </summary>
        public int? ApprovedByManagerId { get; set; }
        
        /// <summary>
        /// Onaylayan yönetici adı
        /// </summary>
        public string ManagerName { get; set; }
        
        /// <summary>
        /// Yönetici onay tarihi
        /// </summary>
        public DateTime? ManagerApprovalDate { get; private set; }
        
        /// <summary>
        /// Yönetici notu
        /// </summary>
        public string ManagerNotes { get; set; }
        
        /// <summary>
        /// Fatura ID'si
        /// </summary>
        public int? InvoiceId { get; set; }
        
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }
        
        /// <summary>
        /// Fatura durumu
        /// </summary>
        public string InvoiceStatus { get; set; }
        
        /// <summary>
        /// Fotoğraf URL'leri (JSON formatında)
        /// </summary>
        public string PhotoUrlsJson { get; set; }
        
        /// <summary>
        /// Ek dosya URL'leri (JSON formatında)
        /// </summary>
        public string AttachmentUrlsJson { get; set; }
        
        /// <summary>
        /// Teknisyen imza URL'si
        /// </summary>
        public string TechnicianSignatureUrl { get; set; }
        
        /// <summary>
        /// Takip gerektiriyor mu?
        /// </summary>
        public bool RequiresFollowUp { get; set; }
        
        /// <summary>
        /// Takip tarihi
        /// </summary>
        public DateTime? FollowUpDate { get; set; }
        
        /// <summary>
        /// Takip notları
        /// </summary>
        public string FollowUpNotes { get; set; }
        
        /// <summary>
        /// Raporla ilişkili görevler
        /// </summary>
        public ICollection<ServiceTask> Tasks { get; set; }
        
        /// <summary>
        /// Raporla ilişkili kontrol listeleri (JSON formatında)
        /// </summary>
        public string ChecklistsJson { get; set; }
        
        /// <summary>
        /// Durum izleme bilgileri (JSON formatında)
        /// </summary>
        public string StatusTrackingInfo { get; set; }
        
        /// <summary>
        /// Garanti bilgisi
        /// </summary>
        public string WarrantyInfo { get; set; }
        
        /// <summary>
        /// Rapor kategorizasyon etiketleri (virgülle ayrılmış)
        /// </summary>
        public string Tags { get; set; }
        
        /// <summary>
        /// Fatura oluşturuldu mu?
        /// </summary>
        public bool IsBilled { get; private set; }
        
        /// <summary>
        /// Özel alanlar (JSON formatında)
        /// </summary>
        public string CustomFields { get; set; }
        
        /// <summary>
        /// İç notlar
        /// </summary>
        public string InternalNotes { get; set; }
        
        /// <summary>
        /// Memnuniyet değerlendirmesi
        /// </summary>
        public int? SatisfactionRating { get; set; }
        
        /// <summary>
        /// Memnuniyet yorumları
        /// </summary>
        public string FeedbackComments { get; set; }
        
        /// <summary>
        /// İzlenebilirlik için log
        /// </summary>
        public string ActivityLogJson { get; set; }
        
        /// <summary>
        /// Rapor müşteri tarafından onaylandı mı?
        /// </summary>
        public bool IsCustomerApproved { get; private set; }
        
        /// <summary>
        /// Rapor yönetici tarafından onaylandı mı?
        /// </summary>
        public bool IsManagerApproved { get; private set; }
        
        /// <summary>
        /// Raporla ilişkili kontrol listeleri
        /// </summary>
        public List<Dictionary<string, object>> Checklists 
        { 
            get => string.IsNullOrEmpty(ChecklistsJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(ChecklistsJson); 
            set => ChecklistsJson = JsonSerializer.Serialize(value); 
        }
        
        /// <summary>
        /// Raporla ilişkili fotoğraflar
        /// </summary>
        public List<string> PhotoUrls 
        { 
            get => string.IsNullOrEmpty(PhotoUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(PhotoUrlsJson); 
            set => PhotoUrlsJson = JsonSerializer.Serialize(value); 
        }
        
        /// <summary>
        /// Raporla ilişkili ek dosyalar
        /// </summary>
        public List<string> AttachmentUrls 
        { 
            get => string.IsNullOrEmpty(AttachmentUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(AttachmentUrlsJson); 
            set => AttachmentUrlsJson = JsonSerializer.Serialize(value); 
        }
        
        /// <summary>
        /// Raporla ilişkili malzeme ve parçalar
        /// </summary>
        public List<Dictionary<string, object>> MaterialsUsed 
        { 
            get => string.IsNullOrEmpty(MaterialsUsedJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(MaterialsUsedJson); 
            set => MaterialsUsedJson = JsonSerializer.Serialize(value); 
        }
        
        /// <summary>
        /// Raporla ilişkili aktivite log
        /// </summary>
        public List<Dictionary<string, object>> ActivityLog 
        { 
            get => string.IsNullOrEmpty(ActivityLogJson) ? new List<Dictionary<string, object>>() : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(ActivityLogJson); 
            set => ActivityLogJson = JsonSerializer.Serialize(value); 
        }
        
        public ServiceReport()
        {
            ReportNumber = GenerateReportNumber();
            Status = ServiceReportStatus.Draft;
            ReportDate = DateTime.Now;
            Currency = "TRY";
            RequiresCustomerApproval = false;
            RequiresManagerApproval = true;
            IsBilled = false;
            CreatedDate = DateTime.Now;
            Tasks = new List<ServiceTask>();
        }
        
        /// <summary>
        /// Rapor numarası oluşturur
        /// </summary>
        /// <returns>Rapor numarası</returns>
        private string GenerateReportNumber()
        {
            return $"SR-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
        
        /// <summary>
        /// Toplam servis süresini hesaplar
        /// </summary>
        public void CalculateServiceTime()
        {
            if (ServiceEndTime > ServiceStartTime)
            {
                TotalServiceTime = ServiceEndTime - ServiceStartTime;
            }
            else
            {
                TotalServiceTime = TimeSpan.Zero;
            }
        }
        
        /// <summary>
        /// Toplam maliyeti hesaplar
        /// </summary>
        public void CalculateTotalCost()
        {
            TotalCost = LaborCost + MaterialsCost + AdditionalCosts;
        }
        
        /// <summary>
        /// Rapor durumunu günceller
        /// </summary>
        /// <param name="newStatus">Yeni durum</param>
        /// <param name="userId">İşlemi yapan kullanıcı ID'si</param>
        /// <param name="notes">Notlar</param>
        public void UpdateStatus(ServiceReportStatus newStatus, int userId, string notes = null)
        {
            var oldStatus = Status;
            Status = newStatus;
            this.UpdatedBy = userId;
            this.UpdatedDate = DateTime.Now;
            
            // Durum izleme bilgilerini güncelle
            var trackingInfo = new List<StatusTrackingItem>();
            
            if (!string.IsNullOrEmpty(StatusTrackingInfo))
            {
                // Mevcut durum izleme bilgilerini deserialize et
                trackingInfo = JsonSerializer.Deserialize<List<StatusTrackingItem>>(StatusTrackingInfo);
            }
            
            // Yeni durum bilgisini ekle
            trackingInfo.Add(new StatusTrackingItem
            {
                Status = newStatus.ToString(),
                Date = DateTime.Now,
                UserId = userId,
                Notes = notes ?? string.Empty
            });
            
            // Durum izleme bilgilerini serialize et
            StatusTrackingInfo = JsonSerializer.Serialize(trackingInfo);
        }
        
        /// <summary>
        /// Müşteri onayını kaydeder
        /// </summary>
        /// <param name="isApproved">Onay durumu</param>
        /// <param name="notes">Müşteri notu</param>
        /// <param name="signatureUrl">İmza URL'si</param>
        /// <param name="satisfactionRating">Memnuniyet puanı</param>
        public void SetCustomerApproval(bool isApproved, string notes = null, string signatureUrl = null)
        {
            IsCustomerApproved = isApproved;
            CustomerApprovalDate = isApproved ? DateTime.Now : (DateTime?)null;
            
            if (!string.IsNullOrEmpty(notes))
            {
                CustomerNotes = notes;
            }
            
            if (!string.IsNullOrEmpty(signatureUrl))
            {
                CustomerSignatureUrl = signatureUrl;
            }
            
            if (isApproved && (!RequiresManagerApproval || IsManagerApproved))
            {
                Status = ServiceReportStatus.Completed;
            }
            
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Yönetici onayını kaydeder
        /// </summary>
        /// <param name="isApproved">Onay durumu</param>
        /// <param name="managerId">Yönetici ID'si</param>
        /// <param name="managerName">Yönetici adı</param>
        /// <param name="notes">Yönetici notu</param>
        public void SetManagerApproval(bool isApproved, int managerId, string managerName, string notes = null)
        {
            IsManagerApproved = isApproved;
            ManagerApprovalDate = isApproved ? DateTime.Now : (DateTime?)null;
            ApprovedByManagerId = isApproved ? managerId : (int?)null;
            
            if (!string.IsNullOrEmpty(notes))
            {
                ManagerNotes = notes;
            }
            
            this.UpdatedBy = managerId;
            this.UpdatedDate = DateTime.Now;
            
            if (isApproved)
            {
                Status = ServiceReportStatus.Completed;
                
                // Durum izleme bilgilerini güncelle
                var trackingInfo = new List<StatusTrackingItem>();
                
                if (!string.IsNullOrEmpty(StatusTrackingInfo))
                {
                    // Mevcut durum izleme bilgilerini deserialize et
                    trackingInfo = JsonSerializer.Deserialize<List<StatusTrackingItem>>(StatusTrackingInfo);
                }
                
                // Yeni durum bilgisini ekle
                trackingInfo.Add(new StatusTrackingItem
                {
                    Status = Status.ToString(),
                    Date = DateTime.Now,
                    UserId = managerId,
                    Notes = notes
                });
                
                // Durum izleme bilgilerini serialize et
                StatusTrackingInfo = JsonSerializer.Serialize(trackingInfo);
            }
        }
        
        /// <summary>
        /// Rapora fotoğraf ekler
        /// </summary>
        /// <param name="photoUrl">Fotoğraf URL'si</param>
        public void AddPhoto(string photoUrl)
        {
            if (string.IsNullOrEmpty(photoUrl))
            {
                return;
            }
            
            var photos = PhotoUrls ?? new List<string>();
            photos.Add(photoUrl);
            PhotoUrls = photos;
            
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Rapora ek dosya ekler
        /// </summary>
        /// <param name="attachmentUrl">Ek dosya URL'si</param>
        /// <param name="fileName">Dosya adı</param>
        public void AddAttachment(string attachmentUrl, string fileName)
        {
            if (string.IsNullOrEmpty(attachmentUrl))
            {
                return;
            }
            
            var attachments = AttachmentUrls ?? new List<string>();
            attachments.Add(attachmentUrl);
            AttachmentUrls = attachments;
            
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Rapora kontrol listesi ekler
        /// </summary>
        /// <param name="checklistItems">Kontrol listesi öğeleri</param>
        public void AddChecklist(List<ChecklistItem> checklistItems)
        {
            if (checklistItems == null || checklistItems.Count == 0)
            {
                return;
            }
            
            // Dictionary formatına dönüştürme
            var checklistDict = checklistItems.Select(item => item.ToDictionary()).ToList();
            var checklists = Checklists ?? new List<Dictionary<string, object>>();
            checklists.Add(checklistDict.FirstOrDefault() ?? new Dictionary<string, object>());
            Checklists = checklists;
            
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Fatura ilişkilendirir
        /// </summary>
        /// <param name="invoiceId">Fatura ID'si</param>
        /// <param name="invoiceNumber">Fatura numarası</param>
        /// <param name="invoiceStatus">Fatura durumu</param>
        public void LinkInvoice(int invoiceId, string invoiceNumber, string invoiceStatus)
        {
            this.InvoiceId = invoiceId;
            this.InvoiceNumber = invoiceNumber;
            this.InvoiceStatus = invoiceStatus;
            this.IsBilled = true;
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Takip bilgisini günceller
        /// </summary>
        /// <param name="requiresFollowUp">Takip gerekiyor mu?</param>
        /// <param name="followUpDate">Takip tarihi</param>
        /// <param name="notes">Takip notları</param>
        public void SetFollowUpInfo(bool requiresFollowUp, DateTime? followUpDate = null, string notes = null)
        {
            this.RequiresFollowUp = requiresFollowUp;
            
            if (requiresFollowUp)
            {
                this.FollowUpDate = followUpDate ?? DateTime.Now.AddDays(7);
                this.FollowUpNotes = notes;
            }
            else
            {
                this.FollowUpDate = null;
                this.FollowUpNotes = null;
            }
            
            this.UpdatedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Malzeme ekleme
        /// </summary>
        /// <param name="name">Malzeme adı</param>
        /// <param name="quantity">Miktar</param>
        /// <param name="unitPrice">Birim fiyat</param>
        /// <param name="description">Açıklama</param>
        public void AddMaterial(string name, int quantity, decimal unitPrice, string description = null)
        {
            var materials = MaterialsUsed ?? new List<Dictionary<string, object>>();
            materials.Add(new Dictionary<string, object>
            {
                { "name", name },
                { "quantity", quantity },
                { "unitPrice", unitPrice },
                { "totalPrice", quantity * unitPrice },
                { "description", description ?? string.Empty },
                { "addedAt", DateTime.Now }
            });
            
            MaterialsUsed = materials;
            
            // Malzeme maliyetini güncelle
            MaterialsCost += quantity * unitPrice;
            CalculateTotalCost();
        }
    }
    
    /// <summary>
    /// Durum izleme bilgisi
    /// </summary>
    public class StatusTrackingItem
    {
        /// <summary>
        /// Durum
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Tarih
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Kullanıcı ID'si
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Kullanıcı adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Notlar
        /// </summary>
        public string Notes { get; set; }
    }
    
    /// <summary>
    /// Rapor türleri için enum
    /// </summary>
    public enum ServiceReportType
    {
        Maintenance = 1,
        Repair = 2,
        Installation = 3,
        Inspection = 4,
        Emergency = 5,
        Scheduled = 6,
        Other = 99
    }
    
    /// <summary>
    /// Rapor durumları için enum
    /// </summary>
    public enum ServiceReportStatus
    {
        Draft = 1,
        PendingCustomerApproval = 2,
        PendingManagerApproval = 3,
        Completed = 4,
        Cancelled = 5,
        Billed = 6
    }
    
    /// <summary>
    /// Kontrol listesi öğesi
    /// </summary>
    public class ChecklistItem
    {
        /// <summary>
        /// Öğe metni
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Öğe tamamlandı mı?
        /// </summary>
        public bool IsChecked { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Sıra numarası
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// Sözlük formatına dönüştürme
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "text", Text },
                { "isChecked", IsChecked },
                { "notes", Notes ?? string.Empty },
                { "order", Order },
                { "timestamp", DateTime.Now }
            };
        }
    }
} 