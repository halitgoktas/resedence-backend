# Identity Entities

## Firma Entity

### Tanım

`Firma` entity sınıfı, Residence Manager sistemini kullanan ve yöneten şirketleri temsil eder. Her firma sistemde kendi rezidanslarını, sitelerini ve şubelerini yönetebilir. 

### Multi-Tenant Yapı

Bu entity, sistemin çok kiracılı (multi-tenant) mimarisinin temel yapı taşıdır:

- Her firma kendine özgü bir `FirmaId` değerine sahiptir
- Firma altında birden çok şube (`Sube`) bulunabilir
- Firmaya bağlı kullanıcılar (`Kullanici`) firma verilerine erişim sağlar
- Tüm tenant-specific veriler ilgili firma ve şube ID'leri ile filtrelenir

### Özellikler

| Özellik | Tip | Açıklama |
|---------|-----|----------|
| Id | int | Birincil anahtar |
| FirmaId | int | Tenant kimliği (Id ile aynı değerde) |
| Name | string | Firma adı |
| TaxNumber | string | Vergi numarası |
| Address | string | Adres bilgisi |
| Phone | string | Telefon numarası |
| Email | string | E-posta adresi | 
| WebSite | string | Web sitesi |
| LogoUrl | string | Logo URL |
| SubscriptionStartDate | DateTime | Abonelik başlangıç tarihi |
| SubscriptionEndDate | DateTime? | Abonelik bitiş tarihi (null olabilir) |

### İlişkiler

`Firma` entity'si aşağıdaki entity'lerle ilişkilidir:

- `Sube`: Bir firmaya birden çok şube bağlı olabilir (1:N)
- `Kullanici`: Bir firmaya birden çok kullanıcı bağlı olabilir (1:N)

### Kullanım

Bu entity genellikle aşağıdaki senaryolarda kullanılır:

1. Yeni bir firma tanımlama/kaydetme
2. Firma bilgilerini güncelleme
3. Firma abonelik durumunu kontrol etme
4. Multi-tenant veri filtreleme (FirmaId üzerinden)
5. Firma-şube ilişkilerini yönetme

### Oluşturma/Güncelleme Prosedürü

- Yeni bir firma oluşturulduğunda constructor içinde koleksiyonlar başlatılır
- FirmaId değeri otomatik olarak Id değerine eşitlenir
- IsActive varsayılan olarak true ayarlanır
- Tüm temel entity özellikleri BaseEntity'den miras alınır

---

## Sube Entity

### Tanım

`Sube` entity sınıfı, bir firmaya ait şubeleri temsil eder. Şubeler, bir firmanın farklı fiziksel lokasyonlardaki operasyonlarını ifade eder.

### Multi-Tenant Yapı

Multi-tenant yapıda ikinci seviye olarak görev yapar:

- Her şube bir firmaya (`FirmaId`) bağlıdır
- Her şube kendine özgü bir `SubeId` değerine sahiptir
- Verilerin çoğu hem firma hem de şube bazında filtrelenebilir
- Şube 0 (sıfır) değeri, firmanın tüm şubelerinde geçerli kayıtları temsil eder

### Özellikler

| Özellik | Tip | Açıklama |
|---------|-----|----------|
| Id | int | Birincil anahtar |
| FirmaId | int | Bağlı olduğu firmanın ID'si |
| SubeId | int | Şube ID'si (Id ile aynı değerde) |
| Name | string | Şube adı |
| Address | string | Adres bilgisi | 
| Phone | string | Telefon numarası |
| Email | string | E-posta adresi |
| ManagerName | string | Şube sorumlusu |
| OpeningDate | DateTime | Açılış tarihi |
| ClosingDate | DateTime? | Kapanış tarihi (null olabilir) |
| Region | string | Şubenin bulunduğu bölge/şehir |

### İlişkiler

`Sube` entity'si aşağıdaki entity'lerle ilişkilidir:

- `Firma`: Her şube bir firmaya bağlıdır (N:1)
- `Kullanici`: Bir şubeye birden çok kullanıcı bağlı olabilir (1:N)

### Kullanım

Bu entity genellikle aşağıdaki senaryolarda kullanılır:

1. Firma altında yeni şubeler oluşturma
2. Şube bazlı operasyonları yönetme
3. Şubeye özel veri filtreleme
4. Şube kullanıcılarını yönetme

### Oluşturma/Güncelleme Prosedürü

- Yeni bir şube oluşturulduğunda ilgili koleksiyonlar başlatılır
- SubeId değeri otomatik olarak Id değerine eşitlenir
- IsActive varsayılan olarak true ayarlanır
- FirmaId değeri, bağlı olduğu firmanın ID'si ile doldurulmalıdır 