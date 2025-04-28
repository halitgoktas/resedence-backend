# ResidenceManagement.Core

Bu proje, Residence Manager uygulamasının çekirdek (Core) katmanını içerir. Clean Architecture / Onion Architecture prensiplerine uygun olarak tasarlanmıştır.

## Mimari Yapı

Core katmanı, uygulamanın en iç katmanıdır ve dış katmanlara bağımlılığı yoktur. Bu katman:

- Uygulamanın domain modellerini (entity'ler)
- İş kurallarını
- Arayüzleri (interface'ler)
- Enum'ları ve diğer domain nesnelerini

içerir.

## Dizin Yapısı

- **Base**: Tüm entity'ler için temel sınıflar (`BaseEntity`, `BaseTransactionEntity`, `BaseSystemEntity` vb.)
- **Common**: Ortak kullanılan arayüzler, DTO'lar ve yardımcı sınıflar
- **Entities**: Domain entity'leri
  - **Identity**: Kullanıcı, rol, firma, şube gibi kimlik entity'leri
  - **Property**: Rezidans, blok, daire gibi gayrimenkul entity'leri
  - **Financial**: Aidat, fatura, ödeme gibi finansal entity'ler
  - **Management**: Yönetim ile ilgili entity'ler
  - **Inventory**: Envanter ve ekipman entity'leri
- **Enums**: Domain'de kullanılan enum türleri
- **Exceptions**: Özel exception sınıfları
- **Interfaces**: Repository, servis ve diğer bileşenlerin arayüzleri
- **ValueObjects**: Domain için değer nesneleri

## Multi-Tenant Mimarisi

Uygulama, çok kiracılı (multi-tenant) bir yapıya sahiptir:

- `ITenant` arayüzü ile tüm varlıklar firma ve şube bazlı filtrelenir
- `BaseEntity` sınıfı, tüm tenant-specific entity'ler için temel oluşturur
- `BaseSystemEntity` sınıfı, tenant filtrelemesi olmayan global entity'ler için temel oluşturur
- Veri erişimi otomatik olarak ilgili tenant'a göre filtrelenir

## Entity Yapısı

Tüm entity'ler:

- Birincil anahtar olarak `int` türünde `Id` özelliğine sahiptir
- `CreatedDate`, `UpdatedDate` gibi audit alanlarını içerir
- `IsDeleted` ve `IsActive` özellikleri ile yumuşak silme (soft delete) desteklenir
- Multi-tenant entity'leri `FirmaId` ve `SubeId` alanlarını içerir

## Domain Model Örneği

Entity hiyerarşisi örneği:

```
BaseEntity
  ├─ Residence (Site/Rezidans)
  │    └─ Block (Blok)
  │         └─ Apartment (Daire)
  ├─ Firma (Şirket)
  │    └─ Sube (Şube)
  └─ User (Kullanıcı)
```

## Kurallar ve Kısıtlamalar

- Core katmanı sadece diğer Core bileşenlerine bağımlı olabilir
- Dış katmanlara bağımlılık yasaktır
- Infrastructure veya diğer katmanların referansları eklenemez
- İş mantığı bu katmanda tanımlanır 