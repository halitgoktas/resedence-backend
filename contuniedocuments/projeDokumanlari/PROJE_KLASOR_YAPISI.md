# PROJE KLASÖR YAPISI VE TEKNOLOJİ DAĞILIMI

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin klasör yapısını ve hangi teknolojilerin hangi klasörlerde geliştirileceğini açıklar.

## 1. Ana Klasör Yapısı

Projenin ana klasör yapısı aşağıdaki gibidir:

```
C:\c\Residance-Manager\
│
├── backend/                   # Backend uygulaması (C# .NET 8 Web API)
│
├── frontend/                  # Frontend uygulaması (React.js ve Material UI)
│
├── docs/                      # Proje dokümantasyonu
│
└── projeDokumanlari/          # Proje yönetimi dokümantasyonu
```

## 2. Backend Klasör Yapısı (C# .NET 8)

Backend uygulaması `backend` klasöründe geliştirilecek ve aşağıdaki yapıya sahip olacaktır:

```
backend/
│
├── ResidenceManagement.API/              # API katmanı
│   ├── Controllers/                      # API kontrolcüleri
│   ├── Middlewares/                      # Özel middleware'ler
│   ├── Filters/                          # API filtreleri
│   ├── DTOs/                             # Veri transfer nesneleri
│   ├── Extensions/                       # Servis uzantıları
│   └── Configurations/                   # API yapılandırmaları
│
├── ResidenceManagement.Core/             # Çekirdek katmanı
│   ├── Entities/                         # Veritabanı modelleri
│   │   ├── Base/                         # Temel entity sınıfları
│   │   ├── Identity/                     # Kimlik doğrulama modelleri
│   │   ├── Financial/                    # Finansal modeller
│   │   ├── Property/                     # Mülk ile ilgili modeller
│   │   ├── Activity/                     # Etkinlik modelleri
│   │   └── Integration/                  # Entegrasyon modelleri
│   ├── Interfaces/                       # Servis ve repository arayüzleri
│   ├── Enums/                            # Sabit değer tipleri
│   ├── Exceptions/                       # Özel istisna sınıfları
│   └── Constants/                        # Sabit değerler
│
├── ResidenceManagement.Infrastructure/   # Altyapı katmanı
│   ├── Data/                             # Veritabanı işlemleri
│   │   ├── Context/                      # DbContext sınıfları
│   │   ├── Repositories/                 # Repository implementasyonları
│   │   ├── Configurations/               # Entity konfigürasyonları
│   │   ├── Migrations/                   # Veritabanı migrasyonları
│   │   └── Seeding/                      # Başlangıç verileri
│   ├── Services/                         # Servis implementasyonları
│   ├── Caching/                          # Önbellek mekanizması
│   ├── Logging/                          # Loglama servisleri
│   ├── Email/                            # E-posta servisleri
│   ├── SMS/                              # SMS servisleri
│   └── ExternalServices/                 # Dış servisler
│
└── ResidenceManagement.Tests/            # Test katmanı
    ├── Unit/                             # Birim testleri
    ├── Integration/                      # Entegrasyon testleri
    ├── Fixtures/                         # Test verileri
    └── Mocks/                            # Mock nesneleri
```

### Backend Teknolojileri:

