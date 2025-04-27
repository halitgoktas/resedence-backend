# Residence Management Projesi Teknik Dokümantasyon

## 1. Genel Bakış

Residence Management projesi, rezidans ve site yönetimini kolaylaştırmak amacıyla geliştirilmiş çok katmanlı bir .NET 8 Web API uygulamasıdır. Proje, Clean Architecture prensiplerini takip etmekte olup, DDD (Domain-Driven Design) yaklaşımını benimsemektedir. Bu doküman, projenin teknik yapısını incelemek ve sorunları çözmek isteyen geliştiriciler için hazırlanmıştır.

## 2. Proje Mimarisi

Proje, Clean Architecture prensiplerine uygun olarak aşağıdaki katmanlardan oluşmaktadır:

### 2.1. Katmanlar

- **ResidenceManagement.Core**: Projenin merkezi katmanıdır. Domain modelleri, interfaces, enums ve temel iş mantığı burada yer alır.
- **ResidenceManagement.Infrastructure**: Veritabanı işlemleri, veritabanı konfigürasyonları ve harici servis entegrasyonları burada yer alır.
- **ResidenceManagement.API**: HTTP isteklerini karşılayan API katmanıdır. Controller'lar ve DTO'lar burada bulunur.
- **ResidenceManagement.Application**: İş mantığının uygulandığı katmandır. Servis implementasyonları burada yer alır.
- **ResidenceManagement.Data**: Veri erişim katmanıdır. Repository implementasyonları ve DbContext burada yer alır.
- **ResidenceManagement.Services**: Ortak servisler ve yardımcı fonksiyonlar burada yer alır.

### 2.2. Proje Bağımlılık Yönü

Bağımlılık yönü, Clean Architecture prensiplerine uygun olarak dışarıdan içeriye doğrudur:

```
API -> Application -> Core <- Infrastructure -> Data
```

## 3. Klasör Yapısı ve İçerikleri

### 3.1. Backend/src

```
Backend/src/
├── ResidenceManagement.Core/             # Çekirdek domain katmanı
├── ResidenceManagement.Infrastructure/    # Altyapı katmanı
├── ResidenceManagement.API/              # API katmanı
├── ResidenceManagement.Application/      # Uygulama katmanı
├── ResidenceManagement.Data/             # Veri erişim katmanı
├── ResidenceManagement.Services/         # Ortak servisler
```

### 3.2. ResidenceManagement.Core

```
ResidenceManagement.Core/
├── Models/                   # Domain modelleri
│   ├── Base/                 # Temel model sınıfları (BaseEntity, BaseLookupEntity, vb.)
│   ├── System/               # Sistem modelleri (User, Role, UserRole, vb.)
│   ├── Lookup/               # Referans modelleri (Site, Building, Apartment, vb.)
│   ├── Transaction/          # İşlem modelleri
├── Interfaces/               # Arayüz tanımları
│   ├── Repositories/         # Repository arayüzleri
│   ├── Services/             # Servis arayüzleri
├── Enums/                    # Enumeration tanımları (UserRole, NotificationType, vb.)
├── DTOs/                     # Veri transfer nesneleri
├── Services/                 # Core servis implementasyonları
├── Validation/               # Doğrulama kuralları
├── Exceptions/               # Özel hata sınıfları
```

### 3.3. ResidenceManagement.Infrastructure

```
ResidenceManagement.Infrastructure/
├── Identity/                # Kimlik doğrulama ve yetkilendirme
├── Email/                   # E-posta servisleri
├── FileStorage/             # Dosya depolama servisleri
├── Logging/                 # Loglama servisleri
```

### 3.4. ResidenceManagement.API

```
ResidenceManagement.API/
├── Controllers/             # API controller'ları
├── DTOs/                    # API istekleri ve yanıtları için DTO'lar
├── Middleware/              # API middleware'leri
├── Validation/              # API validasyon kuralları
├── Filters/                 # Action filtreler
├── Program.cs               # Uygulama başlangıç noktası
├── Startup.cs               # Uygulama konfigürasyonu
```

### 3.5. ResidenceManagement.Data

```
ResidenceManagement.Data/
├── Context/                 # DbContext sınıfları
├── Migrations/              # Veritabanı migrasyonları
├── Configurations/          # Entity konfigürasyonları
├── Repositories/            # Repository implementasyonları
```

## 4. Önemli Modeller ve İlişkileri

