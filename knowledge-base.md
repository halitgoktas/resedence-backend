# Residence Management Projesi - Hata Çözüm Veritabanı

Bu dosya, projede karşılaşılan hata mesajlarını ve bunların çözümlerini saklamak için kullanılır. Yeni bir hata ile karşılaşıldığında önce bu veritabanını kontrol edin, çözüm yoksa çözüm bulunduktan sonra buraya ekleyin.

## [error CS0104: 'ITenant', 'ResidenceManagement.Core.Common.ITenant' ile 'ResidenceManagement.Core.Interfaces.ITenant' arasında belirsiz bir başvuru]Çözüm: 
Bu hata, aynı isimde iki farklı ITenant arayüzü olduğunu gösteriyor. Bu dosyalarda tam yol adını kullanarak belirtin:
```csharp
using ResidenceManagement.Core.Common; // veya
using ResidenceManagement.Core.Interfaces;

// Sınıf içinde:
public class MyClass : ResidenceManagement.Core.Common.ITenant // Tam yol belirtin
```

## [error CS0246: 'CreateKbsGuestInfoDto' türü veya ad alanı adı bulunamadı]Çözüm:
İlgili sınıfın tanımlandığı ad alanını import edin veya sınıfı oluşturun:
```csharp
using ResidenceManagement.Core.DTOs.Kbs; // Türün bulunduğu ad alanı
// veya
// CreateKbsGuestInfoDto sınıfını oluşturun
```

## [error CS0311: 'ResidenceManagement.Core.Entities.Kbs.KbsNotification' türü, 'IGenericRepository<T>' genel türü veya yöntemi için 'T' tür parametresi olarak kullanılamaz.]Çözüm:
KbsNotification sınıfının BaseEntity'den türetildiğinden emin olun:
```csharp
// KbsNotification.cs dosyasında:
public class KbsNotification : ResidenceManagement.Core.Entities.Base.BaseEntity
{
    // Sınıf özellikleri...
}
```

## [error CS0246: 'KbsNotificationCreateDto' türü veya ad alanı adı bulunamadı]Çözüm:
İlgili DTO sınıfını tanımlayın veya import edin:
```csharp
using ResidenceManagement.Core.DTOs.Kbs;
// veya DTOs klasöründe bu sınıfı oluşturun
public class KbsNotificationCreateDto
{
    // Özellikler...
}
```

## [error CS0535: 'KbsIntegrationService', 'IKbsIntegrationService.SendNotificationAsync(KbsNotification)' arabirim üyesini uygulamaz]Çözüm:
KbsIntegrationService sınıfında eksik yöntemleri uygulayın:
```csharp
public class KbsIntegrationService : IKbsIntegrationService
{
    // Diğer methodlar...
    
    public async Task<bool> SendNotificationAsync(KbsNotification notification)
    {
        // Uygulama...
        return true;
    }
    
    public async Task<KbsNotificationStatusResultDto> CheckNotificationStatusAsync(string referenceId)
    {
        // Uygulama...
        return new KbsNotificationStatusResultDto();
    }
    
    // Diğer eksik methodları da uygulayın...
}
```

## [error CS0738: 'KbsIntegrationService, 'IKbsIntegrationService.GetNotificationLogsAsync(string)' arabirim üyesini uygulamıyor. 'Task<IEnumerable<KbsNotificationLog>>' eşleşen dönüş türüne sahip olmadığından]Çözüm:
Metodun dönüş türünü düzeltin ve arabirimi doğru şekilde uygulayın:
```csharp
public async Task<IEnumerable<KbsNotificationLog>> GetNotificationLogsAsync(string referenceId)
{
    // Uygulama...
    return await _kbsNotificationLogRepository.GetAllAsync(l => l.ReferenceId == referenceId);
}
```

## [error CS0104: 'BaseEntity', 'ResidenceManagement.Core.Entities.Base.BaseEntity' ile 'ResidenceManagement.Core.Entities.Common.BaseEntity' arasında belirsiz bir başvuru]Çözüm:
ServiceRequest sınıfında tam yol belirtin:
```csharp
using ResidenceManagement.Core.Entities.Base; // veya
using ResidenceManagement.Core.Entities.Common;

// Sınıf tanımında:
public class ServiceRequest : ResidenceManagement.Core.Entities.Base.BaseEntity // Tam yol kullanın
```

## [error CS0246: 'KbsNotification' türü veya ad alanı adı bulunamadı]Çözüm:
KbsNotification sınıfını oluşturun veya import edin:
```csharp
using ResidenceManagement.Core.Entities.Kbs;
// veya
// Entities/Kbs klasöründe bu sınıfı oluşturun
namespace ResidenceManagement.Core.Entities.Kbs
{
    public class KbsNotification : BaseEntity
    {
        // Özellikler...
    }
}
```

## [error CS0246: 'KbsNotificationStatusResultDto' türü veya ad alanı adı bulunamadı]Çözüm:
İlgili DTO sınıfını tanımlayın:
```csharp
namespace ResidenceManagement.Core.DTOs
{
    public class KbsNotificationStatusResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        // Diğer özellikler...
    }
}
```

## [error CS0246: 'KbsNotificationLog' türü veya ad alanı adı bulunamadı]Çözüm:
KbsNotificationLog sınıfını tanımlayın:
```csharp
namespace ResidenceManagement.Core.Entities.Kbs
{
    public class KbsNotificationLog : BaseEntity
    {
        public string ReferenceId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
        // Diğer özellikler...
    }
}
```

