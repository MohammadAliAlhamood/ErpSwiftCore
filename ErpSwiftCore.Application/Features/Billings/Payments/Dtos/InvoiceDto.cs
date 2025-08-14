using ErpSwiftCore.Application.Dtos; 
using ErpSwiftCore.Domain.Enums; 

namespace ErpSwiftCore.Application.Features.Billings.Payments.Dtos
{
    // ============================
    // Query DTOs
    // ============================

    

    public class InvoiceSummaryDto
    {
        public Guid InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
    }

     

    public class InvoiceTaxDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string TaxName { get; set; } = null!;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }

    public class InvoiceDiscountDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string DiscountName { get; set; } = null!;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }
    public class AddInvoiceApprovalDto
    {
        public Guid InvoiceId { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }
    public class AddPaymentDto
    {
        public Guid InvoiceId { get; set; }
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }

    public class CurrencyLinkDto
    {
        public Guid InvoiceId { get; set; }
        public Guid CurrencyId { get; set; }
    }


    public class CountResultDto
    {
        public int Count { get; set; }
    }
    public class InvoiceIdDto
    {
        public Guid InvoiceId { get; set; }
    }
    public class InvoiceLineIdDto
    {
        public Guid LineId { get; set; }
    }
    public class InvoiceApprovalIdDto
    {
        public Guid ApprovalId { get; set; }
    }
    public class PaymentIdDto
    {
        public Guid PaymentId { get; set; }
    }

    public class ExistsResultDto
    {
        public bool Exists { get; set; }
    }

    public class DecimalResultDto
    {
        public decimal Value { get; set; }
    }
    public class BatchDeleteInvoicesDto
    {
        public IEnumerable<Guid> InvoiceIds { get; set; } = new List<Guid>();
    }















}








