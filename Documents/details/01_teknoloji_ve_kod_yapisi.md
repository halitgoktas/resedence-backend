# Residence Management Projesi - Teknoloji ve Kod Yapısı

## Kullanılan Teknolojiler

### Backend Teknolojileri
- **.NET 8.0**: Modern, güvenli ve performanslı bir framework
- **C#**: Nesne yönelimli programlama dili
- **Entity Framework Core**: ORM (Object-Relational Mapping) aracı
- **SQL Server**: Veritabanı yönetim sistemi
- **RESTful API**: Web servisleri için standart mimari
- **JWT (JSON Web Tokens)**: Güvenli kimlik doğrulama
- **Swagger/OpenAPI**: API dokümantasyonu
- **Serilog**: Gelişmiş loglama
- **AutoMapper**: Nesne eşleştirme
- **FluentValidation**: Veri doğrulama
- **MediatR**: CQRS ve Mediator pattern implementasyonu

### Mimari Yapı
- **Clean Architecture**: Katmanlı mimari yapı
- **SOLID Prensipleri**: Yazılım tasarım prensipleri
- **Repository Pattern**: Veri erişim katmanı
- **Unit of Work**: İşlem bütünlüğü
- **Dependency Injection**: Bağımlılık yönetimi
- **CQRS**: Komut ve Sorgu Sorumluluk Ayrımı
- **Mediator Pattern**: Bileşenler arası iletişim

### Güvenlik
- **JWT Authentication**: Güvenli kimlik doğrulama
- **Role-Based Authorization**: Rol tabanlı yetkilendirme
- **HTTPS**: Güvenli iletişim
- **Data Encryption**: Veri şifreleme
- **Input Validation**: Girdi doğrulama

### Performans ve Ölçeklenebilirlik
- **Caching**: Önbellekleme mekanizması
- **Async/Await**: Asenkron programlama
- **Background Services**: Arka plan işlemleri
- **Distributed Caching**: Dağıtık önbellekleme

### Monitoring ve Logging
- **Serilog**: Yapılandırılabilir loglama
- **Health Checks**: Sistem sağlık kontrolleri
- **Performance Monitoring**: Performans izleme
- **Error Tracking**: Hata takibi

### Test
- **xUnit**: Birim testleri
- **Moq**: Mock nesneleri
- **FluentAssertions**: Test assertions
- **Integration Tests**: Entegrasyon testleri

### DevOps
- **Docker**: Konteynerizasyon
- **CI/CD**: Sürekli entegrasyon ve dağıtım
- **Git**: Versiyon kontrolü
- **Azure DevOps**: Proje yönetimi ve CI/CD

## Kod Yapısı ve Standartlar

### Kod Organizasyonu
- **Katmanlı Mimari**: Core, Infrastructure, API, Services
- **Modüler Yapı**: Bağımsız ve yeniden kullanılabilir modüller
- **Separation of Concerns**: Sorumlulukların ayrılması
- **Domain-Driven Design**: Alan odaklı tasarım

### Kod Standartları
- **C# Coding Standards**: Microsoft'un önerdiği kod standartları
- **Clean Code**: Temiz kod prensipleri
- **SOLID Principles**: Yazılım tasarım prensipleri
- **DRY (Don't Repeat Yourself)**: Kod tekrarını önleme
- **KISS (Keep It Simple, Stupid)**: Basitlik prensibi

### Dokümantasyon
- **XML Documentation**: Kod dokümantasyonu
- **Swagger/OpenAPI**: API dokümantasyonu
- **README**: Proje dokümantasyonu
- **Architecture Decision Records**: Mimari kararların dokümantasyonu

### Versiyon Kontrolü
- **Git Flow**: Versiyon kontrol stratejisi
- **Semantic Versioning**: Anlamlı versiyon numaralandırma
- **Branching Strategy**: Dal stratejisi
- **Code Review**: Kod inceleme süreci 