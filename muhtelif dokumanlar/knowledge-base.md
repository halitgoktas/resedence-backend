# Residence Management Projesi - Hata Çözüm Veritabanı

Bu dosya, projede karşılaşılan hata mesajlarını ve bunların çözümlerini saklamak için kullanılır. Yeni bir hata ile karşılaşıldığında önce bu veritabanını kontrol edin, çözüm yoksa çözüm bulunduktan sonra buraya ekleyin.

## [Türkçe-İngilizce Ad Standardizasyonu Hataları]Çözüm:
Projedeki Türkçe isimlendirmeden İngilizce isimlendirmeye geçiş sürecinde ortaya çıkan derleme hataları aşağıdaki adımlarla çözülmüştür:

1. TokenService sınıfındaki Kullanici/Rol referansları User/Role olarak değiştirildi
   ```csharp
   // Değişiklik öncesi
   private readonly IRepository<Kullanici> _kullaniciRepository;
   private readonly IRepository<Rol> _rolRepository;
   
   // Değişiklik sonrası
   private readonly IRepository<User> _userRepository;
   private readonly IRepository<Role> _roleRepository;
   ```

2. NotificationSettingsController ve INotificationSettingsService'deki firmaId/subeId parametreleri companyId/branchId olarak değiştirildi
   ```csharp
   // Değişiklik öncesi
   public async Task<IActionResult> GetNotificationSettings([FromQuery] int firmaId, [FromQuery] int subeId)
   
   // Değişiklik sonrası
   public async Task<IActionResult> GetNotificationSettings([FromQuery] int companyId, [FromQuery] int branchId)
   ```

3. NotificationSettingsDto, SmsLog ve benzeri entity sınıflarında FirmaId/SubeId özellikleri CompanyId/BranchId olarak değiştirildi
   ```csharp
   // Değişiklik öncesi
   public int FirmaId { get; set; }
   public int SubeId { get; set; }
   
   // Değişiklik sonrası
   public int CompanyId { get; set; }
   public int BranchId { get; set; }
   ```

4. SmsService sınıfındaki log mesajları ve metod parametreleri CompanyId/BranchId kullanacak şekilde güncellendi
   ```csharp
   // Değişiklik öncesi
   _logger.LogInformation("SMS gönderiliyor. Alıcı: {PhoneNumber}, FirmaId: {FirmaId}, SubeId: {SubeId}", smsDto.PhoneNumber, smsDto.FirmaId, smsDto.SubeId);
   
   // Değişiklik sonrası
   _logger.LogInformation("SMS gönderiliyor. Alıcı: {PhoneNumber}, CompanyId: {CompanyId}, BranchId: {BranchId}", smsDto.PhoneNumber, smsDto.CompanyId, smsDto.BranchId);
   ```

5. INotificationSettingsRepository ve implementasyonundaki GetByFirmaAndSubeIdAsync methodu GetByCompanyAndBranchIdAsync olarak yeniden adlandırıldı
   ```csharp
   // Değişiklik öncesi
   Task<NotificationSettings> GetByFirmaAndSubeIdAsync(int firmaId, int subeId);
   
   // Değişiklik sonrası
   Task<NotificationSettings> GetByCompanyAndBranchIdAsync(int companyId, int branchId);
   ```

6. DependencyInjection sınıfındaki servis kayıtları güncellendi
   ```csharp
   // Değişiklik öncesi
   services.AddScoped<IFirmaRepository, FirmaRepository>();
   services.AddScoped<ISubeRepository, SubeRepository>();
   services.AddScoped<IFirmaService, FirmaService>();
   services.AddScoped<ISubeService, SubeService>();
   
   // Değişiklik sonrası
   services.AddScoped<ICompanyRepository, CompanyRepository>();
   services.AddScoped<IBranchRepository, BranchRepository>();
   services.AddScoped<ICompanyService, CompanyService>();
   services.AddScoped<IBranchService, BranchService>();
   ```

7. UserService ve benzeri servis sınıflarında Kullanici ve Rol referansları User ve Role olarak değiştirildi
   ```csharp
   // Değişiklik öncesi
   private readonly IRepository<Kullanici> _kullaniciRepository;
   
   // Değişiklik sonrası
   private readonly IRepository<User> _userRepository;
   ```

8. DTO sınıflarında string özellikler için varsayılan değer (string.Empty) eklendi
   ```csharp
   // Değişiklik öncesi
   public string SmtpServer { get; set; }
   
   // Değişiklik sonrası
   public string SmtpServer { get; set; } = string.Empty;
   ```

