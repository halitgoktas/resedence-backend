# Rezidans ve Site Yönetim Sistemi - Teknik Dokümantasyon

## 1. Proje Genel Bakış

Rezidans ve Site Yönetim Sistemi, modern rezidans ve site yönetimi ihtiyaçlarına yönelik geliştirilmiş kapsamlı bir platformdur. Bu sistem, multi-tenant (çok kiracılı) bir mimari kullanarak birden fazla firma ve şube için hizmet vermektedir. Temel amacı, site yöneticileri, sakinler ve yönetim kurulları için etkili, güvenli ve kullanıcı dostu bir yönetim çözümü sunmaktır.

### 1.1. Teknoloji Yığını

- **Backend**: .NET 8 Web API, C#
- **ORM**: Entity Framework Core
- **Veritabanı**: MSSQL
- **Kimlik Doğrulama**: JWT Token
- **Frontend**: React.js ve Material UI
- **Mobil**: React Native (Geliştirilme Aşamasında)

### 1.2. Proje Mimari Yapısı

Proje, Clean Architecture prensiplerini temel alarak N-katmanlı mimari kullanmaktadır:

```
[Sunum Katmanı]        ResidenceManagement.API
         ↓
[Uygulama Katmanı]     ResidenceManagement.Application
         ↓
[Servis Katmanı]       ResidenceManagement.Services
         ↓
[Domain Katmanı]       ResidenceManagement.Core
         ↑
[Veri Erişim Katmanı]  ResidenceManagement.Data
         ↑
[Altyapı Katmanı]      ResidenceManagement.Infrastructure
```

## 2. Klasör Yapısı ve Organizasyon

### 2.1. Proje Klasör Yapısı

```
Backend/
├── src/
│   ├── ResidenceManagement.API/         # API kontrollerleri ve konfigürasyonu
│   ├── ResidenceManagement.Application/ # İş mantığı servisleri
│   ├── ResidenceManagement.Core/        # Domain modelleri, enums, interfaces
│   ├── ResidenceManagement.Data/        # Veritabanı erişim katmanı
│   ├── ResidenceManagement.Infrastructure/ # Harici servisler ve utilities
│   ├── ResidenceManagement.Services/    # Ortak servisler
├── tests/                              # Test projeleri
└── ResidenceManagement.sln             # Solution dosyası
```

### 2.2. ResidenceManagement.Core - Modeller ve Arayüzler

Core katmanı, aşağıdaki temel bileşenleri içerir:
- Domain modelleri
- Enums
- Interfaces
- Base sınıflar
- DTOs (Data Transfer Objects)
- Validasyon kuralları

#### 2.2.1. Model Klasör Yapısı
```
Models/
├── Base/               # Temel entity sınıfları
├── Auth/               # Kimlik doğrulama modelleri
├── Building/           # Bina ve daire modelleri
├── Transaction/        # İşlem modelleri
├── Identity/           # Kullanıcı ve rol modelleri
├── Site/               # Site modelleri
├── Resident/           # Sakin modelleri
├── System/             # Sistem modelleri
├── Setting/            # Ayar modelleri
└── Integration/        # Entegrasyon modelleri
```

## 3. Temel Model Yapısı

### 3.1. Base Modeller

Tüm model sınıfları için temel yapıyı sağlayan abstract sınıflar:

#### 3.1.1. BaseEntity
```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public int FirmaId { get; set; }
    public int SubeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
```

#### 3.1.2. BaseLookupEntity
Tanım (lookup) tabloları için temel sınıf:
```csharp
public abstract class BaseLookupEntity : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DisplayOrder { get; set; }
}
```

#### 3.1.3. BaseTransactionEntity
İşlem tabloları için temel sınıf:
```csharp
public abstract class BaseTransactionEntity : BaseEntity
{
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string Notes { get; set; }
}
```

### 3.2. Önemli Domain Modelleri

#### 3.2.1. Site Modeli
```csharp
public class Site : BaseLookupEntity
{
    // Site temel özellikleri
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public decimal TotalArea { get; set; }
    public int NumberOfBuildings { get; set; }
    public int NumberOfApartments { get; set; }
    
    // Site özellikleri
    public bool HasPool { get; set; }
    public bool HasGym { get; set; }
    public bool HasSecurity { get; set; }
    
    // Site yönetim bilgileri
    public string ManagerName { get; set; }
    public string ManagerPhone { get; set; }
    public string ManagerEmail { get; set; }
    
    // İlişkiler
    public virtual ICollection<Building> Buildings { get; set; }
}
```

