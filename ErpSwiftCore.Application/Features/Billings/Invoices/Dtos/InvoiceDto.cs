using ErpSwiftCore.Application.Dtos; 
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{ 
    public class InvoiceDto : AuditableEntityDto
    {
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public bool IsFinalized { get; set; }
        public Guid OrderId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Guid CurrencyId { get; set; }
    } 
}