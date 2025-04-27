// Bakım kontrol listesi öğesi için veritabanı konfigürasyonları
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Infrastructure.Data.Configurations
{
    /// <summary>
    /// MaintenanceChecklistItem entity'si için veritabanı konfigürasyonları
    /// </summary>
    public class MaintenanceChecklistItemConfiguration : IEntityTypeConfiguration<MaintenanceChecklistItem>
    {
        public void Configure(EntityTypeBuilder<MaintenanceChecklistItem> builder)
        {
            // Temel konfigürasyonlar
            builder.ToTable("MaintenanceChecklistItems");
            builder.HasKey(x => x.Id);

            // Alan konfigürasyonları
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.IsRequired)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.IsCompleted)
                .IsRequired();

            builder.Property(x => x.CompletedDate);
            
            // Planlanan tamamlanma tarihi
            builder.Property(x => x.CompletionDate);
            
            builder.Property(x => x.CompletedByUserId);
            
            builder.Property(x => x.OrderNumber)
                .IsRequired();
                
            // OrderIndex, OrderNumber ile aynı değeri kullanır. Veritabanında saklanmaz.
            builder.Ignore(x => x.OrderIndex);

            builder.Property(x => x.MaintenanceScheduleId)
                .IsRequired();

            // Multi-tenant yapısı için gerekli alanlar
            builder.Property(x => x.FirmaId)
                .IsRequired();
            
            builder.Property(x => x.SubeId)
                .IsRequired();

            // FirmaId ve SubeId için composite index
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });

            // İlişkiler
            builder.HasOne(x => x.MaintenanceSchedule)
                .WithMany(x => x.ChecklistItems)
                .HasForeignKey(x => x.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CompletedByUser)
                .WithMany()
                .HasForeignKey(x => x.CompletedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
} 