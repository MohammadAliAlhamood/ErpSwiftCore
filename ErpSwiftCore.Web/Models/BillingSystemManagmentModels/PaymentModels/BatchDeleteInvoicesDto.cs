namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{
    public class BatchDeleteInvoicesDto
    {
        public IEnumerable<Guid> InvoiceIds { get; set; } = new List<Guid>();
    }

}
