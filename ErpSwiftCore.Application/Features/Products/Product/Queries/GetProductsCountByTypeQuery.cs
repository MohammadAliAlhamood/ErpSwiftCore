using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsCountByTypeQuery : IRequest<APIResponseDto>
    {
        public Guid ProductTypeId { get; }
        public GetProductsCountByTypeQuery(Guid productTypeId) => ProductTypeId = productTypeId;
    } 
}
