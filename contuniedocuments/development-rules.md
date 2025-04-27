# Rezidans ve Site Yönetim Sistemi - Geliştirme Kuralları

Bu kurallar Entegre Rezidans ve Site Yönetim Sistemi'nin geliştirme sürecinde uyulması gereken temel prensipleri ve standartları belirlemektedir. Cursor uygulamasında geçerli olacak şekilde hazırlanmıştır.

## Proje Klasör Yapısı

- Ana Proje Klasörü: `C:\c\Residence Management`
- Backend Klasörü: `C:\c\Residence Management\Backend`
- Frontend Klasörü: `C:\c\Residence Management\FrondEnd`
- Mobil Klasörü: `C:\c\Residence Management\mobil`

## 1. Genel Kurallar

1. **API Odaklı Geliştirme:** Proje tamamen API'den çalışacaktır. Tüm geliştirme ekranlarında API bağlantısının yapılacağı dikkate alınmalıdır.

2. **İlişkisel Veri Yapısı:** Projede birçok tablonun birbiriyle ilişkisi olacaktır. Veritabanı tasarımında ilişkiler doğru kurgulanmalıdır.

3. **Multi-tenant Yapı:** Projede tüm tablolarda FirmaID alanı olacaktır. Bunun amacı, uygulamanın birden fazla müşteriye satılması ve her müşterinin verisinin kendi ID'si ile tutulmasıdır.

4. **Şube Yapısı:** Her firmanın şubesi olma ihtimaline karşı tablolarda FirmaID ve SubeID alanları olacaktır. Login olan kullanıcı hangi firma ve şubesi için tanımlandıysa o firma ve şubesi için işlemler yapabilecektir.

5. **Yetki Sistemi:** Her kullanıcı kendisine atanan yetki seviyesine göre işlem yapacaktır. Bu sebeple tasarlanan veya tasarlanacak tüm formlarda yetki kontrolü (ekle, yenile, sil, gör) olmalıdır.

6. **Kullanıcı Tanımları:** Detaylı bir kullanıcı yönetim ekranı olacaktır:
   - Yetki seviyeli tasarım olacaktır (admin, firma yöneticisi, teknik, servis, ara eleman, resepsiyonist, misafir, kiracı)
   - Eklenen her daire sahibi için otomatik kullanıcı tanımı oluşacaktır
   - Eklenen her kiracı için otomatik kullanıcı tanımı oluşacaktır
   - Tüm formlar login olan kullanıcının yetki seviyesine göre ekrana gelecektir

7. **Veri Bütünlüğü:** Hiçbir tablo kendi başına veri tutamaz. Ya diğer veriler için kaynak tablodur ve ID'si diğer tablolara işlenir, ya da hareket gören bir tablodur ve tanım tabloları ile birlikte hareket eder.

8. **Teknoloji Uyumluluğu:** Projede kullanılan tüm teknolojiler proje bütünü ile uyumlu olacaktır. Eklenen teknolojilerin uyumsuzluk yaratmaması için önceden kontrolleri yapılacaktır.

9. **Kullanıcı Arayüzü:** Proje tamamen kullanıcı dostu ve son teknoloji UI ile geliştirilecektir. Tüm ekranlardaki form tasarımları birbiri ile uyumlu olacaktır.

10. **Raporlama:** Tüm form ekranlarında Excel çıktısı alma, PDF çıktı alma ve raporlama ekranı olacaktır. İlgili işleme ait hareketlerin görüntülenebildiği detay menüsü bulunacaktır.

11. **Dashboard:** Projenin kapsamlı grafiksel ve metinsel detaylarla genişletilmiş bir dashboard'u olacaktır. Bu dashboard, site içerisindeki birçok konuda özet bilgiyi aktarabilmelidir.

12. **Mock Data:** Mock data kullanılacak ise bu verinin tamamı database'den gelecektir.

## 2. Teknik Geliştirme Kuralları ve Geliştirme Sıralaması

