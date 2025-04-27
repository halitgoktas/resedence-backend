using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities.Financial
{
    /// <summary>
    /// Gider kategorileri sınıfı
    /// </summary>
    public class ExpenseCategory : BaseLookupEntity
    {
        /// <summary>
        /// Kategori kodu
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// Kategori açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Üst kategori ID (alt kategori ise)
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Kategorinin görüntülenme sırası
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Bütçe limiti
        /// </summary>
        public decimal? BudgetLimit { get; set; }

        /// <summary>
        /// Bütçe para birimi
        /// </summary>
        public string BudgetCurrency { get; set; } = "TRY";

        /// <summary>
        /// Sabit gider mi? (Her ay düzenli olarak ödenen)
        /// </summary>
        public bool IsFixedExpense { get; set; }

        /// <summary>
        /// İkon/Görsel URL
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Kategori renk kodu
        /// </summary>
        public string ColorCode { get; set; }

        // Navigation Properties
        /// <summary>
        /// Üst kategori
        /// </summary>
        public virtual ExpenseCategory ParentCategory { get; set; }

        /// <summary>
        /// Alt kategoriler
        /// </summary>
        public virtual ICollection<ExpenseCategory> SubCategories { get; set; }

        /// <summary>
        /// Bu kategorideki giderler
        /// </summary>
        public virtual ICollection<Expense> Expenses { get; set; }
    }
} 