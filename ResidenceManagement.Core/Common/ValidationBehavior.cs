using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// FluentValidation için validasyon hatalarını ApiResponse formatına dönüştüren işleyici sınıf
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>
        where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
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
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count == 0)
            {
                return null;
            }

            var errorMessages = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToList();
            return ApiResponse.Failure("Doğrulama hataları oluştu.", HttpStatusCode.BadRequest);
        }

        public async Task<ApiResponse<T>> ValidateAsync<T>(TRequest request, CancellationToken cancellationToken = default)
        {
            var response = await ValidateAsync(request, cancellationToken);
            if (response == null)
            {
                return null;
            }

            return ApiResponse<T>.Failure("Doğrulama hataları oluştu.", HttpStatusCode.BadRequest);
        }
    }
}