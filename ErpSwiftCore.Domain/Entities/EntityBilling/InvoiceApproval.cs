using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class InvoiceApproval : AuditableEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }
}