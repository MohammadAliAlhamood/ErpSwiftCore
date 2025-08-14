using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundleWithUnitQuery : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetProductBundleWithUnitQuery(Guid bundleId)
        {
            BundleId = bundleId;
        }
    }
}
