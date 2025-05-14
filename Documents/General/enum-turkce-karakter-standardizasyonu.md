# Enum Değerlerinde Kullanıcı Arayüzü Türkçe Metin Standardı

## Özet
Bu doküman, Mekik-Residence-Manager projesinde enum değerlerinin isimlendirilmesi ve kullanıcı arayüzünde görüntülenmesine ilişkin standardı açıklamaktadır.

## Standartlar

1. **Enum Sınıf İsimlendirmesi**:
   - Enum sınıf isimleri İngilizce olmalıdır
   - PascalCase yazım şekli kullanılmalıdır
   - Örnek: `PaymentStatus`, `PaymentMethod`, `GenderType`, `MaritalStatus`

2. **Enum Değer İsimlendirmesi**:
   - Enum değerleri İngilizce olmalıdır
   - PascalCase yazım şekli kullanılmalıdır
   - Örnek: `Pending`, `PartiallyPaid`, `Completed`, `Cancelled`

3. **Kullanıcı Arayüzü Metinleri**:
   - Enum değerlerinin kullanıcıya gösterilen metinleri Türkçe olmalıdır
   - Türkçe karakter kullanımına izin verilir ve teşvik edilir
   - Bu metinler veritabanından çekilmelidir (dinamik değişim için)
   - Örnek: `Pending` -> `Beklemede`, `PartiallyPaid` -> `Kısmi Ödeme`, `Completed` -> `Tamamlandı`

4. **Enum Dosya İsimlendirmesi**:
   - Enum dosya adları İngilizce olmalıdır
   - PascalCase yazım şekli kullanılmalıdır
   - Örnek: `PaymentStatus.cs`, `PaymentMethod.cs`

## Örnek Uygulama

### Doğru Kullanım:

```csharp
// PaymentStatus.cs
public enum PaymentStatus
{
    Pending = 1,
    PartiallyPaid = 2,
    Completed = 3,
    Cancelled = 4,
    Delayed = 5
}

// Kullanıcı arayüzünde görüntüleme için
public static class PaymentStatusDisplayNames
{
    private static readonly Dictionary<PaymentStatus, string> DisplayNames = new Dictionary<PaymentStatus, string>
    {
        { PaymentStatus.Pending, "Beklemede" },
        { PaymentStatus.PartiallyPaid, "Kısmi Ödeme" },
        { PaymentStatus.Completed, "Tamamlandı" },
        { PaymentStatus.Cancelled, "İptal" },
        { PaymentStatus.Delayed, "Gecikti" }
    };

    public static string GetDisplayName(PaymentStatus status)
    {
        return DisplayNames.TryGetValue(status, out var displayName) ? displayName : status.ToString();
    }
}
```

### Veritabanı Bazlı Uygulama:

```csharp
// EnumDisplayService.cs
public interface IEnumDisplayService
{
    Task<string> GetDisplayNameAsync(string enumType, int enumValue);
    Task<Dictionary<int, string>> GetAllDisplayNamesForEnumAsync(string enumType);
}

// Kullanım
var paymentStatusText = await _enumDisplayService.GetDisplayNameAsync("PaymentStatus", (int)paymentStatus);
```

### Yanlış Kullanım:

```csharp
// Enum sınıf ismi Türkçe (YANLIŞ)
public enum OdemeDurumu
{
    Beklemede = 1,
    KismiOdeme = 2
}
```

```csharp
// Enum değerleri doğrudan Türkçe (YANLIŞ)
public enum PaymentStatus
{
    Beklemede = 1,
    KısmiÖdeme = 2
}
```

## Gerekçe

1. **Neden Enum Değerleri İngilizce**: 
   - Kodun teknik tutarlılığını sağlar
   - Refactoring, arama ve IDE otomatik tamamlama süreçlerini kolaylaştırır
   - Türkçe karakterler programlama dillerinde sorunlara yol açabilir
   - Uluslararası geliştirme ekipleri için kodun anlaşılırlığını artırır

2. **Neden Kullanıcı Arayüzü Metinleri Türkçe**: 
   - Projenin varsayılan kullanıcı ana dili Türkçedir
   - Son kullanıcılar için daha anlaşılır ve doğaldır
   - Türkçe metinler, Türk kullanıcılar için UX kalitesini yükseltir

3. **Neden Veritabanı Bazlı Metinler**: 
   - Kullanıcı arayüz metinlerinin kod değişikliği gerektirmeden dinamik olarak değiştirilebilmesini sağlar
   - Çoklu dil desteği için esneklik sağlar
   - İş gereksinimlerine göre metin güncellemelerine olanak tanır

## Güncelleme Geçmişi

- **01.06.2025**: Enum değerlerinin İngilizce, gösterilen metinlerin Türkçe olması standardı güncellendi
- **30.05.2025**: Veritabanı bazlı gösterim metinleri yaklaşımı eklendi
- **28.05.2025**: İlk standart tanımı oluşturuldu 