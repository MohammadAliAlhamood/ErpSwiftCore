using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesByTypeQuery : IRequest<APIResponseDto>
    {
        public string PriceType { get; }
        public GetPricesByTypeQuery(string priceType) => PriceType = priceType;
    }


}