## [RedisCacheService Eksik Metot İmplementasyonları]Çözüm:
RedisCacheService sınıfında interface'lerde tanımlanan ancak implementasyonu olmayan metotların tümü implemente edilmiştir:

1. UpdateExpiryAsync, SearchKeysAsync, ClearCategoryAsync, FlushAllAsync, GetOrCreateAsync metotları eklendi

## [Duplicate Class Definition (EnumValue, PaginatedResult)]Çözüm:
Aynı isimli sınıfların farklı namespace'lerde tanımlanmasından kaynaklanan hatalar için using aliasları kullanıldı:

```csharp
// EnumValue sınıfı için alias tanımlama
using EnumValueBase = ResidenceManagement.Core.Entities.Base.EnumValue;

// PaginatedResult sınıfı için alias tanımlama
using PaginatedResultData = ResidenceManagement.Core.Common.PaginatedResult;
```

## [ApplicationDbContext Expression Namespace Hatası]Çözüm:
ApplicationDbContext sınıfına eksik System.Linq.Expressions namespace'i eklendi:

```csharp
using System.Linq.Expressions;
```

## [Repository Base Implementation Eksikliği]Çözüm:
MigrationHistoryRepository gibi sınıfların GenericRepository<T> sınıfından düzgün kalıtım almasını sağlamak için gerekli base constructor çağrısı eklendi:

```csharp
public MigrationHistoryRepository(ApplicationDbContext dbContext) 
    : base(dbContext)
{
}
```

## Türkçe-İngilizce İsimlendirme Standartlaştırma Rehberi

Projede Türkçe'den İngilizce'ye geçiş sürecinde tutarlılığı sağlamak için aşağıdaki eşleştirme tablosu kullanılmalıdır:

### Entity ve DTO Sınıfları

| Türkçe İsim        | İngilizce Karşılık |
|--------------------|-------------------|
| Kullanici          | User              |
| Rol                | Role              |
| Firma              | Company           |
| Sube               | Branch            |
| FirmaId            | CompanyId         |
| SubeId             | BranchId          |
| KullaniciAdi       | Username          |
| Ad                 | FirstName         |
| Soyad              | LastName          |
| Sifre              | Password          |
| SifreSalt          | PasswordSalt      |
| SifreOnay          | PasswordConfirm   |
| ProfilResimUrl     | ProfileImageUrl   |
| Telefon            | Phone             |
| FirmaSubeController| CompanyBranchController |

### Interface İsimleri

| Türkçe İsim         | İngilizce Karşılık      |
|---------------------|-------------------------|
| IFirmaRepository    | ICompanyRepository      |
| ISubeRepository     | IBranchRepository       |
| IFirmaService       | ICompanyService         |
| ISubeService        | IBranchService          |
| IKullaniciRepository| IUserRepository         |
| IRolRepository      | IRoleRepository         |

### Metot İsimleri

| Türkçe İsim                 | İngilizce Karşılık              |
|-----------------------------|--------------------------------|
| GetByFirmaAndSubeIdAsync    | GetByCompanyAndBranchIdAsync   |
| SetFirmaAndSubeId           | SetCompanyAndBranchId          |
| FindByKullaniciAdiAsync     | FindByUsernameAsync            |
| ValidateSifreAsync          | ValidatePasswordAsync          |

### Kalan İşler

Tarama sonuçlarına göre aşağıdaki bölümlerde hala Türkçe isimlendirmeler mevcuttur ve düzeltilmesi gerekmektedir:

1. `ResidenceManagement.Core.Interfaces.Repositories` altındaki arayüzlerde:
   - IGenericRepository'de `SetFirmaAndSubeId` -> `SetCompanyAndBranchId`
   - IEmailTemplateRepository'de `GetByFirmaAndSubeIdAsync` -> `GetByCompanyAndBranchIdAsync`

2. Core.Validations altındaki validator sınıflarında:
   - CreateUserDtoValidator ve UpdateUserDtoValidator'da Türkçe property doğrulamaları (KullaniciAdi, Soyad, Sifre vb.)

3. Core.Models altındaki model sınıflarında:
   - Financial, Residence, Staff, Services, Maintenance, Rental gibi tüm alt namespace'lerdeki modellerde FirmaId/SubeId özellikleri