#### 3.2.2. Building Modeli
```csharp
public class Building : BaseLookupEntity
{
    // Bina özellikleri
    public int SiteId { get; set; }
    public string BlockName { get; set; }
    public int NumberOfFloors { get; set; }
    public int NumberOfApartments { get; set; }
    
    // Teknik özellikler
    public bool HasElevator { get; set; }
    public bool HasGenerator { get; set; }
    public string HeatingSystem { get; set; }
    
    // İlişkiler
    public virtual Site Site { get; set; }
    public virtual ICollection<Block> Blocks { get; set; }
    public virtual ICollection<Apartment> Apartments { get; set; }
}
```

#### 3.2.3. Apartment Modeli
```csharp
public class Apartment : BaseEntity
{
    // Daire özellikleri
    public int BlockId { get; set; }
    public int ApartmentNumber { get; set; }
    public int Floor { get; set; }
    public string ApartmentType { get; set; }
    public decimal GrossArea { get; set; }
    public decimal? NetArea { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfLivingRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public int? NumberOfBalconies { get; set; }
    
    // Durum bilgileri
    public ApartmentStatus Status { get; set; }
    public OwnershipType OwnershipType { get; set; }
    public int? OwnerId { get; set; }
    public int? TenantId { get; set; }
    
    // Finansal bilgiler
    public decimal? DuesAmount { get; set; }
    public decimal DuesCoefficient { get; set; }
    public DateTime? LastDuesPaymentDate { get; set; }
    
    // Diğer bilgiler
    public string Description { get; set; }
    public string HeatingType { get; set; }
    public string Notes { get; set; }
    
    // İlişkiler
    public virtual Block Block { get; set; }
    public virtual ICollection<ApartmentOwner> Owners { get; set; }
    public virtual ICollection<ApartmentResident> Residents { get; set; }
}
```

#### 3.2.4. User Modeli
```csharp
public class User : BaseEntity
{
    // Kullanıcı bilgileri
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    // Güvenlik bilgileri
    public bool EmailConfirmed { get; set; }
    public bool PhoneConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    
    // İlişkiler
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
```

## 4. API Yapısı ve Controller'lar

ResidenceManagement.API, RESTful API prensiplerini takip eden bir yapıya sahiptir. Tüm controller'lar `BaseController` sınıfından türetilmiştir.

### 4.1. BaseController
Tüm controller'lar için ortak fonksiyonalite sağlayan temel controller:

```csharp
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    
    // Kullanıcı ve firma bilgisi alma yöntemleri
    protected int GetCurrentUserId()
    protected int GetCurrentFirmaId()
    protected int GetCurrentSubeId()
    
    // Standart API yanıtları
    protected IActionResult Success<T>(T data, string message = "İşlem başarılı")
    protected IActionResult Error(string message, int statusCode = 400)
    protected IActionResult NotFound(string message = "Kayıt bulunamadı")
    protected IActionResult Unauthorized(string message = "Yetkisiz erişim")
}
```

### 4.2. Önemli Controller'lar

#### 4.2.1. SiteController
```csharp
[Route("api/[controller]")]
[ApiController]
public class SiteController : BaseController
{
    private readonly ISiteService _siteService;
    
    // CRUD operasyonları
    [HttpGet]
    public async Task<IActionResult> GetAll()
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    
    [HttpPost]
    public async Task<IActionResult> Create(SiteCreateDto dto)
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SiteUpdateDto dto)
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
}
```

#### 4.2.2. BuildingController
Benzer şekilde, bina yönetimi için CRUD operasyonları sunar.

#### 4.2.3. ApartmentController
Daire yönetimi için API endpoint'leri.

#### 4.2.4. UserController
Kullanıcı yönetimi işlemleri:
```csharp
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    // Kullanıcı listeleme, ekleme, güncelleme, silme
    // Kullanıcı rollerini yönetme
    // Kullanıcı durumunu değiştirme (aktif/pasif)
}
```

