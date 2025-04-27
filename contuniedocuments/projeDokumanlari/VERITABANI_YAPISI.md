# VERİTABANI YAPISI

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin veritabanı yapısını, tablolarını ve ilişkilerini detaylı olarak açıklar.

## 1. Veritabanı Teknolojisi ve Yaklaşım

- **Veritabanı Yönetim Sistemi:** Microsoft SQL Server (MSSQL)
- **ORM Teknolojisi:** Entity Framework Core 8
- **Yaklaşım:** Code First
- **Migration Stratejisi:** Otomatik migration ve update-database

## 2. Temel Veri Modelleri (Base Entities)

### 2.1. BaseEntity

Tüm entity'lerin temel aldığı ana sınıftır. Multi-tenant yapısını ve temel izleme alanlarını içerir.

```csharp
public class BaseEntity
{
    public Guid Id { get; set; }
    public int FirmaId { get; set; }
    public int SubeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public Firma Firma { get; set; }
    public Sube Sube { get; set; }
}
```

### 2.2. BaseLookupEntity

Referans tablolarının temel aldığı sınıftır. Kod, ad ve açıklama bilgilerini içerir.

```csharp
public class BaseLookupEntity : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

### 2.3. BaseTransactionEntity

İşlem/hareket tablolarının temel aldığı sınıftır. İşlem tarihi, referans numarası, notlar ve durum bilgilerini içerir.

```csharp
public class BaseTransactionEntity : BaseEntity
{
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string Notes { get; set; }
    public int Status { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? ApprovalDate { get; set; }
}
```

## 3. Multi-tenant Yapısı

### 3.1. Firma (Company)

Sistemde tanımlı firmaları temsil eder.

```csharp
public class Firma
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TaxNumber { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string WebSite { get; set; }
    public string LogoUrl { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public DateTime SubscriptionEndDate { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public ICollection<Sube> Subeler { get; set; }
    public ICollection<User> Users { get; set; }
}
```

### 3.2. Şube (Branch)

Firma altındaki şubeleri temsil eder.

```csharp
public class Sube
{
    public int Id { get; set; }
    public int FirmaId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public Firma Firma { get; set; }
    public ICollection<User> Users { get; set; }
}
```

## 4. Ana Veri Grupları

### 4.1. Mülk Yönetimi (Property Management)

#### 4.1.1. Site (ResidentialComplex)

Rezidans veya siteleri temsil eder.

```csharp
public class Site : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int TotalBlocks { get; set; }
    public int TotalApartments { get; set; }
    public decimal TotalArea { get; set; }
    public DateTime ConstructionDate { get; set; }
    
    // Navigation Properties
    public ICollection<Blok> Bloklar { get; set; }
    public ICollection<Common> CommonAreas { get; set; }
}
```

#### 4.1.2. Blok (Block)

Site içindeki blokları temsil eder.

```csharp
public class Blok : BaseEntity
{
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public int TotalFloors { get; set; }
    public int TotalApartments { get; set; }
    public DateTime ConstructionDate { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public ICollection<Daire> Daireler { get; set; }
}
```

#### 4.1.3. Daire (Apartment)

Blok içindeki daireleri temsil eder.

```csharp
public class Daire : BaseEntity
{
    public Guid BlokId { get; set; }
    public string No { get; set; }
    public int Floor { get; set; }
    public int RoomCount { get; set; }
    public decimal GrossArea { get; set; }
    public decimal NetArea { get; set; }
    public bool IsOccupied { get; set; }
    public int OccupancyType { get; set; } // 1: Owner, 2: Tenant
    
    // Navigation Properties
    public Blok Blok { get; set; }
    public ICollection<DaireSakin> DaireSakinleri { get; set; }
    public ICollection<DaireAidat> DaireAidatlari { get; set; }
}
```

#### 4.1.4. Ortak Alan (CommonArea)

Site içindeki ortak alanları temsil eder.

```csharp
public class OrtakAlan : BaseEntity
{
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Area { get; set; }
    public bool IsReservable { get; set; }
    public decimal ReservationFee { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public ICollection<OrtakAlanRezervasyon> Rezervasyonlar { get; set; }
}
```

### 4.2. Kullanıcı Yönetimi (User Management)

#### 4.2.1. Kullanıcı (User)

Sisteme giriş yapan kullanıcıları temsil eder.

```csharp
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime LastLoginDate { get; set; }
    public bool IsLocked { get; set; }
    public int LoginAttempts { get; set; }
    
    // Navigation Properties
    public ICollection<UserRole> UserRoles { get; set; }
}
```

#### 4.2.2. Rol (Role)

Kullanıcı rollerini temsil eder.

```csharp
public class Role : BaseLookupEntity
{
    // Navigation Properties
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}
```

#### 4.2.3. İzin (Permission)

Sistemdeki yetkileri temsil eder.

```csharp
public class Permission : BaseLookupEntity
{
    public string Module { get; set; } // Örn: Daire, Aidat, Sakin
    public string Action { get; set; } // Örn: Create, Read, Update, Delete
    
    // Navigation Properties
    public ICollection<RolePermission> RolePermissions { get; set; }
}
```

#### 4.2.4. Kullanıcı-Rol İlişkisi (UserRole)

Kullanıcı ve roller arasındaki çoka-çok ilişkiyi temsil eder.

```csharp
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
    public Role Role { get; set; }
}
```

#### 4.2.5. Rol-İzin İlişkisi (RolePermission)

Rol ve izinler arasındaki çoka-çok ilişkiyi temsil eder.

```csharp
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    
    // Navigation Properties
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
```

#### 4.2.6. Sakin (Resident)

Dairelerde yaşayan kişileri temsil eder.

```csharp
public class Sakin : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TCKN { get; set; } // TC Kimlik Numarası
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public string EmergencyContactName { get; set; }
    public string EmergencyContactPhone { get; set; }
    
    // Navigation Properties
    public ICollection<DaireSakin> DaireSakinleri { get; set; }
}
```

#### 4.2.7. Daire-Sakin İlişkisi (ApartmentResident)

Daire ve sakinler arasındaki çoka-çok ilişkiyi temsil eder.

```csharp
public class DaireSakin : BaseEntity
{
    public Guid DaireId { get; set; }
    public Guid SakinId { get; set; }
    public int RelationType { get; set; } // 1: Owner, 2: Tenant, 3: Family Member
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsPrimary { get; set; } // Ana sakin/sorumlu kişi
    
