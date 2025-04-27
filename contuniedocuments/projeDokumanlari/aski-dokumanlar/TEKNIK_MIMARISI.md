# Rezidans ve Site Yönetim Sistemi - Teknik Mimari

Bu belge, Rezidans ve Site Yönetim Sistemi projesinin teknik mimarisini ve kullanılan teknolojileri açıklamaktadır.

## Genel Teknik Mimari

Rezidans ve Site Yönetim Sistemi, modern ve ölçeklenebilir bir mimari üzerine kurulmuştur. Sistem, aşağıdaki ana bileşenlerden oluşmaktadır:

1. **Backend API** - .NET 8 Web API
2. **Frontend Web Uygulaması** - React.js ve Material UI
3. **Mobil Uygulama** - React Native
4. **Veritabanı** - Microsoft SQL Server

Bu bileşenler, farklı katmanlarda tasarlanarak ölçeklenebilirlik, bakım kolaylığı ve performans sağlamak üzere optimize edilmiştir.

## Backend Mimari

### Teknoloji Stack'i

- **Çerçeve:** .NET 8 Web API
- **Dil:** C#
- **Veritabanı:** Microsoft SQL Server
- **ORM:** Entity Framework Core 8
- **Kimlik Doğrulama:** JWT Token tabanlı kimlik doğrulama
- **Belge Yönetimi:** Swagger / OpenAPI

### Katmanlı Mimari

Backend, aşağıdaki katmanlardan oluşan N-katmanlı bir mimari kullanmaktadır:

1. **ResidenceManagement.Core**
   - Domain modelleri
   - Interface tanımları
   - DTOs (Data Transfer Objects)
   - Validation kuralları
   - Sabitler ve yardımcı sınıflar
   - Exception sınıfları

2. **ResidenceManagement.Data**
   - DbContext sınıfları
   - Entity Configurations
   - Repositories
   - Migrations
   - Seed Data

3. **ResidenceManagement.Infrastructure**
   - Harici servislere entegrasyonlar (KBS, Email, SMS, vb.)
   - Kimlik doğrulama ve yetkilendirme servisleri
   - Loglama servisleri
   - Dosya yönetimi
   - Döviz kuru servisleri

4. **ResidenceManagement.API**
   - Controllers
   - Filters
   - Middleware
   - Dependency Injection konfigürasyonu
   - API versiyonlama

5. **ResidenceManagement.Tests**
   - Unit testler
   - Integration testler
   - Controller testleri

### Design Patterns

Backend uygulamasında aşağıdaki design pattern'ler kullanılmaktadır:

- **Repository Pattern** - Veri erişim katmanını soyutlamak için
- **Unit of Work Pattern** - Transaction yönetimi için
- **Dependency Injection** - Loosely coupled mimarisi elde etmek için
- **Factory Pattern** - Kompleks nesnelerin oluşturulması için
- **Strategy Pattern** - Algoritmaların runtime'da değiştirilebilmesi için
- **Mediator Pattern** - Command Query Responsibility Segregation (CQRS)
- **Specification Pattern** - İş kurallarının encapsulation'ı için

### API Mimarisi

- **RESTful API** prensiplerini takip eder
- **Versiyonlama** - URL temelli API versiyonlaması (api/v1, api/v2 vb.)
- **HATEOAS** - Hypermedia as the Engine of Application State
- **Swagger / OpenAPI** - API dokümantasyonu
- **JWT (JSON Web Token)** - Kimlik doğrulama ve yetkilendirme
- **Role-Based Access Control (RBAC)** - Rol bazlı yetkilendirme
- **API Rate Limiting** - DDoS önlemi için istek sınırlaması

### Multi-tenant Yapı

Sistem, çok kiracılı (multi-tenant) mimariye sahiptir. Her kiracı (firma), sistemde kendi verilerine erişebilir ancak diğer kiracıların verilerine erişemez. Bu yapı aşağıdaki şekilde uygulanmıştır:

