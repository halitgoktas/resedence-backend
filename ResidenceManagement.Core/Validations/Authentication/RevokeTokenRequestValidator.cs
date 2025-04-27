using FluentValidation;
using ResidenceManagement.Core.DTOs.Authentication;

namespace ResidenceManagement.Core.Validations.Authentication
{
    /// <summary>
    /// RevokeTokenRequest nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class RevokeTokenRequestValidator : AbstractValidator<RevokeTokenRequest>
    {
        public RevokeTokenRequestValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("İptal edilecek token boş olamaz.")
                .MinimumLength(20).WithMessage("Geçersiz token formatı.");
        }
    }
} 