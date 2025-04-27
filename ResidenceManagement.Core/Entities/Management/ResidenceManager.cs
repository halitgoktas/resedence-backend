using System;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Entities.Management
{
    // Site/Rezidans yöneticisi sınıfı
    public class ResidenceManager : BaseEntity
    {
        // İlişkili site/rezidans ID'si
        public int ResidenceId { get; set; }
        
        // Yönetici kullanıcı ID'si
        public int UserId { get; set; }
        
        // Yönetici tipi (1: Site Yöneticisi, 2: Blok Yöneticisi, 3: Yönetim Kurulu Üyesi, vb.)
        public int ManagerType { get; set; }
        
        // Başlangıç tarihi
        public DateTime StartDate { get; set; }
        
        // Bitiş tarihi (varsa)
        public DateTime? EndDate { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Yetki derecesi
        public int AuthorityLevel { get; set; }
        
        // İletişim numarası
        public string ContactNumber { get; set; }
        
        // E-posta adresi
        public string Email { get; set; }
        
        // İmza yetkilisi mi?
        public bool IsSignatory { get; set; }
        
        // Aktif mi?
        public bool IsCurrentlyActive { get; set; }
        
        // Navigation Properties
        // İlişkili site/rezidans
        public virtual Residence Residence { get; set; }
        
        // İlişkili kullanıcı
        public virtual User User { get; set; }
        
        public ResidenceManager()
        {
            StartDate = DateTime.Now;
            IsCurrentlyActive = true;
        }
    }
} 