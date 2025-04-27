using Microsoft.AspNetCore.Mvc;

namespace ResidenceManagement.API.Controllers.V1
{
    /// <summary>
    /// API V1 controllerları için temel sınıf.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : Controllers.BaseApiController
    {
        // Ana BaseApiController'dan miras alınır
    }
} 