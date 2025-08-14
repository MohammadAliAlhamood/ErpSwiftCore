using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class DeleteProductBundleCommand : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public DeleteProductBundleCommand(Guid bundleId)
        {
            BundleId = bundleId;
        }
    } 
}
