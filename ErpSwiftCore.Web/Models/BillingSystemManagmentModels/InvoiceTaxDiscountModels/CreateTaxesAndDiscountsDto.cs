namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{

    public class CreateTaxesAndDiscountsDto
    {
        public Guid InvoiceId { get; set; }
        public IEnumerable<CreateInvoiceTaxDto> Taxes { get; set; } = new List<CreateInvoiceTaxDto>();
        public IEnumerable<CreateInvoiceDiscountDto> Discounts { get; set; } = new List<CreateInvoiceDiscountDto>();
    }


}
