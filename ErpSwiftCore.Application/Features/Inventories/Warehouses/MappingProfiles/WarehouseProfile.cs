using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.MappingProfiles
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            // Entity → DTO
            CreateMap<Warehouse, WarehouseDto>()  .ReverseMap();

            CreateMap<Warehouse, WarehouseWithBranchDto>()
                .IncludeBase<Warehouse, WarehouseDto>()
                .ForMember(d => d.Branch, o => o.MapFrom(s => s.Branch));

            CreateMap<Warehouse, WarehouseWithInventoriesDto>()
                .IncludeBase<Warehouse, WarehouseDto>()
                .ForMember(d => d.Inventories, o => o.MapFrom(s => s.Inventories));

          

            // RecentWarehouseDto reuses WarehouseDto mapping
            CreateMap<Warehouse, RecentWarehouseDto>()
                .IncludeBase<Warehouse, WarehouseDto>();
             
            CreateMap<CreateWarehouseDto, Warehouse>().ReverseMap();

            CreateMap<UpdateWarehouseDto, Warehouse>().ReverseMap();


        }
    }
}