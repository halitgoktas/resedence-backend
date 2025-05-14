# Residence Manager - Yazılım Geliştirme Standartları

Bu dokümantasyon, SQL Server, C# (.NET) ve React.js ile Clean Architecture/Onion Architecture prensiplerine dayalı geliştirme yaparken uyulması gereken teknik standartları içerir. Tüm geliştirme, refaktör ve bakım işlemleri bu standartlara bağlı kalmalıdır.

## 1. Mimari ve Tasarım İlkeleri

- **Katmanlı Mimari**: Backend için Clean Architecture / Onion Architecture prensiplerine uygun yapı oluştur:
  - Core (Entities, Interfaces, DTOs, Domain Services)
  - Application (Use Cases, Application Services, Validators)
  - Infrastructure (DB, External Services, Implementations)
  - Presentation (API Controllers, Filters, Middlewares)

- **Bağımlılık Yönetimi**:
  - Bağımlılıklar her zaman daha içteki katmanlara doğru olmalı
  - Domain katmanı hiçbir dış katmana bağımlı olmamalı
  - Dependency Injection ile gevşek bağlaşma (loose coupling) sağlanmalı
  - Interface'ler ile servis soyutlaması yaratılmalı

- **SOLID Prensipleri**:
  - Single Responsibility: Her sınıf/modül tek bir sorumluluk taşımalı
  - Open/Closed: Sınıflar değişikliğe kapalı, genişletmeye açık olmalı
  - Liskov Substitution: Alt sınıflar üst sınıfların yerine geçebilmeli
  - Interface Segregation: İstemciler kullanmadıkları metodlara bağımlı olmamalı
  - Dependency Inversion: Soyutlamalara bağımlı olunmalı, somut implementasyonlara değil

- **Multi-tenant Mimarisi**:
  - Shared schema approach (her tabloda CompanyId ve BranchId alanları)
  - DbContext seviyesinde global query filtreleme
  - Performans için CompanyId ve BranchId üzerinde uygun indeksleme
  - Tenant bazlı veri erişim kontrolü
  - JWT token içinde tenant bilgisi
  - API katmanında tenant doğrulama middleware'i
  - Her servis çağrısında tenant context'i aktarımı
  - Cross-tenant veri erişimi için explicit yönetim
  - Tenant-specific konfigürasyon yapılandırması

## 2. İsimlendirme ve Dil Standardı

### 2.1 Genel İsimlendirme Kuralları
- **Teknik kodlarda** (sınıf, metot, değişken, property isimleri) İngilizce kullanılmalıdır
- **Yorum satırları, enum değerleri, hata mesajları, doğrulama mesajları ve kullanıcı arayüzündeki metinler** Türkçe olabilir
- Teknik kodlarda Türkçe karakter kullanılmamalı (enum değerleri dahil)
- Kısaltmalar yerine açık ve anlaşılır isimler tercih edilmeli
- Sayfa isimleri, tablo adı, tablo alanları vb. kodlar tamamen İngilizce olacak
- Projenin varsayılan kullanıcı ana dili Türkçe, ayrıca İngilizce, Almanca, Rusça, Arapça ve Farsça kullanıcı dil desteği de olacak

### 2.2 Entity İsimlendirme Standardı

1. Entity İsimleri:
   - PascalCase kullanılmalı
   - Tekil form kullanılmalı (Companies yerine Company)
   - Domain-specific prefix/suffix kullanılmamalı
   - Örnek: User, Company, Branch, Apartment

2. Property İsimleri:
   - PascalCase kullanılmalı
   - Bool property'ler Is/Has/Can ile başlamalı
   - ID alanları için EntityNameId formatı kullanılmalı
   - Örnek: CompanyId, IsActive, HasPermission

3. Navigation Property'ler:
   - İlişki tipini yansıtmalı (tekil/çoğul)
   - Virtual keyword kullanılmalı
   - Örnek: public virtual Company Company { get; set; }
   - Örnek: public virtual ICollection<Branch> Branches { get; set; }

### 2.3 Dosya Başlangıç Yorum Standardı
Her .cs dosyasının başında aşağıdaki formatta yorum bloğu bulunmalıdır:

```csharp
// -----------------------------------------------------------------------
// <copyright file="EntityName.cs" company="ResidenceManager">
// Copyright (c) ResidenceManager. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Purpose: [Entity/Service/Controller amacının kısa açıklaması]
// Dependencies: [Bağımlı olduğu temel sınıflar/servisler]
// Usage: [Kullanım örneği veya önemli notlar]
// Author: [Geliştirici adı]
// Created: [Oluşturulma tarihi]
// Modified: [Son değişiklik tarihi]
// -----------------------------------------------------------------------
```

