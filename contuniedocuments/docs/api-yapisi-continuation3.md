- **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rezervasyon başarıyla iptal edildi."
    }
    ```

### 2.6. Teknik Servis ve Bakım

#### 2.6.1. Hizmet Tipleri

- **GET** `/api/v1/service-types`
  - **Açıklama**: Hizmet tiplerini listeler
  - **Parametreler**:
    - `departmentId`: Departman ID'sine göre filtreleme
    - `isPaid`: Ücretli/ücretsiz filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet tipleri listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Musluk Tamiri",
          "departmentId": 2,
          "departmentName": "Teknik Servis",
          "price": 100.00,
          "currency": "TRY",
          "estimatedDuration": 60,
          "requiresApproval": false
        }
      ],
      "totalCount": 15
    }
    ```

- **GET** `/api/v1/service-types/{id}`
  - **Açıklama**: Belirli bir hizmet tipinin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet tipi bulundu.",
      "data": {
        "id": 1,
        "name": "Musluk Tamiri",
        "description": "Musluk ve batarya tamiri hizmeti",
        "departmentId": 2,
        "departmentName": "Teknik Servis",
        "price": 100.00,
        "currency": "TRY",
        "estimatedDuration": 60,
        "requiresApproval": false,
        "chargeToApartment": true
      }
    }
    ```

- **POST** `/api/v1/service-types`
  - **Açıklama**: Yeni hizmet tipi oluşturur
  - **İstek**:
    ```json
    {
      "name": "Musluk Tamiri",
      "description": "Musluk ve batarya tamiri hizmeti",
      "departmentId": 2,
      "price": 100.00,
      "currency": "TRY",
      "estimatedDuration": 60,
      "requiresApproval": false,
      "chargeToApartment": true
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet tipi başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "Musluk Tamiri"
      }
    }
    ```

- **PUT** `/api/v1/service-types/{id}`
  - **Açıklama**: Hizmet tipi bilgilerini günceller
  - **İstek**: Oluşturma isteği ile aynı format
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet tipi başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/service-types/{id}`
  - **Açıklama**: Hizmet tipini siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet tipi başarıyla silindi."
    }
    ```

#### 2.6.2. Hizmet Talepleri

- **GET** `/api/v1/service-requests`
  - **Açıklama**: Hizmet taleplerini listeler
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
    - `status`: Durum (Pending, Approved, Scheduled, InProgress, Completed, Cancelled)
    - `userId`: Kullanıcı ID'sine göre filtreleme
    - `apartmentId`: Daire ID'sine göre filtreleme
    - `serviceTypeId`: Hizmet tipi ID'sine göre filtreleme
    - `priority`: Öncelik (Low, Medium, High, Urgent)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talepleri listelendi.",
      "data": [
        {
          "id": 1,
          "userId": 5,
          "userName": "Ahmet Yılmaz",
          "apartmentId": 1,
          "apartmentNumber": "A-101",
          "serviceTypeId": 1,
          "serviceTypeName": "Musluk Tamiri",
          "requestDate": "2023-07-10T09:00:00Z",
          "status": "Scheduled",
          "priority": "Medium",
          "scheduledDate": "2023-07-12T14:00:00Z"
        }
      ],
      "totalCount": 85
    }
    ```

