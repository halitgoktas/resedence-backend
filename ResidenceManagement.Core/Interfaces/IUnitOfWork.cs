using System;
using System.Threading.Tasks;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Interfaces.Repositories;

namespace ResidenceManagement.Core.Interfaces
{
    // Unit of Work arayüzü - tüm repository'leri tek bir transaction üzerinden yönetmek için
    public interface IUnitOfWork : IDisposable
    {
        // Tüm Bakım Planı ile ilgili repolar
        IRepository<Core.Entities.MaintenanceSchedule> MaintenanceSchedules { get; }
        IRepository<Core.Entities.MaintenanceChecklistItem> MaintenanceChecklists { get; }
        IRepository<Core.Entities.MaintenanceDocument> MaintenanceDocuments { get; }
        IRepository<Core.Entities.MaintenanceLog> MaintenanceLogs { get; }
        IRepository<Core.Entities.MaintenanceCost> MaintenanceCosts { get; }
        
        // Tüm ekipman ile ilgili repolar
        IRepository<Core.Entities.Equipment> Equipments { get; }
        
        // Diğer entity'ler için repository'ler
        // ...
        
        // Transaction yönetimi
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
} 