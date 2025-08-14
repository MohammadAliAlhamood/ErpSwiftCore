using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels
{
    public class ProductTaxDto : AuditableEntityDto
    {
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public decimal Rate { get; set; }
    }
}
