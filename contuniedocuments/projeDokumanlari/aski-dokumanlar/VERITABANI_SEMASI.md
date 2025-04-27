# Rezidans ve Site Yönetim Sistemi - Veritabanı Şeması

Bu belge, Rezidans ve Site Yönetim Sistemi için tasarlanan veritabanı şemasını ve ilişkilerini içermektedir.

## Temel Prensipler

1. **Multi-tenant Yapı:** Tüm tablolarda FirmaID ve SubeID alanları bulunacaktır.
2. **Lookup ve Transaction Yapısı:** Tanım tabloları (Lookup) ve hareket tabloları (Transaction) olarak iki ana kategori.
3. **İlişkili Veri Yapısı:** Tablolar arasında güçlü ilişkiler kurulacaktır.
4. **Ortak Alanlar:** Tüm tablolarda ortak alanlar (ID, Createdat, UpdatedAt, IsActive vb.) kullanılacaktır.

## Temel Sınıflar

### 1. BaseEntity
- ID: int (Primary Key)
- FirmaID: int (Foreign Key)
- SubeID: int (Foreign Key)
- CreatedAt: DateTime
- CreatedBy: int
- UpdatedAt: DateTime
- UpdatedBy: int
- IsActive: bool
- IsDeleted: bool

### 2. BaseLookupEntity (BaseEntity'den türer)
- Code: string
- Name: string
- Description: string
- DisplayOrder: int

### 3. BaseTransactionEntity (BaseEntity'den türer)
- TransactionDate: DateTime
- ReferenceNumber: string
- Notes: string

## Tanım (Lookup) Tabloları

### 1. Firma (Company)
- BaseLookupEntity'den türer
- TaxNumber: string
- Address: string
- PhoneNumber: string
- Email: string
- LogoPath: string
- WebsiteUrl: string
- PreferredCurrency: string (enum: TRY, EUR, USD, GBP)
- PreferredLanguage: string (enum: tr, en, de, ru, ar, fa)

### 2. Şube (Branch)
- BaseLookupEntity'den türer
- FirmaID: int (Foreign Key -> Firma)
- Address: string
- PhoneNumber: string
- Email: string
- ManagerName: string
- PreferredCurrency: string (enum: TRY, EUR, USD, GBP)
- PreferredLanguage: string (enum: tr, en, de, ru, ar, fa)

### 3. Kullanıcı Tipleri (UserType)
- BaseLookupEntity'den türer
- Permissions: string (JSON)

### 4. Kullanıcılar (User)
- BaseLookupEntity'den türer
- UserTypeID: int (Foreign Key -> UserType)
- Username: string
- PasswordHash: string
- Email: string
- PhoneNumber: string
- LastLoginDate: DateTime
- AccessExpiryDate: DateTime
- ProfilePicturePath: string
- TwoFactorEnabled: bool
- TwoFactorKey: string
- PreferredCurrency: string (enum: TRY, EUR, USD, GBP)
- PreferredLanguage: string (enum: tr, en, de, ru, ar, fa)

### 5. Departman (Department)
- BaseLookupEntity'den türer
- ResponsibleName: string
- ResponsiblePhone: string
- Email: string

### 6. Bina (Building)
- BaseLookupEntity'den türer
- Address: string
- NumberOfFloors: int
- BuildYear: int
- NumberOfApartments: int
- ManagerName: string
- ManagerPhone: string
- Location: string (GPS koordinatları)
- Features: string (JSON)

### 7. Daire Tipleri (ApartmentType)
- BaseLookupEntity'den türer
- NumberOfRooms: int
- NumberOfBathrooms: int
- HasBalcony: bool
- Features: string (JSON)
- BaseFee: decimal
- BaseRent: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)

