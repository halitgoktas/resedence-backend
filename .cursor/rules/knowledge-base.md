## [Hata Mesajı] "(" was not closed
Çözüm: Parantez açıldığında mutlaka kapatılmalıdır. Python'da parantez açma ve kapama işaretlerinin eşleştiğinden emin olun. 

Örnek düzeltme:
```python
# Hatalı kod
print("Hello World"

# Düzeltilmiş kod
print("Hello World")
```

## [Hata Mesajı] .NET Core Web API projesinde servis entegrasyonu
Çözüm: .NET Core projelerinde servislerin doğru şekilde entegre edilmesi gerekir. Bunun için aşağıdaki adımlar izlenmelidir:

1. Öncelikle gerekli servislerin Interface'lerini `ResidenceManagement.Core/Interfaces` altında tanımlayın
2. Servislerin implementasyonlarını `ResidenceManagement.Infrastructure/Services` altında gerçekleştirin
3. Servisleri `ResidenceManagement.Infrastructure/DependencyInjection.cs` veya `ServicesRegistration.cs` dosyasında kaydedin
4. Son olarak Program.cs dosyasında bu servisleri kullanılabilir hale getirin

Örnek kayıt:
```csharp
// DependencyInjection.cs veya ServicesRegistration.cs
services.AddScoped<IMyService, MyService>();

// Program.cs
builder.Services.AddInfrastructureServices(builder.Configuration);
```

## [Hata Mesajı] error: No such remote 'origin'
Çözüm: Bu hata, belirtilen isimde bir git remote olmadığı zaman meydana gelir. Yeni bir git remote eklemek için doğru sözdizimi kullanılmalıdır.

Doğru kullanım:
```bash
# Önce remote ekle, sonra diğer işlemleri yap
git remote add origin https://github.com/kullaniciadi/repo-adi.git

# Eğer remote varsa ama URL değiştirmek istiyorsanız:
git remote set-url origin https://github.com/kullaniciadi/repo-adi.git
```

## [Hata Mesajı] error: src refspec master does not match any
Çözüm: Bu hata, git push yapmaya çalıştığınız branch'te hiç commit olmadığında ortaya çıkar. Push yapmadan önce en az bir commit yapmalısınız.

Doğru çözüm:
```bash
# Önce dosyaları ekleyin
git add .

# Commit yapın
git commit -m "İlk commit"

# Sonra push yapın
git push -u origin master
```

## [Hata Mesajı] İki farklı IUnitOfWork arayüzü ve SaveChangesAsync/Complete metot karışıklığı
Çözüm: Projede iki farklı IUnitOfWork arayüzü olduğunda ve bunlar farklı metot isimleri kullandığında (SaveChangesAsync ve Complete gibi) şu adımları uygula:

1. Her iki arayüzü inceleyerek farklılıkları belirle:
   - `ResidenceManagement.Core.Interfaces.IUnitOfWork`: Spesifik repository'ler ve asenkron transaction metodları içerir
   - `ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork`: Jenerik repository ve senkron transaction metodları içerir

2. Arayüzlerin birbirleriyle uyumlu hale getir:
   - Eksik metodları her iki arayüze de ekle (örn: SaveChangesAsync ve Complete metodlarının her ikisinde de olması)
   - Metot imzalarının aynı şekilde tanımlandığından emin ol

3. UnitOfWork implementasyonunu güncelleyerek her iki arayüzde tanımlı metodların implementasyonunu sağla:
   ```csharp
   // Örneğin Complete ve SaveChangesAsync metodlarının her ikisini de ekle
   public async Task<int> Complete()
   {
       return await _dbContext.SaveChangesAsync();
   }

   public async Task<int> SaveChangesAsync()
   {
       // Complete metodu ile aynı işi yapar, sadece farklı isimle
       return await _dbContext.SaveChangesAsync();
   }
   ```

