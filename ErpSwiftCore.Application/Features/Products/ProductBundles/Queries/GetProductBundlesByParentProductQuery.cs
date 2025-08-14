using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{

    public class GetProductBundlesByParentProductQuery : IRequest<APIResponseDto>
    {
        public Guid ParentProductId { get; }
        public GetProductBundlesByParentProductQuery(Guid parentProductId)
        {
            ParentProductId = parentProductId;
        }
    }


}