### 8. Daire (Apartment)
- BaseLookupEntity'den türer
- BuildingID: int (Foreign Key -> Building)
- ApartmentTypeID: int (Foreign Key -> ApartmentType)
- FloorNumber: int
- DoorNumber: string
- GrossSqm: decimal
- NetSqm: decimal
- Status: string (enum: Occupied, Vacant, ForSale, ForRent)
- MonthlyFee: decimal
- Features: string (JSON)
- IsRentable: bool
- RentalFeePerDay: decimal
- RentalFeePerWeek: decimal
- RentalFeePerMonth: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)

### 9. Aktivite Alanları (ActivityArea)
- BaseLookupEntity'den türer
- IsPaid: bool
- PricePerHour: decimal
- PricePerDay: decimal
- PricePerPerson: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- Capacity: int
- AvailableTimeStart: TimeSpan
- AvailableTimeEnd: TimeSpan
- MinimumReservationHours: int
- Features: string (JSON)
- RequiresApproval: bool

### 10. Hizmet Türleri (ServiceType)
- BaseLookupEntity'den türer
- Price: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- EstimatedDuration: int (dakika)
- RequiresApproval: bool
- ChargeToApartment: bool
- DepartmentID: int (Foreign Key -> Department)

### 11. Para Birimleri (Currency)
- BaseLookupEntity'den türer
- Symbol: string
- ExchangeRate: decimal
- LastUpdateDate: DateTime
- Source: string (enum: TCMB, ECB, Manual)
- IsBaseCurrency: bool

### 12. Sistem Parametreleri (SystemParameter)
- BaseLookupEntity'den türer
- ParameterKey: string
- ParameterValue: string
- ParameterType: string (enum: String, Int, Decimal, Bool, DateTime)
- IsEditable: bool
- DisplayGroup: string

## Hareket (Transaction) Tabloları

### 1. Daire Sahipleri (ApartmentOwner)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- OwnerUserID: int (Foreign Key -> User)
- StartDate: DateTime
- EndDate: DateTime
- OwnershipPercentage: decimal
- ContractPath: string
- BankAccountDetails: string
- RevenueSharePercentage: decimal

### 2. Kiracılar (Tenant)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- TenantUserID: int (Foreign Key -> User)
- LeaseStartDate: DateTime
- LeaseEndDate: DateTime
- MonthlyRent: decimal
- RentCurrency: string (enum: TRY, EUR, USD, GBP)
- ContractPath: string
- SecurityDeposit: decimal
- IsActive: bool

### 3. Aile Üyeleri (FamilyMember)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- Relationship: string (enum: Spouse, Child, Parent, Other)
- FullName: string
- IdentityNumber: string
- DateOfBirth: DateTime
- PhoneNumber: string
- Email: string
- IsKbsRegistered: bool
- KbsRegistrationDate: DateTime
- IsActive: bool

### 4. Rezervasyonlar (Reservation)
- BaseTransactionEntity'den türer
- ReservationType: string (enum: Apartment, ActivityArea)
- RelatedID: int (Foreign Key -> Apartment veya ActivityArea)
- UserID: int (Foreign Key -> User)
- StartDateTime: DateTime
- EndDateTime: DateTime
- NumberOfPeople: int
- GuestNames: string (JSON)
- TotalPrice: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- Status: string (enum: Pending, Confirmed, CheckedIn, CheckedOut, Cancelled)
- PaymentID: int (Foreign Key -> Payment)
- Notes: string
- IsKbsRegistered: bool
- CheckInDate: DateTime
- CheckOutDate: DateTime
- ProcessedByUserID: int (Foreign Key -> User)

### 5. Ödemeler (Payment)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- ExchangeRate: decimal
- BaseAmount: decimal (Ana para birimi cinsinden değer)
- BaseCurrency: string (enum: TRY, EUR, USD, GBP)
- PaymentType: string (enum: Dues, Rent, Service, ActivityArea)
- PaymentMethod: string (enum: Cash, CreditCard, BankTransfer, Check)
- Status: string (enum: Pending, Completed, Failed, Refunded)
- RelatedEntityID: int
- RelatedEntityType: string
- ReceiptNumber: string
- ReceiptPath: string
- ProcessedByUserID: int (Foreign Key -> User)
- PaymentDate: DateTime

