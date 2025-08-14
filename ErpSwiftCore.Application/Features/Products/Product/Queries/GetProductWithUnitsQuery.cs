using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductWithUnitsQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductWithUnitsQuery(Guid productId) => ProductId = productId;
    } 
}
