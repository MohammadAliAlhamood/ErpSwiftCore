using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetSoftDeletedPriceByIdQuery : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public GetSoftDeletedPriceByIdQuery(Guid priceId) => PriceId = priceId;
    }

}
