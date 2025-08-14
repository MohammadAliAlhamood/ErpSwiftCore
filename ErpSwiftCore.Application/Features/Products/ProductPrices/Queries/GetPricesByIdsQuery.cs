using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> PriceIds { get; }
        public GetPricesByIdsQuery(IEnumerable<Guid> priceIds) => PriceIds = priceIds;
    }

}