    // Navigation Properties
    public Daire Daire { get; set; }
    public Sakin Sakin { get; set; }
}
```

### 4.3. Finansal İşlemler (Financial Transactions)

#### 4.3.1. Aidat (Dues)

Site/rezidans aidatlarını temsil eder.

```csharp
public class Aidat : BaseTransactionEntity
{
    public Guid SiteId { get; set; }
    public string Period { get; set; } // Örn: 2023-01
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public ICollection<DaireAidat> DaireAidatlari { get; set; }
}
```

#### 4.3.2. Daire Aidat (ApartmentDues)

Dairelere atanan aidatları temsil eder.

```csharp
public class DaireAidat : BaseTransactionEntity
{
    public Guid DaireId { get; set; }
    public Guid AidatId { get; set; }
    public decimal Amount { get; set; } // Dairenin özel aidat tutarı
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentReference { get; set; }
    
    // Navigation Properties
    public Daire Daire { get; set; }
    public Aidat Aidat { get; set; }
}
```

#### 4.3.3. Gider (Expense)

Site/rezidans giderlerini temsil eder.

```csharp
public class Gider : BaseTransactionEntity
{
    public Guid SiteId { get; set; }
    public Guid? GiderKategoriId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string InvoiceNumber { get; set; }
    public string VendorName { get; set; }
    public string PaymentMethod { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public GiderKategori GiderKategori { get; set; }
}
```

#### 4.3.4. Gider Kategorisi (ExpenseCategory)

Gider kategorilerini temsil eder.

```csharp
public class GiderKategori : BaseLookupEntity
{
    // Navigation Properties
    public ICollection<Gider> Giderler { get; set; }
}
```

### 4.4. Rezervasyon ve Etkinlik Yönetimi (Reservation and Event Management)

#### 4.4.1. Ortak Alan Rezervasyonu (CommonAreaReservation)

Ortak alanlar için yapılan rezervasyonları temsil eder.

```csharp
public class OrtakAlanRezervasyon : BaseTransactionEntity
{
    public Guid OrtakAlanId { get; set; }
    public Guid SakinId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Purpose { get; set; }
    public int AttendeeCount { get; set; }
    public decimal Fee { get; set; }
    public bool IsPaid { get; set; }
    
    // Navigation Properties
    public OrtakAlan OrtakAlan { get; set; }
    public Sakin Sakin { get; set; }
}
```

#### 4.4.2. Etkinlik (Event)

Site/rezidans etkinliklerini temsil eder.

```csharp
public class Etkinlik : BaseTransactionEntity
{
    public Guid SiteId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public int MaxAttendees { get; set; }
    public bool IsPublic { get; set; }
    public decimal Fee { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public ICollection<EtkinlikKatilimci> Katilimcilar { get; set; }
}
```

#### 4.4.3. Etkinlik Katılımcısı (EventParticipant)

Etkinlik katılımcılarını temsil eder.

```csharp
public class EtkinlikKatilimci : BaseEntity
{
    public Guid EtkinlikId { get; set; }
    public Guid SakinId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool HasAttended { get; set; }
    public decimal PaidAmount { get; set; }
    
