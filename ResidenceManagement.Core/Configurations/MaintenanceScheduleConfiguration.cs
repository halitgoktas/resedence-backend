using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Core.Configurations
{
    // Bakım planı entity konfigürasyonları
    public class MaintenanceScheduleConfiguration : IEntityTypeConfiguration<MaintenanceSchedule>
    {
        public void Configure(EntityTypeBuilder<MaintenanceSchedule> builder)
        {
            // Temel alan konfigürasyonları
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Priority).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Notes).HasMaxLength(1000);

            // İlişki konfigürasyonları
            builder.HasMany(x => x.ChecklistItems)
                  .WithOne(x => x.MaintenanceSchedule)
                  .HasForeignKey(x => x.MaintenanceScheduleId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Multi-tenant yapısı için index
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });
        }
    }
} 