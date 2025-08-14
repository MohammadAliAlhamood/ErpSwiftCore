using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class UpdateProductPriceCommand : IRequest<APIResponseDto>
    {
        public ProductPriceUpdateDto Price { get; }
        public UpdateProductPriceCommand(ProductPriceUpdateDto price) => Price = price;
    } 
}
