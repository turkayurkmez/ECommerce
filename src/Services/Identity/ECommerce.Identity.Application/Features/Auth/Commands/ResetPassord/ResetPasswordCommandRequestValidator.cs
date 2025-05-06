using FluentValidation;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ResetPassord
{
    public class ResetPasswordCommandRequestValidator : AbstractValidator<ResetPasswordCommandRequest>
    {
        public ResetPasswordCommandRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email boş olamaz")
                .EmailAddress()
                .WithMessage("Geçersiz email adresi");
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token boş olamaz");
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Yeni şifre boş olamaz")
                .MinimumLength(6)
                .WithMessage("Yeni şifre en az 6 karakter olmalıdır")
                .MaximumLength(20)
                .WithMessage("Yeni şifre en fazla 20 karakter olmalıdır");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Şifre onayı boş olamaz")
                .Equal(x => x.NewPassword)
                .WithMessage("Şifre onayı yeni şifre ile aynı olmalıdır");
        }

    }

}
