using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    // Bulk soft delete
    public class BulkSoftDeleteProductBundlesCommand 
        : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public BulkSoftDeleteProductBundlesCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    } 
}
