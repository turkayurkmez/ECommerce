using FluentValidation;

namespace ECommerce.Identity.Application.Features.Roles.Commands.CreateRole
{
    //Validation
    public class CreateRoleCommandRequestValidator : AbstractValidator<CreateRoleCommandRequest>
    {
        public CreateRoleCommandRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .MaximumLength(50)
                .WithMessage("Role name must be at most 50 characters long.");
            
        }


    }
}
