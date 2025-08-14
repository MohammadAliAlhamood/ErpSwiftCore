using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundleWithComponentProductQuery 
                : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetProductBundleWithComponentProductQuery(Guid bundleId)
        {
            BundleId = bundleId;
        }
    } 
}
