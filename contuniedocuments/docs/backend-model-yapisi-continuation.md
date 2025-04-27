### 8.1. Duyuru Modeli (Announcement.cs) (devam)

```csharp
public class Announcement : BaseTransactionEntity
{
    // ... önceki özellikler ...
    public string TargetAudience { get; set; } // All, Owners, Tenants, Staff, Specific
    public string TargetApartments { get; set; } // JSON (Belirli dairelere yapılan duyurular için)
    public string AttachmentPaths { get; set; } // JSON
    public bool NotificationSent { get; set; }
    public string NotificationChannels { get; set; } // Email, SMS, App, All
    public int CreatedByUserId { get; set; }
    
    // Navigation Properties
    public virtual User CreatedByUser { get; set; }
}
```

### 8.2. Bildirim Modeli (Notification.cs)

Bildirim modeli, kullanıcılara gönderilen bildirimleri temsil eder.

```csharp
public class Notification : BaseTransactionEntity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string NotificationType { get; set; } // System, Payment, Reservation, Maintenance
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }
    public string RelatedEntityType { get; set; }
    public int? RelatedEntityId { get; set; }
    public string NotificationChannel { get; set; } // Email, SMS, App
    public string Status { get; set; } // Pending, Delivered, Failed
    
    // Navigation Properties
    public virtual User User { get; set; }
}
```

## 9. Bakım ve Hizmet Modelleri

### 9.1. Bakım Talebi Modeli (MaintenanceRequest.cs)

Bakım talebi modeli, bakım ve onarım taleplerini temsil eder.

```csharp
public class MaintenanceRequest : BaseTransactionEntity
{
    public int UserId { get; set; }
    public int? ApartmentId { get; set; }
    public int? ServiceTypeId { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public TimeSpan? ScheduledTimeStart { get; set; }
    public TimeSpan? ScheduledTimeEnd { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string Status { get; set; } // Pending, Approved, Scheduled, InProgress, Completed, Cancelled
    public int? AssignedPersonnelId { get; set; }
    public decimal? Price { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public int? PaymentId { get; set; }
    public string Notes { get; set; }
    public string Priority { get; set; } // Low, Medium, High, Urgent
    public string AttachmentPaths { get; set; } // JSON
    public int? Rating { get; set; }
    public string Feedback { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Apartment Apartment { get; set; }
    public virtual ServiceType ServiceType { get; set; }
    public virtual User AssignedPersonnel { get; set; }
    public virtual Payment Payment { get; set; }
}
```

### 9.2. Hizmet Tipi Modeli (ServiceType.cs)

Hizmet tipi modeli, sunulan hizmet tiplerini temsil eder.

```csharp
public class ServiceType : BaseLookupEntity
{
    public decimal Price { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public int EstimatedDuration { get; set; } // dakika cinsinden
    public bool RequiresApproval { get; set; }
    public bool ChargeToApartment { get; set; }
    public int? DepartmentId { get; set; }
    
    // Navigation Properties
    public virtual Department Department { get; set; }
    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
}
```

### 9.3. Personel Modeli (Personnel.cs)

Personel modeli, site personelini temsil eder.

```csharp
public class Personnel : BaseTransactionEntity
{
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string SalaryCurrency { get; set; } // TRY, USD, EUR, GBP
    public string WorkingHours { get; set; } // JSON
    public string EmergencyContact { get; set; }
    public string IdentityNumber { get; set; }
    public string BankAccountDetails { get; set; }
    public string Skills { get; set; } // JSON
    public string Certifications { get; set; } // JSON
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Department Department { get; set; }
}
```

### 9.4. Departman Modeli (Department.cs)

Departman modeli, personel departmanlarını temsil eder.

```csharp
public class Department : BaseLookupEntity
{
    public string ResponsibleName { get; set; }
    public string ResponsiblePhone { get; set; }
    public string Email { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Personnel> Personnel { get; set; }
    public virtual ICollection<ServiceType> ServiceTypes { get; set; }
}
```