### 2.4 Namespace Standardı

```csharp
// Doğru
namespace ResidenceManager.Core.Entities.Identity
namespace ResidenceManager.Infrastructure.Data.Repositories
namespace ResidenceManager.API.Controllers

// Yanlış
namespace ResidenceYonetimi.Core.Entities
namespace Residence.Manager.Core
namespace ResidenceManager.Entities.Firma
```

### 2.5 C# İsimlendirme Standardı
- Sınıflar, Interface'ler, metotlar: PascalCase
- Parametreler, yerel değişkenler: camelCase
- Private field'lar: _camelCase
- Constant'lar: UPPER_SNAKE_CASE
- Interface'ler I öneki ile başlamalı (IUserService)
- Boolean değişkenler/metotlar is/has/can öneki ile başlamalı

### 2.6 Enum İsimlendirmesi
- Enum sınıf isimleri İngilizce ve PascalCase olmalı (Gender, MaritalStatus, PaymentType)
- Enum değerleri Türkçe olabilir (Erkek, Kadin, Evli, Bekar) ancak teknik kod olduğundan Türkçe karakter içermemelidir
- Enum dosya adları İngilizce olmalıdır

### 2.7 Çeviri Sözlüğü

Aşağıdaki çeviri sözlüğü, temel entity ve kavramların Türkçe-İngilizce karşılıklarını göstermektedir:

| Türkçe        | İngilizce           |
|---------------|---------------------|
| Firma         | Company             |
| Sube          | Branch              |
| Daire         | Apartment           |
| Blok          | Block               |
| Sakin         | Resident            |
| Aidat         | Dues                |
| DaireSakin    | ApartmentResident   |
| HizmetTipi    | ServiceType         |
| HizmetKategorisi | ServiceCategory  |
| BildirimSablonu | NotificationTemplate |
| SaklanmisDosya | StoredFile         |
| DosyaKategorisi | FileCategory      |
| HizmetTalebi  | ServiceRequest      |
| OdemeDurumu   | PaymentStatus       |
| OdemeYontemi  | PaymentMethod       |
| OdemeYonu     | PaymentDirection    |
| OdemeTipi     | PaymentType         |

## 3. Entity ve Kod Çakışma Önleme Kuralları

### 3.1 Entity Lokasyon Kontrolü
- Yeni entity eklemeden önce tüm solution'da arama yapılmalı
- Hem İngilizce hem Türkçe karşılıkları kontrol edilmeli
- Benzer isimli entity'ler için ekip lead'i ile görüşülmeli

### 3.2 Merkezi Entity Repository
- Tüm entity'ler Core/Entities altında toplanmalı
- Alt domainler için uygun klasör yapısı kullanılmalı
- Entity isimleri solution-wide unique olmalı ve İngilizce olmalıdır

### 3.3 Entity Değişiklik Prosedürü
- Entity değişikliği için pull request zorunlu
- Entity değişikliği için ekip lead onayı zorunlu
- Değişiklik öncesi etki analizi zorunlu
- Migration script'leri için DBA review zorunlu

### 3.4 Code Review Kontrol Listesi
- [ ] Entity ismi İngilizce mi?
- [ ] Dosya başlangıç yorumu eklenmiş mi?
- [ ] Namespace standardına uygun mu?
- [ ] Benzer/çakışan entity var mı?
- [ ] Base class doğru seçilmiş mi?

## 4. Dökümantasyon Gereksinimleri

### 4.1 XML Documentation
```csharp
/// <summary>
/// Represents a company in the residence management system
/// </summary>
/// <remarks>
/// This entity is part of the multi-tenant architecture
/// </remarks>
public class Company : BaseEntity, ITenant
```

### 4.2 README Güncellemesi
- Her yeni entity için README güncellenmeli
- Entity ilişkileri dokümante edilmeli
- Breaking change'ler belirtilmeli
- Entity'nin nerede kullanıldığı ve amacı hakkında bilgi verilmeli
- Entity'nin özellikleri ve kullanımı için açıklamalar yapılmalı
- Entity'nin oluşturulma/güncellenme prosedürü tanımlanmalı

### 4.3 Sayfa Yorumları
- Tüm sayfalarda sayfa hakkında bilgi içeren Türkçe yorum satırları olacak

