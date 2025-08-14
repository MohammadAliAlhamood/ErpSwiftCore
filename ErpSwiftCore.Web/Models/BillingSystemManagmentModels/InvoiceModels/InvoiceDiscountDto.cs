using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class InvoiceDiscountDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }

        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }

}
