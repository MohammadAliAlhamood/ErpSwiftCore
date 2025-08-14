namespace ErpSwiftCore.Application.Features.Products.Product.Dtos
{ 
    public class ProductStockUpdateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    } 
}
