namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{
    public class IndexViewModel
    {
        public Guid InvoiceId { get; set; }
        public int TaxesCount { get; set; }
        public int DiscountsCount { get; set; }
        public decimal? TotalTaxes { get; set; }
        public decimal? TotalDiscounts { get; set; }
    }

}