4. CreateUserDto ve UpdateUserDto gibi DTO sınıflarında:
   - KullaniciAdi -> Username
   - Sifre -> Password
   - SifreOnay -> PasswordConfirm
   - Ad -> FirstName
   - Soyad -> LastName

5. UserService gibi servis implementasyonlarında:
   - MapToUserDtoAsync gibi metotlarda Türkçe özellik adlarını referans alan kod parçaları

Bu değişiklikleri sistematik olarak uygulamak için şu adımları izleyin:

1. Önce temel entity ve DTO sınıflarını güncelleyin
2. Repository arayüz ve implementasyonlarını güncelleyin
3. Servis arayüz ve implementasyonlarını güncelleyin
4. Controller sınıflarını güncelleyin
5. Validator ve yardımcı sınıfları güncelleyin

Böylece projedeki tüm Türkçe isimlendirmelerin İngilizce karşılıklarıyla değiştirilmesini tamamlamış olacaksınız.

## [AuthenticationService Türkçe Referansları]Çözüm:
AuthenticationService sınıfındaki Türkçe referanslar İngilizce karşılıklarıyla değiştirildi:

1. Kullanici ve Rol repository referansları User ve Role olarak güncellendi
   ```csharp
   // Değişiklik öncesi
   private readonly IRepository<Kullanici> _userRepository;
   private readonly IRepository<Rol> _roleRepository;
   
   // Değişiklik sonrası
   private readonly IRepository<User> _userRepository;
   private readonly IRepository<Role> _roleRepository;
   ```

2. Constructor parametreleri güncellendi
   ```csharp
   // Değişiklik öncesi
   public AuthenticationService(
       IRepository<Kullanici> userRepository,
       IRepository<Rol> roleRepository,
       ...)
   
   // Değişiklik sonrası
   public AuthenticationService(
       IRepository<User> userRepository,
       IRepository<Role> roleRepository,
       ...)
   ```

3. Kullanıcı referanslarındaki property isimleri güncellendi
   ```csharp
   // Değişiklik öncesi
   u.KullaniciAdi == loginRequest.UserName
   user.Sifre, user.SifreSalt
   user.SonGirisTarihi = DateTime.UtcNow
   
   // Değişiklik sonrası
   u.Username == loginRequest.UserName
   user.Password, user.PasswordSalt
   user.LastLoginDate = DateTime.UtcNow
   ```

4. Kullanıcı-rol ilişkilerindeki property isimleri güncellendi
   ```csharp
   // Değişiklik öncesi
   userRoleEntities = await _userRoleRepository.GetAllAsync(ur => ur.KullaniciId == user.Id);
   roleIds = userRoleEntities.Select(ur => ur.RolId).ToList();
   roles.Add(role.RolAdi);
   
   // Değişiklik sonrası
   userRoleEntities = await _userRoleRepository.GetAllAsync(ur => ur.UserId == user.Id);
   roleIds = userRoleEntities.Select(ur => ur.RoleId).ToList();
   roles.Add(role.RoleName);
   ```

5. JWT Token içindeki claim isimleri güncellendi
   ```csharp
   // Değişiklik öncesi
   new Claim("username", user.KullaniciAdi),
   new Claim("firmaId", user.FirmaId.ToString()),
   new Claim("subeId", user.SubeId.ToString()),
   new Claim("fullName", user.TamAd)
   
   // Değişiklik sonrası
   new Claim("username", user.Username),
   new Claim("companyId", user.CompanyId.ToString()),
   new Claim("branchId", user.BranchId.ToString()),
   new Claim("fullName", user.FullName)
   ```

6. RefreshToken entity özellikleri güncellendi
   ```csharp
   // Değişiklik öncesi
   KullaniciId = user.Id,
   FirmaId = user.FirmaId,
   SubeId = user.SubeId
   
   // Değişiklik sonrası
   UserId = user.Id,
   CompanyId = user.CompanyId,
   BranchId = user.BranchId
   ```

7. GenerateJwtToken metodu parametresi güncellendi
   ```csharp
   // Değişiklik öncesi
   private string GenerateJwtToken(Kullanici user, List<string> roles)
   
   // Değişiklik sonrası
   private string GenerateJwtToken(User user, List<string> roles)
   ```

## [NotificationLogService ve NotificationPreferenceService'te Türkçe İsim Referansları]Çözüm:
NotificationLogService ve NotificationPreferenceService sınıflarında Türkçe isim referansları (FirmaId, SubeId) İngilizce karşılıklarıyla (CompanyId, BranchId) değiştirildi:

