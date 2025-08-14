using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class BulkImportCategoriesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductCategoryCreateDto> Categories { get; }

        public BulkImportCategoriesCommand(IEnumerable<ProductCategoryCreateDto> categories)
        {
            Categories = categories;
        }
    }
}
