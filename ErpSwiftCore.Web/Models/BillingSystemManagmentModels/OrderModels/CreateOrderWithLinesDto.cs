namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{
    public class CreateOrderWithLinesDto
    {
        public CreateOrderDto Order { get; set; } = default!;
        public IEnumerable<CreateOrderLineDto> OrderLines { get; set; } = default!;
    }
}