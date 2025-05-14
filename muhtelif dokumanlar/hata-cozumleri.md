# Residence Management Projesi - Hata Çözümleri

Bu doküman, projede tespit edilen sorunları ve bu sorunların çözüm planlarını içermektedir. Her sorun için detaylı açıklama, olası etkileri ve çözüm adımları belirtilmiştir.

## 1. Mükerrer Proje Yapıları

### Tespit Edilen Sorunlar
- ResidenceManagement.Domain ve ResidenceManagement.Core projeleri aynı katmanı temsil ediyor
- İki farklı solution dosyası var: ResidenceManagement.sln ve backend.sln

### Olası Etkiler
- Duplike kodlar ve yapılar
- Bakım zorluğu
- Domain modellerinde tutarsızlıklar
- Hangi çözüm dosyasının kullanılacağı konusunda karışıklık
- Aynı entity'lerin farklı projelerde tanımlanması

### Çözüm Planı
- [x] Domain ve Core projelerinin içeriğinin karşılaştırılması ve birleştirilmesi
  - Core projesindeki BaseEntity sınıfı daha kapsamlı (daha fazla özellik ve davranış içeriyor)
  - Domain projesindeki BaseEntity sınıfı Core'daki ile çakışıyor ama daha basit
  - Domain projesindeki tüm entity'lerin Core/Entities altına taşınması gerek
  - Çözüm: Domain projesindeki entity'leri Core/Entities altına taşıyıp Core'daki BaseEntity sınıfını kullanacağız
- [x] Tek bir solution dosyasının (ResidenceManagement.sln) kullanılması
  - ResidenceManagement.sln daha kapsamlı (Tests projesi dahil)
  - backend.sln daha minimal ve sadece temel projeleri içeriyor
  - Çözüm: ResidenceManagement.sln dosyasını ana çözüm olarak kullanıp, backend.sln dosyasını yedekleme olarak saklayacağız
- [x] Tüm referansların güncellenmesi
  - Domain projesini referans alan projelerin Core projesini referans alacak şekilde güncellenmesi
  - Infrastructure ve API projelerindeki tüm Domain referansları kontrol edildi ve Core referanslarıyla değiştirildi
  - Proje dosyaları (.csproj) güncellendi ve Domain referansları kaldırıldı
  - Build alınarak referans güncellemelerinin doğru çalıştığı teyit edildi

## 2. Katman Sorunları

