namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    public class EditTaxViewModel
    {
        public Guid InvoiceId { get; set; }
        public CreateInvoiceTaxDto CreateDto { get; set; } = new CreateInvoiceTaxDto();
        public UpdateInvoiceTaxDto UpdateDto { get; set; }
        public bool IsEdit => UpdateDto != null && UpdateDto.Id != Guid.Empty;
    }

}