## 5. Backend (.NET Core) Geliştirme Standartları

### 5.1 Proje Yapısı ve Organizasyon

- **Çözüm Yapısı**:
  ```
  ResidenceManagement/
  ├── ResidenceManagement.Domain (Core katmanı)
  │   ├── Entities/
  │   ├── Interfaces/
  │   ├── DTOs/
  │   ├── Enums/
  │   ├── Exceptions/
  │   └── ValueObjects/
  ├── ResidenceManagement.Application
  │   ├── Services/
  │   ├── Interfaces/
  │   ├── Validators/
  │   ├── MappingProfiles/
  │   └── Behaviors/ (Cross-cutting concerns)
  ├── ResidenceManagement.Infrastructure
  │   ├── Data/
  │   │   ├── Context/
  │   │   ├── Repositories/
  │   │   ├── Migrations/
  │   │   └── Configurations/
  │   ├── Services/
  │   ├── Security/
  │   └── Logging/
  └── ResidenceManagement.API
      ├── Controllers/
      ├── Middlewares/
      ├── Extensions/
      ├── Filters/
      └── Configuration/
  ```

- **Asenkron Programlama**: 
  - I/O bound işlemler için her zaman asenkron metotlar kullan
  - Task, Task<T> ve ValueTask<T> dönüş tiplerini doğru şekilde kullan
  - ConfigureAwait(false) kalıbını library kodlarında kullan
  - CancellationToken'ları tüm uzun süren işlemlere geçir

### 5.2 Veritabanı ve ORM Kullanımı

- **Entity Framework Core Optimizasyonları**:
  - Change tracking'i gerektiğinde etkisizleştir (AsNoTracking())
  - Kullanılacak alanlar için Projection (Select) kullan
  - N+1 problem için Include/ThenInclude veya koleksiyon yükleme stratejilerini optimize et
  - Soft Delete için global query filtreleri kullan
  - Büyük datasetler için pagination zorunlu olmalı

- **Veritabanı Tasarımı**:
  - İlişkiler düzgün tanımlanmalı (Cascade, Restrict, SetNull)
  - İndeksler arama, filtreleme, sıralama gibi sık kullanılan alanlara uygulanmalı
  - Veritabanı nesneleri için uygun tipler kullanılmalı (örn. metin için nvarchar(MAX) yerine uygun uzunluk)
  - Multi-tenant mimarisi için DbContext seviyesinde global query filtreleri eklenecek

- **Migration Stratejisi**:
  - Migrationlar küçük, açıklayıcı ve geri alınabilir olmalı
  - Üretim veri kaybı önlemleri alınmalı (örn. yeni kolon eklemeden önce var olan kolonun silinmemesi)
  - Migrationlar CI/CD pipeline içinde otomatik uygulanacak şekilde yapılandırılmalı

### 5.3 API Tasarımı ve İmplementasyonu

- **RESTful Endpoint Standartları**:
  - Resource-oriented URL yapısı (`/api/v1/companies/{id}/branches`)
  - HTTP metodlarının doğru kullanımı (GET, POST, PUT, PATCH, DELETE)
  - Tutarlı API response yapısı, hata kodları ve mesajlar
  - 200, 201, 204, 400, 401, 403, 404, 500 status kodlarının uygun kullanımı

- **API Versiyonlama**:
  - URL path versiyonlama (`/api/v1/` veya `/api/v2/`)
  - API backward compatibility için sözleşme kuralları
  - Deprecated API'ler için uyarı header'larının eklenmesi

- **Controller Tasarımı**:
  - İnce controller prensibi (business logic servislerde olmalı)
  - Endpoint'ler için ayrı Request/Response DTO'ları
  - ActionResult<T> dönüş tipi ile tutarlı API yanıtları
  - Route ve HTTP metodlarının açık şekilde belirtilmesi

- **API Dokümantasyonu**:
  - Swagger/OpenAPI entegrasyonu
  - XML yorum satırları ile endpoint dokümantasyonu
  - Örnek request/response modellerinin oluşturulması

### 5.4 Güvenlik

- **Kimlik Doğrulama ve Yetkilendirme**:
  - JWT yapılandırması (issuer, audience, expiration, signing key)
  - Refresh token implementasyonu (secure, http-only)
  - Rol ve policy tabanlı yetkilendirme
  - Claims-based identity yönetimi
  - API koruma (rate limiting, API key)

