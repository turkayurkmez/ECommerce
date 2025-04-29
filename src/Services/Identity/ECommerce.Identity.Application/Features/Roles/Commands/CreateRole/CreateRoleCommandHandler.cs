using ECommerce.Common.Results;
using ECommerce.Identity.Domain.Entities;
using ECommerce.Identity.Domain.Repositories;
using Mapster;
using MediatR;

namespace ECommerce.Identity.Application.Features.Roles.Commands.CreateRole
{
    //Handler
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, Result<RoleDto>>
    {
        private readonly IRoleRepository roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }
        public async Task<Result<RoleDto>> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var role = request.Adapt<Role>();
            var existingRole = await roleRepository.ExistsByIdAsync(role.Id, cancellationToken);
            if (existingRole)
            {
                return Result<RoleDto>.Failure("Role zaten mevcut");
            }
            await roleRepository.AddAsync(role, cancellationToken);
            var roleDto = role.Adapt<RoleDto>();
            return Result<RoleDto>.Success(roleDto);

        }
    }
}
