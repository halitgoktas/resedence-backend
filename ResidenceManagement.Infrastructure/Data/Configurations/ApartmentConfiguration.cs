using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities.Property;

namespace ResidenceManagement.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Daire entity'si için veritabanı konfigürasyonlarını içeren sınıf
    /// </summary>
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Daireler");

            builder.HasKey(x => x.Id);

            // Temel özellikler
            builder.Property(x => x.ApartmentNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.ApartmentType)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.GrossArea)
                .HasPrecision(10, 2);

            builder.Property(x => x.NetArea)
                .HasPrecision(10, 2);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.HeatingType)
                .HasMaxLength(50);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.DuesAmount)
                .HasPrecision(12, 2);

            builder.Property(x => x.DuesCoefficient)
                .HasPrecision(6, 4);

            // İlişkiler
            builder.HasOne(x => x.Block)
                .WithMany(b => b.Apartments)
                .HasForeignKey(x => x.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // İndeksler
            
            // Blok ve daire numarası kombinasyonu benzersiz olmalı
            builder.HasIndex(x => new { x.BlockId, x.ApartmentNumber }).IsUnique();
            
            // Daire numarası için arama indeksi
            builder.HasIndex(x => x.ApartmentNumber);
            
            // Kat bazlı filtreleme için indeks
            builder.HasIndex(x => x.Floor);
            
            // Daire türü (1+1, 2+1 vb.) için filtreleme indeksi
            builder.HasIndex(x => x.ApartmentType);
            
            // Daire durumu için filtreleme indeksi
            builder.HasIndex(x => x.Status);
            
            // Mülkiyet tipi için filtreleme indeksi
            builder.HasIndex(x => x.OwnershipType);
            
            // Mülk sahibi ve kiracı bazlı indeksler
            builder.HasIndex(x => x.OwnerId);
            builder.HasIndex(x => x.TenantId);
            
            // Aidat tutarı ve katsayısı için filtreleme indeksleri
            builder.HasIndex(x => x.DuesAmount);
            builder.HasIndex(x => x.DuesCoefficient);
            
            // Son aidat ödeme tarihi için filtreleme indeksi
            builder.HasIndex(x => x.LastDuesPaymentDate);
            
            // Çok kiracılı yapı için indeksler
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });
            
            // Aktif daireleri filtrelemek için indeks
            builder.HasIndex(x => x.IsActive);
        }
    }
}