- **Veri Güvenliği**:
  - Hassas verilerin şifrelenmesi (AES-256, salt algoritmaları)
  - Kişisel verilerin KVKK/GDPR uyumlu işlenmesi
  - API girdilerinin validasyonu (FluentValidation)
  - SQL injection, XSS, CSRF önlemleri
  - HTTPS zorunluluğu ve güvenli header yapılandırması

## 6. Frontend (React.js) Geliştirme Standartları

### 6.1 Proje Yapısı ve Organizasyon

- **Atomic Design Prensibi**:
  ```
  src/
  ├── api/               # API istek fonksiyonları
  ├── assets/            # Statik dosyalar
  ├── components/
  │   ├── atoms/         # Temel UI elementleri
  │   ├── molecules/     # Atom kombinasyonları
  │   ├── organisms/     # Form, card vb. karmaşık yapılar
  │   ├── templates/     # Sayfa şablonları
  │   └── pages/         # Route bağlantılı tam sayfalar
  ├── context/           # React Context API ile state yönetimi
  ├── hooks/             # Reusable mantıklar
  ├── utils/             # Yardımcı fonksiyonlar
  ├── styles/            # Global stillendirme
  ├── types/             # TypeScript tip tanımları
  └── config/            # Ortam değişkenleri ve yapılandırma
  ```

- **Modern React Pratikleri**:
  - Functional Component yapısının kullanımı
  - React Hooks API'sinin etkin kullanımı
  - Prop drilling yerine Context API veya state yönetim çözümleri
  - Component bölünmesi ve reusability prensipleri
  - Code splitting ve lazy loading

### 6.2 İsimlendirme Standardı

- **JavaScript/TypeScript İsimlendirme**:
  - Komponentler: PascalCase
  - Fonksiyonlar, değişkenler: camelCase
  - Sabitler: UPPER_SNAKE_CASE
  - Enum'lar: PascalCase
  - Event handler'lar handle öneki ile: handleClick, handleSubmit
  - Custom hook'lar use öneki ile: useAuth, useFormValidation

### 6.3 Performans Optimizasyonları

- **Render Optimizasyonu**:
  - React.memo() ile gereksiz renderlama engellenecek
  - useCallback() ve useMemo() kullanılarak fonksiyon ve değer önbelleğe alınacak
  - Virtual List kullanımı (büyük listeler için)
  - Reselect ile Redux selector memoizasyonu

- **Bundle Optimizasyonu**:
  - Code splitting ve dynamic import
  - Tree-shaking imkanı sağlayan import yazımı
  - Vendor chunk oluşturma
  - Webpack Bundle Analyzer ile paket boyutu optimizasyonu
  - Görsel optimizasyonu (WebP, responsive images)

### 6.4 State Yönetimi

- **Lokal State**:
  - useState() ve useReducer() kullanımı (karmaşıklığa bağlı seçim)
  - Form state için form kütüphaneleri (React Hook Form, Formik)

- **Global State**:
  - Context API + useReducer kombinasyonu
  - Büyük uygulamalar için Redux Toolkit
  - Immutable state updates (immer entegrasyonu)
  - Selector paterninin uygulanması (state slice erişimi için)

- **Asenkron State**:
  - React Query veya SWR kullanımı
  - Cache invalidation stratejileri
  - Optimistic updates
  - Error retrying ve retry politikaları

### 6.5 Güvenlik

- **Frontend Güvenliği**:
  - Content Security Policy (CSP) tanımları
  - XSS koruması için HTML Sanitizer kullanımı
  - API isteklerinde CSRF token
  - HTTPS Only cookie kullanımı
  - JWT ve Refresh Token güvenli saklama

## 7. İsimlendirme ve Değişiklik Uygulaması

### 7.1 Türkçe'den İngilizce'ye Geçiş Stratejisi

1. **Değişiklik Öncesi Etki Analizi**:
   - Değişecek entity ve property'lerin tam listesi çıkarılmalı
   - Veritabanı kolonları ve migration strateji belirlenme
   - Bağımlı sınıflar ve serviceler tespit edilmeli
   - Frontend tarafındaki etkilenecek komponentler listelenme

2. **Tutarlı Değişiklik Uygulama Prosedürü**:
   - Core/Domain katmanından başlayarak dışa doğru ilerleme
   - Tüm ilgili property'lerin tek seferde değiştirilmesi
   - Bağımlı metot ve servis güncellemeleri
   - API endpoint ve URL güncelleme
   - Frontend entegrasyon