## 10. Raporlama ve Ayarlar Modelleri

### 10.1. Sistem Parametresi Modeli (SystemParameter.cs)

Sistem parametresi modeli, sistem ayarlarını temsil eder.

```csharp
public class SystemParameter : BaseLookupEntity
{
    public string ParameterKey { get; set; }
    public string ParameterValue { get; set; }
    public string ParameterType { get; set; } // String, Int, Decimal, Bool, DateTime
    public bool IsEditable { get; set; }
    public string DisplayGroup { get; set; }
}
```

### 10.2. Aktivite Logu Modeli (ActivityLog.cs)

Aktivite logu modeli, sistem aktivitelerini temsil eder.

```csharp
public class ActivityLog : BaseEntity
{
    public int? UserId { get; set; }
    public string ActivityType { get; set; }
    public string EntityType { get; set; }
    public int? EntityId { get; set; }
    public string Details { get; set; } // JSON
    public string OldValues { get; set; } // JSON
    public string NewValues { get; set; } // JSON
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public DateTime ActivityDate { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
}
```

## 11. Enum Değerleri

Sistem içerisinde kullanılan önemli enum değerleri:

### 11.1. Kullanıcı Rolleri (UserRoleEnum)

```csharp
public enum UserRoleEnum
{
    SystemAdmin = 1,
    FirmaYoneticisi = 2,
    SiteAdmin = 3,
    Personel = 4,
    Sakin = 5,
    MalSahibi = 6,
    Teknik = 7,
    Misafir = 8
}
```

### 11.2. Daire Durumu (ApartmentStatus)

```csharp
public enum ApartmentStatus
{
    Vacant = 1,            // Boş
    OccupiedByOwner = 2,   // Sahibi tarafından kullanılıyor
    OccupiedByTenant = 3,  // Kiracı tarafından kullanılıyor
    ForSale = 4,           // Satışta
    ForRent = 5            // Kiralık
}
```

### 11.3. Site Durumu (SiteStatus)

```csharp
public enum SiteStatus
{
    UnderConstruction = 1, // İnşaat aşamasında
    Active = 2,            // Aktif
    UnderMaintenance = 3,  // Bakım/Yenileme sürecinde
    Inactive = 4           // Pasif
}
```

### 11.4. İzin Seviyeleri (PermissionLevelEnum)

```csharp
public enum PermissionLevelEnum
{
    None = 0,      // Erişim yok
    View = 1,      // Sadece görüntüleme
    Basic = 2,     // Temel işlemler (görüntüleme + ekleme)
    Standard = 3,  // Standart işlemler (görüntüleme + ekleme + düzenleme)
    Full = 4,      // Tam erişim (görüntüleme + ekleme + düzenleme + silme)
    Admin = 5      // Yönetici erişimi (tüm işlemler + yönetimsel yetkiler)
}
```

### 11.5. Aidat Hesaplama Tipi (DuesCalculationType)

```csharp
public enum DuesCalculationType
{
    Fixed = 1,      // Sabit - tüm daireler aynı aidatı öder
    AreaBased = 2,  // Metrekare bazlı - daire büyüklüğüne göre
    RoomBased = 3,  // Oda sayısına göre
    Mixed = 4       // Karma hesaplama
}
```

## 12. Model İlişkileri ve Entity Framework Konfigürasyonu

Veritabanı ilişkileri, Entity Framework Core'un Fluent API'si kullanılarak konfigüre edilecektir. Örnek konfigürasyon:

```csharp
public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("Apartments");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.ApartmentNumber)
               .IsRequired()
               .HasMaxLength(20);
               
        builder.Property(a => a.Floor)
               .IsRequired();
               
        builder.Property(a => a.GrossArea)
               .HasColumnType("decimal(18,2)")
               .IsRequired();
               
        builder.HasOne(a => a.Block)
               .WithMany(b => b.Apartments)
               .HasForeignKey(a => a.BlockId)
               .OnDelete(DeleteBehavior.Restrict);
               
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
```

