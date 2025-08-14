using AutoMapper;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;

namespace ErpSwiftCore.Application.Features.Billings.Orders.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // ────────────────────────────────────────────────────────────────
            // 1) الخرائط الأساسية بين Order <-> OrderDto
            // ────────────────────────────────────────────────────────────────
            CreateMap<Order, OrderDto>()
                .ReverseMap();

            // 2) خريطة إنشاء طلب جديد CreateOrderDto -> Order
            CreateMap<CreateOrderDto, Order>()
                .ReverseMap();

            // 3) خريطة تعديل طلب موجود UpdateOrderDto -> Order
            CreateMap<UpdateOrderDto, Order>()
                .ReverseMap();

            // ────────────────────────────────────────────────────────────────
            // 4) الخرائط الأساسية بين OrderLine <-> OrderLineDto
            // ────────────────────────────────────────────────────────────────
            CreateMap<OrderLine, OrderLineDto>()
                .ReverseMap();

            // 5) خريطة إنشاء سطر طلب جديد CreateOrderLineDto -> OrderLine
            CreateMap<CreateOrderLineDto, OrderLine>()
                .ReverseMap();

            // 6) خريطة تعديل سطر طلب موجود UpdateOrderLineDto -> OrderLine
            CreateMap<UpdateOrderLineDto, OrderLine>()
                .ReverseMap();

            
             
        }
    }
}
