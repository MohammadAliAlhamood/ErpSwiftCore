using MediatR;  
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetLatestPriceByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public string PriceType { get; }
        public GetLatestPriceByProductQuery(Guid productId, string priceType)
        {
            ProductId = productId;
            PriceType = priceType;
        }
    }


}
