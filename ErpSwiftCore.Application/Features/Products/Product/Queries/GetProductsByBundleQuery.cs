using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{
    public class GetProductsByBundleQuery : IRequest<APIResponseDto>
    {
        public Guid BundleId { get; }
        public GetProductsByBundleQuery(Guid bundleId) => BundleId = bundleId;
    } 
}
