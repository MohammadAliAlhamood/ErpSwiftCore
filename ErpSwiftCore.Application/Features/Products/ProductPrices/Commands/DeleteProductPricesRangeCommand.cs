using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class DeleteProductPricesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> PriceIds { get; }
        public DeleteProductPricesRangeCommand(IEnumerable<Guid> priceIds) => PriceIds = priceIds;
    } 
}
