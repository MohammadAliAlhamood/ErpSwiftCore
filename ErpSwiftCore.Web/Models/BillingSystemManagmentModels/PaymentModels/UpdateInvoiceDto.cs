using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{
    public class UpdateInvoiceDto
    {
        public Guid Id { get; set; }
        public DateTime? DueDate { get; set; }
        public InvoiceStatus? InvoiceStatus { get; set; }
        public bool? IsFinalized { get; set; }
    }


}
