1. Temel Mimari ve Altyapı (İlk Build)
Program.cs dosyası halihazırda tamamlanmış durumda. Şimdi sıradaki adımlar üzerinde çalışmalıyız:
Base entity yapılarının tamamlanması
Entity Framework DbContext yapılandırması
Repository ve Unit of Work pattern'in uygulanması
Multi-tenant filtre mekanizmasının implementasyonu
CORS, DI ve temel middleware yapılandırması
Öncelikle mevcut entity yapılarını inceleyip, eksik kısımları tamamlamalıyız. Sonrasında veritabanı bağlantı yapılandırmasını ve repository pattern'i uygulamalıyız.
Bu aşama tamamlandığında, ilk build alınmalı ve temel yapının sağlam çalıştığından emin olunmalıdır. Veritabanı migrasyonları oluşturulup test ortamında çalıştırılmalıdır.
2. Kimlik Doğrulama ve Yetkilendirme Altyapısı
Bu aşamada JWT authentication, rol yapısı ve token mekanizması üzerine odaklanılmalıdır. Multi-tenant yapısı ile JWT entegrasyonu özellikle önemlidir.
3. Temel Veri Modeli API'leri
Firma, şube, site, blok, daire ve kullanıcı gibi temel veri modellerinin API'leri oluşturulmalıdır. Bu aşamada, sistemin çekirdek veri modelleri üzerinde CRUD işlemleri yapılabilir olmalıdır.
Bu aşamaları sırasıyla tamamladıktan sonra, 4 numaralı "Servis Katmanı ve İş Mantığı" aşamasına geçeceğiz. Burada AutoMapper, FluentValidation ve iş mantığı implementasyonları yapılacaktır.
Her aşamayı tamamladıkça, ilgili maddeleri eklenecekler.md dokümanında işaretleyeceğiz ve CHANGELOG.md dosyasına ekleyeceğiz.
Belirli aşamalarda düzenli build alınmalı ve her bir aşamayı tamamlama sürecinde, dokümantasyonda belirtilen build stratejisini izlemeliyiz. Özellikle:
Development build'i günlük olarak
Integration build'i her feature tamamlandığında
Staging build'i her sprint sonunda
Build süreçlerinde code quality check, unit testler, integration testler, statik kod analizi ve güvenlik taramaları yapılması kritik öneme sahiptir.
Bir sonraki adımda hangi özelliğe odaklanmamız gerektiğini belirtirseniz, o konuda daha detaylı yardımcı olabilirim.