using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Data.Repositories
{
    // Generic repository implementasyonu
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly int _firmaId;
        private readonly int _subeId;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            // Burada firma ve şube bilgileri kullanıcıdan veya konfigürasyondan alınacak
            _firmaId = 1; // Default değer, gerçek uygulamada değiştirilecek
            _subeId = 1; // Default değer, gerçek uygulamada değiştirilecek
            
            // DbContext'te firma ve şube filtreleme ayarlanır
            _dbContext.SetFirmaAndSubeId(_firmaId, _subeId);
        }

        public void SetFirmaAndSubeId(int firmaId, int subeId)
        {
            _dbContext.SetFirmaAndSubeId(firmaId, subeId);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                    string includeString = null, 
                                                    bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            
            if (disableTracking) 
                query = query.AsNoTracking();
            
            if (predicate != null) 
                query = query.Where(predicate);
            
            if (!string.IsNullOrWhiteSpace(includeString)) 
                query = query.Include(includeString);
            
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                    List<Expression<Func<T, object>>> includes = null, 
                                                    bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            
            if (disableTracking) 
                query = query.AsNoTracking();
            
            if (predicate != null) 
                query = query.Where(predicate);
            
            if (includes != null) 
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            
            return await query.ToListAsync();
        }

        public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, 
                                                                                Expression<Func<T, bool>> predicate = null, 
                                                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                                                List<Expression<Func<T, object>>> includes = null, 
                                                                                bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            
            if (disableTracking) 
                query = query.AsNoTracking();
            
            if (predicate != null) 
                query = query.Where(predicate);
            
            if (includes != null) 
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            
            var totalCount = await query.CountAsync();
            
            if (orderBy != null)
                query = orderBy(query);
            
            var items = await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            
            return (items, totalCount);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbContext.Set<T>().CountAsync();
            
            return await _dbContext.Set<T>().CountAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbContext.Set<T>().AnyAsync();
            
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T entity)
        {
            // Soft delete uygulanacak
            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            // Soft delete her birine uygulanacak
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            
            _dbContext.Set<T>().UpdateRange(entities);
            await Task.CompletedTask;
        }
    }
} 