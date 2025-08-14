using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.MappingProfiles
{
    public class InventoryAdjustmentProfile : Profile
    {
        public InventoryAdjustmentProfile()
        {
            // Entity -> DTO
            CreateMap<InventoryAdjustment, InventoryAdjustmentDto>().ReverseMap();


            // Create DTO -> Entity
            CreateMap<CreateInventoryAdjustmentDto, InventoryAdjustment>().ReverseMap();
            CreateMap<UpdateInventoryAdjustmentDto, InventoryAdjustment>().ReverseMap();
        }
    }
}