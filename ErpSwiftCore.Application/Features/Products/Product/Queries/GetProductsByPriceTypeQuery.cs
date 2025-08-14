using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByPriceTypeQuery : IRequest<APIResponseDto>
    {
        public string PriceType { get; }
        public GetProductsByPriceTypeQuery(string priceType) => PriceType = priceType;
    } 
}
