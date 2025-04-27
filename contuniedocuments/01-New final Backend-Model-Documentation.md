# Backend Model Sorumlulukları

Bu doküman, Rezidans ve Site Yönetim Sistemi backend modellerinin sorumluluklarını ve ilişkilerini açıklar.

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

- SiteName: Site adı
- Description: Site açıklaması
- Address: Site adresi
- City: İl
- District: İlçe
- PostalCode: Posta kodu
- TotalBlocks: Toplam blok sayısı
- TotalApartments: Toplam daire sayısı
- TotalArea: Site yüzölçümü (m²)
- GreenArea: Yeşil alan yüzölçümü (m²)
- ParkingCapacity: Otopark kapasitesi
- EstablishmentDate: Kurulum/yapım tarihi
- SiteManagerId: Site yöneticisi ID'si
- ManagementCompanyId: Site yönetici şirketi ID'si
- Status: Sitenin durumu (SiteStatus enum)
- Amenities: Sitenin sahip olduğu olanaklar
- DuesCalculationType: Aidat tutarının hesaplanma şekli (DuesCalculationType enum)
- Blocks: Site içerisindeki blokların koleksiyonu

### 2.2. Blok Modeli (Block.cs)

Blok modeli, site içerisindeki blokları temsil eder.

- SiteId: Bağlı olduğu sitenin ID'si
- BlockName: Blok adı veya kodu
- Description: Blok açıklaması
- TotalFloors: Toplam kat sayısı
- TotalApartments: Toplam daire sayısı
- ApartmentsPerFloor: Her kattaki daire sayısı
- BuildingArea: Bina yüzölçümü (m²)
- ConstructionYear: Blok yapım yılı
- BlockType: Blok türü (BlockType enum)
- BlockManagerId: Blok yöneticisi ID'si
- NumberOfElevators: Asansör sayısı
- HeatingSystem: Isınma sistemi türü
- Apartments: Blok içinde bulunan dairelerin koleksiyonu

### 2.3. Daire Modeli (Apartment.cs)

Daire modeli, bir konut birimini temsil eder.

- BlockId: Bağlı olduğu blok ID'si
- ApartmentNumber: Daire numarası
- Floor: Dairenin bulunduğu kat
- ApartmentType: Daire tipi (1+1, 2+1, vb.)
- GrossArea: Daire brüt alanı (m²)
- NetArea: Daire net alanı (m²)
- NumberOfRooms: Oda sayısı
- NumberOfLivingRooms: Salon sayısı
- NumberOfBathrooms: Banyo sayısı
- NumberOfBalconies: Balkon sayısı
- Status: Dairenin durumu (ApartmentStatus enum)
- OwnershipType: Dairenin mülkiyet türü (OwnershipType enum)
- OwnerId: Daire sahibi ID'si
- TenantId: Kiracı ID'si
- Description: Daire açıklaması
- DuesAmount: Dairenin aidat tutarı
- LastDuesPaymentDate: Son aidat ödeme tarihi
- DuesCoefficient: Daire aidat katsayısı
- HeatingType: Isıtma tipi
- Notes: Notlar
- Owners: Daire sahiplerinin listesi
- Residents: Dairede ikamet edenlerin listesi

### 2.4. Daire Sahibi Modeli (ApartmentOwner.cs)

Daire sahibi modeli, daire sahipliği bilgilerini temsil eder.

- ApartmentId: Daire ID'si
- UserId: Malik/Sahip ID'si
- OwnershipPercentage: Sahiplik oranı (% olarak)
- StartDate: Sahiplik başlangıç tarihi
- EndDate: Sahiplik bitiş tarihi
- DeedInformation: Tapu bilgisi veya nosu
- Apartment: Daire ile ilişkisi
- User: Malik/Sahip ile ilişkisi

### 2.5. Daire Sakini Modeli (ApartmentResident.cs)

Daire sakini modeli, dairede ikamet eden kişi bilgilerini temsil eder.

- ApartmentId: Daire ID'si
- UserId: İkamet eden kişi ID'si
- ResidenceType: İkamet türü (Kiracı, Aile Üyesi, Misafir, vb.)
- StartDate: İkamet başlangıç tarihi
- EndDate: İkamet bitiş tarihi
- NumberOfResidents: İkamet ettiği kişi sayısı
- MonthlyRent: Aylık kira bedeli
- ContractReference: Kontrat bilgisi veya dosya referansı
- Apartment: Daire ile ilişkisi
- User: İkamet eden kişi ile ilişkisi

