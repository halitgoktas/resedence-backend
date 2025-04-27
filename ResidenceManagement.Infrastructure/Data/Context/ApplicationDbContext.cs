using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Entities.Base;
using ResidenceManagement.Core.Entities.Financial;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Property;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.Entities.Kbs;
using ResidenceManagement.Core.Models.Financial;
// Türkçe ve İngilizce sınıf çakışmasını önlemek için alias tanımlama
using PropertyApartment = ResidenceManagement.Core.Entities.Property.Apartment;
using PropertyBlock = ResidenceManagement.Core.Entities.Property.Block;
using ResidenceManagement.Infrastructure.Data.Configurations;

namespace ResidenceManagement.Infrastructure.Data.Context
{
    // Uygulama veritabanı bağlantısını sağlayan ve multi-tenant filtreleme yapan DbContext
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        private int _currentFirmaId;
        private int _currentSubeId;
        private bool _multiTenantFilterEnabled = true;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        // Entitiy DbSets - Türkçe isimlerini geri getiriyoruz
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<RolYetki> RolYetkiler { get; set; }
        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<Sube> Subeler { get; set; }
        public DbSet<ResidenceManagement.Core.Entities.Property.Apartment> Daireler { get; set; }
        public DbSet<ResidenceManagement.Core.Entities.Property.Block> Bloklar { get; set; }
        public DbSet<ApartmentResident> Sakinler { get; set; }
        public DbSet<Dues> Aidatlar { get; set; }
        public DbSet<ResidenceManagement.Core.Entities.Financial.Payment> Odemeler { get; set; }
        public DbSet<ResidenceManagement.Core.Entities.Financial.Expense> Giderler { get; set; }
        
        // Hizmet talepleri ilişkili DbSet'ler
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceNote> ServiceNotes { get; set; }
        public DbSet<ServiceAttachment> ServiceAttachments { get; set; }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
        
        // Bakım yönetimi için DbSet'ler
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<MaintenanceChecklistItem> MaintenanceChecklistItems { get; set; }
        public DbSet<MaintenanceDocument> MaintenanceDocuments { get; set; }
        public DbSet<MaintenanceChecklistItem> MaintenanceChecklists { get; set; }
        public DbSet<MaintenanceCost> MaintenanceCosts { get; set; }
        
        // KBS bildirimleri için DbSet
        public DbSet<KbsNotification> KbsNotifications { get; set; }

        // Multi-tenant için FirmaId ve SubeId değerlerini ayarla
        public void SetFirmaAndSubeId(int firmaId, int subeId)
        {
            _currentFirmaId = firmaId;
            _currentSubeId = subeId;
            _logger.LogDebug("Tenant bilgileri ayarlandı: FirmaId={FirmaId}, SubeId={SubeId}", firmaId, subeId);
        }
        
        // Multi-tenant filtrelemeyi etkinleştir/devre dışı bırak
        public void SetMultiTenantFilterEnabled(bool enabled)
        {
            _multiTenantFilterEnabled = enabled;
            _logger.LogDebug("Multi-tenant filtreleme: {Enabled}", enabled ? "Etkin" : "Devre Dışı");
        }

        // Model oluşturma sırasında çağrılır, veritabanı modelini konfigüre eder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Entity konfigürasyonlarını uygula
            ApplyConfigurations(modelBuilder);
            
            // Multi-tenant filtreleme için query filter'lar eklenir
            ApplyGlobalFilters(modelBuilder);
            
