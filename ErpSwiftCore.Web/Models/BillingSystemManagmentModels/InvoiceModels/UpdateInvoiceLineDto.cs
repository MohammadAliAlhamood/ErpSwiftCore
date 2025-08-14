namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class UpdateInvoiceLineDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }

}
