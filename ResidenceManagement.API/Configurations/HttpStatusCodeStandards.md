# HTTP Durum Kodu Standartları

Bu doküman, API yanıtlarında kullanılacak HTTP durum kodları için standartları tanımlar.

## 1. Başarılı Yanıtlar

| Durum Kodu | Açıklama                                           | Kullanım Senaryosu                                           |
|------------|----------------------------------------------------|------------------------------------------------------------|
| 200 OK     | İstek başarılı                                     | GET, PUT, PATCH, DELETE başarılı işlemler                   |
| 201 Created| Kaynak başarıyla oluşturuldu                       | POST ile yeni bir kaynak oluşturulduğunda                   |
| 204 No Content | İşlem başarılı, ancak dönüş içeriği yok        | DELETE işlemi başarıyla tamamlandığında                      |

## 2. Yönlendirme Yanıtları

| Durum Kodu | Açıklama                                           | Kullanım Senaryosu                                           |
|------------|----------------------------------------------------|------------------------------------------------------------|
| 301 Moved Permanently | Kaynak kalıcı olarak taşındı             | URL yapısı değiştiğinde                                    |
| 302 Found  | Kaynak geçici olarak başka bir URL'ye yönlendirildi | Geçici yönlendirmeler                                      |
| 304 Not Modified | Kaynak değişmemiş                            | Conditional GET isteklerinde (ETags)                         |

## 3. İstemci Hataları

| Durum Kodu | Açıklama                                           | Kullanım Senaryosu                                           |
|------------|----------------------------------------------------|------------------------------------------------------------|
| 400 Bad Request | Geçersiz istek                                | Validasyon hatası, hatalı format                            |
| 401 Unauthorized | Kimlik doğrulama gerekli                     | Giriş yapılmamış veya token geçersiz                         |
| 403 Forbidden | Yetki yok                                       | Kullanıcı istek için gerekli yetkiye sahip değil             |
| 404 Not Found | Kaynak bulunamadı                              | İstenen kaynak veritabanında mevcut değil                   |
| 405 Method Not Allowed | HTTP metodu desteklenmiyor             | Endpoint bu HTTP metodu desteklemiyor                        |
| 409 Conflict | Kaynak çakışması                                | Kaynak üzerinde sürüm çakışması                             |
| 422 Unprocessable Entity | Veri geçerli ancak işlenemedi        | İş kuralı ihlali, veri geçerli formatına sahip ancak işlenemez |
| 429 Too Many Requests | Çok fazla istek                         | Rate limiting uygulandığında                                |

## 4. Sunucu Hataları

| Durum Kodu | Açıklama                                           | Kullanım Senaryosu                                           |
|------------|----------------------------------------------------|------------------------------------------------------------|
| 500 Internal Server Error | Sunucu hatası                       | Beklenmeyen hata durumları                                  |
| 502 Bad Gateway | Gateway hatası                               | Harici sistemlere erişim hatası                            |
| 503 Service Unavailable | Servis geçici olarak kullanılamıyor   | Bakım modu veya aşırı yük durumu                            |
| 504 Gateway Timeout | Gateway zaman aşımı                       | Harici sistem yanıt vermediğinde                           |

## 5. Durum Kodları Kullanım Kuralları

### 5.1 Başarılı İşlemler

- **GET işlemlerinde**:
  - Kaynak mevcutsa: `200 OK` + veri
  - Kaynak bulunamazsa: `404 Not Found`

- **POST işlemlerinde**:
  - Kaynak başarıyla oluşturulduğunda: `201 Created` + oluşturulan kaynak
  - Validasyon hatası varsa: `400 Bad Request` + hata detayları
  - İş kuralı ihlali varsa: `422 Unprocessable Entity` + hata detayları

- **PUT işlemlerinde**:
  - Kaynak başarıyla güncellendiğinde: `200 OK` + güncellenen kaynak
  - Kaynak bulunamazsa: `404 Not Found`
  - Validasyon hatası varsa: `400 Bad Request` + hata detayları
  - İş kuralı ihlali varsa: `422 Unprocessable Entity` + hata detayları

- **PATCH işlemlerinde**:
  - Kaynak başarıyla kısmen güncellendiğinde: `200 OK` + güncellenen kaynak
  - Kaynak bulunamazsa: `404 Not Found`
  - Validasyon hatası varsa: `400 Bad Request` + hata detayları
  - İş kuralı ihlali varsa: `422 Unprocessable Entity` + hata detayları

- **DELETE işlemlerinde**:
  - Kaynak başarıyla silindiğinde: `200 OK` veya `204 No Content`
  - Kaynak bulunamazsa: `404 Not Found`
  - İş kuralı ihlali varsa: `422 Unprocessable Entity` + hata detayları

### 5.2 Hata Yanıtlarında Mesaj İçeriği

Hata yanıtlarında aşağıdaki bilgiler sağlanmalıdır:

```json
{
  "success": false,
  "message": "Hata açıklaması",
  "statusCode": 400,
  "errorCode": 1001,
  "errors": ["Validasyon hatası 1", "Validasyon hatası 2"]
}
```

- `message`: Kullanıcı dostu, anlaşılır bir hata mesajı
- `statusCode`: HTTP durum kodu
- `errorCode`: İç sistem hata kodu (ErrorCodes sınıfından)
- `errors`: Detaylı hata listesi (özellikle validasyon hataları için)

### 5.3 HTTP Durum Kodu ve ErrorCode İlişkisi

| HTTP Durum Kodu | ErrorCode Aralığı  | Açıklama                                |
|-----------------|--------------------|-----------------------------------------|
| 400             | 1000-1999          | Genel hatalar ve validasyon hataları    |
| 401, 403        | 2000-2999          | Kimlik doğrulama ve yetkilendirme hataları |
| 404             | (Kaynak bazlı)     | Kaynak bulunamadığında ilgili kod       |
| 422             | (Kaynak bazlı)     | İş kuralı ihlali durumunda ilgili kod   |
| 429             | 9000-9099          | Rate limiting hataları                   |
| 500             | 9900-9999          | Sunucu hataları                          |

### 5.4 Ek İçerik

Özel durumlarda yanıta ek bilgiler eklenebilir:

- `X-Pagination`: Sayfalama için meta bilgileri (Response Header)
- `Retry-After`: Rate limiting durumunda bekleme süresi (Response Header)
- `Location`: 201 Created yanıtında oluşturulan kaynağın URL'i (Response Header) 