4. Uzun vadede, bu iki arayüzün birleştirilmesi veya hiyerarşik olarak düzenlenmesi daha temiz bir çözüm olacaktır.

## Residence Manager Backend - Yapılacaklar Listesi

### 1. Temel Eksiklikler ve Yapılandırma
- [ ] Authentication ve Authorization mekanizmasının tamamlanması (JWT, Refresh Token)
- [ ] CORS politikalarının yapılandırılması
- [ ] Exception handling middleware geliştirilmesi
- [ ] Loglama mekanizmasının geliştirilmesi (Serilog)
- [ ] API versiyonlama altyapısının kurulması
- [ ] Response formatı için standart şablon oluşturulması

### 2. Veri Tabanı ve Migrasyon
- [ ] DB migrasyonlarının oluşturulması
- [ ] DB seed data eklenmesi (test verileri, admin kullanıcıları)
- [ ] DB bağlantı string güvenliği (appsettings.json dışına taşıma)
- [ ] Entity sınıflarının validasyon kuralları eklenmesi

### 3. Controller'lar ve Endpoint'ler 
- [ ] User Management API (Kullanıcı Yönetimi)
- [ ] Tenant Management API (Kiracı/Sakin Yönetimi)
- [ ] Property Management API (Bina/Daire Yönetimi)
- [ ] Financial Management API (Aidat/Ödeme Yönetimi)
- [ ] Services API (Hizmet/Talep Yönetimi)

### 4. Servis Katmanı
- [ ] Email servisi entegrasyonu
- [ ] SMS servisi entegrasyonu 
- [ ] Dosya yükleme servisi geliştirilmesi
- [ ] Cache mekanizması implementasyonu (Redis)
- [ ] Background job sistemi (Hangfire)

### 5. Güvenlik
- [ ] Kullanıcı yetkilendirme kuralları (Policy-based authorization)
- [ ] Input validasyon kuralları (FluentValidation)
- [ ] API Rate limiting 
- [ ] SQL injection koruması (Parametrized queries)
- [ ] XSS koruması 

### 6. Test ve Bakım
- [ ] Birim test projesinin yapılandırılması
- [ ] Entegrasyon testlerinin eklenmesi
- [ ] Performans testleri
- [ ] Kod refaktör ve temizliği
- [ ] Swagger dokümantasyonu

## [Hata Mesajı] Entity ve DTO alanları uyumsuzluğu
Çözüm: Entity sınıflarında ve DTO'larda tanımlanan alanlar arasında tutarlılık sağlanmalıdır. Şu adımları izleyin:

1. Entity ve DTO arasında şu uyumsuzluklar kontrol edilmeli:
   - Property isimleri farklı olabilir: Entity'de `Sifre` varken DTO'da `Password` kullanılmış olabilir
   - Property tipleri farklı olabilir: Entity'de `int` varken DTO'da `Guid` kullanılmış olabilir
   - Entity'de olmayan property'ler DTO'da tanımlanmış olabilir veya tam tersi

2. DTO ve Entity arasında mapperlar yazılırken şu noktalara dikkat edin:
   - AutoMapper kullanıyorsanız, farklı isimlerdeki property'ler için özel mapping tanımlayın
   ```csharp
   CreateMap<Kullanici, UserDto>()
       .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Sifre));
   ```

   - Manuel mapping yapıyorsanız, tüm alanların doğru şekilde maplendiğinden emin olun
   ```csharp
   var userDto = new UserDto 
   {
       Password = user.Sifre,
       // Diğer alanlar
   };
   ```

3. Tür dönüşümlerinde dikkat edilmesi gerekenler:
   - `int` ve `Guid` arasında dönüşüm yapılırken açık dönüşüm kullanın
   ```csharp
   // Hatalı:
   rolId = kullaniciId; // int -> Guid dönüşümü

   // Doğru:
   rolId = new Guid(kullaniciId.ToString());
   // veya
   rolId = Guid.Parse(kullaniciId.ToString());
   ```

   - Null olabilen tiplerde null kontrolü yapın
   ```csharp
   int? nullableId = entity.Id;
   int id = nullableId ?? 0; // Null ise 0 kullan
   ```

