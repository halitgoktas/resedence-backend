# Mekik Residence Manager - API Yapısı ve Endpoint Durumu

Bu dokümantasyon, Mekik Residence Manager projesinin API yapısını ve endpoint'lerinin mevcut durumunu detaylandırmaktadır.

## API Mimarisi

Mekik Residence Manager API'si, RESTful prensiplere uygun olarak tasarlanmıştır ve aşağıdaki özelliklere sahiptir:

### Versiyonlama

API, URL path tabanlı versiyonlama stratejisi kullanmaktadır:

- `/api/v1/` - Versiyon 1 endpoint'leri
- `/api/v2/` - Versiyon 2 endpoint'leri (gelecekte kullanım için)

Bu yaklaşım, API'nin geriye dönük uyumluluğunu sağlarken yeni özelliklerin eklenmesine de olanak tanır.

### Controller Yapısı

Tüm controller'lar `BaseApiController` sınıfından türetilmiştir. Bu temel sınıf:

- Standart API yanıt formatını sağlar
- Ortak hata işleme mantığını içerir
- Tenant bazlı filtreleme mekanizmasını destekler

Her bir controller, ilgili domain entity'si için CRUD ve özel işlemleri içerir.

### Endpoint Standardı

API endpoint'leri aşağıdaki standarda uygun olarak tasarlanmıştır:

- **GET** `/api/v1/[controller]` - Koleksiyon listeleme
- **GET** `/api/v1/[controller]/{id}` - Tekil kayıt getirme
- **POST** `/api/v1/[controller]` - Yeni kayıt oluşturma
- **PUT** `/api/v1/[controller]/{id}` - Kayıt güncelleme (tüm alanlar)
- **PATCH** `/api/v1/[controller]/{id}` - Kayıt güncelleme (kısmi)
- **DELETE** `/api/v1/[controller]/{id}` - Kayıt silme

### Yanıt Formatı

Tüm API yanıtları aşağıdaki standart formatta döner:

```json
{
  "success": true/false,
  "data": { ... },
  "message": "İşlem başarılı",
  "errors": [ ... ]
}
```

### Güvenlik ve Yetkilendirme

- Tüm API endpoint'leri JWT token doğrulaması gerektirir (AuthController hariç)
- Endpoint'ler rol bazlı yetkilendirme kullanır (`[Authorize(Roles = "Admin")]`)
- Tenant bazlı veri izolasyonu middleware tarafından sağlanır

## Mevcut Endpoint'ler ve Durumları

### Kimlik Doğrulama ve Yetkilendirme

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/auth/login` | POST | Kullanıcı girişi | [x] |
| `/api/v1/auth/register` | POST | Yeni kullanıcı kaydı | [x] |
| `/api/v1/auth/refresh-token` | POST | Token yenileme | [x] |
| `/api/v1/auth/logout` | POST | Çıkış yapma | [x] |
| `/api/v1/auth/forgot-password` | POST | Şifre sıfırlama talebi | [x] |
| `/api/v1/auth/reset-password` | POST | Şifre sıfırlama | [x] |

### Firma ve Şube Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/company` | GET | Firma listesi | [x] |
| `/api/v1/company/{id}` | GET | Firma detayı | [x] |
| `/api/v1/company` | POST | Yeni firma oluşturma | [x] |
| `/api/v1/company/{id}` | PUT | Firma güncelleme | [x] |
| `/api/v1/company/{id}` | DELETE | Firma silme | [x] |
| `/api/v1/branch` | GET | Şube listesi | [x] |
| `/api/v1/branch/{id}` | GET | Şube detayı | [x] |
| `/api/v1/branch` | POST | Yeni şube oluşturma | [x] |
| `/api/v1/branch/{id}` | PUT | Şube güncelleme | [x] |
| `/api/v1/branch/{id}` | DELETE | Şube silme | [x] |