3. **Derleme ve Test Sırası**:
   - Core/Domain → Infrastructure → API → Frontend sırasını takip et
   - Her aşamada birim testleri çalıştır
   - Backend değişiklikleri tamamlandıktan sonra entegrasyon testleri yap
   - Frontend değişiklikleri öncesi regression test

4. **İsimlendirme Değişiklikleri İçin Özel Kurallar**:
   - Multi-tenant yapıda `FirmaId`/`SubeId` yerine `CompanyId`/`BranchId` kullanımı zorunlu
   - Entity isimlerinde Türkçe kullanımı yasaklı (Firma, Sube, Daire, Blok yerine Company, Branch, Apartment, Block kullanılmalı)
   - API endpoint URL'lerinde Türkçe segment kullanımı yasaklı
   - İsimlendirmelerde tutarlılık için çeviri sözlüğüne başvurulmalı

5. **Etki Alanı Kontrolü İçin Araçlar**:
   - Solution-wide arama (CTRL+SHIFT+F) ile Türkçe terimlerin tespiti
   - DbContext üzerinde ilgili property ve navigation property'lerin incelenmesi
   - API Controller'ları üzerinde route ve metot isimlerinin kontrolü
   - Frontend API call'larında kullanılan endpoint ve model property'lerinin kontrolü

6. **Değişiklik Sonrası Kontrol Listesi**:
   - [ ] Tüm solution hatasız derleniyor mu?
   - [ ] Tüm birim ve entegrasyon testleri geçiyor mu?
   - [ ] Migrationlar doğru şekilde çalışıyor mu?
   - [ ] API endpoint'leri doğru çalışıyor mu?
   - [ ] Frontend komponentleri doğru şekilde render oluyor mu?
   - [ ] Multi-tenant filtreleme doğru çalışıyor mu?

### 7.2 İsimlendirme Geçiş Prosedürü

İsimlendirme değişikliklerinin güvenli ve tutarlı bir şekilde uygulanması için aşağıdaki adımlar izlenmelidir:

1. **Geçiş Sırası**:
   - Öncelik 1: BaseEntity sınıfında FirmaId/SubeId -> CompanyId/BranchId değişimi
   - Öncelik 2: Core Entity sınıflarının İngilizceye çevrilmesi (Firma -> Company, Sube -> Branch, vb.)
   - Öncelik 3: Controller endpoint URL'lerinin İngilizceye çevrilmesi
   - Öncelik 4: Servis ve repository metotlarının İngilizceye çevrilmesi
   - Öncelik 5: DTO ve View Model isimlerinin İngilizceye çevrilmesi

2. **Veritabanı Uyumluluk Stratejisi**:
   - Entity Framework Fluent API veya Data Annotation'lar ile Column/Table isimlendirme mapping'leri yapılacak
   - Migration stratejisi: Rename Column/Table yerine kolon mapping tercih edilecek
   - Geçiş sürecinde çift property destekli (eski isim + yeni isim) ara model kullanımı düşünülebilir

3. **API Backward Compatibility**:
   - Geçiş sürecinde route uyumluluk için `[Route("api/v1/[controller]")]` yerine açık URL mapping kullanılabilir
   - Eski endpoint'leri koruyarak yeni endpoint'lere yönlendirme yapılabilir
   - API versiyonlama ile v1 (eski) ve v2 (yeni) endpoint'ler paralel sunulabilir

4. **Frontend Adaptasyon Stratejisi**:
   - API wrapper katmanı oluşturarak isim değişikliklerini izole etme
   - Model mapper kullanarak eski/yeni model dönüşümlerini otomatikleştirme
   - Feature flag ile kademeli geçiş yönetimi

5. **Kod Dönüşüm Otomasyonu**:
   - Regex search/replace script'leri hazırlanabilir
   - Custom roslyn analyzer ile kod dönüşüm asistanı oluşturulabilir
   - Solution-wide search/replace + code review kombinasyonu ile güvenli değişim

6. **Sıkı Test Önlemleri**:
   - Her değişiklik öncesi ve sonrası test case'ler hazırlanmalı
   - Değişiklik sonrası tüm endpoint'ler için smoke test
   - Kritik iş akışı testleri
   - Veritabanı veri bütünlüğü ve migration geri alma testleri

Bu prosedür, kod tabanındaki Türkçe/İngilizce isim karmaşasının çözülmesi ve istikrarlı bir şekilde İngilizce teknik kodlara geçiş yapılması için kullanılacaktır. 