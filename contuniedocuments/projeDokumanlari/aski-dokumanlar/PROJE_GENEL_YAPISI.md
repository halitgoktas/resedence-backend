# Rezidans ve Site Yönetim Sistemi Genel Yapısı

## 1. Proje Genel Bakış

Rezidans ve Site Yönetim Sistemi, rezidans, site ve apartman komplekslerinin tüm yönetim ihtiyaçlarını karşılamak üzere geliştirilmiş kapsamlı bir yazılım çözümüdür. Sistem multi-tenant yapıda, çoklu dil ve çoklu para birimi desteği ile tasarlanmıştır. Bu sistem daire, sakin ve kiracı yönetiminden finansal işlemlere, rezervasyonlardan bakım-onarım taleplerine kadar tüm süreçleri tek bir platformda yönetmeyi amaçlamaktadır.

### 1.1. Proje Amacı

- Rezidans ve site yönetimlerinin iş süreçlerini dijitalleştirmek
- Daire sahipleri, kiracılar ve yönetim arasındaki iletişimi kolaylaştırmak
- Finansal işlemleri ve aidat yönetimini otomatikleştirmek
- Ortak alan ve tesislerin rezervasyon süreçlerini düzenlemek
- Kimlik Bildirim Sistemi (KBS) gibi resmi yükümlülükleri kolaylaştırmak
- Bakım-onarım talepleri ve teknik servis yönetimini sağlamak
- Raporlama ve analizlerle yönetimsel kararları desteklemek
- Çoklu para birimi desteği ile farklı ödeme seçenekleri sunmak
- Çoklu dil desteği ile uluslararası kullanıma uygun olmak

## 2. Teknoloji Yapısı

### 2.1. Backend

- **.NET 8 Web API**: Core API yapısı
- **Entity Framework Core**: ORM (Object-Relational Mapping) teknolojisi
- **MSSQL**: Veritabanı
- **JWT**: Kimlik doğrulama ve yetkilendirme
- **Repository ve Unit of Work Pattern**: Veri erişim katmanı tasarımı
- **Service Layer Pattern**: İş mantığı katmanı
- **FluentValidation**: Veri doğrulama kütüphanesi
- **AutoMapper**: DTO ve Entity dönüşümleri için
- **Hosted Services**: Zamanlanmış görevler için (örn. otomatik kur çekme)
- **API Versiyonlama**: API sürüm yönetimi
- **İki Faktörlü Kimlik Doğrulama (2FA)**: Güvenlik için
- **HealthChecks**: Sistem sağlık kontrolü

### 2.2. Frontend

- **React.js**: UI kütüphanesi
- **Material UI**: UI bileşen kütüphanesi
- **React Router**: Sayfa yönlendirme
- **Context API**: State yönetimi
- **i18next**: Çoklu dil desteği (Türkçe, İngilizce, Rusça, Almanca, Farsça, Arapça)
- **Axios**: HTTP istekleri için
- **Formik ve Yup**: Form yönetimi ve doğrulama
- **React Query**: Sunucu state yönetimi
- **Chart.js**: Grafiksel raporlar

### 2.3. Mobil (Planlanan)

- **React Native**: Mobil uygulama geliştirme çerçevesi
- **React Navigation**: Ekran yönlendirmesi
- **Native Base**: UI bileşenleri
- **Push Notifications**: Mobil bildirimler
- **Secure Storage**: Güvenli veri depolama
- **Camera ve Barcode Scanner**: Kimlik ve belge taraması

## 3. Klasör Yapısı ve Mimari

### 3.1. Backend Klasör Yapısı

```
Backend/
│
├── src/
│   ├── ResidenceManagement.API/           # API katmanı
│   │   ├── Controllers/                  # API kontrolcüleri
│   │   ├── DTOs/                         # Data Transfer Objects
│   │   ├── Middlewares/                  # Özel middleware'ler
│   │   ├── Program.cs                    # Uygulama başlangıç noktası
│   │   └── appsettings.json              # Yapılandırma dosyası
│   │
│   ├── ResidenceManagement.Core/         # Çekirdek katman
│   │   ├── Entities/                     # Veritabanı varlıkları
│   │   ├── Interfaces/                   # Servis ve repository arayüzleri
│   │   ├── Services/                     # Servis implementasyonları
│   │   ├── DTOs/                         # Data Transfer Objects
│   │   ├── Validation/                   # Doğrulama kuralları
│   │   ├── Exceptions/                   # Özel exception sınıfları
│   │   └── Options/                      # Yapılandırma seçenekleri
│   │
│   ├── ResidenceManagement.Infrastructure/ # Altyapı katmanı
│   │   ├── Persistence/                  # Veritabanı işlemleri
│   │   ├── Repositories/                 # Repository implementasyonları
│   │   ├── Services/                     # Servis implementasyonları
│   │   ├── Migrations/                   # EF Core migrations
│   │   ├── Data/                         # Seed data ve veri yükleme
│   │   └── External/                     # Dış servis entegrasyonları
│   │
│   └── ResidenceManagement.Tests/         # Test projeleri
│       ├── UnitTests/                    # Birim testleri
│       ├── IntegrationTests/             # Entegrasyon testleri
│       └── TestHelpers/                  # Test yardımcıları
│
└── tools/                                # Yardımcı araçlar
```

### 3.2. Frontend Klasör Yapısı

```
Frontend/
│
├── public/                              # Statik dosyalar
│   ├── assets/                         # Görseller, fontlar vb.
│   └── locales/                        # Dil dosyaları
│
├── src/
│   ├── components/                     # Paylaşılan bileşenler
│   │   ├── common/                    # Genel bileşenler
│   │   ├── forms/                     # Form bileşenleri
│   │   ├── layout/                    # Sayfa düzeni bileşenleri
│   │   └── widgets/                   # Dashboard widget'ları
│   │
│   ├── pages/                          # Uygulama sayfaları
│   │   ├── dashboard/                 # Dashboard sayfaları
│   │   ├── apartments/                # Daire yönetimi
│   │   ├── residents/                 # Sakin yönetimi
│   │   ├── financial/                 # Finansal işlemler
│   │   ├── reservations/              # Rezervasyon yönetimi
│   │   ├── maintenance/               # Bakım-onarım işlemleri
│   │   ├── kbs/                       # KBS entegrasyonu
│   │   └── settings/                  # Ayarlar
│   │
│   ├── contexts/                       # Context API
│   ├── hooks/                          # Custom hooks
│   ├── services/                       # API servisleri
│   ├── utils/                          # Yardımcı fonksiyonlar
│   ├── i18n/                           # Çoklu dil yapılandırması
│   ├── styles/                         # Stil dosyaları
│   ├── config/                         # Yapılandırma
│   ├── App.js                          # Ana uygulama bileşeni
│   └── index.js                        # Uygulama giriş noktası
│
├── package.json                        # Bağımlılıklar
└── README.md                           # Dokümantasyon
```

### 3.3. Mobil Klasör Yapısı (Planlanan)

```
Mobile/
│
├── src/
│   ├── components/                     # Paylaşılan bileşenler
│   ├── screens/                        # Uygulama ekranları
│   ├── navigation/                     # Gezinme yapılandırması
│   ├── services/                       # API servisleri
│   ├── hooks/                          # Custom hooks
│   ├── utils/                          # Yardımcı fonksiyonlar
│   ├── i18n/                           # Çoklu dil yapılandırması
│   ├── store/                          # State yönetimi
│   └── App.js                          # Ana uygulama bileşeni
│
├── assets/                             # Görseller, fontlar vb.
├── android/                            # Android platformu dosyaları
├── ios/                                # iOS platformu dosyaları
├── package.json                        # Bağımlılıklar
└── README.md                           # Dokümantasyon
```

## 4. Veri Tabanı Yapısı

Sistem Code First yaklaşımı ile Entity Framework Core üzerinden oluşturulmuştur. Temel veritabanı yapısı:

### 4.1. Temel Tablolar

- **Firma**: Site yönetim firmaları bilgilerini tutar
- **Sube**: Firma şubelerinin bilgilerini tutar 
- **Kullanici**: Sistem kullanıcılarının bilgilerini tutar
- **Rol**: Kullanıcı rollerini ve yetkilerini tanımlar
- **KullaniciRol**: Kullanıcı-rol ilişkisini tanımlar
- **Site**: Site/rezidans bilgilerini tutar
- **Blok**: Site içindeki bina/blokları tanımlar
- **Daire**: Daire bilgilerini tutar
- **Sakin**: Daire sakinlerinin bilgilerini tutar
- **Malik**: Mülk sahiplerinin bilgilerini tutar
- **Kiraci**: Kiracı bilgilerini tutar

### 4.2. Finansal Tablolar

- **Aidat**: Aidat tanımlarını ve kayıtlarını tutar
- **Odeme**: Ödeme işlemlerini kaydeder
- **Tahsilat**: Aidat ve ödeme tahsilatlarını tutar
- **GelirGider**: Gelir ve gider kayıtlarını tutar
- **ParaBirimi**: Para birimlerini tanımlar
- **DovizKuru**: Döviz kurlarını tutar
- **BankaHesabi**: Banka hesap bilgilerini tutar
- **OdemeYontemi**: Ödeme yöntemlerini tanımlar
- **Fatura**: Faturaları kaydeder

### 4.3. Rezervasyon Tabloları

- **AktiviteAlani**: Ortak alanları tanımlar
- **Rezervasyon**: Rezervasyon kayıtlarını tutar
- **RezerveZamani**: Rezervasyon zaman aralıklarını tutar
- **Hizmet**: Sunulan hizmetleri tanımlar
- **HizmetTarifeleri**: Hizmet fiyatlandırmalarını tutar

### 4.4. Bakım-Onarım Tabloları

- **BakimTalebi**: Bakım-onarım taleplerini kaydeder
- **BakimDurumu**: Bakım-onarım durumlarını tanımlar
- **TeknikPersonel**: Teknik personel bilgilerini tutar
- **Demirbas**: Demirbaş kayıtlarını tutar

### 4.5. Entegrasyon Tabloları

- **KbsKayit**: KBS bildirimleri için kayıtları tutar
- **RfidKart**: RFID kart bilgilerini tutar
- **PlakaKayit**: Plaka kayıtlarını tutar
- **OdemeEntegrasyonu**: Ödeme sistemi entegrasyonlarını yapılandırır
- **Bildirim**: Sistem bildirimlerini tutar

### 4.6. Multi-tenant Yaklaşımı

Tüm tablolarda:
- **FirmaID**: Hangi firmaya ait olduğunu belirtir
- **SubeID**: Hangi şubeye ait olduğunu belirtir

## 5. Tamamlanan İşler

### 5.1. Backend Geliştirmeleri

- [x] Temel altyapı ve veritabanı yapısı
- [x] Migration'lar ve Seed Data
- [x] JWT tabanlı kimlik doğrulama
- [x] İki faktörlü kimlik doğrulama (2FA)
- [x] API versiyonlama
- [x] Çoklu döviz desteği (CurrencyController)
- [x] TCMB'den otomatik kur çekme
- [x] Temel entity yapılarının oluşturulması
- [x] Repository ve Unit of Work pattern implementasyonu
- [x] FluentValidation entegrasyonu
- [x] TcIdentityValidator ve PhoneValidator
- [x] Firma, Şube, Daire, Sakin, Aidat API'leri
- [x] IPaymentIntegrationService arayüzü ve temel implementasyonu
- [x] Ödeme sistemleri entegrasyonu altyapısı (Iyzico, PayTR, PayU)
- [x] KBS entegrasyonu altyapısı
- [x] Rezervasyon sistemi API'leri
- [x] Excel/PDF çıktı oluşturma
- [x] Dashboard veri API'leri
- [x] Unit ve Integration testlerin başlangıcı

### 5.2. Frontend Geliştirmeleri

