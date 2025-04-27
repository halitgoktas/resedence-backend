# PROJE GELİŞİM MİMARİSİ

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin teknik mimarisini ve gelişim adımlarını detaylı olarak açıklar.

## 1. Mimari Yapı

Proje, temiz mimari (Clean Architecture) prensiplerine uygun olarak çok katmanlı bir yapıda geliştirilecektir:

### 1.1. Katmanlar ve Sorumlulukları

#### 1.1.1. API Katmanı (ResidenceManagement.API)
- **Sorumluluk:** Dış dünya ile iletişim, istek alma ve yanıt verme
- **İçerik:**
  - Controllers/ (API kontrolcüleri)
  - Middlewares/ (Özel middleware'ler)
  - Filters/ (API filtreleri)
  - DTOs/ (Veri transfer nesneleri)
  - Extensions/ (Servis uzantıları)
  - Configurations/ (API yapılandırmaları)

#### 1.1.2. Çekirdek Katmanı (ResidenceManagement.Core)
- **Sorumluluk:** İş kuralları, domain modelleri ve iş mantığı
- **İçerik:**
  - Entities/ (Veritabanı modelleri)
    - Base/ (Temel entity sınıfları)
    - Identity/ (Kimlik doğrulama modelleri)
    - Financial/ (Finansal modeller)
    - Property/ (Mülk ile ilgili modeller)
    - Activity/ (Etkinlik modelleri)
    - Integration/ (Entegrasyon modelleri)
  - Interfaces/ (Servis ve repository arayüzleri)
  - Enums/ (Sabit değer tipleri)
  - Exceptions/ (Özel istisna sınıfları)
  - Constants/ (Sabit değerler)

#### 1.1.3. Altyapı Katmanı (ResidenceManagement.Infrastructure)
- **Sorumluluk:** Dış sistemlerle entegrasyon, veritabanı işlemleri, servis uygulamaları
- **İçerik:**
  - Data/ (Veritabanı işlemleri)
    - Context/ (DbContext sınıfları)
    - Repositories/ (Repository implementasyonları)
    - Configurations/ (Entity konfigürasyonları)
    - Migrations/ (Veritabanı migrasyonları)
    - Seeding/ (Başlangıç verileri)
  - Services/ (Servis implementasyonları)
  - Caching/ (Önbellek mekanizması)
  - Logging/ (Loglama servisleri)
  - Email/ (E-posta servisleri)
  - SMS/ (SMS servisleri)
  - ExternalServices/ (Dış servisler)

#### 1.1.4. Test Katmanı (ResidenceManagement.Tests)
- **Sorumluluk:** Birim testler, entegrasyon testleri ve API testleri
- **İçerik:**
  - Unit/ (Birim testleri)
  - Integration/ (Entegrasyon testleri)
  - Fixtures/ (Test verileri)
  - Mocks/ (Mock nesneleri)

### 1.2. Katmanlar Arası İletişim

- **Bağımlılık Yönü:** API -> Infrastructure -> Core
- **Bağımlılık Enjeksiyonu:** Tüm servisler ve repository'ler için Dependency Injection kullanılacak
- **İletişim Modeli:** Interface'ler üzerinden iletişim (Loose Coupling)

## 2. Veri Erişim Mimarisi

### 2.1. Entity Framework Core ve Code First Yaklaşımı

- **ORM:** Entity Framework Core 8
- **Yaklaşım:** Code First
- **Migrations:** Otomatik migration oluşturma ve uygulama

### 2.2. Repository Pattern

- **Generic Repository:** Tüm entityler için temel CRUD işlemleri
- **Özel Repository'ler:** Entity-spesifik özel sorgular
- **Unit of Work:** Transaction yönetimi ve repository koordinasyonu

### 2.3. Multi-Tenant Yapısı

- **Tenant İzolasyonu:** FirmaID ve SubeID bazlı veri filtreleme
- **Global Query Filters:** Otomatik tenant filtreleme
- **Veri Erişim Kontrolü:** Kullanıcının yetkili olduğu tenant verilerini görebilmesi

## 3. API ve Servis Mimarisi

### 3.1. RESTful API Tasarımı

- **Endpoint Standartları:** Kaynak odaklı URL yapısı
- **HTTP Metotları:** GET, POST, PUT, DELETE, PATCH
- **Durum Kodları:** Standart HTTP durum kodları kullanımı
- **Versiyonlama:** URL tabanlı (v1, v2, ...)

### 3.2. Servis Katmanı

- **Servis Soyutlaması:** Interface ve implementasyon ayrımı
- **Validasyon:** FluentValidation ile istek doğrulama
- **İş Mantığı:** Tüm iş kuralları servis katmanında

### 3.3. DTO ve AutoMapper

- **Veri Transfer Nesneleri:** İstek ve yanıt modelleri
- **Otomatik Eşleme:** AutoMapper ile entity ve DTO dönüşümleri

## 4. Güvenlik Mimarisi

### 4.1. Kimlik Doğrulama ve Yetkilendirme

- **JWT Tabanlı Auth:** Token bazlı kimlik doğrulama
- **Rol Tabanlı Yetkilendirme:** Kullanıcı rolleri ve izinleri
- **Claims Bazlı Yetkilendirme:** Detaylı yetki kontrolleri

### 4.2. Veri Güvenliği

- **Şifreleme:** Hassas verilerin şifrelenmesi
- **Veri Maskeleme:** Kritik bilgilerin API yanıtlarında maskelenmesi
- **HTTPS:** Tüm API iletişiminde HTTPS kullanımı

## 5. Loglama ve İzleme

- **Yapılandırılmış Loglama:** Serilog ile yapılandırılmış log kayıtları
- **Log Seviyeleri:** Debug, Info, Warning, Error, Fatal
- **Log Hedefleri:** Dosya, veritabanı ve uygulama içi
- **Performans İzleme:** API yanıt süreleri ve kaynak kullanımı izleme

## 6. Geliştirme Adımları

### 6.1. Ön Hazırlık (Hafta 1)
- [x] Proje gereksinimlerinin analizi
- [x] Proje klasör yapısının oluşturulması
- [x] Dokümantasyon dosyalarının hazırlanması
- [ ] Solution ve proje yapısının oluşturulması
- [ ] NuGet paketlerinin yüklenmesi
- [ ] Temel DbContext yapılandırması
- [ ] Dependency Injection yapılandırması

### 6.2. Temel Veri Modellerinin Oluşturulması (Hafta 2-3)
- [ ] BaseEntity sınıfının oluşturulması
- [ ] BaseLookupEntity sınıfının oluşturulması
- [ ] BaseTransactionEntity sınıfının oluşturulması
- [ ] Multi-tenant yapısı için FirmaID ve SubeID alanlarının eklenmesi
- [ ] Ana veri modelleri:
  - [ ] Mülk Yönetimi: Site, Blok, Daire modelleri
  - [ ] Kullanıcı Yönetimi: Kullanıcı, Rol, İzin modelleri
  - [ ] Finansal İşlemler: Aidat, Harcama, Ödeme modelleri
  - [ ] Rezervasyon ve Etkinlik modelleri
  - [ ] Entegrasyon modelleri
- [ ] Entity konfigürasyonlarının oluşturulması
- [ ] İlişkilerin tanımlanması
- [ ] İlk migration'ın oluşturulması
- [ ] Seed verilerinin hazırlanması

### 6.3. Repository Katmanının Geliştirilmesi (Hafta 3-4)
- [ ] IRepository interface'inin tanımlanması
- [ ] BaseRepository sınıfının oluşturulması
- [ ] Multi-tenant filtreleme mekanizmasının eklenmesi
- [ ] Entity-spesifik repository'lerin oluşturulması:
  - [ ] IUserRepository ve UserRepository
  - [ ] ISiteRepository ve SiteRepository
  - [ ] IApartmentRepository ve ApartmentRepository
  - [ ] IFinancialRepository ve FinancialRepository
- [ ] Unit of Work pattern'in uygulanması
- [ ] Repository unit testlerinin yazılması

### 6.4. Servis Katmanının Geliştirilmesi (Hafta 4-6)
- [ ] IBaseService interface'inin tanımlanması
- [ ] BaseService sınıfının oluşturulması
- [ ] Entity-spesifik servislerin oluşturulması:
  - [ ] IUserService ve UserService
  - [ ] IAuthService ve AuthService
  - [ ] ISiteService ve SiteService
  - [ ] IApartmentService ve ApartmentService
  - [ ] IFinancialService ve FinancialService
- [ ] FluentValidation validasyon sınıflarının oluşturulması
- [ ] İş mantığı kurallarının uygulanması
- [ ] Exception handling mekanizmasının oluşturulması
- [ ] Servis unit testlerinin yazılması

### 6.5. API Katmanının Geliştirilmesi (Hafta 6-8)
- [ ] BaseController sınıfının oluşturulması
- [ ] JWT token tabanlı kimlik doğrulama sisteminin kurulması
- [ ] Role tabanlı yetkilendirme sisteminin kurulması
- [ ] API DTO'larının oluşturulması
- [ ] AutoMapper profillerinin oluşturulması
- [ ] Controller sınıflarının oluşturulması:
  - [ ] AuthController
  - [ ] UserController
  - [ ] SiteController
  - [ ] BuildingController
  - [ ] ApartmentController
  - [ ] ResidentController
  - [ ] FinancialController
  - [ ] ReservationController
- [ ] API endpoint'lerinin oluşturulması
- [ ] API versiyonlama yapısının kurulması
- [ ] Swagger/OpenAPI dokümantasyonunun eklenmesi
- [ ] Global exception handling middleware'in eklenmesi
- [ ] API filtrelerinin oluşturulması
- [ ] API entegrasyon testlerinin yazılması

### 6.6. İleri Özellikler ve Entegrasyonlar (Hafta 8-10)
- [ ] İki faktörlü kimlik doğrulama (2FA) desteği
- [ ] Çoklu dil desteği
- [ ] Çoklu para birimi desteği
- [ ] Otomatik kur çekme ve dönüştürme servisi
- [ ] E-posta gönderim servisi entegrasyonu
- [ ] SMS gönderim servisi entegrasyonu
- [ ] Dosya yükleme ve saklama servisi
- [ ] PDF ve Excel çıktı oluşturma servisi
- [ ] Ödeme sistemleri entegrasyonu
- [ ] Redis ile cache mekanizmasının uygulanması
- [ ] Hangfire ile background job'ların oluşturulması

### 6.7. Test ve Optimizasyon (Hafta 10-12)
- [ ] Unit testlerin tamamlanması
- [ ] Integration testlerin tamamlanması
- [ ] API testlerin tamamlanması
- [ ] Sorgu optimizasyonlarının yapılması
- [ ] N+1 sorgu probleminin çözümü
- [ ] Veritabanı indekslerinin oluşturulması
- [ ] API response sürelerinin optimize edilmesi
- [ ] Güvenlik testlerinin yapılması
- [ ] Code review ve refactoring
- [ ] Performans testleri

## 7. Teknoloji Seçimleri

### 7.1. Backend Teknolojileri
- **Ana Framework:** .NET 8 Web API (C#)
- **ORM:** Entity Framework Core 8
- **Veritabanı:** MSSQL Server
- **API Dokümantasyonu:** Swagger/OpenAPI
- **Validasyon:** FluentValidation
- **Nesne Eşleme:** AutoMapper
- **Kimlik Doğrulama:** JWT Authentication
- **Loglama:** Serilog
- **Önbellekleme:** Redis
- **Arkaplan İşleri:** Hangfire
- **Birim Test:** xUnit
- **Mock:** Moq
- **API Test:** Postman / REST Client

### 7.2. Dış Servisler ve Entegrasyonlar
- **E-posta Servisi:** SMTP / SendGrid
- **SMS Servisi:** Twilio / Netgsm
- **Dosya Depolama:** Azure Blob Storage / AWS S3
- **Ödeme İşlemleri:** İyzico / PayTR
- **Kimlik Doğrulama:** KPS (Kimlik Paylaşım Sistemi)
- **Adres Doğrulama:** UAVT (Ulusal Adres Veri Tabanı)

Bu teknik mimari ve gelişim planı, projenin başarılı bir şekilde uygulanması için detaylı bir yol haritası sağlamaktadır. Geliştirme süreci boyunca, ilerlemeler kaydedildikçe bu dokümanda güncellemeler yapılacaktır. 