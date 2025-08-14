using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class AddCategoriesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductCategoryCreateDto> Categories { get; }

        public AddCategoriesRangeCommand(IEnumerable<ProductCategoryCreateDto> categories)
        {
            Categories = categories;
        }
    } 
}
