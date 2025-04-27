# Rezidans ve Site Yönetim Sistemi - Proje Özeti

## 1. Proje Amacı

Rezidans ve Site Yönetim Sistemi, çok kiracılı (multi-tenant) bir yapıda, rezidans ve site yönetimlerinin tüm süreçlerini yönetebilecekleri kapsamlı bir yazılım çözümüdür. Sistem daireleri, site sakinlerini, finansal işlemleri, etkinlikleri, rezervasyonları, güvenlik erişimlerini ve bildirim yönetimini içerir.

## 2. Teknoloji Seçimi

- **Backend**: .NET 8 Web API (C#) ve MSSQL
- **Frontend**: React.js ve Material UI
- **Mobil**: React Native

## 3. Önceki Projede Karşılaşılan Sorunlar

### 3.1. Mimari Tutarsızlıklar
- Backend modelleri arasında tutarsızlıklar ve gereksiz tekrarlar
- Aynı amaca hizmet eden birden fazla sınıf (örn: BaseEntity)
- Karmaşık namespace yapısı ve eksik import tanımlamaları

### 3.2. Veritabanı Tasarım Sorunları
- Multi-tenant yapısının uygulamada yaşattığı performans sorunları
- Entity ilişkilerinin optimizasyon eksikliği

### 3.3. Kod Kalitesi Sorunları
- Tekrarlanan kod parçaları
- Yetersiz soyutlama
- Yorum ve dokümantasyon eksikliği

### 3.4. Test Eksikliği
- Yetersiz birim ve entegrasyon testleri
- Test odaklı geliştirme (TDD) yaklaşımının eksikliği

## 4. Model Yapısı

### 4.1. Temel Veri Modelleri
- **BaseEntity**: Tüm modellerin temel sınıfı
  - Id, FirmaId, SubeId, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, IsDeleted, IsActive

- **BaseLookupEntity**: Referans veri modelleri için temel sınıf
  - BaseEntity + Code, Name, Description

- **BaseTransactionEntity**: İşlem/hareket tabloları için temel sınıf
  - BaseEntity + TransactionDate, ReferenceNumber, Notes, Status, ApprovedBy, ApprovalDate

### 4.2. Ana Veri Grupları
- **Site/Bina/Daire**: Site, Building, Apartment
- **Kullanıcı/Sakin**: User, Resident, Staff
- **Finansal İşlemler**: FinancialTransaction, ApartmentRentalPayment
- **Etkinlikler**: Event, EventParticipant, EventExpense
- **Güvenlik**: AccessCard, AccessLog, AccessPermission
- **İzleme/Performans**: PerformanceMetric, EndpointUsageMetric
- **Bildirimler**: Notification, NotificationSetting, NotificationTemplate

## 5. Geliştirme Sıralaması

1. Proje yapısının oluşturulması ve mimari tasarımın belirlenmesi
2. Gereksinimlerin analiz edilmesi ve test senaryolarının planlanması
3. Veritabanı modellerinin test senaryolarının yazılması
4. Veritabanı modellerinin tasarlanması ve model validasyonlarının eklenmesi
5. Migration'ların oluşturulması ve çalıştırılması
6. Repository katmanının oluşturulması ve birim testleri
7. Service katmanının oluşturulması, iş kuralı validasyonlarının eklenmesi ve birim testleri
8. API kontrollerlerinin yazılması, API validasyonlarının eklenmesi ve entegrasyon testleri
9. Hata yakalama ve loglama mekanizmalarının eklenmesi
10. Güvenlik ve yetkilendirme sisteminin kurulması
11. Performans optimizasyonlarının yapılması
12. Sistem ve kabul testlerinin gerçekleştirilmesi
13. Dokümantasyon ve kullanım kılavuzlarının hazırlanması

## 6. Proje Geliştirme Prensipleri

### 6.1. Kod Organizasyonu
- Domain-Driven Design (DDD) prensiplerinin uygulanması
- SOLID prensiplerine uygun kod yazımı
- Modüler ve tekrar kullanılabilir komponent yapısı

### 6.2. Test Stratejisi
- Test-Driven Development (TDD) yaklaşımı
- Kapsamlı birim testleri
- Entegrasyon ve sistem testleri
- Performans testleri

### 6.3. Veritabanı Yaklaşımı
- Code First yaklaşımı ile Entity Framework Core kullanımı
- Multi-tenant yapısının optimize edilmesi
- İlişkilerin performans odaklı tasarlanması

### 6.4. Güvenlik ve Yetkilendirme
- JWT Authentication
- Rol tabanlı yetki sistemi
- Her kullanıcının kendi firma ve şubesi için işlem yapabilmesi

### 6.5. Validasyon
- Model validasyonları (veri yapısı doğrulama)
- İş kuralı validasyonları (iş mantığı doğrulama)
- API validasyonları (istek verisi doğrulama)

## 7. Multi-tenant Yapısı

- Her tabloda FirmaID ve SubeID alanları bulunacak
- Kullanıcı hangi firma ve şubeye tanımlandıysa, o firma ve şube için işlem yapabilecek
- Veri izolasyonu ve güvenliği sağlanacak
- Performans optimizasyonları yapılacak

## 8. Yetki Sistemi

- Kullanıcı rolleri (Admin, Firma Yöneticisi, Teknik, Servis, vb.)
- Her rol için erişilebilir işlemler tanımlanacak
- Tüm formlarda yetki kontrolü (ekle, yenile, sil, gör)

## 9. Frontend ve UI Yaklaşımı

- React.js ve Material UI kullanımı
- Komponent tabanlı yapı
- Duyarlı tasarım (responsive design)
- Kullanıcı dostu arayüz
- Türkçe arayüz metinleri

## 10. Önemli Hatırlatmalar

- Her kod dosyası ve kod bloğu için Türkçe yorum satırlarıyla açıklama yapılması
- Her komponent dosyası 400 satırı geçmeyecek şekilde tasarlanması
- Tüm arayüz metinlerinin Türkçe olması
- API önceliği ve tüm ekranlarda API bağlantısı yapılması 