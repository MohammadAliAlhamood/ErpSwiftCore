using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class BulkImportProductBundlesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<ProductBundleCreateDto> Bundles { get; }
        public BulkImportProductBundlesCommand(IEnumerable<ProductBundleCreateDto> bundles)
        {
            Bundles = bundles;
        }
    } 
}
