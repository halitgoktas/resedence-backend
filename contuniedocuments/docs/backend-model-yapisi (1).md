# Rezidans ve Site Yönetim Sistemi - Backend Model Yapısı

Bu döküman, Rezidans ve Site Yönetim Sistemi'nin backend model yapısını ve ilişkilerini detaylı olarak açıklamaktadır.

## Döküman Bağlantıları
- TEKNIK_MIMARISI.md - Projenin teknik mimari yapısı
- VERITABANI_SEMASI.md - Veritabanı şeması ve tablolar

## Sunucu Bilgileri
- Sunucu: 127.0.0.1
- SQL Kullanıcı Adı: sa
- SQL Şifre: q.1

## 1. Temel Model Yapısı

Sistem, aşağıdaki temel model sınıflarını içerir:

### 1.1. Temel Sınıflar

- **BaseEntity**: Tüm modellerin temel sınıfıdır. Aşağıdaki ortak özellikleri içerir:
  - Id: Benzersiz tanımlayıcı
  - FirmaId: Multi-tenant yapısı için firma ID'si
  - SubeId: Multi-tenant yapısı için şube ID'si
  - CreatedDate: Oluşturulma tarihi
  - UpdatedDate: Güncellenme tarihi
  - CreatedBy: Oluşturan kullanıcı ID'si
  - UpdatedBy: Güncelleyen kullanıcı ID'si
  - IsDeleted: Soft delete durumu
  - IsActive: Aktif durumu

