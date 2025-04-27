"heatingSystem": "Merkezi Isıtma"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Blok başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "string"
      }
    }
    ```

- **PUT** `/api/v1/blocks/{id}`
  - **Açıklama**: Blok bilgilerini günceller
  - **İstek**: Oluşturma isteği ile aynı format
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Blok başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/blocks/{id}`
  - **Açıklama**: Bloğu siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Blok başarıyla silindi."
    }
    ```

#### 2.3.3. Daire

- **GET** `/api/v1/apartments`
  - **Açıklama**: Daireleri listeler
  - **Parametreler**:
    - `blockId`: Blok ID'sine göre filtreleme
    - `status`: Durum filtreleme (Vacant, OccupiedByOwner, OccupiedByTenant, ForSale, ForRent)
    - `floor`: Kat filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Daireler listelendi.",
      "data": [
        {
          "id": 1,
          "apartmentNumber": "A-101",
          "blockId": 1,
          "blockName": "A Blok",
          "floor": 1,
          "apartmentType": "2+1",
          "grossArea": 120.5,
          "status": "OccupiedByOwner",
          "ownerName": "Ahmet Yılmaz"
        }
      ],
      "totalCount": 60
    }
    ```

- **GET** `/api/v1/apartments/{id}`
  - **Açıklama**: Belirli bir dairenin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Daire bulundu.",
      "data": {
        "id": 1,
        "apartmentNumber": "A-101",
        "blockId": 1,
        "blockName": "A Blok",
        "siteId": 1,
        "siteName": "Örnek Rezidans",
        "floor": 1,
        "apartmentType": "2+1",
        "grossArea": 120.5,
        "netArea": 105.3,
        "numberOfRooms": 2,
        "numberOfLivingRooms": 1,
        "numberOfBathrooms": 1,
        "numberOfBalconies": 1,
        "status": "OccupiedByOwner",
        "ownershipType": "Individual",
        "ownerId": 5,
        "ownerName": "Ahmet Yılmaz",
        "tenantId": null,
        "tenantName": null,
        "description": "Güneye bakan, ferah daire",
        "duesAmount": 500.00,
        "lastDuesPaymentDate": "2023-07-15T00:00:00Z",
        "duesCoefficient": 1.0,
        "heatingType": "Doğalgaz Kombi",
        "notes": "Daire notları",
        "features": {
          "hasParking": true,
          "hasStorage": true,
          "hasFireplace": false,
          "hasCentralAC": false
        },
        "owners": [
          {
            "id": 3,
            "userId": 5,
            "name": "Ahmet Yılmaz",
            "ownershipPercentage": 100.0,
            "startDate": "2020-03-15T00:00:00Z",
            "endDate": null
          }
        ],
        "residents": [
          {
            "id": 5,
            "userId": 5,
            "name": "Ahmet Yılmaz",
            "residenceType": "Owner",
            "startDate": "2020-03-15T00:00:00Z",
            "endDate": null
          },
          {
            "id": 6,
            "userId": 8,
            "name": "Ayşe Yılmaz",
            "residenceType": "FamilyMember",
            "startDate": "2020-03-15T00:00:00Z",
            "endDate": null
          }
        ]
      }
    }
    ```

- **POST** `/api/v1/apartments`
  - **Açıklama**: Yeni daire oluşturur
  - **İstek**:
    ```json
    {
      "blockId": 1,
      "apartmentNumber": "A-101",
      "floor": 1,
      "apartmentType": "2+1",
      "grossArea": 120.5,
      "netArea": 105.3,
      "numberOfRooms": 2,
      "numberOfLivingRooms": 1,
      "numberOfBathrooms": 1,
      "numberOfBalconies": 1,
      "status": "Vacant",
      "ownershipType": "Individual",
      "description": "Güneye bakan, ferah daire",
      "duesAmount": 500.00,
      "duesCoefficient": 1.0,
      "heatingType": "Doğalgaz Kombi",
      "notes": "Daire notları",
      "features": {
        "hasParking": true,
        "hasStorage": true,
        "hasFireplace": false,
        "hasCentralAC": false
      }
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Daire başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "apartmentNumber": "A-101"
      }
    }
    ```

- **PUT** `/api/v1/apartments/{id}`
  - **Açıklama**: Daire bilgilerini günceller
  - **İstek**: Oluşturma isteği ile aynı format
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Daire başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/apartments/{id}`
  - **Açıklama**: Daireyi siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Daire başarıyla silindi."
    }
    ```

- **POST** `/api/v1/apartments/{id}/owner`
  - **Açıklama**: Daireye malik atar
  - **İstek**:
    ```json
    {
      "userId": 5,
      "ownershipPercentage": 100.0,
      "startDate": "2020-03-15T00:00:00Z",
      "deedInformation": "Tapu no: 12345"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Malik başarıyla atandı."
    }
    ```

- **POST** `/api/v1/apartments/{id}/tenant`
  - **Açıklama**: Daireye kiracı atar
  - **İstek**:
    ```json
    {
      "userId": 8,
      "startDate": "2023-01-01T00:00:00Z",
      "endDate": "2024-01-01T00:00:00Z",
      "monthlyRent": 5000.00,
      "rentCurrency": "TRY",
      "securityDeposit": 10000.00
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Kiracı başarıyla atandı."
    }
    ```

### 2.4. Aidat ve Ödeme Yönetimi

#### 2.4.1. Aidat Yönetimi

- **GET** `/api/v1/dues`
  - **Açıklama**: Aidatları listeler
  - **Parametreler**:
    - `year`: Yıla göre filtreleme
    - `month`: Aya göre filtreleme
    - `apartmentId`: Daire ID'sine göre filtreleme
    - `isPaid`: Ödeme durumuna göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aidatlar listelendi.",
      "data": [
        {
          "id": 1,
          "apartmentId": 1,
          "apartmentNumber": "A-101",
          "blockName": "A Blok",
          "year": 2023,
          "month": 7,
          "amount": 500.00,
          "currency": "TRY",
          "dueDate": "2023-07-15T00:00:00Z",
          "isPaid": true,
          "paymentDate": "2023-07-10T00:00:00Z"
        }
      ],
      "totalCount": 250
    }
    ```

