using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ResidenceManagement.Core.Services;
using ResidenceManagement.Infrastructure.Services;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// Loglama servisleri için uzantı metotları
    /// </summary>
    public static class LoggingServiceExtensions
    {
        /// <summary>
        /// Loglama servislerini kaydeder
        /// </summary>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Operasyon log servisi
            services.AddScoped<IOperationLogService, OperationLogService>();
            
            // Audit log servisi
            services.AddScoped<IAuditLogService, AuditLogService>();
            
            // Loglama filtreleri
            services.AddScoped<LoggingActionFilter>();
            
            // Performans loglama
            services.AddScoped<PerformanceLogAttribute>();
            
            return services;
        }
    }
    
    /// <summary>
    /// Controller action'ları için loglama filtresi
    /// </summary>
    public class LoggingActionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;
        private readonly IAuditLogService _auditLogService;
        // Parametreleri saklamak için kullanılacak değişken
        private object? _actionParameters = null;
        
        public LoggingActionFilter(ILogger<LoggingActionFilter> logger, IAuditLogService auditLogService)
        {
            _logger = logger;
            _auditLogService = auditLogService;
        }
        
        public void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            // Parametreleri sınıf değişkeninde sakla
            _actionParameters = context.ActionArguments;
            
            // Action çalışmaya başlarken log
            _logger.LogInformation(
                "Action {ControllerName}.{ActionName} executing with parameters: {@Parameters}",
                context.ActionDescriptor.RouteValues["controller"],
                context.ActionDescriptor.RouteValues["action"],
                context.ActionArguments
            );
        }
        
        public void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context)
        {
            // Action çalışması bitince log
            if (context.Exception != null)
            {
                _logger.LogError(
                    context.Exception,
                    "Action {ControllerName}.{ActionName} threw an exception: {ExceptionMessage}",
                    context.ActionDescriptor.RouteValues["controller"],
                    context.ActionDescriptor.RouteValues["action"],
                    context.Exception.Message
                );
            }
            else
            {
                _logger.LogInformation(
                    "Action {ControllerName}.{ActionName} executed with result: {Result}",
                    context.ActionDescriptor.RouteValues["controller"],
                    context.ActionDescriptor.RouteValues["action"],
                    context.Result
                );
                
                // Eğer kullanıcı kimliği doğrulanmışsa audit log kaydet
                if (context.HttpContext.User.Identity?.IsAuthenticated == true)
                {
                    var userId = context.HttpContext.User.FindFirst("sub")?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        try
                        {
                            var controller = context.ActionDescriptor.RouteValues["controller"] ?? "";
                            var action = context.ActionDescriptor.RouteValues["action"] ?? "";
                            var parameters = _actionParameters ?? new object(); // Null ise boş obje kullan
                            
                            // Result null olabilir, bu durumda yeni bir boş object geçirelim
                            object result = context.Result ?? new object();
                            
                            _auditLogService.LogAsync(
                                userId,
                                controller,
                                action,
                                parameters,
                                result,
                                context.HttpContext.Request.Path
                            );
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to log audit information");
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Performans loglama attribute'u
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class PerformanceLogAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        private readonly ILogger<PerformanceLogAttribute> _logger;
        private System.Diagnostics.Stopwatch? _stopwatch;
        
        public PerformanceLogAttribute(ILogger<PerformanceLogAttribute> logger)
        {
            _logger = logger;
        }
        
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            _stopwatch = System.Diagnostics.Stopwatch.StartNew();
            base.OnActionExecuting(context);
        }
        
        public override void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context)
        {
            if (_stopwatch != null)
            {
                _stopwatch.Stop();
                var elapsedMs = _stopwatch.ElapsedMilliseconds;
                
                _logger.LogInformation(
                    "Performance: {ControllerName}.{ActionName} executed in {ElapsedMilliseconds} ms",
                    context.ActionDescriptor.RouteValues["controller"],
                    context.ActionDescriptor.RouteValues["action"],
                    elapsedMs
                );
            }
            
            base.OnActionExecuted(context);
        }
    }
} 