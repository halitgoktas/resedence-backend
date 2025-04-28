# Multi-Tenant Yapı Arayüzleri

Bu dizinde, Residence Manager uygulamasının çok kiracılı (multi-tenant) mimarisini destekleyen temel arayüzler bulunmaktadır.

## ITenant Arayüzü

`ITenant` arayüzü, hem firma hem de şube bazlı filtreleme gereksinimlerini karşılamak için kullanılır. Bu arayüz, `IFirmaTenant` ve `ISubeTenant` arayüzlerini birleştirir.

```csharp
public interface ITenant : IFirmaTenant, ISubeTenant
{
    // IFirmaTenant ve ISubeTenant arayüzlerinden gelen özellikleri içerir
}
```

## IFirmaTenant Arayüzü

`IFirmaTenant` arayüzü, entity'lerin firma bazlı filtrelenmesini sağlar.

```csharp
public interface IFirmaTenant
{
    int FirmaId { get; set; }
}
```

## ISubeTenant Arayüzü

`ISubeTenant` arayüzü, entity'lerin şube bazlı filtrelenmesini sağlar.

```csharp
public interface ISubeTenant
{
    int SubeId { get; set; }
}
```

## TenantInfo Sınıfı

`TenantInfo` sınıfı, sistemde aktif olan tenant bilgilerini taşımak için kullanılır.

```csharp
public class TenantInfo
{
    public int FirmaId { get; set; }
    public int SubeId { get; set; }
}
```

## Kullanım

Bu arayüzler, sistemdeki tüm entity'lerin multi-tenant yapısına uygun olmasını sağlamak için kullanılır. 

- Tüm temel entity sınıfları `BaseEntity` aracılığıyla `ITenant` arayüzünü uygular
- Repository'ler veri erişiminde otomatik olarak FirmaId ve SubeId filtresi uygular
- `TenantInfo` kullanılarak istek bazında tenant bilgisi taşınır

## Filtreleme Mekanizması

Global query filtresi:

```csharp
modelBuilder.Entity<T>().HasQueryFilter(e => 
    !e.IsDeleted && 
    e.FirmaId == _tenantInfo.FirmaId && 
    (e.SubeId == _tenantInfo.SubeId || e.SubeId == 0)
);
```

> Not: SubeId 0 (sıfır) değeri, tüm şubelerde geçerli olan kayıtları temsil eder. 