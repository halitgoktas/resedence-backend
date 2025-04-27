namespace ResidenceManagement.Core.Models.Sorting
{
    /// <summary>
    /// API sorgularında kullanılacak sıralama parametreleri modeli
    /// </summary>
    public class SortingParameters
    {
        /// <summary>
        /// Sıralama yapılacak alan adı
        /// </summary>
        public string SortBy { get; set; } = "Id";
        
        /// <summary>
        /// Sıralama yönü (true: artan, false: azalan)
        /// </summary>
        public bool SortAscending { get; set; } = true;
        
        /// <summary>
        /// Sıralama yönünü belirler
        /// </summary>
        public string SortDirection => SortAscending ? "asc" : "desc";
    }
} 