## [Hata Mesajı] FindAsync metodu tanımlanmamış hatası
Çözüm: Repository'lerde tanımlı olmayan metotların kullanımı hatası. Aşağıdaki adımları izleyin:

1. IRepository arayüzünü kontrol edin ve kullanmaya çalıştığınız metodun tanımlı olup olmadığını görün:
   ```csharp
   // IRepository'de şöyle bir metot tanımlı olabilir
   Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
   
   // Ancak kodda FindAsync çağrılıyor
   await _repository.FindAsync(x => x.Id == id);
   ```

2. Mevcut metotları kullanarak sorunu çözün:
   ```csharp
   // FindAsync yerine GetAsync kullanın
   var items = await _repository.GetAsync(x => x.Id == id);
   ```

3. Veya IRepository arayüzüne eksik metodu ekleyin:
   ```csharp
   // IRepository.cs
   Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
   
   // Repository.cs implementasyon
   public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
   {
       return await _dbContext.Set<T>().Where(predicate).ToListAsync();
   }
   ```

## [Hata Mesajı] Salt-okunur property'ye atama yapılamaz hatası
Çözüm: Entity ve DTO sınıflarındaki salt-okunur property'lere atama yapılamaz. Şu adımları izleyin:

1. Salt-okunur property'yi tespit edin:
   ```csharp
   // Örneğin:
   public bool IsActive { get; } // Salt-okunur property
   ```

2. Bu property'yi iki şekilde düzeltebilirsiniz:
   - Property'yi yazılabilir hale getirin:
   ```csharp
   public bool IsActive { get; set; } // Artık yazılabilir
   ```
   
   - Veya constructor üzerinden değer atayın:
   ```csharp
   private readonly bool _isActive;
   public bool IsActive => _isActive;
   
   public Entity(bool isActive)
   {
       _isActive = isActive;
   }
   ```

3. Alternatif olarak, entity'nin yapısını değiştirmek istemiyorsanız, yeni bir nesne oluşturun:
   ```csharp
   // Eski entity'yi değiştirmek yerine
   var newEntity = new RefreshToken
   {
       Token = token,
       KullaniciId = id,
       // IsActive = true // Bu hata verir
   };
   ```

## [Hata Mesajı] '==' işleci 'Guid' ve 'int' türündeki işlenenlere uygulanamaz
Çözüm: Farklı türler arasında karşılaştırma yapılması hatasıdır. Şu adımları izleyin:

1. Türleri aynı hale getirin:
   ```csharp
   // Hatalı:
   if (entity.Id == userId) // Guid == int
   
   // Doğru (int -> Guid):
   if (entity.Id == new Guid(userId.ToString()))
   // veya
   if (entity.Id == Guid.Parse(userId.ToString()))
   
   // Doğru (Guid -> int):
   if (entity.Id.ToString() == userId.ToString())
   ```

2. Entity ve DTO'larda tutarlı türleri kullanın:
   - Tüm ID'ler için ya `int` ya da `Guid` tercih edin, karışık kullanmayın
   - Zorunlu değilse, daha basit olan `int` türünde ID'leri tercih edin

3. Proje başında ID türlerini standardize edin ve tüm entity'lerde buna uyun

## [Hata Mesajı] Kullanici ve DTO sınıflarında property uyuşmazlığı sorunları
Çözüm: Entity ve DTO sınıfları arasındaki property isim uyuşmazlıklarını aşağıdaki yöntemlerle çözebilirsiniz:

