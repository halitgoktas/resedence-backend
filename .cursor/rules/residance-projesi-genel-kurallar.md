# Cursor Uygulaması - Kapsamlı Geliştirme Kuralları

Bu dokümantasyon, Cursor üzerinde SQL Server, C# ve React.js ile geliştirilen projeler için geçerli kuralları kategori bazında toplar. Her yeni özellik, düzeltme veya refaktör işlemi bu belgedeki başlıklara uygun olarak yürütülmelidir.

## 1. Her Zaman Geçerli Temel Kurallar

- Gerekli tüm kütüphaneleri ekle ve eksiksiz olduklarını doğrula
- Projeyi MVVM mimarisi ile oluştur
- Gerekli klasörleri yarat ve varlıklarını kontrol et
- İsteği mümkün olan en az kod ile gerçekleştir
- Yazılan kod SOLID prensiplerine uygun olsun
- Okunabilirliği artırmak için anlamlı isimlendirmeler ve Türkçe yorum satırları kullan
- Metot ve sınıflar tek sorumluluk ilkesine sahip olmalı
- Görev tamamlanana kadar durma
- Her önemli fonksiyon için kapsamlı ve Türkçe yorumlar ekle

## 2. Geliştirme Felsefesi

- Temiz, bakımı kolay ve ölçeklenebilir kod yaz
- SOLID prensiplerini uygula
- Fonksiyonel ve deklaratif programlama desenlerini tercih et
- Tip güvenliğini ve statik analizi vurgula
- Komponent odaklı geliştirme yaklaşımını benimse

## 3. Kod Güvenliği

- API isteklerinde validasyon yap
- Hassas verileri güvenli biçimde sakla
- XSS, CSRF gibi açıklara karşı input sanitizasyonu uygula
- Ortam değişkenlerini güvenle yönet
- JWT token doğrulaması uygula
- Rol tabanlı erişim kontrolleri ekle

## 4. Uygulama Performansı

### 4.1 Genel

- Memory leak oluşturmayan kod yaz; mevcut kodu gerekirse refaktör et
- Gereksiz döngü ve işlemleri kaldır
- Veritabanı sorgularını optimize et
- N+1 sorgu problemini engelle

### 4.2 Backend (C#/.NET) Performans

- Asenkron metotları doğru şekilde kullan (async/await)
- Veritabanı bağlantılarını pooling ile yönet
- LINQ sorgularını optimize et
- Veritabanı indekslerini etkili şekilde kullan
- Entity Framework'te Include ve lazy loading kullanımını optimize et

### 4.3 Frontend (React.js) Performans

- React.memo() ile gereksiz yeniden renderlama'ları önle
- useCallback ve useMemo hook'larını stratejik olarak kullan
- İlk yükleme performansını artırmak için code splitting kullan
- Bundle boyutunu küçült ve gereksiz bağımlılıkları azalt
- Görüntü optimizasyonları uygula

## 5. Hata Yönetimi

- Try–catch bloklarını stratejik yerleştir
- Merkezi loglama mekanizması kur
- Kullanıcı dostu hata mesajları göster
- Global exception handler (Backend) + standart hata formatı (API)
- Frontend'de düzgün hata sınırları (error boundaries) uygula

## 6. Kod Bakımı & Genişletilebilirlik

- DRY prensibine uy; tekrar eden kodu ortak metodlara al
- Dependency Injection ile bağımlılık yönet
- Teknik borçları sprint sonunda ele al
- Kodun ileride genişletilebilir ve geliştirilebilir olmasına dikkat et
- Kullanılmayan kodu ve bağımlılıkları temizle

## 7. Dokümantasyon

- Her önemli sınıf & metot için Markdown açıklaması ekle
- API endpoint'leri için ayrı dokümantasyon oluştur
- Kurulum & çalıştırma adımları README.md dosyasında belirt
- Değişiklikler CHANGELOG.md'ye eklenir
- Tüm kod bloklarında // ile başlayan Türkçe yorum satırları ekle

## 8. UI / UX Kuralları