            // Removing redundant configuration
            // modelBuilder.ApplyConfiguration(new MaintenanceChecklistItemConfiguration());
        }
        
        // Multi-tenant için global filtreleri uygular
        private void ApplyGlobalFilters(ModelBuilder modelBuilder)
        {
            // Firma ve Sube tabloları için tenant filtreleri uygulanmaz
            
            // Diğer tüm multi-tenant entity'ler için global filtre uygula
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Multi-tenant özelliği olan entity'leri filtrele
                if (typeof(ITenant).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    
                    // FirmaId filtresi
                    var firmaProperty = Expression.Property(parameter, "FirmaId");
                    var firmaEqual = Expression.Equal(firmaProperty, Expression.Constant(_currentFirmaId));
                    
                    // SubeId filtresi (SubeId = 0 olan kayıtlar tüm şubelerde görünür)
                    var subeProperty = Expression.Property(parameter, "SubeId");
                    var subeZero = Expression.Equal(subeProperty, Expression.Constant(0));
                    var subeEqual = Expression.Equal(subeProperty, Expression.Constant(_currentSubeId));
                    var subeFilter = Expression.OrElse(subeZero, subeEqual);
                    
                    // Filtreleri birleştir
                    var filter = Expression.AndAlso(firmaEqual, subeFilter);
                    
                    // IsDeleted filtresini de ekle - eğer BaseEntity'den türüyorsa
                    if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                    {
                        var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                        var isDeletedFilter = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                        filter = Expression.AndAlso(filter, isDeletedFilter);
                    }
                    
                    // Multi-tenant filtresinin devre dışı olması durumu
                    var isEnabledProperty = Expression.Constant(_multiTenantFilterEnabled);
                    var isEnabled = Expression.Equal(isEnabledProperty, Expression.Constant(true));
                    var orElseFilter = Expression.OrElse(Expression.Not(isEnabled), filter);
                    
                    // Filtre lambda ifadesini oluştur ve uygula
                    var lambda = Expression.Lambda(orElseFilter, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    
                    _logger.LogDebug("Multi-tenant filtresi eklendi: {EntityType}", entityType.ClrType.Name);
                }
                // Sadece BaseEntity için soft-delete (IsDeleted) filtrelemesi
                else if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
                    var isDeletedFilter = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                    var lambda = Expression.Lambda(isDeletedFilter, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    
                    _logger.LogDebug("Soft-delete filtresi eklendi: {EntityType}", entityType.ClrType.Name);
                }
            }
        }
        
        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            // Tüm IEntityTypeConfiguration sınıflarını otomatik olarak uygula
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            
            // Rol ve Yetki ilişkileri
            modelBuilder.Entity<RolYetki>()
                .HasOne(ry => ry.Rol)
                .WithMany(r => r.RolYetkiler)
                .HasForeignKey(ry => ry.RolId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<RolYetki>()
                .HasOne(ry => ry.Yetki)
                .WithMany(y => y.RolYetkiler)
                .HasForeignKey(ry => ry.YetkiId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Hizmet talebi ilişkileri
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.RequestedBy)
                .WithMany()
                .HasForeignKey(sr => sr.RequestedById)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.AssignedTo)
                .WithMany()
                .HasForeignKey(sr => sr.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Category)
                .WithMany(sc => sc.ServiceRequests)
                .HasForeignKey(sr => sr.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Hizmet notları ilişkileri
            modelBuilder.Entity<ServiceNote>()
                .HasOne(sn => sn.ServiceRequest)
                .WithMany(sr => sr.Notes)
                .HasForeignKey(sn => sn.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Hizmet ekleri ilişkileri
            modelBuilder.Entity<ServiceAttachment>()
                .HasOne(sa => sa.ServiceRequest)
                .WithMany(sr => sr.Attachments)
                .HasForeignKey(sa => sa.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Hizmet tarihçesi ilişkileri
            modelBuilder.Entity<ServiceHistory>()
                .HasOne(sh => sh.ServiceRequest)
                .WithMany(sr => sr.History)
                .HasForeignKey(sh => sh.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Kategori hiyerarşisi
            modelBuilder.Entity<ServiceCategory>()
                .HasOne(sc => sc.ParentCategory)
                .WithMany(sc => sc.SubCategories)
                .HasForeignKey(sc => sc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Bakım yönetimi ilişkileri
            modelBuilder.Entity<MaintenanceLog>()
                .HasOne(ml => ml.MaintenanceSchedule)
                .WithMany(ms => ms.Logs)
                .HasForeignKey(ml => ml.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<MaintenanceChecklistItem>()
                .HasOne(mci => mci.MaintenanceSchedule)
                .WithMany(ms => ms.ChecklistItems)
                .HasForeignKey(mci => mci.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<MaintenanceDocument>()
                .HasOne(md => md.MaintenanceSchedule)
                .WithMany(ms => ms.Documents)
                .HasForeignKey(md => md.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        public override int SaveChanges()
        {
            UpdateMultiTenantEntities();
            return base.SaveChanges();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateMultiTenantEntities();
            return base.SaveChangesAsync(cancellationToken);
        }
        
        private void UpdateMultiTenantEntities()
        {
            // Tüm eklenen veya güncellenen entity'leri işle
            foreach (var entry in ChangeTracker.Entries())
            {
                // Multi-tenant entityleri için tenant bilgilerini ayarla
                if (entry.Entity is ITenant tenantEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        // Yeni kayıtlara tenant bilgisi ata
                        tenantEntity.FirmaId = _currentFirmaId;
                        tenantEntity.SubeId = _currentSubeId;
                        
                        _logger.LogDebug("Eklenen entity için tenant ayarlandı: {EntityType}, FirmaId={FirmaId}, SubeId={SubeId}", 
                            entry.Entity.GetType().Name, _currentFirmaId, _currentSubeId);
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        // Tenant ID'lerin güncellenmesini engelle
                        entry.Property(nameof(ITenant.FirmaId)).IsModified = false;
                        entry.Property(nameof(ITenant.SubeId)).IsModified = false;
                    }
                }
                
                // BaseEntity alanlarını güncelle
                if (entry.Entity is BaseEntity baseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        // Yeni kayıt oluşturma tarihini ata
                        baseEntity.CreatedDate = DateTime.Now;
                        
                        // IsActive ve IsDeleted alanlarını varsayılan değerlere ayarla
                        baseEntity.IsActive = true;
                        baseEntity.IsDeleted = false;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        // Güncelleme alanlarını değiştir
                        baseEntity.UpdatedDate = DateTime.Now;
                        
                        // Oluşturma tarihinin değiştirilmesini engelle
                        entry.Property(nameof(BaseEntity.CreatedDate)).IsModified = false;
                        entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    }
                }
            }
        }
        
        // Geçerli kullanıcı adını alır (HTTP context üzerinden)
        private int? GetCurrentUserName()
        {
            // User ID'yi bir provider üzerinden alabiliriz
            // Şu anda placeholder olarak 1 dönüyoruz, gerçek uygulamada bir ICurrentUserService kullanılmalı
            return 1;
        }
    }
} 