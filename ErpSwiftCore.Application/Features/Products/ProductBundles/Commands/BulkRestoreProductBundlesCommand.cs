using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class BulkRestoreProductBundlesCommand 
        : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public BulkRestoreProductBundlesCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    } 
}
