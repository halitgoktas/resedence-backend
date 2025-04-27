using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Infrastructure.Data.Configurations
{
    // MaintenanceSchedule entity'si için veritabanı konfigürasyonlarını içeren sınıf
    public class MaintenanceScheduleConfiguration : IEntityTypeConfiguration<MaintenanceSchedule>
    {
        public void Configure(EntityTypeBuilder<MaintenanceSchedule> builder)
        {
            builder.ToTable("MaintenanceSchedules");

            builder.HasKey(x => x.Id);

            // Temel özellikler
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.MaintenanceType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Priority)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.RecurrencePattern)
                .HasMaxLength(50);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.CompletionNotes)
                .HasMaxLength(1000);

            builder.Property(x => x.CancellationReason)
                .HasMaxLength(500);

            builder.Property(x => x.RequiredMaterials)
                .HasMaxLength(1000);

            builder.Property(x => x.RequiredTools)
                .HasMaxLength(1000);

            builder.Property(x => x.RequiredSkills)
                .HasMaxLength(500);

            builder.Property(x => x.PreMaintenanceChecklist)
                .HasMaxLength(1000);

            builder.Property(x => x.MaintenanceSteps)
                .HasMaxLength(2000);

            builder.Property(x => x.PostMaintenanceChecklist)
                .HasMaxLength(1000);

            builder.Property(x => x.SafetyPrecautions)
                .HasMaxLength(1000);

            builder.Property(x => x.EmergencyProcedures)
                .HasMaxLength(1000);

            builder.Property(x => x.CostCenter)
                .HasMaxLength(50);

            builder.Property(x => x.BudgetCode)
                .HasMaxLength(50);

            builder.Property(x => x.NotificationEmail)
                .HasMaxLength(100);

            builder.Property(x => x.NotificationSMS)
                .HasMaxLength(20);

            builder.Property(x => x.AssignedToUserName)
                .HasMaxLength(100);

            builder.Property(x => x.ServiceProviderName)
                .HasMaxLength(100);

            builder.Property(x => x.CompletedByUserName)
                .HasMaxLength(100);

            // İlişkiler
            builder.HasOne(x => x.Residence)
                .WithMany()
                .HasForeignKey(x => x.ResidenceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Block)
                .WithMany()
                .HasForeignKey(x => x.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Apartment)
                .WithMany()
                .HasForeignKey(x => x.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Equipment)
                .WithMany()
                .HasForeignKey(x => x.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedToUser)
                .WithMany()
                .HasForeignKey(x => x.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UpdatedByUser)
                .WithMany()
                .HasForeignKey(x => x.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CompletedByUser)
                .WithMany()
                .HasForeignKey(x => x.CompletedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tekrarlama ilişkisi
            builder.HasOne(x => x.ParentMaintenance)
                .WithMany(x => x.RecurrenceInstances)
                .HasForeignKey(x => x.ParentMaintenanceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Koleksiyonlar
            builder.HasMany(x => x.ChecklistItems)
                .WithOne(x => x.MaintenanceSchedule)
                .HasForeignKey(x => x.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Documents)
                .WithOne(x => x.MaintenanceSchedule)
                .HasForeignKey(x => x.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Logs)
                .WithOne(x => x.MaintenanceSchedule)
                .HasForeignKey(x => x.MaintenanceScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Çok kiracılı yapı için indeksler
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });
        }
    }
} 