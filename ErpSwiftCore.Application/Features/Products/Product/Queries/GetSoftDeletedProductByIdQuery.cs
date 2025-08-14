using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{
    public class GetSoftDeletedProductByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetSoftDeletedProductByIdQuery(Guid productId) => ProductId = productId;
    }
}
