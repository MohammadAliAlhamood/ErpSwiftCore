using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{



    public class CreateInvoiceDto
    {
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid OrderId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Guid CurrencyId { get; set; }
        public IEnumerable<CreateInvoiceLineDto>? Lines { get; set; }
    }

}
