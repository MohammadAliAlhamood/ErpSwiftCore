using MediatR;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    // Restore range
    public class RestoreProductBundlesRangeCommand
        : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public RestoreProductBundlesRangeCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    }
}
