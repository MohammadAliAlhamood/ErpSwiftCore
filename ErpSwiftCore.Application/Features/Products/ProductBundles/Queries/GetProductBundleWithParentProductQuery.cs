using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    // Relations
    public class GetProductBundleWithParentProductQuery 
        : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetProductBundleWithParentProductQuery(Guid bundleId)
        {
            BundleId = bundleId;
        }
    }
}