## 13. Multi-tenant Yapısı ve Filtreleme

Tüm modeller, multi-tenant yapıya uygun olarak FirmaId ve SubeId alanlarını içerir. Repository katmanında otomatik tenant filtreleme için aşağıdaki yapı kullanılacaktır:

```csharp
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    
    public BaseRepository(AppDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>()
                            .Where(e => e.FirmaId == _currentUserService.FirmaId && 
                                   e.SubeId == _currentUserService.SubeId &&
                                   !e.IsDeleted)
                            .ToListAsync();
    }
    
    // Diğer metotlar...
}
```

## 14. Validasyon

Model validasyonu için FluentValidation kütüphanesi kullanılacaktır. Örnek validasyon sınıfı:

```csharp
public class ApartmentValidator : AbstractValidator<Apartment>
{
    public ApartmentValidator()
    {
        RuleFor(a => a.ApartmentNumber)
            .NotEmpty().WithMessage("Daire numarası boş olamaz.")
            .MaximumLength(20).WithMessage("Daire numarası en fazla 20 karakter olabilir.");
            
        RuleFor(a => a.Floor)
            .NotEmpty().WithMessage("Kat bilgisi boş olamaz.");
            
        RuleFor(a => a.GrossArea)
            .GreaterThan(0).WithMessage("Brüt alan sıfırdan büyük olmalıdır.");
            
        RuleFor(a => a.NetArea)
            .GreaterThan(0).WithMessage("Net alan sıfırdan büyük olmalıdır.")
            .LessThanOrEqualTo(a => a.GrossArea).WithMessage("Net alan, brüt alandan büyük olamaz.");
            
        RuleFor(a => a.BlockId)
            .NotEmpty().WithMessage("Blok bilgisi boş olamaz.");
    }
}
```

## 15. DTO (Data Transfer Object) Kullanımı

Veri transferi için DTO sınıfları kullanılacaktır. Örnek DTO:

```csharp
public class ApartmentDto
{
    public int Id { get; set; }
    public string ApartmentNumber { get; set; }
    public int Floor { get; set; }
    public string ApartmentType { get; set; }
    public decimal GrossArea { get; set; }
    public decimal NetArea { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public string Status { get; set; }
    public string BlockName { get; set; }
    public string SiteName { get; set; }
    public string OwnerName { get; set; }
    public string TenantName { get; set; }
    public decimal DuesAmount { get; set; }
}
```

## 16. Servis Katmanı

Servis katmanı, iş mantığını uygulayacak ve repository'ler ile controller'lar arasında aracı olacaktır. Örnek servis arayüzü:

```csharp
public interface IApartmentService
{
    Task<IEnumerable<ApartmentDto>> GetAllAsync();
    Task<ApartmentDto> GetByIdAsync(int id);
    Task<ApartmentDto> CreateAsync(ApartmentCreateDto dto);
    Task<ApartmentDto> UpdateAsync(int id, ApartmentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ApartmentDto>> GetByBlockIdAsync(int blockId);
    Task<IEnumerable<ApartmentDto>> GetVacantAsync();
    Task<bool> AssignOwnerAsync(int id, int ownerId);
    Task<bool> AssignTenantAsync(int id, int tenantId);
}
```

## 17. Sonuç

Bu döküman, Rezidans ve Site Yönetim Sistemi'nin backend model yapısını ve ilişkilerini detaylı olarak açıklamaktadır. Geliştirme sürecinde, burada belirtilen model yapısı ve ilişkiler doğrultusunda veritabanı oluşturulacak ve kod geliştirme yapılacaktır. Multi-tenant yapı ve rol bazlı yetkilendirme sistemi, projenin temel yapı taşlarını oluşturmaktadır.
