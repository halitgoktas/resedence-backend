# ResidenceManagement Backend Analizi

## Mimari Yapı

Mekik Residence Manager projesi backend bileşeni Clean Architecture / Onion Architecture prensiplerine uygun olarak tasarlanmış ve aşağıdaki katmanlardan oluşmaktadır:

### 1. Core Katmanı (Domain Layer)
Domain modellerini, temel entity'leri ve iş mantığını içerir. Diğer katmanlara bağımlılığı yoktur.

- **Entities**: Veritabanı entity'leri burada tanımlanır
  - `BaseEntity.cs`: Tüm entity'lerin temel sınıfı
  - Domain modellerinin tanımlandığı sınıflar (Company, Branch, Apartment, Block, Resident vb.)
- **Interfaces**: Repository ve servis arayüzleri
  - `IRepository<T>`: Temel repository arayüzü
  - `IGenericRepository<T>`: Generic repository arayüzü
  - `IUnitOfWork`: Transaction yönetimi için arayüz
  - Servis arayüzleri (IUserService, IResidentService vb.)
- **DTOs**: Veri transfer nesneleri
- **Enums**: Sabit değerler
- **Common**: Ortak kullanılan sınıflar ve arayüzler
  - `ITenant`: Multi-tenant yapı için arayüz

### 2. Infrastructure Katmanı
Core katmanındaki arayüzlerin implementasyonlarını ve veritabanı erişimini içerir.

- **Data**:
  - **Context**: Entity Framework DbContext sınıfları
  - **Repositories**: Repository pattern implementasyonları
    - `GenericRepository<T>`: Generic repository implementasyonu
    - Diğer repository implementasyonları (UserRepository, ResidentRepository vb.)
  - **Configurations**: Entity konfigürasyonları
  - **Migrations**: Veritabanı migrasyonları
- **Services**: Servis implementasyonları
- **Security**: Kimlik doğrulama ve yetkilendirme
- **Email**: E-posta gönderimi
- **SMS**: SMS gönderimi
- **Logging**: Loglama mekanizması

### 3. Application Katmanı
İş mantığı ve kullanım durumlarını içerir. Core ve Infrastructure katmanları arasında bir aracı görevi görür.

- **Services**: Uygulama servisleri
- **Behaviors**: Çapraz kesişen ilgiler (Cross-cutting concerns)
- **Validators**: Veri doğrulama
- **MappingProfiles**: AutoMapper profilleri

### 4. API Katmanı (Presentation Layer)
API controller'ları ve endpoint'leri içerir. İstemcilerle iletişimi sağlar.

- **Controllers**: API Controller'ları
  - `BaseApiController`: Tüm controller'lar için temel sınıf
  - `V1/`: API versiyonu 1'e ait controller'lar
  - `V2/`: API versiyonu 2'ye ait controller'lar
- **Middleware**: API isteklerini işleyen ara yazılımlar
- **Filters**: Action filter'lar
- **Extensions**: Servis konfigürasyonları için extension metotları

## Veritabanı Yapısı

Projede SQL Server veritabanı kullanılmaktadır ve aşağıdaki özelliklerle tasarlanmıştır:

### Multi-tenant Yapı
- Her entity `BaseEntity` sınıfından türetilerek `CompanyId` ve `BranchId` alanlarına sahiptir
- DbContext seviyesinde global query filtreleme ile tenant bazlı veri izolasyonu sağlanır
- Her sorgu ve işlem tenant bazlı olarak filtrelenir

### Temel Entity Yapısı
- **BaseEntity**: Tüm entity'lerin temel sınıfı
  - `Id`: Birincil anahtar
  - `CompanyId`: Firma ID (tenant)
  - `BranchId`: Şube ID (tenant)
  - `CreatedDate`, `CreatedBy`: Oluşturma bilgileri
  - `UpdatedDate`, `UpdatedBy`: Güncelleme bilgileri
  - `IsDeleted`, `DeletedDate`, `DeletedBy`: Soft delete için alanlar
  - `IsActive`: Aktiflik durumu

### Repository Pattern
- `GenericRepository<T>`: Temel CRUD işlemleri
- Entity'e özel repository'ler özel sorgular için
- `IUnitOfWork`: Transaction yönetimi

## Kimlik Doğrulama ve Yetkilendirme

- JWT tabanlı kimlik doğrulama
- Role bazlı yetkilendirme
- Refresh token mekanizması
- API endpoint'leri için yetkilendirme kontrolü

## API Versiyonlama

- URL path bazlı versiyonlama (`/api/v1/`, `/api/v2/`)
- Controller'lar versiyon klasörlerinde organize edilmiş 