- Responsive tasarım uygulamaları
- Uzun işlemlerde iptal seçeneği
- Sonuçları toast/snackbar bildirimleriyle göster
- Tutarlı font ve spacing kullan
- Gerekli görselleri internetten bul ve ekle
- Fare etkileşimleri için özel cursor sınıfları, hover/focus efektleri
- Erişilebilirlik: tabIndex="0", aria-label vb.
- Ekrana her zaman Türkçe yaz

## 9. İsimlendirme Kuralları

### 9.1 Genel Kurallar

- PascalCase kullan:
  - .NET sınıfları ve metotlar
  - React komponentleri
  - Tip tanımlamaları ve interfaceler
- camelCase kullan:
  - JavaScript/TypeScript değişkenler
  - Fonksiyonlar
  - Props
- UPPERCASE kullan:
  - Sabitler (Constants)
  - Ortam değişkenleri

### 9.2 Özel İsimlendirme Kalıpları

- Olay işleyicilerini 'handle' ile başlat: handleClick, handleSubmit
- Boolean değişkenleri fiillerle başlat: isLoading, hasError, canSubmit
- Custom hook'ları 'use' ile başlat: useAuth, useForm
- Kısaltmalar yerine tam kelimeleri tercih et (bazı istisnalar hariç):
  - err (error)
  - req (request)
  - res (response)
  - props (properties)
  - ref (reference)

## 10. .NET/C# Geliştirici Kılavuzu

### 10.1 Kod Stili

- Tüm API endpointleri için DTO (Data Transfer Object) kullan
- SOLID prensiplerine uygun kod yaz
- Asenkron programlama için async/await kullan
- Repository ve Unit of Work desenlerini uygula
- Defensive Programming yaklaşımıyla null kontrolleri yap

### 10.2 Veritabanı ve Veri Erişimi

- Entity Framework Core Code First yaklaşımı ile çalış
- Fluent API ile ilişkileri ve kısıtlamaları tanımla
- LINQ sorgularını optimize et, gereksiz Include'ları kaldır
- Stored Procedure'ları karmaşık sorgular için kullan
- Her tabloda FirmaID ve SubeID alanları ekle (Multi-tenant)

### 10.3 Güvenlik

- JWT authentication ve role-based authorization uygula
- Hassas verileri şifrele (örn. kişisel veriler)
- API endpoint'lerinde input validasyonu yap
- Cross-Site Scripting (XSS) ve SQL Injection önlemleri al
- OWASP güvenlik prensiplerine uy

### 10.4 Performans

- Veritabanı indekslerini stratejik olarak kullan
- Lazy loading ve eager loading kullanımını optimum şekilde yönet
- Response compression uygula
- Önbellek (caching) mekanizmaları ekle
- SQL Server sorgu performans optimizasyonları uygula

## 11. React.js Geliştirici Kılavuzu

### 11.1 Komponent Mimarisi

- Fonksiyonel komponentleri TypeScript interfaceleri ile kullan
- Komponentleri `function` anahtar kelimesi ile tanımla
- Tekrar kullanılabilir mantığı custom hook'lara çıkar
- Uygun komponent kompozisyonu uygula
- Performans için React.memo() kullan

### 11.2 State Yönetimi

- Redux Toolkit veya Context API kullan
- Server state için React Query veya SWR kullan
- Prop drilling sorunlarını Context ile çöz
- Normalizasyon ile state yapısını düzenle
- Selectors ile state erişimini kapsülle

### 11.3 Form ve Validasyon

- Kontrollü komponentleri form girdileri için kullan
- Form validasyonu uygula (client-side ve server-side)
- Karmaşık formlar için react-hook-form kullan
- Şema validasyonu için Zod veya Joi kullan

### 11.4 Erişilebilirlik (a11y)

- Semantik HTML elementleri kullan
- ARIA öznitelikleri doğru şekilde uygula
- Klavye navigasyon desteği sağla
- Renk kontrastı oranlarını erişilebilirlik standartlarına uygun hale getir
- Mantıklı başlık hiyerarşisi izle

### 11.5 Test

