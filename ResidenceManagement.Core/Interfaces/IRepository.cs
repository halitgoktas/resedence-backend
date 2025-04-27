using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Interfaces
{
    // Generic Repository arayüzü
    public interface IRepository<T> where T : BaseEntity
    {
        // Tüm varlıkları getir
        Task<IReadOnlyList<T>> GetAllAsync();
        
        // Filtrelenmiş tüm varlıkları getir (predicate parametresi ile)
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        
        // Filtrelenmiş tüm varlıkları getir
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        
        // Filtrelenmiş ve sıralanmış varlıkları getir
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);
        
        // Birden fazla include (ilişkili entity) ile varlıkları getir
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            bool disableTracking = true);
        
        // Id ile varlık getir
        Task<T> GetByIdAsync(int id);
        
        // Tek bir varlık getir
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        
        // FindAsync - Filtrelenmiş varlıkları getir (UserService ve TokenService'de kullanılıyor)
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        // IQueryable döndür (sayfalama ve ileri sorgular için)
        IQueryable<T> GetQueryable();
        
        // Varlık ekle
        Task<T> AddAsync(T entity);
        
        // Varlık güncelle
        Task UpdateAsync(T entity);
        
        // Varlık sil
        Task DeleteAsync(T entity);
        
        // Birden fazla varlık ekle
        Task AddRangeAsync(IEnumerable<T> entities);
        
        // Birden fazla varlık sil
        Task DeleteRangeAsync(IEnumerable<T> entities);
        
        // Belirli bir alanın benzersiz olup olmadığını kontrol et
        Task<bool> IsUniqueAsync(Expression<Func<T, bool>> predicate);
        
        // Verilen koşula göre varlık sayısı
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        
        // Verilen koşula göre varlık var mı kontrol et
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        
        // Değişiklikleri kaydet
        Task<int> SaveChangesAsync();
    }
} 