1. Entity sınıfını düzenleyin:
   ```csharp
   // Kullanici.cs - eksik property'lerin eklenmesi
   public class Kullanici : BaseEntity, ITenant
   {
       // Mevcut property'ler
       public string Sifre { get; set; }
       public string SifreSalt { get; set; }
       
       // Eksik property'ler (AuthenticationService'de kullanılanlar)
       public byte[] PasswordHash { get; set; }
       public byte[] PasswordSalt { get; set; }
   }
   ```

2. Alternatif olarak, AuthenticationService'i düzenleyin:
   ```csharp
   // AuthenticationService.cs - PasswordHash ve PasswordSalt yerine Sifre ve SifreSalt kullanımı
   // Hatalı: if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
   // Doğru: 
   if (!VerifyPassword(request.Password, user.Sifre, user.SifreSalt))
   {
       // İşlemler
   }
   
   private bool VerifyPassword(string password, string hash, string salt)
   {
       // Hash'i ve salt'ı kullanarak şifre doğrulama işlemi
   }
   ```

3. Token yanıtlarında benzer durum için:
   ```csharp
   // TokenResponse.cs - eksik property'lerin eklenmesi
   public class TokenResponse
   {
       // Mevcut property'ler
       public string AccessToken { get; set; }
       public DateTime AccessTokenExpiration { get; set; }
       
       // Eksik property'ler (AuthenticationService'de kullanılanlar)
       public string Token { get; set; } // AccessToken'ın yerine kullanılabilir
       public DateTime Expires { get; set; } // AccessTokenExpiration'ın yerine kullanılabilir
   }
   ```

4. Ya da AuthenticationService'i düzenleme:
   ```csharp
   // AuthenticationService.cs - property isimlerinin uyumlu hale getirilmesi
   // Hatalı: response.Token = token;
   // Doğru:
   response.AccessToken = token;
   
   // Hatalı: response.Expires = expires;
   // Doğru:
   response.AccessTokenExpiration = expires;
   ```

## [Hata Mesajı] '==' işleci 'Guid' ve 'int' türündeki işlenenlere uygulanamaz hatası
Çözüm: Farklı türdeki ID'lerin karşılaştırılmasında tip dönüşümü yapılması gerekir:

1. ID tipi karşılaştırmalarında açık dönüşüm kullanın:
   ```csharp
   // Hatalı:
   if (entity.Id == userId) // Guid ile int karşılaştırma hatası
   
   // Doğru (int -> Guid):
   if (entity.Id == new Guid(userId.ToString()))
   
   // Doğru (Guid -> int) - ancak bu veri kaybına yol açabilir:
   if (int.Parse(entity.Id.ToString()) == userId)
   
   // En doğrusu - İki değeri de string'e çevirip karşılaştırma:
   if (entity.Id.ToString() == userId.ToString())
   ```

2. Proje genelinde ID tiplerini standardize edin:
   ```csharp
   // Tüm entity'lerde aynı ID tipi kullanımı
   public class BaseEntity
   {
       public int Id { get; set; } // veya Guid Id { get; set; }
   }
   ```

3. Kullanıcı tarafından oluşturulan ID'ler için:
   ```csharp
   // Yeni bir Guid oluşturma:
   var newGuid = Guid.NewGuid();
   
   // Bir string'i Guid'e çevirme:
   var guidFromString = Guid.Parse("12345678-1234-1234-1234-123456789012");
   ```

## [Hata Mesajı] Salt-okunur property'ye atama yapılamaz hatası (RefreshToken.IsActive)
Çözüm: Salt-okunur property'lere değer atama işlemini düzeltmek için:

1. Property'yi yazılabilir yapın:
   ```csharp
   // RefreshToken.cs
   // Hatalı:
   public bool IsActive { get; } // Salt-okunur
   
   // Doğru:
   public bool IsActive { get; set; } // Artık yazılabilir
   ```

2. Eğer salt-okunur olması gerekiyorsa, constructor üzerinden atama yapın:
   ```csharp
   public class RefreshToken
   {
       private readonly bool _isActive;
       
       public bool IsActive => _isActive;
       
       public RefreshToken(bool isActive = true)
       {
           _isActive = isActive;
       }
   }
   ```

