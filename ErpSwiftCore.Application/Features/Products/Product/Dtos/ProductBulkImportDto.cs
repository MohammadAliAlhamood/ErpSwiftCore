namespace ErpSwiftCore.Application.Features.Products.Product.Dtos
{ 
    public class ProductBulkImportDto
    {
        public IEnumerable<ProductCreateDto> Products { get; set; }
    } 
}
