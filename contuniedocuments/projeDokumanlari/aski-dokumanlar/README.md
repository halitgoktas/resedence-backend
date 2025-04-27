# Rezidans Yönetim Sistemi

## Proje Amacı
Rezidans Yönetim Sistemi, modern rezidans ve site yönetiminin tüm ihtiyaçlarını karşılayan, kapsamlı bir yönetim platformudur. Sistem, rezidans sakinleri, site yöneticileri ve yönetim kurulları için güvenli, verimli ve kullanıcı dostu bir ortam sunar.

## Temel Özellikler

### 1. Site ve Bina Yönetimi
- **Site Bilgileri**: Site adı, adres, iletişim bilgileri ve genel özelliklerin yönetimi
- **Bina Yönetimi**: Bina bilgileri, kat sayısı, daire sayısı ve özelliklerin takibi
- **Daire Yönetimi**: Daire numarası, kat bilgisi, alan, oda sayısı ve sakin bilgilerinin yönetimi

### 2. Kullanıcı ve Yetkilendirme
- **Kullanıcı Yönetimi**: Rezidans sakinleri, yöneticiler ve personel için kullanıcı hesapları
- **Rol Tabanlı Yetkilendirme**: Farklı kullanıcı grupları için özelleştirilmiş yetkiler
- **Güvenlik**: Kimlik doğrulama ve yetkilendirme mekanizmaları

### 3. Finansal Yönetim
- **Gider Takibi**: Site giderlerinin kategorize edilmiş şekilde takibi
- **Fatura Yönetimi**: Daire başına fatura oluşturma ve takibi
- **Ödeme Takibi**: Ödemelerin kaydı ve raporlanması

### 4. Bakım ve Onarım
- **Bakım Talepleri**: Sakinlerin bakım taleplerinin yönetimi
- **İş Emirleri**: Bakım işlerinin planlanması ve takibi
- **Durum Takibi**: Bakım işlerinin durumlarının izlenmesi

### 5. Duyuru ve Etkinlik Yönetimi
- **Duyurular**: Önemli bilgilerin sakinlere iletilmesi
- **Etkinlikler**: Site içi etkinliklerin planlanması ve duyurulması
- **Katılım Takibi**: Etkinliklere katılımın yönetimi

### 6. Erişim Kontrolü
- **Erişim Kartları**: Sakinler ve personel için erişim kartı yönetimi
- **Erişim İzinleri**: Farklı alanlar için erişim izinlerinin yönetimi
- **Erişim Kayıtları**: Erişimlerin kaydı ve raporlanması

### 7. Belge Yönetimi
- **Belge Arşivi**: Önemli belgelerin güvenli saklanması
- **Versiyon Kontrolü**: Belge versiyonlarının takibi
- **İzin Yönetimi**: Belgelere erişim izinlerinin kontrolü

### 8. Bildirim Sistemi
- **Anlık Bildirimler**: Önemli olaylar için anlık bildirimler
- **E-posta Bildirimleri**: Düzenli bilgilendirme e-postaları
- **Öncelikli Bildirimler**: Acil durumlar için öncelikli bildirimler

## Teknik Yapı

### Veritabanı Modelleri

#### 1. Temel Modeller
- **Site**: Rezidans kompleksinin temel bilgileri
- **Bina**: Site içindeki binaların bilgileri
- **Daire**: Binalardaki dairelerin bilgileri

#### 2. Kullanıcı Modelleri
- **User**: Sistem kullanıcıları
- **Role**: Kullanıcı rolleri
- **UserRole**: Kullanıcı-rol ilişkileri

#### 3. Finansal Modeller
- **Expense**: Site giderleri
- **Invoice**: Daire faturaları
- **Payment**: Ödemeler

#### 4. Bakım Modelleri
- **Maintenance**: Bakım talepleri
- **MaintenanceType**: Bakım türleri
- **MaintenanceStatus**: Bakım durumları

#### 5. İletişim Modelleri
- **Announcement**: Duyurular
- **Event**: Etkinlikler
- **Notification**: Bildirimler

