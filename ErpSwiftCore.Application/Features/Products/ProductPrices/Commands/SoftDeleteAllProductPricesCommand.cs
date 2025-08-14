using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class SoftDeleteAllProductPricesCommand : IRequest<APIResponseDto> { }
}
