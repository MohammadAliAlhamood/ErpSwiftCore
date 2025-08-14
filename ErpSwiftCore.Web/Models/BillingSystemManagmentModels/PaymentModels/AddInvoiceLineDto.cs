namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{

    public class AddInvoiceLineDto
    {
        public Guid InvoiceId { get; set; }
        public CreateInvoiceLineDto Line { get; set; } = null!;
    }


}
