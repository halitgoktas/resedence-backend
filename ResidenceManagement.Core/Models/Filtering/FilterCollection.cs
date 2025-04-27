using System;
using System.Collections.Generic;
using System.Linq;

namespace ResidenceManagement.Core.Models.Filtering
{
    /// <summary>
    /// Birden fazla filtreyi bir arada tutan koleksiyon
    /// </summary>
    public class FilterCollection
    {
        /// <summary>
        /// Filtreleme grubunda kullanılacak mantıksal operatör
        /// </summary>
        public enum LogicalOperator
        {
            /// <summary>
            /// AND operatörü: tüm filtreler karşılanmalı
            /// </summary>
            And,
            
            /// <summary>
            /// OR operatörü: filtrelerden en az biri karşılanmalı
            /// </summary>
            Or
        }
        
        /// <summary>
        /// Filtreleri bir arada tutan liste
        /// </summary>
        private readonly List<object> _filters = new List<object>();
        
        /// <summary>
        /// Filtreler arasındaki mantıksal operatör
        /// </summary>
        public LogicalOperator Operator { get; set; } = LogicalOperator.And;
        
        /// <summary>
        /// Filtreleri alır
        /// </summary>
        public IReadOnlyList<object> Filters => _filters.AsReadOnly();
        
        /// <summary>
        /// Filtre sayısı
        /// </summary>
        public int Count => _filters.Count;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public FilterCollection()
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operator">Filtreler arası mantıksal operatör</param>
        public FilterCollection(LogicalOperator @operator)
        {
            Operator = @operator;
        }
        
        /// <summary>
        /// Filtreye filtre ekler
        /// </summary>
        /// <typeparam name="T">Filtre değer tipi</typeparam>
        /// <param name="filter">Eklenecek filtre</param>
        /// <returns>Güncellenmiş filtre koleksiyonu</returns>
        public FilterCollection Add<T>(Filter<T> filter)
        {
            _filters.Add(filter ?? throw new ArgumentNullException(nameof(filter)));
            return this;
        }
        
        /// <summary>
        /// Filtreye başka bir filtre koleksiyonu ekler
        /// </summary>
        /// <param name="filterCollection">Eklenecek filtre koleksiyonu</param>
        /// <returns>Güncellenmiş filtre koleksiyonu</returns>
        public FilterCollection Add(FilterCollection filterCollection)
        {
            _filters.Add(filterCollection ?? throw new ArgumentNullException(nameof(filterCollection)));
            return this;
        }
        
        /// <summary>
        /// AND operatörlü filtre koleksiyonu oluşturur
        /// </summary>
        /// <param name="filters">İlk eklenen filtreler</param>
        /// <returns>Yeni filtre koleksiyonu</returns>
        public static FilterCollection And(params object[] filters)
        {
            var collection = new FilterCollection(LogicalOperator.And);
            foreach (var filter in filters)
            {
                collection._filters.Add(filter);
            }
            return collection;
        }
        
        /// <summary>
        /// OR operatörlü filtre koleksiyonu oluşturur
        /// </summary>
        /// <param name="filters">İlk eklenen filtreler</param>
        /// <returns>Yeni filtre koleksiyonu</returns>
        public static FilterCollection Or(params object[] filters)
        {
            var collection = new FilterCollection(LogicalOperator.Or);
            foreach (var filter in filters)
            {
                collection._filters.Add(filter);
            }
            return collection;
        }
    }
} 