using FluentValidation;
using ResidenceManagement.Core.DTOs.Authentication;

namespace ResidenceManagement.Core.Validations.Authentication
{
    /// <summary>
    /// LoginRequest nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı veya e-posta adresi boş olamaz.")
                .Length(3, 100).WithMessage("Kullanıcı adı veya e-posta adresi 3-100 karakter arasında olmalıdır.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
} 