using ErpSwiftCore.Application.Dtos;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos
{
 
    public class ProductCategoryDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ProductCategoryDto ParentCategory { get; set; }
     } 
}