- [x] React.js projesi oluşturma
- [x] Material UI entegrasyonu
- [x] Proje klasör yapısının düzenlenmesi
- [x] Routing yapılandırması
- [x] Context API / State yönetimi yapılandırması
- [x] Çoklu dil desteği yapılandırması (i18n)
- [x] Türkçe, İngilizce, Rusça, Almanca, Farsça, Arapça dil dosyaları
- [x] Form bileşenleri (input, select, checkbox vb.)
- [x] Giriş sayfası
- [x] Şifremi unuttum sayfası
- [x] Profil sayfası

## 6. Yapılacak İşler

### 6.1. Backend Yapılacaklar

- [ ] End-to-end testlerin tamamlanması
- [ ] Benchmark testleri
- [ ] Güvenlik testleri
- [ ] Validasyon testleri (FluentValidation)
- [ ] Sınır değerleri ve kenar durumları testleri
- [ ] Sorgu optimizasyonu
- [ ] Veritabanı indeksleme
- [ ] Cache implementasyonu
- [ ] Asenkron operasyonların iyileştirilmesi
- [ ] Mikroservis mimarisine geçiş araştırması
- [ ] Validasyon kontrolleri iyileştirmesi
- [ ] Tüm işlemlerin detaylı loglanması
- [ ] Veritabanı ilişki optimizasyonu
- [ ] N+1 sorgu probleminin çözümü
- [ ] Büyük veri setleri için sayfalama optimizasyonu
- [ ] Filtre ve sıralama için LINQ optimizasyonu
- [ ] API dokümantasyonunun tamamlanması
- [ ] Kod dokümantasyonu
- [ ] Veritabanı diyagramı
- [ ] Geliştirici kılavuzu

### 6.2. Frontend Yapılacaklar

- [ ] Responsive tasarım uygulaması
- [ ] Kullanıcı dil tercihi saklama ve otomatik uyarlama sistemi
- [ ] Tablo bileşeni (filtreleme, sıralama, Excel/PDF çıktı)
- [ ] Detay görünümü bileşeni
- [ ] Dashboard widget'ları
- [ ] Bildirim ve uyarı bileşenleri
- [ ] Gelişmiş dil değiştirici bileşeni
- [ ] Para birimi seçici ve dönüştürücü bileşeni
- [ ] İleri-geri butonları ve tarih aralığı seçici
- [ ] Grafiksel görselleştirme bileşenleri
- [ ] Firma ve şube tanımları sayfaları
- [ ] Kullanıcı ve rol yönetimi sayfaları
- [ ] Kullanıcı dil ve para birimi tercihleri sayfası
- [ ] Daire ve sakin yönetimi sayfaları
- [ ] Finansal sayfalar
- [ ] Rezervasyon ve hizmet sayfaları
- [ ] Entegrasyon sayfaları
- [ ] Dashboard ve raporlama sayfaları
- [ ] Frontend testleri

### 6.3. Mobil Yapılacaklar

- [ ] React Native proje kurulumu
- [ ] Backend API entegrasyonu
- [ ] Temel ekranların geliştirilmesi
- [ ] Push notification entegrasyonu
- [ ] Mobil-spesifik özellikler
- [ ] Çoklu dil ve para birimi desteği
- [ ] Rezervasyon takibi ve bildirimler
- [ ] Aidat ödemeleri ve borç görüntüleme
- [ ] Aktivite alanları rezervasyonu
- [ ] Teknik servis talep oluşturma
- [ ] Sakin/kiracı profil yönetimi
- [ ] Giriş-çıkış bildirimleri

## 7. Geliştirme Süreçleri

Proje geliştirmesi aşağıdaki yaşam döngüsüne göre planlanmıştır:

### 7.1. Faz 1: Temel Altyapı ve Backend (Tamamlandı)
- Migration'ların ve Seed Data'nın oluşturulması
- İki faktörlü kimlik doğrulama ve API versiyonlama

### 7.2. Faz 2: Backend Tamamlama (Kısmen Tamamlandı)
- Çoklu döviz ve dil desteği iyileştirmeleri
- KBS ve diğer entegrasyonlar
- Eksik backend testlerinin geliştirilmesi
- Backend optimizasyonları
- Model validasyon ve doğrulama mekanizmaları

### 7.3. Faz 3: Frontend Geliştirme (Kısmen Başlandı)
- Temel frontend yapısının tamamlanması
- Ortak bileşenlerin geliştirilmesi
- Responsive tasarımın tamamlanması
- Temel sayfaların geliştirilmesi (Kimlik doğrulama, daire ve sakin yönetimi)
- Finansal ve rezervasyon sayfalarının geliştirilmesi
- Dashboard ve raporlama ekranlarının geliştirilmesi
- Frontend testlerinin yazılması

### 7.4. Faz 4: Test, Optimizasyon ve Canlıya Alma (Beklemede)
- Entegrasyon ve uçtan uca testler
- Performans optimizasyonları
- Güvenlik denetimi
- Dokümantasyon ve eğitim materyallerinin hazırlanması
- CI/CD pipeline kurulumu
- Canlı ortam hazırlığı ve pilot kullanım
- Tam geçiş

### 7.5. Faz 5: Mobil Uygulama ve İleriki Geliştirmeler (Beklemede)
- Mobil uygulama geliştirme
- İleri düzey analitik özellikleri
- Üçüncü parti entegrasyonların genişletilmesi
- Mikroservis mimarisine geçiş
- Yapay zeka ve makine öğrenmesi entegrasyonları

## 8. Projenin Faydaları

### 8.1. Site Yönetimi İçin Faydalar

- Aidat takibini otomatikleştirme ve tahsilat sürecini kolaylaştırma
- Gelir-gider takibini ve finansal yönetimi iyileştirme
- Daire, sakin ve kiracı bilgilerini merkezi bir sistemde yönetme
- KBS gibi yasal yükümlülükleri kolay ve hatasız yerine getirme
- Bakım-onarım taleplerini etkin şekilde yönetme
- Ortak alanların rezervasyon süreçlerini düzenleme
- Çoklu para birimi desteği ile farklı ödeme seçenekleri sunma
- Raporlar ve analizlerle veri odaklı karar alma
- Sakinler ve yönetim arasında etkin iletişim

### 8.2. Daire Sahipleri ve Kiracılar İçin Faydalar

- Borç/alacak durumunu anlık görüntüleme
- Aidat ve diğer ödemeleri çevrimiçi yapabilme
- Bakım-onarım taleplerini kolay oluşturma ve takip etme
- Ortak alan rezervasyonlarını çevrimiçi yapabilme
- Duyuru ve bildirimlere anında erişim
- Giriş-çıkış bildirimleri ve güvenlik bilgileri
- Çoklu dil desteği ile kendi dillerinde kullanabilme
- Mobil uygulama ile her an her yerden erişim

### 8.3. Teknik Faydalar

- Multi-tenant yapı ile aynı altyapıda birden çok firma ve şube yönetimi
- Çoklu dil desteği ile uluslararası kullanıma uygunluk
- Çoklu para birimi desteği ile her türlü ödemeleri yönetebilme
- Modüler yapı ile eklentiler ve özelleştirmeler kolaylığı
- API-first yaklaşımı ile üçüncü taraf entegrasyonlara uygunluk
- Responsive tasarım ile tüm cihazlarda kullanılabilme
- Yüksek güvenlik standartları (2FA, JWT, şifreleme)
- Ölçeklenebilir mimari ile büyüyen ihtiyaçlara adaptasyon

## 9. Sonuç

Rezidans ve Site Yönetim Sistemi projesi, modern site ve rezidans yönetimlerinin tüm ihtiyaçlarını karşılayan, teknik olarak güncel ve güçlü bir altyapıya sahip, çoklu dil ve para birimi desteği ile uluslararası kullanıma uygun, hem web hem de mobil erişim sağlayan kapsamlı bir çözümdür. 

Projenin temel amacı, site yönetimlerinin günlük operasyonlarını dijitalleştirmek, finansal süreçleri otomatikleştirmek, sakinler ve yönetim arasındaki iletişimi güçlendirmek ve yasal yükümlülükleri kolaylaştırmaktır. Aynı zamanda, kullanıcı dostu arayüzü, çoklu dil desteği ve esnek ödeme seçenekleri ile herkesin kolayca kullanabileceği bir platform sunmaktır.

Proje, planlanan fazlar halinde geliştirilerek, her aşamada test edilerek ve sürekli iyileştirilerek ilerlemektedir. Mevcut durumda backend ve temel frontend yapısı büyük ölçüde tamamlanmış olup, ilerleyen aşamalarda frontend'in tüm modülleri, mobil uygulama ve ileri analitik özellikleri de eklenecektir. 


# Rezidans ve Site Yönetim Sistemi Frontend Tasarım Dokümantasyonu

Bu doküman, Rezidans ve Site Yönetim Sistemi'nin frontend yapısını detaylı bir şekilde açıklar. Backend model yapısına dayanarak oluşturulan tüm ekranlar, formlar, menüler ve bileşenler burada belirtilmiştir.

## İçindekiler

