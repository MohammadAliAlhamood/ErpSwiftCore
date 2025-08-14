namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{ 
    public class UpdateOrderLineDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
