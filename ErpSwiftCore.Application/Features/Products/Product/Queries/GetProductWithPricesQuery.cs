using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductWithPricesQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductWithPricesQuery(Guid productId) => ProductId = productId;
    }



}
