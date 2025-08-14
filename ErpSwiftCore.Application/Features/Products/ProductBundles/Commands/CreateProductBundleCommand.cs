using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Commands
{
    public class CreateProductBundleCommand : IRequest<APIResponseDto>
    {
        public ProductBundleCreateDto Bundle { get; }
        public CreateProductBundleCommand
            (ProductBundleCreateDto bundle)
        {
            Bundle = bundle;
        }
    } 
}