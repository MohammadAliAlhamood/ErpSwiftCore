using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class RestoreProductPricesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> PriceIds { get; }
        public RestoreProductPricesRangeCommand(IEnumerable<Guid> priceIds) => PriceIds = priceIds;
    }
}
