using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Kullanıcı entity'si için veritabanı konfigürasyonlarını içeren sınıf
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.ToTable("Kullanicilar");

            builder.HasKey(x => x.Id);

            // Temel özellikler
            builder.Property(x => x.KullaniciAdi)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Telefon)
                .HasMaxLength(20);

            builder.Property(x => x.Ad)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Soyad)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Sifre)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SifreSalt)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.ProfilResimUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ResetPasswordToken)
                .HasMaxLength(255);

            // İlişkiler
            builder.HasOne(x => x.Firma)
                .WithMany()
                .HasForeignKey(x => x.FirmaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Sube)
                .WithMany()
                .HasForeignKey(x => x.SubeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Kullanıcı rolleri ilişkisi
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Kullanici)
                .HasForeignKey(x => x.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            // İndeksler
            // Kullanıcı adı ve e-posta için benzersiz indeksler
            builder.HasIndex(x => x.KullaniciAdi).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            
            // Telefon numarsı için indeks
            builder.HasIndex(x => x.Telefon);
            
            // Ad ve soyad için filtreleme/arama indeksleri
            builder.HasIndex(x => x.Ad);
            builder.HasIndex(x => x.Soyad);
            
            // Çok kiracılı yapı için indeksler
            builder.HasIndex(x => new { x.FirmaId, x.SubeId });
            
            // Son giriş tarihi üzerinde istatistikler için indeks
            builder.HasIndex(x => x.SonGirisTarihi);
            
            // Aktif kullanıcıları filtrelemek için indeks
            builder.HasIndex(x => x.IsActive);
        }
    }
}
