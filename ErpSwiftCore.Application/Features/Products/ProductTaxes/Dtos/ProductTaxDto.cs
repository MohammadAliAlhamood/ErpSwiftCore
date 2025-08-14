using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos; 

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos
{
    public class ProductTaxDto : AuditableEntityDto
    {
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public decimal Rate { get; set; }
    }
}
