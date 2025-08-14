using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Session.MappingProfiles
{
    internal class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<ActivityLog, ActivityLogDto>().ReverseMap();
        }
    }
}