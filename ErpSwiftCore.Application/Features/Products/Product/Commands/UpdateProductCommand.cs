using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class UpdateProductCommand : IRequest<APIResponseDto>
    {
        public ProductUpdateDto Product { get; }
        public UpdateProductCommand(ProductUpdateDto product) => Product = product;
    } 
}
