using ErpSwiftCore.Web.Enums;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{ 
    public class CreateInvoiceApprovalDto
    {
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    } 
}
