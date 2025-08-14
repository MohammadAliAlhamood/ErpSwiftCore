using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class CreateCategoryCommand : IRequest<APIResponseDto>
    {
        public ProductCategoryCreateDto Category { get; }

        public CreateCategoryCommand(ProductCategoryCreateDto category)
        {
            Category = category;
        }
    } 
}