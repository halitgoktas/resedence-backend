# Residence Manager - RESTful API Standartları

Bu doküman, Residence Manager API projesinde izlenecek endpoint URL yapılandırma standartlarını tanımlar.

## 1. URL Yapısı

### 1.1 Genel URL Yapısı
```
https://{host}/api/v{version}/{resource}/{resourceId}/{subresource}/{subresourceId}
```

### 1.2 Çoğul Kaynak İsimleri
- Kaynak isimleri çoğul olarak kullanılacaktır
  - ✅ `/api/v1/apartments`
  - ❌ `/api/v1/apartment`

### 1.3 Kebab-Case Kullanımı
- URL'lerde kebab-case kullanılacaktır (küçük harflerle yazılmış, tire ile ayrılmış kelimeler)
  - ✅ `/api/v1/maintenance-schedules`
  - ❌ `/api/v1/maintenanceSchedules` veya `/api/v1/maintenance_schedules`

### 1.4 İç İçe Kaynak İlişkileri
- İlişkili kaynaklar hiyerarşik olarak ifade edilecektir
  - ✅ `/api/v1/blocks/{blockId}/apartments`
  - ✅ `/api/v1/apartments/{apartmentId}/residents`
  - ❌ `/api/v1/getApartmentsByBlock/{blockId}`

### 1.5 Filtreleme, Sıralama ve Sayfalama
- Sorgu parametreleri kullanılacaktır
  - Filtreleme: `?filter[field]=value`
  - Sıralama: `?sort=field` veya `?sort=-field` (- işareti azalan sıra için)
  - Sayfalama: `?page=1&pageSize=20`

### 1.6 Davranışsal Eylemler
- Özel davranışlar ve işlemler için `/action` yapısı kullanılacaktır
  - ✅ `/api/v1/payments/{paymentId}/approve`
  - ✅ `/api/v1/service-requests/{requestId}/assign`
  - ❌ `/api/v1/approvePayment/{paymentId}`

## 2. HTTP Metot Kullanımı

| HTTP Metodu | Kullanım                               | Örnek                                         |
|-------------|----------------------------------------|-----------------------------------------------|
| GET         | Kaynak veya koleksiyon okuma           | `GET /api/v1/apartments`                      |
| POST        | Yeni kaynak oluşturma                  | `POST /api/v1/apartments`                     |
| PUT         | Kaynağın tamamını güncelleme           | `PUT /api/v1/apartments/{id}`                 |
| PATCH       | Kaynağın belirli alanlarını güncelleme | `PATCH /api/v1/apartments/{id}`               |
| DELETE      | Kaynak silme                           | `DELETE /api/v1/apartments/{id}`              |

### 2.1 GET Metot Kullanımı
- Koleksiyon getirme: `GET /api/v1/apartments`
- Tekil kaynak getirme: `GET /api/v1/apartments/{id}`
- İlişkili kaynakları getirme: `GET /api/v1/blocks/{blockId}/apartments`

### 2.2 POST Metot Kullanımı
- Yeni kaynak oluşturma: `POST /api/v1/apartments`
- Davranışsal eylemler için: `POST /api/v1/apartments/{id}/duplicate`

### 2.3 PUT Metot Kullanımı
- Bir kaynağın tüm alanlarını güncelleme: `PUT /api/v1/apartments/{id}`
- İstek gövdesinde tüm zorunlu alanlar bulunmalıdır

### 2.4 PATCH Metot Kullanımı
- Bir kaynağın kısmi güncellemesi: `PATCH /api/v1/apartments/{id}`
- Sadece değiştirilecek alanlar gönderilmelidir

### 2.5 DELETE Metot Kullanımı
- Kaynak silme: `DELETE /api/v1/apartments/{id}`
- Soft-delete kullanılacaktır (fiziksel silme yerine IsDeleted=true olarak işaretleme)

## 3. HTTP Durum Kodları

| Kod   | Durum                 | Kullanım                                                |
|-------|------------------------|--------------------------------------------------------|
| 200   | OK                     | Başarılı GET, PUT, PATCH veya DELETE işlemleri         |
| 201   | Created                | Başarılı POST ile yeni kaynak oluşturma                |
| 204   | No Content             | Başarılı işlem, ancak dönen veri yok (örn. DELETE)     |
| 400   | Bad Request            | Geçersiz istek, validasyon hatası                      |
| 401   | Unauthorized           | Kimlik doğrulama hatası                                |
| 403   | Forbidden              | Yetkilendirme hatası                                   |
| 404   | Not Found              | Kaynak bulunamadı                                      |
| 422   | Unprocessable Entity   | İş kuralı ihlali, veriler geçerli ama işlenemedi       |
| 500   | Internal Server Error  | Sunucu hatası                                          |

## 4. Response Format

### 4.1 Başarılı Yanıt
```json
{
  "success": true,
  "message": "İşlem başarılı",
  "statusCode": 200,
  "data": { ... }
}
```

### 4.2 Hata Yanıtı
```json
{
  "success": false,
  "message": "İşlem sırasında hata oluştu",
  "statusCode": 400,
  "errors": ["Hata 1", "Hata 2"]
}
```

### 4.3 Koleksiyon Yanıtı
```json
{
  "success": true,
  "message": "Veriler başarıyla getirildi",
  "statusCode": 200,
  "data": [ ... ],
  "pagination": {
    "page": 1,
    "pageSize": 20,
    "totalPages": 5,
    "totalCount": 100
  }
}
```

## 5. Örnek Endpoint Yapıları

### 5.1 Kullanıcı Yönetimi
- `GET /api/v1/users` - Tüm kullanıcıları getir
- `GET /api/v1/users/{id}` - Belirli bir kullanıcıyı getir
- `POST /api/v1/users` - Yeni kullanıcı oluştur
- `PUT /api/v1/users/{id}` - Kullanıcıyı güncelle
- `DELETE /api/v1/users/{id}` - Kullanıcıyı sil
- `GET /api/v1/users/{id}/roles` - Kullanıcının rollerini getir
- `POST /api/v1/users/{id}/roles` - Kullanıcıya rol ekle

### 5.2 Firma ve Şube Yönetimi
- `GET /api/v1/companies` - Tüm firmaları getir
- `GET /api/v1/companies/{id}` - Belirli bir firmayı getir
- `GET /api/v1/companies/{id}/branches` - Firmanın şubelerini getir
- `POST /api/v1/companies/{id}/branches` - Firmaya şube ekle

### 5.3 Rezidans Yönetimi
- `GET /api/v1/blocks` - Tüm blokları getir
- `GET /api/v1/blocks/{id}/apartments` - Bloktaki daireleri getir
- `GET /api/v1/apartments/{id}/residents` - Dairede oturan sakinleri getir

### 5.4 Finansal Yönetim
- `GET /api/v1/dues` - Tüm aidatları getir
- `POST /api/v1/dues/{id}/payments` - Aidat ödemesi ekle
- `GET /api/v1/apartments/{id}/dues` - Daireye ait aidatları getir

### 5.5 Hizmet Talep Yönetimi
- `GET /api/v1/service-requests` - Tüm hizmet taleplerini getir
- `POST /api/v1/service-requests/{id}/notes` - Hizmet talebine not ekle
- `PATCH /api/v1/service-requests/{id}/status` - Hizmet talebi durumunu güncelle 