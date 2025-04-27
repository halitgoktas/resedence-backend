using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Hizmet talebi entity'si için veritabanı konfigürasyonlarını içeren sınıf
    /// </summary>
    public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.ToTable("ServiceRequests");

            builder.HasKey(x => x.Id);

            // Temel özellikler
            builder.Property(x => x.RequestNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Location)
                .HasMaxLength(200);

            builder.Property(x => x.ResolutionDescription)
                .HasMaxLength(1000);

            builder.Property(x => x.UserFeedback)
                .HasMaxLength(500);

            // İlişkiler
            builder.HasOne(x => x.RequestedBy)
                .WithMany()
                .HasForeignKey(x => x.RequestedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedTo)
                .WithMany()
                .HasForeignKey(x => x.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Apartment)
                .WithMany()
                .HasForeignKey(x => x.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Block)
                .WithMany()
                .HasForeignKey(x => x.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Koleksiyonlar
            builder.HasMany(x => x.Notes)
                .WithOne(n => n.ServiceRequest)
                .HasForeignKey(n => n.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Attachments)
                .WithOne(a => a.ServiceRequest)
                .HasForeignKey(a => a.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.History)
                .WithOne(h => h.ServiceRequest)
                .HasForeignKey(h => h.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // İndeksler
            
            // Talep numarası için benzersiz indeks
            builder.HasIndex(x => x.RequestNumber).IsUnique();
            
            // Durum ve öncelik için filtreleme indeksleri
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.Priority);
            
            // Talep zamanlaması için tarih indeksleri (sorgulamada sık kullanılan)
            builder.HasIndex(x => x.RequestDate);
            builder.HasIndex(x => x.AssignmentDate);
            builder.HasIndex(x => x.EstimatedResolutionDate);
            builder.HasIndex(x => x.ResolutionDate);
            builder.HasIndex(x => x.UserClosedDate);
            builder.HasIndex(x => x.ReminderDate);
            
            // Kullanıcı bazlı sorgulama indeksleri
            builder.HasIndex(x => x.RequestedById);
            builder.HasIndex(x => x.AssignedToId);
            
            // Kategori bazlı sorgulama indeksi
            builder.HasIndex(x => x.CategoryId);
            
            // Konum bazlı sorgulama indeksleri
            builder.HasIndex(x => x.ApartmentId);
            builder.HasIndex(x => x.BlockId);
            
            // Kullanıcı memnuniyet puanı için sorgulama indeksi
            builder.HasIndex(x => x.UserSatisfactionRating);
            
            // Kullanıcı kapatma durumu için sorgulama indeksi
            builder.HasIndex(x => x.IsClosedByUser);
            
            // Çok kiracılı yapı için indeksler
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });
            
            // Aktif talepleri filtrelemek için indeks
            builder.HasIndex(x => x.IsActive);
            
            // Bileşik indeksler - sık yapılan sorgular için optimize
            builder.HasIndex(x => new { x.Status, x.RequestDate });
            builder.HasIndex(x => new { x.Status, x.Priority });
            builder.HasIndex(x => new { x.RequestedById, x.Status });
            builder.HasIndex(x => new { x.AssignedToId, x.Status });
        }
    }
}
