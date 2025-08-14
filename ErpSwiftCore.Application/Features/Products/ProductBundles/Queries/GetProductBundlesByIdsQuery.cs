using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundlesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BundleIds { get; }
        public GetProductBundlesByIdsQuery(IEnumerable<Guid> bundleIds)
        {
            BundleIds = bundleIds;
        }
    }


}
