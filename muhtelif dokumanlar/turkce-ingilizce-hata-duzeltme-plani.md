# Türkçe-İngilizce İsimlendirme Hatalarını Düzeltme Planı

Bu dosya, projede tespit edilen 622 derleme hatasının kategorilerine göre sınıflandırılmasını ve çözüm yöntemlerini içerir. Hataların çoğu, projenin Türkçe-İngilizce isim karışıklığından kaynaklanmaktadır.

## İçindekiler
1. [Hata Kategorileri](#hata-kategorileri)
2. [Düzeltme Sıralaması](#düzeltme-sıralaması)
3. [Hatalar ve Çözümleri](#hatalar-ve-çözümleri)
4. [İlerleme Durumu](#ilerleme-durumu)

## Hata Kategorileri

Projede tespit edilen hatalar şu kategorilere ayrılabilir:

1. **Türkçe-İngilizce Property İsimleri (~250-300 hata)**
   - FirmaId/SubeId yerine CompanyId/BranchId kullanılması gereken yerler
   - Ad/Soyad/TamAd gibi kişi bilgilerinin FirstName/LastName/FullName olarak değiştirilmesi gerekli

2. **Eksik/Yanlış API Response Parametreleri (~100-120 hata)**
   - ApiResponse constructor'ında eksik parametreler (message, data vb.)
   - ErrorCodes sınıfındaki hata kodlarının bulunamama sorunları

3. **Interface Uygulamalarındaki Uyumsuzluklar (~80-100 hata)**
   - GetPaginatedResidentsAsync gibi metotların dönüş tiplerinin farklı olması
   - IRepository<T> implementasyonlarında eksik/yanlış metotlar

4. **Tip Dönüşüm Hataları (~60-80 hata)**
   - string -> bool, bool -> string gibi uyumsuz dönüşümler
   - int -> char gibi uyumsuz dönüşümler
   - Nullable tip kontrolü eksiklikleri

5. **ResidentFilterRequest Property Erişim Hataları (~20-25 hata)**
   - SearchText yerine SearchTerm kullanımı
   - SortDirection eksikliği

6. **Salt Okunur Özelliklere Atama Hataları (~5-10 hata)**
   - TotalPages gibi read-only özelliklere atama yapılması

## Düzeltme Sıralaması

Hataların düzeltilmesi için aşağıdaki sıralama izlenecektir:

1. **En Yüksek Öncelik - ResidentFilterRequest Düzeltmeleri**
   - SearchText -> SearchTerm değişimi
   - SortDirection eklemesi
   - Uyumsuz tür dönüşümlerinin düzeltilmesi

2. **Yüksek Öncelik - Temel Entity ve Tip Yapıları**
   - BaseEntity ve temel DTOs
   - Özellikle multi-tenant yapıyla ilgili FirmaId/SubeId alanları

3. **Orta Öncelik - Interface ve Repository Implementasyonları**
   - Servis interface'leri ve implementasyonları
   - Repository yapıları

4. **Orta Öncelik - API Response ve Error Code Yapıları**
   - ErrorCodes sınıfı güncelleme
   - ApiResponse yapıcı metot kullanımlarını düzeltme

## Hatalar ve Çözümleri

### 1. ResidentFilterRequest Property Sorunları

**Sorun:** Yaklaşık 20-25 hata, ResidentFilterRequest sınıfında SearchText yerine SearchTerm ve SortDirection sorunlarından kaynaklanıyor.

**Çözüm Adımları:**
- [x] ResidentFilterRequest.cs sınıfının incelenmesi
- [x] SearchText -> SearchTerm dönüşümünün yapılması (SearchTerm zaten tanımlıydı)
- [x] SortDirection property'sinin doğru şekilde eklenmesi
- [x] ResidentRepository.cs içindeki kullanımların güncellenmesi
- [x] ApplySorting metodunun imzasının güncellenmesi
- [ ] int -> char dönüşüm hatalarının giderilmesi

### 2. FirmaId/SubeId -> CompanyId/BranchId Dönüşümü

**Sorun:** Yaklaşık 150-180 hata, temel varlıklarda FirmaId/SubeId gibi Türkçe property isimlerinin kullanımından kaynaklanıyor.

**Çözüm Adımları:**
- [ ] Core/Entities altındaki BaseEntity sınıfında FirmaId/SubeId -> CompanyId/BranchId dönüşümü
- [ ] Core/DTOs sınıflarındaki aynı dönüşümlerin uygulanması
- [ ] Repository sınıflarındaki filtreleme metotlarının güncellenmesi
- [ ] Service sınıflarındaki metot parametrelerinin güncellenmesi
- [ ] Data/Configurations altındaki configuration dosyalarının güncellenmesi

### 3. Ad/Soyad -> FirstName/LastName Dönüşümü

**Sorun:** Yaklaşık 60-80 hata, kişi bilgilerinin Türkçe olarak tanımlanmasından kaynaklanıyor.

**Çözüm Adımları:**
- [ ] User entity sınıfında Ad/Soyad/TamAd -> FirstName/LastName/FullName dönüşümü
- [ ] Resident entity sınıfında benzer dönüşümlerin yapılması
- [ ] KbsIntegrationService.cs içindeki ilgili alanların güncellenmesi
- [ ] UserService.cs içindeki Türkçe property erişimlerinin düzeltilmesi
- [ ] UserDTO ve ResidentDTO sınıflarının güncellenmesi

### 4. API Response Constructor Parametreleri

**Sorun:** Yaklaşık 80-100 hata, ApiResponse sınıfı constructor'larında eksik parametrelerden kaynaklanıyor.

**Çözüm Adımları:**
- [ ] ApiResponse<T> sınıfı yapıcı metotlarının incelenmesi ve doğru parametre yapısının belirlenmesi
- [ ] EmailService.cs içindeki ApiResponse kullanımlarının düzeltilmesi
- [ ] SmsService.cs içindeki ApiResponse kullanımlarının düzeltilmesi
- [ ] BranchService.cs içindeki ApiResponse kullanımlarının düzeltilmesi
- [ ] NotificationSettingsService.cs içindeki ApiResponse kullanımlarının düzeltilmesi

### 5. ErrorCodes Sınıfında Eksik Tanımlar

**Sorun:** Yaklaşık 20-30 hata, ErrorCodes sınıfında Unauthorized, InternalServerError gibi tanımların eksik olmasından kaynaklanıyor.

**Çözüm Adımları:**
- [ ] ErrorCodes.cs sınıfının incelenmesi ve eksik hata kodlarının eklenmesi
- [ ] Unauthorized, InternalServerError, NotFound, BadRequest kodlarının eklenmesi
- [ ] ServiceNotEnabled, ServiceConfigurationError gibi özel hata kodlarının eklenmesi

### 6. Interface Uyumluluk Sorunları

**Sorun:** Yaklaşık 80-100 hata, interface'lerin doğru uygulanmamasından kaynaklanıyor.

**Çözüm Adımları:**
- [ ] IResidentRepository.cs ve ResidentRepository.cs arasındaki metot imzası uyumsuzluklarının giderilmesi
- [ ] INotificationSettingsRepository.cs ve NotificationSettingsRepository.cs arasındaki Update/GetByFirmaAndSubeIdAsync sorunlarının çözülmesi
- [ ] Repository sınıflarında eksik olan Update/Delete/DeleteRange metotlarının implementasyonu

### 7. Tip Dönüşüm Sorunları

**Sorun:** Yaklaşık 60-80 hata, uyumsuz tip dönüşümlerinden kaynaklanıyor.

**Çözüm Adımları:**
- [ ] string -> bool, bool -> string dönüşümlerinin düzeltilmesi
- [ ] int -> char dönüşümlerinin düzeltilmesi
- [ ] Nullable tiplerde doğru kontrol mekanizmalarının uygulanması
- [ ] MaintenanceScheduleService.cs içindeki tip dönüşüm hatalarının düzeltilmesi

### 8. Salt Okunur Özelliklere Atama Sorunları

**Sorun:** Yaklaşık 5-10 hata, TotalPages gibi salt okunur özelliklere atama yapılmasından kaynaklanıyor.

**Çözüm Adımları:**
- [ ] ResidentRepository.cs ve NotificationLogService.cs içindeki PagedResponse<T>.TotalPages ataması sorunlarının düzeltilmesi
- [ ] Sayfalama mantığının gözden geçirilmesi ve doğru yaklaşımın belirlenmesi

## İlerleme Durumu

### 1. ResidentFilterRequest Property Sorunları
- [x] ResidentFilterRequest.cs incelemesi tamamlandı
- [x] SearchText -> SearchTerm değişikliği (SearchTerm zaten mevcuttu)
- [x] SortDirection property'si SortDesc yerine string olarak eklendi
- [x] ResidentRepository.cs içindeki filter.SearchText kullanımları filter.SearchTerm olarak güncellendi
- [x] ApplySorting metodu bool SortDesc yerine string SortDirection alacak şekilde güncellendi
- [ ] int -> char dönüşüm hatalarının giderilmesi

### 2. FirmaId/SubeId -> CompanyId/BranchId Dönüşümü
- [ ] BaseEntity sınıfı güncellemesi
- [ ] DTO sınıfları güncellemesi
- [ ] Repository sınıfları güncellemesi
- [ ] Service sınıfları güncellemesi
- [ ] Configuration dosyaları güncellemesi

### 3. Ad/Soyad -> FirstName/LastName Dönüşümü
- [ ] User entity sınıfı güncellemesi
- [ ] Resident entity sınıfı güncellemesi
- [ ] KbsIntegrationService.cs güncellemesi
- [ ] UserService.cs güncellemesi
- [ ] DTO sınıfları güncellemesi

### 4. API Response Constructor Parametreleri
- [ ] ApiResponse<T> sınıfı incelemesi
- [ ] EmailService.cs güncellemesi
- [ ] SmsService.cs güncellemesi
- [ ] BranchService.cs güncellemesi
- [ ] NotificationSettingsService.cs güncellemesi

### 5. ErrorCodes Sınıfında Eksik Tanımlar
- [ ] ErrorCodes.cs incelemesi
- [ ] Eksik hata kodlarının eklenmesi

### 6. Interface Uyumluluk Sorunları
- [ ] IResidentRepository.cs ve ResidentRepository.cs güncellemesi
- [ ] INotificationSettingsRepository.cs ve ilgili implementasyon güncellemesi
- [ ] Eksik repository metotlarının implementasyonu

### 7. Tip Dönüşüm Sorunları
- [ ] string <-> bool dönüşüm düzeltmeleri
- [ ] int -> char dönüşüm düzeltmeleri
- [ ] Nullable tip kontrolü düzeltmeleri
- [ ] MaintenanceScheduleService.cs güncellemesi

### 8. Salt Okunur Özelliklere Atama Sorunları
- [ ] ResidentRepository.cs düzeltmesi
- [ ] NotificationLogService.cs düzeltmesi 