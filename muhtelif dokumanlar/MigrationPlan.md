# Mekik Residence Manager - Migration Planı

Bu doküman, Mekik Residence Manager projesinin veritabanı migration işlemleri için kapsamlı bir çalışma planını içermektedir. Aşağıdaki adımlar, projenin Entity Framework Core migration'larını başarıyla oluşturmak ve uygulamak için gerekli çalışmaları detaylandırmaktadır.

## Sorun Tespiti

Mevcut durumda tespit edilen sorunlar:

- [x] Entity Framework Core DbContext yapılandırmasında sorunlar
- [x] Entity ilişkilerinde çakışmalar (özellikle User, ApartmentOwner, ApartmentResident arasında)
- [x] Bağımlılık enjeksiyonu (dependency injection) yapılandırmasında eksiklikler
- [x] EnumValue entity'si için tablo çakışması
- [x] Discriminator yapılandırması sorunları (Apartment entity'si için)

## Çalışma Planı

### 1. Entity İlişkilerinin Düzenlenmesi

- [x] User ve ApartmentOwner ilişkisinin düzenlenmesi
  - [x] Navigation property'lerin doğru şekilde yapılandırılması
  - [x] Fluent API ile ilişkilerin açıkça tanımlanması

- [x] User ve ApartmentResident ilişkisinin düzenlenmesi
  - [x] Navigation property'lerin doğru şekilde yapılandırılması
  - [x] Fluent API ile ilişkilerin açıkça tanımlanması

- [x] Apartment ve Block ilişkisinin düzenlenmesi
  - [x] Navigation property'lerin doğru şekilde yapılandırılması
  - [x] Fluent API ile ilişkilerin açıkça tanımlanması

### 2. Entity Yapılandırmalarının Düzenlenmesi

- [x] EnumValue entity'si için tablo çakışmasının çözülmesi
  - [x] Entity Configuration sınıflarının kontrol edilmesi
  - [x] Tablo adlandırma stratejisinin gözden geçirilmesi

- [x] Discriminator yapılandırmasının düzenlenmesi
  - [x] Apartment entity'si için inheritance stratejisinin belirlenmesi
  - [x] TPT (Table Per Type) stratejisinin uygulanması

### 3. DbContext Yapılandırmasının Düzenlenmesi

- [x] OnModelCreating metodunun gözden geçirilmesi
  - [x] Entity konfigürasyonlarının doğru şekilde uygulanması
  - [x] Global filtrelerin kontrol edilmesi

- [x] DbSet tanımlamalarının kontrol edilmesi
  - [x] Eksik DbSet tanımlamalarının eklenmesi (EnumValue için ayrı DbSet'ler)
  - [x] Gereksiz DbSet tanımlamalarının kaldırılması

### 4. Bağımlılık Enjeksiyonu Yapılandırmasının Düzenlenmesi

- [x] ServicesRegistration sınıfının gözden geçirilmesi
  - [x] Repository kayıtlarının kontrol edilmesi
  - [x] Service kayıtlarının kontrol edilmesi

- [x] Program.cs dosyasının gözden geçirilmesi
  - [x] Servis kayıtlarının doğru sırada yapılması
  - [x] Eksik servis kayıtlarının eklenmesi

### 5. Derleme Hatalarının Çözülmesi

- [x] Null referans uyarılarının çözülmesi
  - [x] Null kontrollerinin eklenmesi
  - [x] Nullable tip tanımlamalarının düzenlenmesi

- [x] Kod kalite sorunlarının çözülmesi
  - [x] EnumValueRepository sınıfının düzenlenmesi
  - [x] Metot gizleme (hiding) sorunlarının çözülmesi

### 6. Migration İşlemleri

- [x] Mevcut migration'ların temizlenmesi (eğer varsa)
  - [x] Migration klasörünün kontrol edilmesi
  - [x] Gerekirse migration'ların silinmesi

- [ ] Initial migration'ın oluşturulması
  - [ ] `dotnet ef migrations add InitialMigration` komutunun çalıştırılması
  - [ ] Migration dosyalarının kontrol edilmesi

- [ ] Migration'ın uygulanması
  - [ ] `dotnet ef database update` komutunun çalıştırılması
  - [ ] Veritabanının kontrol edilmesi

### 6. Test ve Doğrulama

- [ ] Veritabanı şemasının kontrol edilmesi
  - [ ] Tabloların doğru şekilde oluşturulduğunun doğrulanması
  - [ ] İlişkilerin doğru şekilde kurulduğunun doğrulanması

- [ ] CRUD işlemlerinin test edilmesi
  - [ ] Entity'lerin eklenmesi, güncellenmesi ve silinmesi
  - [ ] İlişkili entity'lerin doğru şekilde çalıştığının doğrulanması

## Notlar ve Öneriler

- Entity Framework Core'un Code First yaklaşımını kullanıyoruz, bu nedenle entity'lerin ve ilişkilerin doğru şekilde tanımlanması önemlidir.
- Migration işlemleri sırasında hata alınması durumunda, hata mesajlarını dikkatlice inceleyip, ilgili sorunları çözmek gerekir.
- Bağımlılık enjeksiyonu yapılandırması, Entity Framework Core'un DbContext'ini ve repository'leri doğru şekilde kaydetmek için önemlidir.
- Migration işlemleri öncesinde, projenin başarıyla derlenmesi ve çalışması gerekir.

## İlerleme Durumu

- [x] Sorun tespiti yapıldı
- [x] Entity ilişkileri düzenlendi
- [x] Entity yapılandırmaları düzenlendi (EnumValue çakışması çözüldü)
- [x] DbContext yapılandırması düzenlendi
- [x] Bağımlılık enjeksiyonu yapılandırması düzenlendi
- [x] Derleme hatalarının çözülmesi (Null referans uyarıları)
- [ ] Migration işlemleri tamamlandı (Projede hala birçok uyarı ve hata var)
- [ ] Test ve doğrulama yapıldı

## Sonuç ve Öneriler

Projede yapılan düzenlemeler sonucunda, entity ilişkileri ve yapılandırmaları düzenlendi, DbContext yapılandırması ve bağımlılık enjeksiyonu yapılandırması düzenlendi, null referans uyarıları çözüldü. Ancak, projede hala birçok uyarı ve hata var.

Bu aşamada, migration işlemlerini gerçekleştirmek için, projenin daha kapsamlı bir şekilde düzenlenmesi gerekiyor. Bu düzenlemeler:

1. Tüm null referans uyarılarının çözülmesi
2. Metot gizleme (hiding) sorunlarının çözülmesi
3. Kullanılmayan using direktiflerinin kaldırılması
4. Olası null başvuru ataması uyarılarının çözülmesi
5. Zaman uyumsuz yöntemlerdeki await işleçlerinin düzenlenmesi

Bu düzenlemeler, migration planımızın kapsamını aşıyor. Bu nedenle, migration işlemleri tamamlanamadı.

Migration işlemlerini gerçekleştirmek için, öncelikle projenin başarıyla derlenmesi gerekiyor. Ancak, projede hala birçok hata ve uyarı olduğu için, derleme işlemi başarısız oluyor. Bu nedenle, migration işlemlerini gerçekleştirmek için, öncelikle projenin daha kapsamlı bir şekilde düzenlenmesi gerekiyor.

## Alternatif Yaklaşım

Projede yapılan düzenlemeler sonucunda, migration işlemlerini gerçekleştirmek için alternatif bir yaklaşım denendi:

1. MigrationDbContext sınıfı oluşturuldu
2. DesignTimeDbContextFactory sınıfı oluşturuldu
3. appsettings.json dosyası oluşturuldu

Ancak, projede hala birçok hata ve uyarı olduğu için, bu yaklaşım da başarısız oldu. Bu nedenle, migration işlemlerini gerçekleştirmek için, öncelikle projenin daha kapsamlı bir şekilde düzenlenmesi gerekiyor.

## Yapılan Düzenlemeler

1. **EnumValueRepository Düzenlemesi**: EnumValueRepository sınıfı, `BaseEnumValues` DbSet'ini kullanacak şekilde düzenlendi.
2. **ApplicationDbContext Düzenlemesi**: ApplicationDbContext'e geriye dönük uyumluluk için `EnumValues` DbSet'i eklendi.
3. **User İlişkileri Düzenlemesi**: User sınıfı ile ApartmentOwner ve ApartmentResident sınıfları arasındaki ilişkiler düzenlendi.
4. **ApartmentOwnerConfiguration ve ApartmentResidentConfiguration Düzenlemesi**: İlişkiler düzenlendi.

## Sonraki Adımlar

1. Projedeki tüm null referans uyarılarının çözülmesi
2. Metot gizleme (hiding) sorunlarının çözülmesi
3. Kullanılmayan using direktiflerinin kaldırılması
4. Olası null başvuru ataması uyarılarının çözülmesi
5. Zaman uyumsuz yöntemlerdeki await işleçlerinin düzenlenmesi
6. Migration işlemlerinin gerçekleştirilmesi
7. Test ve doğrulama yapılması

## Karşılaşılan Sorunlar ve Çözümleri

1. **EnumValue Entity Çakışması**: Projede iki farklı namespace'te (Base ve Common) EnumValue sınıfları bulunuyordu. Bu sorunu çözmek için:
   - ApplicationDbContext'te farklı DbSet'ler tanımlandı: `BaseEnumValues` ve `CommonEnumValues`
   - EnumValueRepository sınıfı, `BaseEnumValues` DbSet'ini kullanacak şekilde düzenlendi

2. **User İlişkileri Sorunu**: User sınıfı ile ApartmentOwner ve ApartmentResident sınıfları arasındaki ilişkilerde sorun vardı. Bu sorunu çözmek için:
   - ApplicationDbContext'te ilişkiler `WithMany()` şeklinde düzenlendi
   - ApartmentOwnerConfiguration ve ApartmentResidentConfiguration sınıflarında ilişkiler düzenlendi

3. **Apartment Entity Çakışması**: Projede iki farklı namespace'te Apartment sınıfları bulunuyordu. Bu sorunu çözmek için:
   - ApplicationDbContext'te farklı tablo adları tanımlandı: `BaseApartments` ve `PropertyApartments`

## Kaynaklar

- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Entity Framework Core - Relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships)
- [Entity Framework Core - Inheritance](https://docs.microsoft.com/en-us/ef/core/modeling/inheritance)
- [Entity Framework Core - Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
