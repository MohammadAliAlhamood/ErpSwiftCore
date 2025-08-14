namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos
{
    public class ProductPriceBulkImportDto
    {
        public IEnumerable<ProductPriceCreateDto> Prices { get; set; }
    }

}
