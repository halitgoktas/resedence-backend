# Rezidans ve Site Yönetim Sistemi - API Yapısı

Bu döküman, Rezidans ve Site Yönetim Sistemi'nin API yapısını, endpoint'lerini ve kullanım örneklerini içermektedir.

## Döküman Bağlantıları
- TEKNIK_MIMARISI.md - Projenin teknik mimari yapısı
- BACKEND_MODEL_YAPISI.md - Backend model yapısı ve ilişkiler
- VERITABANI_SEMASI.md - Veritabanı şeması ve tablolar

## Sunucu Bilgileri
- Sunucu: 127.0.0.1
- SQL Kullanıcı Adı: sa
- SQL Şifre: q.1

## 1. API Genel Yapısı

### 1.1. RESTful API Prensipleri

API, RESTful prensiplerine uygun olarak tasarlanmıştır:

- HTTP metotlarını (GET, POST, PUT, DELETE) uygun şekilde kullanır
- HTTP durum kodlarını standartlara uygun döner
- Kaynaklar (resources) URL'lerde isim olarak kullanılır
- Çoğul kaynak isimleri kullanılır (Örn: /api/apartments, /api/users)
- Alt kaynaklar ana kaynağın altında konumlandırılır (Örn: /api/apartments/{apartmentId}/residents)
- Sorgulama parametreleri için query string kullanılır (Örn: /api/apartments?status=vacant)

### 1.2. API Endpoint Yapısı

API'ler aşağıdaki temel yapıyı takip eder:

```
/api/v1/[resource]/[action]
```

Örneğin:
- `/api/v1/apartments` - Tüm daireleri listeler
- `/api/v1/apartments/{id}` - Belirli bir dairenin detaylarını getirir
- `/api/v1/apartments/filter` - Daireleri filtreleme

### 1.3. Versiyonlama

API'ler URL'de versiyon bilgisini içerir:

- `/api/v1/...` - Versiyon 1
- `/api/v2/...` - Versiyon 2 (ileride gerektiğinde)

Büyük değişikliklerde yeni versiyon çıkarılacak, geriye dönük uyumluluk korunacaktır.

### 1.4. İstek ve Yanıt Formatı

#### İstek (Request) Formatı

Tüm POST ve PUT istekleri JSON formatında gönderilir:

```json
{
  "property1": "value1",
  "property2": "value2"
}
```

#### Yanıt (Response) Formatı

Tüm API yanıtları aşağıdaki standart formatta döner:

```json
{
  "success": true,
  "message": "İşlem başarılı.",
  "data": {
    // Yanıt verileri
  },
  "errors": null,
  "totalCount": 0
}
```

Hata durumunda:

```json
{
  "success": false,
  "message": "Hata mesajı",
  "data": null,
  "errors": [
    "Alan1 için hata mesajı",
    "Alan2 için hata mesajı"
  ],
  "totalCount": 0
}
```

Listeleme yanıtlarında:

```json
{
  "success": true,
  "message": "İşlem başarılı.",
  "data": [
    // Liste öğeleri
  ],
  "errors": null,
  "totalCount": 25
}
```

### 1.5. Kimlik Doğrulama ve Yetkilendirme

API, JWT token tabanlı kimlik doğrulama kullanır:

1. Kullanıcı `/api/v1/auth/login` endpoint'ine giriş bilgilerini gönderir
2. Başarılı giriş sonrası bir JWT token alır
3. Sonraki tüm isteklerde bu token Authorization header'ında gönderilir:
   ```
   Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR...
   ```

### 1.6. Rate Limiting

API'de DDoS koruması için rate limiting uygulanmıştır:

- IP bazlı limitleme: Her IP adresi için dakikada 100 istek
- Kullanıcı bazlı limitleme: Her kullanıcı için saniyede 10 istek
- Aşım durumunda 429 (Too Many Requests) durum kodu döner

### 1.7. Multi-tenant Ayrımı