#### 4.2.5. FinancialController
Finansal işlemler için API endpoint'leri:
```csharp
[Route("api/[controller]")]
[ApiController]
public class FinancialController : BaseController
{
    // Aidat yönetimi
    // Gider yönetimi
    // Ödeme işlemleri
    // Finansal raporlar
}
```

## 5. Servis Katmanı

### 5.1. Temel Servis Yapısı

Servisler, iş mantığını içeren ve repository'ler ile controller'lar arasında aracı olan katmandır. 

#### 5.1.1. Service Interface'leri
```csharp
public interface IUserService
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(UserCreateDto dto);
    Task<User> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
}
```

#### 5.1.2. Service Implementasyonları
```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    // Interface metotlarının implementasyonu
    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    
    // Diğer metotlar...
}
```

### 5.2. Önemli Servisler

#### 5.2.1. AuthService
Kimlik doğrulama ve yetkilendirme işlemleri:
```csharp
public class AuthService : IAuthService
{
    // Kullanıcı girişi
    // Token oluşturma
    // Şifre sıfırlama
    // E-posta doğrulama
}
```

#### 5.2.2. FinancialService
Finansal işlemler:
```csharp
public class FinancialService : IFinancialService
{
    // Aidat hesaplama
    // Ödeme işlemleri
    // Borç/alacak takibi
}
```

#### 5.2.3. AccessService
Erişim kontrolü:
```csharp
public class AccessService : IAccessService
{
    // Kullanıcı yetkilendirme
    // Rol bazlı erişim kontrolü
    // RFID kart yönetimi
}
```

## 6. Veritabanı Yapısı ve Repository Pattern

### 6.1. DbContext Yapısı
```csharp
public class AppDbContext : DbContext
{
    // DbSet'ler
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    // Diğer tablolar...
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity konfigürasyonları
        // İlişki tanımlamaları
        // Index tanımlamaları
        
        // Multi-tenant filtreleme
        modelBuilder.Entity<BaseEntity>().HasQueryFilter(e => !e.IsDeleted);
    }
}
```

### 6.2. Repository Pattern

#### 6.2.1. Generic Repository
```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> SaveChangesAsync();
}
```

#### 6.2.2. Entity-Specific Repository
```csharp
public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetByRoleAsync(int roleId);
}
```

## 7. Multi-tenant Yapı

Sistem, multi-tenant bir mimari kullanmaktadır. Her kayıt, şu alanları içerir:

- **FirmaId**: Kaydın ait olduğu firma
- **SubeId**: Kaydın ait olduğu şube

Bu yapı sayesinde:
- Her firma kendi verilerini ayrı olarak yönetebilir
- Bir firmanın birden fazla şubesi olabilir
- Sorgular, kullanıcının bağlı olduğu firma ve şubeye göre filtrelenir

## 8. Güvenlik ve Kimlik Doğrulama

### 8.1. JWT Tabanlı Kimlik Doğrulama
```csharp
public class JwtService : IJwtService
{
    // Token oluşturma
    public string GenerateToken(User user, IList<string> roles)
    
    // Token doğrulama
    public ClaimsPrincipal ValidateToken(string token)
    
    // Token yenileme
    public string RefreshToken(string token)
}
```

### 8.2. İki Faktörlü Kimlik Doğrulama
```csharp
public class TwoFactorAuthService : ITwoFactorAuthService
{
    // İki faktörlü doğrulama kodunu oluşturma
    public string GenerateTwoFactorCode(User user)
    
    // Doğrulama kodunu doğrulama
    public bool ValidateTwoFactorCode(User user, string code)
}
```

## 9. Validasyon Yapısı

Sistem, FluentValidation kütüphanesini kullanarak veri doğrulama işlemlerini gerçekleştirir:

```csharp
public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\+?[0-9]{10,15}$");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
    }
}
```

## 10. Hata Yönetimi ve Loglama

### 10.1. Global Exception Handling

```csharp
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Exception tipine göre uygun yanıt oluşturma
        // BusinessException, ValidationException, AuthorizationException vb.
    }
}
```

### 10.2. Yapılandırılmış Loglama (Serilog)

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

