# Rezidans Yönetim Sistemi - Changelog

Bu doküman, Rezidans Yönetim Sistemi projesindeki önemli değişiklikleri ve gelişmeleri kronolojik sırayla listeler.

## [Unreleased]

### Eklenenler
- Projenin kapsamlı özelliklerini açıklayan `index.html` dokümantasyon sayfası
- Bilgi tabanı için `.cursor/knowledge-base.md` dosyası
- Multi-tenant yapı için temel altyapı geliştirmeleri

### Değişiklikler
- LoggingServiceExtensions.cs dosyasında LogLevel belirsizliği düzeltildi
- ActionArguments erişim problemi çözüldü
- Null kontrolü ve güvenli null işleme eklendi

### Düzeltmeler
- LoggingServiceExtensions.cs'deki derlenme hataları giderildi
- OperationLogService.cs'deki ambiguous referanslar temizlendi
- Infrastructure ve API projelerinde build sorunları giderildi

## [0.1.0] - 2024-04-27

### Eklenenler
- JWT Token tabanlı kimlik doğrulama
- Refresh Token mekanizması
- Token validasyon middleware
- Role-based ve policy-based yetkilendirme
- Kullanıcı giriş/çıkış işlemleri
- CORS politikaları
- API versiyonlama
- Repository pattern ve UnitOfWork implementasyonu
- FluentValidation entegrasyonu
- Global exception handler middleware

### Geliştirilen Altyapı Özellikleri
- Multi-tenant mimari (FirmaID ve SubeID)
- Çoklu dil desteği (TR, EN, DE, RU, AR, FA)
- Çoklu para birimi desteği (TL, EUR, USD, GBP)
- Clean Architecture/Onion Architecture prensipleri
- Asenkron veri erişim metotları

### Teknik Konular
- Entity Framework Core entegrasyonu
- SOLID prensiplerinin uygulanması
- Dependency Injection yapılandırması
- API response standardizasyonu
- Hata kod standardı ve mesajları 