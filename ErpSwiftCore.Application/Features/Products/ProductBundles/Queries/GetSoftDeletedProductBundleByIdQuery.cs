using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetSoftDeletedProductBundleByIdQuery : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetSoftDeletedProductBundleByIdQuery(Guid bundleId)
        {
            BundleId = bundleId;
        }
    }


}
