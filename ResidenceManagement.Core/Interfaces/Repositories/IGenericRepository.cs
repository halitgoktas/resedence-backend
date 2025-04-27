using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Interfaces.Repositories
{
    // Generic repository arayüzü
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Tekil entity işlemleri
        Task<T> GetByIdAsync(int id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        
        // Çoklu entity işlemleri
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                         string includeString = null,
                                         bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                         List<Expression<Func<T, object>>> includes = null,
                                         bool disableTracking = true);
        
        // Sayfalama işlemleri
        Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, 
                                                 Expression<Func<T, bool>> predicate = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                 List<Expression<Func<T, object>>> includes = null,
                                                 bool disableTracking = true);
        
        // Sayım işlemleri
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        
        // Ekleme işlemleri
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        
        // Güncelleme işlemleri
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        
        // Silme işlemleri
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        
        // Firma ve şube filtreleme (multi-tenant)
        void SetFirmaAndSubeId(int firmaId, int subeId);
    }
} 