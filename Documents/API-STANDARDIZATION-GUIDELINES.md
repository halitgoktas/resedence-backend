# API Standardizasyon Kılavuzu

Bu doküman, Mekik Residence Manager projesi için API geliştirme standartlarını ve en iyi uygulamaları içerir. API'lerin tutarlı, güvenli ve kolayca bakımı yapılabilir şekilde geliştirilmesi için tüm geliştiricilerin bu standartlara uyması gerekmektedir.

## 1. RESTful API Tasarım İlkeleri

### 1.1 URL Yapısı

- **Kaynak Odaklı URL'ler**: URL'ler, işlemin yapıldığı kaynağı (entity) temsil etmelidir.
  
  ```
  DOĞRU: /api/v1/apartments 
  YANLIŞ: /api/v1/get-apartments
  ```

- **Çoğul İsimlendirme**: Kaynak isimleri çoğul olmalıdır.
  
  ```
  DOĞRU: /api/v1/apartments
  YANLIŞ: /api/v1/apartment
  ```

- **Küçük Harfler ve Tire (-) Kullanımı**: URL segmentleri küçük harflerle yazılmalı ve kelimeler arasında tire (-) kullanılmalıdır.
  
  ```
  DOĞRU: /api/v1/apartment-residents
  YANLIŞ: /api/v1/apartmentResidents, /api/v1/ApartmentResidents
  ```

- **Alt Kaynaklar**: İlişkili alt kaynaklar için aşağıdaki yapı kullanılmalıdır:
  
  ```
  Ana kaynak + ID + Alt kaynak: /api/v1/apartments/{id}/residents
  ```

### 1.2 HTTP Metot Kullanımı

API'lerde standart HTTP metotları doğru anlamlarıyla kullanılmalıdır:

| HTTP Metodu | Kullanım Amacı | Örnek URL |
|-------------|----------------|-----------|
| GET | Veri alma (okuma) | GET /api/v1/apartments |
| POST | Yeni kaynak oluşturma | POST /api/v1/apartments |
| PUT | Mevcut kaynağı tamamen güncelleme | PUT /api/v1/apartments/{id} |
| PATCH | Mevcut kaynağı kısmen güncelleme | PATCH /api/v1/apartments/{id} |
| DELETE | Kaynak silme | DELETE /api/v1/apartments/{id} |

### 1.3 HTTP Durum Kodları

API'lerde doğru HTTP durum kodları kullanılmalıdır:

| Durum Kodu | Anlam | Kullanım Durumu |
|------------|-------|-----------------|
| 200 OK | Başarılı | GET, PUT, PATCH işlemleri başarılı |
| 201 Created | Oluşturuldu | POST işlemi başarılı |
| 204 No Content | İçerik Yok | DELETE işlemi başarılı |
| 400 Bad Request | Hatalı İstek | Geçersiz istek formatı veya parametreler |
| 401 Unauthorized | Yetkisiz | Kimlik doğrulama eksik veya geçersiz |
| 403 Forbidden | Yasak | Kimlik doğrulama tamam, ancak yetki yok |
| 404 Not Found | Bulunamadı | Kaynak bulunamadı |
| 409 Conflict | Çakışma | Kaynakta çakışma (örn. duplicate key) |
| 422 Unprocessable Entity | İşlenemeyen Varlık | Doğrulama hatası |
| 500 Internal Server Error | Sunucu Hatası | Beklenmeyen sunucu hatası |

## 2. API Yanıt Formatı

### 2.1 Standart Yanıt Yapısı

Tüm API yanıtları aşağıdaki standart yapıyı izlemelidir:

```json
{
  "isSuccess": true,
  "message": "İşlem başarılı",
  "data": { ... },
  "timestamp": "2023-07-12T10:15:30Z"
}
```

- **isSuccess**: İşlemin başarılı olup olmadığını belirtir (boolean)
- **message**: İşlem hakkında bilgi veren mesaj (string)
- **data**: Yanıt verileri (object, array, veya null)
- **timestamp**: Yanıtın oluşturulduğu tarih ve saat (ISO 8601 formatında)

### 2.2 Hata Yanıt Yapısı

Hata durumunda yanıt şu şekilde olmalıdır:

