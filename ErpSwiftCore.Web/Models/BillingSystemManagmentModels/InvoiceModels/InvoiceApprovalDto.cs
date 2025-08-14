using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{ 
    public class InvoiceApprovalDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }

        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    } 
}
