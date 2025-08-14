using ErpSwiftCore.Web.Enums;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{
    public class AddInvoiceApprovalDto
    {
        public Guid InvoiceId { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }

}