- Jest ve React Testing Library ile birim testler yaz
- Kritik kullanıcı akışları için entegrasyon testleri uygula
- API çağrıları ve harici bağımlılıklar için mock kullan
- Snapshot testlerini seçici olarak kullan

## 12. SQL Server İyi Uygulamalar

### 12.1 Veritabanı Tasarımı

- Normalizasyon kurallarını (3NF) uygula
- İlişkileri düzgün şekilde tasarla (Foreign Keys)
- Uygun veri tiplerini seç
- İndeksleri stratejik olarak yerleştir
- Multi-tenant yapı için tüm tablolarda FirmaID ve SubeID ekle

### 12.2 Sorgu Optimizasyonu

- Execution plan analizi yap
- İndeksleri verimli kullan
- Store Procedure'lar ile karmaşık mantığı veritabanına taşı
- Parametre sniffing sorunları için önlemler al
- Gereksiz JOIN'leri azalt

### 12.3 Veritabanı Güvenliği

- Least privilege prensibi ile kullanıcı yetkileri tanımla
- Hassas verileri encrypt et
- SQL Injection saldırılarına karşı önlem al
- Düzenli yedekleme stratejisi uygula
- Audit logging mekanizması kur

## 13. Rezidans & Site Yönetim Sistemi – Proje Özel Kuralları

### 13.1 Genel

- Backend: .NET 8 Web API + MSSQL
- Frontend: React + Material‑UI
- Mobil: React Native
- Multi‑tenant: Tüm tablolarda FirmaID & SubeID
- Yetki kontrolleri (ekle, yenile, sil, gör)

### 13.2 Dosya Yapısı

- Backend klasörü: C:\c\Residence-Manager\backend
- Frontend klasörü: C:\c\Residence-Manager\frontend

### 13.3 Kod Stili & Yorumlar

- Backend: PascalCase; Frontend: PascalCase / camelCase
- Her dosya ve fonksiyon başında Türkçe açıklama
- Kod, Prettier ve ESLint (Frontend) veya StyleCop (Backend) kullanılarak formatlanacak

### 13.4 State & API

- Context API tabanlı global state
- services/ klasöründe async API çağrıları
- Tüm API isteklerinde hata yakalama (try/catch)

### 13.5 Güvenlik

- JWT auth + refresh token
- Rol tabanlı erişim
- Token'lar kısa süreli (maksimum 1 saat)

### 13.6 Veri Tabanı ve Migration Kuralları

- Entity Framework Code First yaklaşımı
- Migration'lar açıklayıcı isimlerle kaydedilecek
- Tüm sorgularda firma ve şube filtrelemesi

### 13.7 Hata Yönetimi, Performans, Test, CI/CD

- Global exception handler
- Jest/xUnit testleri
- Otomatik pipeline
- Her commit sonrası testler otomatik çalıştırılacak

### 13.8 Geliştirme Sırası

1. Backend & DB
2. Frontend (Web)
3. Mobil (React Native)
4. Her aşama tamamlanmadan sonraki aşamaya geçilmeyecek

## 14. Analitik & Loglama

- Uygulama genelinde kapsamlı analitik ve log toplama altyapısı kur
- Hata durumlarını loglama mekanizmasını oluştur
- Kullanıcı davranışlarını analiz etmek için telemetri ekle
- Performans metriklerini izle ve raporla
- Kritik işlemleri audit log'lara kaydet

## 15. Deployment ve DevOps

- CI/CD pipeline kurulumu
- Docker containerization
- Environment bazlı konfigürasyon
- Otomatik test ve build süreçleri
- Deployment stratejileri (Blue-Green, Canary)
- Infrastructure as Code (IaC) yaklaşımı

## 16. Diğer Önemli Kurallar

- Tüm kod bloklarında Türkçe yorum satırları
- Ekrana ve dokümana her zaman Türkçe yaz
- Her sprint sonunda kod refaktör edilecek
- Kullanılmayan kod ve bağımlılıklar temizlenecek
- Teknik borçlar düzenli olarak ele alınacak