- **GET** `/api/v1/dues/{id}`
  - **Açıklama**: Belirli bir aidatın detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aidat bulundu.",
      "data": {
        "id": 1,
        "apartmentId": 1,
        "apartmentNumber": "A-101",
        "blockId": 1,
        "blockName": "A Blok",
        "siteId": 1,
        "siteName": "Örnek Rezidans",
        "year": 2023,
        "month": 7,
        "amount": 500.00,
        "currency": "TRY",
        "dueDate": "2023-07-15T00:00:00Z",
        "isPaid": true,
        "paymentId": 15,
        "paymentDate": "2023-07-10T00:00:00Z",
        "isReconciled": true,
        "reconciliationDate": "2023-07-01T00:00:00Z",
        "reconciliationUserId": 3,
        "reconciliationUserName": "Admin User"
      }
    }
    ```

- **POST** `/api/v1/dues/batch`
  - **Açıklama**: Toplu aidat oluşturur
  - **İstek**:
    ```json
    {
      "year": 2023,
      "month": 8,
      "dueDate": "2023-08-15T00:00:00Z",
      "siteId": 1,
      "blockIds": [1, 2],
      "apartmentIds": [],
      "increasePercentage": 10.0,
      "notes": "Ağustos ayı aidatları"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "120 adet aidat başarıyla oluşturuldu."
    }
    ```

- **POST** `/api/v1/dues`
  - **Açıklama**: Tek bir daire için aidat oluşturur
  - **İstek**:
    ```json
    {
      "apartmentId": 1,
      "year": 2023,
      "month": 8,
      "amount": 550.00,
      "currency": "TRY",
      "dueDate": "2023-08-15T00:00:00Z",
      "notes": "Ağustos ayı aidatı"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aidat başarıyla oluşturuldu.",
      "data": {
        "id": 280,
        "apartmentId": 1,
        "year": 2023,
        "month": 8
      }
    }
    ```

- **PUT** `/api/v1/dues/{id}`
  - **Açıklama**: Aidat bilgilerini günceller
  - **İstek**:
    ```json
    {
      "amount": 550.00,
      "currency": "TRY",
      "dueDate": "2023-08-15T00:00:00Z",
      "notes": "Güncellenen Ağustos ayı aidatı"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aidat başarıyla güncellendi."
    }
    ```

- **POST** `/api/v1/dues/reconciliation`
  - **Açıklama**: Aidat mahsuplaştırma işlemi yapar
  - **İstek**:
    ```json
    {
      "year": 2023,
      "month": 7,
      "siteId": 1,
      "blockIds": [1, 2],
      "apartmentIds": [],
      "reconciliationType": "Monthly",
      "notes": "Temmuz ayı aidat mahsuplaştırması"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "120 adet aidat başarıyla mahsuplaştırıldı.",
      "data": {
        "id": 25,
        "affectedApartmentCount": 120,
        "totalAmount": 60000.00
      }
    }
    ```

#### 2.4.2. Ödeme Yönetimi

- **GET** `/api/v1/payments`
  - **Açıklama**: Ödemeleri listeler
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
    - `paymentType`: Ödeme tipi (Dues, Rent, Service, ActivityArea)
    - `status`: Durum (Pending, Completed, Failed, Refunded)
    - `userId`: Kullanıcı ID'sine göre filtreleme
    - `apartmentId`: Daire ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Ödemeler listelendi.",
      "data": [
        {
          "id": 15,
          "userId": 5,
          "userName": "Ahmet Yılmaz",
          "apartmentId": 1,
          "apartmentNumber": "A-101",
          "amount": 500.00,
          "currency": "TRY",
          "paymentType": "Dues",
          "paymentMethod": "CreditCard",
          "status": "Completed",
          "paymentDate": "2023-07-10T00:00:00Z"
        }
      ],
      "totalCount": 450
    }
    ```

