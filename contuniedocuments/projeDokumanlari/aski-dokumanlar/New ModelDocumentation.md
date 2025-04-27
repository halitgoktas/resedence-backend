# Backend Model Dokümantasyonu




## İçindekiler
- [1. Temel Model Yapısı](#1-temel-model-yapısı)
- [2. Yapı/Bina Modelleri](#2-yapıbina-modelleri)
  - [2.1 Site Modeli](#21-site-modeli)
  - [2.2 Block (Blok) Modeli](#22-block-blok-modeli)
  - [2.3 Apartment (Daire) Modeli](#23-apartment-daire-modeli)
  - [2.4 ApartmentOwner (Daire Sahibi) Modeli](#24-apartmentowner-daire-sahibi-modeli)
  - [2.5 ApartmentResident (Daire Sakini) Modeli](#25-apartmentresident-daire-sakini-modeli)
- [3. Sistem Modelleri](#3-sistem-modelleri)
  - [3.1 User (Kullanıcı) Modeli](#31-user-kullanıcı-modeli)
  - [3.2 UserRole (Kullanıcı Rolü) Modeli](#32-userrole-kullanıcı-rolü-modeli)

## 1. Temel Model Yapısı

Rezidans Yönetimi uygulaması, tüm modellerin temel aldığı `BaseEntity` sınıfını kullanmaktadır. Bu sınıf, her varlık için aşağıdaki ortak özellikleri sağlar:

- **Id**: Benzersiz tanımlayıcı
- **FirmaId**: Entity'nin hangi firmaya ait olduğu (multi-tenant yapısı için)
- **SubeId**: Entity'nin hangi şubeye ait olduğu (multi-tenant yapısı için)
- **CreatedDate**: Oluşturulma tarihi
- **UpdatedDate**: Son güncelleme tarihi
- **CreatedBy**: Oluşturan kullanıcı ID'si
- **UpdatedBy**: Güncelleyen kullanıcı ID'si
- **IsDeleted**: Silindi durumu (soft delete için)
- **IsActive**: Aktif durumu

## 2. Yapı/Bina Modelleri

### 2.1 Site Modeli

`Site` sınıfı, konut sitelerini temsil eden modeldir.

#### Sorumlulukları:
- Site genel bilgilerini saklamak
- Site adres ve konum bilgilerini yönetmek
- Site özellikleri ve olanaklarını tanımlamak
- Site blokları ile ilişkiyi sağlamak
- Aidat hesaplama yöntemini belirlemek

#### Özellikler:
- **SiteName**: Site adı
- **Description**: Site açıklaması
- **Address**: Site adresi
- **City**: İl
- **District**: İlçe
- **PostalCode**: Posta kodu
- **TotalBlocks**: Toplam blok sayısı
- **TotalApartments**: Toplam daire sayısı
- **TotalArea**: Site yüzölçümü (m²)
- **GreenArea**: Yeşil alan yüzölçümü (m²)
- **ParkingCapacity**: Otopark kapasitesi
- **EstablishmentDate**: Kurulum/yapım tarihi
- **SiteManagerId**: Site yöneticisi ID'si
- **ManagementCompanyId**: Site yönetici şirketi ID'si
- **Status**: Sitenin durumu (SiteStatus enum)
- **Amenities**: Sitenin sahip olduğu olanaklar
- **DuesCalculationType**: Aidat tutarının hesaplanma şekli (DuesCalculationType enum)

#### İlişkiler:
- **Blocks**: Site içerisindeki blokların koleksiyonu

#### SiteStatus Enum Değerleri:
- **UnderConstruction (1)**: İnşaat aşamasında
- **Active (2)**: Aktif
- **UnderMaintenance (3)**: Bakım/Yenileme sürecinde
- **Inactive (4)**: Pasif

#### DuesCalculationType Enum Değerleri:
- **Fixed (1)**: Sabit - tüm daireler aynı aidatı öder
- **AreaBased (2)**: Metrekare bazlı - daire büyüklüğüne göre
- **RoomBased (3)**: Oda sayısına göre
- **Mixed (4)**: Karma hesaplama

### 2.2 Block (Blok) Modeli

`Block` sınıfı, site içerisindeki blokları temsil eden modeldir.

#### Sorumlulukları:
- Blok genel bilgilerini saklamak
- Blok fiziksel özelliklerini yönetmek
- Blok ve daireler arasındaki ilişkiyi sağlamak

#### Özellikler:
- **SiteId**: Bağlı olduğu sitenin ID'si
- **Site**: Site bilgisi (Navigation property)
- **BlockName**: Blok adı veya kodu (A Blok, B Blok, vb.)
- **Description**: Blok açıklaması
- **TotalFloors**: Toplam kat sayısı
- **TotalApartments**: Toplam daire sayısı
- **ApartmentsPerFloor**: Her kattaki daire sayısı
- **BuildingArea**: Bina yüzölçümü (m²)
- **ConstructionYear**: Blok yapım yılı
- **BlockType**: Blok türü (BlockType enum)
- **BlockManagerId**: Blok yöneticisi ID'si
- **NumberOfElevators**: Asansör sayısı
- **HeatingSystem**: Isınma sistemi türü

#### İlişkiler:
- **Apartments**: Blok içinde bulunan dairelerin koleksiyonu

#### BlockType Enum Değerleri:
- **Apartment (1)**: Apartman
- **Villa (2)**: Villa
- **Residence (3)**: Rezidans
- **BusinessCenter (4)**: İş merkezi
- **MixedUse (5)**: Karma kullanım

### 2.3 Apartment (Daire) Modeli

`Apartment` sınıfı, rezidans veya site içindeki daireleri temsil eden modeldir.

#### Sorumlulukları:
- Daire fiziksel özelliklerini saklamak (alan, oda sayısı, vb.)
- Daire durumunu ve mülkiyet bilgisini yönetmek
- Daire sakinleri ve sahipleri ile ilişkiyi sağlamak
- Aidat bilgilerini yönetmek

#### Özellikler:
- **BlockId**: Bağlı olduğu blok ID'si
- **Block**: Blok bilgisi (Navigation property)
- **ApartmentNumber**: Daire numarası
- **Floor**: Dairenin bulunduğu kat
- **ApartmentType**: Daire tipi (1+1, 2+1, vb.)
- **GrossArea**: Daire brüt alanı (m²)
- **NetArea**: Daire net alanı (m²)
- **NumberOfRooms**: Oda sayısı
- **NumberOfLivingRooms**: Salon sayısı
- **NumberOfBathrooms**: Banyo sayısı
- **NumberOfBalconies**: Balkon sayısı
- **Status**: Dairenin durumu (ApartmentStatus enum)
- **OwnershipType**: Dairenin mülkiyet türü (OwnershipType enum)
- **OwnerId**: Daire sahibi ID'si
- **TenantId**: Kiracı ID'si
- **Description**: Daire açıklaması
- **DuesAmount**: Dairenin aidat tutarı
- **LastDuesPaymentDate**: Son aidat ödeme tarihi
- **DuesCoefficient**: Daire aidat katsayısı
- **HeatingType**: Daire ısıtma tipi
- **Notes**: Notlar

#### İlişkiler:
- **Owners**: Daire sahiplerinin listesi (ApartmentOwner koleksiyonu)
- **Residents**: Dairede ikamet edenlerin listesi (ApartmentResident koleksiyonu)

#### ApartmentStatus Enum Değerleri:
- **Vacant (1)**: Boş
- **OccupiedByOwner (2)**: Sahibi tarafından kullanılıyor
- **OccupiedByTenant (3)**: Kiracı tarafından kullanılıyor
- **ForSale (4)**: Satışta
- **ForRent (5)**: Kiralık

#### OwnershipType Enum Değerleri:
- **Individual (1)**: Bireysel
- **Corporate (2)**: Kurumsal

### 2.4 ApartmentOwner (Daire Sahibi) Modeli

`ApartmentOwner` sınıfı, daire sahipliği (malik) bilgilerini tutan modeldir.

#### Sorumlulukları:
- Daire ve sahibi arasındaki ilişkiyi yönetmek
- Malik bilgilerini saklamak
- Sahiplik oranlarını ve sürelerini takip etmek
- Tapu bilgilerini saklamak

#### Özellikler:
- **ApartmentId**: Daire ID'si
- **UserId**: Malik/Sahip ID'si (Kullanıcı tablosuna referans)
- **OwnershipPercentage**: Sahiplik oranı (% olarak)
- **StartDate**: Sahiplik başlangıç tarihi
- **EndDate**: Sahiplik bitiş tarihi (satış yapıldığında)
- **DeedInformation**: Tapu bilgisi veya nosu

#### İlişkiler:
- **Apartment**: Daire ile ilişkisi (navigation property)
- **User**: Malik/Sahip ile ilişkisi (navigation property)

### 2.5 ApartmentResident (Daire Sakini) Modeli

`ApartmentResident` sınıfı, dairede ikamet eden kişi bilgilerini tutan modeldir.

#### Sorumlulukları:
- Dairede ikamet eden kişi bilgilerini saklamak
- İkamet türünü (kiracı, malik, aile üyesi vb.) yönetmek
- İkamet sürelerini takip etmek
- Kira kontrat bilgilerini saklamak

#### Özellikler:
- **ApartmentId**: Daire ID'si
- **UserId**: İkamet eden kişi ID'si (Kullanıcı tablosuna referans)
- **ResidenceType**: İkamet türü (Kiracı, Aile Üyesi, Misafir, vb.)
- **StartDate**: İkamet başlangıç tarihi
- **EndDate**: İkamet bitiş tarihi
- **NumberOfResidents**: İkamet ettiği kişi sayısı
- **MonthlyRent**: Aylık kira bedeli (Eğer kiracı ise)
- **ContractReference**: Kontrat bilgisi veya dosya referansı

#### İlişkiler:
- **Apartment**: Daire ile ilişkisi (navigation property)
- **User**: İkamet eden kişi ile ilişkisi (navigation property)

#### ResidenceType Enum Değerleri:
- **Tenant (1)**: Kiracı
- **Owner (2)**: Daire sahibi
- **FamilyMember (3)**: Aile üyesi
- **Guest (4)**: Misafir
- **Other (5)**: Diğer

## 3. Sistem Modelleri

### 3.1 User (Kullanıcı) Modeli

`User` sınıfı, sistemdeki kullanıcıları temsil eden modeldir.

#### Sorumlulukları:
- Kullanıcı kimlik bilgilerini saklamak
- Kullanıcı iletişim bilgilerini yönetmek
- Kullanıcı tercihlerini ve ayarlarını saklamak
- Kullanıcı hesap bilgilerini yönetmek

#### Özellikler:
- **FirstName**: Kullanıcının adı
- **LastName**: Kullanıcının soyadı
- **Email**: Kullanıcının e-posta adresi (aynı zamanda kullanıcı adı olarak kullanılır)
- **PasswordHash**: Şifre hash'i
- **Phone**: Telefon numarası
- **IsActive**: Kullanıcı aktif mi?
- **LastLoginDate**: Son giriş tarihi
- **PreferredLanguage**: Tercih edilen dil kodu (tr, en, de, ar, ru, vb.)
- **PreferredCurrency**: Tercih edilen para birimi (TRY, USD, EUR, vb.)
- **PreferredDateFormat**: Tercih edilen tarih formatı
- **PreferredTimeFormat**: Tercih edilen zaman formatı
- **PreferredNumberFormat**: Tercih edilen sayı formatı bölgesel ayarı
- **ThemePreference**: Kullanıcı tema tercihi (Light, Dark, System)
- **ReceiveEmailNotifications**: E-posta bildirimlerini al
- **ReceiveSmsNotifications**: SMS bildirimlerini al
- **ReceivePushNotifications**: Uygulama içi bildirimlerini al
- **ApartmentId**: Kullanıcının bağlı olduğu daire ID'si (varsa)

#### İlişkiler:
- **Apartment**: Kullanıcının bağlı olduğu daire
- **UserRoles**: Kullanıcının rolleri
- **AccessCards**: Kullanıcının erişim kartları
- **Notifications**: Kullanıcının bildirimleri

### 3.2 UserRole (Kullanıcı Rolü) Modeli

`UserRole` sınıfı, sistemdeki kullanıcı rollerini temsil eden modeldir.

#### Sorumlulukları:
- Kullanıcı yetki seviyelerini tanımlamak
- Kullanıcı ve rol arasındaki ilişkiyi yönetmek

#### Enum Değerleri:
- **Admin (1)**: Sistem yöneticisi
- **FirmaYoneticisi (2)**: Firma yöneticisi
- **SiteYoneticisi (3)**: Site yöneticisi
- **Personel (4)**: Site personeli
- **Sakin (5)**: Daire sakini/kullanıcısı
- **Misafir (6)**: Misafir kullanıcı 