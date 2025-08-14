using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.MappingProfiles
{
    public class InventoryPolicyProfile : Profile
    {
        public InventoryPolicyProfile()
        {
            // Entity ↔ DTO
            CreateMap<InventoryPolicy, InventoryPolicyDto>() .ReverseMap();

            // Create/Update full policy
            CreateMap<UpdatePolicyDto, InventoryPolicy>().ReverseMap();

            // Enable/Disable auto-reorder commands map only DTO → primitive values, 
            // handled directly in handlers—no mapping to entity needed.

            // Update reorder level only
            CreateMap<UpdateReorderLevelDto, InventoryPolicy>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.PolicyId))
                .ForMember(d => d.ReorderLevel, o => o.MapFrom(s => s.ReorderLevel));

            // Update max stock level only
            CreateMap<UpdateMaxStockLevelDto, InventoryPolicy>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.PolicyId))
                .ForMember(d => d.MaxStockLevel, o => o.MapFrom(s => s.MaxStockLevel));

            // Batch update policies
            CreateMap<UpdatePoliciesRangeDto, IEnumerable<InventoryPolicy>>()
                .ConvertUsing((src, dest, ctx) =>
                    src.Policies.Select(dto => ctx.Mapper.Map<InventoryPolicy>(dto))
                );
        }
    }
}