### 6. Aidatlar (Dues)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- Year: int
- Month: int
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- DueDate: DateTime
- IsPaid: bool
- PaymentID: int (Foreign Key -> Payment)
- IsReconciled: bool
- ReconciliationDate: DateTime
- ReconciliationUserID: int (Foreign Key -> User)

### 7. Demirbaşlar (Inventory)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- Name: string
- Description: string
- SerialNumber: string
- PurchaseDate: DateTime
- PurchasePrice: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- CurrentValue: decimal
- Condition: string (enum: New, Good, Fair, Poor)
- WarrantyEndDate: DateTime
- PhotoPath: string
- Location: string
- CategoryID: int (Foreign Key -> InventoryCategory)

### 8. Demirbaş Kategorileri (InventoryCategory)
- BaseLookupEntity'den türer
- ParentCategoryID: int (Foreign Key -> InventoryCategory)
- DepreciationRate: decimal

### 9. Hizmet Talepleri (ServiceRequest)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- ServiceTypeID: int (Foreign Key -> ServiceType)
- RequestDate: DateTime
- ScheduledDate: DateTime
- ScheduledTimeStart: TimeSpan
- ScheduledTimeEnd: TimeSpan
- CompletionDate: DateTime
- Status: string (enum: Pending, Approved, Scheduled, InProgress, Completed, Cancelled)
- AssignedPersonnelID: int (Foreign Key -> User)
- Price: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- PaymentID: int (Foreign Key -> Payment)
- Notes: string
- Priority: string (enum: Low, Medium, High, Urgent)
- AttachmentPaths: string (JSON)
- Rating: int
- Feedback: string

### 10. Personel (Personnel)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- DepartmentID: int (Foreign Key -> Department)
- Position: string
- HireDate: DateTime
- Salary: decimal
- SalaryCurrency: string (enum: TRY, EUR, USD, GBP)
- WorkingHours: string (JSON)
- EmergencyContact: string
- IdentityNumber: string
- BankAccountDetails: string
- Skills: string (JSON)
- Certifications: string (JSON)

### 11. RFID Kartları (RFIDCard)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- CardNumber: string
- IssueDate: DateTime
- ExpiryDate: DateTime
- Status: string (enum: Active, Inactive, Lost)
- Access: string (JSON)
- LastAccessDate: DateTime
- LastAccessPoint: string
- DepositAmount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)

### 12. Araç Plakaları (VehiclePlate)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- PlateNumber: string
- VehicleMake: string
- VehicleModel: string
- VehicleColor: string
- VehicleYear: int
- RegistrationDate: DateTime
- ParkingSpot: string
- IsAuthorized: bool
- AccessType: string (enum: Resident, Visitor, Staff)

### 13. KBS Bildirimleri (KBSNotification)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- RelatedPersonID: int (Foreign Key -> User veya FamilyMember)
- RelatedPersonType: string (enum: User, FamilyMember)
- NotificationType: string (enum: CheckIn, CheckOut)
- NotificationDate: DateTime
- Status: string (enum: Pending, Success, Error)
- ErrorMessage: string
- SystemRecordNumber: string
- ResponseDetails: string (JSON)
- ProcessedByUserID: int (Foreign Key -> User)
- IsAutomatic: bool