Tüm isteklerde multi-tenant ayrımı otomatik olarak uygulanır:

- JWT token içinde FirmaId ve SubeId bilgileri bulunur
- Tüm sorgular, kullanıcının FirmaId ve SubeId değerlerine göre filtrelenir
- Farklı firmalara ait verilere erişim engellenir

## 2. API Endpointleri

### 2.1. Kimlik Doğrulama ve Kullanıcı Yönetimi

#### 2.1.1. Giriş ve Kimlik Doğrulama

- **POST** `/api/v1/auth/login`
  - **Açıklama**: Kullanıcı girişi yapar ve token alır
  - **İstek**:
    ```json
    {
      "username": "string",
      "password": "string",
      "rememberMe": true
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Giriş başarılı.",
      "data": {
        "token": "string",
        "refreshToken": "string",
        "expiresIn": 3600,
        "user": {
          "id": 1,
          "username": "string",
          "email": "string",
          "firstName": "string",
          "lastName": "string",
          "roles": ["string"]
        }
      }
    }
    ```

- **POST** `/api/v1/auth/refresh-token`
  - **Açıklama**: Yeni bir token almak için refresh token kullanır
  - **İstek**:
    ```json
    {
      "refreshToken": "string"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Token yenileme başarılı.",
      "data": {
        "token": "string",
        "refreshToken": "string",
        "expiresIn": 3600
      }
    }
    ```

- **POST** `/api/v1/auth/forgot-password`
  - **Açıklama**: Şifre sıfırlama talebi oluşturur
  - **İstek**:
    ```json
    {
      "email": "string"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şifre sıfırlama talimatları e-posta adresinize gönderildi."
    }
    ```

- **POST** `/api/v1/auth/reset-password`
  - **Açıklama**: Şifre sıfırlama
  - **İstek**:
    ```json
    {
      "token": "string",
      "newPassword": "string",
      "confirmPassword": "string"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şifreniz başarıyla sıfırlandı."
    }
    ```

- **POST** `/api/v1/auth/verify-2fa`
  - **Açıklama**: İki faktörlü doğrulama kodunu doğrular
  - **İstek**:
    ```json
    {
      "code": "string",
      "userId": 1
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Doğrulama başarılı.",
      "data": {
        "token": "string",
        "refreshToken": "string",
        "expiresIn": 3600
      }
    }
    ```

#### 2.1.2. Kullanıcı Yönetimi

- **GET** `/api/v1/users`
  - **Açıklama**: Kullanıcıları listeler
  - **Parametreler**: 
    - `page`: Sayfa numarası (Varsayılan: 1)
    - `pageSize`: Sayfa büyüklüğü (Varsayılan: 10)
    - `search`: Arama terimi
    - `roleId`: Rol ID'sine göre filtreleme
    - `isActive`: Aktif durumuna göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kullanıcılar listelendi.",
      "data": [
        {
          "id": 1,
          "username": "string",
          "email": "string",
          "firstName": "string",
          "lastName": "string",
          "isActive": true,
          "lastLoginDate": "2023-01-01T00:00:00Z",
          "roles": ["string"]
        }
      ],
      "totalCount": 25
    }
    ```

- **GET** `/api/v1/users/{id}`
  - **Açıklama**: Belirli bir kullanıcının detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kullanıcı bulundu.",
      "data": {
        "id": 1,
        "username": "string",
        "email": "string",
        "firstName": "string",
        "lastName": "string",
        "phoneNumber": "string",
        "isActive": true,
        "lastLoginDate": "2023-01-01T00:00:00Z",
        "roles": ["string"],
        "twoFactorEnabled": false,
        "preferredLanguage": "tr",
        "preferredCurrency": "TRY"
      }
    }
    ```

