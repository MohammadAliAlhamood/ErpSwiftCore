using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetPricesByProductQuery(Guid productId) => ProductId = productId;
    }


}
