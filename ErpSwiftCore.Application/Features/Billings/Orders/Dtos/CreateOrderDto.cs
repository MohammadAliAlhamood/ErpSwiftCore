using ErpSwiftCore.Domain.Enums; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{
    /// <summary>
    /// بيانات إنشاء طلب مع أسطره
    /// </summary>
    public class CreateOrderDto
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderType OrderType { get; set; }

        // ربط بالطرف الموحد
        public Guid PartyId { get; set; }

        public Guid CurrencyId { get; set; }
        public IEnumerable<CreateOrderLineDto>? OrderLines { get; set; }
    }
}
