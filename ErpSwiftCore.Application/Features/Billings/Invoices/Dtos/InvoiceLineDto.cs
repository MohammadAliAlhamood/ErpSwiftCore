using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class InvoiceLineDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
    }

}
