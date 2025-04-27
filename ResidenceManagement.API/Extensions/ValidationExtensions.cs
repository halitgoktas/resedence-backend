using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ResidenceManagement.Core.Common;
using FluentValidation.AspNetCore;

namespace ResidenceManagement.API.Extensions
{
    /// <summary>
    /// Validasyon ile ilgili extension metodları
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// API validasyon altyapısını yapılandırır
        /// </summary>
        public static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            // ModelState validasyon ayarları
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var response = ApiResponse.Failure(
                        message: "Doğrulama hataları oluştu.",
                        statusCode: HttpStatusCode.BadRequest);

                    // Hataları ekle
                    response.Errors.AddRange(errors);

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
} 