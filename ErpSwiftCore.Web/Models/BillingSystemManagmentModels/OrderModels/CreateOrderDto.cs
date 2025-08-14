using ErpSwiftCore.Web.Enums;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{ 
    public class CreateOrderDto
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderType OrderType { get; set; } 
        public Guid PartyId { get; set; } 
        public Guid CurrencyId { get; set; }
        public IEnumerable<CreateOrderLineDto>? OrderLines { get; set; }
    }
}
