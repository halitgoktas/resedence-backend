using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces.Logging;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Infrastructure.Data.Repositories;

namespace ResidenceManagement.Infrastructure.Data
{
    /// <summary>
    /// UnitOfWork implementasyonu
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IPerformanceMetricsLoggingService _performanceLogger;

        /// <summary>
        /// UnitOfWork constructor
        /// </summary>
        /// <param name="dbContext">DB context</param>
        /// <param name="logger">Logger</param>
        /// <param name="performanceLogger">Performans loglama servisi</param>
        public UnitOfWork(
            ApplicationDbContext dbContext,
            ILogger<UnitOfWork> logger,
            IPerformanceMetricsLoggingService performanceLogger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _performanceLogger = performanceLogger ?? throw new ArgumentNullException(nameof(performanceLogger));
        }

        /// <inheritdoc/>
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_dbContext);
        }

        /// <inheritdoc/>
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> CompleteWithTransaction()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.BeginTransaction();
            }
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            var transaction = _dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch
                {
                    RollbackTransaction();
                    throw;
                }
            }
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            var transaction = _dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
} 