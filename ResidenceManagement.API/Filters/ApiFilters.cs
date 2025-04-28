using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ResidenceManagement.API.Filters
{
    /// <summary>
    /// API katmanı için genel filtreler
    /// </summary>
    public class ApiFilters
    {
        /// <summary>
        /// Maintenence Schedule filtreleme işlemleri için action filter
        /// </summary>
        public class MaintenanceScheduleFilter : IActionFilter
        {
            /// <summary>
            /// Aksiyon çalıştırılmadan önce
            /// </summary>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                // Gerekli doğrulamalar yapılabilir
                if (context.ActionArguments.TryGetValue("filter", out var filter))
                {
                    if (filter == null)
                    {
                        context.Result = new BadRequestObjectResult("Filtre parametresi geçersiz.");
                        return;
                    }
                }
            }

            /// <summary>
            /// Aksiyon çalıştırıldıktan sonra
            /// </summary>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                // Sonuç işlemleri yapılabilir
            }
        }
    }
} 