using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Identity.Domain.Repositories;
using Mapster;
using MediatR;

namespace ECommerce.Identity.Application.Features.Roles.Queries.GetAllRoles
{
    //Create Handler:

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, Result<List<RoleDto>>>
    {
        private readonly IRoleRepository roleRepository;
        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }
        public async Task<Result<List<RoleDto>>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            var roles = await roleRepository.GetAllAsync(skip, request.PageSize, cancellationToken);
            if (roles == null || roles.Count == 0)
            {
                return Result<List<RoleDto>>.Failure("Herhangi bir rol bulunamadı!");
            }
            var roleDtos = roles.Adapt<List<RoleDto>>();
            return Result<List<RoleDto>>.Success(roleDtos);

        }
    }


}