#### 6. Güvenlik Modelleri
- **AccessCard**: Erişim kartları
- **AccessPermission**: Erişim izinleri
- **AccessLog**: Erişim kayıtları

## Faydalar

### Site Yönetimi İçin
1. **Verimli Yönetim**: Tüm site işlemlerinin merkezi yönetimi
2. **Maliyet Kontrolü**: Giderlerin detaylı takibi ve kontrolü
3. **İletişim Kolaylığı**: Sakinlerle hızlı ve etkili iletişim
4. **Güvenlik**: Erişim kontrolü ve kayıt tutma
5. **Raporlama**: Detaylı raporlar ve analizler

### Rezidans Sakinleri İçin
1. **Kolay İletişim**: Yönetimle hızlı iletişim
2. **Online İşlemler**: Fatura ödeme, bakım talebi gibi işlemler
3. **Bilgilendirme**: Duyuru ve etkinliklerden anında haberdar olma
4. **Güvenlik**: Erişim kontrolü ve güvenlik önlemleri
5. **Hizmet Kalitesi**: Hızlı ve etkili hizmet alımı

### Yönetim Kurulu İçin
1. **Şeffaflık**: Tüm işlemlerin takip edilebilirliği
2. **Karar Desteği**: Detaylı raporlar ve analizler
3. **Maliyet Optimizasyonu**: Giderlerin kontrolü ve optimizasyonu
4. **Risk Yönetimi**: Güvenlik ve bakım risklerinin yönetimi
5. **Sakin Memnuniyeti**: Sakinlerin ihtiyaçlarının hızlı karşılanması

## Sistem Gereksinimleri
- .NET 6.0 veya üzeri
- SQL Server 2019 veya üzeri
- Windows Server 2019 veya üzeri
- Minimum 4GB RAM
- Minimum 50GB depolama alanı

## Güvenlik Özellikleri
1. **Rol Tabanlı Erişim Kontrolü**
2. **İki Faktörlü Kimlik Doğrulama**
3. **Şifre Politikaları**
4. **Oturum Yönetimi**
5. **Erişim Logları**
6. **Veri Şifreleme**

## Entegrasyonlar
1. **SMS Servisleri**
2. **E-posta Servisleri**
3. **Ödeme Sistemleri**
4. **Güvenlik Sistemleri**
5. **Muhasebe Yazılımları**

## Gelecek Geliştirmeler
1. **Mobil Uygulama**
2. **Yapay Zeka Destekli Analizler**
3. **Akıllı Ev Entegrasyonu**
4. **Blockchain Tabanlı Güvenlik**
5. **Gelişmiş Raporlama Araçları**

## Kurulum Kılavuzu

### 1. Gereksinimlerin Hazırlanması
- .NET 6.0 SDK'nın yüklenmesi
- SQL Server 2019 veya üzeri veritabanı sunucusunun kurulumu
- Visual Studio 2022 veya Visual Studio Code'un yüklenmesi

### 2. Proje Kurulumu
1. Proje dosyalarının indirilmesi
2. `appsettings.json` dosyasında veritabanı bağlantı bilgilerinin güncellenmesi
3. Paketlerin yüklenmesi: `dotnet restore`
4. Veritabanı migrasyonlarının uygulanması: `dotnet ef database update`

### 3. Çalıştırma
1. API projesinin başlatılması: `dotnet run`
2. Swagger arayüzü üzerinden API testlerinin yapılması
3. Frontend uygulamasının başlatılması

## API Dokümantasyonu

### Kimlik Doğrulama
- **Login**: `/api/auth/login`
- **Register**: `/api/auth/register`
- **Refresh Token**: `/api/auth/refresh-token`

### Site Yönetimi
- **Site Listesi**: `GET /api/site`
- **Site Detay**: `GET /api/site/{id}`
- **Site Ekleme**: `POST /api/site`
- **Site Güncelleme**: `PUT /api/site/{id}`
- **Site Silme**: `DELETE /api/site/{id}`