- **GET** `/api/v1/service-requests/{id}`
  - **Açıklama**: Belirli bir hizmet talebinin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi bulundu.",
      "data": {
        "id": 1,
        "userId": 5,
        "userName": "Ahmet Yılmaz",
        "apartmentId": 1,
        "apartmentNumber": "A-101",
        "blockName": "A Blok",
        "siteName": "Örnek Rezidans",
        "serviceTypeId": 1,
        "serviceTypeName": "Musluk Tamiri",
        "requestDate": "2023-07-10T09:00:00Z",
        "scheduledDate": "2023-07-12T14:00:00Z",
        "scheduledTimeStart": "14:00:00",
        "scheduledTimeEnd": "15:00:00",
        "completionDate": null,
        "status": "Scheduled",
        "assignedPersonnelId": 12,
        "assignedPersonnelName": "Mehmet Ustaoğlu",
        "price": 100.00,
        "currency": "TRY",
        "paymentId": null,
        "notes": "Banyodaki musluk damlatıyor",
        "priority": "Medium",
        "attachmentPaths": [
          "/uploads/service_requests/sr_1_1.jpg",
          "/uploads/service_requests/sr_1_2.jpg"
        ],
        "rating": null,
        "feedback": null
      }
    }
    ```

- **POST** `/api/v1/service-requests`
  - **Açıklama**: Yeni hizmet talebi oluşturur
  - **İstek**:
    ```json
    {
      "userId": 5,
      "apartmentId": 1,
      "serviceTypeId": 1,
      "notes": "Banyodaki musluk damlatıyor",
      "priority": "Medium",
      "attachments": [
        {
          "fileName": "musluk_foto_1.jpg",
          "fileData": "base64_encoded_file_data..."
        }
      ]
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "price": 100.00,
        "currency": "TRY"
      }
    }
    ```

- **PUT** `/api/v1/service-requests/{id}/status`
  - **Açıklama**: Hizmet talebi durumunu günceller
  - **İstek**:
    ```json
    {
      "status": "Approved",
      "notes": "Talep onaylandı, teknisyen atanacak."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi durumu başarıyla güncellendi."
    }
    ```

- **PUT** `/api/v1/service-requests/{id}/schedule`
  - **Açıklama**: Hizmet talebi için randevu planlar
  - **İstek**:
    ```json
    {
      "scheduledDate": "2023-07-12T14:00:00Z",
      "scheduledTimeStart": "14:00:00",
      "scheduledTimeEnd": "15:00:00",
      "assignedPersonnelId": 12,
      "notes": "Öğleden sonra hizmet verilecek."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi başarıyla planlandı."
    }
    ```

- **PUT** `/api/v1/service-requests/{id}/complete`
  - **Açıklama**: Hizmet talebini tamamlar
  - **İstek**:
    ```json
    {
      "completionDate": "2023-07-12T14:45:00Z",
      "actualPrice": 120.00,
      "currency": "TRY",
      "notes": "Musluk tamiri tamamlandı, ek parça gerektiği için fiyat değişti.",
      "attachments": [
        {
          "fileName": "tamamlanan_is.jpg",
          "fileData": "base64_encoded_file_data..."
        }
      ]
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi başarıyla tamamlandı."
    }
    ```

- **POST** `/api/v1/service-requests/{id}/rating`
  - **Açıklama**: Hizmet talebine değerlendirme ekler
  - **İstek**:
    ```json
    {
      "rating": 4,
      "feedback": "Hızlı ve temiz bir şekilde tamir edildi, teşekkürler."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Değerlendirmeniz için teşekkürler."
    }
    ```

- **POST** `/api/v1/service-requests/{id}/cancel`
  - **Açıklama**: Hizmet talebini iptal eder
  - **İstek**:
    ```json
    {
      "cancelReason": "Müşteri talebi üzerine",
      "notes": "Müşteri kendi çözdüğünü belirtti."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Hizmet talebi başarıyla iptal edildi."
    }
    ```

### 2.7. KBS (Kimlik Bildirim Sistemi) Entegrasyonu

- **GET** `/api/v1/kbs/notifications`
  - **Açıklama**: KBS bildirimlerini listeler
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
    - `status`: Durum (Pending, Success, Error)
    - `userId`: Kullanıcı ID'sine göre filtreleme
    - `notificationType`: Bildirim tipi (CheckIn, CheckOut)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "KBS bildirimleri listelendi.",
      "data": [
        {
          "id": 1,
          "userId": 5,
          "userName": "Ahmet Yılmaz",
          "relatedPersonId": 5,
          "relatedPersonName": "Ahmet Yılmaz",
          "relatedPersonType": "User",
          "notificationType": "CheckIn",
          "notificationDate": "2023-07-10T09:00:00Z",
          "status": "Success",
          "systemRecordNumber": "KBS-2023-001"
        }
      ],
      "totalCount": 120
    }
    ```

