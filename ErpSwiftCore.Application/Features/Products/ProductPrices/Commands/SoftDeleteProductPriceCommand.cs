using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class SoftDeleteProductPriceCommand : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public SoftDeleteProductPriceCommand(Guid priceId) => PriceId = priceId;
    }


}