## 3. İşlem Modelleri

### 3.1. Finansal İşlem Modeli (FinancialTransaction.cs)

Finansal işlem modeli, site ile ilgili finansal işlemleri temsil eder.

- TransactionDate: İşlem tarihi
- TransactionType: İşlem tipi (Gelir, Gider)
- FinancialAccountTypeId: Hesap kalemi ID
- Description: İşlem açıklaması
- Amount: Tutar
- VATAmount: KDV tutarı
- VATRate: KDV oranı (%)
- ApartmentId: İşlemin bağlı olduğu daire ID
- PaymentMethod: Ödeme yöntemi
- BankAccountId: Banka hesabı ID
- InvoiceNumber: Fatura numarası
- InvoiceDate: Fatura tarihi
- DueDate: Son ödeme tarihi
- IsPaid: Ödendi mi?
- PaymentDate: Ödeme tarihi
- RelatedEntityType: İlişkili kayıt tipi (Daire, Blok, Site)
- RelatedEntityId: İlişkili kayıt ID'si
- PersonId: İlgili kişi ID'si

### 3.2. Aidat (Dues) İşlemleri

#### 3.2.1. Aidat Karşılaştırma (DuesReconciliation.cs)

- Period: Mahsuplaştırma dönemi
- StartDate: Mahsuplaştırma başlangıç tarihi
- EndDate: Mahsuplaştırma bitiş tarihi
- Description: Mahsuplaştırma açıklaması
- TotalIncome: Mahsuplaştırma toplam gelir tutarı
- TotalExpense: Mahsuplaştırma toplam gider tutarı
- TotalDuesAmount: Mahsuplaştırma toplam aidat tutarı
- TotalDuesDebt: Mahsuplaştırma toplam aidat borcu

### 3.3. Bakım Modeli (Maintenance.cs)

Bakım modeli, bakım ve onarım işlemlerini temsil eder.

- Title: Bakım başlığı
- Description: Bakım açıklaması
- MaintenanceDate: Bakım tarihi
- EstimatedEndDate: Tahmini bitiş tarihi
- ActualEndDate: Gerçekleşen bitiş tarihi
- Status: Bakım durumu (MaintenanceStatus enum)
- Cost: Bakım maliyeti
- Location: Bakım yapılan yer
- Contractor: Bakımı yapan firma veya kişi
- ApartmentId: Daire ID
- Apartment: Daire

### 3.4. Gider Modeli (Expense.cs)

Gider modeli, gider kayıtlarını temsil eder.

- Title: Gider başlığı
- Description: Gider açıklaması
- Amount: Gider tutarı
- ExpenseDate: Gider tarihi
- Type: Gider türü (ExpenseType enum)
- Status: Gider durumu (ExpenseStatus enum)
- InvoiceNumber: Fatura numarası
- InvoiceDate: Fatura tarihi
- Attachments: Gider ekleri
- PaymentMethod: Ödeme yöntemi
- PaymentDate: Ödeme tarihi

## 4. Kullanıcı ve Yetkilendirme Modelleri

### 4.1. Kullanıcı Modeli (User.cs)

Kullanıcı modeli, sistem kullanıcılarını temsil eder.

- FirstName: Kullanıcının adı
- LastName: Kullanıcının soyadı
- Email: Kullanıcının e-posta adresi
- PasswordHash: Şifre hash'i
- Phone: Telefon numarası
- IsActive: Kullanıcı aktif mi?
- LastLoginDate: Son giriş tarihi
- PreferredLanguage: Tercih edilen dil kodu
- PreferredCurrency: Tercih edilen para birimi
- UserRoles: Kullanıcının rolleri

### 4.2. Rol Modeli (Role.cs)

Rol modeli, sistem içerisindeki rolleri tanımlar.

- Name: Rol adı
- Description: Rol açıklaması
- CompanyId: Rolün ait olduğu firma ID'si
- BranchId: Rolün ait olduğu şube ID'si
- IsSystemDefined: Rolün sistem tanımlı olup olmadığı
- IsActive: Rolün aktif olup olmadığı
- RolePermissions: Role ait olan tüm rol-izin ilişkileri
- UserRoles: Role sahip olan tüm kullanıcılar

### 4.3. İzin Modeli (Permission.cs)

İzin modeli, sistem izinlerini tanımlar.

