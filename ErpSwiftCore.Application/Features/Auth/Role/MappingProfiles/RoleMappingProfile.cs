using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Role.MappingProfiles
{
    internal class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            // من string إلى RoleDto
            CreateMap<string, RoleDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
        }
    }
}
