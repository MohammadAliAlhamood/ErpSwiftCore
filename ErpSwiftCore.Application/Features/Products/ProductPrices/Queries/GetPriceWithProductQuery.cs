using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPriceWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public GetPriceWithProductQuery(Guid priceId) => PriceId = priceId;
    }

}
