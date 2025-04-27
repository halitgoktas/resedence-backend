using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gider kategorilerini temsil eden sınıf
    public class ExpenseCategory
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Firma ve şube bilgileri (multi-tenant yapı için)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Kategori adı
        public string Name { get; set; }
        
        // Kategori açıklaması
        public string Description { get; set; }
        
        // Kategori kodu
        public string Code { get; set; }
        
        // Üst kategori ID (hiyerarşik yapı için)
        public int? ParentCategoryId { get; set; }
        public ExpenseCategory ParentCategory { get; set; }
        
        // Alt kategoriler
        public ICollection<ExpenseCategory> SubCategories { get; set; }
        
        // Bu kategoriye bağlı gider işlemleri
        public ICollection<ExpenseTransaction> ExpenseTransactions { get; set; }
        
        // Kategori türü
        public ExpenseCategoryType CategoryType { get; set; }
        
        // Muhasebe entegrasyonu için hesap kodu
        public string AccountCode { get; set; }
        
        // Bütçe entegrasyonu için bütçe kodu
        public string BudgetCode { get; set; }
        
        // Varsayılan dağıtım yöntemi
        public DistributionMethod DefaultDistributionMethod { get; set; }
        
        // Kategori durumu
        public bool IsActive { get; set; }
        
        // Kategori sırası
        public int SortOrder { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public ExpenseCategory()
        {
            SubCategories = new List<ExpenseCategory>();
            ExpenseTransactions = new List<ExpenseTransaction>();
            IsActive = true;
        }
    }
    
    // Gider kategori türleri
    public enum ExpenseCategoryType
    {
        // Genel giderler
        General = 0,
        
        // Işletme giderleri
        Operation = 1,
        
        // Bakım giderleri
        Maintenance = 2,
        
        // Onarım giderleri
        Repair = 3,
        
        // Personel giderleri
        Personnel = 4,
        
        // Vergi/harç giderleri
        Tax = 5,
        
        // Sigorta giderleri
        Insurance = 6,
        
        // Yatırım giderleri
        Investment = 7,
        
        // Diğer giderler
        Other = 8
    }
} 