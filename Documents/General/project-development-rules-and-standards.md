# Proje Geliştirme Kuralları ve Standartları

Bu doküman, Mekik Residence Manager projesinin geliştirme sürecinde uyulması gereken temel prensipleri, teknik standartları, kodlama yönergelerini, mimari yaklaşımları ve proje yönetimi süreçlerini kapsamaktadır. Tüm ekip üyelerinin bu kurallara uyması, projenin kalitesini, sürdürülebilirliğini ve tutarlılığını sağlamak için kritik öneme sahiptir.

## 1. Genel Geliştirme Prensipleri

1.  **API Odaklı Geliştirme:** Proje, temel olarak bir API üzerine inşa edilecektir. Frontend, mobil uygulama ve potansiyel diğer istemciler, bu merkezi API'yi kullanarak sistemle iletişim kuracaktır.
2.  **İlişkisel Veri Yapısı ve Bütünlüğü:** Veritabanı tasarımında normalizasyon ve ilişkisel bütünlük kurallarına uyulacaktır. Tanım tabloları (lookup) ve hareket tabloları (transaction) arasındaki ilişkiler net bir şekilde tanımlanacaktır. Hiçbir tablo kendi başına anlamsız veri tutmayacak, veriler mantıksal bütünlük içinde olacaktır.
3.  **Multi-tenant (Çok Kiracılı) Mimari:**
    *   Sistem, birden fazla site yönetim şirketine (Firma/Company) ve bu şirketlere bağlı şubelere (Sube/Branch) hizmet verebilecek şekilde tasarlanmıştır.
    *   Veri tabanındaki ilgili tablolarda `CompanyId` ve `BranchId` alanları bulunacaktır.
    *   Kullanıcıların veri erişimi, bağlı oldukları `CompanyId` ve `BranchId`'ye göre filtrelenecektir.
4.  **Rol ve Yetki Sistemi (RBAC - Role-Based Access Control):**
    *   Kullanıcıların sistemdeki yetkileri, atandıkları rollere göre belirlenecektir.
    *   Tüm formlar, ekranlar ve API endpoint'leri, kullanıcının yetki seviyesine (Ekle, Yenile, Sil, Gör vb.) göre erişim kontrolü yapacaktır.
5.  **Kullanıcı Tanımları ve Otomasyon:**
    *   Detaylı bir kullanıcı yönetim ekranı olacaktır.
    *   Farklı kullanıcı tipleri (Admin, Firma Yöneticisi, Site Yöneticisi, Teknik Personel, Sakin, Kiracı vb.) tanımlanacaktır.
    *   Yeni daire sahibi veya kiracı eklendiğinde, sistem tarafından otomatik olarak (limitli yetkilerle) kullanıcı hesapları oluşturulması ve bilgilendirme yapılması hedeflenmelidir.
    *   Kullanıcı erişim süreleri yönetilebilir olmalıdır (örneğin, kiracının kontrat bitiş tarihi, daire sahibinin üyelik süresi vb.).
6.  **Teknoloji ve Kütüphane Uyumluluğu:** Projede kullanılacak tüm teknolojiler, kütüphaneler ve paketler birbiriyle uyumlu olacaktır. Yeni bir teknoloji veya kütüphane eklenmeden önce uyumluluk ve performans etkileri analiz edilecektir.
7.  **Kullanıcı Arayüzü (UI) ve Kullanıcı Deneyimi (UX):**
    *   Proje, modern, kullanıcı dostu ve sezgisel bir kullanıcı arayüzü ile geliştirilecektir.
    *   Tüm ekranlar ve formlar arasında tasarım tutarlılığı sağlanacaktır.
    *   Responsive tasarım prensiplerine uyularak farklı ekran boyutlarında (mobil, tablet, masaüstü) optimum kullanıcı deneyimi sunulacaktır.
8.  **Raporlama ve Veri Görselleştirme:**
    *   Kapsamlı bir raporlama altyapısı oluşturulacaktır. Birçok form ve liste ekranında Excel/PDF çıktısı alma özellikleri bulunacaktır.
    *   Kritik metrikler ve veriler için grafiksel ve metinsel detaylar içeren dashboard'lar tasarlanacaktır (Yönetim, Kiracı, Mülk Sahibi için ayrı).