1. [Genel Mimari](#genel-mimari)
2. [Ana Modüller ve Ekranlar](#ana-modüller-ve-ekranlar)
3. [Model-Ekran İlişkileri](#model-ekran-ilişkileri)
4. [Ortak Bileşenler](#ortak-bileşenler)
5. [Veri Akışı](#veri-akışı)
6. [Form Yapıları](#form-yapıları)
7. [Menü Sistemi](#menü-sistemi)
8. [Çoklu Dil Desteği](#çoklu-dil-desteği)
9. [Tema ve Stil](#tema-ve-stil)
10. [Yetkilendirme ve Erişim Kontrolü](#yetkilendirme-ve-erişim-kontrolü)

## Genel Mimari

Frontend, React.js ve Material UI kullanılarak geliştirilmiş bir single-page application'dır. Temel mimari bileşenleri şunlardır:

- **React**: UI bileşenlerinin geliştirilmesi için kullanılan JavaScript kütüphanesi
- **Material UI**: Kullanıcı arayüzü tasarımı için kullanılan React bileşen kütüphanesi
- **React Router**: Sayfa yönlendirmeleri için
- **Context API**: Durum yönetimi için
- **i18next**: Çoklu dil desteği için
- **Axios**: API istekleri için

Mimari yapı, aşağıdaki katmanlardan oluşur:

1. **Görünüm Katmanı**: Kullanıcı arayüzü bileşenleri (pages, components)
2. **Durum Yönetim Katmanı**: Context API ile global state yönetimi
3. **Servis Katmanı**: API istekleri ve veri manipülasyonu
4. **Yardımcı Katman**: Yardımcı fonksiyonlar, özel hooks, utilities

## Ana Modüller ve Ekranlar

### 1. Kimlik Doğrulama ve Kullanıcı Yönetimi

#### Ekranlar:
- **Giriş (Login)**: Kullanıcı adı/şifre ile giriş
- **Şifremi Unuttum**: Şifre sıfırlama
- **İki Faktörlü Doğrulama**: Güvenlik için ikinci faktör doğrulama
- **Profil**: Kullanıcı profil bilgileri ve ayarları
- **Kullanıcı Yönetimi**: Admin için kullanıcı listeleme, ekleme, düzenleme, silme
- **Rol Yönetimi**: Kullanıcı rollerini tanımlama ve yetkilendirme

### 2. Ana Sayfa ve Dashboard

#### Ekranlar:
- **Dashboard**: Özet bilgiler, istatistikler ve hızlı erişim
- **İstatistik Kartları**: Toplam daire, dolu daire, boş daire, toplam sakin, bekleyen ödemeler
- **Aktivite Özeti**: Son aktiviteler ve bildirimler
- **Hızlı Erişim Menüsü**: Sık kullanılan işlevlere hızlı erişim

### 3. Site ve Bina Yönetimi

#### Ekranlar:
- **Site Listesi**: Tüm sitelerin listesi, arama ve filtreleme
- **Site Detay**: Site bilgileri, binalar, daireler
- **Site Ekle/Düzenle Formu**: Site bilgilerini güncelleme
- **Bina Listesi**: Sitedeki binaların listesi
- **Bina Detay**: Bina bilgileri ve daireler
- **Bina Ekle/Düzenle Formu**: Bina bilgilerini güncelleme

### 4. Daire Yönetimi

#### Ekranlar:
- **Daire Listesi**: Tüm dairelerin listesi, arama ve filtreleme
- **Daire Haritası**: Site/binalardaki dairelerin görsel haritası ve durumları
- **Daire Detay**: Daire bilgileri, sakinler, ödemeler, bakım talepleri
- **Daire Ekle/Düzenle Formu**: Daire bilgilerini güncelleme
- **Daire Durumu Değiştirme**: Daire durumunu güncelleme (boş, dolu, bakımda)

### 5. Sakin Yönetimi

#### Ekranlar:
- **Sakin Listesi**: Tüm sakinlerin listesi (ev sahipleri, kiracılar)
- **Sakin Tipi Sekmeler**: Ev sahipleri, kiracılar, misafirler vb.
- **Sakin Detay**: Sakin bilgileri, ilişkili daire, ödemeler, talepler
- **Sakin Ekle/Düzenle Formu**: Sakin bilgilerini güncelleme
- **Daire Atama**: Sakine daire atama/değiştirme
- **Sakin Belgeleri**: Kimlik, sözleşme vb. belge yönetimi

### 6. Aidat ve Ödeme Yönetimi

#### Ekranlar:
- **Aidat Listesi**: Tüm aidatların listesi, filtreleme (tarih, durum)
- **Aidat Oluşturma**: Toplu veya tekil aidat oluşturma
- **Ödeme Listesi**: Tüm ödemelerin listesi
- **Ödeme Detay**: Ödeme detayları ve ilişkili fatura/aidat
- **Ödeme Alma Formu**: Yeni ödeme kaydetme
- **Ödeme Entegrasyonları**: Çevrimiçi ödeme seçenekleri
- **Aidat ve Ödeme Raporları**: Tahsilat durum raporları

### 7. Şikayet ve Bakım Yönetimi

#### Ekranlar:
- **Şikayet Listesi**: Tüm şikayetlerin listesi ve durumları
- **Şikayet Detay**: Şikayet detayları, atama, yorumlar, çözüm
- **Şikayet Oluşturma Formu**: Yeni şikayet kaydı
- **Bakım Talepleri Listesi**: Tüm bakım taleplerinin listesi
- **Bakım Talebi Detay**: Talep detayları, atama, ilerleme
- **Bakım Görevleri Atama**: Teknisyen/personel atama
- **Bakım Takvimi**: Planlanan bakım işleri takvimi

### 8. Finans ve Raporlama

#### Ekranlar:
- **Gelir/Gider Özeti**: Finansal durum özeti
- **Bütçe Yönetimi**: Bütçe planlama ve takip
- **Gider Yönetimi**: Siteye ait giderler ve ödemeler
- **Rapor Oluşturma**: Özelleştirilebilir rapor oluşturma
- **Rapor Listesi**: Kaydedilmiş raporlar
- **Mali Raporlar**: Gelir-gider raporları, aidat tahsilat oranları

### 9. Rezervasyon ve Ortak Alan Yönetimi

#### Ekranlar:
- **Ortak Alan Listesi**: Tüm ortak alanların listesi (havuz, spor salonu vb.)
- **Ortak Alan Detay**: Alan bilgileri, kapasite, kurallar
- **Rezervasyon Takvimi**: Ortak alan rezervasyonlarının takvim görünümü
- **Rezervasyon Listesi**: Tüm rezervasyonların listesi
- **Rezervasyon Oluşturma**: Yeni rezervasyon kaydı
- **Rezervasyon Onaylama/İptal**: Yönetim onay işlemleri

### 10. Sistem Ayarları

#### Ekranlar:
- **Genel Ayarlar**: Sistem genelindeki ayarlar
- **Kullanıcı Ayarları**: Kullanıcı bazlı ayarlar
- **Site Ayarları**: Site bazlı özel ayarlar
- **Bildirim Ayarları**: Bildirim tercihleri ve kuralları
- **Dil ve Bölge Ayarları**: Çoklu dil ve bölge ayarları
- **Yedekleme ve Geri Yükleme**: Veri yedekleme işlemleri

## Model-Ekran İlişkileri

### Site Modeli -> Site Yönetimi Ekranları
- `Site` modeli, Site Listesi ve Site Detay ekranlarını besler
- `Building` modeli, binalar bölümünde kullanılır
- `Complex` modeli, site kompleksi bilgilerini gösterir

### Daire ve Sakin Modelleri -> Daire ve Sakin Yönetimi
- `Apartment` modeli, Daire Listesi ve Detay ekranlarını besler
- `Resident` modeli, Sakin Listesi ve Detay ekranlarında kullanılır
- `Block` modeli, Daire Haritası ekranında blok yapısını gösterir

### Ödeme ve Finans Modelleri -> Finansal Ekranlar
- `Payment` modeli, Ödeme Listesi ve Detay ekranlarında kullanılır
- `DuesReconciliation` ve ilişkili modeller, Aidat ekranlarını besler
- `SiteFinancialReport` modeli, raporlama ekranlarında kullanılır

### Bakım ve Şikayet Modelleri -> Bakım/Şikayet Ekranları
- `Maintenance` modeli, Bakım Talepleri ekranlarını besler
- `Complaint` modeli, Şikayet Yönetimi ekranlarını besler
- `TechnicalService` modeli, teknik servis ekranlarında kullanılır

### Rezervasyon Modelleri -> Rezervasyon Ekranları
- `Reservation` modeli, rezervasyon ekranlarını besler
- `ActivityArea` modeli, Ortak Alan Yönetimi ekranlarında kullanılır
- `FacilityReservation` modeli, tesis rezervasyonlarını gösterir

### Kullanıcı ve Yetkilendirme Modelleri -> Kullanıcı Yönetimi
- `User` modeli, kullanıcı yönetimi ekranlarını besler
- `Role` ve `UserRole` modelleri, rol ve yetkilendirme ekranlarında kullanılır
- `CompanyManager`, `SiteManager` modelleri, yönetici ekranlarını besler

## Ortak Bileşenler

Uygulama genelinde kullanılan ortak bileşenler:

1. **Navbar**: Üst navigasyon çubuğu, kullanıcı bilgileri ve hızlı erişim
2. **Sidebar**: Ana menü ve modüllere erişim
3. **Footer**: Telif hakkı, sürüm bilgisi
4. **DataTable**: Veri listeleme, filtreleme, sıralama, sayfalama özellikli tablo
5. **FormBuilder**: Form oluşturma ve doğrulama
6. **ModalDialog**: Modal pencereler
7. **NotificationBadge**: Bildirim gösterim bileşeni
8. **FilterPanel**: Arama ve filtreleme paneli
9. **StatusBadge**: Durum göstergeleri (aktif, pasif, beklemede vb.)
10. **ActionButtons**: İşlem butonları grubu (düzenle, sil, görüntüle)
11. **FileUploader**: Dosya yükleme bileşeni
12. **DatePicker**: Tarih seçici
13. **CustomSelects**: Özelleştirilmiş seçim kutuları
14. **LoadingIndicator**: Yükleme göstergesi
15. **ErrorBoundary**: Hata sınırlayıcı bileşen

## Veri Akışı

1. **API İstekleri**:
   - Services katmanında tanımlanan servisler aracılığıyla API'ye istek yapılır
   - Axios interceptor'lar ile istekler ve yanıtlar özelleştirilir
   - Token yönetimi ve yenileme otomatik olarak yapılır

2. **Durum Yönetimi**:
   - Context API kullanılarak global state yönetilir
   - Modül bazlı context'ler (UserContext, SiteContext, ApartmentContext vb.)
   - Reducers ile state güncellemeleri standardize edilir

3. **Form Yönetimi**:
   - Formik ile form state yönetimi
   - Yup şemaları ile form doğrulama
   - FormBuilder ile standart form yapısı

4. **Error Handling**:
   - Global error boundary ile beklenmeyen hatalar yakalanır
   - API hataları için standart hata yönetimi
   - Kullanıcı dostu hata mesajları

## Form Yapıları

### Ortak Form Özellikleri
- Zorunlu alan işaretleme
- Anlık form doğrulama
- Otomatik tamamlama
- İptal ve kaydet butonları
- İşlem sırasında loading göstergesi

### Ana Formlar
1. **Site Ekleme/Düzenleme Formu**
   - Temel site bilgileri (ad, adres, iletişim)
   - Site tipi ve özellikleri
   - Site görselleri

2. **Bina Ekleme/Düzenleme Formu**
   - Bina bilgileri (ad, blok, kat sayısı)
   - Bina tipi ve özellikleri
   - Bina-site ilişkisi

3. **Daire Ekleme/Düzenleme Formu**
   - Daire bilgileri (no, kat, oda sayısı, alan)
   - Daire tipi ve özellikleri
   - Aidat bilgileri

4. **Sakin Ekleme/Düzenleme Formu**
   - Kişisel bilgiler (ad, soyad, kimlik no, iletişim)
   - İkamet bilgileri (taşınma tarihi, ikamet tipi)
   - Araç ve evcil hayvan bilgileri
   - Daire atama

5. **Ödeme Kayıt Formu**
   - Ödeme bilgileri (tutar, tarih, ödeme yöntemi)
   - İlişkili daire ve sakin seçimi
   - Ödeme tipi (aidat, fatura, ceza vb.)

6. **Şikayet/Bakım Talebi Formu**
   - Talep bilgileri (başlık, açıklama, öncelik)
   - İlgili daire ve sakin seçimi
   - Dosya ekleme

7. **Rezervasyon Formu**
   - Rezervasyon bilgileri (tarih, saat, süre)
   - Rezerve edilecek alan seçimi
   - Katılımcı bilgileri

## Menü Sistemi

### Ana Menü
1. **Dashboard**
2. **Site Yönetimi**
   - Siteler
   - Binalar
   - Bloklar
3. **Daire Yönetimi**
   - Daire Listesi
   - Daire Haritası
   - Daire Tipleri
4. **Sakin Yönetimi**
   - Sakinler
   - Mal Sahipleri
   - Kiracılar
5. **Finans Yönetimi**
   - Aidat Yönetimi
   - Ödeme İşlemleri
   - Gider Yönetimi
   - Bütçe Planlama
   - Raporlar
6. **Talep Yönetimi**
   - Şikayetler
   - Bakım Talepleri
   - Teknik Servis
7. **Rezervasyon**
   - Ortak Alan Rezervasyonları
   - Rezervasyon Takvimi
   - Tesis Yönetimi
8. **Kullanıcı Yönetimi**
   - Kullanıcılar
   - Roller ve Yetkiler
   - Erişim Logları
9. **Ayarlar**
   - Sistem Ayarları
   - Kullanıcı Ayarları
   - Bildirim Ayarları
   - Yedekleme

### Kullanıcı Menüsü (Dropdown)
1. **Profil**
2. **Hesap Ayarları**
3. **Bildirimler**
4. **Dil Değiştir**
5. **Çıkış**

## Çoklu Dil Desteği

İnternasyonalizasyon için i18next kütüphanesi kullanılır:

- Desteklenen diller: Türkçe, İngilizce, Arapça, Rusça, Almanca, Farsça
- Dil dosyaları: src/locales/ altında JSON formatında
- Dil değiştirme: Navbar'da dil seçim dropdown'ı
- Format kuralları: Tarih, saat, para birimi formatları dile göre otomatik ayarlanır
- RTL desteği: Arapça ve Farsça için RTL (sağdan sola) düzen desteği

## Tema ve Stil

Material UI tema sistemi kullanılarak:

- **Açık/Koyu Tema**: Kullanıcı tercihine göre tema değiştirme
- **Birincil Renk**: #1976d2 (mavi)
- **İkincil Renk**: #f50057 (pembe)
- **Tipografi**: Roboto font ailesi
- **Özelleştirilmiş Bileşenler**: Material UI bileşenlerinin özelleştirilmiş versiyonları
- **Responsive Tasarım**: Tüm ekran boyutlarına uyumlu (mobil, tablet, masaüstü)

## Yetkilendirme ve Erişim Kontrolü

- **Rol Tabanlı Erişim**: Kullanıcı rollerine göre erişim kontrolü
- **Yetki Seviyeleri**: Görüntüleme, Düzenleme, Silme, Onaylama yetkileri
- **Özel Yetkiler**: Belirli işlemler için özel yetkiler
- **Erişim Kısıtlama**: Yetkisiz ekranlara erişimlerin engellenmesi
- **Koşullu UI**: Yetkilere göre UI elemanlarının gösterilmesi/gizlenmesi 





# Rezidans ve Site Yönetim Sistemi Frontend Tasarım Dokümantasyonu - Bölüm 3

Bu doküman, Rezidans ve Site Yönetim Sistemi'nin frontend tasarımının son bölümünü detaylandırmaktadır.

## Modül Bazlı Ekran ve Form Detayları (Devam)

### 8. Finans ve Raporlama

#### 8.1. Gelir/Gider Özeti Ekranı
- **Kullanılan Modeller**: `Payment`, `Expense`, `SiteFinancialReport`
- **Bileşenler**:
  - Özet kart (toplam gelir, toplam gider, bakiye)
  - Tarih aralığı seçici
  - Gelir/gider dengesi grafikleri
  - Kategori bazlı harcama dağılımı pasta grafiği
  - Gelir kaynakları dağılımı pasta grafiği
  - Aylık trend çizgi grafiği
- **API Endpoint**: `GET /api/financial/summary`, `GET /api/financial/statistics`

#### 8.2. Bütçe Yönetimi Ekranı
- **Kullanılan Modeller**: `Budget`, `BudgetCategory`, `BudgetItem`
- **Bileşenler**:
  - Yıllık/aylık bütçe planlama tablosu
  - Kategori bazlı bütçe planı
  - Gerçekleşen vs. planlanan karşılaştırma grafikleri
  - Bütçe düzenleme formu
  - Bütçe gerçekleşme oranları
- **API Endpoint**: `GET /api/budgets`, `POST /api/budgets`, `PUT /api/budgets/{id}`

#### 8.3. Gider Yönetimi Ekranı
- **Kullanılan Modeller**: `Expense`, `ExpenseCategory`
- **Bileşenler**:
  - Gider listesi tablosu (tarih, kategori, açıklama, tutar)
  - Kategori filtreleme
  - Tarih aralığı filtreleme
  - Gider ekleme/düzenleme butonları
  - Sayfalama
  - Fatura görüntüleme modalı
- **API Endpoint**: `GET /api/expenses`, `POST /api/expenses`, `PUT /api/expenses/{id}`

#### 8.4. Gider Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `Expense`, `ExpenseCategory`
- **Form Alanları**:
  - Gider başlığı
  - Gider kategorisi
  - Tutar
  - Para birimi
  - Tarih
  - Ödeme yöntemi
  - Açıklama
  - Fatura/makbuz yükleme alanı
  - Bütçe kategorisi ilişkilendirme
- **API Endpoint**: `POST /api/expenses`, `PUT /api/expenses/{id}`

#### 8.5. Rapor Oluşturma Ekranı
- **Kullanılan Modeller**: `SiteFinancialReport`, `Report`, `ReportTemplate`
- **Bileşenler**:
  - Rapor tipi seçim kartları (finansal, aidat, doluluk, bakım vb.)
  - Tarih aralığı seçici
  - Parametre formu (rapor tipine göre dinamik)
  - Rapor formatı seçimi (PDF, Excel, CSV)
  - Rapor önizleme
  - Kaydet ve indir butonları
- **API Endpoint**: `POST /api/reports/generate`, `GET /api/report-templates`

#### 8.6. Rapor Listesi Ekranı
- **Kullanılan Modeller**: `Report`
- **Bileşenler**:
  - Rapor listesi tablosu (oluşturma tarihi, rapor adı, rapor tipi, oluşturan)
  - Rapor tipi filtreleme
  - Tarih aralığı filtreleme
  - Rapor görüntüleme butonu
  - Raporları indirme butonu
  - Sayfalama
- **API Endpoint**: `GET /api/reports`, `GET /api/reports/{id}`

### 9. Rezervasyon ve Ortak Alan Yönetimi

#### 9.1. Ortak Alan Listesi Ekranı
- **Kullanılan Modeller**: `ActivityArea`, `Facility`
- **Bileşenler**:
  - Ortak alan listesi tablosu (alan adı, tipi, kapasite, durum)
  - Alan tipi filtreleme
  - Durum filtreleme
  - Alan ekleme/düzenleme/silme butonları
  - Sayfalama
  - Alan detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/activity-areas`, `POST /api/activity-areas`, `PUT /api/activity-areas/{id}`

#### 9.2. Ortak Alan Detay Ekranı
- **Kullanılan Modeller**: `ActivityArea`, `Facility`, `Reservation`
- **Bileşenler**:
  - Alan bilgileri kartı
  - Fotoğraf galerisi
  - Kullanım kuralları listesi
  - Ekipman ve olanaklar listesi
  - Müsaitlik durumu
  - Yaklaşan rezervasyonlar listesi
  - Rezervasyon butonu
- **API Endpoint**: `GET /api/activity-areas/{id}`, `GET /api/activity-areas/{id}/reservations`

#### 9.3. Ortak Alan Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `ActivityArea`, `Facility`
- **Form Alanları**:
  - Alan adı
  - Alan tipi (havuz, spor salonu, toplantı odası vb.)
  - Kapasite
  - Çalışma saatleri
  - Ücret bilgisi (ücretli ise)
  - Kullanım kuralları
  - Ekipman ve olanaklar
  - Fotoğraf yükleme alanı
  - Durum (aktif/bakımda/kapalı)
  - Rezervasyon ayarları (onay gerektiren, min/max süre vb.)
- **API Endpoint**: `POST /api/activity-areas`, `PUT /api/activity-areas/{id}`

#### 9.4. Rezervasyon Takvimi Ekranı
- **Kullanılan Modeller**: `Reservation`, `ActivityArea`
- **Bileşenler**:
  - Alan seçim dropdown'ı
  - Aylık/haftalık/günlük takvim görünümü
  - Rezervasyonları renk kodlama
  - Hızlı rezervasyon ekleme
  - Rezervasyon detayı görüntüleme
  - Bugüne git butonu
- **API Endpoint**: `GET /api/reservations/calendar`, `GET /api/activity-areas/{id}/availability`

#### 9.5. Rezervasyon Listesi Ekranı
- **Kullanılan Modeller**: `Reservation`
- **Bileşenler**:
  - Rezervasyon listesi tablosu (tarih, saat, alan, sakin, durum)
  - Alan tipi filtreleme
  - Durum filtreleme (bekliyor, onaylandı, tamamlandı, iptal)
  - Tarih aralığı filtreleme
  - Rezervasyon ekleme butonu
  - Sayfalama
  - Rezervasyon detay modalı
- **API Endpoint**: `GET /api/reservations`

#### 9.6. Rezervasyon Oluşturma Formu
- **Kullanılan Modeller**: `Reservation`, `ActivityArea`, `Resident`
- **Form Alanları**:
  - Alan seçimi
  - Rezervasyon sahibi (sakin) seçimi
  - Tarih seçimi
  - Başlangıç ve bitiş saati
  - Katılımcı sayısı
  - Özel notlar
  - Kuralları kabul etme onayı
  - Ücret görüntüleme (ücretli ise)
- **API Endpoint**: `POST /api/reservations`, `GET /api/activity-areas/{id}/availability`

### 10. Sistem Ayarları

#### 10.1. Genel Ayarlar Ekranı
- **Kullanılan Modeller**: `SystemSetting`, `Company`
- **Bileşenler**:
  - Site adı ve logo ayarları
  - İletişim bilgileri
  - Bölgesel ayarlar (zaman dilimi, tarih formatı)
  - Para birimi ayarları
  - Sistem varsayılanları
  - E-posta şablonları
  - Bildirim ayarları
- **API Endpoint**: `GET /api/settings`, `PUT /api/settings`

#### 10.2. Kullanıcı Ayarları Ekranı
- **Kullanılan Modeller**: `User`, `NotificationSetting`
- **Bileşenler**:
  - Kullanıcı arayüz tercihleri (tema, dil)
  - Bildirim tercihleri (e-posta, SMS, uygulama)
  - Zaman dilimi ve tarih formatı
  - Veri gizlilik tercihleri
  - Özel gösterge paneli yapılandırması
- **API Endpoint**: `GET /api/users/{id}/settings`, `PUT /api/users/{id}/settings`

#### 10.3. Site Ayarları Ekranı
- **Kullanılan Modeller**: `Site`, `SiteSetting`
- **Bileşenler**:
  - Site özellikleri yapılandırması
  - Aidat hesaplama parametreleri
  - Rezervasyon kuralları
  - Şikayet ve bakım talebi kategorileri
  - Daire tipleri yapılandırması
  - Blok yapılandırması
- **API Endpoint**: `GET /api/sites/{id}/settings`, `PUT /api/sites/{id}/settings`

#### 10.4. Bildirim Ayarları Ekranı
- **Kullanılan Modeller**: `NotificationSetting`, `NotificationTemplate`
- **Bileşenler**:
  - Bildirim tipi listesi (aidat, bakım, rezervasyon vb.)
  - E-posta bildirimleri yapılandırması
  - SMS bildirimleri yapılandırması
  - Uygulama bildirimleri yapılandırması
  - Bildirim şablonları düzenleme
  - Test bildirim gönderme butonları
- **API Endpoint**: `GET /api/notification-settings`, `PUT /api/notification-settings`, `GET /api/notification-templates`

#### 10.5. Dil ve Bölge Ayarları Ekranı
- **Kullanılan Modeller**: `SystemSetting`, `Language`
- **Bileşenler**:
  - Varsayılan dil seçimi
  - Aktif diller listesi
  - Dil çevirilerini düzenleme arayüzü
  - Bölgesel format ayarları
  - Para birimi formatı
  - Tarih ve saat formatı
- **API Endpoint**: `GET /api/settings/localization`, `PUT /api/settings/localization`

#### 10.6. Yedekleme ve Geri Yükleme Ekranı
- **Kullanılan Modeller**: `Backup`
- **Bileşenler**:
  - Manuel yedekleme butonu
  - Otomatik yedekleme zamanlaması
  - Mevcut yedekler listesi
  - Yedek indirme butonu
  - Yedekten geri yükleme formu
  - Yedekleme geçmişi
- **API Endpoint**: `POST /api/system/backup`, `GET /api/system/backups`, `POST /api/system/restore`

### 11. Daire Kiralama ve Satış Yönetimi

#### 11.1. Daire Kiralama Listesi Ekranı
- **Kullanılan Modeller**: `ApartmentRental`
- **Bileşenler**:
  - Kiralama listesi tablosu (tarih, daire, müşteri, süre, durum)
  - Durum filtreleme
  - Tarih aralığı filtreleme
  - Kiralama ekleme butonu
  - Check-in/check-out işlem butonları
  - Sayfalama
  - Kiralama detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/apartment-rentals`

#### 11.2. Daire Kiralama Detay Ekranı
- **Kullanılan Modeller**: `ApartmentRental`, `ApartmentRentalPayment`
- **Bileşenler**:
  - Kiralama bilgileri kartı
  - Müşteri bilgileri
  - Daire bilgileri
  - Ödeme geçmişi
  - Check-in/check-out detayları
  - Sözleşme görüntüleme
  - İptal/değişiklik formu
- **API Endpoint**: `GET /api/apartment-rentals/{id}`, `GET /api/apartment-rentals/{id}/payments`

#### 11.3. Daire Kiralama Oluşturma Formu
- **Kullanılan Modeller**: `ApartmentRental`, `Apartment`
- **Form Alanları**:
  - Daire seçimi
  - Kiracı bilgileri (ad, soyad, kimlik, iletişim)
  - Kiralama başlangıç tarihi
  - Kiralama bitiş tarihi
  - Kira tutarı ve para birimi
  - Depozito tutarı
  - Ödeme periyodu (aylık, 3 aylık, yıllık)
  - Sözleşme yükleme alanı
  - Ek notlar
- **API Endpoint**: `POST /api/apartment-rentals`, `GET /api/apartments/available`

#### 11.4. Daire Satış Listesi Ekranı
- **Kullanılan Modeller**: `ApartmentSale`
- **Bileşenler**:
  - Satış listesi tablosu (tarih, daire, alıcı, satış tutarı, durum)
  - Durum filtreleme
  - Tarih aralığı filtreleme
  - Satış ekleme butonu
  - Sayfalama
  - Satış detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/apartment-sales`

#### 11.5. Daire Satış Detay Ekranı
- **Kullanılan Modeller**: `ApartmentSale`, `ApartmentSalePayment`
- **Bileşenler**:
  - Satış bilgileri kartı
  - Alıcı bilgileri
  - Daire bilgileri
  - Ödeme geçmişi
  - Tapu ve sözleşme bilgileri
  - İşlem geçmişi
- **API Endpoint**: `GET /api/apartment-sales/{id}`, `GET /api/apartment-sales/{id}/payments`

#### 11.6. Daire Satış Oluşturma Formu
- **Kullanılan Modeller**: `ApartmentSale`, `Apartment`
- **Form Alanları**:
  - Daire seçimi
  - Alıcı bilgileri (ad, soyad, kimlik, iletişim)
  - Satış tarihi
  - Satış tutarı ve para birimi
  - Ödeme planı (peşin, taksitli)
  - Tapu bilgileri
  - Sözleşme yükleme alanı
  - Ek notlar
- **API Endpoint**: `POST /api/apartment-sales`, `GET /api/apartments/for-sale`

### 12. Mobil Uygulama Özellikleri

#### 12.1. Mobil Gösterge Paneli
- **Kullanılan Modeller**: `Dashboard`, `User`, `Notification`
- **Bileşenler**:
  - Aidat ödemeleri özeti
  - Aktif bildirimler
  - Yaklaşan rezervasyonlar
  - Bakım talepleri durumu
  - Hızlı işlem butonları
- **API Endpoint**: `GET /api/mobile/dashboard`

#### 12.2. Mobil Ödeme Ekranları
- **Kullanılan Modeller**: `Payment`, `PaymentMethod`, `Invoice`
- **Bileşenler**:
  - Ödenmemiş faturalar listesi
  - Ödeme geçmişi
  - Ödeme detayı görüntüleme
  - Mobil ödeme formu
  - Kart bilgisi saklama
  - Makbuz görüntüleme
- **API Endpoint**: `GET /api/mobile/payments`, `POST /api/mobile/payments`, `GET /api/mobile/payment-methods`

#### 12.3. Mobil Talep/Bakım Ekranları
- **Kullanılan Modeller**: `MaintenanceRequest`, `RequestCategory`
- **Bileşenler**:
  - Talep listesi
  - Talep detayı
  - Yeni talep oluşturma formu
  - Fotoğraf/video ekleme
  - Talep durumu takibi
  - Geribildirim formu
- **API Endpoint**: `GET /api/mobile/maintenance-requests`, `POST /api/mobile/maintenance-requests`

#### 12.4. Mobil Rezervasyon Ekranları
- **Kullanılan Modeller**: `Reservation`, `ActivityArea`
- **Bileşenler**:
  - Rezervasyon listesi
  - Rezervasyon takvimi
  - Alan mevcudiyet kontrolü
  - Rezervasyon oluşturma formu
  - Bildirim ayarları
  - Rezervasyon iptal/değiştirme
- **API Endpoint**: `GET /api/mobile/reservations`, `POST /api/mobile/reservations`

## UI/UX ve Erişilebilirlik Özellikleri

### 1. Responsive Tasarım
- Tüm ekranlar 4 farklı ekran boyutuna göre tasarlanmıştır:
  - Mobil: < 600px
  - Tablet: 600px - 900px
  - Laptop: 900px - 1200px
  - Masaüstü: > 1200px
- Mobil ekranlarda menü daraltılabilir
- Tablolar mobil ekranlarda card görünümüne dönüşür

### 2. Erişilebilirlik Özellikleri
- WCAG 2.1 AA standartlarına uyumluluk
- Ekran okuyucu uyumluluğu
- Klavye navigasyonu desteği
- Yeterli kontrast oranları
- Boyut ve zoom desteği
- Alt text tanımlamaları

### 3. Tema ve Görünüm
- Açık/koyu tema desteği
- Dinamik renk paletleri
- Özelleştirilebilir arayüz bileşenleri
- Kurumsal renk şemaları desteği

### 4. Performans Optimizasyonları
- Lazy loading bileşenler
- Memoization ile gereksiz render'ların önlenmesi
- API yanıtlarının önbelleğe alınması
- Bundle splitting ile sayfa yükleme sürelerinin azaltılması
- Görsel optimizasyon (WebP, SVG)

## Geliştirme ve Test Süreci

### 1. Geliştirme Ortamı
- Node.js ve npm
- React 18
- TypeScript
- ESLint ve Prettier
- Git workflow (branch, commit, PR)
- Storybook ile bileşen geliştirme

### 2. Test Yaklaşımı
- Jest ile birim testler
- React Testing Library ile bileşen testleri
- Cypress ile E2E testler
- Mock Service Worker ile API testleri
- Lighthouse ile performans testleri

### 3. Deployment Süreci
- CI/CD pipeline (GitHub Actions)
- Otomatik test ve build
- Staging ve production ortamları
- Versiyonlama ve changelog
- Monitörleme ve hata takibi

## Sonuç

Bu dokümantasyon, Rezidans ve Site Yönetim Sistemi'nin frontend yapısını detaylı bir şekilde açıklamaktadır. Backend model yapısına dayanarak oluşturulan ekranlar, formlar, veri akışları ve kullanıcı deneyimi detayları burada belirtilmiştir. Geliştirme ekibi, bu dokümana göre tutarlı ve kaliteli bir frontend uygulaması geliştirerek projenin başarılı bir şekilde tamamlanmasını sağlayacaktır. 




# Rezidans ve Site Yönetim Sistemi Frontend Tasarım Dokümantasyonu - Bölüm 2

Bu doküman, Rezidans ve Site Yönetim Sistemi'nin frontend tasarımı ve uygulama yapısının detaylarını içermektedir.

## Modül Bazlı Ekran ve Form Detayları

### 1. Kimlik Doğrulama ve Kullanıcı Yönetimi

#### 1.1. Giriş (Login) Ekranı
- **Kullanılan Modeller**: `User`, `Auth`
- **Bileşenler**:
  - Kullanıcı adı/e-posta alanı
  - Şifre alanı (göster/gizle seçeneği ile)
  - "Beni Hatırla" seçeneği
  - Giriş butonu
  - "Şifremi Unuttum" bağlantısı
- **API Endpoint**: `POST /api/auth/login`
- **Navigasyon**: Başarılı girişte Dashboard'a yönlendirilir

#### 1.2. İki Faktörlü Doğrulama Ekranı
- **Kullanılan Modeller**: `Auth`, `TwoFactorAuth`
- **Bileşenler**:
  - Doğrulama kodu giriş alanı
  - Doğrulama butonu
  - Yeni kod gönder butonu
  - Geri dön bağlantısı
- **API Endpoint**: `POST /api/auth/verify-2fa`

#### 1.3. Şifremi Unuttum Ekranı
- **Kullanılan Modeller**: `User`
- **Bileşenler**:
  - E-posta giriş alanı
  - Sıfırlama talebi gönder butonu
  - Giriş sayfasına dön bağlantısı
- **API Endpoint**: `POST /api/auth/forgot-password`

#### 1.4. Şifre Sıfırlama Ekranı
- **Kullanılan Modeller**: `User`
- **Bileşenler**:
  - Yeni şifre alanı
  - Şifre onay alanı
  - Şifre gereksinimleri göstergesi
  - Sıfırlama butonu
- **API Endpoint**: `POST /api/auth/reset-password`

#### 1.5. Kullanıcı Profili Ekranı
- **Kullanılan Modeller**: `User`, `NotificationSetting`
- **Bileşenler**:
  - Profil bilgileri kartı (ad, soyad, e-posta, telefon)
  - Profil fotoğrafı (değiştirme seçeneği ile)
  - Şifre değiştirme bölümü
  - İki faktörlü doğrulama ayarları
  - Bildirim tercihleri
- **API Endpoint**: `GET /api/users/profile`, `PUT /api/users/profile`

#### 1.6. Kullanıcı Yönetimi Ekranı
- **Kullanılan Modeller**: `User`, `Role`, `UserRole`
- **Bileşenler**:
  - Kullanıcı listesi tablosu (ad, soyad, e-posta, rol, durum)
  - Arama ve filtreleme araçları
  - Sayfalama
  - Kullanıcı ekleme/düzenleme/silme butonları
  - Kullanıcı detay modalı
- **API Endpoint**: `GET /api/users`, `POST /api/users`, `PUT /api/users/{id}`, `DELETE /api/users/{id}`

#### 1.7. Rol Yönetimi Ekranı
- **Kullanılan Modeller**: `Role`
- **Bileşenler**:
  - Rol listesi tablosu (rol adı, açıklama, kullanıcı sayısı)
  - Rol ekleme/düzenleme/silme butonları
  - Rol detay modalı
  - Rol yetkilerini düzenleme arayüzü
- **API Endpoint**: `GET /api/roles`, `POST /api/roles`, `PUT /api/roles/{id}`, `DELETE /api/roles/{id}`

### 2. Ana Sayfa ve Dashboard

#### 2.1. Dashboard Ekranı
- **Kullanılan Modeller**: `Site`, `Building`, `Apartment`, `Resident`, `Payment`, `Maintenance`, `Complaint`
- **Bileşenler**:
  - Özet istatistik kartları
    - Toplam daire sayısı
    - Dolu daire sayısı
    - Boş daire sayısı
    - Toplam sakin sayısı
    - Bekleyen ödeme sayısı
  - Daire doluluk grafiği (pie chart)
  - Aylık gelir/gider grafiği (bar chart)
  - Son aktiviteler listesi
  - Bekleyen işlemler listesi
  - Yaklaşan ödemeler listesi
- **API Endpoint**: `GET /api/dashboard/stats`, `GET /api/dashboard/activities`

#### 2.2. Son Aktiviteler Ekranı
- **Kullanılan Modeller**: `ActivityLog`, `ManagerActivityLog`
- **Bileşenler**:
  - Aktivite listesi tablosu (tarih, kullanıcı, işlem, detay)
  - Tarih aralığı filtreleme
  - İşlem tipi filtreleme
  - Kullanıcı filtreleme
- **API Endpoint**: `GET /api/activities`

### 3. Site ve Bina Yönetimi

#### 3.1. Site Listesi Ekranı
- **Kullanılan Modeller**: `Site`
- **Bileşenler**:
  - Site listesi tablosu (site adı, adres, bina sayısı, daire sayısı)
  - Arama ve filtreleme araçları
  - Site ekleme/düzenleme/silme butonları
  - Sayfalama
  - Site detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/sites`, `POST /api/sites`, `PUT /api/sites/{id}`, `DELETE /api/sites/{id}`

#### 3.2. Site Detay Ekranı
- **Kullanılan Modeller**: `Site`, `Building`, `SiteManager`
- **Bileşenler**:
  - Site bilgileri kartı
  - Site yöneticileri listesi
  - Binalar listesi
  - Site mali özeti
  - Site istatistikleri grafiği
  - İşlem butonları (düzenle, sil)
- **API Endpoint**: `GET /api/sites/{id}`, `GET /api/sites/{id}/buildings`, `GET /api/sites/{id}/managers`

#### 3.3. Site Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `Site`
- **Form Alanları**:
  - Site adı
  - Site tipi
  - Adres bilgileri (il, ilçe, mahalle, tam adres)
  - İletişim bilgileri (telefon, e-posta)
  - Site özellikleri (çoklu seçim)
  - Site görselleri yükleme alanı
  - Site açıklaması
  - Site durumu (aktif/pasif)
- **API Endpoint**: `POST /api/sites`, `PUT /api/sites/{id}`

#### 3.4. Bina Listesi Ekranı
- **Kullanılan Modeller**: `Building`
- **Bileşenler**:
  - Bina listesi tablosu (bina adı, blok, kat sayısı, daire sayısı)
  - Site bazlı filtreleme
  - Bina ekleme/düzenleme/silme butonları
  - Sayfalama
  - Bina detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/buildings`, `GET /api/sites/{siteId}/buildings`

#### 3.5. Bina Detay Ekranı
- **Kullanılan Modeller**: `Building`, `Apartment`
- **Bileşenler**:
  - Bina bilgileri kartı
  - Daireler listesi
  - Bina planı görüntüleme
  - Blok bilgileri
  - Kat planları
  - İşlem butonları (düzenle, sil)
- **API Endpoint**: `GET /api/buildings/{id}`, `GET /api/buildings/{id}/apartments`

#### 3.6. Bina Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `Building`
- **Form Alanları**:
  - Bina adı
  - Blok numarası/adı
  - Bina tipi
  - Kat sayısı
  - Bodrum kat sayısı
  - Asansör sayısı
  - Yapım yılı
  - Bina açıklaması
  - Bina görselleri yükleme alanı
- **API Endpoint**: `POST /api/buildings`, `PUT /api/buildings/{id}`

### 4. Daire Yönetimi

#### 4.1. Daire Listesi Ekranı
- **Kullanılan Modeller**: `Apartment`
- **Bileşenler**:
  - Daire listesi tablosu (daire no, blok, kat, daire tipi, alan, durum)
  - Site ve bina bazlı filtreleme
  - Daire durumu filtreleme (boş, dolu, bakımda)
  - Daire ekleme/düzenleme/silme butonları
  - Sayfalama
  - Daire detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/apartments`, `GET /api/buildings/{buildingId}/apartments`

#### 4.2. Daire Haritası Ekranı
- **Kullanılan Modeller**: `Apartment`, `Building`
- **Bileşenler**:
  - Site/bina seçimi
  - Daire durumu renk göstergeleri
  - Etkileşimli daire yerleşim haritası
  - Kat bazlı filtreleme
  - Daire bilgi kutuları (hover/tıklama ile)
- **API Endpoint**: `GET /api/buildings/{id}/apartment-map`

#### 4.3. Daire Detay Ekranı
- **Kullanılan Modeller**: `Apartment`, `Resident`, `Payment`, `Maintenance`, `Complaint`
- **Bileşenler**:
  - Daire bilgileri kartı
  - Sakinler listesi (mevcut ve geçmiş)
  - Aidat ve ödeme geçmişi
  - Bakım talepleri listesi
  - Şikayet listesi
  - Daire özellikleri kartı
  - İşlem butonları (düzenle, durum değiştir)
- **API Endpoint**: `GET /api/apartments/{id}`, `GET /api/apartments/{id}/residents`, `GET /api/apartments/{id}/payments`

#### 4.4. Daire Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `Apartment`
- **Form Alanları**:
  - Daire numarası
  - Blok
  - Kat
  - Daire tipi (1+1, 2+1, 3+1 vb.)
  - Oda sayısı
  - Banyo sayısı
  - Brüt/net alan
  - Daire özellikleri (balkon, teras, eşyalı vb.)
  - Aylık aidat tutarı
  - Daire durumu (boş, dolu, bakımda)
  - Daire görselleri yükleme alanı
- **API Endpoint**: `POST /api/apartments`, `PUT /api/apartments/{id}`

### 5. Sakin Yönetimi

#### 5.1. Sakin Listesi Ekranı
- **Kullanılan Modeller**: `Resident`
- **Bileşenler**:
  - Sekme grupları (Tümü, Daire Sahipleri, Kiracılar, Rezervasyonlar, Ayrılanlar)
  - Sakin listesi tablosu (ad, soyad, iletişim, daire, durum)
  - Arama ve filtreleme araçları
  - Sakin ekleme/düzenleme/silme butonları
  - Sayfalama
  - Sakin detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/residents`, `GET /api/residents/owners`, `GET /api/residents/tenants`

#### 5.2. Sakin Detay Ekranı
- **Kullanılan Modeller**: `Resident`, `Apartment`, `Payment`, `Complaint`, `Maintenance`
- **Bileşenler**:
  - Sakin bilgileri kartı
  - İkamet bilgileri
  - Aile üyeleri listesi
  - Araç bilgileri
  - Ödemeler listesi
  - Talepler listesi
  - Sakin belgeleri
  - İşlem butonları (düzenle, taşınma işlemleri)
- **API Endpoint**: `GET /api/residents/{id}`, `GET /api/residents/{id}/payments`, `GET /api/residents/{id}/complaints`

#### 5.3. Sakin Ekleme/Düzenleme Formu
- **Kullanılan Modeller**: `Resident`
- **Form Alanları**:
  - Kişisel bilgiler (ad, soyad, TC/pasaport, doğum tarihi)
  - İletişim bilgileri (telefon, e-posta, acil durum kişisi)
  - İkamet bilgileri (ikamet tipi, taşınma tarihi)
  - Daire seçimi
  - Araç bilgileri
  - Evcil hayvan bilgileri
  - Belge yükleme (kimlik, sözleşme vb.)
  - Profil fotoğrafı
  - Kullanıcı hesabı oluşturma seçeneği
- **API Endpoint**: `POST /api/residents`, `PUT /api/residents/{id}`

#### 5.4. Daire Atama Ekranı
- **Kullanılan Modeller**: `Resident`, `Apartment`
- **Bileşenler**:
  - Sakin seçimi
  - Daire seçimi (boş daireler listesi)
  - İkamet tipi seçimi (mal sahibi, kiracı, misafir)
  - Taşınma tarihi seçimi
  - Kira/kontrat bilgileri
- **API Endpoint**: `POST /api/residents/{id}/assign-apartment`

### 6. Aidat ve Ödeme Yönetimi

#### 6.1. Aidat Listesi Ekranı
- **Kullanılan Modeller**: `DuesReconciliation`, `Payment`
- **Bileşenler**:
  - Aidat listesi tablosu (dönem, daire, sakin, tutar, durum)
  - Dönem filtreleme (ay/yıl)
  - Ödeme durumu filtreleme
  - Toplu aidat oluşturma butonu
  - Toplu tahsilat butonu
  - Sayfalama
  - Aidat detay modalı
- **API Endpoint**: `GET /api/dues`, `GET /api/dues/by-period`

#### 6.2. Aidat Oluşturma Ekranı
- **Kullanılan Modeller**: `DuesReconciliation`, `Apartment`
- **Bileşenler**:
  - Dönem seçimi (ay/yıl)
  - Site/bina/blok seçimi
  - Daire listesi (çoklu seçim)
  - Aidat tutarı (sabit veya daire bazlı)
  - Son ödeme tarihi
  - Açıklama alanı
  - Önizleme tablosu
- **API Endpoint**: `POST /api/dues/create-batch`

#### 6.3. Ödeme Listesi Ekranı
- **Kullanılan Modeller**: `Payment`
- **Bileşenler**:
  - Ödeme listesi tablosu (tarih, daire, sakin, tutar, ödeme tipi, durum)
  - Tarih aralığı filtreleme
  - Ödeme tipi filtreleme
  - Durum filtreleme
  - Ödeme ekleme butonu
  - Sayfalama
  - Ödeme detay modalı
- **API Endpoint**: `GET /api/payments`

#### 6.4. Ödeme Alma Formu
- **Kullanılan Modeller**: `Payment`, `Resident`, `Apartment`
- **Form Alanları**:
  - Ödeme yapan sakin seçimi
  - İlgili daire seçimi
  - Ödeme tipi (aidat, fatura, bakım ücreti vb.)
  - Ödeme miktarı
  - Ödeme yöntemi (nakit, kredi kartı, havale vb.)
  - Ödeme tarihi
  - Açıklama alanı
  - Dekont/makbuz yükleme alanı
- **API Endpoint**: `POST /api/payments`

#### 6.5. Ödeme Entegrasyonları Ekranı
- **Kullanılan Modeller**: `PaymentIntegration`
- **Bileşenler**:
  - Ödeme sağlayıcıları listesi (Iyzico, PayTR, PayU vb.)
  - Entegrasyon durumu göstergeleri
  - Entegrasyon ayarları formları
  - Test ödeme butonu
  - Webhook URL kopyalama
- **API Endpoint**: `GET /api/payment-integrations`, `PUT /api/payment-integrations/{id}`

### 7. Şikayet ve Bakım Yönetimi

#### 7.1. Şikayet Listesi Ekranı
- **Kullanılan Modeller**: `Complaint`
- **Bileşenler**:
  - Şikayet listesi tablosu (tarih, konu, sakin, daire, öncelik, durum)
  - Durum filtreleme
  - Öncelik filtreleme
  - Tarih aralığı filtreleme
  - Şikayet ekleme butonu
  - Sayfalama
  - Şikayet detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/complaints`

#### 7.2. Şikayet Detay Ekranı
- **Kullanılan Modeller**: `Complaint`, `TaskComment`, `User`
- **Bileşenler**:
  - Şikayet bilgileri kartı
  - Durum takip çubuğu
  - Yorumlar bölümü
  - Dosya ekleri görüntüleme
  - Atama formu
  - Durum güncelleme butonu
  - Çözüm kaydı formu
- **API Endpoint**: `GET /api/complaints/{id}`, `GET /api/complaints/{id}/comments`, `POST /api/complaints/{id}/status`

#### 7.3. Şikayet Oluşturma Formu
- **Kullanılan Modeller**: `Complaint`
- **Form Alanları**:
  - Başlık
  - Kategori seçimi
  - Öncelik seçimi
  - Ayrıntılı açıklama
  - İlgili daire seçimi
  - Dosya ekleme alanı
  - Bildirim tercihleri
- **API Endpoint**: `POST /api/complaints`

#### 7.4. Bakım Talepleri Listesi Ekranı
- **Kullanılan Modeller**: `Maintenance`
- **Bileşenler**:
  - Bakım talepleri tablosu (tarih, tip, sakin, daire, durum)
  - Bakım tipi filtreleme
  - Durum filtreleme
  - Tarih aralığı filtreleme
  - Bakım talebi ekleme butonu
  - Sayfalama
  - Bakım talebi detay sayfasına yönlendirme
- **API Endpoint**: `GET /api/maintenance-requests`

#### 7.5. Bakım Talebi Detay Ekranı
- **Kullanılan Modeller**: `Maintenance`, `Employee`
- **Bileşenler**:
  - Bakım talebi bilgileri kartı
  - Durum takip çubuğu
  - Atanan personel bilgileri
  - Planlanan ve gerçekleşen tarih bilgileri
  - Teknisyen notları
  - Maliyet bilgileri
  - Fotoğraf galerisi
  - Durum güncelleme butonu
- **API Endpoint**: `GET /api/maintenance-requests/{id}`, `PUT /api/maintenance-requests/{id}/status`

#### 7.6. Bakım Takvimi Ekranı
- **Kullanılan Modeller**: `Maintenance`, `Employee`, `TechnicalService`
- **Bileşenler**:
  - Aylık/haftalık/günlük takvim görünümü
  - Bakım tipine göre renk kodlaması
  - Personel filtreleme
  - Bakım tipi filtreleme
  - Hızlı bakım detayı görüntüleme
  - Sürükle-bırak ile tarih güncelleme
- **API Endpoint**: `GET /api/maintenance-requests/calendar` 



# Rezidans ve Site Yönetim Sistemi - Frontend Mimarisi

## 1. Kullanılacak Teknoloji ve Kütüphaneler

### 1.1. Temel Teknolojiler
- **React.js 18** - UI kütüphanesi
- **TypeScript 5** - Tip güvenliği için
- **Material UI 5** - UI komponent kütüphanesi
- **React Router 6** - Sayfa yönlendirme ve navigasyon
- **Context API** - State yönetimi

### 1.2. Yardımcı Kütüphaneler
- **Axios** - API istekleri
- **React Hook Form** - Form yönetimi ve validasyon
- **Yup** - Şema tabanlı form validasyonu
- **date-fns** - Tarih işlemleri
- **i18next** - Çoklu dil desteği (Türkçe ana dil)
- **Chart.js** - Grafikler ve raporlar için
- **react-table** - Tablo yönetimi ve veri gösterimi
- **react-query** - Sunucu state yönetimi ve önbellek
- **react-beautiful-dnd** - Sürükle-bırak işlemleri
- **react-pdf** - PDF raporları oluşturma ve görüntüleme
- **react-icons** - İkon kütüphanesi

### 1.3. Geliştirme Araçları
- **Vite** - Hızlı build ve hot-reload için
- **ESLint** - Kod kalitesi kontrol
- **Prettier** - Kod formatı
- **Jest** - Unit testler
- **Testing Library** - Component testleri
- **Cypress** - E2E testler

## 2. Proje Yapısı

```
frontend/
├── public/              # Statik dosyalar
├── src/
│   ├── assets/          # Resimler, fontlar, v.b.
│   ├── components/      # Yeniden kullanılabilir bileşenler
│   │   ├── common/      # Ortak bileşenler (Button, Input vb.)
│   │   ├── layouts/     # Layout bileşenleri
│   │   ├── forms/       # Form bileşenleri
│   │   ├── tables/      # Tablo bileşenleri
│   │   ├── cards/       # Kart bileşenleri
│   │   └── modals/      # Modal bileşenleri
│   ├── config/          # Yapılandırma dosyaları
│   ├── contexts/        # Context API tanımları
│   ├── hooks/           # Custom hooklar
│   ├── pages/           # Sayfa bileşenleri
│   │   ├── dashboard/   # Dashboard sayfaları
│   │   ├── site/        # Site yönetimi sayfaları
│   │   ├── apartment/   # Daire yönetimi sayfaları
│   │   ├── user/        # Kullanıcı yönetimi sayfaları
│   │   ├── financial/   # Finansal yönetim sayfaları
│   │   ├── department/  # Departman yönetimi sayfaları
│   │   ├── reports/     # Raporlar sayfaları
│   │   └── settings/    # Ayarlar sayfaları
│   ├── services/        # API servisleri
│   ├── types/           # TypeScript tipleri
│   ├── utils/           # Yardımcı fonksiyonlar
│   ├── App.tsx          # Ana uygulama bileşeni
│   ├── routes.tsx       # Rota tanımları
│   ├── theme.ts         # Tema yapılandırması
│   └── main.tsx         # Giriş noktası
├── package.json         # Paket bağımlılıkları
└── vite.config.ts       # Vite yapılandırması
```

## 3. Uygulama Mimarisi

### 3.1. Tema ve Stil
- Kurumsal bir tasarım dili
- Responsive tasarım (mobil, tablet, masaüstü)
- Koyu ve açık tema desteği
- Özelleştirilebilir renk paleti
- Material Design tasarım prensipleri

### 3.2. Ana Komponentler
- **AppBar** - Üst navigasyon çubuğu, kullanıcı profili, bildirimler
- **SideNav** - Yan navigasyon menüsü, modül erişimi
- **Dashboard** - Ana gösterge paneli
- **DataTable** - Veri tabloları
- **FormBuilder** - Dinamik form oluşturma
- **FilterPanel** - Arama ve filtreleme
- **ActionPanel** - İşlem butonları
- **NotificationSystem** - Bildirim yönetimi
- **ModalManager** - Modal diyalog yönetimi
- **ThemeProvider** - Tema yönetimi

### 3.3. API İletişimi
- Axios ile RESTful API entegrasyonu
- Interceptor'lar ile token yönetimi
- React Query ile önbellek ve yeniden fetch stratejisi
- Hata işleme ve kullanıcı geri bildirimi

### 3.4. Güvenlik
- JWT token tabanlı kimlik doğrulama
- Rol tabanlı erişim kontrolü
- Oturum yönetimi ve otomatik çıkış
- Güvenli localStorage kullanımı

## 4. Ekranlar ve Özellikler

### 4.1. Giriş ve Yetkilendirme
- **Giriş Ekranı**
  - Kullanıcı adı/e-posta ve şifre ile giriş
  - Şifremi unuttum işlevi
  - Remember me seçeneği
  
- **İki Faktörlü Doğrulama**
  - SMS veya e-posta doğrulama kodu
  - Güvenlik soruları

- **Şifre Sıfırlama**
  - E-posta ile şifre sıfırlama bağlantısı
  - Yeni şifre belirleme ekranı

### 4.2. Dashboard
- **Genel Bakış**
  - Özet kart bileşenleri (toplam site, daire, kullanıcı vs.)
  - Son aktiviteler tablosu
  - Kritik bildirimler
  
- **Finansal Özet**
  - Gelir/gider grafiği
  - Ödeme durumu çizelgesi
  - Aidat tahsilat oranı
  
- **Site Durumu**
  - Aktif/pasif siteler
  - Doluluk oranları
  - Bakım/onarım durumları

### 4.3. Site Yönetimi
- **Site Listesi Ekranı**
  - Siteler listesi (tablo görünümü)
  - Filtreler (konum, tip, durum)
  - Site ekleme, düzenleme, silme işlemleri
  
- **Site Detay Ekranı**
  - Site bilgileri (genel, iletişim, adres)
  - Blok ve bina bilgileri
  - Yönetici bilgileri
  - Site etkinlikleri
  - Site duyuruları
  
- **Blok/Bina Yönetimi**
  - Blok listesi
  - Blok detayları (kat planı, daireler)
  - Bakım bilgileri

### 4.4. Daire Yönetimi
- **Daire Listesi Ekranı**
  - Daireler listesi (tablo görünümü)
  - Filtreler (blok, kat, durum, daire tipi)
  - Daire ekleme, düzenleme, silme işlemleri
  
- **Daire Detay Ekranı**
  - Daire bilgileri (no, kat, alan, oda sayısı)
  - Mülk sahibi bilgileri
  - Kiracı bilgileri
  - Aidat ve ödeme bilgileri
  - Bakım/arıza kayıtları
  
- **Toplu İşlemler**
  - Filtreye göre toplu aidat atama
  - Toplu mesaj/bildirim gönderme
  - Toplu yazdırma işlemleri

### 4.5. Kişi Yönetimi
- **Mülk Sahipleri Listesi**
  - Mülk sahipleri tablosu
  - Filtreler (site, daire, durum)
  - Mülk sahibi ekleme, düzenleme, silme
  
- **Kiracılar Listesi**
  - Kiracılar tablosu
  - Filtreler (site, daire, kontrat durumu)
  - Kiracı ekleme, düzenleme, silme

- **Kişi Detay Ekranı**
  - Kişisel bilgiler
  - İletişim bilgileri
  - Sahip olduğu/kiraladığı daireler
  - Ödeme geçmişi
  - Belge yönetimi

### 4.6. Departman Yönetimi
- **Departman Listesi Ekranı**
  - Departmanlar tablosu
  - Filtreler (site, tip, durum)
  - Departman ekleme, düzenleme, silme
  
- **Departman Detay Ekranı**
  - Temel bilgiler (ad, tip, açıklama)
  - Sorumlu kişi bilgileri
  - Çalışma saatleri
  - İletişim bilgileri
  
- **Departman Tipi Yönetimi**
  - Tip listesi
  - Tip özellikleri (ikon, renk)
  - Sistem tanımlı tipler

### 4.7. Finansal Yönetim
- **Aidat Yönetimi**
  - Aidat tanımlama ekranı
  - Dönemsel aidat belirleme
  - Daire tipine göre aidat tanımlama
  
- **Ödeme Takibi**
  - Ödeme listesi
  - Ödeme detayları
  - Vadesi geçen ödemeler
  
- **Gelir/Gider Yönetimi**
  - Gelir kaydı ekleme
  - Gider kaydı ekleme
  - Hesap kategorileri
  
- **Fatura Yönetimi**
  - Fatura oluşturma
  - Fatura listesi
  - Fatura detayı

### 4.8. Raporlar
- **Finansal Raporlar**
  - Aylık/yıllık gelir-gider raporu
  - Aidat tahsilat raporu
  - Borç durumu raporu
  
- **Doluluk Raporları**
  - Site/bina doluluk oranları
  - Daire durumu analizi
  
- **Aktivite Raporları**
  - Sistem kullanım raporları
  - Kullanıcı işlem kayıtları
  
- **KBS Raporları**
  - Kimlik Bildirme Sistemi entegrasyonu
  - KBS raporları oluşturma ve gönderim

### 4.9. Ayarlar
- **Sistem Ayarları**
  - Sistem parametreleri
  - Bildirim ayarları
  - Yedekleme ayarları
  
- **Kullanıcı Yönetimi**
  - Kullanıcı listesi
  - Rol ve yetki tanımları
  - Şirket yöneticileri
  
- **Şirket Ayarları**
  - Şirket bilgileri
  - Şube yönetimi
  - Logo ve tema ayarları

### 4.10. Rezervasyon Yönetimi
- **Daire Rezervasyon Sistemi**
  - Kiralanabilir dairelerin listesi
  - Müsaitlik durumu takibi (boş/dolu tarih aralıkları)
  - Rezervasyon takvimi (aylık, haftalık görünüm)
  - Rezervasyon oluşturma/düzenleme/iptal etme
  - Minimum/maksimum konaklama süreleri
  - Sezon bazlı fiyatlandırma

- **Rezervasyon Listesi Ekranı**
  - Tüm rezervasyonlar tablosu
  - Filtreler (tarih aralığı, durum, daire, site)
  - Rezervasyon detayları
  - Checkin/Checkout işlemleri
  - Toplu rezervasyon listesi yazdırma

- **Rezervasyon Detay Ekranı**
  - Müşteri bilgileri (ad, telefon, email)
  - Rezervasyon detayları (giriş-çıkış, kişi sayısı)
  - Ödeme bilgileri (tutar, ödeme yöntemi, ödeme durumu)
  - Hizmet ekstra bilgileri (özel istekler, notlar)
  - Sözleşme oluşturma ve yazdırma

- **Gelir Paylaşımı ve Hakediş**
  - Daire sahiplerine ödenecek hakediş hesaplama
  - Komisyon ve kesinti tanımlama
  - Otomatik hakediş raporu oluşturma
  - Ödeme takibi ve onay süreci
  - Vergi hesaplamaları ve bildirimleri

### 4.11. Sosyal Tesis ve Ortak Alan Rezervasyonları
- **Rezervasyon Edilebilir Alanlar Yönetimi**
  - Alan tanımlama (spor salonu, toplantı odası, havuz, vb.)
  - Alan özellikleri ve kapasiteleri
  - Ücretli/ücretsiz alan tanımlama
  - Kullanım kuralları ve kısıtlamaları

- **Alan Rezervasyon Takvimi**
  - Zaman dilimi bazlı görünüm (saat, yarım gün, tam gün)
  - Rezervasyon durumu gösterimi (boş, rezerve, bakımda)
  - Rezervasyon çakışma kontrolü
  - Sürükle-bırak ile rezervasyon yönetimi

- **Rezervasyon Oluşturma**
  - Tarih ve saat aralığı seçimi
  - Kişi sayısı belirleme
  - Ödeme seçenekleri (ücretli alanlarda)
  - Online ödeme entegrasyonu
  - Onayla/iptal e-postaları

- **Kullanım Raporları**
  - Alan bazlı doluluk oranları
  - Gelir raporları (ücretli alanlar için)
  - En çok tercih edilen alanlar ve saatler
  - Raporları dışa aktarma (PDF, Excel)

## 5. Backend Entegrasyonu

### 5.1. Veri Modeli Entegrasyonu
| Frontend Ekranı | Backend Model | Kullanılan API Endpoint |
|-----------------|---------------|-------------------------|
| Site Listesi | Site | GET /api/sites |
| Site Detay | Site, Building | GET /api/sites/{id} |
| Daire Listesi | Apartment | GET /api/apartments |
| Daire Detay | Apartment, Owner, Tenant | GET /api/apartments/{id} |
| Departman Listesi | Department | GET /api/departments |
| Departman Tip Listesi | DepartmentType | GET /api/department-types |
| Kullanıcı Yönetimi | User, CompanyManager | GET /api/users |
| Finansal İşlemler | FinancialTransaction | GET /api/financial-transactions |
| Aktivite Logları | CompanyActivityLog | GET /api/activity-logs |
| Daire Rezervasyonları | Reservation | GET /api/reservations |
| Rezervasyon Detayı | Reservation, Payment | GET /api/reservations/{id} |
| Hakediş Raporları | Payment, Owner | GET /api/payments/owner-payments |
| Ortak Alan Rezervasyonları | FacilityReservation | GET /api/facility-reservations |
| Alan Müsaitlik Durumu | FacilityReservation | GET /api/facilities/{id}/availability |

### 5.2. API Hata Yönetimi
- HTTP durum kodlarına göre hata işleme
- Kullanıcı dostu hata mesajları
- Retry mekanizmaları
- Offline mod desteği (sınırlı işlevsellikle)

## 6. Performans Optimizasyonu

### 6.1. Yükleme Stratejileri
- Lazy loading (React.lazy ve Suspense)
- Code splitting (route bazlı)
- Progressive loading (iskelet yükleyiciler)
- Önbellek yönetimi (React Query)

### 6.2. Render Optimizasyonu
- Memo ve useCallback kullanımı
- Sanal listeler (büyük veri setleri için)
- Debounce ve throttle mekanizmaları

## 7. Erişilebilirlik

- WCAG 2.1 AA uyumluluğu
- Klavye navigasyonu desteği
- Screen reader uyumluluğu
- Yüksek kontrast modu

## 8. Kurulum ve Başlatma

### 8.1. Geliştirme Ortamı Kurulumu
```bash
# Bağımlılıkları yükle
npm install

# Geliştirme sunucusunu başlat
npm run dev
```

### 8.2. Build ve Deployment
```bash
# Üretim build'i oluştur
npm run build

# Üretim ortamında test et
npm run preview
```

## 9. Test Stratejisi

### 9.1. Birim Testleri
- Komponent testleri
- Hook testleri
- Util fonksiyon testleri

### 9.2. Entegrasyon Testleri
- Sayfa testleri
- Form akışları
- API entegrasyonları

### 9.3. E2E Testleri
- Kullanıcı hikayesi bazlı testler
- Çoklu tarayıcı testleri
- Mobil cihaz testleri

## 10. Multitenancy Yaklaşımı

Frontend uygulaması, multi-tenant yapıyı destekleyecek şekilde tasarlanacaktır:

- Kullanıcı girişi yapıldığında, FirmaID ve SubeID bilgileri saklanır
- Tüm API isteklerinde bu bilgiler header'larda gönderilir
- Kullanıcı arayüzü, firma ve şube yetkilerine göre dinamik olarak şekillenir
- Firma/şube bazlı tema ve özelleştirmeler
- Çoklu oturum yönetimi ve şubeler arası geçiş desteği

## 11. Arayüz Örnekleri

### 11.1. Dashboard Ekranı

```


# Rezidans ve Site Yönetim Sistemi - Proje Özeti

## Proje Tanımı

Rezidans ve Site Yönetim Sistemi, toplu konut alanlarının (site, rezidans, apartman vb.) profesyonel yönetimini sağlayan entegre bir çözümdür. Sistem, çok kiracılı (multi-tenant) mimari ile birden fazla site yönetim şirketine hizmet verebilir. Her şirket kendi sitelerini, binalarını, dairelerini ve sakinlerini ayrı olarak yönetebilir.

## Sistem Özellikleri ve Fonksiyonları

### 1. Site ve Rezidans Yönetimi
Site ve rezidans bilgilerinin merkezi yönetim alanıdır. 

- Site ve bina kayıtları oluşturma ve düzenleme
- Daire envanteri ve özelliklerini yönetme
- Ortak alanların takibi ve bakım planlaması
- Site özelliklerinin (havuz, spor salonu, sosyal tesisler vb.) yönetimi
- Site kuralları ve yönetmeliklerinin dijital arşivi

### 2. Sakin ve Kiracı Yönetimi
Sitelerde yaşayan sakinlerin, kiracıların ve mal sahiplerinin bilgilerinin tutulduğu ve yönetildiği modüldür.

- Sakin bilgilerinin kaydı ve güncellemesi
- Kiracı sözleşmelerinin takibi
- Ev sahipleri ile iletişim yönetimi
- Aile üyelerinin ve ziyaretçilerin kaydı
- Araç ve evcil hayvan bilgilerinin yönetimi
- KBS entegrasyonu ile kimlik bildirimlerinin otomatikleştirilmesi

### 3. Finansal Yönetim
Sitenin tüm gelir ve giderlerinin yönetildiği, aidat takibinin yapıldığı finansal yönetim modülüdür.

- Aidat tahsilatı ve takibi
- Fatura yönetimi ve ödeme takibi
- Gider yönetimi ve bütçeleme
- Finansal raporlama ve analiz
- Borç/alacak takibi
- Banka entegrasyonu ile otomatik ödeme sistemleri
- Çoklu para birimi desteği
- Farklı para birimlerinde ödeme kabul etme ve otomatik dönüşüm
- Daire sahiplerinin aidat borçlarını mahsuplaştırma

### 4. Rezervasyon ve Müsaitlik Yönetimi
Ortak alanların ve kiralama için uygun dairelerin rezervasyonunun yapıldığı modüldür.

- Dairelerin doluluk/boşluk takibi
- Saatlik, günlük, haftalık, aylık rezervasyon imkanı
- Ortak alanlar için rezervasyon oluşturma
- Ücretli ve ücretsiz aktivite alanları yönetimi
- Takvim görünümü ile uygunluk kontrolü
- Check-in ve check-out işlemleri
- Rezervasyon hatırlatıcıları
- Kullanım istatistikleri ve doluluk raporu

### 5. Teknik Servis ve Hizmet Planlaması
Site yöneticilerinin görevlerini planladığı, teknik ekiplere iş emirleri oluşturduğu ve takip ettiği modüldür.

- Görev oluşturma ve atama
- İş emirlerinin oluşturulması ve takibi
- Planlı bakım programları
- Teknik servis talepleri ve yönetimi
- Hizmetlerin ücretlendirilmesi ve daire hesabına yansıtılması
- Görev hatırlatıcıları ve bildirimler
- Görev tamamlama raporları

### 6. İletişim ve Duyuru Yönetimi
Site sakinleri ile iletişimin sağlandığı, duyuruların yönetildiği ve şikayetlerin alındığı modüldür.

- Site duyurularının yayınlanması
- Acil durum bildirimlerinin gönderilmesi
- Şikayet ve taleplerin alınması ve yönetimi
- Anket ve geri bildirim toplama
- Etkinlik duyuruları ve katılım yönetimi
- SMS ve e-posta ile toplu bildirim gönderimi

### 7. Güvenlik Yönetimi
Site güvenliğinin yönetildiği, giriş-çıkış kayıtlarının tutulduğu ve güvenlik kameraları gibi sistemlerin entegre edildiği modüldür.

- Ziyaretçi giriş-çıkış takibi
- Araç giriş-çıkış kaydı
- Güvenlik personeli görev planlaması
- Kamera ve güvenlik sistemleri entegrasyonu
- Olay raporlama ve takibi
- Güvenlik talimatları ve acil durum prosedürleri

### 8. KBS (Kimlik Bildirimi Sistemi) Entegrasyonu
Yasal gerekliliklere uygun olarak kimlik bildirimlerinin yapıldığı modüldür.

- Bir rezervasyona birden fazla aile üyesi kimlik bilgisi bağlanabilmesi
- Jandarma KBS raporunun otomatik hazırlanması ve gönderilmesi
- Site sakinleri için kimlik bildirim sistemi
- Giriş-çıkış bildirimleri ve takibi
- Toplu kimlik bildirimi gönderimi
- Otomatik gönderim seçeneği

### 9. Doküman ve Arşiv Yönetimi
Site ile ilgili tüm yasal ve idari belgelerin, projelerin ve planların dijital olarak saklandığı arşiv modülüdür.

- Yasal belgelerin arşivlenmesi
- Site projelerinin ve planlarının saklanması
- Yönetim kurulu kararlarının arşivlenmesi
- Sözleşme ve protokollerin yönetimi
- Geçmiş yazışma ve raporların saklanması
- Doküman arama ve filtreleme özellikleri

### 10. Dil ve Para Birimi Özellikleri
Sistemin çoklu dil ve para birimi desteği ile uluslararası kullanıma uygun olarak tasarlanmıştır.

- Türkçe, İngilizce, Almanca, Rusça, Arapça ve Farsça dil desteği
- Kullanıcı bazlı dil tercihi
- Firma/site bazlı ana para birimi tanımlama
- Farklı para birimlerinde işlem yapabilme
- Kurlar arası otomatik dönüşüm
- TCMB'den otomatik kur güncellemesi

### 11. Mobil Erişim
Hem site yöneticileri hem de sakinler için mobil uygulama üzerinden sisteme erişim sağlayan modüldür.

- Mobil uygulama ile sakinlerin bilgilere erişimi
- Aidat ödeme ve borç sorgulama
- Duyuru alma ve bildirim görüntüleme
- Rezervasyon yapma ve takip etme
- Şikayet ve talep oluşturma
- Ziyaretçi kaydı oluşturma

## Teknik Altyapı

- Backend: .NET 8 Web API (C#) ve MSSQL
- Frontend: React.js ve Material UI
- Mobil: React Native
- Mimari: Multi-tenant, katmanlı mimari (Core, Infrastructure, API, Data)
- Güvenlik: JWT token tabanlı kimlik doğrulama, rol bazlı yetkilendirme, iki faktörlü kimlik doğrulama
- Entegrasyon: KBS, RFID kart sistemi, plaka tanıma sistemi, SMS/Email, banka entegrasyonları, E-Fatura ve E-Arşiv

## Sistemin Faydaları

- Verimlilik Artışı: Manuel işlemlerin otomatikleştirilmesi ile zaman tasarrufu sağlar.
- Finansal Şeffaflık: Gelir-gider takibi ve raporlama ile finansal durumun net görülmesini sağlar.
- İletişim İyileştirmesi: Site yönetimi ve sakinler arasında etkin iletişim kanalları sunar.
- Yasal Uyumluluk: KBS ve diğer yasal gerekliliklerin kolayca karşılanmasını sağlar.
- Karar Verme Desteği: Veri analitiği ve raporlama özellikleri ile daha iyi kararlar alınmasını destekler.
- Sakin Memnuniyeti: Daha iyi hizmet ve iletişim ile sakin memnuniyetini artırır.
- Maliyetlerin Azaltılması: Planlı bakım ve etkili bütçe yönetimi ile gereksiz harcamaları azaltır.
- Güvenlik İyileştirmesi: Gelişmiş giriş-çıkış kontrolü ve güvenlik yönetimi ile daha güvenli bir yaşam alanı sunar. 