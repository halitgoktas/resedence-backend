# Multi-Tenant Mimari - Rezidans ve Site Yönetim Sistemi

Bu doküman, Rezidans ve Site Yönetim Sistemi'nin multi-tenant mimari yapısını detaylı olarak açıklamaktadır.

## 1. Genel Bakış

Multi-tenant (çoklu kiracı) mimarisi, tek bir yazılım uygulamasının birden fazla müşteri (kiracı) tarafından kullanılmasını sağlayan bir yaklaşımdır. Rezidans ve Site Yönetim Sistemi'nde bu mimari, farklı site yönetim şirketlerinin (firmalar) ve bu şirketlerin farklı şubelerinin sistemi aynı anda kullanabilmesine olanak tanır.

```
Kullanıcı --> [JWT/Headers ile Tenant Bilgileri] --> API --> [MultiTenant Middleware] --> [DbContext Filtreleme] --> Veritabanı
```

## 2. Arayüz Katmanı (Interface Layer)

### 2.1. ITenant, IFirmaTenant ve ISubeTenant Arayüzleri

Multi-tenant yapısının temelini oluşturan arayüzler:

```csharp
// Firma bazlı multi-tenant yapısı için filtreleme arayüzü
public interface IFirmaTenant
{
    int FirmaId { get; set; }
}

// Şube bazlı multi-tenant yapısı için filtreleme arayüzü
public interface ISubeTenant
{
    int SubeId { get; set; }
}

// Multi-tenant yapısı için hem firma hem de şube bazlı filtreleme arayüzü
public interface ITenant : IFirmaTenant, ISubeTenant
{
    // IFirmaTenant ve ISubeTenant arayüzlerinden gelen özellikleri içerir
}
```

## 3. Varlık Katmanı (Entity Layer)

### 3.1. BaseEntity Sınıfı

Tüm varlık sınıflarının temelini oluşturan ve multi-tenant yapısını destekleyen ana sınıf:

```csharp
// BaseEntity sınıfı, tüm entity sınıfları için temel özellikleri tanımlar
public abstract class BaseEntity : ITenant
{
    // Ana kimlik numarası
    public int Id { get; set; }

    // Multi-tenant yapısı için gerekli alanlar
    public int FirmaId { get; set; }
    public int SubeId { get; set; }

    // Oluşturma bilgileri
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    // Güncelleme bilgileri
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    // Silme işlemi için yumuşak silme özelliği
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }

    // Aktif/Pasif durumu
    public bool IsActive { get; set; } = true;

    // Yapılandırıcı metod
    protected BaseEntity()
    {
        CreatedAt = DateTime.Now;
        IsDeleted = false;
        IsActive = true;
    }
}
```

## 4. Veritabanı Erişim Katmanı (Data Access Layer)

### 4.1. ApplicationDbContext Sınıfı

Multi-tenant yapısını veritabanı seviyesinde uygulayan DbContext sınıfı:

```csharp
// ApplicationDbContext sınıfı, veritabanı erişim ve işlemleri için
public class ApplicationDbContext : DbContext
{
    // Multi-tenant yapısı için gerekli ID değerleri
    private int _firmaId;
    private int _subeId;
    private bool _multiTenantFilterEnabled = true;
    
    // Multi-tenant için firma ve şube ID ayarlaması
    public void SetFirmaAndSubeId(int firmaId, int subeId)
    {
        _firmaId = firmaId;
        _subeId = subeId;
    }

    // Multi-tenant filtre durumunu ayarlamak için
    public void SetMultiTenantFilterEnabled(bool enabled)
    {
        _multiTenantFilterEnabled = enabled;
    }

    // Global filtreleri uygulayan metot
    private void ApplyGlobalFilters(ModelBuilder modelBuilder)
    {
        // FirmaTenant entitileri için filtre
        modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(IFirmaTenant).IsAssignableFrom(e.ClrType))
            .ToList()
            .ForEach(e => 
            {
                var parameter = Expression.Parameter(e.ClrType, "e");
                var propertyExpression = Expression.Property(parameter, "FirmaId");
                var filterExpression = Expression.Equal(propertyExpression, Expression.Constant(_firmaId));
                var lambdaExpression = Expression.Lambda(filterExpression, parameter);
                
                modelBuilder.Entity(e.ClrType).HasQueryFilter(lambdaExpression);
            });
        
        // SubeTenant entitileri için filtre
        modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(ISubeTenant).IsAssignableFrom(e.ClrType))
            .ToList()
            .ForEach(e => 
            {
                var parameter = Expression.Parameter(e.ClrType, "e");
                var propertyExpression = Expression.Property(parameter, "SubeId");
                var filterExpression = Expression.Equal(propertyExpression, Expression.Constant(_subeId));
                var lambdaExpression = Expression.Lambda(filterExpression, parameter);
                
                modelBuilder.Entity(e.ClrType).HasQueryFilter(lambdaExpression);
            });
    }

    // SaveChanges metodunu override ederek, kayıt öncesi FirmaId ve SubeId alanlarının
    // otomatik doldurulmasını sağlar
    private void SetTenantIds()
    {
        if (!_multiTenantFilterEnabled)
            return;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                // FirmaId alanını doldur
                if (entry.Entity is IFirmaTenant firmaTenant && firmaTenant.FirmaId <= 0)
                {
                    firmaTenant.FirmaId = _firmaId;
                }

                // SubeId alanını doldur
                if (entry.Entity is ISubeTenant subeTenant && subeTenant.SubeId <= 0)
                {
                    subeTenant.SubeId = _subeId;
                }
            }
        }
    }
}
```