- **GET** `/api/v1/payments/{id}`
  - **Açıklama**: Belirli bir ödemenin detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Ödeme bulundu.",
      "data": {
        "id": 15,
        "userId": 5,
        "userName": "Ahmet Yılmaz",
        "apartmentId": 1,
        "apartmentNumber": "A-101",
        "blockName": "A Blok",
        "siteName": "Örnek Rezidans",
        "amount": 500.00,
        "currency": "TRY",
        "exchangeRate": 1.0,
        "baseAmount": 500.00,
        "baseCurrency": "TRY",
        "paymentType": "Dues",
        "paymentMethod": "CreditCard",
        "status": "Completed",
        "relatedEntityId": 280,
        "relatedEntityType": "Dues",
        "receiptNumber": "RCP-2023-015",
        "receiptPath": "/uploads/receipts/rcp-2023-015.pdf",
        "processedByUserId": 3,
        "processedByUserName": "Admin User",
        "paymentDate": "2023-07-10T00:00:00Z",
        "notes": "Temmuz ayı aidat ödemesi"
      }
    }
    ```

- **POST** `/api/v1/payments`
  - **Açıklama**: Yeni ödeme kaydı oluşturur
  - **İstek**:
    ```json
    {
      "userId": 5,
      "apartmentId": 1,
      "amount": 500.00,
      "currency": "TRY",
      "paymentType": "Dues",
      "paymentMethod": "CreditCard",
      "relatedEntityId": 280,
      "relatedEntityType": "Dues",
      "paymentDate": "2023-07-10T00:00:00Z",
      "notes": "Temmuz ayı aidat ödemesi"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Ödeme başarıyla kaydedildi.",
      "data": {
        "id": 15,
        "receiptNumber": "RCP-2023-015"
      }
    }
    ```

- **PUT** `/api/v1/payments/{id}/status`
  - **Açıklama**: Ödeme durumunu günceller
  - **İstek**:
    ```json
    {
      "status": "Completed",
      "notes": "Ödeme başarıyla tamamlandı."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Ödeme durumu başarıyla güncellendi."
    }
    ```

- **POST** `/api/v1/payments/{id}/refund`
  - **Açıklama**: Ödeme iadesi oluşturur
  - **İstek**:
    ```json
    {
      "refundReason": "Müşteri talebi üzerine",
      "refundAmount": 500.00,
      "notes": "Temmuz ayı aidat ödemesi iadesi"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Ödeme iadesi başarıyla oluşturuldu.",
      "data": {
        "id": 16,
        "status": "Refunded"
      }
    }
    ```

### 2.5. Rezervasyon ve Aktivite Alanları

#### 2.5.1. Aktivite Alanları

- **GET** `/api/v1/activity-areas`
  - **Açıklama**: Aktivite alanlarını listeler
  - **Parametreler**:
    - `siteId`: Site ID'sine göre filtreleme
    - `isPaid`: Ücretli/ücretsiz filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aktivite alanları listelendi.",
      "data": [
        {
          "id": 1,
          "name": "Yüzme Havuzu",
          "siteId": 1,
          "siteName": "Örnek Rezidans",
          "isPaid": true,
          "pricePerHour": 50.00,
          "currency": "TRY",
          "capacity": 30,
          "availableTimeStart": "09:00:00",
          "availableTimeEnd": "22:00:00"
        }
      ],
      "totalCount": 8
    }
    ```

