using AutoMapper;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;

namespace ErpSwiftCore.Web.Mappings
{
    public class BillingProfile : Profile
    {
        public BillingProfile()
        {
            // ────────────────────────────────────────────────────────────────
            // 1) الخرائط الأساسية بين Order <-> OrderDto
            // ────────────────────────────────────────────────────────────────

            // 2) خريطة إنشاء طلب جديد CreateOrderDto -> Order
            CreateMap<CreateOrderDto, OrderDto>()
                .ReverseMap();

            // 3) خريطة تعديل طلب موجود UpdateOrderDto -> Order
            CreateMap<UpdateOrderDto, OrderDto>()
                .ReverseMap();

            // ────────────────────────────────────────────────────────────────
            // 4) الخرائط الأساسية بين OrderLine <-> OrderLineDto
            // ────────────────────────────────────────────────────────────────

            // 5) خريطة إنشاء سطر طلب جديد CreateOrderLineDto -> OrderLine
            CreateMap<CreateOrderLineDto, OrderLineDto>()
                .ReverseMap();

            // 6) خريطة تعديل سطر طلب موجود UpdateOrderLineDto -> OrderLine
            CreateMap<UpdateOrderLineDto, OrderLineDto>()
                .ReverseMap();



        }
    }
}