## 5. API Katmanı (API Layer)

### 5.1. MultiTenantMiddleware Sınıfı

HTTP isteklerinden tenant bilgilerini alan ve DbContext'e aktaran middleware:

```csharp
// Multi-tenant yapısı için HTTP isteklerinden tenant (firma ve şube) bilgilerini alan middleware
public class MultiTenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MultiTenantMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Firma ve şube ID değerlerini header'dan al
            if (context.Request.Headers.TryGetValue("X-FirmaId", out var firmaIdValue) &&
                context.Request.Headers.TryGetValue("X-SubeId", out var subeIdValue))
            {
                if (int.TryParse(firmaIdValue, out int firmaId) && 
                    int.TryParse(subeIdValue, out int subeId))
                {
                    // DbContext'e firma ve şube ID değerlerini ayarla
                    var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                    dbContext.SetFirmaAndSubeId(firmaId, subeId);
                }
            }
            else
            {
                // JWT token'dan tenant bilgilerini al
                var user = context.User;
                if (user.Identity?.IsAuthenticated == true)
                {
                    var firmaClaim = user.FindFirst("FirmaId");
                    var subeClaim = user.FindFirst("SubeId");
                    
                    if (firmaClaim != null && subeClaim != null &&
                        int.TryParse(firmaClaim.Value, out int firmaId) && 
                        int.TryParse(subeClaim.Value, out int subeId))
                    {
                        // DbContext'e firma ve şube ID değerlerini ayarla
                        var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                        dbContext.SetFirmaAndSubeId(firmaId, subeId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MultiTenant middleware hatası");
        }

        // Request pipeline'ında bir sonraki middleware'e geç
        await _next(context);
    }
}
```

### 5.2. MultiTenantExtensions Sınıfı

Middleware'i uygulama pipeline'ına ekleyen extension sınıfı:

```csharp
// Multi-tenant middleware'i sisteme eklemek için extension metodları
public static class MultiTenantExtensions
{
    // Uygulama builder'a multi-tenant middleware'i ekler
    public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
    {
        // MultiTenantMiddleware'i pipeline'a ekle
        return app.UseMiddleware<MultiTenantMiddleware>();
    }
}
```

## 6. Kullanım Örnekleri

### 6.1. API İsteğiyle Tenant Bilgisi Gönderme

```http
GET /api/apartments HTTP/1.1
Host: api.residencemanager.com
X-FirmaId: 1
X-SubeId: 2
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 6.2. JWT Token ile Tenant Bilgisi Taşıma

```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1516239022,
  "exp": 1516242622,
  "FirmaId": "1",
  "SubeId": "2",
  "roles": ["Admin", "Manager"]
}
```

## 7. Avantajları

1. **Veri İzolasyonu**: Her firma sadece kendi verilerine erişebilir
2. **Merkezi Kod Tabanı**: Tek bir kod tabanı ile tüm kiracılara hizmet verilir
3. **Kolay Bakım**: Güncellemeler ve bakım tüm kiracılar için bir kerede yapılır
4. **Ölçeklenebilirlik**: Yeni kiracılar kolayca eklenebilir
5. **Maliyet Verimliliği**: Kaynakların paylaşılması ile maliyet azalır

## 8. Dikkat Edilmesi Gerekenler

1. **Performans**: Büyük veri kümeleri için sorgu performansı izlenmelidir
2. **Güvenlik**: Tenant izolasyonunun doğru yapılandırılması kritiktir
3. **Bakım Planlama**: Güncellemeler tüm kiracıları etkileyeceği için dikkatli planlanmalıdır
4. **Özelleştirme**: Kiracı bazlı özelleştirmeler için esnek bir yapı sağlanmalıdır
5. **Yetkilendirme**: Kompleks yetki yapıları oluşturulmalıdır

## 9. Test Stratejisi

Multi-tenant yapısının doğru çalıştığını doğrulamak için aşağıdaki testler yapılmalıdır:

1. **Tenant İzolasyon Testleri**: Bir kiracının diğer kiracı verilerine erişememesi
2. **Performans Testleri**: Çok sayıda kiracı ve büyük veri kümesi ile performans
3. **Güvenlik Testleri**: Tenant bazlı yetkilendirme ve erişim kontrolü
4. **Entegrasyon Testleri**: Middleware ve DbContext entegrasyonu
5. **Birim Testler**: Temel multi-tenant fonksiyonları

## 10. Sonuç

Rezidans ve Site Yönetim Sistemi'nde uygulanan multi-tenant mimari, sistemi birden fazla site yönetim şirketinin ve bu şirketlerin şubelerinin aynı anda kullanabilmesine olanak tanır. Bu yapı sayesinde, her firma ve şube sadece kendi verilerine erişebilir, bu da veri güvenliğini ve izolasyonunu sağlar. Ayrıca, firma ve şube bazlı filtreleme otomatik olarak uygulandığı için, geliştirme sürecinde ekstra kod yazmaya gerek kalmaz. 