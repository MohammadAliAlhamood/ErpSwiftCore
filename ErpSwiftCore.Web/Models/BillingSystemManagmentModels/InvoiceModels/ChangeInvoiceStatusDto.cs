using ErpSwiftCore.Web.Enums;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class ChangeInvoiceStatusDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceStatus NewStatus { get; set; }
    }

}
