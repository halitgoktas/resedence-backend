using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Property;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Entities
{
    /// <summary>
    /// Hizmet taleplerini temsil eden entity sınıfı
    /// </summary>
    public class ServiceRequest : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// Talep numarası
        /// </summary>
        public string RequestNumber { get; set; }

        /// <summary>
        /// Talep başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Talep açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Talebin durumu
        /// </summary>
        public ServiceRequestStatus Status { get; set; }

        /// <summary>
        /// Talebin öncelik seviyesi
        /// </summary>
        public PriorityLevel Priority { get; set; }

        /// <summary>
        /// Talebi oluşturan kullanıcı ID
        /// </summary>
        public int RequestedById { get; set; }

        /// <summary>
        /// Talep oluşturulma tarihi
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Talebin atandığı personel ID
        /// </summary>
        public int? AssignedToId { get; set; }

        /// <summary>
        /// Atama tarihi
        /// </summary>
        public DateTime? AssignmentDate { get; set; }

        /// <summary>
        /// Tahmini çözüm tarihi
        /// </summary>
        public DateTime? EstimatedResolutionDate { get; set; }

        /// <summary>
        /// Çözüm tarihi
        /// </summary>
        public DateTime? ResolutionDate { get; set; }

        /// <summary>
        /// Talep kategorisi ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// İlgili daire ID
        /// </summary>
        public int? ApartmentId { get; set; }

        /// <summary>
        /// İlgili blok ID
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Konum bilgisi
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Çözüm açıklaması
        /// </summary>
        public string ResolutionDescription { get; set; }

        /// <summary>
        /// Kullanıcı tarafından talep kapatıldı mı?
        /// </summary>
        public bool IsClosedByUser { get; set; }

        /// <summary>
        /// Kullanıcı kapama tarihi
        /// </summary>
        public DateTime? UserClosedDate { get; set; }

        /// <summary>
        /// Kullanıcı memnuniyet puanı (1-5)
        /// </summary>
        public int? UserSatisfactionRating { get; set; }

        /// <summary>
        /// Kullanıcı yorumu
        /// </summary>
        public string UserFeedback { get; set; }

        /// <summary>
        /// Hatırlatma tarihi
        /// </summary>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// İlişkiler
        /// </summary>
        public virtual User RequestedBy { get; set; }
        public virtual User AssignedTo { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual Block Block { get; set; }
        public virtual ServiceCategory Category { get; set; }
        public virtual ICollection<ServiceNote> Notes { get; set; }
        public virtual ICollection<ServiceAttachment> Attachments { get; set; }
        public virtual ICollection<ServiceHistory> History { get; set; }

        public ServiceRequest()
        {
            Notes = new HashSet<ServiceNote>();
            Attachments = new HashSet<ServiceAttachment>();
            History = new HashSet<ServiceHistory>();
            RequestDate = DateTime.Now;
            Status = ServiceRequestStatus.Open;
            Priority = PriorityLevel.Medium;
            IsClosedByUser = false;
        }
    }

    /// <summary>
    /// Hizmet talebi notlarını temsil eden entity
    /// </summary>
    public class ServiceNote : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// Hizmet talebi ID
        /// </summary>
        public int ServiceRequestId { get; set; }

        /// <summary>
        /// Not içeriği
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Notu oluşturan kullanıcı ID
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Not tarihi
        /// </summary>
        public DateTime NoteDate { get; set; }

        /// <summary>
        /// Not özel mi? (Sadece personel görebilir)
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// İlişkiler
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; }
        public virtual User CreatedBy { get; set; }
    }

    /// <summary>
    /// Hizmet talebi eklerini temsil eden entity
    /// </summary>
    public class ServiceAttachment : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// Hizmet talebi ID
        /// </summary>
        public int ServiceRequestId { get; set; }

        /// <summary>
        /// Dosya adı
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Dosya yolu
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Dosya türü
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Dosya boyutu (byte)
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Yükleme tarihi
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Yükleyen kullanıcı ID
        /// </summary>
        public int UploadedById { get; set; }

        /// <summary>
        /// İlişkiler
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; }
        public virtual User UploadedBy { get; set; }
    }

    /// <summary>
    /// Hizmet talebi durum geçmişini temsil eden entity
    /// </summary>
    public class ServiceHistory : ResidenceManagement.Core.Entities.Base.BaseEntity
    {
        /// <summary>
        /// Hizmet talebi ID
        /// </summary>
        public int ServiceRequestId { get; set; }

        /// <summary>
        /// Önceki durum
        /// </summary>
        public ServiceRequestStatus OldStatus { get; set; }

        /// <summary>
        /// Yeni durum
        /// </summary>
        public ServiceRequestStatus NewStatus { get; set; }

        /// <summary>
        /// Değişiklik tarihi
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Değişikliği yapan kullanıcı ID
        /// </summary>
        public int ChangedById { get; set; }

        /// <summary>
        /// Değişiklik sebebi
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// İlişkiler
        /// </summary>
        public virtual ServiceRequest ServiceRequest { get; set; }
        public virtual User ChangedBy { get; set; }
    }
} 