### Blok ve Daire Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/block` | GET | Blok listesi | [x] |
| `/api/v1/block/{id}` | GET | Blok detayı | [x] |
| `/api/v1/block` | POST | Yeni blok oluşturma | [x] |
| `/api/v1/block/{id}` | PUT | Blok güncelleme | [x] |
| `/api/v1/block/{id}` | DELETE | Blok silme | [x] |
| `/api/v1/apartment` | GET | Daire listesi | [x] |
| `/api/v1/apartment/{id}` | GET | Daire detayı | [x] |
| `/api/v1/apartment` | POST | Yeni daire oluşturma | [x] |
| `/api/v1/apartment/{id}` | PUT | Daire güncelleme | [x] |
| `/api/v1/apartment/{id}` | DELETE | Daire silme | [x] |

### Sakin Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/resident` | GET | Sakin listesi | [x] |
| `/api/v1/resident/{id}` | GET | Sakin detayı | [x] |
| `/api/v1/resident` | POST | Yeni sakin oluşturma | [x] |
| `/api/v1/resident/{id}` | PUT | Sakin güncelleme | [x] |
| `/api/v1/resident/{id}` | DELETE | Sakin silme | [x] |
| `/api/v1/apartment-resident` | POST | Daire-sakin ilişkisi oluşturma | [ ] |
| `/api/v1/apartment-resident/{id}` | DELETE | Daire-sakin ilişkisi silme | [ ] |

### Aidat ve Finans Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/dues-definition` | GET | Aidat tanımı listesi | [x] |
| `/api/v1/dues-definition/{id}` | GET | Aidat tanımı detayı | [x] |
| `/api/v1/dues-definition` | POST | Yeni aidat tanımı oluşturma | [x] |
| `/api/v1/dues-definition/{id}` | PUT | Aidat tanımı güncelleme | [x] |
| `/api/v1/dues-definition/{id}` | DELETE | Aidat tanımı silme | [x] |
| `/api/v1/payment` | GET | Ödeme listesi | [x] |
| `/api/v1/payment/{id}` | GET | Ödeme detayı | [x] |
| `/api/v1/payment` | POST | Yeni ödeme oluşturma | [x] |
| `/api/v1/payment/{id}` | PUT | Ödeme güncelleme | [x] |
| `/api/v1/payment/{id}` | DELETE | Ödeme silme | [x] |
| `/api/v1/financial/summary` | GET | Finansal özet raporu | [x] |
| `/api/v1/financial/block-payment-status` | GET | Blok bazlı ödeme durumu | [x] |
| `/api/v1/financial/apartment-payment-history` | GET | Daire bazlı ödeme geçmişi | [x] |

### Bakım ve Tamir

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/maintenance-plan` | GET | Bakım planı listesi | [x] |
| `/api/v1/maintenance-plan/{id}` | GET | Bakım planı detayı | [x] |
| `/api/v1/maintenance-plan` | POST | Yeni bakım planı oluşturma | [x] |
| `/api/v1/maintenance-plan/{id}` | PUT | Bakım planı güncelleme | [x] |
| `/api/v1/maintenance-plan/{id}` | DELETE | Bakım planı silme | [x] |
| `/api/v1/maintenance-schedule` | GET | Bakım takvimi | [x] |
| `/api/v1/maintenance-schedule/assign` | POST | Bakım görevi atama | [x] |

### Ekipman ve Envanter

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/equipment-inventory` | GET | Ekipman listesi | [x] |
| `/api/v1/equipment-inventory/{id}` | GET | Ekipman detayı | [x] |
| `/api/v1/equipment-inventory` | POST | Yeni ekipman oluşturma | [x] |
| `/api/v1/equipment-inventory/{id}` | PUT | Ekipman güncelleme | [x] |
| `/api/v1/equipment-inventory/{id}` | DELETE | Ekipman silme | [x] |