```json
{
  "isSuccess": false,
  "message": "Doğrulama hatası",
  "timestamp": "2023-07-12T10:15:30Z",
  "validationErrors": {
    "name": ["İsim alanı boş olamaz"],
    "email": ["Geçerli bir e-posta adresi giriniz"]
  }
}
```

### 2.3 Sayfalama Yanıt Yapısı

Listeleme API'leri için sayfalama desteği eklenmelidir:

```json
{
  "isSuccess": true,
  "message": "Kayıtlar listelendi",
  "data": [ ... ],
  "timestamp": "2023-07-12T10:15:30Z",
  "pagination": {
    "totalCount": 100,
    "pageSize": 10,
    "currentPage": 1,
    "totalPages": 10,
    "hasPrevious": false,
    "hasNext": true
  }
}
```

## 3. API Controller İmplementasyonu

### 3.1 Controller Yapısı

Controller'lar `BaseApiController` sınıfından türetilmelidir:

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApartmentsController : BaseApiController
{
    // Controller kodu buraya
}
```

### 3.2 Endpoint Metotları

Endpoint metotları aşağıdaki yapıda olmalıdır:

```csharp
[HttpGet]
public async Task<ActionResult<ApiResponse<List<ApartmentDto>>>> GetAllApartments()
{
    // İş mantığı burada
    var apartments = await _apartmentService.GetAllApartmentsAsync();
    return Ok(apartments, "Daireler başarıyla listelendi");
}

[HttpGet("{id}")]
public async Task<ActionResult<ApiResponse<ApartmentDto>>> GetApartmentById(int id)
{
    var apartment = await _apartmentService.GetApartmentByIdAsync(id);
    
    if (apartment == null)
        return NotFound($"ID: {id} olan daire bulunamadı");
        
    return Ok(apartment, "Daire başarıyla getirildi");
}

[HttpPost]
public async Task<ActionResult<ApiResponse<ApartmentDto>>> CreateApartment(CreateApartmentDto createApartmentDto)
{
    var newApartment = await _apartmentService.CreateApartmentAsync(createApartmentDto);
    return Created(newApartment, "Daire başarıyla oluşturuldu");
}

[HttpPut("{id}")]
public async Task<ActionResult<ApiResponse<ApartmentDto>>> UpdateApartment(int id, UpdateApartmentDto updateApartmentDto)
{
    var updatedApartment = await _apartmentService.UpdateApartmentAsync(id, updateApartmentDto);
    return Ok(updatedApartment, "Daire başarıyla güncellendi");
}

[HttpDelete("{id}")]
public async Task<ActionResult> DeleteApartment(int id)
{
    await _apartmentService.DeleteApartmentAsync(id);
    return NoContent();
}
```

### 3.3 İstisna Yönetimi

- Özel istisnaları `throw` etmek için CustomException sınıfları kullanın:
  - `NotFoundException`: Kaynak bulunamadığında
  - `BadRequestException`: Geçersiz istek durumunda
  - `ConflictException`: Veri çakışması durumunda
  - `ForbiddenException`: Yetki problemi durumunda

```csharp
if (apartment == null)
    throw new NotFoundException("Apartment", id);
