using MediatR;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    // Restore all
    public class RestoreAllProductBundlesCommand : IRequest<APIResponseDto>
    {
        public RestoreAllProductBundlesCommand() { }
    }
}