- **GET** `/api/v1/activity-areas/{id}`
  - **Açıklama**: Belirli bir aktivite alanının detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aktivite alanı bulundu.",
      "data": {
        "id": 1,
        "name": "Yüzme Havuzu",
        "description": "Açık yüzme havuzu",
        "siteId": 1,
        "siteName": "Örnek Rezidans",
        "isPaid": true,
        "pricePerHour": 50.00,
        "pricePerDay": 300.00,
        "pricePerPerson": null,
        "currency": "TRY",
        "capacity": 30,
        "availableTimeStart": "09:00:00",
        "availableTimeEnd": "22:00:00",
        "minimumReservationHours": 1,
        "features": {
          "isHeated": true,
          "hasShowers": true,
          "hasTowelService": false,
          "isIndoor": false
        },
        "requiresApproval": false
      }
    }
    ```

- **POST** `/api/v1/activity-areas`
  - **Açıklama**: Yeni aktivite alanı oluşturur
  - **İstek**:
    ```json
    {
      "siteId": 1,
      "name": "Yüzme Havuzu",
      "description": "Açık yüzme havuzu",
      "isPaid": true,
      "pricePerHour": 50.00,
      "pricePerDay": 300.00,
      "pricePerPerson": null,
      "currency": "TRY",
      "capacity": 30,
      "availableTimeStart": "09:00:00",
      "availableTimeEnd": "22:00:00",
      "minimumReservationHours": 1,
      "features": {
        "isHeated": true,
        "hasShowers": true,
        "hasTowelService": false,
        "isIndoor": false
      },
      "requiresApproval": false
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aktivite alanı başarıyla oluşturuldu.",
      "data": {
        "id": 1,
        "name": "Yüzme Havuzu"
      }
    }
    ```

- **PUT** `/api/v1/activity-areas/{id}`
  - **Açıklama**: Aktivite alanı bilgilerini günceller
  - **İstek**: Oluşturma isteği ile aynı format
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aktivite alanı başarıyla güncellendi."
    }
    ```

- **DELETE** `/api/v1/activity-areas/{id}`
  - **Açıklama**: Aktivite alanını siler (soft delete)
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Aktivite alanı başarıyla silindi."
    }
    ```

- **GET** `/api/v1/activity-areas/{id}/availability`
  - **Açıklama**: Aktivite alanının belirli tarih aralığındaki müsaitlik durumunu getirir
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Müsaitlik durumu listelendi.",
      "data": [
        {
          "date": "2023-07-15",
          "timeSlots": [
            {
              "startTime": "09:00:00",
              "endTime": "10:00:00",
              "isAvailable": true
            },
            {
              "startTime": "10:00:00",
              "endTime": "11:00:00",
              "isAvailable": false,
              "reservationId": 45
            }
          ]
        }
      ]
    }
    ```

#### 2.5.2. Rezervasyonlar