### Bina Yönetimi
- **Bina Listesi**: `GET /api/building`
- **Bina Detay**: `GET /api/building/{id}`
- **Bina Ekleme**: `POST /api/building`
- **Bina Güncelleme**: `PUT /api/building/{id}`
- **Bina Silme**: `DELETE /api/building/{id}`

### Daire Yönetimi
- **Daire Listesi**: `GET /api/apartment`
- **Daire Detay**: `GET /api/apartment/{id}`
- **Daire Ekleme**: `POST /api/apartment`
- **Daire Güncelleme**: `PUT /api/apartment/{id}`
- **Daire Silme**: `DELETE /api/apartment/{id}`

## Veritabanı Şeması

### Site Tablosu
- Id (int, PK)
- Name (nvarchar(100))
- Address (nvarchar(500))
- Phone (nvarchar(20))
- Email (nvarchar(100))
- CreatedAt (datetime)
- UpdatedAt (datetime)
- IsActive (bit)

### Bina Tablosu
- Id (int, PK)
- SiteId (int, FK)
- Name (nvarchar(100))
- Block (nvarchar(50))
- NumberOfFloors (int)
- NumberOfApartments (int)
- Description (nvarchar(500))
- CreatedAt (datetime)
- UpdatedAt (datetime)
- IsActive (bit)

### Daire Tablosu
- Id (int, PK)
- BuildingId (int, FK)
- Number (nvarchar(20))
- Floor (int)
- Area (decimal)
- RoomCount (int)
- Description (nvarchar(500))
- CreatedAt (datetime)
- UpdatedAt (datetime)
- IsActive (bit)

## Hata Kodları

### Genel Hatalar
- 400: Geçersiz İstek
- 401: Yetkisiz Erişim
- 403: Erişim Reddedildi
- 404: Kaynak Bulunamadı
- 500: Sunucu Hatası

### Özel Hatalar
- 1001: Kullanıcı Bulunamadı
- 1002: Geçersiz Şifre
- 1003: Token Süresi Doldu
- 2001: Site Bulunamadı
- 2002: Bina Bulunamadı
- 2003: Daire Bulunamadı

## Güvenlik Önlemleri

### 1. Veri Şifreleme
- Hassas verilerin AES-256 ile şifrelenmesi
- Şifrelerin bcrypt ile hash'lenmesi
- SSL/TLS kullanımı

### 2. Erişim Kontrolü
- JWT tabanlı kimlik doğrulama
- Rol tabanlı yetkilendirme
- IP kısıtlamaları

### 3. Güvenlik Duvarı
- SQL enjeksiyon koruması
- XSS koruması
- CSRF koruması

## Performans Optimizasyonu

### 1. Önbellekleme
- Redis önbellek kullanımı
- Sayfa önbellekleme
- Veri önbellekleme

### 2. Veritabanı Optimizasyonu
- İndeksleme
- Sorgu optimizasyonu
- Bağlantı havuzu yönetimi

### 3. Kod Optimizasyonu
- Asenkron işlemler
- Lazy loading
- Memory management

## Destek ve İletişim

### Teknik Destek
- E-posta: support@rezidansyonetim.com
- Telefon: +90 212 123 45 67
- Çalışma Saatleri: 09:00 - 18:00 (Pazartesi - Cuma)

### Dokümantasyon
- API Dokümantasyonu: https://docs.rezidansyonetim.com
- Kullanıcı Kılavuzu: https://guide.rezidansyonetim.com
- SSS: https://faq.rezidansyonetim.com

## Lisans Bilgisi
Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için `LICENSE` dosyasına bakınız.

## Detaylı Kullanım Kılavuzu

### 1. Sistem Girişi ve Kullanıcı Yönetimi

#### 1.1. Kullanıcı Girişi
1. Tarayıcınızdan sisteme erişin
2. Giriş sayfasında e-posta ve şifrenizi girin
3. "Giriş Yap" butonuna tıklayın
4. İki faktörlü doğrulama gerekiyorsa, SMS ile gelen kodu girin

#### 1.2. Şifre Sıfırlama
1. Giriş sayfasında "Şifremi Unuttum" linkine tıklayın
2. E-posta adresinizi girin
3. Gelen e-postadaki linke tıklayın
4. Yeni şifrenizi belirleyin ve onaylayın

