using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResidenceManagement.Core.Entities.Base;
// using ResidenceManagement.Core.Interfaces; // Ambiguity sorununu çözmek için kaldırıldı
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Data.Repositories
{
    // Unit of Work implementasyonu
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            // Repository zaten oluşturulmuşsa mevcut repository'i döndür
            if (_repositories.ContainsKey(typeof(T)))
                return (IGenericRepository<T>)_repositories[typeof(T)];

            // Repository henüz oluşturulmamışsa oluştur ve döndür
            var repository = new GenericRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            // Complete metodu ile aynı işi yapar, sadece farklı isimle
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CompleteWithTransaction()
        {
            if (_transaction == null)
            {
                BeginTransaction();
            }

            try
            {
                // Değişiklikleri kaydet
                var result = await _dbContext.SaveChangesAsync();

                // Transaction'ı commit et
                CommitTransaction();
                
                return result;
            }
            catch
            {
                // Hata durumunda rollback yap
                RollbackTransaction();
                throw;
            }
        }

        public void BeginTransaction()
        {
            // Eğer aktif bir transaction yoksa yeni bir transaction başlat
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                // Transaction'ı commit et
                _transaction?.Commit();
            }
            finally
            {
                // Transaction'ı dispose et
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            // Transaction'ı rollback yap
            _transaction?.Rollback();

            // Transaction'ı dispose et
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // Transaction'ı dispose et
                _transaction?.Dispose();
                
                // DbContext'i dispose et
                _dbContext.Dispose();
                
                _disposed = true;
            }
        }
    }
} 