namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{ 
    public class CreateOrderLineDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
