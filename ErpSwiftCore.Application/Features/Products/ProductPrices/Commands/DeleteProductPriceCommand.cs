using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class DeleteProductPriceCommand : IRequest<APIResponseDto>
    {
        public Guid PriceId { get; }
        public DeleteProductPriceCommand(Guid priceId) => PriceId = priceId;
    }

}
