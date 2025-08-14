using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class CreateProductCommand : IRequest<APIResponseDto>
    {
        public ProductCreateDto Product { get; }
        public CreateProductCommand(ProductCreateDto product) => Product = product;
    } 
}