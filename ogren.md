# Mekik Residence Manager Proje Analizi

## Proje Genel Yapısı

Mekik Residence Manager, modern konut kompleksleri ve rezidansların yönetimini dijitalleştiren kapsamlı bir yazılım çözümüdür. Proje, Clean Architecture prensiplerine uygun olarak tasarlanmış ve çeşitli katmanlardan oluşmaktadır.

### Proje Katmanları

1. **ResidenceManagement.Core**:
   - Domain modelleri, entity'ler, DTO'lar, interfaces ve business logic'i içerir
   - Diğer katmanlara bağımlılığı yoktur
   - Projenin merkezi ve en önemli katmanıdır

2. **ResidenceManagement.Infrastructure**:
   - Veritabanı işlemleri, harici servisler, caching, logging gibi altyapı işlemlerini içerir
   - Entity Framework Core implementasyonu burada bulunur
   - Core katmanındaki interface'lerin implementasyonlarını içerir

3. **ResidenceManagement.API**:
   - RESTful API endpoint'leri ve controller'ları içerir
   - Frontend ile iletişimi sağlar
   - Kullanıcı isteklerini karşılar ve uygun servislere yönlendirir

4. **ResidenceManagement.Tests**:
   - Birim testleri ve entegrasyon testlerini içerir

### Frontend Yapısı

Frontend, React.js kullanılarak geliştirilmiş ve Atomic Design prensipleri uygulanmıştır:

1. **Atoms**: Buton, input, icon gibi en temel UI bileşenleri
2. **Molecules**: Form field, card, dialog gibi atomlardan oluşan bileşenler
3. **Organisms**: Form, navbar, sidebar gibi daha karmaşık bileşenler
4. **Templates**: Sayfa düzenleri ve layout'lar
5. **Pages**: Kullanıcının etkileşimde bulunduğu tam sayfalar

## Veritabanı Yapısı

Proje, Entity Framework Core ORM kullanarak veritabanı işlemlerini gerçekleştirir. Multi-tenant bir yapıya sahiptir, yani aynı veritabanı üzerinde farklı şirketler ve şubeler için veri izolasyonu sağlanmıştır.

### Temel Entity Yapısı

Tüm entity'ler `BaseEntity` sınıfından türetilmiştir:

```csharp
public abstract class BaseEntity : ITenant
{
    public virtual int Id { get; set; }
    public virtual int CompanyId { get; set; }
    public virtual int BranchId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int? DeletedBy { get; set; }
    public bool IsActive { get; set; }
}
```

### Multi-Tenant Yapı

Veritabanı sorguları otomatik olarak `CompanyId` ve `BranchId` alanlarına göre filtrelenir. Bu, `ApplicationDbContext` içinde global query filter'lar kullanılarak sağlanır.

## Temel Domain Modelleri

Proje, aşağıdaki ana domain alanlarını içerir:

1. **Property Management**:
   - Block (Bina/Blok)
   - Apartment (Daire)
   - Unit (Birim)
   - Common Areas (Ortak Alanlar)

2. **Resident Management**:
   - User (Kullanıcı)
   - Resident (Sakin)
   - Owner (Malik)
   - Tenant (Kiracı)

3. **Financial Management**:
   - Invoice (Fatura)
   - Payment (Ödeme)
   - Dues (Aidat)
   - Expense (Gider)

4. **Maintenance Management**:
   - ServiceRequest (Hizmet Talebi)
   - MaintenanceSchedule (Bakım Programı)
   - Equipment (Ekipman)

5. **Communication**:
   - Notification (Bildirim)
   - Email (E-posta)
   - SMS

6. **Security & Access Control**:
   - Role (Rol)
   - Permission (İzin)
   - Authentication (Kimlik Doğrulama)

## Önemli Teknik Özellikler

1. **Clean Architecture**: Domain-driven design ve bağımlılık yönü prensiplerine uygun tasarım
2. **Repository Pattern**: Veri erişim katmanı için repository pattern kullanımı
3. **Unit of Work**: Veritabanı işlemlerinin bütünlüğünü sağlamak için Unit of Work pattern
4. **CQRS Pattern**: Command Query Responsibility Segregation yaklaşımı
5. **Multi-Tenant**: Şirket ve şube bazlı veri izolasyonu
6. **RESTful API**: HTTP protokolü üzerinden RESTful API
7. **JWT Authentication**: JSON Web Token tabanlı kimlik doğrulama
8. **Role-Based Access Control**: Rol tabanlı yetkilendirme sistemi

## Entegrasyonlar

1. **KBS (Kimlik Bildirim Sistemi)**: Resmi kimlik bildirim sistemi entegrasyonu
2. **SMS Gateway**: SMS gönderimi için harici servis entegrasyonu
3. **Email Service**: E-posta gönderimi için SMTP entegrasyonu
4. **File Storage**: Dosya depolama için harici servis entegrasyonu

## Proje Dosya Yapısı

### Backend

```
ResidenceManagement.Core/
├── Common/
├── DTOs/
├── Entities/
│   ├── Base/
│   ├── Common/
│   ├── Financial/
│   ├── Identity/
│   ├── Property/
│   └── ...
├── Interfaces/
├── Models/
└── Services/

ResidenceManagement.Infrastructure/
├── Data/
│   ├── Context/
│   ├── Repositories/
│   └── Migrations/
├── Services/
└── ...

ResidenceManagement.API/
├── Controllers/
├── Extensions/
├── Middlewares/
└── ...
```

### Frontend

```
src/
├── api/
├── components/
│   ├── atoms/
│   ├── molecules/
│   ├── organisms/
│   ├── templates/
│   └── pages/
├── hooks/
├── services/
├── store/
└── utils/
```

## Sonuç

Mekik Residence Manager, modern yazılım mimarisi prensiplerini uygulayan, kapsamlı ve ölçeklenebilir bir site yönetim sistemidir. Multi-tenant yapısı sayesinde birden fazla site ve şirket için kullanılabilir, modüler yapısı sayesinde yeni özellikler kolayca eklenebilir.