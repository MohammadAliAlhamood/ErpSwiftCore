using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{ 
    public class SoftDeleteAllProductBundlesCommand : 
        IRequest<APIResponseDto>
    {
        public SoftDeleteAllProductBundlesCommand() { }
    } 
}
