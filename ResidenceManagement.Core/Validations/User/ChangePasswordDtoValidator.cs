using FluentValidation;
using ResidenceManagement.Core.DTOs.User;

namespace ResidenceManagement.Core.Validations.User
{
    /// <summary>
    /// ChangePasswordDto nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre boş olamaz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Yeni şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Yeni şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Yeni şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Yeni şifre en az bir özel karakter içermelidir.")
                .NotEqual(x => x.CurrentPassword).WithMessage("Yeni şifre mevcut şifre ile aynı olamaz.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Şifre onayı boş olamaz.")
                .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor.");
        }
    }
} 