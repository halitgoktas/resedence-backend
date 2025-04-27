# Rezidans ve Site Yönetim Sistemi - Veritabanı Şeması

Bu döküman, Rezidans ve Site Yönetim Sistemi için tasarlanan veritabanı şemasını, tablolarını ve ilişkilerini içermektedir.

## Döküman Bağlantıları
- BACKEND_MODEL_YAPISI.md - Backend model yapısı ve ilişkiler
- TEKNIK_MIMARISI.md - Teknik altyapı ve mimari yapı

## Sunucu Bilgileri
- Sunucu: 127.0.0.1
- SQL Kullanıcı Adı: sa
- SQL Şifre: q.1

## 1. Veritabanı Temel Prensipleri

1. **Multi-tenant Yapı:** Tüm tablolarda FirmaID ve SubeID alanları bulunacaktır.
2. **Lookup ve Transaction Yapısı:** Tanım tabloları (Lookup) ve hareket tabloları (Transaction) olarak iki ana kategori.
3. **İlişkili Veri Yapısı:** Tablolar arasında güçlü ilişkiler kurulacaktır.
4. **Ortak Alanlar:** Tüm tablolarda ortak alanlar (ID, CreatedAt, UpdatedAt, IsActive vb.) kullanılacaktır.

## 2. Temel Sınıflar ve Tablolar

### 2.1. BaseEntity
Bu sınıf, tüm tabloların temel alanlarını içerir:

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| ID | int, PK | Benzersiz tanımlayıcı |
| FirmaID | int, FK | Firma ID'si |
| SubeID | int, FK | Şube ID'si |
| CreatedAt | datetime | Oluşturulma tarihi |
| CreatedBy | int | Oluşturan kullanıcı ID'si |
| UpdatedAt | datetime | Güncellenme tarihi |
| UpdatedBy | int | Güncelleyen kullanıcı ID'si |
| IsActive | bit | Aktif durumu |
| IsDeleted | bit | Silinmiş durumu |

### 2.2. BaseLookupEntity (BaseEntity'den türer)
Referans/tanım tabloları için kullanılan temel sınıf:

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseEntity alanları* | | |
| Code | nvarchar(50) | Kod |
| Name | nvarchar(100) | Ad |
| Description | nvarchar(500) | Açıklama |
| DisplayOrder | int | Görüntülenme sırası |

### 2.3. BaseTransactionEntity (BaseEntity'den türer)
İşlem tabloları için kullanılan temel sınıf:

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseEntity alanları* | | |
| TransactionDate | datetime | İşlem tarihi |
| ReferenceNumber | nvarchar(50) | Referans numarası |
| Notes | nvarchar(500) | Notlar |

## 3. Tanım (Lookup) Tabloları

### 3.1. Firma (Company)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| TaxNumber | nvarchar(20) | Vergi numarası |
| Address | nvarchar(500) | Adres |
| PhoneNumber | nvarchar(20) | Telefon numarası |
| Email | nvarchar(100) | E-posta adresi |
| LogoPath | nvarchar(255) | Logo dosya yolu |
| WebsiteUrl | nvarchar(255) | Web sitesi URL'si |
| PreferredCurrency | nvarchar(10) | Tercih edilen para birimi (TRY, EUR, USD, GBP) |
| PreferredLanguage | nvarchar(10) | Tercih edilen dil (tr, en, de, ru, ar, fa) |

### 3.2. Şube (Branch)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| FirmaID | int, FK | Firma ID'si |
| Address | nvarchar(500) | Adres |
| PhoneNumber | nvarchar(20) | Telefon numarası |
| Email | nvarchar(100) | E-posta adresi |
| ManagerName | nvarchar(100) | Yönetici adı |
| PreferredCurrency | nvarchar(10) | Tercih edilen para birimi |
| PreferredLanguage | nvarchar(10) | Tercih edilen dil |

### 3.3. Kullanıcı Tipleri (UserType)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Permissions | nvarchar(MAX) | İzinler (JSON) |

### 3.4. Kullanıcılar (User)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| UserTypeID | int, FK | Kullanıcı tipi ID'si |
| Username | nvarchar(50) | Kullanıcı adı |
| PasswordHash | nvarchar(255) | Şifre hash'i |
| Email | nvarchar(100) | E-posta adresi |
| PhoneNumber | nvarchar(20) | Telefon numarası |
| FirstName | nvarchar(50) | Adı |
| LastName | nvarchar(50) | Soyadı |
| LastLoginDate | datetime | Son giriş tarihi |
| AccessExpiryDate | datetime | Erişim sona erme tarihi |
| ProfilePicturePath | nvarchar(255) | Profil resmi dosya yolu |
| TwoFactorEnabled | bit | İki faktörlü doğrulama aktif mi |
| TwoFactorKey | nvarchar(100) | İki faktörlü doğrulama anahtarı |
| PreferredCurrency | nvarchar(10) | Tercih edilen para birimi |
| PreferredLanguage | nvarchar(10) | Tercih edilen dil |

