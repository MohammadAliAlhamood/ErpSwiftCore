using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class DeleteAllProductBundlesCommand : 
        IRequest<APIResponseDto>
    {
        public DeleteAllProductBundlesCommand() { }
    } 
}