9.  **Mock Data Kullanımı:** Eğer test veya geliştirme aşamasında mock data kullanılacaksa, bu verilerin yapısı ve formatı gerçek veritabanı şemasıyla uyumlu olacaktır.
10. **Geliştirme Sırası ve Öncelikler:** Proje geliştirme süreci belirlenen fazlara ve sprint'lere göre ilerleyecektir. Önceliklendirme, projenin temel fonksiyonlarının erken tamamlanması ve risklerin azaltılması üzerine kurulacaktır (Genellikle Backend > Frontend > Mobil sıralaması izlenir).
11. **Çoklu Dil Desteği:**
    *   Proje, baştan itibaren çoklu dil desteği (Türkçe varsayılan olmak üzere İngilizce, Almanca, Rusça, Arapça, Farsça hedeflenmiştir) ile geliştirilecektir.
    *   Tüm statik metinler, arayüz elemanları ve kullanıcıya gösterilen mesajlar yerelleştirme (localization) sistemi üzerinden yönetilecektir.
    *   Sağdan sola (RTL) ve soldan sağa (LTR) metin desteği sağlanacaktır.
12. **Proje Yapısı Bütünlüğü:** Backend, Frontend ve Mobil geliştirmeleri kendi belirlenmiş ana klasörleri (`Backend/`, `Frontend/`, `Mobil/`) içinde yapılacaktır. Bu yapı korunacak ve çapraz geliştirmelerden kaçınılacaktır.
13. **Patron Talimatları ve Esneklik:** Proje dokümanları ve planları genel bir rehber olmakla birlikte, iş ihtiyaçları ve "patron talimatları" doğrultusunda öncelikler ve özellikler güncellenebilir. Esneklik, projenin başarısı için önemlidir.

## 2. Mimari ve Tasarım İlkeleri

1.  **Clean Architecture / Onion Architecture:** Backend mimarisi bu prensiplere uygun olarak katmanlı (Core/Domain, Application, Infrastructure, Presentation/API) bir yapıda olacaktır. Bağımlılıklar her zaman daha içteki katmanlara doğru olacaktır.
2.  **SOLID Prensipleri:** Yazılım tasarımında Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation ve Dependency Inversion prensiplerine uyulacaktır.
3.  **Design Patterns:** Uygun yerlerde Repository, Unit of Work, Service Layer, Dependency Injection, Factory, Strategy, Mediator (CQRS için), Specification gibi tasarım desenleri kullanılacaktır.
4.  **API Tasarımı (RESTful):**
    *   Resource-oriented URL yapısı (örn: `/api/v1/companies/{id}/branches`).
    *   HTTP metotlarının (GET, POST, PUT, PATCH, DELETE) doğru kullanımı.
    *   Tutarlı API response yapısı (başarı durumu, veri, mesaj, hatalar, meta bilgileri).
    *   HTTP durum kodlarının (200, 201, 204, 400, 401, 403, 404, 500 vb.) uygun kullanımı.
    *   API versiyonlama (URL path ile, örn: `/api/v1/`).
5.  **Veritabanı Tasarımı:**
    *   Normalizasyon prensiplerine uyulacaktır.
    *   İlişkiler (Cascade, Restrict, SetNull) doğru tanımlanacaktır.
    *   Performans için gerekli alanlara indeksler uygulanacaktır.
    *   Veritabanı nesneleri için uygun veri tipleri seçilecektir.
    *   Multi-tenant filtreleme DbContext seviyesinde global query filtreleri ile desteklenecektir.
6.  **Asenkron Programlama:** Özellikle I/O bound işlemler için (veritabanı erişimi, harici servis çağrıları) her zaman asenkron metotlar (`async/await`, `Task<T>`) kullanılacaktır.

## 3. Kodlama Standartları

### 3.1. Genel İsimlendirme Kuralları