#### 1.3. Profil Yönetimi
1. Sağ üst köşedeki profil menüsüne tıklayın
2. "Profilim" seçeneğini seçin
3. Kişisel bilgilerinizi güncelleyin
4. Değişiklikleri kaydedin

### 2. Site Yönetimi

#### 2.1. Yeni Site Ekleme
1. Ana menüden "Site Yönetimi"ni seçin
2. "Yeni Site Ekle" butonuna tıklayın
3. Gerekli bilgileri doldurun:
   - Site adı
   - Adres bilgileri
   - İletişim bilgileri
   - Site özellikleri
4. "Kaydet" butonuna tıklayın

#### 2.2. Site Bilgilerini Güncelleme
1. Site listesinden güncellenecek siteyi seçin
2. "Düzenle" butonuna tıklayın
3. Değiştirilecek bilgileri güncelleyin
4. "Kaydet" butonuna tıklayın

### 3. Bina Yönetimi

#### 3.1. Yeni Bina Ekleme
1. Site detay sayfasından "Binalar" sekmesini seçin
2. "Yeni Bina Ekle" butonuna tıklayın
3. Bina bilgilerini girin:
   - Bina adı
   - Blok bilgisi
   - Kat sayısı
   - Daire sayısı
4. "Kaydet" butonuna tıklayın

#### 3.2. Bina Bilgilerini Güncelleme
1. Bina listesinden güncellenecek binayı seçin
2. "Düzenle" butonuna tıklayın
3. Gerekli değişiklikleri yapın
4. "Kaydet" butonuna tıklayın

### 4. Daire Yönetimi

#### 4.1. Yeni Daire Ekleme
1. Bina detay sayfasından "Daireler" sekmesini seçin
2. "Yeni Daire Ekle" butonuna tıklayın
3. Daire bilgilerini girin:
   - Daire numarası
   - Kat bilgisi
   - Alan bilgisi
   - Oda sayısı
4. "Kaydet" butonuna tıklayın

#### 4.2. Daire Bilgilerini Güncelleme
1. Daire listesinden güncellenecek daireyi seçin
2. "Düzenle" butonuna tıklayın
3. Gerekli değişiklikleri yapın
4. "Kaydet" butonuna tıklayın

### 5. Finansal İşlemler

#### 5.1. Gider Ekleme
1. Ana menüden "Finansal İşlemler"i seçin
2. "Yeni Gider Ekle" butonuna tıklayın
3. Gider bilgilerini girin:
   - Gider kategorisi
   - Tutar
   - Tarih
   - Açıklama
4. "Kaydet" butonuna tıklayın

#### 5.2. Fatura Oluşturma
1. "Faturalar" sekmesini seçin
2. "Yeni Fatura Oluştur" butonuna tıklayın
3. Fatura bilgilerini girin:
   - Daire seçimi
   - Fatura tipi
   - Tutar
   - Son ödeme tarihi
4. "Oluştur" butonuna tıklayın

### 6. Bakım Talepleri

#### 6.1. Yeni Bakım Talebi Oluşturma
1. Ana menüden "Bakım Talepleri"ni seçin
2. "Yeni Talep Oluştur" butonuna tıklayın
3. Talep bilgilerini girin:
   - Talep kategorisi
   - Öncelik seviyesi
   - Açıklama
   - Fotoğraf ekleme (opsiyonel)
4. "Gönder" butonuna tıklayın

#### 6.2. Bakım Talebi Takibi
1. "Bakım Talepleri" sayfasına gidin
2. Taleplerinizi liste halinde görüntüleyin
3. Her talep için durum bilgisini kontrol edin
4. Gerekirse talep detaylarını görüntüleyin

### 7. Duyuru ve Etkinlikler

#### 7.1. Duyuru Oluşturma
1. Ana menüden "Duyurular"ı seçin
2. "Yeni Duyuru" butonuna tıklayın
3. Duyuru bilgilerini girin:
   - Başlık
   - İçerik
   - Hedef kitle
   - Yayın tarihi
