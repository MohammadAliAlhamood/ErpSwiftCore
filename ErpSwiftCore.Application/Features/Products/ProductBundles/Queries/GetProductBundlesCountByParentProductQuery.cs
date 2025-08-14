using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundlesCountByParentProductQuery 
        : IRequest<APIResponseDto>
    {
        public Guid ParentProductId { get; }
        public GetProductBundlesCountByParentProductQuery(
            Guid parentProductId)
        {
            ParentProductId = parentProductId;
        }
    }

}