*   **Teknik Kodlar (Sınıf, Metot, Değişken, Property, Interface, Enum Sınıfı, Dosya, Namespace İsimleri):** Tamamen **İngilizce** kullanılacaktır. Türkçe karakter (ö, ü, ı, ğ, ş, ç, İ) kesinlikle kullanılmayacaktır.
*   **İçerik ve Kullanıcı Arayüzü Metinleri:** Yorum satırları, enum değerlerinin kullanıcıya gösterilen açıklamaları, hata mesajları, doğrulama mesajları ve kullanıcı arayüzündeki tüm metinler **Türkçe** olacaktır. Bu metinlerde Türkçe karakter kullanımına izin verilir.
*   **Kısaltmalar:** Genel kabul görmüş ve anlaşılır kısaltmalar dışında, açık ve anlaşılır isimler tercih edilmelidir.
*   **Tutarlılık:** Proje genelinde isimlendirme standartlarında tutarlılık sağlanacaktır.

### 3.2. C# (.NET Backend) İsimlendirme

*   **Sınıflar, Interface'ler, Enum Sınıfları, Metotlar, Property'ler:** `PascalCase` (örn: `UserService`, `IUserService`, `PaymentStatus`, `GetUserById`, `IsEnabled`).
*   **Interface'ler:** `I` öneki ile başlamalıdır (örn: `IUserService`).
*   **Parametreler, Yerel Değişkenler:** `camelCase` (örn: `userId`, `paymentAmount`).
*   **Private Alanlar (Fields):** `_camelCase` (alt çizgi ile başlayan camelCase, örn: `_logger`).
*   **Constant'lar:** `UPPER_SNAKE_CASE` (tamamı büyük harf ve kelimeler alt çizgi ile ayrılmış, örn: `MAX_USER_COUNT`).
*   **Boolean Değişkenler/Property'ler/Metotlar:** Genellikle `Is`, `Has`, `Can` gibi öneklerle başlamalıdır (örn: `IsActive`, `HasPermission`).
*   **Enum Değerleri (Teknik):** Enum sınıfı içindeki değerler İngilizce ve `PascalCase` olmalıdır (örn: `PaymentStatus.Completed`). Kullanıcı arayüzünde gösterilecek Türkçe karşılıkları veritabanından veya bir dil dosyasından alınacaktır.

### 3.3. JavaScript/TypeScript (React Frontend/React Native Mobil) İsimlendirme

*   **Komponentler (Dosya ve Sınıf/Fonksiyon Adı):** `PascalCase` (örn: `UserProfile.tsx`, `ApartmentCard.tsx`).
*   **Fonksiyonlar, Değişkenler:** `camelCase` (örn: `getUserData()`, `apartmentList`).
*   **Sabitler (Constants):** `UPPER_SNAKE_CASE` (örn: `MAX_UPLOAD_SIZE`, `API_URL`).
*   **Enum'lar (TypeScript):** `PascalCase` (örn: `UserRole`, `PaymentStatus`). Enum değerleri de `PascalCase` olmalıdır.
*   **Event Handler'lar:** `handle` öneki ile başlamalıdır (örn: `handleClick`, `handleSubmit`).
*   **Custom Hook'lar:** `use` öneki ile başlamalıdır (örn: `useAuth`, `useFormValidation`).
*   **CSS Class İsimleri:** `kebab-case` (örn: `user-profile-card`).

### 3.4. Dosya ve Klasör Yapısı

*   Her teknoloji (Backend, Frontend, Mobil) kendi ana klasöründe geliştirilecektir. Root klasörde doğrudan kod dosyası oluşturulmayacaktır.
*   **Backend:** Proje isimleri `ResidenceManagement.Core`, `ResidenceManagement.Infrastructure` gibi `PascalCase` olacaktır. Klasör isimleri de `PascalCase` olacaktır (örn: `Controllers`, `Services`, `Repositories`).
*   **Frontend/Mobil:** Komponent dosyaları `PascalCase` (örn: `Button.tsx`), yardımcı dosyalar (utils, services vb.) `camelCase` (örn: `formatDate.ts`, `authService.ts`) veya `kebab-case` (tercihe göre) olabilir. Klasör isimleri genellikle `camelCase` veya `kebab-case` olur.
*   Bir bileşen kendi dosyası içinde tanımlanmalıdır. Büyük bileşenler (genellikle 200-400 satır üzeri) daha küçük, yeniden kullanılabilir parçalara bölünmelidir.

### 3.5. Yorum Satırları ve Kod Dokümantasyonu

