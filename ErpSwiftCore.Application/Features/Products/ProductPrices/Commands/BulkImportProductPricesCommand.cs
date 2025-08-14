using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class BulkImportProductPricesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductPriceCreateDto> Prices { get; }
        public BulkImportProductPricesCommand(IEnumerable<ProductPriceCreateDto> prices) => Prices = prices;
    }
}