4. "Yayınla" butonuna tıklayın

#### 7.2. Etkinlik Oluşturma
1. "Etkinlikler" sekmesini seçin
2. "Yeni Etkinlik" butonuna tıklayın
3. Etkinlik bilgilerini girin:
   - Etkinlik adı
   - Tarih ve saat
   - Konum
   - Katılım kontenjanı
4. "Oluştur" butonuna tıklayın

### 8. Erişim Kontrolü

#### 8.1. Erişim Kartı Oluşturma
1. Ana menüden "Erişim Kontrolü"nü seçin
2. "Yeni Kart" butonuna tıklayın
3. Kart bilgilerini girin:
   - Kart sahibi
   - Kart tipi
   - Geçerlilik süresi
4. "Oluştur" butonuna tıklayın

#### 8.2. Erişim İzni Yönetimi
1. "Erişim İzinleri" sekmesini seçin
2. İzin vermek istediğiniz kartı seçin
3. İzin verilecek alanları seçin
4. "Kaydet" butonuna tıklayın

### 9. Raporlama

#### 9.1. Finansal Rapor Oluşturma
1. Ana menüden "Raporlar"ı seçin
2. "Finansal Rapor" seçeneğini tıklayın
3. Rapor parametrelerini belirleyin:
   - Tarih aralığı
   - Rapor tipi
   - Detay seviyesi
4. "Oluştur" butonuna tıklayın

#### 9.2. Rapor İhracatı
1. Oluşturulan raporu açın
2. "Dışa Aktar" butonuna tıklayın
3. İstediğiniz formatı seçin (PDF, Excel, CSV)
4. "İndir" butonuna tıklayın

### 10. Sorun Giderme

#### 10.1. Sık Karşılaşılan Sorunlar
1. **Giriş Yapılamıyor**
   - Şifrenizi kontrol edin
   - İnternet bağlantınızı kontrol edin
   - Tarayıcı önbelleğini temizleyin

2. **Sayfa Yüklenmiyor**
   - Sayfayı yenileyin
   - Farklı bir tarayıcı deneyin
   - İnternet bağlantınızı kontrol edin

3. **İşlem Yapılamıyor**
   - Yetkinizi kontrol edin
   - Gerekli alanların doldurulduğundan emin olun
   - Sistem yöneticisiyle iletişime geçin

#### 10.2. Teknik Destek
1. Destek ekibine ulaşın:
   - E-posta: support@rezidansyonetim.com
   - Telefon: +90 212 123 45 67
2. Sorununuzu detaylı açıklayın
3. Ekran görüntüsü veya hata mesajı ekleyin
4. Yanıt için bekleyin

### 11. Güvenlik Önerileri

#### 11.1. Hesap Güvenliği
1. Güçlü şifre kullanın
2. İki faktörlü doğrulamayı aktif edin
3. Düzenli olarak şifrenizi değiştirin
4. Şüpheli girişleri bildirin

#### 11.2. Veri Güvenliği
1. Hassas bilgileri paylaşmayın
2. Ortak bilgisayarlarda oturumu kapatın
3. Güvenli bağlantı kullanın
4. Antivirüs yazılımı kullanın

### 12. Mobil Uygulama Kullanımı

#### 12.1. Uygulama Kurulumu
1. App Store veya Google Play'den uygulamayı indirin
2. Uygulamayı açın ve giriş yapın
3. İzinleri onaylayın
4. Bildirimleri aktif edin

#### 12.2. Mobil Özellikler
1. Anlık bildirimler
2. Hızlı erişim menüsü
3. QR kod ile giriş
4. Acil durum butonu

### 13. Sistem Bakımı

#### 13.1. Düzenli Bakım
1. Veritabanı yedekleme
2. Sistem güncellemeleri
3. Performans optimizasyonu
4. Güvenlik taraması

#### 13.2. Acil Durum Prosedürleri
1. Sistem kesintisi durumunda yapılacaklar
2. Veri kaybı durumunda kurtarma
3. Güvenlik ihlali durumunda adımlar
4. İletişim zinciri 