*   **Yorum Dili:** Kod içi yorumlar ve açıklamalar **Türkçe** yazılacaktır.
*   **XML Documentation (C#):** Public API'ler (sınıflar, metotlar, property'ler) için XML dokümantasyon yorumları (`/// <summary>...`) eklenecektir.
*   **JSDoc (JavaScript/TypeScript):** Fonksiyonlar, komponentler ve karmaşık mantıklar için JSDoc formatında yorumlar eklenecektir.
*   **Dosya Başlangıç Yorumları:** Her kaynak kod dosyasının başına, dosyanın amacını, oluşturulma/değiştirilme tarihini, yazarını ve önemli bağımlılıklarını belirten bir yorum bloğu eklenmesi teşvik edilir.
    ```csharp
    // -----------------------------------------------------------------------
    // <copyright file="UserService.cs" company="Mekik Solutions">
    // Copyright (c) Mekik Solutions. All rights reserved.
    // </copyright>
    // -----------------------------------------------------------------------
    // Amaç: Kullanıcı ile ilgili işlemleri yöneten servis sınıfı.
    // Bağımlılıklar: IUserRepository, IUnitOfWork, ILogger
    // Kullanım: UserController üzerinden çağrılır.
    // Yazar: [Geliştirici Adı]
    // Oluşturulma: [GG.AA.YYYY]
    // Son Değişiklik: [GG.AA.YYYY]
    // -----------------------------------------------------------------------
    ```
*   **TODO/FIXME Yorumları:** Yapılacak işler için `// TODO: [Açıklama]`, düzeltilmesi gereken hatalar için `// FIXME: [Açıklama]` formatında yorumlar kullanılacaktır.
*   **Açıklayıcı Olma:** Yorumlar, kodun "ne yaptığını" değil, "neden yaptığını" veya karmaşık kısımların "nasıl çalıştığını" açıklamalıdır. Kod zaten kendini açıklıyorsa gereksiz yorumdan kaçınılmalıdır.
*   **Güncellik:** Kodda değişiklik yapıldığında ilgili yorumlar da güncellenmelidir.

### 3.6. Kod Formatı ve Stili

*   **Tutarlılık:** Proje genelinde tutarlı bir kod formatı ve stili benimsenecektir.
*   **Araçlar:** ESLint, Prettier (Frontend/Mobil için) ve .NET için Roslyn analyzers/editorconfig (Backend için) gibi araçlar kullanılarak kod formatı ve stil kuralları otomatik olarak uygulanacaktır.
*   **Satır Uzunluğu:** Genellikle satır başına 100-120 karakter sınırı hedeflenmelidir.
*   **Girintileme:** Tab yerine boşluk (genellikle 2 veya 4 boşluk) kullanılacaktır. Bu, proje genelinde tutarlı olmalıdır.

## 4. Versiyon Kontrolü (Git)

1.  **Branch Stratejisi:** `Git Flow` benzeri bir branch stratejisi (örn: `main`, `develop`, `feature/feature-name`, `release/version`, `hotfix/issue`) kullanılacaktır.
    *   `main`: Üretim ortamını yansıtan kararlı sürüm.
    *   `develop`: En son geliştirme çalışmalarının entegre edildiği ana geliştirme dalı.
    *   `feature/*`: Yeni özellik geliştirmeleri için `develop` dalından oluşturulur.
    *   `release/*`: Yeni bir üretim sürümü hazırlamak için `develop` dalından oluşturulur.
    *   `hotfix/*`: Üretimdeki acil hataları düzeltmek için `main` dalından oluşturulur.
2.  **Commit Mesajları:** Anlamlı, açıklayıcı ve standart bir formatta (örn: Conventional Commits) commit mesajları yazılacaktır.
    *   Örnek: `feat: Kullanıcı girişi için JWT token entegrasyonu eklendi`
    *   Örnek: `fix: Daire listeleme sayfasındaki sayfalama hatası düzeltildi`
3.  **Pull Request (PR) / Merge Request (MR):**
    *   Tüm `feature` dalları, `develop` dalına (veya `release` dalları `main` ve `develop`'a) Pull Request ile birleştirilecektir.
    *   Her PR için en az bir başka geliştirici tarafından kod incelemesi (code review) yapılacaktır.
    *   PR açıklamaları, yapılan değişiklikleri net bir şekilde özetlemelidir.
4.  **Sık Commit, Küçük PR'lar:** Değişiklikler sık sık commit edilmeli ve Pull Request'ler mümkün olduğunca küçük ve odaklı tutulmalıdır.
5.  **Rebase vs. Merge:** Proje tercihine göre `develop` dalındaki değişiklikleri feature dalına alırken `rebase` veya `merge` kullanılabilir, ancak tutarlılık önemlidir. Genellikle temiz bir geçmiş için `rebase` tercih edilebilir.

## 5. Test Stratejisi ve Kalite Kontrol

### 5.1. Test Türleri ve Kapsamı

*   **Birim Testleri (Unit Tests):**
    *   Backend: xUnit veya MSTest. Moq veya NSubstitute gibi mock kütüphaneleri.
    *   Frontend/Mobil: Jest, React Testing Library.
    *   Kodun en küçük test edilebilir birimlerinin (metotlar, fonksiyonlar, sınıflar, komponentler) davranışları test edilir.
    *   Minimum %80 kod kapsama oranı (code coverage) hedeflenmelidir.
*   **Entegrasyon Testleri (Integration Tests):**
    *   Farklı modüllerin veya katmanların birbiriyle doğru çalışıp çalışmadığı test edilir (örn: Servis katmanı ile Repository katmanı, API ile veritabanı).
    *   Gerçek bağımlılıklar veya kontrollü test ortamları kullanılır.
*   **API Testleri (Backend):**
    *   API endpoint'lerinin doğru çalışıp çalışmadığı, doğru request/response formatlarını kullandığı ve yetkilendirme kurallarına uyduğu Postman, Insomnia veya kod tabanlı testlerle (örn: `WebApplicationFactory`) test edilir.
*   **UI Testleri / E2E (End-to-End) Testleri (Frontend/Mobil):**
    *   Kullanıcı arayüzü üzerinden gerçek kullanıcı senaryoları test edilir.
    *   Frontend: Cypress, Playwright veya Selenium.
    *   Mobil: Appium, Detox veya platforma özel araçlar.
*   **Performans Testleri:**
    *   Uygulamanın yük altında ve stres durumlarındaki performansı test edilir (API yanıt süreleri, sayfa yüklenme süreleri, kaynak kullanımı).
    *   Araçlar: JMeter, k6, LoadRunner.
*   **Güvenlik Testleri:**
    *   OWASP Top 10 gibi bilinen güvenlik açıklarına karşı testler yapılır.
    *   Kimlik doğrulama ve yetkilendirme mekanizmaları detaylı test edilir.
    *   Penetrasyon testleri (gerekirse dış kaynaklı) planlanabilir.

### 5.2. Kalite Kontrol Süreçleri

*   **Kod İncelemeleri (Code Reviews):** Her Pull Request için zorunludur. Kodun standartlara uygunluğu, okunabilirliği, performansı ve potansiyel hatalar gözden geçirilir.
*   **Statik Kod Analizi:** ESLint, StyleCop, SonarQube gibi araçlarla kod kalitesi, potansiyel bug'lar ve güvenlik açıkları otomatik olarak taranır.
*   **Sürekli Entegrasyon (Continuous Integration - CI):**
    *   Her kod değişikliği ana geliştirme dalına (`develop`) otomatik olarak entegre edilir.
    *   Entegrasyon sürecinde otomatik build ve testler (birim, entegrasyon) çalıştırılır.
    *   Testler başarısız olursa birleştirme engellenir.
*   **Teknik Borç Takibi:** Proje sürecinde ortaya çıkan teknik borçlar (yapılması gereken refaktörler, iyileştirmeler) takip edilir ve uygun zamanlarda çözülür.

## 6. Dokümantasyon

1.  **API Dokümantasyonu:** Backend API'leri Swagger/OpenAPI kullanılarak otomatik olarak dokümante edilecek ve güncel tutulacaktır. Endpoint açıklamaları, request/response örnekleri ve yetkilendirme gereksinimleri net bir şekilde belirtilecektir.
2.  **Kod İçi Dokümantasyon:** Yorum standartlarına uygun olarak (Bkz. Madde 3.5) kod içi açıklamalar yapılacaktır.
3.  **Teknik Mimari Dokümanları:** Bu ve benzeri Markdown dosyaları ile projenin mimarisi, teknik kararları, veritabanı şeması, geliştirme planları vb. konular dokümante edilecektir. Bu dokümanlar `Documents/` klasörü altında düzenli bir yapıda tutulacaktır.
4.  **Kullanıcı Kılavuzları:** Son kullanıcılar ve sistem yöneticileri için kullanım kılavuzları hazırlanacaktır.
5.  **CHANGELOG.md:** Projedeki tüm önemli değişiklikler, eklenen özellikler, düzeltilen hatalar ve versiyon bilgileri bu dosyada kronolojik olarak tutulacaktır.
6.  **README.md:** Projenin ana klasöründe ve önemli alt projelerin (Backend, Frontend, Mobil) klasörlerinde, projeye genel bir bakış, kurulum adımları ve temel bilgileri içeren README dosyaları bulunacaktır.
7.  **Mimari Karar Kayıtları (Architecture Decision Records - ADRs):** Önemli mimari kararlar, nedenleri ve sonuçları ADR formatında belgelenebilir.

## 7. Geliştirme Araçları ve Ortamları

### 7.1. Geliştirme Araçları

*   **IDE ve Editörler:** Visual Studio 2022 (Backend), Visual Studio Code (Frontend/Mobil), Cursor.
*   **Veritabanı Araçları:** SQL Server Management Studio (SSMS), Azure Data Studio.
*   **API Geliştirme/Test Araçları:** Postman, Insomnia, Swagger UI.
*   **Versiyon Kontrol:** Git (GitHub, Azure DevOps vb. platformlarda).
*   **Proje Yönetimi/Takip:** Azure DevOps, Jira, Trello veya benzeri bir araç.

### 7.2. Ortamlar

*   **Geliştirme (Development):** Geliştiricilerin lokal makinelerinde çalışan ortam.
*   **Test (Staging):** Kalite güvence ekibinin testlerini yaptığı, canlı ortama en yakın ortam.
*   **Üretim (Production):** Son kullanıcıların eriştiği canlı sistem.
    Her ortam için ayrı yapılandırma dosyaları ve veritabanları kullanılacaktır.

### 7.3. Dağıtım (Deployment) ve CI/CD

*   **Sürekli Entegrasyon/Sürekli Dağıtım (CI/CD):** GitHub Actions, Azure DevOps Pipelines veya Jenkins gibi araçlarla otomatik build, test ve deployment süreçleri kurulacaktır.
*   **Konteynerizasyon (Opsiyonel ama Önerilir):** Docker kullanılarak uygulamanın ve bağımlılıklarının konteynerize edilmesi, ortam tutarlılığını ve dağıtım kolaylığını artırır.
*   **Deployment Stratejileri:** Duruma göre Blue/Green, Canary gibi deployment stratejileri değerlendirilebilir.
*   **Rollback Planı:** Başarısız deployment durumlarında hızlıca önceki stabil sürüme dönebilmek için geri alma planları hazırlanacaktır.

## 8. Proje Yönetimi ve İletişim

1.  **Görev Takibi:** Proje görevleri, sprint'ler ve ilerleme durumu belirlenen proje yönetim aracı üzerinden takip edilecektir.
2.  **Düzenli Toplantılar:** Sprint planlama, günlük stand-up (scrum), sprint review ve sprint retrospective gibi düzenli toplantılar yapılacaktır.
3.  **İletişim Kanalları:** Ekip içi iletişim için Slack, Microsoft Teams gibi anlık mesajlaşma araçları kullanılacaktır.
4.  **Hiyerarşi ve Karar Alma:** Proje içindeki roller, sorumluluklar ve karar alma mekanizmaları net olacaktır. Teknik konularda ekip liderleri ve mimarlar, genel proje yönelimi konusunda ise proje yöneticisi ve "patron" son söz sahibi olabilir. Çelişkili durumlarda veya dokümantasyonla uyuşmayan taleplerde bu hiyerarşi dikkate alınacaktır.
5.  **İlerleme Raporlama:** Proje ilerlemesi düzenli olarak ilgili paydaşlara raporlanacaktır.

Bu kurallar ve standartlar, projenin sağlıklı bir şekilde geliştirilmesi, yönetilmesi ve sürdürülmesi için bir çerçeve sunmaktadır. Zamanla ve ihtiyaçlara göre bu doküman güncellenebilir. 