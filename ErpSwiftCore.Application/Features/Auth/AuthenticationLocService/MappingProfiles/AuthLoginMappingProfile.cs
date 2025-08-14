using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.MappingProfiles
{
    public class AuthLoginMappingProfile : Profile
    {
        public AuthLoginMappingProfile()
        {
            // من RegisterRequestDto إلى ApplicationUser
            CreateMap<RegisterRequestDto, ApplicationUser>().ReverseMap();

            // من (ApplicationUser + token) إلى LoginResponseDto
            CreateMap<(ApplicationUser user, string token), LoginResponseDto>().ReverseMap();
        }
    }
}