> **UYARI:** Proje geliştirme sıralamasına kesinlikle uyulmalıdır. Her aşama tamamlanmadan bir sonraki aşamaya geçilmemelidir.

Proje geliştirme süreci şu sırayla ilerleyecektir:

1. **Backend API ve Veritabanı Geliştirmesi**
   - Veritabanı modellemesi
   - Migration scriptleri
   - Repository ve Service katmanları
   - API endpoint'leri
   - Yetkilendirme sistemi
   - Test ve dokümantasyon

2. **Frontend Web Uygulaması Geliştirmesi**
   - UI/UX tasarımı
   - Komponent geliştirmesi
   - API entegrasyonu
   - State yönetimi
   - Test ve optimizasyon

3. **Mobil Uygulama Geliştirmesi**
   - Web uygulaması tamamen bittikten sonra başlayacaktır
   - Aynı API'leri kullanacaktır
   - Mobil-spesifik UI/UX geliştirilecektir

### 2.1. Backend Geliştirme Kuralları (Backend Klasörü)

> **ÖNEMLİ:** Backend geliştirmesi, projenin ilk aşamasında tamamlanması gereken en kritik bölümdür. Tüm API'ler, migration'lar, veritabanı modellemesi ve yetkilendirme sistemi backend geliştirmesi sırasında oluşturulacaktır.