- **POST** `/api/v1/users`
  - **Açıklama**: Yeni kullanıcı oluşturur
  - **İstek**:
    ```json
    {
      "username": "string",
      "email": "string",
      "password": "string",
      "firstName": "string",
      "lastName": "string",
      "phoneNumber": "string",
      "roleIds": [1, 2],
      "isActive": true,
      "preferredLanguage": "tr",
      "preferredCurrency": "TRY"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kullanıcı başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "username": "string",
        "email": "string"
      }
    }
    ```

- **PUT** `/api/v1/users/{id}`
  - **Açıklama**: Kullanıcı bilgilerini günceller
  - **İstek**:
    ```json
    {
      "email": "string",
      "firstName": "string",
      "lastName": "string",
      "phoneNumber": "string",
      "roleIds": [1, 2],
      "isActive": true,
      "preferredLanguage": "tr",
      "preferredCurrency": "TRY"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kullanıcı başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/users/{id}`
  - **Açıklama**: Kullanıcıyı siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kullanıcı başarıyla silindi."
    }
    ```

#### 2.1.3. Rol Yönetimi

- **GET** `/api/v1/roles`
  - **Açıklama**: Rolleri listeler
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Roller listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Admin",
          "description": "Sistem yöneticisi",
          "isSystemDefined": true,
          "permissions": [
            {
              "id": 1,
              "name": "Kullanıcı Oluşturma",
              "code": "USER_CREATE"
            }
          ]
        }
      ],
      "totalCount": 5
    }
    ```

- **GET** `/api/v1/roles/{id}`
  - **Açıklama**: Belirli bir rolün detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rol bulundu.",
      "data": {
        "id": 1,
        "name": "Admin",
        "description": "Sistem yöneticisi",
        "isSystemDefined": true,
        "permissions": [
          {
            "id": 1,
            "name": "Kullanıcı Oluşturma",
            "code": "USER_CREATE",
            "category": "UserManagement"
          }
        ]
      }
    }
    ```

- **POST** `/api/v1/roles`
  - **Açıklama**: Yeni rol oluşturur
  - **İstek**:
    ```json
    {
      "name": "string",
      "description": "string",
      "permissionIds": [1, 2, 3]
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rol başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "string"
      }
    }
    ```

- **PUT** `/api/v1/roles/{id}`
  - **Açıklama**: Rol bilgilerini günceller
  - **İstek**:
    ```json
    {
      "name": "string",
      "description": "string",
      "permissionIds": [1, 2, 3]
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rol başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/roles/{id}`
  - **Açıklama**: Rolü siler (sistem tanımlı roller silinemez)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rol başarıyla silindi."
    }
    ```

### 2.2. Firma ve Şube Yönetimi

#### 2.2.1. Firma Yönetimi

- **GET** `/api/v1/companies`
  - **Açıklama**: Firmaları listeler (sadece Admin rolü)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Firmalar listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Örnek Firma A.Ş.",
          "code": "ORNEK",
          "taxNumber": "1234567890",
          "address": "Örnek Adres",
          "phoneNumber": "+901234567890",
          "email": "info@ornekfirma.com",
          "isActive": true,
          "branchCount": 3
        }
      ],
      "totalCount": 10
    }
    ```

- **GET** `/api/v1/companies/{id}`
  - **Açıklama**: Belirli bir firmanın detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Firma bulundu.",
      "data": {
        "id": 1,
        "name": "Örnek Firma A.Ş.",
        "code": "ORNEK",
        "taxNumber": "1234567890",
        "address": "Örnek Adres",
        "city": "İstanbul",
        "district": "Kadıköy",
        "phoneNumber": "+901234567890",
        "email": "info@ornekfirma.com",
        "websiteUrl": "https://www.ornekfirma.com",
        "logoPath": "/uploads/logos/ornek-firma.png",
        "isActive": true,
        "preferredCurrency": "TRY",
        "preferredLanguage": "tr",
        "branches": [
          {
            "id": 1,
            "name": "Merkez Şube",
            "address": "Merkez Şube Adresi"
          }
        ]
      }
    }
    ```

