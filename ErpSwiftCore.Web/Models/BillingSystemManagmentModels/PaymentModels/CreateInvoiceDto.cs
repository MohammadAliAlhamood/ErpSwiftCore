using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{

    public class CreateInvoiceDto
    {
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Guid OrderId { get; set; }
        public Guid CurrencyId { get; set; }
        public List<CreateInvoiceLineDto>? Lines { get; set; }
    }

}
