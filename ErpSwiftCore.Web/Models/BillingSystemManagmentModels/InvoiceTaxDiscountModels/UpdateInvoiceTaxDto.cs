namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels
{

    /// <summary>
    /// بيانات تحديث ضريبة موجودة
    /// </summary>
    public class UpdateInvoiceTaxDto
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }


}
