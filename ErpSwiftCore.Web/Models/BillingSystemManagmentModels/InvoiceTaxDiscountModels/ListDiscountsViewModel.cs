using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    public class ListDiscountsViewModel
    {
        public Guid InvoiceId { get; set; }
        public IEnumerable<InvoiceDiscountDto> Discounts { get; set; } = Enumerable.Empty<InvoiceDiscountDto>();
    } 
}
