using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    public class ListTaxesViewModel
    {
        public Guid InvoiceId { get; set; }
        public IEnumerable<InvoiceTaxDto> Taxes { get; set; } 
            = Enumerable.Empty<InvoiceTaxDto>();
    }


}