    // Navigation Properties
    public Etkinlik Etkinlik { get; set; }
    public Sakin Sakin { get; set; }
}
```

### 4.5. Güvenlik ve Erişim Yönetimi (Security and Access Management)

#### 4.5.1. Erişim Kartı (AccessCard)

Erişim kartlarını temsil eder.

```csharp
public class ErisimKarti : BaseEntity
{
    public Guid SakinId { get; set; }
    public string CardNumber { get; set; }
    public string CardType { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public Sakin Sakin { get; set; }
    public ICollection<ErisimKartiIzin> Izinler { get; set; }
    public ICollection<ErisimLogu> ErisimLoglari { get; set; }
}
```

#### 4.5.2. Erişim Kartı İzni (AccessCardPermission)

Erişim kartlarının izinlerini temsil eder.

```csharp
public class ErisimKartiIzin : BaseEntity
{
    public Guid ErisimKartiId { get; set; }
    public Guid ErisimNoktasiId { get; set; }
    public TimeSpan? StartTime { get; set; } // Günlük başlangıç saati
    public TimeSpan? EndTime { get; set; } // Günlük bitiş saati
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    
    // Navigation Properties
    public ErisimKarti ErisimKarti { get; set; }
    public ErisimNoktasi ErisimNoktasi { get; set; }
}
```

#### 4.5.3. Erişim Noktası (AccessPoint)

Erişim noktalarını temsil eder.

```csharp
public class ErisimNoktasi : BaseEntity
{
    public Guid SiteId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DeviceId { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public Site Site { get; set; }
    public ICollection<ErisimKartiIzin> Izinler { get; set; }
    public ICollection<ErisimLogu> ErisimLoglari { get; set; }
}
```

#### 4.5.4. Erişim Logu (AccessLog)

Erişim loglarını temsil eder.

```csharp
public class ErisimLogu : BaseEntity
{
    public Guid ErisimNoktasiId { get; set; }
    public Guid? ErisimKartiId { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsGranted { get; set; }
    public string DenialReason { get; set; }
    
    // Navigation Properties
    public ErisimNoktasi ErisimNoktasi { get; set; }
    public ErisimKarti ErisimKarti { get; set; }
}
```

### 4.6. Bildirim Yönetimi (Notification Management)

#### 4.6.1. Bildirim (Notification)

Kullanıcılara gönderilen bildirimleri temsil eder.

```csharp
public class Bildirim : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? SakinId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string NotificationType { get; set; } // Email, SMS, Push, InApp
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }
    public string RedirectUrl { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
    public Sakin Sakin { get; set; }
}
```

#### 4.6.2. Bildirim Şablonu (NotificationTemplate)

Bildirim şablonlarını temsil eder.

```csharp
public class BildirimSablonu : BaseEntity
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string NotificationType { get; set; } // Email, SMS, Push, InApp
    public bool IsActive { get; set; }
    
