using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPriceByIdQuery : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public GetPriceByIdQuery(Guid priceId) => PriceId = priceId;
    } 
}
