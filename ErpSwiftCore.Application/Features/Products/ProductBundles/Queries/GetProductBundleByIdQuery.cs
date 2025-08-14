using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    // Single retrieval
    public class GetProductBundleByIdQuery : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetProductBundleByIdQuery(Guid bundleId)
        {
            BundleId = bundleId;
        }
    } 
}