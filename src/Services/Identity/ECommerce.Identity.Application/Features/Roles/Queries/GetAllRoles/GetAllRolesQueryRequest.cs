using ECommerce.Common.Results;
using ECommerce.Identity.Application.Features.Roles.Commands.CreateRole;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQueryRequest : IRequest<Result<List<RoleDto>>>
    {
        //PageNumber
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        //PageSize
    }


}
