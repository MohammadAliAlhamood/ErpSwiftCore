using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{
    public class GetProductBySkuQuery : IRequest<APIResponseDto>
    {
        public string SKU { get; }
        public GetProductBySkuQuery(string sku) => SKU = sku;
    }
}