- **BaseLookupEntity**: Referans verileri için kullanılan temel sınıf (BaseEntity'den türetilmiştir)
  - Name: İsim
  - Code: Kod
  - DisplayOrder: Görüntülenme sırası
  - Description: Açıklama
  - IsDefault: Varsayılan mı?

- **BaseTransactionEntity**: İşlem kayıtları için kullanılan temel sınıf (BaseEntity'den türetilmiştir)
  - TransactionDate: İşlem tarihi
  - ReferenceNumber: Referans numarası
  - ProcessedBy: İşlemi yapan kullanıcı ID'si
  - Notes: Notlar
  - Status: Durum
  - StatusDate: Durum tarihi
  - RelatedEntityId: İlişkili kayıt ID'si
  - RelatedEntityType: İlişkili kayıt tipi
  - Amount: Tutar
  - ApprovedBy: Onaylayan kullanıcı ID'si
  - ApprovalDate: Onay tarihi

- **BaseSystemEntity**: Sistem genelinde kullanılan ve tenant bilgisi gerektirmeyen tablolar için temel sınıf

## 2. Bina ve Daire Modelleri

### 2.1. Site Modeli (Site.cs)

Site modeli, bir konut sitesini temsil eder.

```csharp
public class Site : BaseLookupEntity
{
    public string SiteName { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostalCode { get; set; }
    public int TotalBlocks { get; set; }
    public int TotalApartments { get; set; }
    public decimal TotalArea { get; set; }
    public decimal GreenArea { get; set; }
    public int ParkingCapacity { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public int? SiteManagerId { get; set; }
    public int? ManagementCompanyId { get; set; }
    public SiteStatus Status { get; set; }
    public string Amenities { get; set; } // JSON
    public DuesCalculationType DuesCalculationType { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Block> Blocks { get; set; }
    public virtual SiteManager SiteManager { get; set; }
}
```

### 2.2. Blok Modeli (Block.cs)

Blok modeli, site içerisindeki blokları temsil eder.

```csharp
public class Block : BaseLookupEntity
{
    public int SiteId { get; set; }
    public string BlockName { get; set; }
    public string Description { get; set; }
    public int TotalFloors { get; set; }
    public int TotalApartments { get; set; }
    public int ApartmentsPerFloor { get; set; }
    public decimal BuildingArea { get; set; }
    public int ConstructionYear { get; set; }
    public BlockType BlockType { get; set; }
    public int? BlockManagerId { get; set; }
    public int NumberOfElevators { get; set; }
    public string HeatingSystem { get; set; }
    
    // Navigation Properties
    public virtual Site Site { get; set; }
    public virtual ICollection<Apartment> Apartments { get; set; }
}
```

### 2.3. Daire Modeli (Apartment.cs)

Daire modeli, bir konut birimini temsil eder.

```csharp
public class Apartment : BaseEntity
{
    public int BlockId { get; set; }
    public string ApartmentNumber { get; set; }
    public int Floor { get; set; }
    public string ApartmentType { get; set; } // 1+1, 2+1, vb.
    public decimal GrossArea { get; set; }
    public decimal NetArea { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfLivingRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public int NumberOfBalconies { get; set; }
    public ApartmentStatus Status { get; set; }
    public OwnershipType OwnershipType { get; set; }
    public int? OwnerId { get; set; }
    public int? TenantId { get; set; }
    public string Description { get; set; }
    public decimal DuesAmount { get; set; }
    public DateTime? LastDuesPaymentDate { get; set; }
    public decimal DuesCoefficient { get; set; }
    public string HeatingType { get; set; }
    public string Notes { get; set; }
    
    // Navigation Properties
    public virtual Block Block { get; set; }
    public virtual ICollection<ApartmentOwner> Owners { get; set; }
    public virtual ICollection<ApartmentResident> Residents { get; set; }
}
```

### 2.4. Daire Sahibi Modeli (ApartmentOwner.cs)

Daire sahibi modeli, daire sahipliği bilgilerini temsil eder.

```csharp
public class ApartmentOwner : BaseTransactionEntity
{
    public int ApartmentId { get; set; }
    public int UserId { get; set; }
    public decimal OwnershipPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string DeedInformation { get; set; }
    
    // Navigation Properties
    public virtual Apartment Apartment { get; set; }
    public virtual User User { get; set; }
}
```

### 2.5. Daire Sakini Modeli (ApartmentResident.cs)

Daire sakini modeli, dairede ikamet eden kişi bilgilerini temsil eder.

```csharp
public class ApartmentResident : BaseTransactionEntity
{
    public int ApartmentId { get; set; }
    public int UserId { get; set; }
    public ResidenceType ResidenceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int NumberOfResidents { get; set; }
    public decimal? MonthlyRent { get; set; }
    public string ContractReference { get; set; }
    
    // Navigation Properties
    public virtual Apartment Apartment { get; set; }
    public virtual User User { get; set; }
}
```

## 3. Kullanıcı ve Yetkilendirme Modelleri

### 3.1. Kullanıcı Modeli (User.cs)

Kullanıcı modeli, sistem kullanıcılarını temsil eder.

```csharp
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string PreferredLanguage { get; set; } // tr, en, de, ar, ru, fa
    public string PreferredCurrency { get; set; } // TRY, USD, EUR, GBP
    public bool TwoFactorEnabled { get; set; }
    public string TwoFactorKey { get; set; }
    
    // Navigation Properties
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<ApartmentOwner> OwnedApartments { get; set; }
    public virtual ICollection<ApartmentResident> ResidedApartments { get; set; }
}
```

### 3.2. Rol Modeli (Role.cs)

Rol modeli, sistem içerisindeki rolleri tanımlar.

```csharp
public class Role : BaseLookupEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public bool IsSystemDefined { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
```

### 3.3. Kullanıcı Rolü Modeli (UserRole.cs)

Kullanıcı ve rol arasındaki ilişkiyi tanımlayan model.

```csharp
public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}
```

### 3.4. İzin Modeli (Permission.cs)

İzin modeli, sistem izinlerini tanımlar.

```csharp
public class Permission : BaseLookupEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
```

### 3.5. Rol İzin Modeli (RolePermission.cs)

Rol ve izin arasındaki ilişkiyi tanımlayan model.

```csharp
public class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    
    // Navigation Properties
    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
}
```

## 4. Finansal Modeller

### 4.1. Aidat Modeli (Dues.cs)

Aidat modeli, dairelere ait aidat bilgilerini temsil eder.

```csharp
public class Dues : BaseTransactionEntity
{
    public int ApartmentId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public int? PaymentId { get; set; }
    
    // Navigation Properties
    public virtual Apartment Apartment { get; set; }
    public virtual Payment Payment { get; set; }
}
```

### 4.2. Ödeme Modeli (Payment.cs)

Ödeme modeli, yapılan ödemeleri temsil eder.

```csharp
public class Payment : BaseTransactionEntity
{
    public int UserId { get; set; }
    public int? ApartmentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public decimal ExchangeRate { get; set; }
    public decimal BaseAmount { get; set; } // Ana para birimi cinsinden
    public string BaseCurrency { get; set; }
    public string PaymentType { get; set; } // Dues, Rent, Service, ActivityArea
    public string PaymentMethod { get; set; } // Cash, CreditCard, BankTransfer
    public string Status { get; set; } // Pending, Completed, Failed, Refunded
    public int? RelatedEntityId { get; set; }
    public string RelatedEntityType { get; set; }
    public string ReceiptNumber { get; set; }
    public string ReceiptPath { get; set; }
    public int? ProcessedByUserId { get; set; }
    public DateTime PaymentDate { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Apartment Apartment { get; set; }
}
```

### 4.3. Gelir-Gider Modeli (IncomeExpense.cs)

Gelir-gider modeli, finansal işlemleri temsil eder.

```csharp
public class IncomeExpense : BaseTransactionEntity
{
    public string Type { get; set; } // Income, Expense
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public decimal ExchangeRate { get; set; }
    public decimal BaseAmount { get; set; } // Ana para birimi cinsinden
    public string BaseCurrency { get; set; }
    public string Description { get; set; }
    public int? SourceId { get; set; }
    public string SourceType { get; set; } // Dues, Rent, Service, ActivityArea, Other
    public int? PaymentId { get; set; }
    public string PaymentMethod { get; set; } // Cash, CreditCard, BankTransfer, Check
    public string InvoiceNumber { get; set; }
    public string InvoicePath { get; set; }
    
    // Navigation Properties
    public virtual Payment Payment { get; set; }
}
```

### 4.4. Para Birimi Modeli (Currency.cs)

Para birimi modeli, sistemde kullanılan para birimlerini temsil eder.

```csharp
public class Currency : BaseLookupEntity
{
    public string Code { get; set; } // TRY, USD, EUR, GBP
    public string Symbol { get; set; } // ₺, $, €, £
    public decimal ExchangeRate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public string Source { get; set; } // TCMB, ECB, Manual
    public bool IsBaseCurrency { get; set; }
    
    // Navigation Properties
    public virtual ICollection<CurrencyHistory> CurrencyHistory { get; set; }
}
```

### 4.5. Döviz Kuru Geçmişi Modeli (CurrencyHistory.cs)

Döviz kuru geçmişi modeli, kur değişimlerini temsil eder.

```csharp
public class CurrencyHistory : BaseTransactionEntity
{
    public string CurrencyCode { get; set; } // TRY, USD, EUR, GBP
    public string BaseCode { get; set; } // TRY, USD, EUR, GBP
    public decimal BuyingRate { get; set; }
    public decimal SellingRate { get; set; }
    public decimal EffectiveRate { get; set; }
    public string Source { get; set; } // TCMB, ECB, Manual
    public DateTime FetchDate { get; set; }
    public bool IsManuallyEntered { get; set; }
    public int? EnteredByUserId { get; set; }
    
    // Navigation Properties
    public virtual User EnteredByUser { get; set; }
}
```

## 5. Rezervasyon ve Aktivite Modelleri

### 5.1. Aktivite Alanı Modeli (ActivityArea.cs)

Aktivite alanı modeli, ortak alanları temsil eder.

```csharp
public class ActivityArea : BaseLookupEntity
{
    public bool IsPaid { get; set; }
    public decimal? PricePerHour { get; set; }
    public decimal? PricePerDay { get; set; }
    public decimal? PricePerPerson { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public int Capacity { get; set; }
    public TimeSpan AvailableTimeStart { get; set; }
    public TimeSpan AvailableTimeEnd { get; set; }
    public int MinimumReservationHours { get; set; }
    public string Features { get; set; } // JSON
    public bool RequiresApproval { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Reservation> Reservations { get; set; }
}
```

### 5.2. Rezervasyon Modeli (Reservation.cs)

Rezervasyon modeli, rezervasyon kayıtlarını temsil eder.

```csharp
public class Reservation : BaseTransactionEntity
{
    public string ReservationType { get; set; } // Apartment, ActivityArea
    public int RelatedId { get; set; } // ApartmentId veya ActivityAreaId
    public int UserId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int NumberOfPeople { get; set; }
    public string GuestNames { get; set; } // JSON
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    public string Status { get; set; } // Pending, Confirmed, CheckedIn, CheckedOut, Cancelled
    public int? PaymentId { get; set; }
    public string Notes { get; set; }
    public bool IsKbsRegistered { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public int? ProcessedByUserId { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual User ProcessedByUser { get; set; }
    public virtual Payment Payment { get; set; }
}
```

## 6. KBS ve Güvenlik Modelleri

### 6.1. KBS Bildirimi Modeli (KBSNotification.cs)

KBS bildirimi modeli, Kimlik Bildirim Sistemi'ne yapılan bildirimleri temsil eder.

```csharp
public class KBSNotification : BaseTransactionEntity
{
    public int UserId { get; set; }
    public int RelatedPersonId { get; set; }
    public string RelatedPersonType { get; set; } // User, FamilyMember
    public string NotificationType { get; set; } // CheckIn, CheckOut
    public DateTime NotificationDate { get; set; }
    public string Status { get; set; } // Pending, Success, Error
    public string ErrorMessage { get; set; }
    public string SystemRecordNumber { get; set; }
    public string ResponseDetails { get; set; } // JSON
    public int? ProcessedByUserId { get; set; }
    public bool IsAutomatic { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual User ProcessedByUser { get; set; }
}
```

### 6.2. RFID Kart Modeli (RFIDCard.cs)

RFID Kart modeli, erişim kartlarını temsil eder.

```csharp
public class RFIDCard : BaseTransactionEntity
{
    public int UserId { get; set; }
    public string CardNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Status { get; set; } // Active, Inactive, Lost
    public string Access { get; set; } // JSON
    public DateTime? LastAccessDate { get; set; }
    public string LastAccessPoint { get; set; }
    public decimal? DepositAmount { get; set; }
    public string Currency { get; set; } // TRY, USD, EUR, GBP
    
    // Navigation Properties
    public virtual User User { get; set; }
}
```

### 6.3. Araç Plakası Modeli (VehiclePlate.cs)

Araç plakası modeli, kayıtlı araçları temsil eder.

```csharp
public class VehiclePlate : BaseTransactionEntity
{
    public int UserId { get; set; }
    public int? ApartmentId { get; set; }
    public string PlateNumber { get; set; }
    public string VehicleMake { get; set; }
    public string VehicleModel { get; set; }
    public string VehicleColor { get; set; }
    public int? VehicleYear { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string ParkingSpot { get; set; }
    public bool IsAuthorized { get; set; }
    public string AccessType { get; set; } // Resident, Visitor, Staff
    
    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Apartment Apartment { get; set; }
}
```

## 7. Firma ve Şube Modelleri

### 7.1. Firma Modeli (Company.cs)

Firma modeli, site yönetim firmalarını temsil eder.

```csharp
public class Company : BaseLookupEntity
{
    public string TaxNumber { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string LogoPath { get; set; }
    public string WebsiteUrl { get; set; }
    public string PreferredCurrency { get; set; } // TRY, USD, EUR, GBP
    public string PreferredLanguage { get; set; } // tr, en, de, ru, ar, fa
    
    // Navigation Properties
    public virtual ICollection<Branch> Branches { get; set; }
}
```

### 7.2. Şube Modeli (Branch.cs)

Şube modeli, firma şubelerini temsil eder.

```csharp
public class Branch : BaseLookupEntity
{
    public int CompanyId { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ManagerName { get; set; }
    public string PreferredCurrency { get; set; } // TRY, USD, EUR, GBP
    public string PreferredLanguage { get; set; } // tr, en, de, ru, ar, fa
    
    // Navigation Properties
    public virtual Company Company { get; set; }
}
```

## 8. Duyuru ve Bildirim Modelleri

### 8.1. Duyuru Modeli (Announcement.cs)

Duyuru modeli, site duyurularını temsil eder.

```csharp
public class Announcement : BaseTransactionEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string AnnouncementType { get; set; } // General, Emergency, Maintenance, Event
    public string Priority { get; set; } // Low, Medium, High, Urgent
    public DateTime PublishDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsPublished { get; set; }
    public string TargetAudience { get; set; } // All, Owners, Tenants, Staff,