### 3.5. Rol (Role)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Name | nvarchar(50) | Rol adı |
| IsSystemDefined | bit | Sistem tanımlı mı |

### 3.6. Departman (Department)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| ResponsibleName | nvarchar(100) | Sorumlu kişinin adı |
| ResponsiblePhone | nvarchar(20) | Sorumlu kişinin telefonu |
| Email | nvarchar(100) | E-posta adresi |

### 3.7. Site (Site)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Address | nvarchar(500) | Adres |
| City | nvarchar(50) | İl |
| District | nvarchar(50) | İlçe |
| TotalArea | decimal(18,2) | Toplam alan (m²) |
| NumberOfBuildings | int | Bina sayısı |
| NumberOfApartments | int | Daire sayısı |
| ManagerName | nvarchar(100) | Site yöneticisinin adı |
| ManagerPhone | nvarchar(20) | Site yöneticisinin telefonu |
| Status | int | Site durumu (enum: UnderConstruction, Active, UnderMaintenance, Inactive) |
| Amenities | nvarchar(MAX) | Site olanakları (JSON) |
| DuesCalculationType | int | Aidat hesaplama tipi (enum: Fixed, AreaBased, RoomBased, Mixed) |
| EstablishmentDate | datetime | Kuruluş/yapım tarihi |

### 3.8. Bina (Building)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| SiteID | int, FK | Site ID'si |
| Address | nvarchar(500) | Adres |
| NumberOfFloors | int | Kat sayısı |
| BuildYear | int | Yapım yılı |
| NumberOfApartments | int | Daire sayısı |
| ManagerName | nvarchar(100) | Bina yöneticisinin adı |
| ManagerPhone | nvarchar(20) | Bina yöneticisinin telefonu |
| BlockType | int | Blok tipi (enum: Apartment, Villa, Residence, Business, Mixed) |
| NumberOfElevators | int | Asansör sayısı |
| HeatingSystem | nvarchar(50) | Isıtma sistemi |

### 3.9. Blok (Block)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| BuildingID | int, FK | Bina ID'si |
| BlockName | nvarchar(50) | Blok adı |
| NumberOfFloors | int | Kat sayısı |
| NumberOfApartments | int | Daire sayısı |
| NumberOfElevators | int | Asansör sayısı |

### 3.10. Daire (Apartment)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| BlockID | int, FK | Blok ID'si |
| ApartmentNumber | nvarchar(20) | Daire numarası |
| Floor | int | Bulunduğu kat |
| ApartmentType | nvarchar(20) | Daire tipi (1+1, 2+1, 3+1, vb.) |
| GrossSqm | decimal(18,2) | Brüt alan (m²) |
| NetSqm | decimal(18,2) | Net alan (m²) |
| NumberOfRooms | int | Oda sayısı |
| NumberOfLivingRooms | int | Salon sayısı |
| NumberOfBathrooms | int | Banyo sayısı |
| Status | int | Daire durumu (enum: Vacant, OccupiedByOwner, OccupiedByTenant, ForSale, ForRent) |
| OwnershipType | int | Mülkiyet tipi (enum: Individual, Corporate) |
| MonthlyFee | decimal(18,2) | Aylık aidat tutarı |
| Features | nvarchar(MAX) | Daire özellikleri (JSON) |
| IsRentable | bit | Kiralanabilir mi |
| RentalFeePerDay | decimal(18,2) | Günlük kira bedeli |
| RentalFeePerWeek | decimal(18,2) | Haftalık kira bedeli |
| RentalFeePerMonth | decimal(18,2) | Aylık kira bedeli |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| HeatingType | nvarchar(50) | Isıtma tipi |

### 3.11. Aktivite Alanları (ActivityArea)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| SiteID | int, FK | Site ID'si |
| IsPaid | bit | Ücretli mi |
| PricePerHour | decimal(18,2) | Saatlik ücret |
| PricePerDay | decimal(18,2) | Günlük ücret |
| PricePerPerson | decimal(18,2) | Kişi başı ücret |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| Capacity | int | Kapasite |
| AvailableTimeStart | time | Kullanım başlangıç saati |
| AvailableTimeEnd | time | Kullanım bitiş saati |
| MinimumReservationHours | int | Minimum rezervasyon süresi (saat) |
| Features | nvarchar(MAX) | Özellikler (JSON) |
| RequiresApproval | bit | Onay gerektiriyor mu |

### 3.12. Hizmet Türleri (ServiceType)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Price | decimal(18,2) | Fiyat |
| Currency | nvarchar(10) | Para birimi (TRY, EUR, USD, GBP) |
| EstimatedDuration | int | Tahmini süre (dakika) |
| RequiresApproval | bit | Onay gerektiriyor mu |
| ChargeToApartment | bit | Daire hesabına yansıtılsın mı |
| DepartmentID | int, FK | Departman ID'si |

### 3.13. Para Birimleri (Currency)

| Alan Adı | Veri Tipi | Açıklama |
|----------|-----------|----------|
| *BaseLookupEntity alanları* | | |
| Symbol | nvarchar