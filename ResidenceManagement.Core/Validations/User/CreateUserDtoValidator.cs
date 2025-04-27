using FluentValidation;
using ResidenceManagement.Core.DTOs.User;
using System.Text.RegularExpressions;

namespace ResidenceManagement.Core.Validations.User
{
    /// <summary>
    /// CreateUserDto nesnesi için doğrulama kuralları tanımlar
    /// </summary>
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .Length(3, 50).WithMessage("Kullanıcı adı 3-50 karakter arasında olmalıdır.")
                .Matches("^[a-zA-Z0-9._]+$").WithMessage("Kullanıcı adı yalnızca harf, rakam, nokta ve alt çizgi içerebilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Telefon)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(new Regex(@"^[0-9\s\+\(\)]+$")).WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Soyad)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Sifre)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

            RuleFor(x => x.SifreOnay)
                .NotEmpty().WithMessage("Şifre onayı boş olamaz.")
                .Equal(x => x.Sifre).WithMessage("Şifreler eşleşmiyor.");

            RuleFor(x => x.FirmaId)
                .NotEmpty().WithMessage("Firma bilgisi gereklidir.")
                .GreaterThan(0).WithMessage("Geçerli bir firma seçmelisiniz.");

            RuleFor(x => x.SubeId)
                .NotEmpty().WithMessage("Şube bilgisi gereklidir.")
                .GreaterThan(0).WithMessage("Geçerli bir şube seçmelisiniz.");
        }
    }
} 