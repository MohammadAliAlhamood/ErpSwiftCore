using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{ 
    public class InvoiceTaxDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }

        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    } 
}
