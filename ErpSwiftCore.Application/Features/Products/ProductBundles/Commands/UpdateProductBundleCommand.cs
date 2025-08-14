using MediatR;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class UpdateProductBundleCommand : IRequest<APIResponseDto>
    {
        public ProductBundleUpdateDto Bundle { get; }
        public UpdateProductBundleCommand(ProductBundleUpdateDto bundle)
        {
            Bundle = bundle;
        }
    }
}
