namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{
    public class UpdateInvoiceLineDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }


}
