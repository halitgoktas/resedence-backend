# Rezidans Yönetim Sistemi - Bilgi Tabanı

## Kritik Talepler ve Kurallar

### Proje Geliştirme Sıralaması
- Geliştirme yapılacak sıralamanın `.cursor/rules/yapilacaklar.md` belgesine göre yapılması gerekiyor
- Her tamamlanan görev bu belgede işaretlenmeli

### Hata Çözüm Süreci
- Kullanıcının geçmiş hatalarını ve çözümlerini bu `knowledge-base.md` dosyasında saklanmalı
- Hatalarla karşılaşıldığında önce bu dosya kontrol edilmeli
- Yeni hatalar çözüldüğünde bunlar buraya kaydedilmeli
- Kayıt formatı: `## [Hata Mesajı]Çözüm: [Çözüm Açıklaması]`

### Hata Önleme Kuralları
- Tip dönüşümlerinde açık (explicit) dönüşüm kullanılmalı
- Nullable tiplerle çalışırken null kontrolü yapılmalı
- ReadOnlySpan<byte> gerektiren metotlara int? değerleri geçirirken ToString() dönüşümü yapılmalı
- DTO ve Entity sınıflarının tutarlı olması sağlanmalı
- Interface implementasyonları tam yapılmalı
- Namespace yönetimine dikkat edilmeli
- Kodlarda try blokları, parantezler ve metot gövdeleri tam olmalı
- Dependency Injection kurallarına uyulmalı

### Optimized Genel Kurallar
- Projede Clean Architecture/Onion Architecture prensipleri uygulanmakta
- Her katmanın belirli sorumlulukları var (Core, Application, Infrastructure, API)
- SOLID prensipleri, asenkron programlama, veritabanı optimizasyonları gibi konulara dikkat edilmeli
- Multi-tenant mimaride FirmaID ve SubeID alanları kullanılıyor

## Tamamlanan İşler ve Değişiklikler

### LoggingServiceExtensions.cs Düzeltmeleri
- LogLevel belirsizliği çözüldü - Microsoft.Extensions.Logging.LogLevel kullanımı belirtildi
- ActionArguments erişimi için class-level değişken kullanımı eklendi
- Null kontrolü eklenerek güvenli null işleme sağlandı

### Dokümantasyon Oluşturma
- Projenin kapsamlı özelliklerini açıklayan index.html dosyası oluşturuldu
- Belge, UI bileşenleri, mimari yapı ve özellikler hakkında detaylı bilgi içeriyor

## Hatırlanması Gereken Önemli Noktalar

### Proje Özellikleri
- Multi-tenant yapı (FirmaID ve SubeID ile)
- Çoklu dil desteği (TR, EN, DE, RU, AR, FA)
- Çoklu para birimi desteği (TL, EUR, USD, GBP)
- JWT tabanlı kimlik doğrulama
- Rol tabanlı yetkilendirme

### Derleme Sırası
1. Core → Infrastructure → API sırasında derleme yapılmalı
2. Interface değişikliklerinden sonra implementasyon sınıfları güncellenmeli

### Eğitim İhtiyaçları
- Tip dönüşümleri ve null kontrolleri konusunda dikkatli olunmalı
- Entity-DTO ilişkilerinde tutarlılık sağlanmalı
- Repository implementasyonlarında DbContext kullanımına dikkat edilmeli

## Yakında Yapılacak İşler
- `.cursor/rules/yapilacaklar.md` dosyasında belirtilen sıradaki işlere geçilecek
- Eksik kalan loglama işlemleri tamamlanacak
- Response standardizasyonu geliştirilecek
- Entity Framework Core yapılandırması optimize edilecek 

## [ApiResponse.Success yöntem gibi kullanılamaz]Çözüm: 
ApiControllerBase sınıfında `ApiResponse.Success()` ve `ApiResponse.Failure()` metot çağrıları hatalıydı. Doğru metot isimleri `ApiResponse.CreateSuccess()` ve `ApiResponse.CreateFail()` olarak düzeltildi. ApiResponse sınıfında "Success" bir property olduğu için bu şekilde kullanılamaz. Her zaman ApiResponse sınıfındaki factory metotlarının (CreateSuccess, CreateFail) doğru isimlerle çağrılmasına dikkat edilmeli. 