1. NotificationLogService:
   - Tüm metotlarda parametre adları: `firmaId, subeId` -> `companyId, branchId`
   - Tüm log mesajlarında alanlar: `FirmaId, SubeId` -> `CompanyId, BranchId`
   - LINQ sorgularında filtreler: `nl.FirmaId, nl.SubeId` -> `nl.CompanyId, nl.BranchId`

2. NotificationPreferenceService:
   - Repository tanımları: `UserNotificationPreference` -> `NotificationPreference`
   - Tüm metotlarda parametre adları: `firmaId, subeId` -> `companyId, branchId`
   - Tüm log mesajlarında alanlar: `FirmaId, SubeId` -> `CompanyId, BranchId`
   - LINQ sorgularında filtreler: `nl.FirmaId, nl.SubeId` -> `nl.CompanyId, nl.BranchId`
   - Bazı metotlar tamamen yeniden düzenlendi: UpdateUserPreferenceAsync, BulkUpdatePreferencesForTypeAsync

## [ResidentRepository'de Interface Uygulaması Eksik]Çözüm:
ResidentRepository sınıfında IResidentRepository interface'inde tanımlı olup henüz implemente edilmemiş olan `FilterResidentsByBlockIdAsync` metodu eklendi:

```csharp
/// <summary>
/// Blok ID'sine göre sakinleri filtreler
/// </summary>
public async Task<List<Resident>> FilterResidentsByBlockIdAsync(int blockId, int companyId, int branchId)
{
    try
    {
        // Önce belirtilen blok ID'sine sahip tüm daireleri bulalım
        var apartments = await _dbContext.Apartments
            .Where(a => a.BlockId == blockId && 
                       a.CompanyId == companyId && 
                       a.BranchId == branchId)
            .Select(a => a.Id)
            .ToListAsync();

        if (apartments.Count == 0)
        {
            return new List<Resident>();
        }

        // Bulunan dairelerde oturan sakinleri getirelim
        var residents = await _dbContext.Residents
            .Where(r => apartments.Contains(r.ApartmentId) && 
                       r.CompanyId == companyId && 
                       r.BranchId == branchId)
            .Include(r => r.Apartment)
            .Include(r => r.User)
            .ToListAsync();

        return residents;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Block ID'sine göre sakin filtreleme hatası: {ex.Message}");
        throw;
    }
}
```

Metot, önce verilen blok ID'sine sahip tüm daireleri bulup, ardından bu dairelerde oturan sakinleri getiriyor.

## [Core Katmanı Entity ve DTO Türkçe-İngilizce Standardizasyonu]Çözüm:
Core katmanındaki Entity ve DTO sınıflarında Türkçe isim referansları İngilizce karşılıklarıyla değiştirildi:

1. TenantInfo sınıfında:
   ```csharp
   // Değişiklik öncesi
   public int FirmaId { get; set; }
   public int SubeId { get; set; }
   
   // Değişiklik sonrası
   public int CompanyId { get; set; }
   public int BranchId { get; set; }
   ```

2. CustomClaimTypes sınıfında:
   ```csharp
   // Değişiklik öncesi
   public const string FirmaId = "FirmaId";
   public const string SubeId = "SubeId";
   
   // Değişiklik sonrası
   public const string CompanyId = "CompanyId";
   public const string BranchId = "BranchId";
   ```

3. UserDto sınıfında:
   ```csharp
   // Değişiklik öncesi
   public string KullaniciAdi { get; set; }
   public string Telefon { get; set; }
   public string Ad { get; set; }
   public string Soyad { get; set; }
   public string TamAd { get; set; }
   public string ProfilResimUrl { get; set; }
   public DateTime? SonGirisTarihi { get; set; }
   public int FirmaId { get; set; }
   public int SubeId { get; set; }
   public List<string> Roller { get; set; }
   
   // Değişiklik sonrası
   public string Username { get; set; }
   public string Phone { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string FullName { get; set; }
   public string ProfileImageUrl { get; set; }
   public DateTime? LastLoginDate { get; set; }
   public int CompanyId { get; set; }
   public int BranchId { get; set; }
   public List<string> Roles { get; set; }
   ```

