### 3.13. Para Birimleri (Currency) (devam)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Symbol | nvarchar(10) | Sembol (₺, $, €, £) |
| ExchangeRate | decimal(18,6) | Döviz kuru |
| LastUpdateDate | datetime | Son güncelleme tarihi |
| Source | nvarchar(20) | Kaynak (TCMB, ECB, Manual) |
| IsBaseCurrency | bit | Ana para birimi mi |

### 3.14. Sistem Parametreleri (SystemParameter)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| ParameterKey | nvarchar(100) | Parametre anahtarı |
| ParameterValue | nvarchar(MAX) | Parametre değeri |
| ParameterType | nvarchar(20) | Parametre tipi (String, Int, Decimal, Bool, DateTime) |
| IsEditable | bit | Düzenlenebilir mi |
| DisplayGroup | nvarchar(50) | Görüntüleme grubu |

## 4. Hareket (Transaction) Tabloları

### 4.1. Kullanıcı Rol (UserRole)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| RoleID | int, FK | Rol ID'si |

### 4.2. Rol İzin (RolePermission)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseEntity alanları* | | |
| RoleID | int, FK | Rol ID'si |
| PermissionID | int, FK | İzin ID'si |

### 4.3. İzin (Permission)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Code | nvarchar(50) | İzin kodu |
| Category | nvarchar(50) | Kategori |

### 4.4. Daire Sahipleri (ApartmentOwner)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| ApartmentID | int, FK | Daire ID'si |
| OwnerUserID | int, FK | Malik/Sahip ID'si |
| OwnershipPercentage | decimal(5,2) | Sahiplik oranı (%) |
| StartDate | datetime | Sahiplik başlangıç tarihi |
| EndDate | datetime | Sahiplik bitiş tarihi |
| DeedInformation | nvarchar(100) | Tapu bilgisi |
| BankAccountDetails | nvarchar(500) | Banka hesap bilgileri |
| RevenueSharePercentage | decimal(5,2) | Gelir paylaşım oranı (%) |

### 4.5. Kiracılar (Tenant)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| ApartmentID | int, FK | Daire ID'si |
| TenantUserID | int, FK | Kiracı ID'si |
| LeaseStartDate | datetime | Kiralama başlangıç tarihi |
| LeaseEndDate | datetime | Kiralama bitiş tarihi |
| MonthlyRent | decimal(18,2) | Aylık kira bedeli |
| RentCurrency | nvarchar(10) | Kira para birimi (TRY, EUR, USD, GBP) |
| ContractPath | nvarchar(255) | Sözleşme dosya yolu |
| SecurityDeposit | decimal(18,2) | Depozito tutarı |
| IsActive | bit | Aktif mi |

### 4.6. Aile Üyeleri (FamilyMember)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| ApartmentID | int, FK | Daire ID'si |
| Relationship | nvarchar(20) | İlişki tipi (Spouse, Child, Parent, Other) |
| FullName | nvarchar(100) | Tam adı |
| IdentityNumber | nvarchar(20) | Kimlik numarası |
| DateOfBirth | datetime | Doğum tarihi |
| PhoneNumber | nvarchar(20) | Telefon numarası |
| Email | nvarchar(100) | E-posta adresi |
| IsKbsRegistered | bit | KBS'ye kayıtlı mı |
| KbsRegistrationDate | datetime | KBS kayıt tarihi |
| IsActive | bit | Aktif mi |

