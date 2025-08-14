using MediatR;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class DeleteProductBundlesRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public DeleteProductBundlesRangeCommand(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    }
}