### Hizmet Talepleri

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/service-request` | GET | Hizmet talebi listesi | [x] |
| `/api/v1/service-request/{id}` | GET | Hizmet talebi detayı | [x] |
| `/api/v1/service-request` | POST | Yeni hizmet talebi oluşturma | [x] |
| `/api/v1/service-request/{id}` | PUT | Hizmet talebi güncelleme | [x] |
| `/api/v1/service-request/{id}` | DELETE | Hizmet talebi silme | [x] |
| `/api/v1/service-request/{id}/assign` | POST | Hizmet talebi atama | [x] |
| `/api/v1/service-request/{id}/complete` | POST | Hizmet talebi tamamlama | [x] |
| `/api/v1/service-request/{id}/add-note` | POST | Hizmet talebine not ekleme | [x] |
| `/api/v1/service-request/{id}/add-attachment` | POST | Hizmet talebine dosya ekleme | [x] |
| `/api/v1/service-request/by-status/{status}` | GET | Duruma göre talep listesi | [x] |
| `/api/v1/service-request/by-priority/{priority}` | GET | Önceliğe göre talep listesi | [x] |
| `/api/v1/service-request/by-category/{categoryId}` | GET | Kategoriye göre talep listesi | [x] |

### Bildirim Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/notification-settings` | GET | Bildirim ayarları | [x] |
| `/api/v1/notification-settings` | PUT | Bildirim ayarları güncelleme | [x] |
| `/api/v1/notification-log` | GET | Bildirim kayıtları | [x] |
| `/api/v1/notification-preference` | GET | Kullanıcı bildirim tercihleri | [x] |
| `/api/v1/notification-preference` | PUT | Kullanıcı bildirim tercihleri güncelleme | [x] |

### E-posta ve SMS Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/email/send` | POST | E-posta gönderme | [ ] |
| `/api/v1/email/template` | GET | E-posta şablonu listesi | [ ] |
| `/api/v1/email/template/{id}` | GET | E-posta şablonu detayı | [ ] |
| `/api/v1/email/template` | POST | Yeni e-posta şablonu oluşturma | [ ] |
| `/api/v1/email/template/{id}` | PUT | E-posta şablonu güncelleme | [ ] |
| `/api/v1/email/log` | GET | E-posta gönderim kayıtları | [ ] |
| `/api/v1/sms/send` | POST | SMS gönderme | [ ] |
| `/api/v1/sms/log` | GET | SMS gönderim kayıtları | [ ] |

### KBS (Kimlik Bildirim Sistemi) Entegrasyonu

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/kbs-settings` | GET | KBS ayarları | [x] |
| `/api/v1/kbs-settings` | PUT | KBS ayarları güncelleme | [x] |
| `/api/v1/kbs-integration/send-notification` | POST | KBS bildirimi gönderme | [ ] |
| `/api/v1/kbs-integration/check-status` | GET | KBS bildirim durumu kontrolü | [ ] |

### Dosya ve Depolama Yönetimi

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/storage/upload` | POST | Dosya yükleme | [x] |
| `/api/v1/storage/files` | GET | Dosya listesi | [x] |
| `/api/v1/storage/files/{id}` | GET | Dosya detayı | [x] |
| `/api/v1/storage/files/{id}` | DELETE | Dosya silme | [x] |
| `/api/v1/storage/download/{id}` | GET | Dosya indirme | [x] |

### Sistem Ayarları ve Referans Veri

| Endpoint | HTTP Metodu | Açıklama | Durum |
|----------|-------------|----------|-------|
| `/api/v1/enum/{enumType}` | GET | Enum değerleri | [x] |
| `/api/v1/settings/system` | GET | Sistem ayarları | [ ] |
| `/api/v1/settings/system` | PUT | Sistem ayarları güncelleme | [ ] |

## API Tamamlanma Oranları

Aşağıda, farklı domain alanlarına göre API tamamlanma oranları listelenmiştir:

| Domain | Tamamlanma Oranı | Açıklama |
|--------|-----------------|----------|
| Kimlik Doğrulama | %100 | [x] Tüm endpoint'ler tamamlanmış durumda |
| Firma ve Şube | %100 | [x] Tüm endpoint'ler tamamlanmış durumda |
| Blok ve Daire | %100 | [x] Tüm endpoint'ler tamamlanmış durumda |
| Sakin Yönetimi | %80 | [x] Temel endpoint'ler tamamlanmış, [ ] ilişki yönetimi kısmen tamamlanmış |
| Aidat ve Finans | %90 | [x] Aidat tanımları tamamlandı, [x] ödeme modülü tamamlandı |
| Bakım ve Tamir | %90 | [x] Tüm temel endpoint'ler tamamlanmış |
| Ekipman ve Envanter | %90 | [x] Tüm temel endpoint'ler tamamlanmış |
| Hizmet Talepleri | %90 | [x] Temel endpoint'ler tamamlanmış, [x] ileri işlemler tamamlanmış |
| Bildirim Yönetimi | %90 | [x] Tüm temel endpoint'ler tamamlanmış |
| E-posta ve SMS | %50 | [ ] E-posta modülü kısmen tamamlanmış, [ ] SMS modülü başlanmamış |
| KBS Entegrasyonu | %70 | [x] Temel endpoint'ler tamamlanmış, [ ] entegrasyon kısmen tamamlanmış |
| Dosya Yönetimi | %100 | [x] Tüm endpoint'ler tamamlanmış durumda |
| Sistem Ayarları | %30 | [x] Bazı endpoint'ler tamamlanmış, [ ] çoğu başlanmamış |

## Öncelikli API Geliştirmeleri

Projenin kısa vadede tamamlanması için öncelikli API geliştirmeleri:

1. **Aidat ve Finans Modülü**
   - [x] Ödeme endpoint'lerinin tamamlanması
   - [x] Aidat tanımı geliştirmelerinin tamamlanması
   - [ ] Finansal raporlama geliştirmelerinin yapılması

2. **Hizmet Talepleri**
   - [x] Hizmet talebi atama ve tamamlama endpoint'lerinin implementasyonu
   - [x] Hizmet talebi durum akışının geliştirilmesi
   - [ ] Performans optimizasyonları ve raporlama geliştirmeleri

3. **E-posta ve SMS Entegrasyonu**
   - [ ] E-posta gönderim endpoint'lerinin tamamlanması
   - [ ] SMS gönderim altyapısının ve endpoint'lerinin implementasyonu

4. **Sakin Yönetimi**
   - [ ] Daire-sakin ilişkisi endpoint'lerinin tamamlanması
   - [ ] Sakin giriş/çıkış işlemlerinin implementasyonu

5. **KBS Entegrasyonu**
   - [ ] Bildirim gönderme ve durum kontrolü endpoint'lerinin tamamlanması
   - [ ] KBS entegrasyonunun test edilmesi ve hata ayıklama

## API Dokümantasyonu

### Swagger Entegrasyonu

Projede API dokümantasyonu için Swagger/OpenAPI entegrasyonu yapılmıştır. Swagger UI aşağıdaki URL'den erişilebilir:

```
https://[api-host]/swagger
```

API dokümantasyonu:
- Endpoint açıklamaları
- Request ve response modelleri
- Örnek istekler
- Yetkilendirme gereksinimleri

Swagger dokümantasyonu, gerçek zamanlı olarak koddan üretilmektedir ve XML yorum satırları ile zenginleştirilmektedir.

## Sonuç ve Öneriler

Mekik Residence Manager API'si, core iş akışları için temel altyapıyı sağlamış durumdadır. Ödeme, hizmet talepleri ve entegrasyon modüllerinin tamamlanması, sistemin tam fonksiyonel hale gelmesi için öncelikli olarak gereklidir.

### Öneriler

1. **Standart Endpoint Yapısı**: Tüm controller'lar için standart CRUD endpoint yapısı uygulanmalı
2. **Validation**: Tüm endpoint'ler için FluentValidation ile kapsamlı validasyon kuralları eklenme
3. **Global Error Handling**: Global exception handling mekanizması geliştirilerek tüm API'lerde tutarlı hata yanıtları sağlanma
4. **API Testing**: Postman veya Newman ile otomatik API test koleksiyonları oluşturulma
5. **Rate Limiting**: Kritik endpoint'ler için rate limiting uygulanma
6. **API Documentation**: Swagger dokümantasyonunun XML yorum satırları ile zenginleştirilme

API geliştirmeleri tamamlandıkça, bu dokümantasyon güncellenmelidir. 