4. UserInfoResponse sınıfında:
   ```csharp
   // Değişiklik öncesi
   public int? FirmaId { get; set; }
   public int? SubeId { get; set; }
   public int? CompanyId { get => FirmaId; set => FirmaId = value; }
   public int? BranchId { get => SubeId; set => SubeId = value; }
   
   // Değişiklik sonrası
   public int? CompanyId { get; set; }
   public int? BranchId { get; set; }
   ```

5. SmsDto, SmsLogDto ve SmsLogFilterDto sınıflarında:
   ```csharp
   // Değişiklik öncesi
   public int FirmaId { get; set; }
   public int SubeId { get; set; }
   
   // Değişiklik sonrası
   public int CompanyId { get; set; }
   public int BranchId { get; set; }
   ```

6. MaintenanceDocumentDTO, MaintenanceDocument, Invoice, ServiceDefinition, EmailTemplate ve User entity sınıflarında:
   ```csharp
   // Değişiklik öncesi
   public int FirmaId { get; set; }
   public int SubeId { get; set; }
   
   // Değişiklik sonrası
   public int CompanyId { get; set; }
   public int BranchId { get; set; }
   ```

Bu değişikliklerin yapılmasıyla Core katmanındaki temel entity ve DTO sınıflarında Türkçe isim referanslarının İngilizce karşılıkları ile standartlaştırılması sağlanmıştır. Sıradaki adım Infrastructure katmanındaki implementasyonların güncellenmesi olacaktır.

## [Hata Mesajı]Aynı İsimli DTO Sınıflarının Çözümü:
Aynı namespace içerisinde aynı isimli sınıfların bulunması hatası:

1. **Tekrarlanan DTO Tanımlamaları**:
   - NotificationPreferenceUpdateDto.cs dosyasında NotificationPreferenceBulkUpdateDto ve BulkUpdateNotificationPreferencesDto sınıfları aynı zamanda NotificationPreferenceBulkUpdateDto.cs dosyasında da tanımlanmıştı.
   - Çözüm: NotificationPreferenceUpdateDto.cs dosyasından tekrarlanan sınıf tanımlarını kaldırdık. Artık sadece bir tanım bulunuyor.

2. **Entity Namespace Çakışmaları**:
   - NotificationLog ve NotificationType gibi sınıfların hem ResidenceManagement.Core.Entities.Notification hem de ResidenceManagement.Core.Entities.Notifications namespace'lerinde tanımlanması.
   - Çözüm: Uygulamada tek bir namespace standardı belirlenmeli, diğer namespace'teki sınıflar silinmeli veya taşınmalıdır.

3. **Belirsiz Tip Referansları**:
   - UpdateNotificationPreferenceDto, BulkUpdateNotificationPreferencesDto gibi sınıfların farklı namespace'lerde aynı isimle tanımlanması.
   - Çözüm: Namespace yollarını tam olarak belirtmek veya namespace alias'ları kullanmak.

## [Hata Mesajı]Notifications ve Notification ad alanı çakışması Çözüm:
Projede hem Notification tekil isimli ad alanı hem de Notifications çoğul isimli ad alanı kullanılmaktaydı. Bu durum birçok hataya sebep oldu.

### Sorunlar:
1. **Aynı Entity ve DTO sınıfları iki farklı ad alanında tanımlanmıştı**:
   - `ResidenceManagement.Core.Entities.Notification` ve `ResidenceManagement.Core.Entities.Notifications`
   - `ResidenceManagement.Core.DTOs.Notification` ve `ResidenceManagement.Core.DTOs.Notifications`

2. **Aynı isimde tekrar tanımlanan DTO sınıfları**:
   - NotificationPreferenceBulkUpdateDto
   - BulkUpdateNotificationPreferencesDto
   - NotificationLogDto
   - NotificationLogFilterDto
   - NotificationLogSummaryDto

### Çözüm:
1. **Tek ad alanı standardı belirle**: Çoğul form (Notifications) tercih edildi
2. **Using direktiflerini güncelle**: Tüm dosyalarda Notification yerine Notifications kullan
3. **Tekrarlanan DTO sınıflarını kaldır**: Duplicate olan DTO tanımlarını temizle
4. **FirmaId/SubeId → CompanyId/BranchId dönüşümünü tamamla**: Tüm sınıflarda tutarlı olarak İngilizce isimleri kullan

