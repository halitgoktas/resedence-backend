using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    /// <summary>
    /// Bakım dokümanı - bakım planıyla ilişkili belgeler
    /// </summary>
    public class MaintenanceDocument : BaseEntity
    {
        /// <summary>
        /// İlişkili bakım planı ID
        /// </summary>
        public int MaintenanceScheduleId { get; set; }
        
        /// <summary>
        /// İlişkili bakım planı
        /// </summary> 
        public virtual MaintenanceSchedule MaintenanceSchedule { get; set; }
        
        /// <summary>
        /// Belge başlığı
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Belge açıklaması
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Belge tipi
        /// </summary>
        public string DocumentType { get; set; }
        
        /// <summary>
        /// Dosya yolu
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Dosya tipi
        /// </summary>
        public string FileType { get; set; }
        
        /// <summary>
        /// Dosya boyutu (KB)
        /// </summary>
        public long? FileSize { get; set; }
        
        /// <summary>
        /// Yükleme tarihi
        /// </summary>
        public DateTime UploadDate { get; set; }
        
        /// <summary>
        /// Yükleyen kişi
        /// </summary>
        public string UploadedBy { get; set; }
        
        /// <summary>
        /// Yükleyen kullanıcı ID
        /// </summary>
        public int? UploadedByUserId { get; set; }
        
        /// <summary>
        /// Belge dosya adı
        /// </summary>
        public string DocumentName { get; set; }
        
        /// <summary>
        /// Multi-tenant için gerekli alanlar
        /// </summary>
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
    }
} 