- **GET** `/api/v1/kbs/notifications/{id}`
  - **Açıklama**: Belirli bir KBS bildiriminin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "KBS bildirimi bulundu.",
      "data": {
        "id": 1,
        "userId": 5,
        "userName": "Ahmet Yılmaz",
        "relatedPersonId": 5,
        "relatedPersonName": "Ahmet Yılmaz",
        "relatedPersonType": "User",
        "notificationType": "CheckIn",
        "notificationDate": "2023-07-10T09:00:00Z",
        "status": "Success",
        "errorMessage": null,
        "systemRecordNumber": "KBS-2023-001",
        "responseDetails": {
          "responseCode": "200",
          "responseMessage": "Bildirim başarılı"
        },
        "processedByUserId": 3,
        "processedByUserName": "Admin User",
        "isAutomatic": false
      }
    }
    ```

- **POST** `/api/v1/kbs/notifications`
  - **Açıklama**: Yeni KBS bildirimi oluşturur
  - **İstek**:
    ```json
    {
      "userId": 5,
      "relatedPersonId": 5,
      "relatedPersonType": "User",
      "notificationType": "CheckIn",
      "notificationDate": "2023-07-10T09:00:00Z"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "KBS bildirimi başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "status": "Pending"
      }
    }
    ```

- **POST** `/api/v1/kbs/batch-notification`
  - **Açıklama**: Toplu KBS bildirimi oluşturur
  - **İstek**:
    ```json
    {
      "reservationId": null,
      "apartmentId": 1,
      "notificationType": "CheckIn",
      "notificationDate": "2023-07-10T09:00:00Z",
      "userIds": [5, 8, 12]
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "3 adet KBS bildirimi başarıyla oluşturuldu."
    }
    ```

- **POST** `/api/v1/kbs/resend/{id}`
  - **Açıklama**: Başarısız KBS bildirimini tekrar gönderir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "KBS bildirimi başarıyla tekrar gönderildi."
    }
    ```

### 2.8. Para Birimi ve Döviz Kuru Yönetimi

- **GET** `/api/v1/currencies`
  - **Açıklama**: Para birimlerini listeler
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Para birimleri listelendi.",
      "data": [
        {
          "id": 1,
          "code": "TRY",
          "name": "Türk Lirası",
          "symbol": "₺",
          "exchangeRate": 1.0,
          "lastUpdateDate": "2023-07-10T09:00:00Z",
          "isBaseCurrency": true
        },
        {
          "id": 2,
          "code": "USD",
          "name": "Amerikan Doları",
          "symbol": "$",
          "exchangeRate": 25.6785,
          "lastUpdateDate": "2023-07-10T09:00:00Z",
          "isBaseCurrency": false
        }
      ],
      "totalCount": 4
    }
    ```

- **GET** `/api/v1/currencies/{id}`
  - **Açıklama**: Belirli bir para biriminin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Para birimi bulundu.",
      "data": {
        "id": 2,
        "code": "USD",
        "name": "Amerikan Doları",
        "symbol": "$",
        "exchangeRate": 25.6785,
        "lastUpdateDate": "2023-07-10T09:00:00Z",
        "source": "TCMB",
        "isBaseCurrency": false
      }
    }
    ```

- **POST** `/api/v1/currencies`
  - **Açıklama**: Yeni para birimi ekler
  - **İstek**:
    ```json
    {
      "code": "GBP",
      "name": "İngiliz Sterlini",
      "symbol": "£",
      "exchangeRate": 33.2145,
      "source": "TCMB",
      "isBaseCurrency": false
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Para birimi başarıyla eklendi.",
      "data": {
        "id": 3,
        "code": "GBP"
      }
    }
    ```

- **PUT** `/api/v1/currencies/{id}`
  - **Açıklama**: Para birimi bilgilerini günceller
  - **İstek**:
    ```json
    {
      "name": "İngiliz Sterlini",
      "symbol": "£",
      "exchangeRate": 33.5678,
      "source": "Manual",
      "isBaseCurrency": false
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Para birimi başarıyla güncellendi."
    }
    ```

