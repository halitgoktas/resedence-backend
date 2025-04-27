using FluentValidation;
using ResidenceManagement.Core.DTOs.User;
using System.Text.RegularExpressions;

namespace ResidenceManagement.Core.Validations.User
{
    /// <summary>
    /// UpdateUserDto nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telefon)
                .Matches(new Regex(@"^[0-9\s\+\(\)]+$")).WithMessage("Geçerli bir telefon numarası giriniz.")
                .When(x => !string.IsNullOrEmpty(x.Telefon));

            RuleFor(x => x.Ad)
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Ad));

            RuleFor(x => x.Soyad)
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Soyad));

            RuleFor(x => x.SubeId)
                .GreaterThan(0).WithMessage("Geçerli bir şube seçmelisiniz.")
                .When(x => x.SubeId.HasValue);
        }
    }
} 