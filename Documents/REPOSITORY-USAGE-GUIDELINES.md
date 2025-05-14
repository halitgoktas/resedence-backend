# Repository Kullanım Kılavuzu

Bu doküman, Repository pattern kullanımında izlenmesi gereken yöntemleri ve özellikle de N+1 sorgu probleminden kaçınmak için Include ve ThenInclude kullanımının standart yaklaşımını açıklar.

## N+1 Sorgu Problemi

N+1 sorgu problemi, ORM (Object-Relational Mapping) kullanırken karşılaşılan yaygın bir performans sorunudur. Bu problem şu şekilde ortaya çıkar:

1. Ana entity'leri getirmek için 1 adet sorgu yapılır
2. Ardından her bir ana entity için ilişkili entity'leri getirmek için N adet ek sorgu yapılır

Örneğin, 100 apartment kaydı çekip, her bir apartment için sakinleri ayrı sorgularla getirmek, toplam 101 sorguya sebep olur (1 apartment listesi sorgusu + 100 sakin sorgusu).

## Çözüm: Eager Loading (Include ve ThenInclude)

N+1 sorgu problemini çözmek için Entity Framework'ün sağladığı Eager Loading yaklaşımını kullanmalıyız:

```csharp
// YANLIŞ - N+1 sorgu problemi
var apartments = await _repository.GetAllAsync();
foreach (var apartment in apartments)
{
    // Her bir daire için ayrı sorgu
    var residents = await _residentRepository.GetByApartmentIdAsync(apartment.Id);
    // ...
}

// DOĞRU - Tek sorguda ilişkili verileri de getir
var apartments = await _repository.GetAsync(
    predicate: null,
    orderBy: q => q.OrderBy(a => a.BlockId).ThenBy(a => a.Number),
    includes: new List<Expression<Func<Apartment, object>>>
    {
        a => a.Block,
        a => a.Residents,
        a => a.Owner
    }
);
```

## RepositoryExtensions Kullanımı

Projeye eklenmiş olan RepositoryExtensions sınıfındaki extension metotlar, sorgu optimizasyonlarını standartlaştırmak için kullanılmalıdır:

### 1. IncludeMultiple Metodu

```csharp
// Birden fazla property için Include işlemi
var query = _dbContext.Apartments.AsQueryable();
query = query.IncludeMultiple(
    a => a.Block,
    a => a.Owner,
    a => a.Residents
);
```

### 2. GetPagedListAsync Metodu

```csharp
// Filtreleme, sıralama, include ve sayfalama birlikte
var result = await _dbContext.Apartments.AsQueryable()
    .GetPagedListAsync(
        filter: a => a.BlockId == blockId,
        orderBy: q => q.OrderBy(a => a.Number),
        includes: new[] { 
            a => a.Block, 
            a => a.Residents, 
            a => a.Owner 
        },
        pageIndex: pageNumber,
        pageSize: pageSize
    );

// Sonuçlar ve toplam sayı
var apartments = result.Items;
var totalCount = result.TotalCount;
```

### 3. ThenInclude Kullanımı

Eğer iç içe ilişkilere erişmek istiyorsanız, şu şekilde standard Entity Framework metotlarını kullanabilirsiniz:

```csharp
// İç içe ilişkiler için ThenInclude kullanımı
var apartments = await _dbContext.Apartments
    .Include(a => a.Residents)
        .ThenInclude(r => r.Contacts)
    .Include(a => a.Block)
    .Where(a => a.IsActive)
    .ToListAsync();
```

## Repository Metotlarında Standart Yaklaşım

Repository implementasyonlarında, ilişkili verileri getiren metotlar aşağıdaki standartlara uygun olarak yazılmalıdır:

```csharp
public async Task<Apartment> GetApartmentWithDetailsAsync(int id)
{
    return await _dbContext.Apartments
        .Where(a => a.Id == id)
        .IncludeMultiple(
            a => a.Block,
            a => a.Owner,
            a => a.Residents
        )
        .Include(a => a.Residents)
            .ThenInclude(r => r.Contacts)
        .FirstOrDefaultAsync();
}
```

## Select Sorguları ile Projection

Performansı daha da artırmak için, sadece ihtiyaç duyulan alanları seçen projection sorguları kullanılmalıdır:

```csharp
// Tüm entity'yi getirmek yerine sadece ihtiyaç duyulan alanları getir
var apartmentDtos = await _dbContext.Apartments
    .Where(a => a.BlockId == blockId)
    .Select(a => new ApartmentDto
    {
        Id = a.Id,
        Number = a.Number,
        Floor = a.Floor,
        BlockName = a.Block.Name,
        OwnerName = a.Owner != null ? $"{a.Owner.FirstName} {a.Owner.LastName}" : "",
        ResidentCount = a.Residents.Count
    })
    .ToListAsync();
```

## Öneriler

1. **İhtiyaç kadar Include kullanın**: Sadece gerçekten ihtiyaç duyulan ilişkileri include edin.
2. **AsNoTracking kullanın**: Salt okunur sorgularda performansı artırmak için AsNoTracking kullanın.
3. **Sayfalama yapın**: Büyük veri setleri için her zaman sayfalama kullanın.
4. **Projection kullanın**: Tüm nesneyi getirmek yerine ihtiyaç duyulan alanları seçin.

## Anti-Paternler (Kullanılmaması Gerekenler)

1. **ToList + ForEach**: Önce verileri listeleyip sonra döngü içinde ilişkili verileri çekmek.
2. **Explicit Loading**: Önce ana entity'yi getirip sonra Collection.Load() veya Reference.Load() metotlarını kullanmak.
3. **Lazy Loading**: Otomatik yükleme özelliğini açarak ilişkili verilerin ihtiyaç anında otomatik yüklenmesi.

Bu kuralların ve önerilerin izlenmesi, uygulamanın veritabanı erişim performansını önemli ölçüde artıracaktır. 