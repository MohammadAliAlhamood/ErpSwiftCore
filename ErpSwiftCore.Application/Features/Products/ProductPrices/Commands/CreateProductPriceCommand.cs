using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Commands
{
    public class CreateProductPriceCommand : IRequest<APIResponseDto>
    {
        public ProductPriceCreateDto Price { get; }
        public CreateProductPriceCommand(ProductPriceCreateDto price) => Price = price;
    } 
}