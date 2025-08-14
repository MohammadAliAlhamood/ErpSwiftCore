namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class BulkDeleteInvoicesDto
    {
        public IEnumerable<Guid> InvoiceIds { get; set; } = new List<Guid>();
    }

}