### 4.1. Site Yönetimi Modelleri

#### 4.1.1. Site.cs
Site modeli, bir konut kompleksinin temel özelliklerini içerir:

- Konum bilgileri (Adres, İl, İlçe, Koordinatlar)
- Genel özellikler (Toplam alan, Bina sayısı, Daire sayısı)
- Sosyal tesisler ve özellikler (Havuz, spor salonu, güvenlik, vb.)
- Site yönetim bilgileri (Yönetici, iletişim, vb.)

#### 4.1.2. Building.cs
Bina modeli, site içerisindeki binaları temsil eder:

- Temel özellikler (Bina adı, blok numarası, kat sayısı, daire sayısı)
- Teknik özellikler (Asansör, jeneratör, ısıtma sistemi)
- Yönetim bilgileri (Bina yöneticisi, kapıcı, vb.)
- Güvenlik özellikleri (Kamera, giriş kontrolü)

#### 4.1.3. Block.cs
Blok modeli, binalardaki blokları temsil eder:

- Kat sayısı, daire sayısı
- Blok sorumlusu bilgileri
- Rezidans ile ilişki

#### 4.1.4. Apartment.cs
Daire modeli, binalardaki bağımsız birimleri temsil eder:

- Daire özellikleri (Kapı no, kat, alan, oda sayısı)
- Kullanım durumu (Dolu, boş, sahibi, kiracı)
- Kira ve satış bilgileri

### 4.2. Kullanıcı Yönetimi Modelleri

#### 4.2.1. User.cs
Kullanıcı modeli, sisteme giriş yapacak kişileri temsil eder:

- Kimlik bilgileri (ID, kullanıcı adı, şifre)
- Kişisel bilgiler (Ad, soyad, e-posta, telefon)
- Durum bilgileri (Aktif, pasif)

#### 4.2.2. Role.cs
Rol modeli, kullanıcıların sistemdeki yetkilerini belirler:

- Rol adı ve açıklaması
- Yetki seviyesi
- Sistem tanımlı mı (değiştirilemez)

#### 4.2.3. UserRole.cs
Kullanıcı ve rol ilişkisini tanımlayan model:

- Kullanıcı ID ve Rol ID referansları
- Multi-tenant yapı için Firma ve Şube ID'leri

## 5. Önemli Servisler

### 5.1. UserRoleTypeService
Kullanıcı rollerini yöneten servis:

- Rol tipi oluşturma, güncelleme, silme
- Rol tipine göre sorgulama
- Sistem tanımlı roller

### 5.2. AccessService
Erişim kontrolü ve giriş-çıkış yönetimi servisi:

- Giriş-çıkış kayıtları
- Erişim izinleri
- Erişim yetkilendirme
- Erişim kartları yönetimi

## 6. Multi-tenant Yapı

Proje, multi-tenant mimariye sahiptir. Her kayıt, şu alanları içerir:

- **FirmaID**: Kaydın ait olduğu firma
- **SubeID**: Kaydın ait olduğu şube

Bu yapı sayesinde, farklı firmalar ve şubeler kendi verilerini ayrı olarak yönetebilirler. Tüm sorgular ve kayıtlar bu iki alana göre filtrelenir.

## 7. Bilinen Sorunlar ve Çözüm Önerileri

### 7.1. Namespace ve Tanım Hataları

Projede, özellikle `UserRole` gibi aynı isimde birden fazla tanımlama olan durumlar mevcuttur:

- **ResidenceManagement.Core.Enums.UserRole**: Kullanıcı rollerini tanımlayan enum
- **ResidenceManagement.Core.Models.System.UserRole**: Kullanıcı ve rol ilişkisini tanımlayan sınıf

Bu durum, derleme hatalarına yol açmaktadır. Çözüm için:

1. Farklı isimler kullanmak (UserRoleEnum, UserRoleRelation vb.)
2. Namespace alias'ları kullanmak:
   ```csharp
   using EnumUserRole = ResidenceManagement.Core.Enums.UserRole;
   using ModelUserRole = ResidenceManagement.Core.Models.System.UserRole;
   ```

### 7.2. Repository Tanımları

Bazı repository interface'leri eksik veya yanlış tanımlanmıştır:

- **IUserRepository**: Bu interface eksik olabilir veya referansı doğru ayarlanmamış olabilir.
- **IUserRoleTypeRepository**: UserRoleType sınıfını referans etmektedir, ancak sınıf UserRoleTypeEnum olarak değiştirilmişse güncellenmesi gerekir.