## 11. Bilinen Sorunlar ve Çözüm Önerileri

### 11.1. Namespace Çakışmaları
UserRole gibi aynı isimle farklı namespace'lerde tanımlanan sınıflar, derleme hatalarına yol açabilir:

```csharp
// Çözüm 1: Namespace alias kullanmak
using EnumUserRole = ResidenceManagement.Core.Enums.UserRole;
using ModelUserRole = ResidenceManagement.Core.Models.UserRole;

// Çözüm 2: İsimlendirmeyi değiştirmek
// ResidenceManagement.Core.Enums.UserRoleEnum
// ResidenceManagement.Core.Models.UserRoleEntity
```

### 11.2. Repository ve Service Bağımlılıkları
Eksik veya yanlış tanımlanmış bağımlılıklar, runtime hatalarına yol açabilir:

```csharp
// Çözüm: Dependency Injection kontrolleri
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();
// Diğer servis ve repository'ler...
```

## 12. Performans Optimizasyonu

### 12.1. Veritabanı Optimizasyonu
```csharp
// N+1 sorgu problemini çözmek için Include kullanımı
public async Task<IEnumerable<Apartment>> GetAllWithDetails()
{
    return await _dbContext.Apartments
        .Include(a => a.Block)
            .ThenInclude(b => b.Building)
                .ThenInclude(b => b.Site)
        .Include(a => a.Owners)
        .Include(a => a.Residents)
        .Where(a => !a.IsDeleted)
        .ToListAsync();
}
```

### 12.2. API Yanıt Optimizasyonu
```csharp
// Büyük veri setleri için sayfalama
public async Task<PaginatedList<Apartment>> GetPaginatedAsync(int pageIndex, int pageSize)
{
    var query = _dbContext.Apartments.AsQueryable();
    return await PaginatedList<Apartment>.CreateAsync(query, pageIndex, pageSize);
}
```

## 13. Geliştirici Yardımcı Bilgileri

### 13.1. API Endpoint Örnek Kullanımları

#### 13.1.1. Kullanıcı Girişi
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin123!"
}
```

#### 13.1.2. Daire Ekleme
```http
POST /api/apartments
Content-Type: application/json
Authorization: Bearer {token}

{
  "blockId": 1,
  "apartmentNumber": 101,
  "floor": 1,
  "apartmentType": "2+1",
  "grossArea": 120.5,
  "netArea": 105.3,
  "numberOfRooms": 2,
  "numberOfLivingRooms": 1,
  "numberOfBathrooms": 1,
  "status": 1,
  "ownershipType": 1,
  "duesCoefficient": 1.0
}
```

### 13.2. Unit Test Örnekleri

```csharp
[Fact]
public async Task GetById_WhenUserExists_ReturnsUser()
{
    // Arrange
    var userId = 1;
    var expectedUser = new User { Id = userId, Username = "testuser" };
    
    _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
        .ReturnsAsync(expectedUser);
    
    // Act
    var result = await _userService.GetByIdAsync(userId);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal(userId, result.Id);
    Assert.Equal("testuser", result.Username);
}
```

## 14. Dağıtım ve DevOps

### 14.1. CI/CD Pipeline
GitHub Actions kullanarak yapılandırılmış CI/CD pipeline:

```yaml
name: .NET Build and Test

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 8.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

### 14.2. Deployment Ortamları
- Development
- Staging
- Production

Her ortam için ayrı yapılandırma ve environment değişkenleri kullanılmaktadır.

## 15. Ek Kaynaklar ve Referanslar

### 15.1. Önerilen Kaynaklar
- [.NET 8 Dokümantasyonu](https://docs.microsoft.com/en-us/dotnet/core/)
- [Entity Framework Core Dokümantasyonu](https://docs.microsoft.com/en-us/ef/core/)
- [Clean Architecture Principles](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [JWT Authentication](https://jwt.io/introduction)

### 15.2. Proje İçi Referanslar
- [Coding Guidelines](docs/CODING_GUIDELINES.md)
- [Development Plan](docs/DEVELOPMENT_PLAN.md)
- [Rules](docs/RULES.md)
- [API Architecture](Documents/api-architecture.md)
- [Database Schema](Documents/database-schema.md) 