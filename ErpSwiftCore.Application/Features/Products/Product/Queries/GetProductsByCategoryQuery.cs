using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByCategoryQuery : IRequest<APIResponseDto>
    {
        public Guid CategoryId { get; }
        public GetProductsByCategoryQuery(Guid categoryId) => CategoryId = categoryId;
    }
}