    // Navigation Properties
}
```

#### 4.6.3. Bildirim Ayarı (NotificationSetting)

Kullanıcı bildirim ayarlarını temsil eder.

```csharp
public class BildirimAyari : BaseEntity
{
    public Guid UserId { get; set; }
    public bool EmailEnabled { get; set; }
    public bool SmsEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public bool InAppEnabled { get; set; }
    public bool DuesNotification { get; set; }
    public bool PaymentNotification { get; set; }
    public bool EventNotification { get; set; }
    public bool MaintenanceNotification { get; set; }
    public bool AnnouncementNotification { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
}
```

### 4.7. Performans İzleme (Performance Monitoring)

#### 4.7.1. Performans Metriği (PerformanceMetric)

Sistem performans metriklerini temsil eder.

```csharp
public class PerformansMetrigi : BaseEntity
{
    public string Name { get; set; }
    public string Category { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; }
    public string Tags { get; set; }
}
```

#### 4.7.2. API Kullanım Metriği (APIUsageMetric)

API kullanım metriklerini temsil eder.

```csharp
public class ApiKullanimMetrigi : BaseEntity
{
    public Guid? UserId { get; set; }
    public string Endpoint { get; set; }
    public string HttpMethod { get; set; }
    public DateTime Timestamp { get; set; }
    public int StatusCode { get; set; }
    public long ResponseTimeMs { get; set; }
    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
}
```

## 5. Veritabanı İlişkileri ve Örnek Diyagram

Veritabanı ilişkileri, Entity Framework Core Code First yaklaşımı ile aşağıdaki prensipler doğrultusunda kurulacaktır:

1. **Bire-Bir İlişkiler (One-to-One)**:
   - Kullanıcı ve Bildirim Ayarı arasında bire-bir ilişki

2. **Bire-Çok İlişkiler (One-to-Many)**:
   - Firma ve Şube arasında bire-çok ilişki
   - Site ve Blok arasında bire-çok ilişki
   - Blok ve Daire arasında bire-çok ilişki
   - Site ve Ortak Alan arasında bire-çok ilişki
   - Site ve Gider arasında bire-çok ilişki

3. **Çoka-Çok İlişkiler (Many-to-Many)**:
   - Kullanıcı ve Rol arasında çoka-çok ilişki (UserRole ara tablosu üzerinden)
   - Rol ve İzin arasında çoka-çok ilişki (RolePermission ara tablosu üzerinden)
   - Daire ve Sakin arasında çoka-çok ilişki (DaireSakin ara tablosu üzerinden)

## 6. İndeksler ve Performans Optimizasyonları

Veritabanı performansını artırmak için aşağıdaki indeksler oluşturulacaktır:

1. **Birincil Anahtar İndeksleri (Primary Key Indexes)**:
   - Tüm tablolar için otomatik oluşturulur

2. **Yabancı Anahtar İndeksleri (Foreign Key Indexes)**:
   - Tüm yabancı anahtar alanları için indeksler

3. **Özel İndeksler (Custom Indexes)**:
   - FirmaId ve SubeId alanları için bileşik indeks
   - Sık kullanılan filtreleme alanları için indeksler (IsActive, IsDeleted)
   - Tarih alanları için indeksler (CreatedDate, TransactionDate, StartDate, EndDate)
   - Arama yapılan alanlar için indeksler (Name, Code, Email, PhoneNumber)

4. **Multi-tenant Optimizasyonları**:
   - FirmaId ve SubeId için sınırlandırılmış indeksler (filtered indexes)
   - Tenant bazlı partition oluşturma

## 7. Veri Bütünlüğü ve Kısıtlamalar

Veritabanı veri bütünlüğünü sağlamak için aşağıdaki kısıtlamalar uygulanacaktır:

1. **Referans Bütünlüğü (Referential Integrity)**:
   - Foreign key kısıtlamaları
   - Cascade Delete/Update yapılandırması

2. **Zorunlu Alanlar (Required Fields)**:
   - NOT NULL kısıtlamaları
   - Validation attributes ve fluent validation

3. **Benzersiz Kısıtlamalar (Unique Constraints)**:
   - Benzersiz olması gereken alanlar için unique index

4. **Check Kısıtlamaları (Check Constraints)**:
   - Tarih alanları için mantıksal kontroller (StartDate < EndDate)
   - Sayısal alanlar için değer aralığı kontrolleri (Amount > 0)

## 8. Örnek Seed Verileri

Veritabanı oluşturulduktan sonra test ve geliştirme için aşağıdaki seed verileri yüklenecektir:

1. **Firmalar ve Şubeler**:
   - Demo Firma ve şubeleri
   - Test Firma ve şubeleri

2. **Kullanıcılar ve Roller**:
   - Admin, FirmaYöneticisi, SiteYöneticisi, Muhasebe, Teknik, Danışma rolleri
   - Her rol için demo kullanıcılar

3. **Referans Verileri**:
   - Gider kategorileri
   - Daire tipleri
   - Ödeme yöntemleri

4. **Demo Site ve Daireler**:
   - Örnek site yapısı
   - Bloklar, daireler ve sakinler

Bu veritabanı tasarımı, multi-tenant yapısı göz önünde bulundurularak, yüksek performans ve veri bütünlüğü sağlayacak şekilde oluşturulmuştur.

## 9. Veritabanı Migration Stratejisi

Veritabanı şeması değişikliklerinin yönetimi için aşağıdaki stratejiler kullanılacaktır:

1. **Migration Oluşturma**:
   - Her önemli değişiklik için isimlendirilmiş migration oluşturma
   - Migration açıklamalarını detaylı tutma

2. **Migration Uygulama**:
   - Geliştirme ortamında otomatik migration
   - Test ve üretim ortamlarında kontrollü migration

3. **Rollback Stratejisi**:
   - Her migration için geri alma planı
   - Kritik değişiklikler için veritabanı yedekleme

4. **Version Kontrolü**:
   - Migration dosyalarının version kontrolüne dahil edilmesi
   - Migration geçmişinin dokümante edilmesi

Bu veritabanı yapısı, Rezidans ve Site Yönetim Sistemi projesinin tüm gereksinimlerini karşılayacak şekilde tasarlanmıştır. Proje geliştirme sürecinde, ihtiyaçlara göre güncellenebilir ve genişletilebilir. 