### Daha fazla duplicate ve çakışma olmaması için kurallar:
1. Her entity ve DTO sınıfı yalnızca bir dosyada tanımlanmalı
2. Ad alanları tutarlı olmalı (tekil veya çoğul, projenin tamamında aynı standart kullanılmalı)
3. Yeni bir entity veya DTO oluşturmadan önce mevcut sınıflar kontrol edilmeli
4. Tüm property'ler İngilizce olarak isimlendirilmeli (FirmaId yerine CompanyId, SubeId yerine BranchId, vb.)

## [Hata Mesajı]ResidenceManagement.Core.DTOs.Notifications' ad alanı 'BulkUpdateNotificationPreferencesDto' için zaten bir tanım içeriyor Çözüm:

1. **Hata**: `ResidenceManagement.Core.DTOs.Notifications.BulkUpdateNotificationPreferencesDto` sınıfı hem `NotificationPreferenceDto.cs` hem de `NotificationPreferenceBulkUpdateDto.cs` dosyalarında tanımlanmıştı.

2. **Çözüm**: `NotificationPreferenceDto.cs` dosyasındaki `BulkUpdateNotificationPreferencesDto` sınıfı tanımını kaldırdık ve sadece `NotificationPreferenceBulkUpdateDto.cs` dosyasındaki tanımı bıraktık.

3. **Sonuç**: Derleme hatası çözüldü, ancak Notifications (çoğul) ve Notification (tekil) ad alanı çatışmaları devam ediyor.

4. **Eklenecek İşler**: 
   - Tüm `ResidenceManagement.Core.DTOs.Notification` (tekil) ad alanındaki sınıfları `ResidenceManagement.Core.DTOs.Notifications` (çoğul) ad alanına taşı
   - FirmaId/SubeId → CompanyId/BranchId dönüşümü ile ilgili özellik güncellemelerini tamamla
   - Duplicate DTO sınıflarının konsolidasyonunu yap

Bu şekilde namespace standardizasyonu tamamlanarak kod kalitesi ve bakım kolaylığı artırılacaktır.

## [Hata Mesajı]'ResidentFilterDto' türü veya ad alanı adı bulunamadı ve 'ResidentRepository.GetPaginatedResidentsAsync(ResidentFilterRequest)' 'IResidentRepository.GetPaginatedResidentsAsync(ResidentFilterRequest)' öğesini uygulayamaz Çözüm:

1. **Hata**: `ResidentRepository.cs` dosyasında iki hata vardı:
   - `ResidentFilterDto` sınıfı için using direktifi eksikti.
   - `GetPaginatedResidentsAsync` metodu `PagedResponse<ResidentDto>` döndürüyordu, ancak interface'de tanımlı dönüş tipi `PaginatedResult<ResidentDto>` idi.

2. **Çözüm**:
   - `ResidenceManagement.Core.DTOs.Residents` namespace'ini ekledik.
   - `GetPaginatedResidentsAsync` metodunun dönüş tipini `PaginatedResult<ResidentDto>` olarak değiştirdik.
   - Metot içindeki dönüş yapısını bu tip ile uyumlu olacak şekilde güncelledik.

