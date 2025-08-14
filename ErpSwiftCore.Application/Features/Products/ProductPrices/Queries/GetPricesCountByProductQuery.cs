using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesCountByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetPricesCountByProductQuery(Guid productId) 
            => ProductId = productId;
    }
}
