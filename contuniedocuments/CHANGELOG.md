# CHANGELOG - Rezidans ve Site Yönetim Sistemi

Bu dosya, projedeki tüm önemli değişiklikleri ve ilerlemeyi kaydetmek için kullanılmaktadır.

## [Unreleased]

### Eklenen
- Multi-tenant Middleware implementasyonu tamamlandı (`MultiTenantMiddleware.cs`)
- Multi-tenant Extension sınıfı eklendi (`MultiTenantExtensions.cs`)
- API isteklerinden ve JWT tokenlardan tenant bilgilerinin alınması sağlandı
- Tenant bilgilerinin DbContext'e otomatik olarak aktarılması yapıldı
- Hizmet kategorileri sistemi eklendi (`ServiceCategory.cs`)
- Hizmet türleri ve fiyatlandırma yapısı eklendi (`ServiceType.cs`)
- Aidat ayarları ve yönetimi geliştirildi (`DuesSettings.cs`)
- Gider kategorileri sistemi eklendi (`ExpenseCategory.cs`, `Expense.cs`)
- Personel yönetim modülü eklendi (`Employee.cs`, `EmployeeDepartment.cs`, `EmployeeSalaryPayment.cs`)
- Sözleşme yönetim sistemi eklendi (`Contract.cs`, `ContractPaymentSchedule.cs`)
- Finansal özet ve dashboard altyapısı eklendi (`FinancialSummary.cs`)

### Geliştirilen
- BaseEntity yapısı üzerinde düzenlemeler ve iyileştirmeler yapıldı
- ITenant arayüzü ve multi-tenant filtre yönetimi geliştirildi
- Hizmet talebi (`ServiceRequest.cs`) ve hizmet görevi (`ServiceTask.cs`) yapıları genişletildi
- Çoklu para birimi desteği tüm finansal yapılarda uygulandı

## [0.2.0] - 2023-08-15

### Eklenen
- Entity sınıfları oluşturuldu: Apartment, Block, Residence
- ServiceRequest ve ServiceReport sınıfları implementasyonu tamamlandı
- EquipmentInventory sınıfı eklendi
- KbsIntegrationController tamamlandı

### Geliştirilen
- Multi-dil ve multi-para birimi desteği iyileştirildi
- Daire doluluk ve boşluk takip sistemi tasarlandı
- Kiracı ve daire sahibi gelir dağılımı modeli eklendi
- KBS (Kimlik Bildirme Sistemi) entegrasyonu implementasyonu tamamlandı

## [0.1.0] - 2023-06-30

### Eklenen
- Proje temel yapısı oluşturuldu
- Core katmanı tamamlandı
- Infrastructure katmanı eklendi
- API katmanı için temel yapı oluşturuldu
- Multi-tenant mimari altyapısı hazırlandı
- Temel Entity yapıları tanımlandı

### Geliştirilen
- Veritabanı bağlantısı ve Entity Framework Core konfigürasyonu
- Repository pattern implementasyonu
- Unit of Work pattern uygulaması

### Düzeltilen
- N/A

### Değişen
- N/A

## Not
Bu CHANGELOG dosyası, [Keep a Changelog](https://keepachangelog.com/tr/1.0.0/) standardını takip etmektedir. 