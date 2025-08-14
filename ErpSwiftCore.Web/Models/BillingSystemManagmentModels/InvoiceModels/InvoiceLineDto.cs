using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class InvoiceLineDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
    }

}