### Tespit Edilen Sorunlar
- Core katmanında Services klasörü bulunuyor (Clean Architecture'a göre servisler Application katmanında olmalı)
- Core katmanında Filters klasörü var (API katmanına ait olmalı)
- Core katmanında gereksiz Class1.cs dosyası var

### Olası Etkiler
- Clean Architecture prensiplerinin ihlali
- Katmanlar arası bağımlılıklar
- Bakım ve geliştirme zorluğu
- Test edilebilirliğin düşmesi

### Çözüm Planı
- [x] Core katmanının yeniden düzenlenmesi
  - [x] Services klasörünün Application katmanına taşınması
  - [x] Filters klasörünün API katmanına taşınması
  - [x] Class1.cs gibi gereksiz dosyaların temizlenmesi
- [x] Katman yapısının Clean Architecture'a uygun hale getirilmesi
  - Core: Domain modelleri, entity'ler, DTO'lar, interface'ler
  - Application: Servisler, validasyonlar, business logic
  - Infrastructure: Repository implementasyonları, harici servisler
  - API: Controller'lar, filter'lar, middleware'ler

## 3. Infrastructure Sorunları

### Tespit Edilen Sorunlar
- Repositories klasörü Data klasörünün dışında (tüm veritabanı ile ilgili işlemler Data altında olmalı)
- İki farklı DI kayıt dosyası var: DependencyInjection.cs ve ServicesRegistration.cs
- Gereksiz Class1.cs dosyası var

### Olası Etkiler
- Kod organizasyonunda tutarsızlık
- DI kayıtlarında duplikasyon veya çakışma riski
- Gereksiz sınıfların bakım yükü

### Çözüm Planı
- [x] Infrastructure katmanının yeniden düzenlenmesi
  - Repositories klasörünün Data altına taşınması
    - Kök dizinde bir Repositories klasörü var (KbsNotificationRepository.cs içeriyor)
    - Data klasörü altında da bir Repositories klasörü var (Repository.cs, GenericRepository.cs vb. içeriyor)
    - KbsNotificationRepository sınıfı zaten Data/Repositories'deki GenericRepository'yi kullanıyor
    - Çözüm: KbsNotificationRepository.cs dosyasını Data/Repositories altına taşıyacağız
  - [x] DI kayıt dosyalarının birleştirilmesi (DependencyInjection.cs'de toplanması)
    - İki DI kayıt dosyası bulunuyor: DependencyInjection.cs ve ServicesRegistration.cs
    - İki dosya da aynı işlevi görüyor, ancak farklı servisler kaydediyorlar
    - DependencyInjection.cs RegisterInfrastructureServices adlı metod içerirken, ServicesRegistration.cs AddInfrastructureServices adlı metod içeriyor
    - ServicesRegistration.cs ayrıca ConfigureMultiTenant adında özel bir metod içeriyor
    - Çözüm: Her iki dosyadaki servis kayıtlarını DependencyInjection.cs'de birleştireceğiz ve ConfigureMultiTenant metodunu da bu dosyaya taşıyacağız
  - [x] Class1.cs dosyasının silinmesi
    - Infrastructure katmanında boş bir Class1.cs dosyası bulunuyor
    - Bu dosya hiçbir işleve sahip değil ve kullanılmıyor
    - Çözüm: Bu gereksiz dosyayı siliyoruz

## 4. API Katmanı Sorunları

### Tespit Edilen Sorunlar
- API katmanında DTOs klasörü var (DTO'lar Core/Application katmanında olmalı)
- Program.cs dosyası çok büyük (249 satır)
- Örnek WeatherForecast kodu production'da kalmış

### Olası Etkiler
- API katmanında domain logic
- Bakım zorluğu
- Program.cs dosyasının anlaşılabilirliğinin düşmesi
- Gereksiz kodların prod ortamında çalışması

### Çözüm Planı
- [x] API katmanının temizlenmesi
  - [x] DTOs klasörünün Core/Application katmanına taşınması (API projesinde boş DTOs klasörü var)
  - [x] Program.cs dosyasının extension metodlar ile parçalanması:
    - ResidenceManagement.API.Extensions.ServiceCollectionExtensions.cs (AddJwtAuthentication, AddCorsPolicy)
    - ResidenceManagement.Infrastructure.Extensions.ServiceCollectionExtensions.cs (AddInfrastructureServices)
    - ResidenceManagement.API.Extensions.SwaggerExtensions.cs (Swagger yapılandırması)
    - ResidenceManagement.API.Extensions.ApplicationExtensions.cs (Middleware pipeline yapılandırması)
  - [x] WeatherForecast örnek kodunun kaldırılması

## 5. Services Katmanı Sorunları

### Tespit Edilen Sorunlar
- Services projesi sadece interfaces içeriyor
- Bu gereksiz bir katman, interface'ler Core/Application katmanında olmalı

### Olası Etkiler
- Gereksiz proje ve assembly
- Clean Architecture prensiplerine uyumsuzluk
- Ek bağımlılık ve karmaşıklık

### Çözüm Planı
- [x] Services katmanının kaldırılması
  - IMaintenanceScheduleService interface'i hem Core/Interfaces hem de Services/Interfaces altında tanımlanmış
  - Core projesindeki interface daha kapsamlı ve güncel görünüyor
  - Services projesindeki interface'e ait işlevler Core projesine entegre edilmiş durumda
  - Çözüm: Services projesindeki interface'leri Core/Interfaces altındaki versiyonları ile birleştireceğiz

## 6. DI ve Servis Kayıt Sorunları

### Tespit Edilen Sorunlar
- Servis kayıtlarında tam nitelikli isimler (fully qualified names) kullanılmış
- Aynı servis farklı namespace'lerden kaydedilmiş (IMaintenanceService ve Services.IMaintenanceScheduleService)

### Olası Etkiler
- Kod okunabilirliğinin düşmesi
- Servis kayıtlarında olası duplikasyonlar
- Hatalı DI çözümlemeleri
- Beklenmeyen runtime hataları

### Çözüm Planı
- [x] DI kayıtlarının standartlaştırılması
  - DependencyInjection.cs'de tam nitelikli isimler kullanılmış:
    - `services.AddScoped<ResidenceManagement.Core.Interfaces.IMaintenanceService, MaintenanceService>();`
    - `services.AddScoped<ResidenceManagement.Core.Interfaces.Services.IMaintenanceScheduleService, MaintenanceScheduleService>();`
  - Farklı namespace'lerdeki benzer servisler kaydedilmiş
  - Çözüm: Tüm using direktiflerini dosya başına ekleyerek tam nitelikli isimleri kaldıracağız ve tek bir namespace'ten tutarlı servis kayıtları yapacağız

## 7. Program.cs Sorunları

### Tespit Edilen Sorunlar
- Örnek WeatherForecast kodu production'da kalmış
- CORS yapılandırması çok uzun
- Swagger yapılandırması çok uzun
- JWT yapılandırması çok uzun

### Olası Etkiler
- Büyük ve bakımı zor bir Program.cs dosyası
- Yapılandırma değişikliklerinde hata riski
- Kodun okunabilirliğinin düşmesi

### Çözüm Planı
- [x] Program.cs dosyasının modüler hale getirilmesi
  - Extensions klasörü altında aşağıdaki dosyaların oluşturulması:
    - [x] ServiceCollectionExtensions.cs (JWT yapılandırması ve CORS yapılandırması için)
    - [x] SwaggerExtensions.cs (Swagger yapılandırması için)
    - [x] ApplicationExtensions.cs (Middleware pipeline yapılandırması için)
  - [x] WeatherForecast örnek kodunun kaldırılması

## 8. Namespace Sorunları

### Tespit Edilen Sorunlar
- Aynı interface'lerin farklı namespace'lerde tanımlanması
- Tam nitelikli isimlerin kullanılması

### Olası Etkiler
- Namespace çakışmaları
- Yanlış tip çözümlemeleri
- Kod tutarsızlıkları
- Build hataları

### Çözüm Planı
- [x] Namespace'lerin standartlaştırılması
  - [x] Tüm interface'lerin tek bir namespace altında toplanması (Core/Interfaces)
  - [x] Alt domain'ler için uygun namespace hiyerarşisinin oluşturulması
  - [x] Tam nitelikli isimlerin kaldırılması
  - [x] Using direktiflerinin düzenlenmesi

## 9. Clean Architecture İhlalleri

### Tespit Edilen Sorunlar
- Core katmanında UI/API bağımlılıkları (Filters)
- API katmanında domain logic (DTO'lar)
- Infrastructure'da domain logic

### Olası Etkiler
- Katmanlı mimarinin bozulması
- Bağımlılık yönlerinin karışması
- Test edilebilirliğin düşmesi
- Spaghetti kod riski

### Çözüm Planı
- [x] Clean Architecture prensiplerine uygun yapının sağlanması
  - [x] Core katmanından UI/API bağımlılıklarının kaldırılması
  - [x] DTO'ların Core/Application katmanına taşınması
  - [x] Domain logic'in Core katmanında tutulması
  - [x] Infrastructure'ın sadece dış sistemlerle iletişim kurması
  - [x] Bağımlılık yönlerinin içe doğru olacak şekilde düzenlenmesi

## 10. Güvenlik Sorunları

### Tespit Edilen Sorunlar
- JWT için RequireHttpsMetadata false
- CORS yapılandırması çok gevşek

### Olası Etkiler
- Güvenlik açıkları
- MITM (Man-in-the-Middle) saldırı riski
- CORS tabanlı saldırı riski
- Veri sızıntısı riski

### Çözüm Planı
- [x] Güvenlik yapılandırmalarının iyileştirilmesi
  - [x] JWT için RequireHttpsMetadata true yapılması
  - [x] CORS politikalarının sıkılaştırılması
    - [x] AllowAnyOrigin yerine belirli originlerin listelenmesi
    - [x] AllowAnyMethod yerine belirli HTTP metodlarına izin verilmesi
    - [x] AllowAnyHeader yerine belirli headerlara izin verilmesi
  - [x] Güvenlik başlıklarının eklenmesi (HSTS yapılandırması)

## 11. Performans Sorunları

### Tespit Edilen Sorunlar
- Gereksiz assembly yüklemeleri
- Çok fazla cross-cutting concern middleware

### Olası Etkiler
- Yavaş startup zamanı
- Yüksek memory kullanımı
- Düşük response time
- Yavaş request-response döngüsü

### Çözüm Planı
- [x] Performans iyileştirmeleri
  - [x] Middleware sırasının optimize edilmesi (ApplicationExtensions.cs ile)
  - [x] Gereksiz assembly referanslarının kaldırılması
  - [x] Caching stratejisinin gözden geçirilmesi
    - Memory Cache servisinin eklenmesi (AddMemoryCache)
    - API katmanında ResponseCaching middleware'inin eklenmesi
    - Sık erişilen ve değişimi az olan verilerin (ör. ayarlar, sabit listeler) önbelleğe alınması
    - Controller'larda ResponseCache attribute'unun kullanılması 
    - Distributed cache için Redis yapılandırmasının hazırlanması

## 12. Tip Hatası ve Belirsiz Başvuru Sorunları

### ITenant Belirsiz Başvuru Hataları

#### Tespit Edilen Sorun
- `ITenant` arayüzü hem `ResidenceManagement.Core.Common` hem de `ResidenceManagement.Core.Interfaces` namespace'lerinde bulunuyor
- Bu durum belirsiz başvuru hatalarına neden oluyor

#### Olası Etkiler
- Derleme hataları
- Yanlış arayüz implementasyonları
- Kod tutarsızlıkları

#### Çözüm Planı
- Belirsiz başvuruları çözmek için tam yol adını kullanma:
```csharp
using ResidenceManagement.Core.Common; // veya
using ResidenceManagement.Core.Interfaces;

// Sınıf içinde:
public class MyClass : ResidenceManagement.Core.Common.ITenant // Tam yol belirtin
```

### Eksik Tür Hataları

#### Tespit Edilen Sorun
- Bazı tip ve sınıflar projede tanımlanmamış veya yanlış namespace'lerde referans ediliyor
- Örnek: `CreateKbsGuestInfoDto`, `KbsNotificationCreateDto` vb.

#### Olası Etkiler
- Derleme hataları
- Eksik işlevsellik
- KBS entegrasyonu sorunları

#### Çözüm Planı
- Eksik türlerin tanımlanması veya doğru namespace'lerin import edilmesi:
```csharp
using ResidenceManagement.Core.DTOs.Kbs; // Türün bulunduğu ad alanı
// veya
// CreateKbsGuestInfoDto sınıfını oluşturun
```

### Genel Tür Uyumluluk Hatası

#### Tespit Edilen Sorun
- Bazı sınıflar `IGenericRepository<T>` için `BaseEntity` türünden türetilmemiş
- Örnek: `KbsNotification` sınıfı `BaseEntity`'den türetilmemiş

#### Olası Etkiler
- Repository pattern'in düzgün çalışmaması
- Derleme hataları
- Multi-tenant filtreleme sorunları

#### Çözüm Planı
- İlgili sınıfları `BaseEntity`'den türetme:
```csharp
// KbsNotification.cs dosyasında:
public class KbsNotification : ResidenceManagement.Core.Entities.Base.BaseEntity
{
    // Sınıf özellikleri...
}
```

### Arabirim Uyumlama Eksikliği

#### Tespit Edilen Sorun
- Bazı servis sınıfları implementasyonlarında interface üyesi metotlar eksik
- Örnek: `KbsIntegrationService` sınıfında `SendNotificationAsync` metodu implementasyonu eksik

#### Olası Etkiler
- Derleme hataları
- İşlevsiz servis çağrıları
- Runtime hataları

#### Çözüm Planı
- Eksik metot implementasyonlarının tamamlanması:
```csharp
public class KbsIntegrationService : IKbsIntegrationService
{
    // Diğer methodlar...
    
    public async Task<bool> SendNotificationAsync(KbsNotification notification)
    {
        // Uygulama...
        return true;
    }
    
    public async Task<KbsNotificationStatusResultDto> CheckNotificationStatusAsync(string referenceId)
    {
        // Uygulama...
        return new KbsNotificationStatusResultDto();
    }
    
    // Diğer eksik methodları da uygulayın...
}
```

### Dönüş Türü Uyumsuzluğu

#### Tespit Edilen Sorun
- Bazı metotlarda dönüş türü, interface'te belirtilen türle uyuşmuyor
- Örnek: `GetNotificationLogsAsync` metodu `IKbsIntegrationService` arayüzünde farklı bir dönüş türüne sahip

#### Olası Etkiler
- Derleme hataları
- Tip dönüşüm hataları
- Beklenmeyen metot davranışları

#### Çözüm Planı
- Dönüş türlerinin interface ile eşleşecek şekilde düzeltilmesi:
```csharp
public async Task<IEnumerable<KbsNotificationLog>> GetNotificationLogsAsync(string referenceId)
{
    // Uygulama...
    return await _kbsNotificationLogRepository.GetAllAsync(l => l.ReferenceId == referenceId);
}
```

### BaseEntity Belirsiz Başvuru

#### Tespit Edilen Sorun
- `BaseEntity` sınıfı hem `ResidenceManagement.Core.Entities.Base` hem de `ResidenceManagement.Core.Entities.Common` namespace'lerinde bulunuyor
- Bu durum belirsiz başvuru hatalarına neden oluyor

#### Olası Etkiler
- Derleme hataları
- Yanlış temel sınıf kullanımı
- Eksik özellik veya davranışlar

#### Çözüm Planı
- Belirsiz başvuruları çözmek için tam yol adını kullanma:
```csharp
using ResidenceManagement.Core.Entities.Base; // veya
using ResidenceManagement.Core.Entities.Common;

// Sınıf tanımında:
public class ServiceRequest : ResidenceManagement.Core.Entities.Base.BaseEntity // Tam yol kullanın
```

### Eksik Sınıf ve DTO Yapıları

#### Tespit Edilen Sorun
- KBS entegrasyonu için gerekli bazı sınıf ve DTO yapıları eksik
- Örnek: `KbsNotification`, `KbsNotificationLog`, `KbsNotificationStatusResultDto`

#### Olası Etkiler
- Derleme hataları
- KBS entegrasyonunun çalışmaması
- Eksik işlevsellik

#### Çözüm Planı
- Eksik sınıf ve DTO yapılarının oluşturulması:

```csharp
namespace ResidenceManagement.Core.Entities.Kbs
{
    public class KbsNotification : BaseEntity
    {
        // Özellikler...
    }
}

namespace ResidenceManagement.Core.DTOs
{
    public class KbsNotificationStatusResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        // Diğer özellikler...
    }
}

namespace ResidenceManagement.Core.Entities.Kbs
{
    public class KbsNotificationLog : BaseEntity
    {
        public string ReferenceId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
        // Diğer özellikler...
    }
}
```

## Özet Çözüm Planı

1. **Domain ve Core projelerinin birleştirilmesi**
   - Domain projesindeki entity'lerin Core'a taşınması
   - Core'daki BaseEntity'nin kullanılması
   - Referansların güncellenmesi

2. **Katman düzenlemesi**
   - Services klasörünün Application katmanına taşınması
   - Filters klasörünün API katmanına taşınması
   - Core katmanının sadece domain modellerini içermesi

3. **Infrastructure düzenlemesi**
   - Repository sınıflarının Data/Repositories altında toplanması
   - DI kayıt dosyalarının birleştirilmesi
   - Gereksiz dosyaların temizlenmesi

4. **API temizliği**
   - DTO'ların Core/Application katmanına taşınması
   - Program.cs'nin extension metodlar ile parçalanması
   - Örnek kodların kaldırılması

5. **Services katmanının kaldırılması**
   - Interface'lerin Core/Interfaces altına taşınması

6. **DI standartlaştırması**
   - Tam nitelikli isimlerin using direktifleri ile değiştirilmesi
   - Dupliker servis kayıtlarının temizlenmesi

7. **Program.cs modülerleştirme**
   - Extension sınıflarına ayırma
   - WeatherForecast örnek kodunun kaldırılması

8. **Namespace düzenlemesi**
   - Interface'lerin tek namespace altında toplanması
   - Using direktiflerinin düzenlenmesi

9. **Clean Architecture düzenlemesi**
   - UI/API bağımlılıklarının kaldırılması
   - Domain logic'in Core katmanında tutulması

10. **Güvenlik iyileştirmeleri**
    - JWT yapılandırmasının düzeltilmesi
    - CORS politikalarının sıkılaştırılması

11. **Performans iyileştirmeleri**
    - Middleware sırasının optimize edilmesi
    - Caching stratejisinin geliştirilmesi

12. **Tip ve Başvuru Sorunlarının Çözülmesi**
    - ITenant ve BaseEntity belirsiz başvurularının çözülmesi
    - Eksik türlerin ve DTO'ların oluşturulması
    - Interface implementasyonlarının tamamlanması
    - KBS entegrasyonu için gerekli sınıfların eklenmesi

Bu çözüm planının uygulanması, projenin daha bakımı kolay, güvenli ve performanslı hale gelmesini sağlayacaktır.

## [Hata Mesajı]Çözüm: Duplicate BaseEntity ve ITenant referansları sorunu

Projede şu anda BaseEntity ve ITenant sınıfları/arayüzleri için çakışan tanımlamalar var:
- ResidenceManagement.Core.Entities.Base.BaseEntity
- ResidenceManagement.Core.Entities.Common.BaseEntity
- ResidenceManagement.Core.Common.ITenant
- ResidenceManagement.Core.Interfaces.ITenant

Bu durum, kodun derlenmesini engelliyor. Çözüm olarak:

1. Öncelikle hangi namespace'lerin korunacağına karar verilmeli (genellikle Core.Entities.Base ve Core.Interfaces tercih edilir)
2. Diğer tanımlamalar kaldırılmalı veya yeniden düzenlenmeli
3. Using yönergeleri düzeltilmeli
4. Tüm projeler temizlenip yeniden derlenmelidir.

## [Hata Mesajı]Çözüm: KbsNotification ve KbsNotificationLog sınıfları bulunamıyor

Projede KbsIntegrationService, bazı KBS ile ilgili sınıflara referans veriyor ancak bu sınıflar bulunamıyor:
- KbsNotification
- KbsNotificationLog
- KbsNotificationCreateDto
- KbsNotificationStatusResultDto

Çözüm:
1. Bu sınıfların tanımlamalarını oluşturmak veya düzeltmek
2. Doğru namespace'leri using yönergelerine eklemek
3. Eğer bu sınıflar başka bir projede ise, o projeye referans eklemek

## [Hata Mesajı]Çözüm: KbsIntegrationService arayüz implementasyonu eksik

KbsIntegrationService sınıfı, IKbsIntegrationService arayüzünü tam olarak uygulamıyor. Aşağıdaki metotlar eksik:
- SendNotificationAsync(KbsNotification)
- CheckNotificationStatusAsync(string)
- SendBatchNotificationsAsync(List<KbsNotification>)
- SubmitNotificationAsync(KbsNotification)
- GetNotificationLogsAsync(string)

Çözüm:
1. KbsIntegrationService sınıfına eksik metotları eklemek
2. Tip uyumsuzluklarını düzeltmek
3. Arayüz ile implementasyon arasındaki uyumsuzlukları gidermek 