### 14. Gelir-Gider (IncomeExpense)
- BaseTransactionEntity'den türer
- Type: string (enum: Income, Expense)
- Category: string
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- ExchangeRate: decimal
- BaseAmount: decimal (Ana para birimi cinsinden değer)
- BaseCurrency: string (enum: TRY, EUR, USD, GBP)
- Description: string
- SourceID: int
- SourceType: string (enum: Dues, Rent, Service, ActivityArea, Other)
- PaymentID: int (Foreign Key -> Payment)
- PaymentMethod: string (enum: Cash, CreditCard, BankTransfer, Check)
- InvoiceNumber: string
- InvoicePath: string
- DistributionType: string (enum: OwnerShare, ManagementShare, Both)
- OwnerSharePercentage: decimal
- OwnerShareAmount: decimal
- ApprovalStatus: string (enum: Pending, Approved, Rejected)
- ApprovedByUserID: int (Foreign Key -> User)
- ApprovalDate: DateTime

### 15. Döviz Kuru Geçmişi (CurrencyHistory)
- BaseTransactionEntity'den türer
- CurrencyCode: string (enum: TRY, EUR, USD, GBP)
- BaseCode: string (enum: TRY, EUR, USD, GBP)
- BuyingRate: decimal
- SellingRate: decimal
- EffectiveRate: decimal
- Source: string (enum: TCMB, ECB, Manual)
- FetchDate: DateTime
- IsManuallyEntered: bool
- EnteredByUserID: int (Foreign Key -> User)

### 16. Duyurular (Announcement)
- BaseTransactionEntity'den türer
- Title: string
- Content: string
- AnnouncementType: string (enum: General, Emergency, Maintenance, Event)
- Priority: string (enum: Low, Medium, High, Urgent)
- PublishDate: DateTime
- ExpiryDate: DateTime
- IsPublished: bool
- TargetAudience: string (enum: All, Owners, Tenants, Staff, Specific)
- TargetApartments: string (JSON) (Belirli dairelere yapılan duyurular için)
- AttachmentPaths: string (JSON)
- NotificationSent: bool
- NotificationChannels: string (enum: Email, SMS, App, All)
- CreatedByUserID: int (Foreign Key -> User)

### 17. Aidat Mahsuplaştırma (DuesReconciliation)
- BaseTransactionEntity'den türer
- Year: int
- Month: int
- ReconciliationDate: DateTime
- ProcessedByUserID: int (Foreign Key -> User)
- Status: string (enum: Pending, Completed, Cancelled)
- AffectedApartmentCount: int
- TotalAmount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- Notes: string
- ReconciliationType: string (enum: Monthly, Yearly, Custom)
- ApartmentIDs: string (JSON) (Toplu mahsuplaştırmada seçilen daireler)

### 18. Müsaitlik Planı (AvailabilitySchedule)
- BaseTransactionEntity'den türer
- EntityType: string (enum: Apartment, ActivityArea)
- EntityID: int (Foreign Key -> Apartment veya ActivityArea)
- StartDate: DateTime
- EndDate: DateTime
- IsAvailable: bool
- ReasonIfUnavailable: string
- BlockedByUserID: int (Foreign Key -> User)
- RecurringPattern: string (JSON) (Tekrarlanan müsaitlik durumları için)

## İlişki Şeması

```
Company 1 ---< Branch
Branch 1 ---< User
Branch 1 ---< SystemParameter
UserType 1 ---< User
User 1 ---< ApartmentOwner
User 1 ---< Tenant
User 1 ---< Reservation
User 1 ---< ServiceRequest
User 1 ---< RFIDCard
User 1 ---< VehiclePlate
User 1 ---< KBSNotification
User 1 ---< FamilyMember
Department 1 ---< Personnel
Department 1 ---< ServiceType
Building 1 ---< Apartment
ApartmentType 1 ---< Apartment
Apartment 1 ---< ApartmentOwner
Apartment 1 ---< Tenant
Apartment 1 ---< Dues
Apartment 1 ---< Inventory
Apartment 1 ---< ServiceRequest
Apartment 1 ---< FamilyMember
Apartment 1 ---< VehiclePlate
Apartment 1 ---< Reservation (ReservationType = Apartment)
ActivityArea 1 ---< Reservation (ReservationType = ActivityArea)
ServiceType 1 ---< ServiceRequest
Payment 1 ---< Dues
Payment 1 ---< ServiceRequest
Payment 1 ---< Reservation
Payment 1 ---< IncomeExpense
InventoryCategory 1 ---< Inventory
Currency 1 ---< CurrencyHistory
```