- **PUT** `/api/v1/currencies/refresh-rates`
  - **Açıklama**: Döviz kurlarını TCMB'den güncelleştirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Döviz kurları başarıyla güncellendi.",
      "data": {
        "updatedCurrencies": 3,
        "source": "TCMB",
        "lastUpdateDate": "2023-07-10T10:15:00Z"
      }
    }
    ```

- **GET** `/api/v1/currency-history`
  - **Açıklama**: Döviz kuru geçmişini listeler
  - **Parametreler**:
    - `currencyCode`: Para birimi kodu
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Döviz kuru geçmişi listelendi.",
      "data": [
        {
          "id": 1,
          "currencyCode": "USD",
          "baseCode": "TRY",
          "buyingRate": 25.5678,
          "sellingRate": 25.6785,
          "effectiveRate": 25.6232,
          "source": "TCMB",
          "fetchDate": "2023-07-09T09:00:00Z"
        },
        {
          "id": 2,
          "currencyCode": "USD",
          "baseCode": "TRY",
          "buyingRate": 25.6789,
          "sellingRate": 25.7896,
          "effectiveRate": 25.7343,
          "source": "TCMB",
          "fetchDate": "2023-07-10T09:00:00Z"
        }
      ],
      "totalCount": 120
    }
    ```

- **POST** `/api/v1/currency/convert`
  - **Açıklama**: Bir para biriminden diğerine dönüştürme yapar
  - **İstek**:
    ```json
    {
      "amount": 100.00,
      "fromCurrency": "USD",
      "toCurrency": "TRY",
      "date": "2023-07-10T00:00:00Z"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Dönüştürme başarılı.",
      "data": {
        "amount": 100.00,
        "fromCurrency": "USD",
        "toCurrency": "TRY",
        "convertedAmount": 2567.85,
        "exchangeRate": 25.6785,
        "date": "2023-07-10T00:00:00Z"
      }
    }
    ```

### 2.9. Raporlama

- **GET** `/api/v1/reports/types`
  - **Açıklama**: Kullanılabilir rapor tiplerini listeler
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rapor tipleri listelendi.",
      "data": [
        {
          "id": "financial-summary",
          "name": "Finansal Özet Raporu",
          "description": "Belirli bir dönem için gelir-gider özeti",
          "parameters": [
            {
              "name": "startDate",
              "type": "date",
              "required": true
            },
            {
              "name": "endDate",
              "type": "date",
              "required": true
            },
            {
              "name": "siteId",
              "type": "number",
              "required": false
            }
          ],
          "formats": ["PDF", "Excel", "CSV"]
        }
      ],
      "totalCount": 12
    }
    ```

- **POST** `/api/v1/reports/generate`
  - **Açıklama**: Rapor oluşturur
  - **İstek**:
    ```json
    {
      "reportType": "financial-summary",
      "parameters": {
        "startDate": "2023-07-01T00:00:00Z",
        "endDate": "2023-07-31T23:59:59Z",
        "siteId": 1
      },
      "format": "PDF"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rapor başarıyla oluşturuldu.",
      "data": {
        "reportId": "rpt-2023-07-fin-1",
        "reportUrl": "/api/v1/reports/download/rpt-2023-07-fin-1",
        "fileName": "finansal-ozet-temmuz-2023.pdf",
        "expiryDate": "2023-08-10T00:00:00Z"
      }
    }
    ```

- **GET** `/api/v1/reports/download/{reportId}`
  - **Açıklama**: Oluşturulan raporu indirir
  - **Yanıt**: İlgili formatta rapor dosyası

- **GET** `/api/v1/dashboard/stats`
  - **Açıklama**: Dashboard için özet istatistikleri getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Dashboard istatistikleri getirildi.",
      "data": {
        "totalSites": 3,
        "totalBuildings": 12,
        "totalApartments": 450,
        "occupancyRate": 87.5,
        "pendingServiceRequests": 15,
        "upcomingReservations": 8,
        "financialSummary": {
          "currentMonthIncome": 225000.00,
          "currentMonthExpenses": 180000.00,
          "balance": 45000.00,
          "currency": "TRY",
          "duesCollectionRate": 92.3,
          "pendingDuesAmount": 15000.00
        },
        "recentActivities": [
          {
            "id": 1024,
            "type": "ServiceRequest",
            "description": "Yeni hizmet talebi oluşturuldu",
            "relatedEntityId": 85,
            "createdAt": "2023-07-10T14:25:00Z",
            "user": "Ahmet Yılmaz"
          }
        ]
      }
    }
    ```

