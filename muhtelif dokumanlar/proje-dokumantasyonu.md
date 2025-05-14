# Mekik Residence Yönetim Sistemi - Geliştirici Dokümantasyonu

## 1. Sistem Genel Bakış

### 1.1 Amaç
Mekik Residence Yönetim Sistemi, konut kompleksleri, daireler ve kiracıların yönetimi için tasarlanmış kapsamlı bir mülk yönetim çözümüdür. Sistem, sakin takibi, bakım planlaması, bildirimler ve harici sistemlerle entegrasyon dahil olmak üzere konut yönetiminin çeşitli yönlerini ele alır.

### 1.2 Temel Fonksiyonlar
- **Sakin Yönetimi**: Sakinleri, bilgilerini ve mülklerle ilişkilerini takip etme
- **Bakım Yönetimi**: Mülkler için bakım faaliyetlerini planlama ve takip etme
- **Bildirim Sistemi**: SMS ve diğer kanallar aracılığıyla sakinlere bildirim gönderme
- **KBS Entegrasyonu**: Kimlik Bildirme Sistemi ile bağlantı kurma

### 1.3 Mimari
Sistem, katmanlı bir mimari yapı izler:
- **Core Katmanı**: Domain varlıkları, arayüzler ve DTO'ları içerir
- **Infrastructure Katmanı**: Repository'leri ve servisleri uygular
- **Application Katmanı**: İş mantığı ve kullanım durumlarını içerir
- **Web API**: İşlevselliği istemci uygulamalara sunar

## 2. Teknik Yapı

### 2.1 Framework ve Diller
- **.NET 8.0**: Temel framework
- **C#**: Temel programlama dili
- **Entity Framework Core**: Veritabanı erişimi için ORM

### 2.2 Veritabanı
- SQL Server veritabanı
- Entity Framework Core ile kod-öncelikli yaklaşım

### 2.3 Proje Yapısı
- **ResidenceManagement.Core**: Domain varlıkları, arayüzler, DTO'lar
- **ResidenceManagement.Infrastructure**: Veri repository'leri, servis uygulamaları
- **ResidenceManagement.Application**: İş mantığı, kullanım durumları
- **ResidenceManagement.Web**: Web API ve controller'lar

## 3. Domain Modeli

### 3.1 Temel Varlıklar

#### Sakin Yönetimi
- **Resident**: Mülkte yaşayan kişi
- **Apartment**: Konut birimi
- **Block**: Daireleri içeren bina
- **ApartmentResident**: Sakinler ve daireler arasındaki ilişkiyi takip eder

#### Bakım Yönetimi
- **MaintenanceSchedule**: Planlanan bakım faaliyeti
- **MaintenanceChecklistItem**: Bakım planı içindeki görev
- **MaintenanceLog**: Bakım faaliyetlerinin kaydı
- **MaintenanceDocument**: Bakımla ilişkili dosyalar

#### Bildirim Sistemi
- **NotificationLog**: Gönderilen bildirimlerin kayıtları
- **SmsTemplate**: SMS mesajları için şablonlar

#### KBS Entegrasyonu
- **KbsNotification**: KBS sistemine gönderilen kayıtlar
- **KbsNotificationLog**: KBS iletişiminin geçmişi

### 3.2 Çoklu Kiracılık (Multi-Tenancy)
Sistem, çoklu kiracılığı şunlar aracılığıyla destekler:
- **CompanyId**: Yönetim şirketini tanımlar
- **BranchId**: Şirket içindeki şubeleri tanımlar

## 4. Temel Bileşenler

### 4.1 Repository Pattern
Sistem, veri erişimini soyutlamak için repository pattern kullanır:
- **GenericRepository**: CRUD işlemleri için temel repository
- Varlığa özgü repository'ler (örn. ResidentRepository, MaintenanceScheduleRepository)

### 4.2 Servisler
Her domain alanının kendine özel servisleri vardır:
- **MaintenanceService**: Bakım planlaması ve takibini ele alır
- **MaintenanceScheduleService**: Bakım planlamasını yönetir
- **SmsService**: SMS mesajlaşmayı ele alır
- **NotificationLogService**: Bildirim geçmişini takip eder
- **KbsIntegrationService**: KBS entegrasyonunu yönetir

### 4.3 DTO'lar
Katmanlar arası iletişim için Veri Transfer Nesneleri (DTO) kullanılır:
- İstek/yanıt nesneleri
- Filtre nesneleri
- Görünüm modelleri

## 5. Özellik Detayları

### 5.1 Sakin Yönetimi
Sakin yönetimi işlevselliği şunları takip etmeye olanak tanır:
- Kişisel bilgiler (ad, kimlik, iletişim detayları)
- İkamet durumu (mülk sahibi, kiracı)
- İkamet geçmişi (taşınma tarihleri)
- Aile üyeleri
- Acil durum iletişim kişileri

Anahtar sınıflar:
- `ResidentRepository`: Sakin veri işlemlerini ele alır
- `ResidentEntity`: Temel sakin varlığı
- `ResidentDTO`: Sakin bilgileri için veri transfer nesnesi

