namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{ 
    public class UpdateInvoiceDiscountDto
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    } 
}
