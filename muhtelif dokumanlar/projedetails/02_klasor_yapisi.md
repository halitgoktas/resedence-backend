# Residence Management Projesi - Klasör Yapısı

## Proje Ağaç Yapısı

```
ResidenceManagement/
├── ResidenceManagement.Core/                 # Çekirdek katman
│   ├── Common/                              # Ortak kullanılan yapılar
│   ├── Configurations/                      # Yapılandırma sınıfları
│   ├── DTOs/                                # Veri transfer nesneleri
│   ├── Entities/                            # Veritabanı modelleri
│   ├── Enums/                               # Sabit değerler
│   ├── Exceptions/                          # Özel hata sınıfları
│   ├── Filters/                             # Filtreleme mekanizmaları
│   ├── Helpers/                             # Yardımcı sınıflar
│   ├── Interfaces/                          # Servis ve repository arayüzleri
│   ├── Models/                              # İş mantığı modelleri
│   └── Services/                            # Temel servis tanımlamaları
│
├── ResidenceManagement.Infrastructure/       # Altyapı katmanı
│   ├── Caching/                             # Önbellekleme mekanizması
│   ├── Data/                                # Veritabanı işlemleri
│   │   ├── Configurations/                  # Entity konfigürasyonları
│   │   ├── Repositories/                    # Repository implementasyonları
│   │   └── Context/                         # DbContext ve migrations
│   ├── Email/                               # E-posta gönderim servisi
│   ├── ExternalServices/                    # Dış servis entegrasyonları
│   ├── Logging/                             # Loglama mekanizması
│   ├── SMS/                                 # SMS gönderim servisi
│   └── Services/                            # Servis implementasyonları
│
├── ResidenceManagement.API/                  # API katmanı
│   ├── Controllers/                         # API endpoint'leri
│   ├── Configurations/                      # API yapılandırmaları
│   ├── DTOs/                                # API'ye özel DTO'lar
│   ├── Extensions/                          # Genişletme metodları
│   ├── Filters/                             # API filtreleri
│   ├── Middlewares/                         # Ara yazılımlar
│   └── Properties/                          # API özellikleri
│
├── ResidenceManagement.Services/             # Servis katmanı
│   └── Interfaces/                          # Servis arayüzleri
│
└── ResidenceManagement.Tests/                # Test katmanı
    ├── UnitTests/                           # Birim testleri
    └── IntegrationTests/                    # Entegrasyon testleri
```

## Klasör Açıklamaları

### Core Katmanı
- **Common**: Proje genelinde kullanılan ortak yapılar, yardımcı sınıflar ve sabitler
- **Configurations**: Entity ve diğer yapılandırma sınıfları
- **DTOs**: Veri transfer nesneleri, API ve servisler arası veri alışverişi için kullanılan modeller
- **Entities**: Veritabanı tablolarını temsil eden entity sınıfları
- **Enums**: Sabit değerler ve enum tanımlamaları
- **Exceptions**: Özel hata sınıfları ve hata yönetimi
- **Filters**: Filtreleme mekanizmaları ve özel filtreler
- **Helpers**: Yardımcı metodlar ve utility sınıfları
- **Interfaces**: Servis ve repository arayüzleri
- **Models**: İş mantığı modelleri ve domain modelleri
- **Services**: Temel servis tanımlamaları ve arayüzleri

### Infrastructure Katmanı
- **Caching**: Önbellekleme mekanizması ve cache yönetimi
- **Data**: Veritabanı işlemleri ve repository implementasyonları
  - **Configurations**: Entity konfigürasyonları
  - **Repositories**: Repository implementasyonları
  - **Context**: DbContext ve migration dosyaları
- **Email**: E-posta gönderim servisi ve şablonları
- **ExternalServices**: Dış servis entegrasyonları (ödeme, SMS, vb.)
- **Logging**: Loglama mekanizması ve log yönetimi
- **SMS**: SMS gönderim servisi ve şablonları
- **Services**: Servis implementasyonları

### API Katmanı
- **Controllers**: API endpoint'leri ve controller sınıfları
- **Configurations**: API yapılandırmaları ve ayarları
- **DTOs**: API'ye özel veri transfer nesneleri
- **Extensions**: Genişletme metodları ve helper fonksiyonlar
- **Filters**: API filtreleri ve middleware'ler
- **Middlewares**: Ara yazılımlar ve request/response işlemleri
- **Properties**: API özellikleri ve yapılandırmaları

### Services Katmanı
- **Interfaces**: Servis arayüzleri ve sözleşmeleri

### Tests Katmanı
- **UnitTests**: Birim testleri ve test sınıfları
- **IntegrationTests**: Entegrasyon testleri ve test senaryoları 