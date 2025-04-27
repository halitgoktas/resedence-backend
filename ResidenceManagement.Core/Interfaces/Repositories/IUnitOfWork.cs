using System;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Interfaces.Repositories
{
    // Unit of Work arayüzü
    public interface IUnitOfWork : IDisposable
    {
        // Genel Repository erişimi
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        
        // Özel repository'ler (ihtiyaç duyulan repository'ler eklenebilir)
        
        // Transaction işlemleri
        Task<int> Complete();
        Task<int> CompleteWithTransaction();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
} 