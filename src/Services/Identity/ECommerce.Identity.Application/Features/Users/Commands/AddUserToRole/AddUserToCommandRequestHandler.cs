using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Repositories;
using MediatR;

namespace ECommerce.Identity.Application.Features.Users.Commands.AddUserToRole
{
    public class AddUserToCommandRequestHandler : IRequestHandler<AddUserToCommandRequest, Result>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public AddUserToCommandRequestHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }
        public async Task<Result> Handle(AddUserToCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetWithRolesAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Kullanıcı bulunamadı");
            }
            if (!user.Roles.Any(r=>r.Role.Name == request.RoleName))
            {
                return Result.Failure("Kullanıcı zaten bu role sahip");
            }
            var role = await roleRepository.GetByNameAsync(request.RoleName, cancellationToken);
            if (role == null)
            {
                return Result.Failure("Rol bulunamadı");
            }
            user.AddRole(role);

            await userRepository.UpdateAsync(user, cancellationToken);

            return Result.Success("Kullanıcı role eklendi");
        }
    }
}
