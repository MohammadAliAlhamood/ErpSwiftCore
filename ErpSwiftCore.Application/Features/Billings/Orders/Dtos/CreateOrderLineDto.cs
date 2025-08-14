namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{ 
    public class CreateOrderLineDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