```

## 4. Güvenlik

### 4.1 Kimlik Doğrulama ve Yetkilendirme

- JWT Token tabanlı kimlik doğrulama kullanın
- Endpointler üzerinde `[Authorize]` attribute'unu kullanın
- Rol tabanlı yetkilendirme için `[Authorize(Roles = "Admin")]` kullanın
- Politika tabanlı yetkilendirme için `[Authorize(Policy = "CanManageApartments")]` kullanın

### 4.2 API Uç Noktalarının Korunması

- Hassas veriler için HTTPS kullanımı zorunludur
- Rate Limiting uygulanmalıdır
- CORS politikası doğru yapılandırılmalıdır
- API anahtarları ve/veya OAuth kullanılmalıdır

## 5. Versiyonlama

API versiyonlama, URL yolu üzerinden yapılmalıdır:

```
/api/v1/apartments
/api/v2/apartments
```

- Major versiyon değişiklikleri (v1 -> v2) yapılırken, eski versiyon belirli bir süre desteklenmelidir
- Breaking change içeren değişiklikler, yeni bir API versiyonu gerektirir
- Non-breaking değişiklikler mevcut API versiyonuna eklenebilir

## 6. Dokümantasyon

### 6.1 Swagger/OpenAPI

- Tüm API'ler Swagger/OpenAPI ile dokümante edilmelidir
- Her Controller ve Action için XML yorumları eklenmelidir
- Örnek istek/yanıt modelleri eklenmelidir

### 6.2 XML Yorumlar

Controller ve Action metotları için XML yorumları ekleyin:

```csharp
/// <summary>
/// Belirli bir ID'ye sahip daireyi getirir
/// </summary>
/// <param name="id">Daire ID</param>
/// <returns>Daire bilgileri</returns>
/// <response code="200">Daire başarıyla getirildi</response>
/// <response code="404">Belirtilen ID'ye sahip daire bulunamadı</response>
[ProducesResponseType(typeof(ApiResponse<ApartmentDto>), 200)]
[ProducesResponseType(typeof(ApiResponse), 404)]
[HttpGet("{id}")]
public async Task<ActionResult<ApiResponse<ApartmentDto>>> GetApartmentById(int id)
{
    // Metot implementasyonu
}
```

## 7. Test

### 7.1 Unit Tests

- Controller action metotları için birim testler yazılmalıdır
- Farklı HTTP durum kodlarını döndürme durumları test edilmelidir
- Doğrulama hatalarının doğru şekilde işlenmesi test edilmelidir

### 7.2 Integration Tests

- End-to-end entegrasyon testleri yazılmalıdır
- Gerçek veritabanı ve servisleri kullanan testler eklenmelidir
- API yanıtlarının doğru formatta olduğu kontrol edilmelidir

## 8. Performans

### 8.1 Sayfalama ve Filtreleme

- Büyük veri setleri için sayfalama zorunludur
- Filtreleme, sıralama ve arama parametreleri tutarlı bir şekilde uygulanmalıdır

```
GET /api/v1/apartments?pageNumber=1&pageSize=10&sortBy=number&sortDirection=asc&blockId=5
```

### 8.2 API Caching

- Sık erişilen, değişmeyen veriler için API caching uygulanmalıdır
- Cache kontrol headerları kullanılmalıdır
- Distributed caching, büyük ölçekli sistemler için tercih edilmelidir

## 9. Loglama ve İzleme

### 9.1 Loglama Yönergeleri

- Tüm API istekleri ve yanıtları loglanmalıdır
- Hassas kullanıcı verileri maskelenerek loglanmalıdır
- Hata ve istisna detayları loglanmalıdır
- Structured logging kullanılmalıdır

### 9.2 Performans İzleme

- API istek süreleri izlenmelidir
- Yavaş API istekleri için uyarılar tanımlanmalıdır
- Middleware kullanarak request/response süreleri loglanmalıdır

## 10. Örnek Endpoint Şablonları

### 10.1 CRUD Endpoint'leri

```
GET    /api/v1/apartments                 # Tüm daireleri listele
GET    /api/v1/apartments/{id}            # Belirli bir daireyi getir
POST   /api/v1/apartments                 # Yeni daire oluştur
PUT    /api/v1/apartments/{id}            # Daireyi güncelle
DELETE /api/v1/apartments/{id}            # Daireyi sil
```

### 10.2 İlişkisel Endpoint'ler

```
GET    /api/v1/apartments/{id}/residents         # Daire sakinlerini listele
POST   /api/v1/apartments/{id}/residents         # Daireye yeni sakin ekle
DELETE /api/v1/apartments/{id}/residents/{resId} # Daireden sakin çıkar
```

### 10.3 Toplu İşlem Endpoint'leri

```
POST   /api/v1/apartments/bulk-create            # Toplu daire oluştur
PUT    /api/v1/apartments/bulk-update            # Toplu daire güncelle
DELETE /api/v1/apartments/bulk-delete            # Toplu daire sil
```

### 10.4 Özel İşlem Endpoint'leri

```
POST   /api/v1/apartments/{id}/activate          # Daireyi aktifleştir
POST   /api/v1/apartments/{id}/deactivate        # Daireyi devre dışı bırak
GET    /api/v1/apartments/search                 # Dairelerde arama yap
``` 