using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{ 
    public class UpdateInvoiceApprovalDto
    {
        public Guid Id { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    } 
}
