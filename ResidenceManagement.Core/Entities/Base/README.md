# Temel Entity Sınıfları

Bu dizinde, Residence Manager uygulamasındaki tüm domain entity'leri için temel sınıflar bulunmaktadır. Bu temel sınıflar, entity'lerin ortak özelliklerini ve davranışlarını sağlar.

## BaseEntity

`BaseEntity` sınıfı, uygulamadaki çoğu entity için temel oluşturur ve multi-tenant yapısını destekler.

```csharp
public abstract class BaseEntity : ITenant
{
    public virtual int Id { get; set; }
    public virtual int FirmaId { get; set; }
    public virtual int SubeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int? DeletedBy { get; set; }
    public bool IsActive { get; set; }
    
    public BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
        IsDeleted = false;
        IsActive = true;
    }
}
```

### Önemli Özellikler

- **Id**: Birincil anahtar
- **FirmaId, SubeId**: Multi-tenant filtreleme için alanlar
- **CreatedDate, UpdatedDate**: Audit trail için zaman bilgileri
- **CreatedBy, UpdatedBy, DeletedBy**: İşlemi yapan kullanıcı bilgileri
- **IsDeleted**: Yumuşak silme (soft delete) için bayrak
- **IsActive**: Aktif/Pasif durumu için bayrak

## BaseTransactionEntity

`BaseTransactionEntity` sınıfı, işlem (transaction) tipindeki entity'ler için genişletilmiş özelliklere sahiptir.

```csharp
public abstract class BaseTransactionEntity : BaseEntity
{
    public DateTime TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
    public int? ProcessedBy { get; set; }
    public string Notes { get; set; }
    public int Status { get; set; }
    public DateTime? StatusDate { get; set; }
    public int? RelatedEntityId { get; set; }
    public string RelatedEntityType { get; set; }
    public decimal Amount { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovalDate { get; set; }
}
```

### Önemli Özellikler

- **TransactionDate**: İşlem tarihi
- **ReferenceNumber**: İşlem referans numarası
- **Status**: İşlem durumu (Beklemede, Onaylandı, Reddedildi vb.)
- **StatusDate**: Durum değişiklik tarihi
- **RelatedEntityId, RelatedEntityType**: İlişkili entity bilgileri
- **Amount**: İşlem tutarı
- **ApprovedBy, ApprovalDate**: Onay bilgileri

## BaseSystemEntity

`BaseSystemEntity` sınıfı, tenant filtresi olmayan, tüm sistem genelinde geçerli entity'ler için kullanılır.

```csharp
public abstract class BaseSystemEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
```

### Kullanım Alanları

Bu temel sınıf, aşağıdaki durumlarda kullanılır:
- Sistem ayarları
- Global yapılandırmalar
- Tenant'lar arası ortak veriler
- Referans verileri (örn. ülke, il, ilçe tabloları)

## Kullanım Stratejisi

- Çoğu entity için `BaseEntity` sınıfını miras alın
- Ödeme, sipariş, talep gibi işlem entity'leri için `BaseTransactionEntity` sınıfını miras alın
- Global, tenant-filtresiz entity'ler için `BaseSystemEntity` sınıfını miras alın 