using ErpSwiftCore.Web.Enums; 
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies; 
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
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
        public OrderDto Order { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; } 
        public ICollection<InvoiceLineDto> Lines { get; set; } = new List<InvoiceLineDto>();
        public ICollection<InvoiceTaxDto> Taxes { get; set; } = new List<InvoiceTaxDto>();
        public ICollection<InvoiceDiscountDto> Discounts { get; set; } = new List<InvoiceDiscountDto>();
        public ICollection<InvoiceApprovalDto> Approvals { get; set; } = new List<InvoiceApprovalDto>();
        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    } 
}