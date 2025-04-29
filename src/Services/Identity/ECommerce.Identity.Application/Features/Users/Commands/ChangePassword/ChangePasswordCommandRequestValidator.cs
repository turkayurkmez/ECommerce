using FluentValidation;

namespace ECommerce.Identity.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandRequestValidator : AbstractValidator<ChangePasswordCommandRequest>
    {
        public ChangePasswordCommandRequestValidator()
        {

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("Mevcut şifre boş olamaz");
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Yeni şifre boş olamaz")
                .MinimumLength(6)
                .WithMessage("Yeni şifre en az 6 karakter olmalıdır");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Yeni şifre tekrar boş olamaz")
                .Equal(x => x.NewPassword)
                .WithMessage("Yeni şifreler eşleşmiyor");
        }
    }

}