### 7.3. Eksik Servis Implementasyonları

Bazı servis interface'lerinin implementasyonları eksik veya hatalı olabilir:

- **IUserRoleTypeService.GetRoleTypeByRoleValueAsync**: Bu metot, UserRoleTypeService sınıfında tam olarak uygulanmamış olabilir.

## 8. Geliştirme Önerileri

### 8.1. Metot İsimlendirmesi

API'de tutarlı metot isimlendirmesi için:

- **Get...**: Veri okuma operasyonları
- **Create...**: Veri oluşturma operasyonları
- **Update...**: Veri güncelleme operasyonları
- **Delete...**: Veri silme operasyonları

### 8.2. Exception Handling

Merkezi bir exception handling mekanizması kurulması önerilir:

- Özel exception sınıfları tanımlanması
- Middleware içinde global exception handling
- İstemciye anlamlı hata mesajları dönülmesi

### 8.3. Validasyon

FluentValidation kullanılarak tutarlı validasyon:

- Her DTO için validasyon sınıfları oluşturulması
- Middleware'de otomatik validasyon
- Anlamlı validasyon hata mesajları

### 8.4. Loglama

Yapılandırılmış loglama için Serilog kullanılması:

- Farklı log seviyeleri (Error, Warning, Info, Debug)
- Yapılandırılabilir log hedefleri (Dosya, Veritabanı, Elasticsearch)
- Request/Response loglaması

## 9. Deployment ve DevOps

### 9.1. CI/CD Pipeline

GitHub Actions veya Azure DevOps kullanarak CI/CD pipeline kurulması:

- Otomatik build ve test
- Statik kod analizi
- Otomatik deployment

### 9.2. Ortamlar

Farklı ortamlar için konfigürasyon yönetimi:

- Development
- Test
- Staging
- Production

### 9.3. Docker Containerization

Uygulamanın Docker konteynerlarında çalışması için yapılandırma:

- Dockerfile oluşturulması
- Docker Compose ile bağımlılıkların yönetimi
- Kubernetes ile orchestration

## 10. Kod Kalitesi ve Standartlar

### 10.1. Code Reviews

Kod kalitesini artırmak için detaylı code review süreci:

- Belirli standartların kontrol edilmesi
- Performans ve güvenlik değerlendirmesi
- Dokümantasyon yeterlilik kontrolü

### 10.2. Unit Testler

Kapsamlı unit test kapsayıcılığı:

- Service katmanı için unit testler
- Controller'lar için integration testler
- Repository'ler için mock testler

### 10.3. Dokümantasyon

API dokümantasyonu için Swagger/OpenAPI:

- Tüm endpoint'ler için açıklamalar
- Request/Response örnekleri
- Yetkilendirme gereksinimleri

## 11. Dikkat Edilmesi Gereken Dosyalar

- **namespace-hata-cozumleri.md**: Projedeki namespace ve tanım hatalarının analizi ve çözüm önerileri
- **hatalar.md**: Bilinen hatalar ve çözüm yöntemleri
- **UserRole.cs** ve **UserRoleType.cs**: Namespace çakışması olan dosyalar
- **AccessService.cs**: Karmaşık erişim kontrol mekanizmasını içeren servis

## 12. Acil Müdahale Gerektiren Sorunlar

1. **Namespace Çakışmaları**: Özellikle UserRole tanımlamaları arasındaki çakışmalar
2. **Eksik Repository Interface'leri**: IUserRepository gibi temel repository'lerin tamamlanması
3. **Servis Implementasyonları**: IUserRoleTypeService gibi önemli servislerin eksik metodlarının tamamlanması
4. **Multi-tenant Filtreleme**: Tüm metotların firma ve şube bazlı filtreleme yapmasının sağlanması

## 13. Sonuç

Residence Management projesi, .NET 8 ve Clean Architecture kullanılarak geliştirilen kapsamlı bir site yönetim sistemidir. Projenin sorunsuz çalışması için yukarıda belirtilen sorunların çözülmesi ve önerilerin dikkate alınması önem taşımaktadır.

Bu dokümantasyon, projeyi inceleyecek ve sorunları çözecek geliştirici ekibi için bir başlangıç noktası olarak hazırlanmıştır. Proje geliştikçe ve değiştikçe bu dokümantasyonun da güncellenmesi önerilir. 