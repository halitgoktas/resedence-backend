using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Entities.Common;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Entities.Property;
using ResidenceManagement.Core.Enums;
using ResidenceManagement.Infrastructure.Data.Context;
using ResidenceManagement.Infrastructure.Services;

namespace ResidenceManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Veritabanı seed data işlemlerini yöneten sınıf
    /// </summary>
    public static class SeedDataManager
    {
        /// <summary>
        /// Veritabanına temel verileri ekler
        /// </summary>
        public static async Task SeedDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
            var context = services.GetRequiredService<ApplicationDbContext>();

            try
            {
                logger.LogInformation("Veritabanı seed işlemi başlıyor...");
                
                // Admin kullanıcısı ve rolleri ekle
                await SeedAdminUserAndRoles(context, services, logger);
                
                // Temel konfigürasyon verilerini ekle (şirket bilgileri vb.)
                await SeedConfigurationData(context, logger);
                
                // Ortama bağlı seed işlemleri
                var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
                if (isDevelopment)
                {
                    await SeedTestData(context, logger);
                }
                
                logger.LogInformation("Veritabanı seed işlemi başarıyla tamamlandı.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Veritabanı seed işlemi sırasında bir hata oluştu!");
                throw;
            }
        }

        /// <summary>
        /// Admin kullanıcısı ve temel rolleri ekler
        /// </summary>
        private static async Task SeedAdminUserAndRoles(ApplicationDbContext context, IServiceProvider services, ILogger logger)
        {
            // Rolleri kontrol et ve gerekiyorsa ekle
            if (!await context.Roller.AnyAsync())
            {
                logger.LogInformation("Temel roller ekleniyor...");
                
                var roles = new List<Rol>
                {
                    new Rol { Ad = "SuperAdmin", Aciklama = "Sistem yöneticisi", DisplayOrder = 1 },
                    new Rol { Ad = "Admin", Aciklama = "Site yöneticisi", DisplayOrder = 2 },
                    new Rol { Ad = "Manager", Aciklama = "Site görevlisi", DisplayOrder = 3 },
                    new Rol { Ad = "Resident", Aciklama = "Site sakini", DisplayOrder = 4 },
                    new Rol { Ad = "Staff", Aciklama = "Personel", DisplayOrder = 5 },
                    new Rol { Ad = "Tenant", Aciklama = "Kiracı", DisplayOrder = 6 },
                    new Rol { Ad = "Guest", Aciklama = "Misafir kullanıcı", DisplayOrder = 7 }
                };
                
                await context.Roller.AddRangeAsync(roles);
                await context.SaveChangesAsync();
                
                logger.LogInformation($"{roles.Count} adet rol başarıyla eklendi.");
            }
            
            // Admin kullanıcısını kontrol et ve gerekiyorsa ekle
            if (!await context.Kullanicilar.AnyAsync(k => k.KullaniciAdi == "admin"))
            {
                logger.LogInformation("Admin kullanıcısı ekleniyor...");
                
                // Referans firma ve şube oluştur
                var firma = new Firma
                {
                    Name = "ResidenceManager",
                    TaxNumber = "1234567890",
                    Address = "İstanbul",
                    Phone = "02123456789",
                    Email = "info@residencemanager.com",
                    WebSite = "www.residencemanager.com",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                
                var sube = new Sube
                {
                    FirmaId = 1, // İlk firma ID
                    Name = "Merkez",
                    Address = "İstanbul",
                    Phone = "02123456789",
                    Email = "merkez@residencemanager.com",
                    ManagerName = "Admin User",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                
                await context.Firmalar.AddAsync(firma);
                await context.SaveChangesAsync();
                
                sube.FirmaId = firma.Id;
                await context.Subeler.AddAsync(sube);
                await context.SaveChangesAsync();
                
                // Admin kullanıcısı oluştur
                var salt = Guid.NewGuid().ToString();
                // Hash şifreyi oluştur
                var hashedPassword = HashPassword("Admin123!", salt);
                
                var adminUser = new Kullanici
                {
                    KullaniciAdi = "admin",
                    Email = "admin@residencemanager.com",
                    Telefon = "05001234567",
                    Ad = "Site",
                    Soyad = "Yöneticisi",
                    Sifre = hashedPassword,
                    SifreSalt = salt,
                    FirmaId = firma.Id,
                    SubeId = sube.Id,
                    IsActive = true,
                    EmailDogrulandiMi = true,
                    CreatedDate = DateTime.Now
                };
                
                await context.Kullanicilar.AddAsync(adminUser);
                await context.SaveChangesAsync();
                
                // Admin kullanıcısına SuperAdmin rolünü ata
                var superAdminRole = await context.Roller.FirstOrDefaultAsync(r => r.Ad == "SuperAdmin");
                if (superAdminRole != null)
                {
                    var userRole = new UserRole
                    {
                        KullaniciId = adminUser.Id,
                        RolId = superAdminRole.Id,
                        FirmaId = firma.Id,
                        SubeId = sube.Id,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };
                    
                    await context.Set<UserRole>().AddAsync(userRole);
                    await context.SaveChangesAsync();
                    
                    logger.LogInformation($"Admin kullanıcısına SuperAdmin rolü atandı.");
                }
                
                logger.LogInformation("Admin kullanıcısı başarıyla oluşturuldu.");
            }
        }

        /// <summary>
        /// Temel konfigürasyon verilerini ekler
        /// </summary>
        private static async Task SeedConfigurationData(ApplicationDbContext context, ILogger logger)
        {
            logger.LogInformation("Temel konfigürasyon verileri kontrol ediliyor...");
            
            // Hizmet kategorileri
            if (!await context.ServiceCategories.AnyAsync())
            {
                logger.LogInformation("Hizmet kategorileri ekleniyor...");
                
                var categories = new List<ServiceCategory>
                {
                    new ServiceCategory { Name = "Teknik Hizmetler", Description = "Elektrik, su, doğalgaz vb. arıza ve bakım talepleri", DisplayOrder = 1, IsActive = true, FirmaId = 1, SubeId = 1 },
                    new ServiceCategory { Name = "Temizlik Hizmetleri", Description = "Ortak alan temizlik talepleri", DisplayOrder = 2, IsActive = true, FirmaId = 1, SubeId = 1 },
                    new ServiceCategory { Name = "Güvenlik Hizmetleri", Description = "Güvenlik ile ilgili talepler", DisplayOrder = 3, IsActive = true, FirmaId = 1, SubeId = 1 },
                    new ServiceCategory { Name = "Peyzaj Hizmetleri", Description = "Bahçe ve çevre düzenleme talepleri", DisplayOrder = 4, IsActive = true, FirmaId = 1, SubeId = 1 },
                    new ServiceCategory { Name = "Asansör Hizmetleri", Description = "Asansör arıza ve bakım talepleri", DisplayOrder = 5, IsActive = true, FirmaId = 1, SubeId = 1 },
                    new ServiceCategory { Name = "Diğer Talepler", Description = "Diğer tüm talepler", DisplayOrder = 6, IsActive = true, FirmaId = 1, SubeId = 1 }
                };
                
                await context.ServiceCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
                
                logger.LogInformation($"{categories.Count} adet hizmet kategorisi başarıyla eklendi.");
            }
        }

        /// <summary>
        /// Development ortamı için test verilerini ekler
        /// </summary>
        private static async Task SeedTestData(ApplicationDbContext context, ILogger logger)
        {
            logger.LogInformation("Test verileri ekleniyor...");
            
            // Örnek bir site ve blok oluştur
            if (!await context.Bloklar.AnyAsync())
            {
                var residence = new Residence
                {
                    Name = "Test Rezidans",
                    Address = "Test Adres, İstanbul",
                    TotalBlocks = 2,
                    TotalApartments = 40,
                    IsActive = true,
                    FirmaId = 1,
                    SubeId = 1,
                    CreatedDate = DateTime.Now
                };
                
                await context.AddAsync(residence);
                await context.SaveChangesAsync();
                
                // Bloklar ekle - Property namespace'ini kullanarak
                var blocks = new List<ResidenceManagement.Core.Entities.Property.Block>
                {
                    new ResidenceManagement.Core.Entities.Property.Block 
                    { 
                        SiteId = residence.Id, 
                        BlockName = "A Blok", 
                        Description = "A Blok açıklaması", 
                        TotalFloors = 10, 
                        TotalApartments = 20,
                        IsActive = true,
                        FirmaId = 1,
                        SubeId = 1,
                        CreatedDate = DateTime.Now
                    },
                    new ResidenceManagement.Core.Entities.Property.Block 
                    { 
                        SiteId = residence.Id, 
                        BlockName = "B Blok", 
                        Description = "B Blok açıklaması", 
                        TotalFloors = 10, 
                        TotalApartments = 20,
                        IsActive = true,
                        FirmaId = 1,
                        SubeId = 1,
                        CreatedDate = DateTime.Now
                    }
                };
                
                await context.Bloklar.AddRangeAsync(blocks);
                await context.SaveChangesAsync();
                
                logger.LogInformation($"Test site ve {blocks.Count} adet blok eklendi.");
                
                // Örnek daireler oluştur
                var apartments = new List<ResidenceManagement.Core.Entities.Property.Apartment>();
                
                foreach (var block in blocks)
                {
                    var apartmentsForBlock = new List<ResidenceManagement.Core.Entities.Property.Apartment>();
                    for (int floor = 1; floor <= 10; floor++)
                    {
                        for (int unit = 1; unit <= 2; unit++)
                        {
                            string apartmentNo = $"{floor}{unit.ToString().PadLeft(2, '0')}";
                            
                            apartmentsForBlock.Add(new ResidenceManagement.Core.Entities.Property.Apartment
                            {
                                BlockId = block.Id,
                                ApartmentNumber = apartmentNo,
                                Floor = floor,
                                ApartmentType = unit == 1 ? "2+1" : "3+1",
                                GrossArea = unit == 1 ? 100 : 120,
                                NetArea = unit == 1 ? 85 : 105,
                                NumberOfRooms = unit == 1 ? 2 : 3,
                                NumberOfLivingRooms = 1,
                                NumberOfBathrooms = unit == 1 ? 1 : 2,
                                NumberOfBalconies = 1,
                                Status = ApartmentStatus.Vacant,
                                OwnershipType = OwnershipType.Individual,
                                Description = $"{block.BlockName} - {apartmentNo} no'lu daire",
                                DuesAmount = unit == 1 ? 500 : 600,
                                DuesCoefficient = 1.0m,
                                HeatingType = "Merkezi",
                                IsActive = true,
                                FirmaId = 1,
                                SubeId = 1,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    block.Apartments = apartmentsForBlock;
                }
                
                await context.Daireler.AddRangeAsync(apartments);
                await context.SaveChangesAsync();
                
                logger.LogInformation($"{apartments.Count} adet test daire eklendi.");
            }
            
            // Test kullanıcılar ekle
            if (await context.Kullanicilar.CountAsync() < 5) // Admin dışında başka kullanıcı yoksa
            {
                var salt = Guid.NewGuid().ToString();
                // Hash şifreyi oluştur
                var hashedPassword = HashPassword("Test123!", salt);
                
                var testUsers = new List<Kullanici>
                {
                    new Kullanici
                    {
                        KullaniciAdi = "yonetici",
                        Email = "yonetici@test.com",
                        Telefon = "05001234567",
                        Ad = "Site",
                        Soyad = "Yöneticisi",
                        Sifre = hashedPassword,
                        SifreSalt = salt,
                        FirmaId = 1,
                        SubeId = 1,
                        IsActive = true,
                        EmailDogrulandiMi = true,
                        CreatedDate = DateTime.Now
                    },
                    new Kullanici
                    {
                        KullaniciAdi = "sakin1",
                        Email = "sakin1@test.com",
                        Telefon = "05002345678",
                        Ad = "Test",
                        Soyad = "Sakini",
                        Sifre = hashedPassword,
                        SifreSalt = salt,
                        FirmaId = 1,
                        SubeId = 1,
                        IsActive = true,
                        EmailDogrulandiMi = true,
                        CreatedDate = DateTime.Now
                    },
                    new Kullanici
                    {
                        KullaniciAdi = "teknik",
                        Email = "teknik@test.com",
                        Telefon = "05003456789",
                        Ad = "Teknik",
                        Soyad = "Personel",
                        Sifre = hashedPassword,
                        SifreSalt = salt,
                        FirmaId = 1,
                        SubeId = 1,
                        IsActive = true,
                        EmailDogrulandiMi = true,
                        CreatedDate = DateTime.Now
                    }
                };
                
                await context.Kullanicilar.AddRangeAsync(testUsers);
                await context.SaveChangesAsync();
                
                // Kullanıcılara rol atama
                var adminRole = await context.Roller.FirstOrDefaultAsync(r => r.Ad == "Admin");
                var residentRole = await context.Roller.FirstOrDefaultAsync(r => r.Ad == "Resident");
                var staffRole = await context.Roller.FirstOrDefaultAsync(r => r.Ad == "Staff");
                
                if (adminRole != null && residentRole != null && staffRole != null)
                {
                    var userRoles = new List<UserRole>
                    {
                        new UserRole
                        {
                            KullaniciId = testUsers[0].Id, // yonetici
                            RolId = adminRole.Id,
                            FirmaId = 1,
                            SubeId = 1,
                            IsActive = true,
                            CreatedDate = DateTime.Now
                        },
                        new UserRole
                        {
                            KullaniciId = testUsers[1].Id, // sakin1
                            RolId = residentRole.Id,
                            FirmaId = 1,
                            SubeId = 1,
                            IsActive = true,
                            CreatedDate = DateTime.Now
                        },
                        new UserRole
                        {
                            KullaniciId = testUsers[2].Id, // teknik
                            RolId = staffRole.Id,
                            FirmaId = 1,
                            SubeId = 1,
                            IsActive = true,
                            CreatedDate = DateTime.Now
                        }
                    };
                    
                    await context.Set<UserRole>().AddRangeAsync(userRoles);
                    await context.SaveChangesAsync();
                }
                
                logger.LogInformation($"{testUsers.Count} adet test kullanıcı eklendi.");
            }
            
            // Örnek servis talepleri ekle
            if (!await context.ServiceRequests.AnyAsync())
            {
                var users = await context.Kullanicilar.ToListAsync();
                var apartments = await context.Daireler.Take(5).ToListAsync();
                var categories = await context.ServiceCategories.ToListAsync();
                
                var requests = new List<ServiceRequest>();
                var statuses = Enum.GetValues(typeof(ServiceRequestStatus)).Cast<ServiceRequestStatus>().ToList();
                
                for (int i = 0; i < 10; i++)
                {
                    var user = users[i % users.Count];
                    var apartment = apartments[i % apartments.Count];
                    var category = categories[i % categories.Count];
                    var status = statuses[i % statuses.Count];
                    
                    requests.Add(new ServiceRequest
                    {
                        RequestNumber = $"SR-{DateTime.Now.Year}-{i + 1:D4}",
                        Title = $"Test Talep #{i + 1}",
                        Description = $"Bu bir test talebidir #{i + 1}.",
                        Status = status,
                        Priority = (PriorityLevel)(i % 3 + 1),
                        RequestedById = user.Id,
                        RequestDate = DateTime.Now.AddDays(-i),
                        CategoryId = category.Id,
                        ApartmentId = apartment.Id,
                        BlockId = apartment.BlockId,
                        Location = $"{apartment.Block?.BlockName} - Daire {apartment.ApartmentNumber}",
                        FirmaId = 1,
                        SubeId = 1,
                        IsActive = true,
                        CreatedDate = DateTime.Now.AddDays(-i)
                    });
                }
                
                await context.ServiceRequests.AddRangeAsync(requests);
                await context.SaveChangesAsync();
                
                logger.LogInformation($"{requests.Count} adet test servis talebi eklendi.");
            }
            
            logger.LogInformation("Test verileri başarıyla eklendi.");
        }

        /// <summary>
        /// Şifreyi hashler
        /// </summary>
        private static string HashPassword(string password, string salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var saltedPassword = string.Concat(password, salt);
                var saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
                var hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
