using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductWithBundlesQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductWithBundlesQuery(Guid productId) => ProductId = productId;
    }


}