- Tüm tablolarda **FirmaID** ve **SubeID** alanları bulunur
- Repository katmanında **otomatik tenant filtreleme** mekanizması
- API katmanında tenant doğrulama middlewares'i
- Her istek için tenant bilgisi validasyonu

### İki Faktörlü Kimlik Doğrulama (2FA)

Sistem, güvenliği artırmak için iki faktörlü kimlik doğrulama desteği sunar:

- **TOTP (Time-based One-Time Password)** algoritması
- QR kodu ile kolay kurulum
- Yedek kodlar ile acil erişim
- Uygulama veya SMS tabanlı doğrulama seçenekleri

### Entegrasyonlar

Backend, aşağıdaki harici sistemlerle entegrasyon sağlar:

- **KBS (Kimlik Bildirim Sistemi)** - Kimlik bildirimleri için
- **TCMB** - Döviz kurları için
- **SMS Sağlayıcıları** - Bildirimler için
- **E-posta Sağlayıcıları** - Bildirimler için
- **Ödeme Sistemleri** - Online ödemeler için
- **E-Fatura ve E-Arşiv** - Yasal gereklilikler için
- **Banka Entegrasyonları** - Otomatik ödeme işlemleri için

### Çoklu Dil ve Para Birimi Desteği

- **i18n** - Yerelleştirme için altyapı
- Türkçe, İngilizce, Almanca, Rusça, Arapça ve Farsça dil desteği
- Çoklu para birimi desteği (TL, EUR, USD, GBP)
- Otomatik döviz kuru güncelleme ve çevirme altyapısı
- Kullanıcı ve firma bazlı dil ve para birimi tercihleri

## Frontend Mimari

### Teknoloji Stack'i

- **Çerçeve:** React.js
- **Dil:** TypeScript
- **UI Kütüphanesi:** Material UI
- **State Yönetimi:** Context API
- **Form Yönetimi:** Formik ve Yup
- **HTTP İstekleri:** Axios
- **Routing:** React Router
- **Veritablosu:** React Table
- **Tarih İşlemleri:** date-fns
- **Grafik Kütüphanesi:** Recharts

### Komponent Yapısı

Frontend, aşağıdaki yaklaşımları takip eder:

- **Component-Based Architecture**
- Atomic Design prensipleri
- Komponentlerin yeniden kullanılabilirliği
- Tek sorumluluk prensibi (bir komponent bir iş yapar)
- PropTypes veya TypeScript aracılığıyla tip kontrolü

### Dizin Yapısı

```
/src
  /api            # API istekleri
  /assets         # Statik dosyalar (resimler, fontlar)
  /components     # Yeniden kullanılabilir komponentler
    /common       # Genel komponentler
    /forms        # Form komponentleri
    /layout       # Yerleşim komponentleri
    /tables       # Tablo komponentleri
  /config         # Konfigürasyon dosyaları
  /contexts       # Context API sağlayıcıları
  /hooks          # Custom React Hooks
  /i18n           # Çeviri dosyaları
  /pages          # Sayfalar
  /routes         # Rota tanımları
  /services       # API servis katmanı
  /styles         # Stil dosyaları
  /types          # TypeScript tipleri
  /utils          # Yardımcı fonksiyonlar
```

### State Yönetimi

Frontend, state yönetimi için Context API kullanır:

- **AuthContext** - Kimlik doğrulama durumu
- **UIContext** - UI durumu (tema, bildirimler, vb.)
- **SettingsContext** - Kullanıcı tercihleri
- **Domain-specific contexts** - Her modül için özel contextler

### Çoklu Dil Desteği

Frontend, i18next kütüphanesi ile çoklu dil desteği sağlar:

- Türkçe, İngilizce, Almanca, Rusça, Arapça ve Farsça dil desteği
- Dil dosyaları JSON formatında
- Dil değiştirme özelliği
- Tarih, saat ve para birimi formatları yerelleştirilmiş

### Tema ve Stilizasyon

- Material UI theming
- Dark/Light mod desteği
- Firma/site bazlı özelleştirilebilir temalar
- Responsive tasarım (mobil, tablet, masaüstü)

