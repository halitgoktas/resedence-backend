# Residence Manager - Kapsamlı Yazılım Geliştirme Standartları

Bu dokümantasyon, SQL Server, C# (.NET) ve React.js ile Clean Architecture/Onion Architecture prensiplerine dayalı geliştirme yaparken uyulması gereken teknik standartları içerir. Tüm geliştirme, refaktör ve bakım işlemleri bu standartlara bağlı kalmalıdır.

## 1. Mimari ve Tasarım İlkeleri

- **Katmanlı Mimari**: Backend için Clean Architecture / Onion Architecture prensiplerine uygun yapı oluştur:
  - Core (Entities, Interfaces, DTOs, Domain Services)
  - Application (Use Cases, Application Services, Validators)
  - Infrastructure (DB, External Services, Implementations)
  - Presentation (API Controllers, Filters, Middlewares)

- **Bağımlılık Yönetimi**:
  - Bağımlılıklar her zaman daha içteki katmanlara doğru olmalı
  - Domain katmanı hiçbir dış katmana bağımlı olmamalı
  - Dependency Injection ile gevşek bağlaşma (loose coupling) sağlanmalı
  - Interface'ler ile servis soyutlaması yaratılmalı

- **SOLID Prensipleri**:
  - Single Responsibility: Her sınıf/modül tek bir sorumluluk taşımalı
  - Open/Closed: Sınıflar değişikliğe kapalı, genişletmeye açık olmalı
  - Liskov Substitution: Alt sınıflar üst sınıfların yerine geçebilmeli
  - Interface Segregation: İstemciler kullanmadıkları metodlara bağımlı olmamalı
  - Dependency Inversion: Soyutlamalara bağımlı olunmalı, somut implementasyonlara değil

## 2. İsimlendirme ve Dil Standardı

### 2.1 Genel İsimlendirme Kuralları
- Tüm kod tabanında İngilizce kullanılmalı (yorum satırları Türkçe olabilir)
- Türkçe karakter kullanılmamalı
- Kısaltmalar yerine açık ve anlaşılır isimler tercih edilmeli

### 2.2 Entity İsimlendirme Standardı
```markdown
1. Entity İsimleri:
   - PascalCase kullanılmalı
   - Tekil form kullanılmalı (Companies yerine Company)
   - Domain-specific prefix/suffix kullanılmamalı
   - Örnek: User, Company, Branch, Apartment

2. Property İsimleri:
   - PascalCase kullanılmalı
   - Bool property'ler Is/Has/Can ile başlamalı
   - ID alanları için EntityNameId formatı kullanılmalı
   - Örnek: CompanyId, IsActive, HasPermission

3. Navigation Property'ler:
   - İlişki tipini yansıtmalı (tekil/çoğul)
   - Virtual keyword kullanılmalı
   - Örnek: public virtual Company Company { get; set; }
   - Örnek: public virtual ICollection<Branch> Branches { get; set; }
```

### 2.3 Dosya Başlangıç Yorum Standardı
Her .cs dosyasının başında aşağıdaki formatta yorum bloğu bulunmalıdır:

```csharp
// -----------------------------------------------------------------------
// <copyright file="EntityName.cs" company="ResidenceManager">
// Copyright (c) ResidenceManager. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// Purpose: [Entity/Service/Controller amacının kısa açıklaması]
// Dependencies: [Bağımlı olduğu temel sınıflar/servisler]
// Usage: [Kullanım örneği veya önemli notlar]
// Author: [Geliştirici adı]
// Created: [Oluşturulma tarihi]
// Modified: [Son değişiklik tarihi]
// -----------------------------------------------------------------------
```

### 2.4 Namespace Standardı
```csharp
// Doğru
namespace ResidenceManager.Core.Entities.Identity
namespace ResidenceManager.Infrastructure.Data.Repositories
namespace ResidenceManager.API.Controllers

// Yanlış
namespace ResidenceYonetimi.Core.Entities
namespace Residence.Manager.Core
namespace ResidenceManager.Entities.Firma
```

### 2.5 Çakışma Önleme Kuralları
1. **Entity Lokasyon Kontrolü**:
   - Yeni entity eklemeden önce tüm solution'da arama yapılmalı
   - Hem İngilizce hem Türkçe karşılıkları kontrol edilmeli
   - Benzer isimli entity'ler için ekip lead'i ile görüşülmeli

2. **Merkezi Entity Repository**:
   - Tüm entity'ler Core/Entities altında toplanmalı
   - Alt domainler için uygun klasör yapısı kullanılmalı
   - Entity isimleri solution-wide unique olmalı

3. **Code Review Kontrol Listesi**:
   ```markdown
   - [ ] Entity ismi İngilizce mi?
   - [ ] Dosya başlangıç yorumu eklenmiş mi?
   - [ ] Namespace standardına uygun mu?
   - [ ] Benzer/çakışan entity var mı?
   - [ ] Base class doğru seçilmiş mi?
   ```

4. **Entity Değişiklik Prosedürü**:
   - Entity değişikliği için pull request zorunlu
   - Entity değişikliği için ekip lead onayı zorunlu
   - Değişiklik öncesi etki analizi zorunlu
   - Migration script'leri için DBA review zorunlu

### 2.6 Dökümantasyon Gereksinimleri
1. **XML Documentation**:
   ```csharp
   /// <summary>
   /// Represents a company in the residence management system
   /// </summary>
   /// <remarks>
   /// This entity is part of the multi-tenant architecture
   /// </remarks>
   public class Company : BaseEntity, ITenant
   ```

2. **README Güncellemesi**:
   - Her yeni entity için README güncellenmeli
   - Entity ilişkileri dokümante edilmeli
   - Breaking change'ler belirtilmeli

## 3. Backend (.NET Core) Geliştirme Standartları

## 4. Frontend (React) Geliştirme Standartları

## 5. Multi-Tenant Mimari

## 6. DevOps ve CI/CD

## 7. Kod Kalitesi ve Kodlama Standartları

## 8. Geliştirme Süreci

## 9. Teknik Borç Yönetimi

Bu standartlar, projenin tüm yaşam döngüsü boyunca güncel tutulacak ve gerektiğinde genişletilecektir. 