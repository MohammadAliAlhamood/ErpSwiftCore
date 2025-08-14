using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{
    public class GetProductByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductByIdQuery(Guid productId) => ProductId = productId;
    }
}