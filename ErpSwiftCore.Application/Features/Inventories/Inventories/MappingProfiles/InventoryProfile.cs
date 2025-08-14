using AutoMapper;
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Inventories.MappingProfiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            // Base mapping Inventory <-> InventoryDto
            CreateMap<Inventory, InventoryDto>()
                .ReverseMap();

            // Inventory with Policy
            CreateMap<Inventory, InventoryWithPolicyDto>()
                .IncludeBase<Inventory, InventoryDto>()
                .ForMember(d => d.Policy,
                           o => o.MapFrom(src => src.Policy));

            // Inventory with Transactions
            CreateMap<Inventory, InventoryWithTransactionsDto>()
                .IncludeBase<Inventory, InventoryDto>()
                .ForMember(d => d.Transactions,
                           o => o.MapFrom(src => src.Transactions));

            // Inventory with Notifications
            CreateMap<Inventory, InventoryWithNotificationsDto>()
                .IncludeBase<Inventory, InventoryDto>();

            // Full detail: Inventory + Policy + Transactions + Notifications
            CreateMap<Inventory, InventoryFullDetailDto>()
                .IncludeBase<Inventory, InventoryDto>()
                .ForMember(d => d.Policy,
                           o => o.MapFrom(src => src.Policy))
                .ForMember(d => d.Transactions,
                           o => o.MapFrom(src => src.Transactions));

            // Nested mappings
            CreateMap<InventoryPolicy, InventoryPolicyDto>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
             
 
 
 
        }
    }
}