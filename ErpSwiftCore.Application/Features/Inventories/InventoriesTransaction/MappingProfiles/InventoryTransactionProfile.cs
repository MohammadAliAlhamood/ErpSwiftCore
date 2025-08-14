using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.MappingProfiles
{
    public class InventoryTransactionProfile : Profile
    {
        public InventoryTransactionProfile()
        {
          
            // InventoryTransaction ↔ InventoryTransactionDto
            CreateMap<InventoryTransaction, InventoryTransactionDto>().ReverseMap();
             

                    }
    }
}