3. **Öğrenilen Dersler**:
   - Interface implementasyonunda dönüş tiplerinin tam olarak eşleştiğinden emin olunmalı.
   - Dönüş nesneleri arasında `PagedResponse` ve `PaginatedResult` gibi benzer isimlere sahip ama farklı sınıflar kullanılmamalı.
   - Bu tür namespace/tip hataları için ilk bakılacak yer `using` direktifleridir.# #   H a t a :   E k s i k   U p d a t e M a i n t e n a n c e S t a t u s A s y n c   m e t o d u \ n � � z � m : \ n -   M a i n t e n a n c e S c h e d u l e S e r v i c e   s 1n 1f 1n d a   U p d a t e M a i n t e n a n c e S t a t u s A s y n c   m e t o d u n u n   M a i n t e n a n c e S t a t u s   e n u m   p a r a m e t r e l i   s � r � m �   e k s i k t i \ n -   B u   m e t o d u   a _a 1d a k i   _e k i l d e   e k l e d i k :  
 p u b l i c   a s y n c   T a s k < b o o l >   U p d a t e M a i n t e n a n c e S t a t u s A s y n c ( i n t   i d ,   M a i n t e n a n c e S t a t u s   s t a t u s ,   s t r i n g   n o t e s ) \ n { \ n         v a r   s c h e d u l e   =   a w a i t   _ u n i t O f W o r k . M a i n t e n a n c e S c h e d u l e R e p o s i t o r y . G e t B y I d A s y n c ( i d ) ; \ n         i f   ( s c h e d u l e   = =   n u l l ) \ n                 r e t u r n   f a l s e ; \ n \ n         s c h e d u l e . S t a t u s   =   s t a t u s ; \ n         s c h e d u l e . N o t e s   =   n o t e s ; \ n         \ n         / /   U p d a t e   c o m p l e t i o n   r a t e   a n d   d a t e s   b a s e d   o n   s t a t u s \ n         i f   ( s t a t u s   = =   M a i n t e n a n c e S t a t u s . C o m p l e t e d ) \ n         { \ n                 s c h e d u l e . C o m p l e t i o n R a t e   =   1 0 0 ; \ n                 s c h e d u l e . E n d D a t e   =   D a t e T i m e . N o w ; \ n         } \ n         e l s e   i f   ( s t a t u s   = =   M a i n t e n a n c e S t a t u s . I n P r o g r e s s ) \ n         { \ n                 / /   D o n ' t   m o d i f y   c o m p l e t i o n   r a t e   i f   a l r e a d y   s e t \ n                 i f   ( s c h e d u l e . C o m p l e t i o n R a t e   < =   0 ) \ n                         s c h e d u l e . C o m p l e t i o n R a t e   =   1 0 ;   / /   D e f a u l t   s t a r t i n g   p r o g r e s s \ n         } \ n         e l s e   i f   ( s t a t u s   = =   M a i n t e n a n c e S t a t u s . C a n c e l l e d ) \ n         { \ n                 s c h e d u l e . E n d D a t e   =   D a t e T i m e . N o w ; \ n         } \ n \ n         _ u n i t O f W o r k . M a i n t e n a n c e S c h e d u l e R e p o s i t o r y . U p d a t e ( s c h e d u l e ) ; \ n         r e t u r n   a w a i t   _ u n i t O f W o r k . S a v e C h a n g e s A s y n c ( )   >   0 ; \ n }  
 \ n \ n # #   H a t a :   E k s i k   C o m p l e t e M a i n t e n a n c e A s y n c   m e t o d u \ n � � z � m : \ n -   M a i n t e n a n c e S c h e d u l e S e r v i c e   s 1n 1f 1n d a   C o n t r o l l e r ' 1n   b e k l e d i i   i m z a y a   s a h i p   C o m p l e t e M a i n t e n a n c e A s y n c   m e t o d u   e k s i k t i \ n -   B u   m e t o d u   a _a 1d a k i   _e k i l d e   e k l e d i k :  
 p u b l i c   a s y n c   T a s k < b o o l >   C o m p l e t e M a i n t e n a n c e A s y n c ( i n t   i d ,   s t r i n g   c o m p l e t i o n N o t e s ,   d e c i m a l ?   a c t u a l C o s t ,   i n t ?   a c t u a l D u r a t i o n M i n u t e s ) \ n { \ n         v a r   s c h e d u l e   =   a w a i t   _ u n i t O f W o r k . M a i n t e n a n c e S c h e d u l e R e p o s i t o r y . G e t B y I d A s y n c ( i d ) ; \ n         i f   ( s c h e d u l e   = =   n u l l ) \ n                 r e t u r n   f a l s e ; \ n \ n         s c h e d u l e . S t a t u s   =   M a i n t e n a n c e S t a t u s . C o m p l e t e d ; \ n         s c h e d u l e . C o m p l e t i o n R a t e   =   1 0 0 ; \ n         s c h e d u l e . E n d D a t e   =   D a t e T i m e . N o w ; \ n         s c h e d u l e . N o t e s   =   c o m p l e t i o n N o t e s ; \ n         \ n         i f   ( a c t u a l C o s t . H a s V a l u e ) \ n                 s c h e d u l e . A c t u a l C o s t   =   a c t u a l C o s t . V a l u e ; \ n                 \ n         i f   ( a c t u a l D u r a t i o n M i n u t e s . H a s V a l u e ) \ n                 s c h e d u l e . A c t u a l D u r a t i o n   =   a c t u a l D u r a t i o n M i n u t e s . V a l u e ; \ n         \ n         _ u n i t O f W o r k . M a i n t e n a n c e S c h e d u l e R e p o s i t o r y . U p d a t e ( s c h e d u l e ) ; \ n         r e t u r n   a w a i t   _ u n i t O f W o r k . S a v e C h a n g e s A s y n c ( )   >   0 ; \ n }  
 