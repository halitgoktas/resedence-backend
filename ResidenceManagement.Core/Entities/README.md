# Residence Manager Temel Entity Sınıfları

Bu dizinde, Residence Manager uygulamasının ana domain entity'leri bulunmaktadır. Bu entity'ler, uygulamanın temel iş modelini temsil eder.

## Residence (Site/Rezidans)

`Residence` entity'si, yönetilen site veya rezidans bilgilerini temsil eder.

```csharp
public class Residence : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostalCode { get; set; }
    public string TaxNumber { get; set; }
    public string TaxOffice { get; set; }
    public int TotalBlocks { get; set; }
    public int TotalApartments { get; set; }
    public int? ManagementCompanyId { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public int ManagerId { get; set; }
    
    // İlişkiler
    public virtual User Manager { get; set; }
    public virtual ICollection<Block> Blocks { get; set; }
    public virtual ICollection<Dues> Dues { get; set; }
    public virtual ICollection<DuesDefinition> DuesDefinitions { get; set; }
    public virtual ICollection<ResidenceManager> Managers { get; set; }
    public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
}
```

## Block (Blok)

`Block` entity'si, bir site içindeki blokları temsil eder.

```csharp
public class Block : BaseEntity
{
    public string Name { get; set; }
    public int FloorCount { get; set; }
    public int ApartmentCount { get; set; }
    public int ResidenceId { get; set; }
    
    // İlişkiler
    public virtual Residence Residence { get; set; }
    public virtual ICollection<Apartment> Apartments { get; set; }
}
```

## Apartment (Daire)

`Apartment` entity'si, bir blok içindeki daireleri temsil eder.

```csharp
public class Apartment : BaseEntity
{
    public string Number { get; set; }
    public string Type { get; set; }
    public int Floor { get; set; }
    public double GrossArea { get; set; }
    public double NetArea { get; set; }
    public int BlockId { get; set; }
    public int ResidenceId { get; set; }
    public OccupancyStatus OccupancyStatus { get; set; }
    public UsageType UsageType { get; set; }
    public int? OwnerId { get; set; }
    public int? TenantId { get; set; }
    
    // İlişkiler
    public virtual Block Block { get; set; }
    public virtual Residence Residence { get; set; }
    public virtual User Owner { get; set; }
    public virtual User Tenant { get; set; }
    public virtual ICollection<Dues> DuesPayments { get; set; }
}
```

## Entity İlişkileri

Ana entity'lerin ilişkileri aşağıdaki gibidir:

### One-to-Many İlişkiler:
- Bir `Residence` birden çok `Block` içerebilir
- Bir `Block` birden çok `Apartment` içerebilir
- Bir `Residence` dolaylı olarak birden çok `Apartment` içerir
- Bir `User` birden çok `Apartment`'ın sahibi olabilir
- Bir `User` birden çok `Apartment`'ta kiracı olabilir

### Many-to-Many İlişkiler:
- Bir `Residence`'ın birden çok yöneticisi (`ResidenceManager`) olabilir
- Bir `User` birden çok `Residence`'ın yöneticisi olabilir

## Veri Modeli

Temel entity'ler arasındaki ilişkiler şu şekildedir:

```
Residence (Site/Rezidans)
↓ 1:N
Block (Blok)
↓ 1:N
Apartment (Daire)
↓ N:1       ↓ N:1
User (Malik)  User (Kiracı)
```

## Multi-Tenant Yapı

Tüm entity'ler `BaseEntity` sınıfından türetilmiştir ve `FirmaId` ve `SubeId` alanlarını içerir. Bu nedenle:

- Bir firma için birden çok site/rezidans tanımlanabilir
- Tüm veriler otomatik olarak firma ve şube bazında filtrelenir
- Kullanıcılar sadece kendi firmalarına ait verilere erişebilir 