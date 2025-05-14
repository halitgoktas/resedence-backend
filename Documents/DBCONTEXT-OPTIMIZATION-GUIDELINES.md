# DbContext Optimizasyon Kılavuzu

Bu doküman, ApplicationDbContext sınıfı için uygulanan iyileştirmeler ve kullanım kılavuzunu içerir. Multi-tenant ve soft delete yapılarının nasıl kullanılacağı ve performans optimizasyonları için öneriler bu kılavuzda yer almaktadır.

## 1. Global Query Filtreleri

ApplicationDbContext sınıfında aşağıdaki otomatik global filtreler uygulanmaktadır:

### 1.1 Multi-Tenant Filtresi

`ITenant` arayüzünü uygulayan tüm entityler için şu filtreler otomatik uygulanır:

1. **CompanyId Filtresi**: Yalnızca geçerli firma ID'ye sahip kayıtlar getirilir
2. **BranchId Filtresi**: Yalnızca geçerli şube ID'ye sahip VEYA BranchId = 0 olan kayıtlar getirilir (BranchId = 0, tüm şubeleri kapsar)

```csharp
// Tenant bilgilerini ayarlamak için
_dbContext.SetCompanyAndBranchId(1, 2); // CompanyId = 1, BranchId = 2

// Filtrelemeyi geçici olarak devre dışı bırakmak için
_dbContext.SetMultiTenantFilterEnabled(false);

// Filtrelemeyi tekrar etkinleştirmek için
_dbContext.SetMultiTenantFilterEnabled(true);
```

### 1.2 Soft Delete Filtresi

`BaseEntity` sınıfından türeyen tüm entityler için otomatik soft delete filtresi uygulanır:

```csharp
// IsDeleted = false olan kayıtlar otomatik olarak filtrelenir
var apartments = await _dbContext.Apartments.ToListAsync(); // Sadece silinmemiş daireler listelenir
```

## 2. Soft Delete İşlemleri

Fiziksel silme yerine `IsDeleted = true` olarak işaretleme işlemleri için DbContext'te eklenen metotları kullanın:

```csharp
// Bir entity'yi soft delete yapmak için
bool success = _dbContext.SoftDelete(entity);

// Asenkron versiyonu
bool success = await _dbContext.SoftDeleteAsync(entity);

// ID ile soft delete yapmak için
bool success = await _dbContext.SoftDeleteByIdAsync<Apartment>(5);
```

Soft delete işlemi şunları yapar:
- `IsDeleted = true` olarak işaretler
- `DeletedDate` = şu anki tarih/saat olarak ayarlar
- `DeletedBy` = geçerli kullanıcı ID olarak ayarlar
- Sadece bu alanları günceller, diğer alanları değiştirmez

## 3. Performans Optimizasyonları

### 3.1 Change Tracking Optimizasyonu

Salt-okunur sorgular için change tracking'i devre dışı bırakın:

```csharp
// Yaklaşım 1: DbContext üzerinden optimize etme
var query = _dbContext.OptimizeQuery(_dbContext.Apartments);
var apartments = await query.ToListAsync();

// Yaklaşım 2: Manuel olarak AsNoTracking() kullanma
var apartments = await _dbContext.Apartments.AsNoTracking().ToListAsync();
```

### 3.2 İlişki ve Include Optimizasyonu

İlişkili verileri getirirken N+1 sorgu probleminden kaçınmak için RepositoryExtensions sınıfındaki yöntemleri kullanın:

```csharp
// Birden fazla include için
var query = _dbContext.Apartments
    .IncludeMultiple(
        a => a.Block,
        a => a.Residents,
        a => a.Owner
    );

// İç içe include için standard metodu kullanın
var apartments = await _dbContext.Apartments
    .Include(a => a.Residents)
        .ThenInclude(r => r.Contacts)
    .Include(a => a.Block)
    .ToListAsync();
```

### 3.3 Projection Kullanımı

Tüm entity'yi yüklemek yerine sadece ihtiyaç duyulan alanları seçin:

```csharp
var apartmentDtos = await _dbContext.Apartments
    .Select(a => new ApartmentDto
    {
        Id = a.Id,
        Number = a.Number,
        BlockName = a.Block.Name,
        ResidentCount = a.Residents.Count
    })
    .ToListAsync();
```

### 3.4 Sayfalama

Büyük veri setleri için her zaman sayfalama kullanın:

```csharp
// Sayfalama için
var result = await _dbContext.Apartments.AsQueryable()
    .GetPagedListAsync(
        filter: a => a.IsActive,
        orderBy: q => q.OrderBy(a => a.Number),
        pageIndex: pageNumber,
        pageSize: pageSize
    );

var apartments = result.Items; // Sayfa öğeleri
var totalCount = result.TotalCount; // Toplam kayıt sayısı
```

## 4. Entity Kayıt/Güncelleme İşlemleri

DbContext, entity ekleme veya güncelleme sırasında bazı alanları otomatik olarak ayarlar:

### 4.1 Yeni Kayıt Ekleme (Add)

- `CreatedDate` = şu anki tarih/saat
- `CreatedBy` = geçerli kullanıcı ID
- `IsActive` = true
- `IsDeleted` = false
- `CompanyId` = geçerli firma ID
- `BranchId` = geçerli şube ID

### 4.2 Kayıt Güncelleme (Update)

- `UpdatedDate` = şu anki tarih/saat
- `UpdatedBy` = geçerli kullanıcı ID
- `CompanyId` ve `BranchId` değişikliğe karşı korunur (değiştirilmesine izin verilmez)

## 5. Önerilen Veritabanı İndeks Stratejileri

Performans için şu alanlara indeks eklenmesi önerilir:

- Her tablodaki `CompanyId` ve `BranchId` alanları (çoğu sorguda kullanılacağından)
- `IsDeleted` alanı (tüm sorgularda filtrelendiğinden)
- Sık sıralama yapılan alanlar
- Sık filtreleme yapılan alanlar
- Foreign key alanları

Bu indekslerin eklenmesi, özellikle büyük veri setleri ile çalışırken sorgu performansını önemli ölçüde artıracaktır. 