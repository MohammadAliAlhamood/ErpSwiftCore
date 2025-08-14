namespace ErpSwiftCore.Application.Features.Products.Product.Dtos
{
    public class ProductBulkUpdateStockDto
    {
        public IEnumerable<ProductStockUpdateDto> StockUpdates { get; set; }
    } 
}
