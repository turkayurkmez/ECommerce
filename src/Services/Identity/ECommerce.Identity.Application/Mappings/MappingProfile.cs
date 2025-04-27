using ECommerce.Identity.Application.Features.Auth.Commands.RegisterUser;
using ECommerce.Identity.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Mappings
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //User -> UserDto
            config.NewConfig<User, UserDto>()
                .Map(dest => dest.Roles, src => src.Roles.Select(r => r.Role.Name).ToList());

            //Role -> RoleDto
            //config.NewConfig<Role, RoleDto>();

            //User -> UserProfileDto
            //config.NewConfig<User, UserProfileDto>();
               


        }
    }
}