1. **Teknoloji Seçimi:** Backend .NET 8 Web API (C#) ile geliştirilecek ve MSSQL kullanılacaktır.

2. **Veritabanı Yönetimi:** Tüm veritabanı işlemleri (tablo oluşturma, güncelleme) Entity Framework Core üzerinden migration aracılığı ile yapılacaktır.

3. **Repository Pattern:** Veritabanı erişimleri için Repository Pattern kullanılacaktır.

4. **Service Layer:** İş mantığı service layer üzerinden yönetilecektir.

5. **API Standartları:**
   - RESTful API prensipleri takip edilecektir
   - Tüm endpoint'ler `/api/v1/[resource]/[action]` formatında olacaktır
   - API yanıtları standart format içerecektir (success, data, error)

6. **Güvenlik:**
   - JWT token tabanlı authentication kullanılacaktır
   - Role-based authorization uygulanacaktır
   - Tüm API endpoint'lerinde yetki kontrolü yapılacaktır

7. **Validasyon:** Tüm girdiler server tarafında valide edilecektir (FluentValidation kullanılabilir).

8. **Exception Handling:** Global exception handling yapısı kurulacak, tüm hatalar merkezi olarak loglanacaktır.

9. **Logging:** Yapılandırılabilir loglama sistemi kurulacaktır (NLog, Serilog vb.).

10. **Dokümantasyon:** 
    - API'ler Swagger ile dokümante edilecektir
    - Kod içi yorum satırları eklenecektir
    - README.md dosyaları ile projeler açıklanacaktır
    - Migration'lar için açıklayıcı yorumlar eklenecektir

### 2.2. Frontend Geliştirme Kuralları (FrondEnd Klasörü)

> **ÖNEMLİ:** Frontend geliştirmesi, backend mimarisi tamamlandıktan sonra başlayacaktır. Frontend geliştirmesi, backend API'lerini kullanarak kullanıcı arayüzünü oluşturacaktır. Backend tamamlanmadan, frontend geliştirmesine başlanmamalıdır.

1. **Teknoloji Seçimi:** Frontend React.js ve Material UI ile geliştirilecektir.

2. **Kod Organizasyonu:**
   - `components/` klasöründe ortak bileşenler yer alacaktır
   - `pages/` klasöründe sayfa bileşenleri yer alacaktır
   - `services/` klasöründe API çağrıları yer alacaktır
   - `utils/` klasöründe yardımcı fonksiyonlar yer alacaktır
   - `theme/` klasöründe tema yapılandırması yer alacaktır
   - `i18n/` klasöründe dil dosyaları yer alacaktır
   - `context/` klasöründe context tanımları yer alacaktır
   - Her bileşen, kendi klasöründe yer alacaktır (Örn: components/Header/Header.jsx)

3. **Bileşen Yapısı:**
   - Bileşenler maksimum 400 satır olacak şekilde bölünmelidir
   - Props için PropTypes veya TypeScript interfaceleri kullanılmalıdır
   - Fonksiyonel komponentler ve React Hooks kullanılacaktır
   - Her bileşen için unit test yazılmalıdır

4. **State Yönetimi:**
   - Uygulama genelinde React Context API kullanılacaktır
   - Gerektiğinde performans için Redux Toolkit eklenebilir
   - Global state için useReducer pattern tercih edilecektir
   - Form state yönetimi için react-hook-form kullanılabilir

5. **API İletişimi:**
   - Tüm API çağrıları services/ klasöründen yapılacaktır
   - Async/await yapısı kullanılacaktır
   - Tüm API çağrılarında hata yakalama (try/catch) uygulanacaktır
   - Loading state yönetimi yapılacaktır

6. **Kullanıcı Arayüzü:**
   - Tamamen kullanıcı dostu ve son teknoloji UI ile geliştirilecektir
   - Tüm ekranlardaki form tasarımları birbiri ile uyumlu olacaktır
   - Responsive tasarım prensipleri uygulanacaktır
   - Material UI bileşenleri tutarlı kullanılacaktır

7. **Form Fonksiyonları:**
   - Tüm form ekranlarında Excel çıktısı alma, PDF çıktı alma özelliği olacaktır
   - Raporlama ekranı ve ilgili işleme ait hareketlerin görüntülenebildiği detay menüsü olacaktır
   - Listeleme ekranlarında filtreleme, sıralama ve arama özellikleri olacaktır

8. **Dashboard:**
   - Projenin kapsamlı grafiksel ve metinsel detaylarla genişletilmiş bir dashboard'u olacaktır
   - Dashboard site içerisindeki birçok konuda özet bilgiyi aktarabilmelidir
   - Farklı kullanıcı rolleri için özelleştirilmiş dashboard görünümleri sunulacaktır

9. **Kod Kalitesi:**
   - ESLint ve Prettier kullanılarak kod formatı standartlaştırılacaktır
   - Tüm komponenler için JSDoc dokümantasyonu yazılacaktır
   - Memory leak'leri önlemek için useEffect cleanup fonksiyonları kullanılacaktır
   - React Performance Best Practices uygulanacaktır

10. **Çoklu Dil Desteği:**
    - i18next kullanılarak çoklu dil desteği sağlanacaktır
    - Tüm metinler kaynak dosyalarından alınacaktır
    - Varsayılan dil Türkçe olacaktır
    - İngilizce dil desteği eklenecektir

11. **Test Standartları:**
    - Her komponent için unit testler yazılacaktır (Jest, React Testing Library)
    - Kritik fonksiyonlar için entegrasyon testleri yazılacaktır
    - End-to-end testler için Cypress kullanılacaktır
    - Test coverage %80'in üzerinde olmalıdır

### 2.3. Mobil Uygulama Geliştirme Kuralları (mobil Klasörü)

> **ÖNEMLİ:** Mobil uygulama geliştirmesi, web frontend uygulaması tamamen tamamlandıktan sonra başlayacaktır. Aynı backend API'leri kullanılacaktır.

1. **Teknoloji Seçimi:** Mobil uygulama React Native ile geliştirilecektir.

2. **Kod Organizasyonu:**
   - `components/` klasöründe ortak bileşenler yer alacaktır
   - `screens/` klasöründe ekran bileşenleri yer alacaktır
   - `services/` klasöründe API çağrıları yer alacaktır
   - `utils/` klasöründe yardımcı fonksiyonlar yer alacaktır
   - `navigation/` klasöründe navigasyon yapılandırması yer alacaktır
   - `i18n/` klasöründe dil dosyaları yer alacaktır
   - `context/` klasöründe context tanımları yer alacaktır

3. **Bileşen Yapısı:**
   - Bileşenler maksimum 300 satır olacak şekilde bölünmelidir
   - PropTypes veya TypeScript kullanılmalıdır
   - Fonksiyonel komponentler ve React Hooks kullanılacaktır
   - Her bileşen için unit test yazılmalıdır

4. **Native Özellikler:**
   - Push notification entegrasyonu
   - Kamera ve dosya erişimi
   - Lokasyon servisleri
   - Offline çalışma modu

5. **Performans Optimizasyonu:**
   - Memoization kullanılacaktır (React.memo, useMemo, useCallback)
   - Gereksiz render'ları önlemek için optimizasyon yapılacaktır
   - Büyük listelerde FlatList ve VirtualizedList kullanılacaktır
   - Bundle size optimizasyonu yapılacaktır

6. **Test Stratejisi:**
   - Jest ile unit testler
   - Detox ile e2e testler
   - Farklı cihazlarda uyumluluk testleri
   - Performans testleri

7. **Dağıtım Stratejisi:**
   - CI/CD pipeline kurulumu
   - Testflight ve Google Play beta dağıtımı
   - App Store ve Google Play Store yayını
   - Otomatik versiyon yönetimi

## 3. Güvenlik ve Test Kuralları

### 3.1. Güvenlik Kuralları

1. **Kimlik Doğrulama ve Yetkilendirme:**
   - JWT token kullanılmalıdır
   - Token'lar kısa süreli olmalıdır (maksimum 1 saat)
   - Refresh token mekanizması uygulanmalıdır
   - Role-based access control (RBAC) uygulanmalıdır
   - API'lerde yetki kontrolü yapılmalıdır

2. **Veri Güvenliği:**
   - Hassas veriler şifrelenmelidir (kullanıcı şifreleri, kişisel bilgiler)
   - TLS/SSL kullanılmalıdır
   - CORS politikaları uygulanmalıdır
   - Input validasyonu yapılmalıdır
   - SQL injection koruması sağlanmalıdır

3. **İşlem Güvenliği:**
   - Tüm işlemler loglanmalıdır
   - Kritik işlemlerde çift doğrulama istenmelidir
   - Rate limiting uygulanmalıdır
   - IP bazlı kısıtlamalar uygulanabilmelidir
   - Şüpheli aktiviteler raporlanmalıdır

### 3.2. Test Kuralları

1. **Unit Testler:**
   - Backend: xUnit kullanılacaktır
   - Frontend: Jest kullanılacaktır
   - Mobil: Jest kullanılacaktır
   - Minimum %80 test coverage hedeflenmelidir

2. **Entegrasyon Testleri:**
   - API entegrasyon testleri yazılmalıdır
   - Service katmanı entegrasyon testleri yazılmalıdır
   - Test veritabanı kullanılmalıdır

3. **UI Testleri:**
   - Frontend: React Testing Library kullanılacaktır
   - Mobil: Detox kullanılacaktır
   - Temel kullanıcı senaryoları otomatize edilmelidir

4. **Performans Testleri:**
   - API endpoint'leri için yük testleri yapılmalıdır
   - Database query'leri optimize edilmelidir
   - Frontend render performansı ölçülmelidir

5. **Güvenlik Testleri:**
   - OWASP Top 10 güvenlik açıklarına karşı test yapılmalıdır
   - Static code analysis uygulanmalıdır
   - Authentication ve authorization testleri yapılmalıdır

### 3.3. Kalite Kontrol Kuralları

1. **Kod Kalitesi:**
   - Static code analysis araçları kullanılmalıdır
   - Code review yapılmalıdır
   - Coding standards uygulanmalıdır
   - Teknik borç takibi yapılmalıdır

2. **Continuous Integration:**
   - Her commit sonrası otomatik build ve test yapılmalıdır
   - Test başarısız olursa merge engellenmelidir
   - Code coverage raporu oluşturulmalıdır

3. **Dokümantasyon:**
   - API dokümantasyonu güncel tutulmalıdır
   - Kod içi yorum satırları eklenmelidir
   - Teknik dokümantasyon hazırlanmalıdır
   - Kullanıcı kılavuzu hazırlanmalıdır

## 4. Geliştirme Araçları ve Ortamlar

### 4.1. Geliştirme Araçları

1. **IDE ve Editörler:**
   - Visual Studio 2022 (Backend)
   - Visual Studio Code (Frontend ve Mobil)
   - Cursor (Tüm geliştirme sürecinde)

2. **Veritabanı Araçları:**
   - SQL Server Management Studio
   - Azure Data Studio
   - Entity Framework Core Tools

3. **API Geliştirme Araçları:**
   - Swagger UI
   - Postman
   - Insomnia

4. **Frontend Geliştirme Araçları:**
   - React Developer Tools
   - Redux DevTools
   - Lighthouse
   - Chrome DevTools

5. **Kod Kalite Araçları:**
   - ESLint
   - Prettier
   - SonarQube
   - StyleCop

6. **Versiyon Kontrol:**
   - Git
   - GitHub / Azure DevOps
   - GitKraken / SourceTree

### 4.2. Ortamlar

1. **Geliştirme Ortamı (Development):**
   - Geliştirici makinelerinde lokal ortam
   - Konfigürasyon: development
   - Fake/mock services kullanılabilir
   - Debug mode aktif

2. **Test Ortamı (Staging):**
   - Konfigürasyon: staging
   - Gerçek backend API'leri
   - Test veritabanı
   - Test kullanıcıları ve veriler

3. **Üretim Ortamı (Production):**
   - Konfigürasyon: production
   - Optimizasyon aktif
   - Gerçek veriler
   - Monitoring ve alerting aktif

### 4.3. Deployment ve CI/CD

1. **Backend Deployment:**
   - Azure App Service
   - Docker containerization
   - Azure DevOps Pipeline

2. **Frontend Deployment:**
   - Azure Static Web Apps
   - Netlify / Vercel
   - Docker containerization

3. **Mobil Deployment:**
   - App Store Connect
   - Google Play Console
   - CI/CD ile otomatik build

4. **CI/CD Pipeline:**
   - Build, test, deploy otomasyonu
   - Environment-specific konfigürasyonlar
   - Automatic versioning
   - Rollback mekanizması

## 5. Kurulum ve Başlangıç Kılavuzu

### 5.1. Backend Kurulum

1. **Gereksinimler:**
   - .NET 8 SDK
   - SQL Server 2019+
   - Visual Studio 2022

2. **Kurulum Adımları:**
   ```
   cd C:\c\Residence Management\Backend
   dotnet restore
   dotnet ef database update
   dotnet build
   dotnet run
   ```

3. **Ortam Değişkenleri:**
   - `ASPNETCORE_ENVIRONMENT`: Development, Staging, Production
   - `CONNECTION_STRING`: Veritabanı bağlantı stringi
   - `JWT_SECRET`: JWT token için secret key
   - `CORS_ORIGINS`: İzin verilen originler

### 5.2. Frontend Kurulum

1. **Gereksinimler:**
   - Node.js 18+
   - npm 9+
   - Yarn (tercihen)

2. **Kurulum Adımları:**
   ```
   cd C:\c\Residence Management\FrondEnd
   npm install
   npm run start
   ```

3. **Ortam Değişkenleri:**
   - `REACT_APP_API_URL`: Backend API URL
   - `REACT_APP_ENV`: development, staging, production
   - `REACT_APP_VERSION`: Uygulama versiyonu

### 5.3. Mobil Kurulum

1. **Gereksinimler:**
   - Node.js 18+
   - npm 9+
   - React Native CLI
   - Android Studio / Xcode

2. **Kurulum Adımları:**
   ```
   cd C:\c\Residence Management\mobil
   npm install
   npx react-native run-android
   npx react-native run-ios
   ```

3. **Ortam Değişkenleri:**
   - `API_URL`: Backend API URL
   - `ENV`: development, staging, production
   - `VERSION`: Uygulama versiyonu

### 5.4. Veritabanı Kurulumu

1. **SQL Server Kurulumu:**
   - SQL Server 2019+ kurulumu yapılmalıdır
   - Authentication mode: Mixed Mode
   - SQL Server Agent aktif olmalıdır

2. **Veritabanı Oluşturma:**
   - `ResidenceManagement` adında veritabanı oluşturulmalıdır
   - Entity Framework Code First yaklaşımı ile tablolar oluşturulacaktır
   - Migration'lar uygulanmalıdır

3. **Seed Data:**
   - Temel system ayarları
   - Admin kullanıcısı
   - Test verileri (geliştirme ortamı için)

## 6. Sonuç ve Hatırlatmalar

### 6.1. Önemli Hatırlatmalar

1. **Geliştirme Sıralaması:**
   - Önce backend API ve veritabanı
   - Sonra frontend web uygulaması
   - En son mobil uygulama

2. **Proje Yapısı:**
   - Tüm geliştirmeler belirtilen klasör yapısında yapılmalıdır
   - Her bileşen kendi klasöründe olmalıdır
   - Kod standartlarına uyulmalıdır

3. **Versiyon Kontrolü:**
   - Değişiklikler için branch oluşturulmalıdır
   - Pull request ile merge yapılmalıdır
   - Code review yapılmalıdır

### 6.2. Başarı Kriterleri

1. **Kalite Kriterleri:**
   - Minimum %80 test coverage
   - Sıfır kritik güvenlik açığı
   - Performans metriklerine uygunluk
   - Kullanıcı dostu arayüz

2. **Teknik Kriterler:**
   - Temiz kod yapısı
   - Minimum teknik borç
   - Ölçeklenebilir mimari
   - Dokümante edilmiş kodlar

3. **İş Kriterleri:**
   - Tüm gereksinimlerin karşılanması
   - Kullanıcı geri bildirimlerinin dikkate alınması
   - Zamanında teslimat
   - Düşük hata oranı

Bu kurallar, Rezidans ve Site Yönetim Sistemi'nin başarılı bir şekilde geliştirilmesi için uyulması gereken prensipleri belirlemektedir. Tüm ekip üyeleri bu kurallara uymakla yükümlüdür.banı Yönetimi:** Tüm veritabanı işlemleri (tablo oluşturma, güncelleme) Entity Framework Core üzerinden migration aracılığı ile yapılacaktır.

3. **Repository Pattern:** Veritabanı erişimleri için Repository Pattern kullanılacaktır.

4. **Service Layer:** İş mantığı service layer üzerinden yönetilecektir.

5. **API Standartları:**
   - RESTful API prensipleri takip edilecektir
   - Tüm endpoint'ler `/api/v1/[resource]/[action]` formatında olacaktır
   - API yanıtları standart format içerecektir (success, data, error)

6. **Güvenlik:**
   - JWT token tabanlı authentication kullanılacaktır
   - Role-based authorization uygulanacaktır
   - Tüm API endpoint'lerinde yetki kontrolü yapılacaktır

7. **Validasyon:** Tüm girdiler server tarafında valide edilecektir (FluentValidation kullanılabilir).

8. **Exception Handling:** Global exception handling yapısı kurulacak, tüm hatalar merkezi olarak loglanacaktır.

9. **Logging:** Yapılandırılabilir loglama sistemi kurulacaktır (NLog, Serilog vb.).

10. **Dokümantasyon:** 
    - API'ler Swagger ile dokümante edilecektir
    - Kod içi yorum satırları eklenecektir
    - README.md dosyaları ile projeler açıklanacaktır
    - Migration'lar için açıklayıcı yorumlar eklenecektir

### 2.2. Frontend Geliştirme Kuralları (FrondEnd Klasörü)

> **ÖNEMLİ:** Frontend geliştirmesi, backend mimarisi tamamlandıktan sonra başlayacaktır. Frontend geliştirmesi, backend API'lerini kullanarak kullanıcı arayüzünü oluşturacaktır. Backend tamamlanmadan, frontend geliştirmesine başlanmamalıdır.

1. **Teknoloji Seçimi:** Frontend React.js ve Material UI ile geliştirilecektir.

2. **Kod Organizasyonu:**
   - `components/` klasöründe ortak bileşenler yer alacaktır
   - `pages/` klasöründe sayfa bileşenleri yer alacaktır
   - `services/` klasöründe API çağrıları yer alacaktır
   - `utils/` klasöründe yardımcı fonksiyonlar yer alacaktır
   - `theme/` klasöründe tema yapılandırması yer alacaktır
   - `i18n/` klasöründe dil dosyaları yer alacaktır
   - `context/` klasöründe context tanımları yer alacaktır
   - Her bileşen, kendi klasöründe yer alacaktır (Örn: components/Header/Header.jsx)

3. **Bileşen Yapısı:**
   - Bileşenler maksimum 400 satır olacak şekilde bölünmelidir
   - Props için PropTypes veya TypeScript interfaceleri kullanılmalıdır
   - Fonksiyonel komponentler ve React Hooks kullanılacaktır
   - Her bileşen için unit test yazılmalıdır

4. **State Yönetimi:**
   - Uygulama genelinde React Context API kullanılacaktır
   - Gerektiğinde performans için Redux Toolkit eklenebilir
   - Global state için useReducer pattern tercih edilecektir
   - Form state yönetimi için react-hook-form kullanılabilir

5. **API İletişimi:**
   - Tüm API çağrıları services/ klasöründen yapılacaktır
   - Async/await yapısı kullanılacaktır
   - Tüm API çağrılarında hata yakalama (try/catch) uygulanacaktır
   - Loading state yönetimi yapılacaktır

6. **Kullanıcı Arayüzü:**
   - Tamamen kullanıcı dostu ve son teknoloji UI ile geliştirilecektir
   - Tüm ekranlardaki form tasarımları birbiri ile uyumlu olacaktır
   - Responsive tasarım prensipleri uygulanacaktır
   - Material UI bileşenleri tutarlı kullanılacaktır

7. **Form Fonksiyonları:**
   - Tüm form ekranlarında Excel çıktısı alma, PDF çıktı alma özelliği olacaktır
   - Raporlama ekranı ve ilgili işleme ait hareketlerin görüntülenebildiği detay menüsü olacaktır
   - Listeleme ekranlarında filtreleme, sıralama ve arama özellikleri olacaktır

8. **Dashboard:**
   - Projenin kapsamlı grafiksel ve metinsel detaylarla genişletilmiş bir dashboard'u olacaktır
   - Dashboard site içerisindeki birçok konuda özet bilgiyi aktarabilmelidir
   - Farklı kullanıcı rolleri için özelleştirilmiş dashboard görünümleri sunulacaktır

9. **Kod Kalitesi:**
   - ESLint ve Prettier kullanılarak kod formatı standartlaştırılacaktır
   - Tüm komponenler için JSDoc dokümantasyonu yazılacaktır
   - Memory leak'leri önlemek için useEffect cleanup fonksiyonları kullanılacaktır
   - React Performance Best Practices uygulanacaktır

10. **Çoklu Dil Desteği:**
    - i18next kullanılarak çoklu dil desteği sağlanacaktır
    - Tüm metinler kaynak dosyalarından alınacaktır
    - Varsayılan dil Türkçe olacaktır
    - İngilizce dil desteği eklenecektir

11. **Test Standartları:**
    - Her komponent için unit testler yazılacaktır (Jest, React Testing Library)
    - Kritik fonksiyonlar için entegrasyon testleri yazılacaktır
    - End-to-end testler için Cypress kullanılacaktır
    - Test coverage %80'in üzerinde olmalıdır