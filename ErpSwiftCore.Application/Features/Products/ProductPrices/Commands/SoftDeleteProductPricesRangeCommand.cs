using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class SoftDeleteProductPricesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> PriceIds { get; }
        public SoftDeleteProductPricesRangeCommand(IEnumerable<Guid> priceIds) => PriceIds = priceIds;
    }

}