- **GET** `/api/v1/dashboard/occupancy`
  - **Açıklama**: Doluluk oranı istatistiklerini getirir
  - **Parametreler**:
    - `siteId`: Site ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Doluluk oranı istatistikleri getirildi.",
      "data": {
        "totalApartments": 450,
        "occupiedByOwner": 225,
        "occupiedByTenant": 169,
        "vacant": 56,
        "occupancyRate": 87.5,
        "occupancyByBlock": [
          {
            "blockId": 1,
            "blockName": "A Blok",
            "totalApartments": 60,
            "occupiedCount": 54,
            "occupancyRate": 90.0
          }
        ],
        "occupancyHistory": [
          {
            "month": "2023-01",
            "occupancyRate": 80.2
          },
          {
            "month": "2023-02",
            "occupancyRate": 82.5
          }
        ]
      }
    }
    ```

- **GET** `/api/v1/dashboard/financial`
  - **Açıklama**: Finansal özet istatistiklerini getirir
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
    - `siteId`: Site ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Finansal özet istatistikleri getirildi.",
      "data": {
        "totalIncome": 225000.00,
        "totalExpenses": 180000.00,
        "balance": 45000.00,
        "currency": "TRY",
        "duesCollectionRate": 92.3,
        "pendingDuesAmount": 15000.00,
        "incomeByCategory": [
          {
            "category": "Aidat",
            "amount": 200000.00,
            "percentage": 88.9
          },
          {
            "category": "Aktivite Alanları",
            "amount": 25000.00,
            "percentage": 11.1
          }
        ],
        "expensesByCategory": [
          {
            "category": "Personel",
            "amount": 80000.00,
            "percentage": 44.4
          },
          {
            "category": "Bakım",
            "amount": 50000.00,
            "percentage": 27.8
          },
          {
            "category": "Elektrik",
            "amount": 30000.00,
            "percentage": 16.7
          },
          {
            "category": "Su",
            "amount": 20000.00,
            "percentage": 11.1
          }
        ]
      }
    }
    ```

## 3. HTTP Durum Kodları

API, aşağıdaki HTTP durum kodlarını döndürür:

- **200 OK**: İstek başarılı ve yanıt içeriği döndürülüyor
- **201 Created**: Yeni kayıt başarıyla oluşturuldu
- **204 No Content**: İstek başarılı ancak yanıt içeriği yok (genellikle silme işlemlerinde)
- **400 Bad Request**: İstek formatı veya parametreleri hatalı
- **401 Unauthorized**: Kimlik doğrulama başarısız veya eksik
- **403 Forbidden**: Kimlik doğrulama başarılı ancak gereken yetkiye sahip değil
- **404 Not Found**: İstenen kaynak bulunamadı
- **409 Conflict**: İstek, mevcut kaynaklarla çakışma oluşturuyor
- **422 Unprocessable Entity**: Doğrulama hataları
- **429 Too Many Requests**: Rate limit aşıldı
- **500 Internal Server Error**: Sunucu hatası

## 4. Validasyon ve Hata Yönetimi

### 4.1. Validasyon Kuralları

- **Zorunlu Alanlar**: Zorunlu alanlar boş bırakılamaz
- **Format Doğrulama**: E-posta, telefon, TC kimlik numarası gibi alanlar için format doğrulama
- **Sayısal Değerler**: Minimum ve maksimum değer kontrolleri
- **Metin Uzunluğu**: Minimum ve maksimum uzunluk kontrolleri
- **Tarih Aralıkları**: Başlangıç tarihi bitiş tarihinden önce olmalı
- **Benzersizlik**: Benzersiz olması gereken değerler (kullanıcı adı, e-posta vb.)

### 4.2. Hata Mesajları

Hata durumunda API, aşağıdaki formatta hata mesajları döndürür:

```json
{
  "success":