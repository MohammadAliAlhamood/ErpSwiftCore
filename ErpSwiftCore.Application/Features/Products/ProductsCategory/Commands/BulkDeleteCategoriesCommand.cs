using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands
{
    public class BulkDeleteCategoriesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CategoryIds { get; }
        public BulkDeleteCategoriesCommand(IEnumerable<Guid> categoryIds)
        {
            CategoryIds = categoryIds;
        }
    }
}
