# Hata Çözümleri Bilgi Tabanı

Bu dosya, projede karşılaşılan hataların ve çözümlerinin kaydedildiği merkezi bir bilgi tabanıdır.

## [KbsNotificationRepository type or namespace not found]
Çözüm: 
- Hata, `KbsNotificationRepository` sınıfının yanlış namespace'den referans edilmesinden kaynaklanıyor
- Doğru namespace: `ResidenceManagement.Infrastructure.Repositories`
- DependencyInjection.cs ve UnitOfWork.cs dosyalarında namespace referansları düzeltilmeli
- Çözüm adımları:
  1. DependencyInjection.cs'de: `services.AddScoped<IKbsNotificationRepository, Repositories.KbsNotificationRepository>();`
  2. UnitOfWork.cs'de: `new Infrastructure.Repositories.KbsNotificationRepository(_dbContext);`

## [IAuditLogService type or namespace not found]
Çözüm:
- Hata, `IAuditLogService` interface'inin yanlış namespace'den referans edilmesinden kaynaklanıyor
- Doğru namespace: `ResidenceManagement.Core.Services`
- DependencyInjection.cs dosyasında namespace referansı düzeltilmeli
- Çözüm adımları:
  1. DependencyInjection.cs'de: `services.AddScoped<Core.Services.IAuditLogService, AuditLogService>();`

## [ResidenceManagement.Infrastructure.Services.AuditLogService cannot be used as TImplementation]
Çözüm:
- Hata, `AuditLogService` sınıfının `IAuditLogService` interface'ini doğru şekilde implement etmemesinden kaynaklanıyor
- Çözüm adımları:
  1. AuditLogService sınıfının IAuditLogService interface'ini implement ettiğinden emin olun
  2. Tüm interface metodlarının implementasyonunu kontrol edin
  3. Namespace'lerin doğru olduğundan emin olun

## Genel Hata Çözüm Stratejisi
1. Hata mesajını tam olarak anlayın
2. İlgili dosyaları ve bağımlılıkları kontrol edin
3. Namespace ve referans hatalarını düzeltin
4. Interface implementasyonlarını kontrol edin
5. Çözümü knowledge-base.md'ye kaydedin

## Önemli Notlar
- Her hata çözümü bu dosyaya kaydedilmelidir
- Çözümler adım adım ve açık bir şekilde belgelenmelidir
- Benzer hatalar için önceki çözümler kontrol edilmelidir
- Yeni çözümler eklendikçe bu dosya güncellenmelidir

## [RefreshTokenRepository.cs dosyasındaki hatalar]Çözüm:
RefreshTokenRepository.cs dosyasının içinde ApplicationDbContext sınıfından kod parçaları karışmıştı. Dosya içinde sınıf dışında kod parçaları vardı ve bu nedenle derlenirken hatalar oluşuyordu. Sorunu çözmek için:

1. RefreshTokenRepository.cs dosyasındaki sınıf dışına taşınmış kod parçalarını temizledik
2. Yalnızca RefreshTokenRepository sınıfına ait kodu bıraktık
3. SetCompanyAndBranchId ve SetFirmaAndBranchId metotlarını XML dökümantasyon yorumlarıyla düzenledik

Bu tür hatalar genellikle kesme-yapıştırma sırasında veya dosyaların birleştirilmesi sırasında oluşabilir. Özellikle farklı sınıfların kodları aynı dosyaya karıştığında bu tür sentaks hataları meydana gelir. 