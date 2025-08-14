using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class Payment : AuditableEntity
    {
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public bool IsReconciled { get; set; }
    }
}