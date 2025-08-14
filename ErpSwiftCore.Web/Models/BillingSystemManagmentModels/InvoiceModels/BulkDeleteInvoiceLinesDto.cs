namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{
    public class BulkDeleteInvoiceLinesDto
    {
        public IEnumerable<Guid> LineIds { get; set; } = new List<Guid>();
    }

}