- **Ana Framework:** .NET 8 Web API (C#)
- **ORM:** Entity Framework Core 8
- **Veritabanı:** MSSQL Server
- **API Dokümantasyonu:** Swagger/OpenAPI
- **Validasyon:** FluentValidation
- **Nesne Eşleme:** AutoMapper
- **Kimlik Doğrulama:** JWT Authentication
- **Loglama:** Serilog
- **Önbellekleme:** Redis
- **Arkaplan İşleri:** Hangfire
- **Birim Test:** xUnit
- **Mock:** Moq
- **API Test:** Postman / REST Client

## 3. Frontend Klasör Yapısı (React.js)

Frontend uygulaması `frontend` klasöründe geliştirilecek ve aşağıdaki yapıya sahip olacaktır:

```
frontend/
│
├── public/                      # Statik dosyalar
│   ├── index.html               # Ana HTML dosyası
│   ├── favicon.ico              # Favicon
│   └── assets/                  # Diğer statik varlıklar (resimler, fontlar, vb.)
│
├── src/                         # Kaynak kodları
│   ├── components/              # Yeniden kullanılabilir komponentler
│   │   ├── common/              # Ortak komponentler (buton, form, modal vb.)
│   │   ├── layout/              # Düzen komponentleri (header, footer, sidebar vb.)
│   │   └── specific/            # Özel komponentler
│   │
│   ├── pages/                   # Sayfalar
│   │   ├── auth/                # Kimlik doğrulama sayfaları
│   │   ├── dashboard/           # Gösterge paneli sayfaları
│   │   ├── admin/               # Yönetici sayfaları
│   │   ├── resident/            # Sakin sayfaları
│   │   └── settings/            # Ayarlar sayfaları
│   │
│   ├── services/                # API servisleri
│   │   ├── api.js               # API yapılandırması
│   │   ├── authService.js       # Kimlik doğrulama servisi
│   │   ├── userService.js       # Kullanıcı servisi
│   │   └── ...                  # Diğer servisler
│   │
│   ├── context/                 # Context API state yönetimi
│   │   ├── AuthContext.js       # Kimlik doğrulama context'i
│   │   ├── UserContext.js       # Kullanıcı context'i
│   │   └── ...                  # Diğer context'ler
│   │
│   ├── hooks/                   # Özel hook'lar
│   │   ├── useAuth.js           # Kimlik doğrulama hook'u
│   │   ├── useForm.js           # Form hook'u
│   │   └── ...                  # Diğer hook'lar
│   │
│   ├── utils/                   # Yardımcı fonksiyonlar
│   │   ├── formatter.js         # Biçimlendirme fonksiyonları
│   │   ├── validator.js         # Doğrulama fonksiyonları
│   │   └── ...                  # Diğer yardımcı fonksiyonlar
│   │
│   ├── assets/                  # Komponent-spesifik varlıklar
│   │   ├── images/              # Resimler
│   │   ├── styles/              # Stil dosyaları
│   │   └── icons/               # İkonlar
│   │
│   ├── constants/               # Sabitler
│   │   ├── routes.js            # Rota sabitleri
│   │   ├── apiEndpoints.js      # API endpoint sabitleri
│   │   └── ...                  # Diğer sabitler
│   │
│   ├── App.js                   # Ana uygulama komponenti
│   ├── index.js                 # Giriş noktası
│   └── routes.js                # Rota yapılandırması
│
├── package.json                 # Paket bağımlılıkları
├── .env                         # Ortam değişkenleri
└── README.md                    # Proje dokümantasyonu
```

### Frontend Teknolojileri:

- **Ana Framework:** React.js
- **UI Kütüphanesi:** Material UI
- **Routing:** React Router
- **State Yönetimi:** Context API
- **HTTP İstekleri:** Axios
- **Form Yönetimi:** React Hook Form
- **Validasyon:** Yup
- **Stil Yaklaşımı:** CSS-in-JS (styled-components veya emotion)
- **Çoklu Dil Desteği:** i18next
- **Tarih İşlemleri:** date-fns
- **Test:** Jest ve React Testing Library
- **Bundler:** Webpack (Create React App veya Vite)

## 4. Proje Dokümanları

Proje dokümanları `docs` ve `projeDokumanlari` klasörlerinde tutulacaktır:

```
docs/
│
├── api/                        # API dokümantasyonu
│   ├── swagger.json            # Swagger tanımı
│   └── postman_collection.json # Postman koleksiyonu
│
├── database/                   # Veritabanı dokümantasyonu
│   ├── schema.png              # Veritabanı şeması
│   └── erd.png                 # Entity İlişki Diyagramı
│
├── architecture/               # Mimari dokümantasyonu
│   ├── overview.png            # Genel bakış diyagramı
│   └── components.png          # Bileşen diyagramı
│
└── user_guides/                # Kullanıcı kılavuzları
    ├── admin_guide.pdf         # Yönetici kılavuzu
    └── resident_guide.pdf      # Sakin kılavuzu

projeDokumanlari/
│
├── PROJE_YOL_HARITASI.md       # Proje yol haritası
├── PROJE_YASAM_DONGUSU.md      # Proje yaşam döngüsü
├── PROJE_GELISIM_MIMARISI.md   # Teknik mimari ve gelişim adımları
├── VERITABANI_YAPISI.md        # Veritabanı şeması ve ilişkileri
├── KURALLAR.md                 # Geliştirme kuralları
├── CHANGELOG.md                # Değişiklik kaydı
└── PROJE_KLASOR_YAPISI.md      # Bu dosya - klasör yapısını ve teknoloji dağılımını açıklar
```

## 5. Önemli Notlar

1. Backend ve frontend geliştirmesi ayrı klasörlerde yürütülecektir, ancak API sözleşmeleri belirlendikten sonra paralel olarak geliştirmeye başlanabilir.

2. Her iki projenin de kendi versiyonlama sistemi olacaktır, ancak release sürümleri koordineli olarak planlanacaktır.

3. Tüm geliştirmeler ilgili klasörlerde yapılacak, kod karışıklığı önlenecektir.

4. Her iki proje için de ayrı git branch'leri kullanılarak geliştirme yapılacaktır.

5. API değişiklikleri her iki tarafı da etkileyeceğinden, değişiklikler dikkatli planlanmalı ve dokümante edilmelidir. 