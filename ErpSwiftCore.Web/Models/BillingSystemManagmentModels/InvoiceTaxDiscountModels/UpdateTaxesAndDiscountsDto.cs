namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    /// <summary>
    /// تحديث مجموعة ضرائب وخصومات لفاتورة
    /// </summary>
    public class UpdateTaxesAndDiscountsDto
    {
        public Guid InvoiceId { get; set; }

        public IEnumerable<CreateInvoiceTaxDto>? TaxesToAdd { get; set; }
        public IEnumerable<UpdateInvoiceTaxDto>? TaxesToUpdate { get; set; }
        public IEnumerable<Guid>? TaxIdsToDelete { get; set; }

        public IEnumerable<CreateInvoiceDiscountDto>? DiscountsToAdd { get; set; }
        public IEnumerable<UpdateInvoiceDiscountDto>? DiscountsToUpdate { get; set; }
        public IEnumerable<Guid>? DiscountIdsToDelete { get; set; }
    }

}
