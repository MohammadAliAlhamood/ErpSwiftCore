using ErpSwiftCore.Application.Dtos; 
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos
{
    public class ProductCategoryCreateDto  
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }

}
