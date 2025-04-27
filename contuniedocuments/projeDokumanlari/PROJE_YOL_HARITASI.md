# PROJE YOL HARİTASI

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin sıfırdan yüze kadar olan tüm adımlarını içeren detaylı bir yol haritasıdır.

## 0. Ön Hazırlık Aşaması (%0 - %5)

- [x] Proje gereksinimlerinin analizi
- [x] Proje klasör yapısının oluşturulması
- [x] Dokümantasyon dosyalarının hazırlanması
- [ ] Geliştirme ortamının kurulumu
- [ ] Temel proje yapısının oluşturulması
- [ ] Gerekli paketlerin yüklenmesi

## 1. Veritabanı Modellerinin Oluşturulması (%5 - %15)

- [ ] Temel veri modelleri oluşturulması:
  - [ ] BaseEntity, BaseLookupEntity, BaseTransactionEntity sınıfları
  - [ ] Multi-tenant yapısı için FirmaID ve SubeID alanlarının eklenmesi
  - [ ] Site, Block, Apartment modelleri
  - [ ] User, Role, Permission modelleri
  - [ ] Financial transaction modelleri
  - [ ] Reservation ve Activity modelleri
  - [ ] Integration modelleri (KBS, RFID, vb.)
- [ ] Entity Framework Core DbContext sınıfının oluşturulması
- [ ] Entity konfigürasyonlarının yapılması
- [ ] İlişkilerin tanımlanması
- [ ] Migration dosyalarının oluşturulması
- [ ] Seed verilerin hazırlanması

## 2. Repository Katmanının Oluşturulması (%15 - %25)

- [ ] Generic Repository pattern'in uygulanması
- [ ] IRepository interface'inin tanımlanması
- [ ] BaseRepository sınıfının oluşturulması
- [ ] Multi-tenant filtreleme mekanizmasının eklenmesi
- [ ] Entity-spesifik repository'lerin oluşturulması:
  - [ ] UserRepository
  - [ ] SiteRepository
  - [ ] ApartmentRepository
  - [ ] FinancialRepository
  - [ ] ReservationRepository
  - [ ] IntegrationRepository
- [ ] Unit of Work pattern'in uygulanması

## 3. Servis Katmanının Oluşturulması (%25 - %35)

- [ ] Service interface'lerinin tanımlanması
- [ ] Base service sınıfının oluşturulması
- [ ] Entity-spesifik servislerin oluşturulması:
  - [ ] UserService
  - [ ] AuthService
  - [ ] SiteService
  - [ ] ApartmentService
  - [ ] FinancialService
  - [ ] ReservationService
  - [ ] IntegrationService
- [ ] İş mantığı kurallarının uygulanması
- [ ] Validation kurallarının eklenmesi
- [ ] Exception handling mekanizmasının oluşturulması

## 4. API Katmanının Oluşturulması (%35 - %50)

- [ ] BaseController sınıfının oluşturulması
- [ ] JWT tabanlı kimlik doğrulama sisteminin kurulması
- [ ] Role tabanlı yetkilendirme sisteminin kurulması
- [ ] Controller sınıflarının oluşturulması:
  - [ ] AuthController
  - [ ] UserController
  - [ ] SiteController
  - [ ] ApartmentController
  - [ ] FinancialController
  - [ ] ReservationController
  - [ ] IntegrationController
- [ ] API endpoint'lerinin oluşturulması
- [ ] DTO (Data Transfer Object) sınıflarının oluşturulması
- [ ] API versiyonlama yapısının kurulması
- [ ] Swagger/OpenAPI dokümantasyonunun eklenmesi
- [ ] Global exception handling middleware'in eklenmesi
- [ ] API filtrelerinin oluşturulması

## 5. Gelişmiş Backend Özelliklerin Eklenmesi (%50 - %60)

