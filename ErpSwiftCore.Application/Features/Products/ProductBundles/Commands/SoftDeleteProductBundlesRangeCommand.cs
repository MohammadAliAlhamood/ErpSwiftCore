using MediatR;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class SoftDeleteProductBundlesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public SoftDeleteProductBundlesRangeCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    }
}
