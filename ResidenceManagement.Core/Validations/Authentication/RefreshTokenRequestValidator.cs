using FluentValidation;
using ResidenceManagement.Core.DTOs.Authentication;

namespace ResidenceManagement.Core.Validations.Authentication
{
    /// <summary>
    /// RefreshTokenRequest nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Yenileme token'ı boş olamaz.")
                .MinimumLength(20).WithMessage("Geçersiz yenileme token formatı.");
        }
    }
} 