## Veritabanı Diyagramı

```
[BaseEntity] <-- [BaseLookupEntity]
             <-- [BaseTransactionEntity]

[BaseLookupEntity] <-- [Company] <-- [Branch]
                    <-- [UserType] <-- [User]
                    <-- [Department]
                    <-- [Building] <-- [Apartment]
                    <-- [ApartmentType]
                    <-- [ActivityArea]
                    <-- [ServiceType]
                    <-- [Currency]
                    <-- [SystemParameter]
                    <-- [InventoryCategory]

[BaseTransactionEntity] <-- [ApartmentOwner]
                        <-- [Tenant]
                        <-- [Reservation]
                        <-- [Payment]
                        <-- [Dues]
                        <-- [Inventory]
                        <-- [ServiceRequest]
                        <-- [Personnel]
                        <-- [RFIDCard]
                        <-- [VehiclePlate]
                        <-- [KBSNotification]
                        <-- [IncomeExpense]
                        <-- [FamilyMember]
                        <-- [CurrencyHistory]
                        <-- [Announcement]
                        <-- [DuesReconciliation]
                        <-- [AvailabilitySchedule]
```

Not: Bu şema, son kullanıcı taleplerine uygun olarak genişletilmiş ve detaylandırılmıştır. Sistem, daire doluluk/boşluk takibi, saatlik rezervasyon yönetimi, aidat mahsuplaştırma, toplu kimlik bildirimi, hizmet ücretlendirme ve çoklu dil/para birimi gibi özellikleri destekleyecek şekilde güncellenmiştir. Tüm tablolarda FirmaID ve SubeID alanları mevcuttur, bu alanlar multi-tenant yapının temelini oluşturur. 


# Rezidans ve Site Yönetim Sistemi - Veritabanı Şeması

Bu belge, Rezidans ve Site Yönetim Sistemi için tasarlanan veritabanı şemasını ve ilişkilerini içermektedir.

## Temel Prensipler

1. **Multi-tenant Yapı:** Tüm tablolarda FirmaID ve SubeID alanları bulunacaktır.
2. **Lookup ve Transaction Yapısı:** Tanım tabloları (Lookup) ve hareket tabloları (Transaction) olarak iki ana kategori.
3. **İlişkili Veri Yapısı:** Tablolar arasında güçlü ilişkiler kurulacaktır.
4. **Ortak Alanlar:** Tüm tablolarda ortak alanlar (ID, Createdat, UpdatedAt, IsActive vb.) kullanılacaktır.

## Temel Sınıflar

1. **BaseEntity**
   - ID: int (Primary Key)
   - FirmaID: int (Foreign Key)
   - SubeID: int (Foreign Key)
   - CreatedAt: DateTime
   - CreatedBy: int
   - UpdatedAt: DateTime
   - UpdatedBy: int
   - IsActive: bool
   - IsDeleted: bool

