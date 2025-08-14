using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class Invoice : AuditableEntity
    {

        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.Draft;
        public bool IsFinalized { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public InvoiceType InvoiceType { get; set; } = InvoiceType.Unknown;
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
        public ICollection<InvoiceTax> Taxes { get; set; } = new List<InvoiceTax>();
        public ICollection<InvoiceDiscount> Discounts { get; set; } = new List<InvoiceDiscount>();
        public ICollection<InvoiceApproval> Approvals { get; set; } = new List<InvoiceApproval>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}