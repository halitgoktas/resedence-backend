# Mekik Residance Manager Mimari Kuralları ve İyi Uygulama Kılavuzu

## 1. Genel Proje Yapısı ve Mimari Kuralları

### 1.1 Katmanlı Mimari Yapısı
Projemiz aşağıdaki katmanlardan oluşmaktadır:

- **Core**: Domain modelleri, arayüzler ve temel sınıflar
- **Infrastructure**: Veri erişim, harici servisler ve Cross-cutting concerns
- **Application**: İş mantığı ve kullanım senaryoları
- **API**: Sunum katmanı ve endpoint tanımları

### 1.2 Bağımlılık Kuralları
- Her katman sadece kendi seviyesinde veya daha alt seviyedeki katmanlara bağımlı olabilir
- **Core** → Hiçbir projeye bağımlı değil
- **Infrastructure** → Core'a bağımlı
- **Application** → Core'a bağımlı, Infrastructure'a bağımlı olmamalı
- **API** → Core, Application ve Infrastructure'a bağımlı

## 2. Kod Geliştirmede Dikkat Edilmesi Gereken Hususlar

### 2.1 Interfacelerin Doğru Konumlandırılması
Son bir haftada yaşanan sorunların temel sebeplerinden biri, aynı isimli interfacelerin farklı namespace'lerde bulunmasıydı.

✅ **Doğru Kullanım:**
```csharp
// IUnitOfWork interface'ini kullanırken tam adıyla belirtme
using ResidenceManagement.Core.Interfaces;
// veya
using IUnitOfWorkCore = ResidenceManagement.Core.Interfaces.IUnitOfWork;
```

❌ **Yanlış Kullanım:**
```csharp
// Belirsiz referans oluşturabilecek import
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Interfaces.Repositories;
```

### 2.2 Repository ve UnitOfWork İmplementasyonu
Projede iki farklı IUnitOfWork arayüzü bulunmaktadır. Bu durum kafa karışıklığına neden olabilir.

#### Core.Interfaces.IUnitOfWork vs Core.Interfaces.Repositories.IUnitOfWork

✅ **Doğru Kullanım:**
```csharp
// Bakım modülü repository'lerini kullanmak için
using ResidenceManagement.Core.Interfaces;

// Daha genel repository pattern kullanımı için
using IUnitOfWorkRepo = ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork;
```

### 2.3 Repository Interface'lerinin İlişkilendirilmesi
Yeni bir modül eklerken ilgili repository'lerin doğru şekilde tanımlanması ve UnitOfWork'e eklenmesi gerekir.

✅ **Checklist - Yeni Modül Eklerken:**
1. `Core.Interfaces` altında ilgili repository interface'lerini tanımla
2. `Core.Interfaces.IUnitOfWork` ve `Core.Interfaces.Repositories.IUnitOfWork` içine repository property'lerini ekle
3. Infrastructure katmanında repository implementasyonlarını oluştur
4. Her iki UnitOfWork implementasyonuna da ilgili repository'leri ekle
5. DependencyInjection sınıfında servisleri kaydet

## 3. UnitOfWork Pattern Kullanım Kılavuzu

### 3.1 Servis Katmanında UnitOfWork Kullanımı
Servis sınıflarında UnitOfWork kullanırken doğru interface'i referans alın.

✅ **Doğru Kullanım:**
```csharp
public class MaintenanceService : IMaintenanceService
{
    private readonly ResidenceManagement.Core.Interfaces.IUnitOfWork _unitOfWork;

    public MaintenanceService(ResidenceManagement.Core.Interfaces.IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
```

### 3.2 Interface Implementasyonu Kuralları
- Interface'lerde tanımlanan her property ve metot, implementasyonda mutlaka bulunmalıdır
- UnitOfWork implementasyonunda tüm repository property'leri lazy olarak başlatılmalıdır
- Repository implementasyonları DbContext ile çalışmalıdır

## 4. Genel Kodlama Prensipleri

### 4.1 Dependency Injection
- Constructor injection kullanın
- Servis yaşam döngülerini (scope, transient, singleton) doğru şekilde belirleyin
- Interface üzerinden bağımlılık enjekte edin, somut sınıflar üzerinden değil

### 4.2 Namespace Organizasyonu
- Namespace'leri mantıklı şekilde organize edin ve tutarlı olun
- Aynı isimli sınıf/interface'leri farklı namespace'lerde tanımlamaktan kaçının
- Gerektiğinde using alias kullanarak namespace çakışmalarını engelleyin

```csharp
using IUnitOfWorkCore = ResidenceManagement.Core.Interfaces.IUnitOfWork;
using IUnitOfWorkRepo = ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork;
```

### 4.3 Kod Tekrarını Önleme
- Generic repository pattern ile tekrarlanan kod azaltılmalıdır
- Base sınıflar ve interface'ler kullanarak kod tekrarını önleyin
- Extension metodları kullanarak yaygın işlemleri standartlaştırın

## 5. Yeni Modül Ekleme Adımları

Yeni bir modül eklerken aşağıdaki adımları takip edin:

1. **Entity Tanımlama**:
   - Core katmanında entity sınıflarını oluşturun
   - BaseEntity'den türetin
   - İlişkileri doğru şekilde tanımlayın

2. **Interface Tanımlama**:
   - Core.Interfaces altında servis interface'lerini tanımlayın
   - Core.Interfaces.Repositories altında özel repository interface'lerini tanımlayın
   - Her iki UnitOfWork interface'ine ilgili repository'leri ekleyin

3. **Repository Implementasyonu**:
   - Infrastructure katmanında repository sınıflarını oluşturun
   - `IRepository<T>` ve/veya özel repository interface'lerini implement edin
   - DbContext ile veritabanı operasyonlarını gerçekleştirin

4. **UnitOfWork Güncellemesi**:
   - Her iki UnitOfWork implementasyonuna repository property'lerini ekleyin
   - Lazy initialization ile repository'leri başlatın

5. **Servis Implementasyonu**:
   - Application katmanında servis sınıflarını oluşturun
   - UnitOfWork üzerinden repository'lere erişin

6. **Dependency Injection**:
   - DependencyInjection sınıfında servisleri kaydedin

Bu adımları takip ederek, gelecekte benzer sorunlarla karşılaşma riski azalacaktır. 