- **POST** `/api/v1/companies`
  - **Açıklama**: Yeni firma oluşturur
  - **İstek**:
    ```json
    {
      "name": "string",
      "code": "string",
      "taxNumber": "string",
      "address": "string",
      "city": "string",
      "district": "string",
      "phoneNumber": "string",
      "email": "string",
      "websiteUrl": "string",
      "preferredCurrency": "TRY",
      "preferredLanguage": "tr"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Firma başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "string"
      }
    }
    ```

- **PUT** `/api/v1/companies/{id}`
  - **Açıklama**: Firma bilgilerini günceller
  - **İstek**:
    ```json
    {
      "name": "string",
      "code": "string",
      "taxNumber": "string",
      "address": "string",
      "city": "string",
      "district": "string",
      "phoneNumber": "string",
      "email": "string",
      "websiteUrl": "string",
      "isActive": true,
      "preferredCurrency": "TRY",
      "preferredLanguage": "tr"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Firma başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/companies/{id}`
  - **Açıklama**: Firmayı siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Firma başarıyla silindi."
    }
    ```

- **POST** `/api/v1/companies/{id}/logo`
  - **Açıklama**: Firma logosunu yükler
  - **İstek**: Form data olarak logo dosyası
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Logo başarıyla yüklendi.",
      "data": {
        "logoPath": "/uploads/logos/ornek-firma.png"
      }
    }
    ```

#### 2.2.2. Şube Yönetimi

- **GET** `/api/v1/branches`
  - **Açıklama**: Şubeleri listeler
  - **Parametreler**:
    - `companyId`: Firma ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şubeler listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Merkez Şube",
          "code": "MRK",
          "companyId": 1,
          "companyName": "Örnek Firma A.Ş.",
          "address": "Şube Adresi",
          "phoneNumber": "+901234567891",
          "isActive": true
        }
      ],
      "totalCount": 3
    }
    ```

- **GET** `/api/v1/branches/{id}`
  - **Açıklama**: Belirli bir şubenin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şube bulundu.",
      "data": {
        "id": 1,
        "name": "Merkez Şube",
        "code": "MRK",
        "companyId": 1,
        "companyName": "Örnek Firma A.Ş.",
        "address": "Şube Adresi",
        "city": "İstanbul",
        "district": "Kadıköy",
        "phoneNumber": "+901234567891",
        "email": "merkez@ornekfirma.com",
        "managerName": "Ali Yılmaz",
        "isActive": true,
        "preferredCurrency": "TRY",
        "preferredLanguage": "tr"
      }
    }
    ```

- **POST** `/api/v1/branches`
  - **Açıklama**: Yeni şube oluşturur
  - **İstek**:
    ```json
    {
      "companyId": 1,
      "name": "string",
      "code": "string",
      "address": "string",
      "city": "string",
      "district": "string",
      "phoneNumber": "string",
      "email": "string",
      "managerName": "string",
      "preferredCurrency": "TRY",
      "preferredLanguage": "tr"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şube başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "string"
      }
    }
    ```

- **PUT** `/api/v1/branches/{id}`
  - **Açıklama**: Şube bilgilerini günceller
  - **İstek**:
    ```json
    {
      "name": "string",
      "code": "string",
      "address": "string",
      "city": "string",
      "district": "string",
      "phoneNumber": "string",
      "email": "string",
      "managerName": "string",
      "isActive": true,
      "preferredCurrency": "TRY",
      "preferredLanguage": "tr"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şube başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/branches/{id}`
  - **Açıklama**: Şubeyi siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Şube başarıyla silindi."
    }
    ```

### 2.3. Site Yönetimi

#### 2.3.1. Site

- **GET** `/api/v1/sites`
  - **Açıklama**: Siteleri listeler
  - **Parametreler**:
    - `page`: Sayfa numarası
    - `pageSize`: Sayfa büyüklüğü
    - `search`: Arama terimi
    - `status`: Durum filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Siteler listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Örnek Rezidans",
          "address": "Örnek Adres",
          "city": "İstanbul",
          "district": "Kadıköy",
          "totalBlocks": 5,
          "totalApartments": 250,
          "status": "Active"
        }
      ],
      "totalCount": 8
    }
    ```

