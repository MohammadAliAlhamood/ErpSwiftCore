using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.MappingProfiles
{
    public class CostCenterProfile : Profile
    {
        public CostCenterProfile()
        {
            // Entity ↔ DTO
            CreateMap<CostCenter, CostCenterDto>().ReverseMap();
            CreateMap<CostCenter, CreateCostCenterDto>().ReverseMap();
            CreateMap<CostCenter, UpdateCostCenterDto>().ReverseMap();
        }
    }
}