2. **BaseLookupEntity** (BaseEntity'den türer)
   - Code: string
   - Name: string
   - Description: string
   - DisplayOrder: int

3. **BaseTransactionEntity** (BaseEntity'den türer)
   - TransactionDate: DateTime
   - ReferenceNumber: string
   - Notes: string

## Tanım (Lookup) Tabloları

### 1. Firma (Company)
- BaseLookupEntity'den türer
- TaxNumber: string
- Address: string
- PhoneNumber: string
- Email: string
- LogoPath: string
- WebsiteUrl: string

### 2. Şube (Branch)
- BaseLookupEntity'den türer
- FirmaID: int (Foreign Key -> Firma)
- Address: string
- PhoneNumber: string
- Email: string
- ManagerName: string

### 3. Kullanıcı Tipleri (UserType)
- BaseLookupEntity'den türer
- Permissions: string (JSON)

### 4. Kullanıcılar (User)
- BaseLookupEntity'den türer
- UserTypeID: int (Foreign Key -> UserType)
- Username: string
- PasswordHash: string
- Email: string
- PhoneNumber: string
- LastLoginDate: DateTime
- AccessExpiryDate: DateTime
- ProfilePicturePath: string

### 5. Departman (Department)
- BaseLookupEntity'den türer
- ResponsibleName: string
- ResponsiblePhone: string
- Email: string

### 6. Bina (Building)
- BaseLookupEntity'den türer
- Address: string
- NumberOfFloors: int
- BuildYear: int
- NumberOfApartments: int
- ManagerName: string
- ManagerPhone: string

### 7. Daire Tipleri (ApartmentType)
- BaseLookupEntity'den türer
- NumberOfRooms: int
- NumberOfBathrooms: int
- HasBalcony: bool
- Features: string (JSON)

### 8. Daire (Apartment)
- BaseLookupEntity'den türer
- BuildingID: int (Foreign Key -> Building)
- ApartmentTypeID: int (Foreign Key -> ApartmentType)
- FloorNumber: int
- DoorNumber: string
- GrossSqm: decimal
- NetSqm: decimal
- Status: string (enum: Occupied, Vacant, ForSale, ForRent)
- MonthlyFee: decimal
- Features: string (JSON)

### 9. Aktivite Alanları (ActivityArea)
- BaseLookupEntity'den türer
- IsPaid: bool
- PricePerHour: decimal
- PricePerPerson: decimal
- Capacity: int
- Features: string (JSON)

### 10. Hizmet Türleri (ServiceType)
- BaseLookupEntity'den türer
- Price: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- EstimatedDuration: int (dakika)
- RequiresApproval: bool

### 11. Para Birimleri (Currency)
- BaseLookupEntity'den türer
- Symbol: string
- ExchangeRate: decimal

## Hareket (Transaction) Tabloları

### 1. Daire Sahipleri (ApartmentOwner)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- OwnerUserID: int (Foreign Key -> User)
- StartDate: DateTime
- EndDate: DateTime
- OwnershipPercentage: decimal
- ContractPath: string

### 2. Kiracılar (Tenant)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- TenantUserID: int (Foreign Key -> User)
- LeaseStartDate: DateTime
- LeaseEndDate: DateTime
- MonthlyRent: decimal
- RentCurrency: string (enum: TRY, EUR, USD, GBP)
- ContractPath: string

### 3. Rezervasyonlar (Reservation)
- BaseTransactionEntity'den türer
- ActivityAreaID: int (Foreign Key -> ActivityArea)
- UserID: int (Foreign Key -> User)
- StartDateTime: DateTime
- EndDateTime: DateTime
- NumberOfPeople: int
- TotalPrice: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- Status: string (enum: Pending, Confirmed, Cancelled)
- Notes: string

### 4. Ödemeler (Payment)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- PaymentType: string (enum: Dues, Rent, Service, ActivityArea)
- PaymentMethod: string (enum: Cash, CreditCard, BankTransfer)
- Status: string (enum: Pending, Completed, Failed)
- RelatedEntityID: int
- RelatedEntityType: string

### 5. Aidatlar (Dues)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- Year: int
- Month: int
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- DueDate: DateTime
- IsPaid: bool
- PaymentID: int (Foreign Key -> Payment)

### 6. Demirbaşlar (Inventory)
- BaseTransactionEntity'den türer
- ApartmentID: int (Foreign Key -> Apartment)
- Name: string
- Description: string
- PurchaseDate: DateTime
- PurchasePrice: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- CurrentValue: decimal
- Condition: string (enum: New, Good, Fair, Poor)
- PhotoPath: string

### 7. Hizmet Talepleri (ServiceRequest)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- ApartmentID: int (Foreign Key -> Apartment)
- ServiceTypeID: int (Foreign Key -> ServiceType)
- RequestDate: DateTime
- ScheduledDate: DateTime
- CompletionDate: DateTime
- Status: string (enum: Pending, Approved, InProgress, Completed, Cancelled)
- AssignedPersonnelID: int (Foreign Key -> User)
- Price: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- PaymentID: int (Foreign Key -> Payment)
- Notes: string

### 8. Personel (Personnel)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- DepartmentID: int (Foreign Key -> Department)
- Position: string
- HireDate: DateTime
- Salary: decimal
- SalaryCurrency: string (enum: TRY, EUR, USD, GBP)
- WorkingHours: string (JSON)
- EmergencyContact: string

### 9. RFID Kartları (RFIDCard)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- CardNumber: string
- IssueDate: DateTime
- ExpiryDate: DateTime
- Status: string (enum: Active, Inactive, Lost)
- Access: string (JSON)

### 10. Araç Plakaları (VehiclePlate)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- PlateNumber: string
- VehicleMake: string
- VehicleModel: string
- VehicleColor: string
- RegistrationDate: DateTime

### 11. KBS Bildirimleri (KBSNotification)
- BaseTransactionEntity'den türer
- UserID: int (Foreign Key -> User)
- NotificationDate: DateTime
- Status: string (enum: Success, Error)
- ErrorMessage: string
- SystemRecordNumber: string

### 12. Gelir-Gider (IncomeExpense)
- BaseTransactionEntity'den türer
- Type: string (enum: Income, Expense)
- Category: string
- Amount: decimal
- Currency: string (enum: TRY, EUR, USD, GBP)
- TRYEquivalent: decimal
- Description: string
- SourceID: int
- SourceType: string (enum: Dues, Rent, Service, Other)

## İlişki Şeması

```
Company 1 ---< Branch
Branch 1 ---< User
UserType 1 ---< User
User 1 ---< ApartmentOwner
User 1 ---< Tenant
User 1 ---< Reservation
User 1 ---< ServiceRequest
User 1 ---< RFIDCard
User 1 ---< VehiclePlate
User 1 ---< KBSNotification
Department 1 ---< Personnel
Building 1 ---< Apartment
ApartmentType 1 ---< Apartment
Apartment 1 ---< ApartmentOwner
Apartment 1 ---< Tenant
Apartment 1 ---< Dues
Apartment 1 ---< Inventory
Apartment 1 ---< ServiceRequest
ActivityArea 1 ---< Reservation
ServiceType 1 ---< ServiceRequest
Payment 1 ---< Dues
Payment 1 ---< ServiceRequest
```

## Veritabanı Diyagramı

```
[BaseEntity] <-- [BaseLookupEntity]
             <-- [BaseTransactionEntity]

[BaseLookupEntity] <-- [Company] <-- [Branch]
                    <-- [UserType] <-- [User]
                    <-- [Department]
                    <-- [Building] <-- [Apartment]
                    <-- [ApartmentType]
                    <-- [ActivityArea]
                    <-- [ServiceType]
                    <-- [Currency]

[BaseTransactionEntity] <-- [ApartmentOwner]
                        <-- [Tenant]
                        <-- [Reservation]
                        <-- [Payment]
                        <-- [Dues]
                        <-- [Inventory]
                        <-- [ServiceRequest]
                        <-- [Personnel]
                        <-- [RFIDCard]
                        <-- [VehiclePlate]
                        <-- [KBSNotification]
                        <-- [IncomeExpense]
```

Not: Bu şema, projenin gereksinimlerine göre zaman içinde güncellenebilir. Tüm tablolarda FirmaID ve SubeID alanları mevcuttur, bu alanlar multi-tenant yapının temelini oluşturur. 