- **GET** `/api/v1/reservations`
  - **Açıklama**: Rezervasyonları listeler
  - **Parametreler**:
    - `startDate`: Başlangıç tarihi
    - `endDate`: Bitiş tarihi
    - `reservationType`: Rezervasyon tipi (Apartment, ActivityArea)
    - `status`: Durum (Pending, Confirmed, CheckedIn, CheckedOut, Cancelled)
    - `userId`: Kullanıcı ID'sine göre filtreleme
    - `relatedId`: İlgili kayıt ID'sine göre filtreleme
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rezervasyonlar listelendi.",
      "data": [
        {
          "id": 45,
          "reservationType": "ActivityArea",
          "relatedId": 1,
          "relatedName": "Yüzme Havuzu",
          "userId": 5,
          "userName": "Ahmet Yılmaz",
          "startDateTime": "2023-07-15T10:00:00Z",
          "endDateTime": "2023-07-15T11:00:00Z",
          "status": "Confirmed"
        }
      ],
      "totalCount": 120
    }
    ```

- **GET** `/api/v1/reservations/{id}`
  - **Açıklama**: Belirli bir rezervasyonun detaylarını getirir
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rezervasyon bulundu.",
      "data": {
        "id": 45,
        "reservationType": "ActivityArea",
        "relatedId": 1,
        "relatedName": "Yüzme Havuzu",
        "userId": 5,
        "userName": "Ahmet Yılmaz",
        "startDateTime": "2023-07-15T10:00:00Z",
        "endDateTime": "2023-07-15T11:00:00Z",
        "numberOfPeople": 2,
        "guestNames": ["Ayşe Yılmaz"],
        "totalPrice": 50.00,
        "currency": "TRY",
        "status": "Confirmed",
        "paymentId": 48,
        "notes": "Özel not",
        "isKbsRegistered": false,
        "checkInDate": null,
        "checkOutDate": null,
        "processedByUserId": 3,
        "processedByUserName": "Admin User"
      }
    }
    ```

- **POST** `/api/v1/reservations`
  - **Açıklama**: Yeni rezervasyon oluşturur
  - **İstek**:
    ```json
    {
      "reservationType": "ActivityArea",
      "relatedId": 1,
      "userId": 5,
      "startDateTime": "2023-07-15T10:00:00Z",
      "endDateTime": "2023-07-15T11:00:00Z",
      "numberOfPeople": 2,
      "guestNames": ["Ayşe Yılmaz"],
      "notes": "Özel not"
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rezervasyon başarıyla oluşturuldu.",
      "data": {
        "id": 45,
        "totalPrice": 50.00,
        "currency": "TRY"
      }
    }
    ```

- **PUT** `/api/v1/reservations/{id}/status`
  - **Açıklama**: Rezervasyon durumunu günceller
  - **İstek**:
    ```json
    {
      "status": "Confirmed",
      "notes": "Rezervasyon onaylandı."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Rezervasyon durumu başarıyla güncellendi."
    }
    ```

- **POST** `/api/v1/reservations/{id}/check-in`
  - **Açıklama**: Rezervasyon giriş işlemi yapar
  - **İstek**:
    ```json
    {
      "checkInDate": "2023-07-15T10:00:00Z",
      "notes": "Giriş işlemi tamamlandı.",
      "registerToKbs": true
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Giriş işlemi başarıyla tamamlandı."
    }
    ```

- **POST** `/api/v1/reservations/{id}/check-out`
  - **Açıklama**: Rezervasyon çıkış işlemi yapar
  - **İstek**:
    ```json
    {
      "checkOutDate": "2023-07-15T11:00:00Z",
      "notes": "Çıkış işlemi tamamlandı."
    }
    ```
  - **Yanıt**:
    ```json
    {
      "success": true,
      "message": "Çıkış işlemi başarıyla tamamlandı."
    }
    ```

- **POST** `/api/v1/reservations/{id}/cancel`
  - **Açıklama**: Rezervasyonu iptal eder
  - **İstek**:
    ```json
    {
      "cancelReason": "Müşteri talebi üzerine",
      "refundPayment": true,
      "notes": "Rezervasyon iptali"
    }
    ```
  - **Yanı