using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using ErpSwiftCore.Domain.Entities.EntityAuth; 
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.MappingProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        { 
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked));
            CreateMap<UpdateProfileRequestDto, ApplicationUser>().ReverseMap();
            CreateMap<UpdateMyProfileRequestDto, ApplicationUser>().ReverseMap();
        }
    }
}