## [Hata Mesajı]Çözüm: RefreshToken Repository Kalıtım Hatası
   **Hata:** error CS0311: 'ResidenceManagement.Core.Entities.Identity.RefreshToken' türü, 'IRepository<T>' genel türü veya yöntemi için 'T' tür parametresi olarak kullanılamaz.
   
   **Çözüm:** RefreshToken sınıfının BaseEntity'den miras almasını sağlayın:
   ```csharp
   // RefreshToken.cs
   public class RefreshToken : ResidenceManagement.Core.Entities.Base.BaseEntity, ResidenceManagement.Core.Common.ITenant
   {
       // Mevcut özellikler...
   }
   ```
   Bu hata, IRepository<T> generic tipinde T parametresinin BaseEntity tipinden türeyen sınıflar olması gerektiğinden kaynaklanır. RefreshToken sınıfı sadece ITenant arayüzünü uyguladığı için, IRepository<RefreshToken> şeklinde kullanılamıyordu. BaseEntity sınıfından türetilerek sorun çözülmüştür. 

## [Hata Mesajı]Çözüm: Sube ITenant Belirsiz Başvuru
   **Hata:** error CS0104: 'ITenant', 'ResidenceManagement.Core.Common.ITenant' ile 'ResidenceManagement.Core.Interfaces.ITenant' arasında belirsiz bir başvuru
   
   **Çözüm:** Tam ad alanını kullanarak belirtin:
   ```csharp
   public class Sube : BaseEntity, ResidenceManagement.Core.Common.ITenant // veya diğer versiyonu
   ```
   Bu hata, aynı isimde iki farklı ITenant arayüzü olduğunda oluşur. Sınıf deklarasyonunda tam ad alanı yolu belirtilerek hangi ITenant'ın kullanılması gerektiği açıkça belirtilmiş olur ve böylece derleme hatası ortadan kalkar.

## [Hata Mesajı]Çözüm: UserRole ITenant Belirsiz Başvuru
   **Hata:** error CS0104: 'ITenant', 'ResidenceManagement.Core.Common.ITenant' ile 'ResidenceManagement.Core.Interfaces.ITenant' arasında belirsiz bir başvuru
   
   **Çözüm:** Tam ad alanını kullanarak belirtin:
   ```csharp
   public class UserRole : BaseEntity, ResidenceManagement.Core.Common.ITenant // veya diğer versiyonu
   ```
   Bu hata, aynı isimde iki farklı ITenant arayüzü olduğunda oluşur. Sınıf deklarasyonunda tam ad alanı yolu belirtilerek hangi ITenant'ın kullanılması gerektiği açıkça belirtilmiş olur ve böylece derleme hatası ortadan kalkar.

## [Hata Mesajı]Çözüm: KbsNotification Yinelenen Özellik Tanımları
   **Hata:** error CS0102: 'KbsNotification' türü 'Status' için zaten bir tanım içeriyor
   
   **Çözüm:** KbsNotification sınıfında yinelenen özellikleri kaldırın:
   ```csharp
   public class KbsNotification : BaseEntity
   {
       // Yinelenen özellikleri kaldırın veya birleştirin:
       // Status
       // CheckInDate
       // ActualCheckOutDate
       // ApartmentId
       
       // Sınıfın geri kalanı...
   }
   ```
   Bu hata, sınıf içinde aynı isimde birden fazla özellik tanımlandığında oluşur. Tekrarlanan özellikleri kaldırarak veya tek bir tanıma birleştirerek hatayı giderebilirsiniz.

## [Hata Mesajı]Çözüm: ServiceRequest BaseEntity Belirsiz Başvuru
   **Hata:** error CS0104: 'BaseEntity', 'ResidenceManagement.Core.Entities.Base.BaseEntity' ile 'ResidenceManagement.Core.Entities.Common.BaseEntity' arasında belirsiz bir başvuru
   
   **Çözüm:** ServiceRequest sınıfında tam ad alanını kullanarak belirtin:
   ```csharp
   public class ServiceRequest : ResidenceManagement.Core.Entities.Base.BaseEntity
   {
       // İç sınıflarda da tam yol kullanın:
       public class ServiceRequestHistory : ResidenceManagement.Core.Entities.Base.BaseEntity
       {
           // ...
       }
       
       public class ServiceRequestAssignment : ResidenceManagement.Core.Entities.Base.BaseEntity
       {
           // ...
       }
       
       public class ServiceRequestNote : ResidenceManagement.Core.Entities.Base.BaseEntity
       {
           // ...
       }
   }
   ```
   Bu hata, aynı isimde iki farklı BaseEntity sınıfı olduğunda oluşur: bir tanesi ResidenceManagement.Core.Entities.Base, diğeri ResidenceManagement.Core.Entities.Common namespace'inde. Tam yol belirterek hangi sınıftan miras alındığı açıkça belirtilir ve derleme hatası ortadan kalkar.

## [Hata Mesajı]Çözüm: KbsNotificationLog BaseEntity Belirsiz Başvuru
   **Hata:** error CS0104: 'BaseEntity', 'ResidenceManagement.Core.Entities.Base.BaseEntity' ile 'ResidenceManagement.Core.Entities.Common.BaseEntity' arasında belirsiz bir başvuru
   
   **Çözüm:** Tam ad alanını kullanarak belirtin:
   ```csharp
   public class KbsNotificationLog : ResidenceManagement.Core.Entities.Base.BaseEntity
   {
       // ...
   }
   ```
   Bu hata, aynı isimde iki farklı BaseEntity sınıfı olduğunda oluşur: Biri ResidenceManagement.Core.Entities.Base, diğeri ResidenceManagement.Core.Entities.Common namespace'inde. Tam yol belirterek hangi sınıftan miras alındığı açıkça belirtilir ve derleme hatası ortadan kalkar.