3. Eğer entity'yi değiştirmek istemiyorsanız, yeni bir nesne oluşturun:
   ```csharp
   // Eski nesneyi değiştirmek yerine yeni bir nesne oluşturun
   var newRefreshToken = new RefreshToken
   {
       Token = oldRefreshToken.Token,
       KullaniciId = oldRefreshToken.KullaniciId,
       // IsActive property'si constructor'da true olarak ayarlanabilir
   };
   
   // Eski nesneyi silin ve yenisini ekleyin
   await _refreshTokenRepository.DeleteAsync(oldRefreshToken);
   await _refreshTokenRepository.AddAsync(newRefreshToken);
   ```

## [Hata Mesajı] Repository arayüzünde FindAsync metodu tanımlanmamış
Çözüm: Eksik repository metodlarının tanımlanması için:

1. IRepository arayüzüne FindAsync metodunu ekleyin:
   ```csharp
   // IRepository.cs
   public interface IRepository<T> where T : BaseEntity
   {
       // Mevcut metodlar
       
       // Eksik metod
       Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
   }
   ```

2. Repository implementasyonunda FindAsync metodunu ekleyin:
   ```csharp
   // Repository.cs
   public class Repository<T> : IRepository<T> where T : BaseEntity
   {
       // Mevcut metodlar
       
       // FindAsync implementasyonu
       public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
       {
           return await _dbContext.Set<T>()
               .Where(predicate)
               .ToListAsync();
       }
   }
   ```

3. Alternatif olarak, mevcut GetAsync metodunu kullanabilirsiniz:
   ```csharp
   // Hatalı:
   var userRoles = await _userRoleRepository.FindAsync(ur => ur.KullaniciId == userId);
   
   // Doğru (mevcut metot ile):
   var userRoles = await _userRoleRepository.GetAsync(ur => ur.KullaniciId == userId);
   ```

## [Hata Mesajı] Rol sınıfında RolAdi property'si bulunamadı
Çözüm: Rol entity sınıfına eksik property'nin eklenmesi:

1. Rol sınıfına eksik property'yi ekleyin:
   ```csharp
   // Rol.cs
   public class Rol : BaseEntity
   {
       // Mevcut property'ler
       
       // Eksik property
       public string RolAdi { get; set; }
   }
   ```

2. Eğer Rol sınıfında farklı isimde bir property varsa, onu kullanın:
   ```csharp
   // Örneğin, Rol sınıfında Name property'si varsa:
   
   // Hatalı:
   var roleName = role.RolAdi;
   
   // Doğru:
   var roleName = role.Name;
   ```

## [Hata Mesajı] 'int' türü örtülü olarak 'System.Guid' türüne dönüştürülemez
Çözüm: Int değerlerini Guid tipine dönüştürürken açık (explicit) dönüşüm kullanılmalıdır:

1. İnt değerini Guid'e dönüştürme:
   ```csharp
   // Hatalı:
   Guid roleId = userId; // int -> Guid örtülü dönüşüm hatası
   
   // Doğru:
   Guid roleId = new Guid(userId.ToString());
   // veya
   Guid roleId = Guid.Parse(userId.ToString());
   ```

2. ID tipi tutarlılığını sağlayın:
   ```csharp
   // Tüm entity sınıflarında aynı ID tipi kullanımı:
   public class BaseEntity
   {
       public int Id { get; set; } // veya Guid Id { get; set; }
   }
   ```

3. Kullanıcı tarafından oluşturulan ID'ler için:
   ```csharp
   // Yeni bir Guid oluşturma:
   var newGuid = Guid.NewGuid();
   
   // Bir string'i Guid'e çevirme:
   var guidFromString = Guid.Parse("12345678-1234-1234-1234-123456789012");
   ```
