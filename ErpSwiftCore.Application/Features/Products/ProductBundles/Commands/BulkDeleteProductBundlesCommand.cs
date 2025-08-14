using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class BulkDeleteProductBundlesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public BulkDeleteProductBundlesCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    }
}



