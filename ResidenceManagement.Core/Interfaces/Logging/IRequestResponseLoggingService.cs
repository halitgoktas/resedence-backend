using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ResidenceManagement.Core.Interfaces.Logging
{
    /// <summary>
    /// HTTP istek ve yanıtlarını loglayan servis arayüzü
    /// </summary>
    public interface IRequestResponseLoggingService
    {
        /// <summary>
        /// HTTP isteğini loglar
        /// </summary>
        /// <param name="context">HTTP context</param>
        Task LogRequestAsync(HttpContext context);

        /// <summary>
        /// HTTP yanıtını loglar
        /// </summary>
        /// <param name="context">HTTP context</param>
        Task LogResponseAsync(HttpContext context);
    }
} 