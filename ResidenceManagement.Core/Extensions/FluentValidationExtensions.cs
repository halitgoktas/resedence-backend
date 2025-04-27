using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.Core.Extensions
{
    /// <summary>
    /// FluentValidation için extension metodları içerir
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// FluentValidation entegrasyonunu yapılandırır
        /// </summary>
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            // Tüm validator'ları otomatik olarak kaydet
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // FluentValidation.AspNetCore entegrasyonu
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Validasyon davranışı
            services.AddScoped(typeof(ValidationBehavior<,>));

            return services;
        }
    }
} 