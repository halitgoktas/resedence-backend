# Controller Metotları İçin Standart Kılavuz

Bu doküman, controller sınıflarında kullanılacak metot isimlendirme ve yapılandırma standartlarını belirler.

## 1. Metot İsimlendirme Standartları

Controller içerisindeki metotlar aşağıdaki isimlendirme standartlarına uygun olmalıdır:

### 1.1 Koleksiyon İşlemleri

| HTTP Metodu | Metot İsmi    | Açıklama                                    | Örnek                                 |
|-------------|---------------|---------------------------------------------|---------------------------------------|
| GET         | GetAll        | Tüm kaynakları getir                        | `GetAllUsers()`                       |
| GET         | GetPaged      | Sayfalanmış veriler getir                   | `GetPagedUsers(int page, int size)`   |
| GET         | GetByFilter   | Filtreli veri getir                         | `GetUsersByFilter(UserFilterDto)`     |
| POST        | Create        | Yeni kayıt oluştur                          | `CreateUser(CreateUserDto)`           |
| POST        | CreateBulk    | Toplu kayıt oluştur                         | `CreateBulkUsers(IEnumerable<>)`      |

### 1.2 Tekil Kaynak İşlemleri

| HTTP Metodu | Metot İsmi        | Açıklama                               | Örnek                                    |
|-------------|-------------------|----------------------------------------|------------------------------------------|
| GET         | GetById           | ID ile kayıt getir                     | `GetUserById(int id)`                    |
| PUT         | Update            | Kaydı tamamen güncelle                 | `UpdateUser(int id, UpdateUserDto)`      |
| PATCH       | PartialUpdate     | Kaydı kısmen güncelle                  | `PartialUpdateUser(int id, JsonPatch)`   |
| DELETE      | Delete            | Kaydı sil                              | `DeleteUser(int id)`                     |

### 1.3 İlişkili Kaynak İşlemleri

| HTTP Metodu | Metot İsmi            | Açıklama                                | Örnek                                          |
|-------------|------------------------|------------------------------------------|-------------------------------------------------|
| GET         | GetRelated{Resource}s  | İlişkili kaynakları getir               | `GetRelatedRoles(int userId)`                   |
| POST        | Add{Resource}          | İlişkili kaynak ekle                    | `AddRole(int userId, AddRoleDto)`               |
| DELETE      | Remove{Resource}       | İlişkili kaynağı kaldır                 | `RemoveRole(int userId, int roleId)`            |

### 1.4 Özel İşlemler

| HTTP Metodu | Metot İsmi        | Açıklama                               | Örnek                                          |
|-------------|-------------------|----------------------------------------|-------------------------------------------------|
| POST        | {Action}          | Özel eylem uygula                      | `Approve(int paymentId, ApprovePaymentDto)`     |
| GET         | Export{Format}    | Veriyi dışa aktar                      | `ExportExcel(UserFilterDto)`                    |
| POST        | Import{Format}    | Veriyi içe aktar                       | `ImportExcel(IFormFile file)`                   |

## 2. Metot Dönüş Tipleri

Controller metotları tutarlı bir dönüş tipi yapısına sahip olmalıdır:

### 2.1 Başarılı Yanıtlar

```csharp
// Tek kayıt dönüşü
[ProducesResponseType(typeof(ApiResponse<TDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetById(int id)
{
    var response = await _service.GetByIdAsync(id);
    return Ok(response);
}

// Koleksiyon dönüşü
[ProducesResponseType(typeof(ApiResponse<IEnumerable<TDto>>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    var response = await _service.GetAllAsync();
    return Ok(response);
}

// Kayıt oluşturma
[ProducesResponseType(typeof(ApiResponse<TDto>), StatusCodes.Status201Created)]
public async Task<IActionResult> Create(CreateTDto dto)
{
    var response = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
}

// Veri döndürmeyen işlemler
[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
public async Task<IActionResult> Delete(int id)
{
    var response = await _service.DeleteAsync(id);
    return Ok(response);
}
```

### 2.2 Hata Yanıtları

```csharp
[ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status500InternalServerError)]
```

## 3. Metot Parametreleri

### 3.1 Model Binding

```csharp
// Route parametreleri
public async Task<IActionResult> GetById([FromRoute] int id)

// Query string parametreleri
public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)

// Complex filtreleme
public async Task<IActionResult> GetByFilter([FromQuery] TFilterDto filter)

// Request body
public async Task<IActionResult> Create([FromBody] CreateTDto dto)

// Form verileri
public async Task<IActionResult> Import([FromForm] IFormFile file)
```

### 3.2 Validasyon

```csharp
// Manuel validasyon
public async Task<IActionResult> Create([FromBody] CreateTDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new ApiResponse<CreateTDto>
        {
            Success = false,
            Message = "Validasyon hatası",
            Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
            StatusCode = StatusCodes.Status400BadRequest
        });
    }
    
    // ...
}
```

## 4. Örnek Controller Şablonu

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _userService.GetByIdAsync(id);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<CreateUserDto>
            {
                Success = false,
                Message = "Validasyon hatası",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList(),
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        
        var response = await _userService.CreateAsync(dto);
        
        if (!response.Success)
        {
            return UnprocessableEntity(response);
        }
        
        return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new ApiResponse<UpdateUserDto>
            {
                Success = false,
                Message = "Route parametresi ile DTO içindeki ID uyuşmuyor",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        
        var response = await _userService.UpdateAsync(dto);
        
        if (!response.Success)
        {
            return response.StatusCode switch
            {
                StatusCodes.Status404NotFound => NotFound(response),
                StatusCodes.Status422UnprocessableEntity => UnprocessableEntity(response),
                _ => BadRequest(response)
            };
        }
        
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _userService.DeleteAsync(id);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        
        return Ok(response);
    }
    
    [HttpGet("{id}/roles")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRelatedRoles(int id)
    {
        var response = await _userService.GetUserRolesAsync(id);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        
        return Ok(response);
    }
    
    [HttpPost("{id}/roles")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddRole(int id, [FromBody] AddUserRoleDto dto)
    {
        var response = await _userService.AddUserRoleAsync(id, dto);
        
        if (!response.Success)
        {
            return response.StatusCode switch
            {
                StatusCodes.Status404NotFound => NotFound(response),
                StatusCodes.Status422UnprocessableEntity => UnprocessableEntity(response),
                _ => BadRequest(response)
            };
        }
        
        return Ok(response);
    }
} 