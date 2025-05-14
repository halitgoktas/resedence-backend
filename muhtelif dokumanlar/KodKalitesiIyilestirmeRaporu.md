# Kod Kalitesi İyileştirme Raporu

Bu rapor, Mekik Residence Manager projesinde yapılan kod kalitesi iyileştirmelerini özetlemektedir.

## 1. Null Referans Güvenliği

Null referans hatalarını önlemek için repository sınıflarında aşağıdaki iyileştirmeler yapılmıştır:

### 1.1. GenericRepositoryAdapter Sınıfı

- `GetByIdAsync` metodu, null dönüş yerine varsayılan bir entity döndürecek şekilde düzenlendi.
- `GetFirstOrDefaultAsync` metodu, null dönüş yerine varsayılan bir entity döndürecek şekilde düzenlendi.
- `GetAllAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAllAsync(predicate)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate, orderBy, includeString, disableTracking)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate, orderBy, includes, disableTracking)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.

### 1.2. GenericRepository Sınıfı

- `GetByIdAsync` metodu, null dönüş yerine varsayılan bir entity döndürecek şekilde düzenlendi.
- `GetFirstOrDefaultAsync` metodu, null dönüş yerine varsayılan bir entity döndürecek şekilde düzenlendi.
- `GetAllAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAllAsync(predicate)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate, orderBy, includeString, disableTracking)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetAsync(predicate, orderBy, includes, disableTracking)` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `FindAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.

### 1.3. EnumValueRepository Sınıfı

- `GetEnumValueAsync` metodu, null dönüş yerine varsayılan bir EnumValue döndürecek şekilde düzenlendi.
- `GetAllEnumValuesAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetByIdAsync` metodu, null dönüş yerine varsayılan bir EnumValue döndürecek şekilde düzenlendi.

### 1.4. MaintenanceChecklistItemRepository Sınıfı

- `GetChecklistItemsByMaintenanceIdAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetChecklistItemByIdAsync` metodu, null dönüş yerine varsayılan bir MaintenanceChecklistItem döndürecek şekilde düzenlendi.
- `GetAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `FindAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `FirstOrDefaultAsync` metodu, null dönüş yerine varsayılan bir MaintenanceChecklistItem döndürecek şekilde düzenlendi.

### 1.5. MaintenanceLogRepository Sınıfı

- `GetLogsByMaintenanceScheduleIdAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `FindAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.

### 1.6. BranchRepository Sınıfı

- `GetBranchesByCompanyIdAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetBranchByNameAsync` metodu, null dönüş yerine varsayılan bir Branch döndürecek şekilde düzenlendi.

### 1.7. CompanyRepository Sınıfı

- `GetCompanyByNameAsync` metodu, null dönüş yerine varsayılan bir Company döndürecek şekilde düzenlendi.
- `GetCompanyByTaxNumberAsync` metodu, null dönüş yerine varsayılan bir Company döndürecek şekilde düzenlendi.
- `GetActiveCompaniesAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetCompanyWithBranchesAsync` metodu, null dönüş yerine varsayılan bir Company döndürecek şekilde düzenlendi.

### 1.8. RoleRepository Sınıfı

- `GetRoleByNameAsync` metodu, null dönüş yerine varsayılan bir Role döndürecek şekilde düzenlendi.
- `GetRolesByCompanyAndBranchAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetRolesByUserIdAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.
- `GetRoleNamesByUserIdAsync` metodu, boş liste dönüş yerine yeni bir liste döndürecek şekilde düzenlendi.

## 2. Metot Gizleme Sorunları

Metot gizleme (method hiding) sorunlarını çözmek için aşağıdaki iyileştirmeler yapılmıştır:

### 2.1. MaintenanceChecklistItemRepository Sınıfı

- `Update` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `Delete` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `GetQueryable` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `UpdateAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `DeleteAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `UpdateRangeAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.

### 2.2. MaintenanceLogRepository Sınıfı

- `FindAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `GetQueryable` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `UpdateAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `DeleteAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `AddRangeAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `DeleteRangeAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.
- `UpdateRangeAsync` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.

### 2.3. BranchRepository Sınıfı

- `Update` metodu, `new` anahtar kelimesi yerine `override` anahtar kelimesi ile düzenlendi.

## 3. Parametre Kontrolü

Metotlara gelen parametrelerin geçerliliğini kontrol etmek için aşağıdaki iyileştirmeler yapılmıştır:

### 3.1. CompanyRepository Sınıfı

- `GetCompanyByNameAsync` metodunda `name` parametresi için null kontrolü eklendi.
- `GetCompanyByTaxNumberAsync` metodunda `taxNumber` parametresi için null kontrolü eklendi.
- `GetCompanyWithBranchesAsync` metodunda `companyId` parametresi için geçerlilik kontrolü eklendi.

### 3.2. RoleRepository Sınıfı

- `GetRoleByNameAsync` metodunda `roleName` parametresi için null kontrolü eklendi.
- `GetRolesByCompanyAndBranchAsync` metodunda `companyId` ve `branchId` parametreleri için geçerlilik kontrolü eklendi.
- `GetRolesByUserIdAsync` metodunda `userId` parametresi için geçerlilik kontrolü eklendi.
- `GetRoleNamesByUserIdAsync` metodunda `userId` parametresi için geçerlilik kontrolü eklendi.
- `IsUserInRoleAsync` metodunda `userId` ve `roleName` parametreleri için geçerlilik kontrolü eklendi.

## 4. Varsayılan Entity Oluşturma

Null referans hatalarını önlemek için varsayılan entity oluşturan yardımcı metotlar eklenmiştir:

### 4.1. GenericRepositoryAdapter Sınıfı

```csharp
/// <summary>
/// Varsayılan entity oluşturur (null dönüş yerine)
/// </summary>
protected virtual T CreateDefaultEntity(int id)
{
    // Yeni bir entity oluştur
    var entity = Activator.CreateInstance<T>();
    entity.Id = id;
    entity.CreatedDate = DateTime.Now;
    entity.IsActive = true;
    entity.IsDeleted = false;
    return entity;
}
```

### 4.2. GenericRepository Sınıfı

```csharp
/// <summary>
/// Varsayılan entity oluşturur (null dönüş yerine)
/// </summary>
protected virtual T CreateDefaultEntity(int id)
{
    // Yeni bir entity oluştur
    var entity = Activator.CreateInstance<T>();
    entity.Id = id;
    entity.CreatedDate = DateTime.Now;
    entity.IsActive = true;
    entity.IsDeleted = false;
    return entity;
}
```

## 5. Sonuç

Yapılan iyileştirmeler sonucunda:

1. Null referans hatalarının önüne geçilmiştir.
2. Metot gizleme sorunları çözülmüştür.
3. Parametre kontrolü ile daha güvenli metot çağrıları sağlanmıştır.
4. Kod daha güvenli ve daha okunabilir hale getirilmiştir.

Bu iyileştirmeler, projenin daha kararlı çalışmasını sağlayacak ve hata ayıklama sürecini kolaylaştıracaktır.
