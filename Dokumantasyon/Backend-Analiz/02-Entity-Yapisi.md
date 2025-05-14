# Mekik Residence Manager - Entity Yapısı ve İlişkileri

Bu dokümantasyon, Mekik Residence Manager projesindeki entity modelini, entity sınıflarını ve bunlar arasındaki ilişkileri detaylı olarak açıklamaktadır.

## Temel Entity Yapısı

### BaseEntity Sınıfı

Projede tüm entity'ler `BaseEntity` sınıfından türemektedir. Bu sınıf multi-tenant yapıyı desteklemek için `ITenant` arayüzünü uygulamaktadır.

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

### Diğer Temel Entity Türleri

BaseEntity'den türeyen diğer özelleştirilmiş entity tipleri şunlardır:

1. **BaseLookupEntity**: Referans veri tipleri için kullanılır
2. **BaseTransactionEntity**: İşlem tabanlı entity'ler için kullanılır
3. **BaseSystemEntity**: Sistem genelinde kullanılan entity'ler için kullanılır

## Ana Entity Sınıfları

### Şirket ve Şube Yapısı

1. **Company**: Firma bilgilerini içerir
   - Firmaya bağlı şubeleri temsil eden `Branches` collection navigation property'si
   - Firma adı, vergi no, adres gibi temel bilgiler

2. **Branch**: Şube bilgilerini içerir
   - Bağlı olduğu firmayı temsil eden `Company` navigation property'si
   - Şube adı, adres, telefon gibi temel bilgiler
   - Şubeye bağlı blok ve daireleri temsil eden navigation property'ler

### Mülk ve Bina Yapısı

1. **Block**: Apartman bloklarını temsil eder
   - Blok adı, kat sayısı, daire sayısı gibi bilgiler
   - Bloktaki daireleri temsil eden `Apartments` collection property'si

2. **Apartment**: Daire bilgilerini içerir
   - Daire numarası, kat, metrekare, oda sayısı, balkon sayısı gibi özellikler
   - Bağlı olduğu bloğu temsil eden `Block` navigation property'si
   - Dairede oturan sakinleri temsil eden `ApartmentResidents` collection property'si
   - Daire sahibini temsil eden `Owner` navigation property'si

3. **Equipment**: Dairelerdeki ekipman ve demirbaşları temsil eder
   - Ekipman türü, modeli, seri numarası, garanti bilgileri
   - Bağlı olduğu daireyi temsil eden `Apartment` navigation property'si

### Kişi ve Sakin Yapısı

1. **Person**: Temel kişi bilgilerini içerir
   - Ad, soyad, TC kimlik, doğum tarihi, cinsiyet, medeni hal gibi demografik bilgiler
   - İletişim bilgileri (telefon, e-posta, adres)
   - Kişi tipi (sakin, personel, ziyaretçi vb.)

2. **Resident**: Sakin bilgilerini içerir, Person'dan türer
   - Oturduğu daireleri temsil eden `ApartmentResidents` collection property'si
   - Sakin tipi (kiracı, ev sahibi, aile üyesi vb.)

### Hizmet ve Talep Yapısı

1. **ServiceCategory**: Hizmet kategorilerini temsil eder
   - Kategori adı, açıklaması
   - Kategoriye bağlı hizmet tiplerini temsil eden `ServiceTypes` collection property'si

2. **ServiceRequest**: Sakinlerden gelen hizmet taleplerini temsil eder
   - Talep eden sakini temsil eden `Resident` navigation property'si
   - Talep edilen hizmet türünü temsil eden `ServiceType` navigation property'si
   - Talep durumu, öncelik seviyesi, açıklama, talep tarihi gibi bilgiler
   - Atanan personeli temsil eden `AssignedStaff` navigation property'si

### Aidat ve Finansal Yapı

1. **DuesDefinition**: Aidat tanımlamalarını içerir
   - Aidat dönemi, tutar, son ödeme tarihi gibi bilgiler
   - İlgili daire tipini veya bloğu belirten referanslar

2. **Payment**: Ödeme işlemlerini temsil eder
   - Ödeme yapan kişiyi temsil eden `Person` navigation property'si
   - Ödeme türü, tutarı, tarihi, açıklaması
   - Ödeme yöntemi, ödeme durumu
   - İlgili aidat veya faturayı temsil eden referanslar

### Bakım ve Demirbaş Yapısı

1. **MaintenancePlan**: Bakım planlarını temsil eder
   - Bakım tipi, periyodu, sorumlu personel
   - İlgili ekipman veya alanı belirten referanslar

2. **MaintenanceCost**: Bakım maliyetlerini temsil eder
   - Maliyet tutarı, tarihi, açıklaması
   - İlgili bakım planını temsil eden `MaintenancePlan` navigation property'si

### Bildirim ve İletişim Yapısı

1. **NotificationTemplate**: Bildirim şablonlarını temsil eder
   - Şablon adı, içeriği, türü (e-posta, SMS, uygulama bildirimi)
   - Şablonda kullanılan değişkenler

2. **EmailLog**: E-posta gönderim kayıtlarını temsil eder
   - Gönderilen e-posta adresi, konu, içerik, gönderim tarihi
   - Gönderim durumu, hata mesajları

3. **EmailTemplate**: E-posta şablonlarını temsil eder
   - Şablon adı, konusu, HTML içeriği, açıklaması
   - Şablonda kullanılan değişkenler

### Kimlik ve Güvenlik Yapısı

1. **User**: Kullanıcı hesaplarını temsil eder
   - Kullanıcı adı, e-posta, şifre hash'i
   - Rol ve yetki bilgileri
   - Son giriş tarihi, hesap durumu

## Entity İlişkileri

Projedeki temel entity ilişkileri şu şekildedir:

1. **Company - Branch**: One-to-Many (Bir firmaya birden çok şube bağlı olabilir)
2. **Branch - Block**: One-to-Many (Bir şubeye birden çok blok bağlı olabilir)
3. **Block - Apartment**: One-to-Many (Bir blokta birden çok daire bulunabilir) 
4. **Apartment - Resident**: Many-to-Many (Bir dairede birden çok sakin olabilir, bir sakin birden çok dairede kayıtlı olabilir)
5. **Resident - Person**: Inheritance (Sakin, Person sınıfından türer)
6. **ServiceCategory - ServiceRequest**: One-to-Many (Bir kategoriye ait birden çok hizmet talebi olabilir)
7. **User - Person**: One-to-One (Her kullanıcı bir kişiye bağlıdır)

## Multi-tenant Yapı

Projede multi-tenant yapı, tüm entity'lerin BaseEntity sınıfından türetilmesi ve bu sınıfın ITenant arayüzünü uygulaması ile sağlanmaktadır. Her entity'de bulunan CompanyId ve BranchId alanları, verilerin hangi firma ve şubeye ait olduğunu belirtir ve DbContext seviyesinde global query filtreleme ile her sorgu otomatik olarak tenant bazlı filtrelenir.

## Entity Oluşturma ve Kullanım Standartları

1. Tüm entity'ler uygun namespace altında tanımlanmalıdır (`ResidenceManagement.Core.Entities`)
2. Entity property'leri için uygun veri tipleri ve kısıtlamalar tanımlanmalıdır
3. İlişkiler için uygun navigation property'ler eklenmelidir
4. Entity konfigürasyonları Fluent API kullanılarak yapılmalıdır
5. Multi-tenant filtreleme için CompanyId ve BranchId alanları eksiksiz doldurulmalıdır 