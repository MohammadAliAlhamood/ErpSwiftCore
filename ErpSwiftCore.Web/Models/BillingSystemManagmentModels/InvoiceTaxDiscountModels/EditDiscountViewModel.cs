namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    public class EditDiscountViewModel
    {
        public Guid InvoiceId { get; set; }
        public CreateInvoiceDiscountDto CreateDto { get; set; } = new CreateInvoiceDiscountDto();
        public UpdateInvoiceDiscountDto UpdateDto { get; set; }
        public bool IsEdit => UpdateDto != null && UpdateDto.Id != Guid.Empty;
    }


}