### 5.2 Bakım Yönetimi
Bakım yönetimi özellikleri şunları içerir:
- Planlı bakım takibi
- Tek seferlik ve tekrarlayan bakımlar
- Bakım görevleri için kontrol listeleri
- Bakım personeline atama
- Tamamlama takibi ve raporlama
- Belge ekleri

Anahtar sınıflar:
- `MaintenanceScheduleService`: Bakım planlarını yönetir
- `MaintenanceService`: Temel bakım işlevselliği
- `MaintenanceSchedule`: Bakım için birincil varlık
- Enum'lar: `MaintenanceStatus`, `RepeatType`, `CompletionStatus`

Bakım sistemi şunları destekler:
- Farklı tekrar desenleri (günlük, haftalık, aylık, yıllık)
- Önceliklendirme (yüksek, normal, düşük)
- Maliyet takibi (tahmini ve gerçek)
- Durum iş akışı (planlandı, atandı, devam ediyor, tamamlandı, iptal edildi)

### 5.3 Bildirim Sistemi
Bildirim sistemi şunları ele alır:
- Sakinlere SMS mesajları
- Bildirim günlükleri ve takibi
- Şablonlu mesajlar
- Teslim durumu takibi

Anahtar sınıflar:
- `SmsService`: SMS gönderme işlemlerini ele alır
- `NotificationLogService`: Bildirim geçmişini kaydeder
- `SmsTemplate`: Mesaj şablonlarını saklar

### 5.4 KBS Entegrasyonu
KBS (Kimlik Bildirme Sistemi) entegrasyonu şunlara olanak tanır:
- Sakinleri ulusal kimlik sistemine bildirme
- Bildirim durumu takibi
- Hata işleme ve yeniden denemeleri yönetme
- Tüm iletişimleri kaydetme

Anahtar sınıflar:
- `KbsIntegrationService`: KBS ile iletişimi yönetir
- `KbsNotification`: KBS'ye yapılan bir bildirimi temsil eder
- `KbsNotificationLog`: İletişim geçmişini kaydeder

## 6. Yaygın Desenler ve Uygulamalar

### 6.1 Hata İşleme
Servisler, hata işleme için tutarlı bir desen kullanır:
- Ayrıntılı günlük kayıtları ile try-catch blokları
- Standartlaştırılmış yanıtlar için `ApiResponse<T>`
- İstemci geri bildirimi için durum kodları ve mesajlar

### 6.2 Sayfalama
Liste işlemleri genellikle sayfalamayı destekler:
- Sayfalanmış sonuçlar için `PagedResponse<T>`
- Standartlaştırılmış sayfalama için `PaginatedResult<T>`

### 6.3 Filtreleme
Repository'ler filtre desenini uygular:
- Sorgu parametreleri için Filtre DTO'ları
- LINQ ifade oluşturma
- Dinamik sıralama

### 6.4 Null Değer İşleme
Kod tabanı, null işleme için çeşitli yaklaşımlar kullanır:
- Null birleştirme operatörü (`??`)
- Null koşullu operatör (`?.`)
- Açık null kontrolleri

## 7. Geliştirme Yönergeleri

### 7.1 Yeni İşlevsellik Ekleme
Yeni özellikler eklerken:
1. Core katmanında varlıkları tanımlayın
2. Veri erişimi için repository'ler oluşturun
3. Infrastructure katmanında servisleri uygulayın
4. Application katmanı veya API uç noktaları aracılığıyla sunun

### 7.2 Yaygın Tuzaklar
Bu yaygın sorunların farkında olun:
- Benzer varlık türleri arasında tür dönüşümü (örn. farklı Apartment sınıfları)
- LINQ ifadelerinde null referans işleme
- Varsayılan değerler sağlamadan GetValueOrDefault'un aşırı kullanımı
- EF Core ile ifade ağacı sınırlamaları (örn. null kontrolleri)

### 7.3 Çoklu Kiracılık
Her zaman çoklu kiracılığı dikkate alın:
- Sorguları CompanyId ve BranchId'ye göre filtreleyin
- Tüm yeni varlıklarda CompanyId ve BranchId ayarlayın
- Tüm repository metotlarına kiracı ID'lerini dahil edin

## 8. Sonuç

Mekik Residence Yönetim Sistemi, sakin takibi, bakım planlaması ve harici sistem entegrasyonuna odaklanan kapsamlı bir mülk yönetim çözümüdür. Kod tabanı, standart .NET desenlerini takip eder ancak farklı geliştiricilerin katkılarıyla zaman içinde gelişmiştir, bu da stil ve yaklaşımda bazı tutarsızlıklara yol açmıştır.

Sistemle çalışırken, öncelikle domain modelini ve servis etkileşimlerini anlamaya odaklanın, çünkü bunlar tüm işlevsellik için temel oluşturur. Null değerlerin ve tür dönüşümlerinin işlenmesine dikkat edin, çünkü bunlar kod tabanındaki hataların yaygın kaynaklarıdır. 