using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class RestoreProductPriceCommand : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public RestoreProductPriceCommand(Guid priceId) => PriceId = priceId;
    }


}
