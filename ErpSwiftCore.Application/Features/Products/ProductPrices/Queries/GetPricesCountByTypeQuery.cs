using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesCountByTypeQuery : IRequest<APIResponseDto>
    {
        public string PriceType { get; }
        public GetPricesCountByTypeQuery(string priceType) => PriceType = priceType;
    }

}