### Form Yönetimi ve Validasyon

- Formik ile form state yönetimi
- Yup ile form validasyonu
- Custom form komponentleri
- Server-side validasyon entegrasyonu

### Authentication ve Authorization

- JWT token tabanlı kimlik doğrulama
- Access ve refresh token mekanizması
- Rol bazlı yetkilendirme
- Kullanıcı tercihlerinin localStorage ve/veya server'da saklanması

## Mobil Uygulama Mimari

### Teknoloji Stack'i

- **Çerçeve:** React Native
- **Dil:** TypeScript
- **UI Kütüphanesi:** React Native Paper
- **State Yönetimi:** Context API
- **Navigasyon:** React Navigation
- **HTTP İstekleri:** Axios
- **Form Yönetimi:** Formik ve Yup

### Temel Özellikler

- Cross-platform (iOS ve Android)
- Native UI komponentleri
- Push bildirimler
- Offline çalışma modu
- Kamera ve konum entegrasyonu
- Biyometrik kimlik doğrulama

## Veritabanı Mimari

### Teknoloji

- **RDBMS:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Migration Yönetimi:** EF Core Migrations
- **Seed Data:** DbInitializer

### Veritabanı Tasarımı

- Normalizasyon prensipleri
- İndeksleme stratejileri
- Transaction yönetimi
- Concurrency control
- Multi-tenant veri ayrımı

## Güvenlik

### Kimlik Doğrulama ve Yetkilendirme

- JWT token tabanlı kimlik doğrulama
- Rol bazlı yetkilendirme
- İki faktörlü kimlik doğrulama (2FA)
- Parola güvenlik politikaları
- Access ve refresh token mekanizması

### Veri Güvenliği

- HTTPS/TLS ile şifrelenmiş iletişim
- Hassas verilerin şifrelenmesi
- SQL injection önleme
- XSS (Cross-Site Scripting) önleme
- CSRF (Cross-Site Request Forgery) koruması
- CORS (Cross-Origin Resource Sharing) politikaları

### Denetim ve İzleme

- Kullanıcı aktivite logları
- Güvenlik olayları logları
- Başarısız giriş denemeleri takibi
- IP bazlı kısıtlamalar
- Rate limiting

## Performans Optimizasyonu

### Backend Optimizasyonları

- Veritabanı sorgu optimizasyonu
- Indeksleme stratejileri
- Önbellek mekanizması
- Asenkron işlem yönetimi
- Yük dengeleme

### Frontend Optimizasyonları

- Lazy loading
- Code splitting
- Memoizasyon (useMemo, useCallback)
- Gereksiz render'ların önlenmesi
- Bundle size optimizasyonu
- Image optimizasyonu

## CI/CD ve Deployment

### Continuous Integration

- Otomatik build
- Otomatik testler
- Kod kalite kontrolleri
- Static code analysis

### Continuous Deployment

- Staging ve production ortamları
- Blue-green deployment
- Geri alma (rollback) mekanizması
- Zero-downtime deployment

### Hosting ve Infrastructure

- Azure veya AWS tabanlı hosting
- Docker containerization
- Veritabanı backup ve restore stratejileri
- Scalability için auto-scaling

## Logging ve Monitoring

### Logging Stratejisi

- Structured logging
- Log seviyesi ayarlamaları
- Log rotasyonu ve arşivleme
- Sensitive data masking

### Monitoring

- Application performance monitoring
- Error tracking
- Real-time alerting
- Uptime monitoring
- Resource utilization monitoring

## Sonuç

Rezidans ve Site Yönetim Sistemi, modern teknolojiler ve design pattern'ler kullanılarak ölçeklenebilir, güvenli ve bakımı kolay bir mimari üzerine inşa edilmiştir. Multi-tenant yapısı sayesinde, birden fazla site yönetim şirketine hizmet verebilir ve her şirket kendi verilerine güvenli bir şekilde erişebilir. Sistem, değişen gereksinimlere uyum sağlayabilecek şekilde modüler yapıda tasarlanmıştır. 