- **GET** `/api/v1/sites/{id}`
  - **Açıklama**: Belirli bir sitenin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Site bulundu.",
      "data": {
        "id": 1,
        "name": "Örnek Rezidans",
        "description": "Lüks rezidans sitesi",
        "address": "Örnek Adres",
        "city": "İstanbul",
        "district": "Kadıköy",
        "postalCode": "34700",
        "totalBlocks": 5,
        "totalApartments": 250,
        "totalArea": 12000.50,
        "greenArea": 5000.25,
        "parkingCapacity": 300,
        "establishmentDate": "2020-01-01T00:00:00Z",
        "siteManagerId": 15,
        "managementCompanyId": 3,
        "status": "Active",
        "amenities": {
          "hasPool": true,
          "hasGym": true,
          "hasSauna": false,
          "hasSecurity": true
        },
        "duesCalculationType": "AreaBased"
      }
    }
    ```

- **POST** `/api/v1/sites`
  - **Açıklama**: Yeni site oluşturur
  - **İstek**:
    ```json
    {
      "name": "string",
      "description": "string",
      "address": "string",
      "city": "string",
      "district": "string",
      "postalCode": "string",
      "totalBlocks": 5,
      "totalApartments": 250,
      "totalArea": 12000.50,
      "greenArea": 5000.25,
      "parkingCapacity": 300,
      "establishmentDate": "2020-01-01T00:00:00Z",
      "siteManagerId": 15,
      "managementCompanyId": 3,
      "status": "Active",
      "amenities": {
        "hasPool": true,
        "hasGym": true,
        "hasSauna": false,
        "hasSecurity": true
      },
      "duesCalculationType": "AreaBased"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Site başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "string"
      }
    }
    ```

- **PUT** `/api/v1/sites/{id}`
  - **Açıklama**: Site bilgilerini günceller
  - **İstek**: Oluşturma isteği ile aynı format
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Site başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/sites/{id}`
  - **Açıklama**: Siteyi siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Site başarıyla silindi."
    }
    ```

#### 2.3.2. Blok

- **GET** `/api/v1/blocks`
  - **Açıklama**: Blokları listeler
  - **Parametreler**:
    - `siteId`: Site ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Bloklar listelendi.",
      "data": [
        {
          "id": 1,
          "name": "A Blok",
          "siteId": 1,
          "siteName": "Örnek Rezidans",
          "totalFloors": 15,
          "totalApartments": 60
        }
      ],
      "totalCount": 5
    }
    ```

- **GET** `/api/v1/blocks/{id}`
  - **Açıklama**: Belirli bir bloğun detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Blok bulundu.",
      "data": {
        "id": 1,
        "name": "A Blok",
        "description": "A Blok açıklaması",
        "siteId": 1,
        "siteName": "Örnek Rezidans",
        "totalFloors": 15,
        "totalApartments": 60,
        "apartmentsPerFloor": 4,
        "buildingArea": 1500.75,
        "constructionYear": 2019,
        "blockType": "Apartment",
        "blockManagerId": 12,
        "numberOfElevators": 2,
        "heatingSystem": "Merkezi Isıtma"
      }
    }
    ```

- **POST** `/api/v1/blocks`
  - **Açıklama**: Yeni blok oluşturur
  - **İstek**:
    ```json
    {
      "siteId": 1,
      "name": "string",
      "description": "string",
      "totalFloors": 15,
      "totalApartments": 60,
      "apartmentsPerFloor": 4,
      "buildingArea": 1500.75,
      "constructionYear": 2019,
      "blockType": "Apartment",
      "blockManagerId": 12,
      "numberOfElevators": 2,
      "heatingSystem": "