# Residence Management Kod Kalitesi ve Hata Önleme Kılavuzu

## İçindekiler
1. [Giriş](#giriş)
2. [Son Bir Haftada Çözülen Sorunlar](#son-bir-haftada-çözülen-sorunlar)
3. [Kök Nedenler Analizi](#kök-nedenler-analizi)
4. [Kod Yazım Kuralları ve En İyi Uygulamalar](#kod-yazım-kuralları-ve-en-iyi-uygulamalar)
5. [Yeni Sayfa/Özellik Geliştirme Kılavuzu](#yeni-sayfaözellik-geliştirme-kılavuzu)
6. [Kod İnceleme Kontrol Listesi](#kod-İnceleme-kontrol-listesi)

## Giriş

Bu belge, Residence Management projesi içerisinde son bir haftada yaşanan ve çözülen sorunları analiz eder ve benzer hataların gelecekte tekrarlanmasını önlemek amacıyla geliştirici kılavuzu sağlar. Bu rehber, kod kalitesini artırmak, tutarlılığı sağlamak ve derleyici hatalarını en aza indirmek için öneriler içerir.

## Son Bir Haftada Çözülen Sorunlar

Projede karşılaşılan ve çözülen başlıca sorunlar:

1. **Ad Alanı (Namespace) Çakışmaları ve Belirsizlikler**:
   - `ResidenceManagement.Core.DTOs.Notification` ve `ResidenceManagement.Core.DTOs.Notifications` arasında isim çakışmaları
   - `NotificationLogDto`, `NotificationPreferenceDto` ve diğer DTO sınıfları için belirsiz başvurular
   - `Category` ve `CategoryId` gibi farklı özellik isimleri ile aynı amaçlı DTO'lar

2. **Servis Arayüzü Çakışmaları**:
   - `IOperationLogService` arayüzünün farklı ad alanlarında çakışması
   - Servis kayıtlarında tam belirtilmemiş bağımlılıklar

3. **Apartman Sınıfındaki Belirsizlikler**:
   - `ResidenceManagement.Core.Entities.Apartment` ve `ResidenceManagement.Core.Entities.Property.Apartment` arasındaki çakışma
   - Entity Framework ayraç (discriminator) yapılandırma sorunları

4. **ApiResponse Yapıcı Metod (Constructor) Parametre Sıralaması**:
   - Parametre sıralamasının (bool, string, data) yerine (bool, data, string) olarak düzeltilmesi

5. **Test Projesi Eksik Bağımlılıkları**:
   - Test projesinde eksik Moq paketi referansı

## Kök Nedenler Analizi

Bu sorunların temel nedenleri:

1. **Ad Alanı Tasarımı ve İsimlendirme Sorunları**:
   - Benzer isimlendirmede ancak çoğul/tekil farkı olan ad alanları (`Notification` vs `Notifications`)
   - Aynı amaca hizmet eden DTO'ların farklı ad alanlarında duplike edilmesi
   - DTO'lar arasında tutarsız özellik isimlendirmeleri (`Category` vs `CategoryId`)

2. **Servis Tasarımı ve Bağımlılık Yönetimi**:
   - Servis kayıtlarında tam ad alanı belirtilmemesi
   - Benzer isimli ancak farklı ad alanlarında bulunan arayüzler

3. **Entity Framework Yapılandırma Sorunları**:
   - Entity sınıfları arasında isim çakışmaları
   - Kalıtım (inheritance) yapısının düzgün yapılandırılmaması

4. **API Response Tutarsızlığı**:
   - Tutarsız parametre sıralaması kullanımı
   - Hata durumları için standart olmayan yaklaşımlar

5. **Test Altyapısı Eksiklikleri**:
   - Eksik NuGet paket referansları
   - Test projesinin düzenli olarak build edilmemesi

## Kod Yazım Kuralları ve En İyi Uygulamalar

### 1. Ad Alanı (Namespace) Yönetimi ve İsimlendirme

- **Tutarlı Ad Alanı Yapısı**: Tek bir kavram için tek bir ad alanı kullanın. Örneğin, bildirim ile ilgili tüm DTO'lar için `ResidenceManagement.Core.DTOs.Notifications` kullanın, tekil/çoğul karışımından kaçının.

- **DTO İsimlendirme**: DTO sınıfları için tutarlı isim formatı kullanın. Örnek: `EntityNameDto`, `EntityNameCreateDto`, `EntityNameUpdateDto`.

- **Özellik İsimlendirme Tutarlılığı**: Farklı DTO'larda aynı anlama gelen özellikler için aynı isimleri kullanın (örn. hep `CategoryId` kullanın, bazı yerlerde `Category` kullanmayın).

- **Tam Ad Alanı Kullanımı**: Belirsizlik olabilecek durumlarda tam ad alanı belirtin:
  ```csharp
  // Kötü
  using ResidenceManagement.Core.DTOs.Notification;
  using ResidenceManagement.Core.DTOs.Notifications;
  var dto = new NotificationLogDto(); // Belirsiz başvuru
  
  // İyi
  using ResidenceManagement.Core.DTOs.Notifications;
  var dto = new NotificationLogDto(); // Tek bir ad alanı kullanımı
  
  // Veya gerekirse
  var dto = new ResidenceManagement.Core.DTOs.Notifications.NotificationLogDto(); // Tam ad alanı kullanımı
  ```

### 2. Servis Tasarımı ve Bağımlılık Enjeksiyonu

- **Benzersiz Arayüz İsimleri**: Farklı ad alanlarında aynı isimde arayüzler kullanmaktan kaçının. İsim çakışması olacaksa, arayüz ismini daha spesifik yapın.

- **Servis Kaydı Tam Ad Alanı**: Servis kayıtlarında tam ad alanı belirtin:
  ```csharp
  // Kötü
  services.AddScoped<IOperationLogService, OperationLogService>();
  
  // İyi
  services.AddScoped<ResidenceManagement.Core.Interfaces.Logging.IOperationLogService, 
                     ResidenceManagement.Infrastructure.Services.OperationLogService>();
  ```

- **Arayüz Ayrımı**: Farklı işlevler için farklı arayüzler tasarlayın, bir arayüzün tüm işlevleri içermesini beklemeyin.

### 3. Entity Framework ve ORM Yapılandırması

- **Entity İsimlendirme**: Entity sınıfları için benzersiz isimler kullanın. İsim çakışmasından kaçınılamıyorsa ad alanını net şekilde ayırın.

- **Kalıtım (Inheritance) Yapılandırması**: Entity kalıtım hiyerarşisini tasarlarken, ayrıştırıcı (discriminator) sütunları doğru şekilde yapılandırın:
  ```csharp
  modelBuilder.Entity<BaseEntity>()
      .HasDiscriminator<string>("EntityType")
      .HasValue<SpecificEntity>("Specific");
  ```

- **İlişki Yapılandırması**: Entity ilişkilerini açık şekilde tanımlayın, varsayılan davranışlara güvenmeyin.

### 4. API Yanıtları ve Hata Yönetimi

- **Tutarlı Yanıt Yapısı**: API yanıtları için tutarlı bir yapı kullanın:
  ```csharp
  // Doğru parametre sıralaması
  ApiResponse<T>.Success(data, message); // (veri, mesaj)
  ApiResponse<T>.Error(message, statusCode); // (mesaj, durum kodu)
  ```

- **Hata Yönetimi**: Hataları try-catch blokları ile düzgün şekilde yakalayın ve anlamlı hata mesajları döndürün.

### 5. Test Altyapısı

- **Gerekli Paketleri Dahil Etme**: Test projesine tüm gerekli paketleri (xUnit, Moq, vb.) dahil edin.

- **Düzenli Build Etme**: Test projesini ana projeyle birlikte düzenli olarak build edin.

## Yeni Sayfa/Özellik Geliştirme Kılavuzu

Yeni bir sayfa veya özellik geliştirirken izlenecek adımlar:

1. **Veri Modeli ve DTO Tanımı**:
   - İlgili entity sınıflarını açıkça tanımlayın
   - Entity başına tek bir DTO sınıfı oluşturun; farklı işlemler için ayrı DTO'lar gerekiyorsa, tutarlı isimlendirme kullanın
   - DTO sınıflarını mevcut uygun ad alanlarına yerleştirin, yeni ad alanı oluşturmaktan kaçının

2. **Servis Katmanı**:
   - Net bir arayüz tanımı yapın, arayüz adını spesifik tutun
   - Arayüzün tek bir sorumluluğu olmasına dikkat edin
   - Servis implementasyonu, arayüzü tam olarak uygulayarak başlayın

3. **Controller Oluşturma**:
   - RESTful prensiplerine uygun endpoint'ler tasarlayın
   - Modelleri doğrulama (validation) ekleyin
   - ApiResponse yapısını tutarlı şekilde kullanın
   - Try-catch blokları ile hata yönetimini uygulayın

4. **Test Yazımı**:
   - Yeni özellikler için birim testleri yazın
   - Gerekli mock nesnelerini oluşturun
   - Başarı ve hata durumlarını test edin

## Kod İnceleme Kontrol Listesi

Her pull request için kullanılacak kontrol listesi:

- [ ] **Ad Alanı Kontrolü**: Doğru ve tutarlı ad alanları kullanılmış mı?
- [ ] **DTO Tutarlılığı**: DTO'lar mevcut yapıya uygun mu? Duplikasyon var mı?
- [ ] **Servis Tasarımı**: Servis arayüzleri ve implementasyonları SOLID prensiplerine uygun mu?
- [ ] **Bağımlılık Enjeksiyonu**: Servisler doğru şekilde kaydedilmiş mi?
- [ ] **Entity Framework Yapılandırması**: Entity ilişkileri doğru tanımlanmış mı?
- [ ] **Hata Yönetimi**: Try-catch blokları doğru şekilde kullanılmış mı?
- [ ] **API Yanıtları**: ApiResponse tutarlı şekilde kullanılmış mı?
- [ ] **Testler**: Yeni kod için testler yazılmış mı?
- [ ] **Performans**: N+1 sorguları gibi performans sorunları var mı?
- [ ] **Güvenlik**: Yetkilendirme ve kimlik doğrulama kontrolleri eklenmiş mi? 