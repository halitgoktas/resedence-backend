# Residence Management Projesi - Frontend Klasör Yapısı

## Klasör Ağacı

```
src/
├── assets/                    # Statik dosyalar
│   ├── images/               # Görseller
│   ├── icons/                # İkonlar
│   ├── fonts/                # Fontlar
│   └── styles/               # Global stiller
│
├── components/               # Yeniden kullanılabilir komponentler
│   ├── common/              # Genel komponentler
│   │   ├── Button/
│   │   ├── Input/
│   │   ├── Select/
│   │   ├── Table/
│   │   └── Modal/
│   │
│   ├── layout/              # Layout komponentleri
│   │   ├── Header/
│   │   ├── Sidebar/
│   │   ├── Footer/
│   │   └── Dashboard/
│   │
│   └── features/            # Özellik bazlı komponentler
│       ├── auth/
│       ├── dashboard/
│       ├── residents/
│       └── payments/
│
├── config/                   # Konfigürasyon dosyaları
│   ├── routes.ts            # Route tanımlamaları
│   ├── api.ts               # API konfigürasyonu
│   └── theme.ts             # Tema konfigürasyonu
│
├── hooks/                    # Custom hooks
│   ├── useAuth.ts
│   ├── useForm.ts
│   └── useApi.ts
│
├── pages/                    # Sayfa komponentleri
│   ├── auth/
│   │   ├── Login/
│   │   └── Register/
│   │
│   ├── dashboard/
│   │   ├── Overview/
│   │   └── Analytics/
│   │
│   ├── residents/
│   │   ├── List/
│   │   └── Details/
│   │
│   └── payments/
│       ├── List/
│       └── Create/
│
├── services/                 # API servisleri
│   ├── api.ts               # API client
│   ├── auth.service.ts
│   ├── resident.service.ts
│   └── payment.service.ts
│
├── store/                    # State yönetimi
│   ├── slices/              # Redux slices
│   │   ├── auth.slice.ts
│   │   ├── resident.slice.ts
│   │   └── payment.slice.ts
│   │
│   └── index.ts             # Store konfigürasyonu
│
├── types/                    # TypeScript tipleri
│   ├── api.types.ts
│   ├── auth.types.ts
│   └── common.types.ts
│
├── utils/                    # Yardımcı fonksiyonlar
│   ├── formatters.ts
│   ├── validators.ts
│   └── helpers.ts
│
├── App.tsx                   # Ana uygulama komponenti
├── main.tsx                  # Uygulama giriş noktası
└── vite-env.d.ts            # Vite tip tanımlamaları
```

## Klasör Açıklamaları

### assets/
- **images/**: Proje genelinde kullanılan görseller
- **icons/**: SVG ve diğer ikon dosyaları
- **fonts/**: Özel font dosyaları
- **styles/**: Global CSS ve tema dosyaları

### components/
- **common/**: Tüm uygulamada kullanılan temel komponentler
  - Button: Özelleştirilebilir buton komponenti
  - Input: Form input komponenti
  - Select: Dropdown komponenti
  - Table: Veri tablosu komponenti
  - Modal: Dialog komponenti

- **layout/**: Sayfa düzeni komponentleri
  - Header: Üst menü
  - Sidebar: Yan menü
  - Footer: Alt bilgi
  - Dashboard: Dashboard layout

- **features/**: Özellik bazlı komponentler
  - auth: Kimlik doğrulama komponentleri
  - dashboard: Dashboard komponentleri
  - residents: Sakin yönetimi komponentleri
  - payments: Ödeme komponentleri

### config/
- **routes.ts**: Sayfa yönlendirme tanımlamaları
- **api.ts**: API endpoint ve konfigürasyonları
- **theme.ts**: UI tema ayarları

### hooks/
- **useAuth.ts**: Kimlik doğrulama hook'u
- **useForm.ts**: Form yönetimi hook'u
- **useApi.ts**: API çağrıları hook'u

### pages/
- **auth/**: Kimlik doğrulama sayfaları
  - Login: Giriş sayfası
  - Register: Kayıt sayfası

- **dashboard/**: Dashboard sayfaları
  - Overview: Genel bakış
  - Analytics: Analiz sayfası

- **residents/**: Sakin yönetimi sayfaları
  - List: Sakin listesi
  - Details: Sakin detayları

- **payments/**: Ödeme sayfaları
  - List: Ödeme listesi
  - Create: Ödeme oluşturma

### services/
- **api.ts**: API client konfigürasyonu
- **auth.service.ts**: Kimlik doğrulama servisi
- **resident.service.ts**: Sakin yönetimi servisi
- **payment.service.ts**: Ödeme servisi

### store/
- **slices/**: Redux state yönetimi
  - auth.slice.ts: Kimlik doğrulama state'i
  - resident.slice.ts: Sakin yönetimi state'i
  - payment.slice.ts: Ödeme state'i

### types/
- **api.types.ts**: API tip tanımlamaları
- **auth.types.ts**: Kimlik doğrulama tipleri
- **common.types.ts**: Genel tip tanımlamaları

### utils/
- **formatters.ts**: Veri formatlama fonksiyonları
- **validators.ts**: Doğrulama fonksiyonları
- **helpers.ts**: Yardımcı fonksiyonlar 