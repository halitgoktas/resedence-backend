using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// FluentValidation için validasyon hatalarını ApiResponse formatına dönüştüren işleyici sınıf
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        /// <summary>
        /// İstek validasyonunu gerçekleştirir ve hata varsa standart formatta hata mesajı döner
        /// </summary>
        public async Task<ApiResponse> ValidateAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            if (!_validators.Any())
            {
                return null;
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count == 0)
            {
                return null;
            }

            _logger.LogWarning("Validation errors occurred for {RequestType}: {Errors}", 
                typeof(TRequest).Name, 
                string.Join(", ", failures.Select(x => x.ErrorMessage)));

            var errorMessages = failures
                .Select(f => $"{f.PropertyName}: {f.ErrorMessage}")
                .ToList();

            return ApiResponse.CreateFail(
                message: "Doğrulama hataları oluştu.",
                statusCode: 400,
                errorCode: 400,
                errors: errorMessages);
        }
    }
}