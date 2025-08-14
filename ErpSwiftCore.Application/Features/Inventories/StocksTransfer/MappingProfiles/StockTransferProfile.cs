using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.MappingProfiles
{
    public class StockTransferProfile : Profile
    {
        public StockTransferProfile()
        {
            // Entity → DTO
            CreateMap<StockTransfer, StockTransferDto>()
                .ReverseMap();

            // With product details
            CreateMap<StockTransfer, StockTransferWithProductDto>()
                .IncludeBase<StockTransfer, StockTransferDto>()
                .ForMember(d => d.Product,
                           o => o.MapFrom(s => s.Product));

            // With warehouse details
            CreateMap<StockTransfer, StockTransferWithWarehousesDto>()
                .IncludeBase<StockTransfer, StockTransferDto>()
                .ForMember(d => d.FromWarehouse,
                           o => o.MapFrom(s => s.FromWarehouse))
                .ForMember(d => d.ToWarehouse,
                           o => o.MapFrom(s => s.ToWarehouse));

            // Full detail: product + both warehouses
            CreateMap<StockTransfer, StockTransferFullDetailDto>()
                .IncludeBase<StockTransfer, StockTransferDto>()
                .ForMember(d => d.Product,
                           o => o.MapFrom(s => s.Product))
                .ForMember(d => d.FromWarehouse,
                           o => o.MapFrom(s => s.FromWarehouse))
                .ForMember(d => d.ToWarehouse,
                           o => o.MapFrom(s => s.ToWarehouse));

             

            // Last transfer
            CreateMap<StockTransfer, LastStockTransferDto>()
                .ConvertUsing((src, dst, ctx) => new LastStockTransferDto
                {
                    LastTransfer = ctx.Mapper.Map<StockTransferDto>(src)
                });

            // Create/Update DTO → Entity
            CreateMap<CreateStockTransferDto, StockTransfer>().ReverseMap();

            CreateMap<UpdateStockTransferDto, StockTransfer>().ReverseMap(); 
        }
    }
}