### 4.7. Rezervasyonlar (Reservation)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| ReservationType | nvarchar(20) | Rezervasyon tipi (Apartment, ActivityArea) |
| RelatedID | int | İlgili ID (Daire veya Aktivite Alanı ID'si) |
| UserID | int, FK | Kullanıcı ID'si |
| StartDateTime | datetime | Başlangıç tarihi ve saati |
| EndDateTime | datetime | Bitiş tarihi ve saati |
| NumberOfPeople | int | Kişi sayısı |
| GuestNames | nvarchar(MAX) | Misafir isimleri (JSON) |
| TotalPrice | decimal(18,2) | Toplam fiyat |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| Status | nvarchar(20) | Durum (Pending, Confirmed, CheckedIn, CheckedOut, Cancelled) |
| PaymentID | int, FK | Ödeme ID'si |
| Notes | nvarchar(500) | Notlar |
| IsKbsRegistered | bit | KBS'ye kayıtlı mı |
| CheckInDate | datetime | Giriş tarihi |
| CheckOutDate | datetime | Çıkış tarihi |
| ProcessedByUserID | int, FK | İşlemi yapan kullanıcı ID'si |

### 4.8. Ödemeler (Payment)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| ApartmentID | int, FK | Daire ID'si |
| Amount | decimal(18,2) | Tutar |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| ExchangeRate | decimal(18,6) | Döviz kuru |
| BaseAmount | decimal(18,2) | Ana para birimi cinsinden değer |
| BaseCurrency | nvarchar(10) | Ana para birimi |
| PaymentType | nvarchar(20) | Ödeme tipi (Dues, Rent, Service, ActivityArea) |
| PaymentMethod | nvarchar(20) | Ödeme yöntemi (Cash, CreditCard, BankTransfer, Check) |
| Status | nvarchar(20) | Durum (Pending, Completed, Failed, Refunded) |
| RelatedEntityID | int | İlgili kayıt ID'si |
| RelatedEntityType | nvarchar(50) | İlgili kayıt tipi |
| ReceiptNumber | nvarchar(50) | Fiş/makbuz numarası |
| ReceiptPath | nvarchar(255) | Fiş/makbuz dosya yolu |
| ProcessedByUserID | int, FK | İşlemi yapan kullanıcı ID'si |
| PaymentDate | datetime | Ödeme tarihi |

### 4.9. Aidatlar (Dues)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| ApartmentID | int, FK | Daire ID'si |
| Year | int | Yıl |
| Month | int | Ay |
| Amount | decimal(18,2) | Tutar |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| DueDate | datetime | Son ödeme tarihi |
| IsPaid | bit | Ödendi mi |
| PaymentID | int, FK | Ödeme ID'si |
| IsReconciled | bit | Mahsuplaştırıldı mı |
| ReconciliationDate | datetime | Mahsuplaştırma tarihi |
| ReconciliationUserID | int, FK | Mahsuplaştırmayı yapan kullanıcı ID'si |

### 4.10. Demirbaşlar (Inventory)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| ApartmentID | int, FK | Daire ID'si |
| Name | nvarchar(100) | Demirbaş adı |
| Description | nvarchar(500) | Açıklama |
| SerialNumber | nvarchar(50) | Seri numarası |
| PurchaseDate | datetime | Satın alma tarihi |
| PurchasePrice | decimal(18,2) | Satın alma fiyatı |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| CurrentValue | decimal(18,2) | Mevcut değeri |
| Condition | nvarchar(20) | Durumu (New, Good, Fair, Poor) |
| WarrantyEndDate | datetime | Garanti bitiş tarihi |
| PhotoPath | nvarchar(255) | Fotoğraf dosya yolu |
| Location | nvarchar(100) | Konum |
| CategoryID | int, FK | Kategori ID'si |

### 4.11. Demirbaş Kategorileri (InventoryCategory)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| ParentCategoryID | int, FK | Üst kategori ID'si |
| DepreciationRate | decimal(5,2) | Amortisman oranı |

### 4.12. Hizmet Talepleri (ServiceRequest)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| ApartmentID | int, FK | Daire ID'si |
| ServiceTypeID | int, FK | Hizmet türü ID'si |
| RequestDate | datetime | Talep tarihi |
| ScheduledDate | datetime | Planlanan tarih |
| ScheduledTimeStart | time | Planlanan başlangıç saati |
| ScheduledTimeEnd | time | Planlanan bitiş saati |
| CompletionDate | datetime | Tamamlanma tarihi |
| Status | nvarchar(20) | Durum (Pending, Approved, Scheduled, InProgress, Completed, Cancelled) |
| AssignedPersonnelID | int, FK | Atanan personel ID'si |
| Price | decimal(18,2) | Fiyat |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| PaymentID | int, FK | Ödeme ID'si |
| Notes | nvarchar(500) | Notlar |
| Priority | nvarchar(20) | Öncelik (Low, Medium, High, Urgent) |
| AttachmentPaths | nvarchar(MAX) | Ek dosya yolları (JSON) |
| Rating | int | Değerlendirme puanı |
| Feedback | nvarchar(500) | Geribildirim |

### 4.13. Personel (Personnel)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| DepartmentID | int, FK | Departman ID'si |
| Position | nvarchar(50) | Pozisyon |
| HireDate | datetime | İşe başlama tarihi |
| Salary | decimal(18,2) | Maaş |
| SalaryCurrency | nvarchar(10) | Maaş para birimi (TRY, EUR, USD, GBP) |
| WorkingHours | nvarchar(MAX) | Çalışma saatleri (JSON) |
| EmergencyContact | nvarchar(100) | Acil durum iletişim bilgisi |
| IdentityNumber | nvarchar(20) | Kimlik numarası |
| BankAccountDetails | nvarchar(500) | Banka hesap bilgileri |
| Skills | nvarchar(MAX) | Yetenekler (JSON) |
| Certifications | nvarchar(MAX) | Sertifikalar (JSON) |

### 4.14. RFID Kartları (RFIDCard)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| CardNumber | nvarchar(50) | Kart numarası |
| IssueDate | datetime | Veriliş tarihi |
| ExpiryDate | datetime | Son kullanma tarihi |
| Status | nvarchar(20) | Durum (Active, Inactive, Lost) |
| Access | nvarchar(MAX) | Erişim bilgileri (JSON) |
| LastAccessDate | datetime | Son erişim tarihi |
| LastAccessPoint | nvarchar(100) | Son erişim noktası |
| DepositAmount | decimal(18,2) | Depozito tutarı |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |

### 4.15. Araç Plakaları (VehiclePlate)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| ApartmentID | int, FK | Daire ID'si |
| PlateNumber | nvarchar(20) | Plaka numarası |
| VehicleMake | nvarchar(50) | Araç markası |
| VehicleModel | nvarchar(50) | Araç modeli |
| VehicleColor | nvarchar(20) | Araç rengi |
| VehicleYear | int | Araç yılı |
| RegistrationDate | datetime | Kayıt tarihi |
| ParkingSpot | nvarchar(20) | Park yeri |
| IsAuthorized | bit | Yetkili mi |
| AccessType | nvarchar(20) | Erişim tipi (Resident, Visitor, Staff) |

### 4.16. KBS Bildirimleri (KBSNotification)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| UserID | int, FK | Kullanıcı ID'si |
| RelatedPersonID | int | İlgili kişi ID'si |
| RelatedPersonType | nvarchar(20) | İlgili kişi tipi (User, FamilyMember) |
| NotificationType | nvarchar(20) | Bildirim tipi (CheckIn, CheckOut) |
| NotificationDate | datetime | Bildirim tarihi |
| Status | nvarchar(20) | Durum (Pending, Success, Error) |
| ErrorMessage | nvarchar(500) | Hata mesajı |
| SystemRecordNumber | nvarchar(50) | Sistem kayıt numarası |
| ResponseDetails | nvarchar(MAX) | Yanıt detayları (JSON) |
| ProcessedByUserID | int, FK | İşlemi yapan kullanıcı ID'si |
| IsAutomatic | bit | Otomatik mi |

### 4.17. Gelir-Gider (IncomeExpense)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| Type | nvarchar(20) | Tip (Income, Expense) |
| Category | nvarchar(50) | Kategori |
| Amount | decimal(18,2) | Tutar |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| ExchangeRate | decimal(18,6) | Döviz kuru |
| BaseAmount | decimal(18,2) | Ana para birimi cinsinden değer |
| BaseCurrency | nvarchar(10) | Ana para birimi |
| Description | nvarchar(500) | Açıklama |
| SourceID | int | Kaynak ID'si |
| SourceType | nvarchar(20) | Kaynak tipi (Dues, Rent, Service, ActivityArea, Other) |
| PaymentID | int, FK | Ödeme ID'si |
| PaymentMethod | nvarchar(20) | Ödeme yöntemi (Cash, CreditCard, BankTransfer, Check) |
| InvoiceNumber | nvarchar(50) | Fatura numarası |
| InvoicePath | nvarchar(255) | Fatura dosya yolu |
| DistributionType | nvarchar(20) | Dağıtım tipi (OwnerShare, ManagementShare, Both) |
| OwnerSharePercentage | decimal(5,2) | Mal sahibi payı (%) |
| OwnerShareAmount | decimal(18,2) | Mal sahibi payı tutarı |
| ApprovalStatus | nvarchar(20) | Onay durumu (Pending, Approved, Rejected) |
| ApprovedByUserID | int, FK | Onaylayan kullanıcı ID'si |
| ApprovalDate | datetime | Onay tarihi |

### 4.18. Döviz Kuru Geçmişi (CurrencyHistory)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| CurrencyCode | nvarchar(10) | Para birimi kodu (TRY, EUR, USD, GBP) |
| BaseCode | nvarchar(10) | Ana para birimi kodu (TRY, EUR, USD, GBP) |
| BuyingRate | decimal(18,6) | Alış kuru |
| SellingRate | decimal(18,6) | Satış kuru |
| EffectiveRate | decimal(18,6) | Efektif kur |
| Source | nvarchar(20) | Kaynak (TCMB, ECB, Manual) |
| FetchDate | datetime | Çekme tarihi |
| IsManuallyEntered | bit | Manuel olarak girildi mi |
| EnteredByUserID | int, FK | Giren kullanıcı ID'si |

### 4.19. Duyurular (Announcement)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| Title | nvarchar(100) | Başlık |
| Content | nvarchar(MAX) | İçerik |
| AnnouncementType | nvarchar(20) | Duyuru tipi (General, Emergency, Maintenance, Event) |
| Priority | nvarchar(20) | Öncelik (Low, Medium, High, Urgent) |
| PublishDate | datetime | Yayın tarihi |
| ExpiryDate | datetime | Son geçerlilik tarihi |
| IsPublished | bit | Yayınlandı mı |
| TargetAudience | nvarchar(20) | Hedef kitle (All, Owners, Tenants, Staff, Specific) |
| TargetApartments | nvarchar(MAX) | Hedef daireler (JSON) |
| AttachmentPaths | nvarchar(MAX) | Ek dosya yolları (JSON) |
| NotificationSent | bit | Bildirim gönderildi mi |
| NotificationChannels | nvarchar(20) | Bildirim kanalları (Email, SMS, App, All) |
| CreatedByUserID | int, FK | Oluşturan kullanıcı ID'si |

### 4.20. Aidat Mahsuplaştırma (DuesReconciliation)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| Year | int | Yıl |
| Month | int | Ay |
| ReconciliationDate | datetime | Mahsuplaştırma tarihi |
| ProcessedByUserID | int, FK | İşlemi yapan kullanıcı ID'si |
| Status | nvarchar(20) | Durum (Pending, Completed, Cancelled) |
| AffectedApartmentCount | int | Etkilenen daire sayısı |
| TotalAmount | decimal(18,2) | Toplam tutar |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| Notes | nvarchar(500) | Notlar |
| ReconciliationType | nvarchar(20) | Mahsuplaştırma tipi (Monthly, Yearly, Custom) |
| ApartmentIDs | nvarchar(MAX) | Daire ID'leri (JSON) |

### 4.21. Müsaitlik Planı (AvailabilitySchedule)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseTransactionEntity alanları* | | |
| EntityType | nvarchar(20) | Varlık tipi (Apartment, ActivityArea) |
| EntityID | int | Varlık ID'si |
| StartDate | datetime | Başlangıç tarihi |
| EndDate | datetime | Bitiş tarihi |
| IsAvailable | bit | Müsait mi |
| ReasonIfUnavailable | nvarchar(100) | Müsait değilse nedeni |
| BlockedByUserID | int, FK | Bloke eden kullanıcı ID'si |
| RecurringPattern | nvarchar(MAX) | Tekrarlama deseni (JSON) |

## 5. İlişki Şeması

Aşağıda, veritabanındaki önemli ilişkiler belirtilmiştir:

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
Site 1 ---< Building
Building 1 ---< Block
Block 1 ---< Apartment
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

## 6. Önemli İlişkiler

### 6.1. Çoklu Müşteri (Multi-tenant) Yapısı

- Tüm tablolarda FirmaID alanı bulunacak
- Kullanıcılar sadece kendi firmasına ait verileri görebilecek

### 6.2. Şube Yapısı

- Tüm tablolarda SubeID alanı bulunacak
- Kullanıcılar yetkilerine göre belirli şubelerdeki verilere erişebilecek

### 6.3. Daire-Sakin İlişkisi

- Bir dairede birden fazla sakin olabilir
- Sakinlerin daire ile ilişkisi zaman bazlı tutulacak (GirisTarihi, CikisTarihi)

### 6.4. Kullanıcı-Sakin İlişkisi

- Her sakin için bir kullanıcı hesabı oluşturulabilir
- Kullanıcı hesapları süreli olarak aktif/pasif edilebilir

### 6.5. Rezervasyon-Ödeme İlişkisi

- Her rezervasyon bir veya birden fazla ödeme kaydına sahip olabilir
- Ödemeler farklı para birimlerinde tutulabilir

## 7. Veritabanı İndeksleri

Performans optimizasyonu için aşağıdaki alanlara indeks eklenecektir:

1. Tüm FirmaID ve SubeID alanları
2. Tüm tablolardaki CreatedDate alanları
3. UserID, ApartmentID, BlockID gibi sık sorgulanan foreign key alanları
4. Status, IsActive, IsDeleted gibi filtreleme alanları
5. Tarih alanları (StartDate, EndDate, TransactionDate, vb.)

## 8. Veri Tipi Optimizasyonları

1. Para birimi alanları decimal(18,2) olarak tanımlanmıştır
2. Döviz kurları decimal(18,6) hassasiyetinde tutulmaktadır
3. Uzun metinler için nvarchar(MAX) kullanılmıştır
4. Kısa metinler için uygun uzunlukta nvarchar alanları tanımlanmıştır
5. JSON formatında saklanacak veriler için nvarchar(MAX) kullanılmıştır

## 9. Seed Data ve Varsayılan Değerler

Veritabanı kurulumunda aşağıdaki varsayılan veriler yüklenecektir:

1. Varsayılan firma ve şube
2. Varsayılan admin kullanıcısı
3. Temel kullanıcı rolleri (SystemAdmin, FirmaYoneticisi, SiteAdmin, Personel, Sakin, MalSahibi, Teknik, Misafir)
4. Temel izinler ve rol-izin ilişkileri
5. Temel para birimleri (TRY, EUR, USD, GBP)
6. Sistem parametreleri

## 10. Veritabanı Migrasyon Stratejisi

Veritabanı şeması, Code First yaklaşımıyla Entity Framework Core üzerinden yönetilecektir:

1. Domain modelleri tanımlanacak
2. DbContext içinde model konfigürasyonları yapılacak
3. Migration'lar oluşturulacak
4. Seed data hazırlanacak
5. Migration'lar otomatik veya manuel olarak uygulanacak

## 11. Sonuç

Bu şema, Rezidans ve Site Yönetim Sistemi'nin veritabanı yapısını detaylı olarak açıklamaktadır. Şema, multi-tenant yapı, rol bazlı yetkilendirme, çoklu para birimi desteği ve diğer gelişmiş özellikler göz önünde bulundurularak tasarlanmıştır. Geliştirme süreci boyunca, performans ve ölçeklenebilirlik gereksinimleri doğrultusunda bazı değişiklikler yapılabilir.
