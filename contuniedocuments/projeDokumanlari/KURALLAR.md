# REZİDANS VE SİTE YÖNETİM SİSTEMİ GELİŞTİRME KURALLARI

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin geliştirilmesi sırasında uyulması gereken kuralları içerir.

## 1. Genel Kurallar

### 1.1. Teknoloji Seçimi
- Backend: .NET 8 Web API (C#) ve MSSQL kullanılacaktır.
- Frontend: React.js ve Material UI ile geliştirilecektir.
- Mobil: React Native ile geliştirilecektir.

### 1.2. Geliştirme Sıralaması
- ~~Önce backend API ve veritabanı geliştirilecektir.~~
- ~~Sonra frontend web uygulaması geliştirilecektir.~~
- ~~En son mobil uygulama geliştirilecektir.~~
- ~~Her aşama tamamlanmadan sonraki aşamaya geçilmeyecektir.~~

- API kontratları ve veri modelleri, geliştirme başlamadan önce tanımlanacaktır.
- Frontend ve backend geliştirme süreçleri, API kontratları tanımlandıktan sonra paralel olarak yürütülebilecektir.
- Backend ekibi, API'leri ve veritabanını geliştirirken, frontend ekibi bu API'lere bağlanan arayüzleri geliştirebilecektir.
- Mobil uygulama, temel web arayüzü tamamlandıktan sonra geliştirilmeye başlanacaktır.
- Entegrasyon süreçleri, düzenli olarak yapılacak ve sorunlar öncelikli olarak çözülecektir.
- Paralel geliştirme süreçleri hakkında detaylı bilgi için `PARALEL_GELISTIRME.md` dokümanına başvurulacaktır.

### 1.3. Dil Kullanımı
- Tüm kod dosyalarında, fonksiyon ve değişken isimleri İngilizce olacaktır.
- Yorum satırları, commit mesajları ve dokümantasyon Türkçe olacaktır.
- Kullanıcı arayüzündeki tüm metinler Türkçe olacaktır.

### 1.4. Dokümantasyon
- Karmaşık kod blokları ve algoritmaların başına açıklayıcı Türkçe yorum satırları eklenecektir.
- API'ler Swagger ile dokümante edilecektir.
- Düzenli olarak teknik dokümantasyon güncellenecektir.

### 1.5. Multi-tenant Yapı
- Tüm tablolarda FirmaID ve SubeID alanları bulunacaktır.
- Login olan kullanıcı hangi firma ve şubesi için tanımlandıysa o firma ve şubesi için işlemler yapabilecektir.
- Multi-tenant yapı, veritabanı seviyesinde izolasyon sağlayacaktır.

## 2. Kod Standartları

### 2.1. Genel Kod Standartları
- Kodlama standartları her dil için belirlenen best practice'lere uygun olacaktır.
- Kod okunabilirliği ve bakımı kolay olacak şekilde yazılacaktır.
- "DRY (Don't Repeat Yourself)" prensibi gözetilecektir.
- Kod, "SOLID" prensiplerine uygun olarak yazılacaktır.

### 2.2. İsimlendirme Standartları
- Backend:
  - Class'lar ve property'ler için PascalCase kullanılacaktır.
  - Metot isimleri PascalCase olacaktır.
  - Değişken isimleri camelCase olacaktır.
- Frontend:
  - Component'ler PascalCase ile isimlendirilecektir.
  - Fonksiyonlar ve değişkenler camelCase ile isimlendirilecektir.
  - Sabitler UPPER_SNAKE_CASE ile isimlendirilecektir.

### 2.3. Component Yapısı
- Kodlar, Single Responsibility Principle'a uygun olarak mantıksal bütünlük taşıyan küçük parçalara bölünecektir.
- Komponentler, tekrar kullanılabilir olacak şekilde tasarlanacaktır.
- Component'ler ve sınıflar, mantıksal olarak birbirleriyle ilişkili işlevlere sahip olacaktır.

## 3. Backend Geliştirme Kuralları

### 3.1. Mimari
- Clean Architecture prensiplerine uygun katmanlı mimari kullanılacaktır.
- API, Core, Infrastructure, Test katmanları oluşturulacaktır.
- Domain-Driven Design (DDD) yaklaşımı benimsenecektir.

### 3.2. Veritabanı
- Microsoft SQL Server kullanılacaktır.
- Entity Framework Core ile Code First yaklaşımı uygulanacaktır.
- Stored procedure'ler yerine LINQ sorguları tercih edilecektir.
- Migration'lar düzenli commit'lerle versiyonlanacaktır.

### 3.3. API Tasarımı
- RESTful API prensiplerine uygun endpoint'ler tasarlanacaktır.
- API versiyonlaması URL üzerinden yapılacaktır.
- API'ler, Swagger ile dokümante edilecektir.
- API istekleri ve yanıtları validasyondan geçirilecektir.

### 3.4. Güvenlik
- JWT tabanlı kimlik doğrulama kullanılacaktır.
- Rol tabanlı yetkilendirme uygulanacaktır.
- Tüm kullanıcı girdileri güvenlik açısından valide edilecektir.
- HTTPS protokolü kullanılacaktır.

## 4. Frontend Geliştirme Kuralları

### 4.1. Component Tasarımı
- Atom Design prensipleri gözetilecektir.
- Her component tek bir sorumluluk üstlenecektir.
- Shared component'ler ortak kütüphanede toplanacaktır.
- Props type kontrolü yapılacaktır.

### 4.2. State Yönetimi
- Context API kullanılarak global state yönetilecektir.
- Kompleks state'ler için useReducer kullanılacaktır.
- Gereksiz re-render'ları önlemek için memoization teknikleri kullanılacaktır.

### 4.3. Stil ve Görünüm
- Material UI ana stil kütüphanesi olarak kullanılacaktır.
- Tema değişkenleri merkezi olarak yönetilecektir.
- Responsive tasarım tüm sayfalarda uygulanacaktır.
- Erişilebilirlik (a11y) standartlarına uyulacaktır.

### 4.4. Performans
- Bundle size optimizasyonu yapılacaktır.
- Lazy loading ve code splitting teknikleri kullanılacaktır.
- Büyük listelerde virtualization kullanılacaktır.
- Gereksiz re-render'lar önlenecektir.

## 5. Test Kuralları

### 5.1. Test Kapsamı
- Kritik iş mantığı için kapsamlı birim testleri yazılacaktır.
- API endpoint'leri için entegrasyon testleri geliştirilecektir.
- Frontend için önemli kullanıcı akışları test edilecektir.
- Testler otomatize edilecek ve CI/CD pipeline'da çalıştırılacaktır.

### 5.2. Test Yazma Kuralları
- Her test küçük, bağımsız ve tekrarlanabilir olmalıdır.
- AAA (Arrange, Act, Assert) paternine uyulacaktır.
- Mock'lar ve test fixture'ları doğru şekilde kullanılacaktır.
- Test isimlendirmesi açıklayıcı ve tutarlı olacaktır.

## 6. Code Review Kuralları

### 6.1. Pull Request Süreci
- Her feature veya bug fix için ayrı branch açılacaktır.
- Pull request açılmadan önce kod formatlanacak ve linter hataları giderilecektir.
- Pull request'ler, en az bir kişi tarafından review edilecektir.
- Review yorumları yapıcı ve açıklayıcı olacaktır.

### 6.2. Merge Kriterleri
- Tüm testler başarıyla çalışmalıdır.
- Code review onaylanmış olmalıdır.
- Linter hataları bulunmamalıdır.
- Conflict'ler çözülmüş olmalıdır.

## 7. Deployment Kuralları

### 7.1. Ortamlar
- Development, Test, Staging ve Production ortamları ayrı olacaktır.
- Her ortam için konfigürasyon ayrı yönetilecektir.
- Ortamlar arası geçişlerde data migration planı oluşturulacaktır.

### 7.2. CI/CD Pipeline
- Otomatik build, test ve deployment için CI/CD pipeline kullanılacaktır.
- Deployment öncesi smoke test'ler çalıştırılacaktır.
- Deployment sonrası health check yapılacaktır.
- Rollback prosedürleri tanımlanacaktır.

## 8. Performans Kuralları

### 8.1. API Performansı
- API endpoint'lerinin performans hedefleri, kritiklik seviyelerine göre belirlenecektir.
- N+1 sorguları önlenecektir.
- Büyük veri kümeleri için sayfalama uygulanacaktır.
- Uygun cache stratejileri kullanılacaktır.

### 8.2. Frontend Performansı
- İlk yükleme süresi 3 saniyeden az olmalıdır.
- Sayfa içi geçişler 300ms'den hızlı olmalıdır.
- Web Vitals metrikleri takip edilecektir.
- Performans optimizasyonları düzenli olarak yapılacaktır.

## 9. Dokümantasyon Kuralları

### 9.1. Kod Dokümantasyonu
- Karmaşık algoritmalar ve iş mantığı için açıklayıcı yorumlar eklenecektir.
- Public API'ler için XML dokümantasyonu yazılacaktır.
- Component'ler için prop açıklamaları eklenecektir.

### 9.2. Proje Dokümantasyonu
- Teknik mimari dokümanı güncel tutulacaktır.
- Setup ve deployment talimatları dokümante edilecektir.
- Kullanıcı rehberleri oluşturulacaktır.
- Değişiklik kaydı (CHANGELOG) tutulacaktır.

## 10. Diğer Kurallar

### 10.1. Dış Bağımlılıklar
- Kullanılacak dış kütüphaneler proje başında belirlenecektir.
- Bağımlılıkların sürümleri sabitlenecektir.
- Lisans uyumluluğu kontrol edilecektir.
- Güvenlik açıkları düzenli taranacaktır.

### 10.2. İzleme ve Hata Ayıklama
- Uygulama logları merkezi bir sisteme gönderilecektir.
- Hata izleme sistemleri entegre edilecektir.
- Performans metrikleri toplanacak ve analiz edilecektir.
- Kullanıcı davranışları anonim olarak izlenecektir.

### 10.3. Proje Yönetimi
- İki haftalık sprint'ler ile çalışılacaktır.
- Her sprint sonunda demo yapılacaktır.
- Retrospektif toplantılarında süreçler iyileştirilecektir.
- Teknik borçlar düzenli olarak temizlenecektir.

## 11. Potansiyel Verimsizlikler ve Dikkat Edilecek Durumlar

Aşağıda, proje ilerledikçe takip edilmesi ve gerektiğinde yeniden değerlendirilmesi gereken potansiyel verimsizlikler listelenmiştir:

### 11.1. API Tasarımı
- Çok fazla veya çok detaylı endpoint tanımlamak, bakım yükünü artırabilir.
- Her endpoint için farklı DTO'lar oluşturmak kod tekrarına yol açabilir.

### 11.2. Yorum Gereksinimleri
- Her kod bloğu için açıklama eklemek, kendini açıklayan kod prensibini zayıflatabilir.
- Yorum satırlarının güncel tutulmaması, yanlış yönlendirmelere sebep olabilir.

### 11.3. Test Kapsamı Hedefleri
- Sayısal test kapsama hedefleri, anlamlı olmayan testlerin yazılmasına yol açabilir.
- Kritik olmayan kod bloklarının aşırı test edilmesi, geliştirme hızını düşürebilir.

### 11.4. Geliştirme Ortamı
- Çok katı branch politikaları, küçük değişikliklerin hızlı uygulanmasını engelleyebilir.
- Aşırı dokümantasyon gereksinimleri, geliştirme sürecini yavaşlatabilir.

### 11.5. Frontend-Backend Koordinasyonu
- Frontend ve backend arasında sıkı bağımlılıklar, değişikliklerin uygulanmasını zorlaştırabilir.
- Mock servislerin yetersiz kullanımı, paralel geliştirmeyi engelleyebilir.

Bu kurallar, proje ilerledikçe değerlendirilecek ve gerektiğinde optimizasyon için güncellenecektir. Takım üyeleri, verimsizlik gördükleri noktaları açıkça ifade etmek ve iyileştirme önerileri sunmak konusunda teşvik edilmektedir. 