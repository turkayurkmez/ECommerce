using FluentValidation;

namespace ECommerce.Identity.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandRequestValidator : AbstractValidator<ForgotPasswordCommandRequest>
    {
        public ForgotPasswordCommandRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }

}