- [ ] İki faktörlü kimlik doğrulama (2FA) desteği
- [ ] Çoklu dil desteği
- [ ] Çoklu para birimi desteği
- [ ] Otomatik kur çekme ve dönüştürme özelliği
- [ ] KBS (Kimlik Bildirim Sistemi) entegrasyonu
- [ ] RFID kart sistemi entegrasyonu
- [ ] Plaka tanıma sistemi entegrasyonu
- [ ] E-posta ve SMS gönderim sistemi
- [ ] Ödeme sistemleri entegrasyonu
- [ ] Excel/PDF çıktı oluşturma özellikleri
- [ ] Cache mekanizmasının uygulanması
- [ ] Background job'ların oluşturulması

## 6. Backend Test ve Optimizasyon (%60 - %70)

- [ ] Unit testlerin yazılması
- [ ] Integration testlerin yazılması
- [ ] API testlerin yazılması
- [ ] Sorgu optimizasyonlarının yapılması
- [ ] N+1 sorgu probleminin çözümü
- [ ] Veritabanı indekslerinin oluşturulması
- [ ] API response sürelerinin optimize edilmesi
- [ ] Güvenlik testlerinin yapılması
- [ ] Code review ve refactoring

## 7. Frontend Geliştirme - Temel (%70 - %80)

- [ ] React.js projesinin kurulması
- [ ] Material UI entegrasyonu
- [ ] Proje klasör yapısının düzenlenmesi
- [ ] Routing yapılandırması
- [ ] Context API state yönetiminin kurulması
- [ ] Auth ve login sisteminin uygulanması
- [ ] Temel komponentlerin geliştirilmesi
- [ ] API servislerinin oluşturulması
- [ ] Tablo ve form komponentlerinin geliştirilmesi
- [ ] Çoklu dil desteğinin uygulanması
- [ ] Dashboard sayfasının oluşturulması

## 8. Frontend Geliştirme - İleri (%80 - %90)

- [ ] Tüm modül sayfalarının oluşturulması
- [ ] Veri filtreleme ve sıralama özelliklerinin eklenmesi
- [ ] Excel/PDF çıktı alma özelliklerinin eklenmesi
- [ ] Grafiksel raporlama bileşenlerinin eklenmesi
- [ ] Form validasyonlarının tamamlanması
- [ ] Gelişmiş UI özellikleri (animasyonlar, drag-drop, vb.)
- [ ] Responsive tasarımın tamamlanması
- [ ] Tema ve stil özelleştirmelerinin eklenmesi
- [ ] Performans optimizasyonları
- [ ] Frontend testlerin yazılması

## 9. Proje Tamamlama ve Canlıya Alma (%90 - %100)

- [ ] Son testlerin yapılması
- [ ] Hataların düzeltilmesi
- [ ] Dokümantasyonun tamamlanması
- [ ] Kullanıcı kılavuzlarının hazırlanması
- [ ] Deployment script'lerinin hazırlanması
- [ ] CI/CD pipeline'ının kurulması
- [ ] Production ortamının hazırlanması
- [ ] Canlıya alma
- [ ] Kullanıcı eğitimlerinin verilmesi
- [ ] Bakım ve destek süreçlerinin planlanması

## 10. Mobil Uygulama (Gelecek Aşama)

- [ ] React Native projesinin kurulması
- [ ] Mobil UI bileşenlerinin geliştirilmesi
- [ ] Backend API entegrasyonu
- [ ] Push notification desteği
- [ ] Kamera ve konum servisleri entegrasyonu
- [ ] Biyometrik kimlik doğrulama
- [ ] Offline çalışma modu
- [ ] App Store ve Google Play Store yayını

## İlerleme Durumu

- **Genel İlerleme:** %0
- **Backend Geliştirme:** %0
- **Frontend Geliştirme:** %0
- **Test ve Dokümantasyon:** %5

> Not: Bu yol haritası, projenin ilerleyişine göre güncellenecektir. Her bir adım tamamlandığında, ilgili madde işaretlenecek ve ilerleme yüzdeleri güncellenecektir. 