- Name: İzin adı (örn: Kullanıcı Ekle, Daire Görüntüle)
- Code: İzin kodu - benzersiz tanımlayıcı (örn: USER_CREATE, APARTMENT_VIEW)
- Description: İzin açıklaması
- Category: İzin kategorisi (örn: Kullanıcı Yönetimi, Daire Yönetimi)
- IsActive: İznin aktif olup olmadığı
- RolePermissions: İzinle ilişkili rol izinleri
- UserPermissions: İzinle ilişkili kullanıcı izinleri

### 4.4. Rol-İzin İlişkisi (RolePermission.cs)

Rol ve izin arasındaki ilişkiyi tanımlar.

- RoleId: Rol ID'si
- PermissionId: İzin ID'si
- CompanyId: İzin ait olduğu firma ID'si
- BranchId: İzin ait olduğu şube ID'si
- IsActive: İznin aktif olup olmadığı
- Role: Rol ilişkisi
- Permission: İzin ilişkisi

### 4.5. Kullanıcı-İzin İlişkisi (UserPermission.cs)

Kullanıcı ve izin arasındaki ilişkiyi tanımlar.

- UserId: Kullanıcı ID'si
- PermissionId: İzin ID'si
- CompanyId: İzin ait olduğu firma ID'si
- BranchId: İzin ait olduğu şube ID'si
- IsGranted: İzin verildi mi?
- IsActive: İznin aktif olup olmadığı
- User: Kullanıcı ilişkisi
- Permission: İzin ilişkisi

## 5. Enum Değerleri

Sistem içerisinde kullanılan önemli enum değerleri:

### 5.1. Kullanıcı Rolleri (UserRoleEnum)

- SystemAdmin: Sistem yöneticisi
- FirmaYoneticisi: Firma yöneticisi
- SiteAdmin: Site yöneticisi
- Personel: Site personeli
- Sakin: Daire sakini/kullanıcısı
- MalSahibi: Mal sahibi
- Teknik: Teknik ekip
- Misafir: Ziyaretçi/Misafir kullanıcı

### 5.2. Daire Durumu (ApartmentStatus)

- Vacant: Boş
- OccupiedByOwner: Sahibi tarafından kullanılıyor
- OccupiedByTenant: Kiracı tarafından kullanılıyor
- ForSale: Satışta
- ForRent: Kiralık

### 5.3. Site Durumu (SiteStatus)

- UnderConstruction: İnşaat aşamasında
- Active: Aktif
- UnderMaintenance: Bakım/Yenileme sürecinde
- Inactive: Pasif

### 5.4. İzin Seviyeleri (PermissionLevelEnum)

- None: Erişim yok - hiçbir işlem yapılamaz
- View: Sadece görüntüleme izni
- Basic: Temel işlemler (görüntüleme + ekleme)
- Standard: Standart işlemler (görüntüleme + ekleme + düzenleme)
- Full: Tam erişim (görüntüleme + ekleme + düzenleme + silme)
- Admin: Yönetici erişimi (tüm işlemler + yönetimsel yetkiler)

## 6. Multi-tenant Yapısı

Sistem, multi-tenant bir yapıda tasarlanmıştır. Bu yapıya göre:

- Tüm tablolarda FirmaId ve SubeId alanları bulunur
- Kullanıcı hangi firma ve şubeye tanımlandıysa, o firma ve şubeye ait verilere erişebilir
- Veri sorgularında firma ve şube bazlı filtreleme otomatik olarak uygulanır

## 7. İlişki Yapısı

Sistemdeki temel ilişki yapısı şu şekildedir:

- Site -> Block: Bir site, birden fazla blok içerebilir
- Block -> Apartment: Bir blok, birden fazla daire içerebilir
- Apartment -> ApartmentOwner: Bir daire, birden fazla daire sahibi (hissedar) içerebilir
- Apartment -> ApartmentResident: Bir daire, birden fazla ikamet eden kişi içerebilir
- User -> Role: Bir kullanıcı, birden fazla role sahip olabilir
- Role -> Permission: Bir rol, birden fazla izne sahip olabilir
- User -> Permission: Bir kullanıcı, rollere ek olarak doğrudan izinlere sahip olabilir

Bu dokümantasyon, sistemdeki temel model yapılarını ve sorumluluklarını özetlemektedir. Geliştirme sürecinde model yapısında değişiklikler yapılabilir, ancak temel mimari bu şekilde tasarlanmıştır. 