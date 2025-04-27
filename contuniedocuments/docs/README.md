# Rezidans ve Site Yönetim Sistemi

Bu proje, rezidans ve site yönetimlerinin günlük operasyonlarını yönetmek için geliştirilmiş kapsamlı bir yönetim sistemidir.

## Proje Hakkında

Rezidans ve Site Yönetim Sistemi, site ve rezidans yönetim şirketlerinin tüm iş süreçlerini yönetmelerini sağlayan çok kiracılı (multi-tenant) bir yazılım çözümüdür. Sistem, daire yönetimi, aidat takibi, gider yönetimi, rezervasyon işlemleri, teknik servis hizmetleri ve sakin bilgileri gibi birçok modülü içermektedir.

## Projede Kullanılan Teknolojiler

### Backend
- .NET 8 Web API (C#)
- Entity Framework Core 8
- SQL Server
- JWT Authentication
- Repository ve Unit of Work Pattern
- Multi-tenant Mimari

### Frontend (Geliştirilme Aşamasında)
- React.js
- Material UI
- Context API
- Axios

### Mobil (Planlanan)
- React Native
- React Navigation

## Proje Yapısı

```
ResidenceManagement/
│
├── ResidenceManagement.API           # API Katmanı
│   ├── Controllers                   # API Controller'ları
│   ├── Middlewares                   # Custom Middleware'ler
│   ├── Extensions                    # Extension Metodları
│   └── Configurations                # API Yapılandırmaları
│
├── ResidenceManagement.Core          # Çekirdek Katmanı
│   ├── Entities                      # Veri Modelleri
│   ├── Interfaces                    # Arayüzler
│   ├── Enums                         # Enumlar
│   ├── DTOs                          # Data Transfer Objects
│   └── Exceptions                    # Özel Exception'lar
│
├── ResidenceManagement.Infrastructure # Altyapı Katmanı
│   ├── Data                          # Veritabanı İşlemleri
│   ├── Services                      # Servis İmplementasyonları
│   ├── Extensions                    # Extension Metodları
│   └── Utilities                     # Yardımcı Metotlar
│
├── ResidenceManagement.Services      # İş Mantığı Katmanı
│   ├── Services                      # Servisler
│   └── Validators                    # Veri Doğrulayıcılar
│
└── ResidenceManagement.Tests         # Test Projeleri
    ├── Unit                          # Birim Testler
    └── Integration                   # Entegrasyon Testleri
```

## Belgeler

Proje hakkında detaylı bilgiler içeren dökümantasyon dosyaları:

| Dosya | Açıklama |
|------|----------|
| [CHANGELOG.md](./CHANGELOG.md) | Değişiklik kayıtları |
| [MULTITENANCY_MIMARISI.md](./MULTITENANCY_MIMARISI.md) | Multi-tenant mimari açıklaması |
| [backend-model-yapisi.md](./backend-model-yapisi.md) | Backend model yapısı |
| [veritabani-semasi.md](./veritabani-semasi.md) | Veritabanı şeması |
| [teknik-mimarisi-continuation.md](./teknik-mimarisi-continuation.md) | Teknik mimari detayları |

## Tamamlanan Özellikler

- Multi-tenant mimari altyapısı (firma ve şube bazlı veri izolasyonu)
- Temel varlık yapıları (BaseEntity, BaseLookupEntity, vb.)
- Veri modelleri (Apartment, Block, Residence, vb.)
- Repository pattern implementasyonu
- Multi-tenant middleware implementasyonu
- Temel kontroller yapıları
- KBS (Kimlik Bildirme Sistemi) entegrasyonu
- Servis istek ve rapor yönetimi
- Envanter yönetimi
- Çoklu dil ve para birimi desteği

## Geliştirme Aşamasındaki Özellikler

- Frontend web uygulaması
- Mobil uygulama
- Email ve SMS bildirimleri
- Otomatik raporlama sistemi
- Dashboard ve analitik özellikler
- Online ödeme entegrasyonu

## Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server 2019 veya üzeri
- Node.js ve npm (Frontend geliştirmesi için)

### Backend Kurulum
1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/organizasyon/rezidans-yonetim.git
   ```

2. Projeyi restore edin:
   ```bash
   cd rezidans-yonetim/backend
   dotnet restore
   ```

3. Veritabanını oluşturun:
   ```bash
   cd ResidenceManagement.Infrastructure
   dotnet ef database update
   ```

4. Uygulamayı çalıştırın:
   ```bash
   cd ../ResidenceManagement.API
   dotnet run
   ```

## Lisans

Bu proje özel lisans altındadır ve tüm hakları saklıdır. 