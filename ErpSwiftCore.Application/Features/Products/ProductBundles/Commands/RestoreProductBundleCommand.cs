using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class RestoreProductBundleCommand : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public RestoreProductBundleCommand(Guid bundleId)
        {
            BundleId = bundleId;
        }
    }
}
