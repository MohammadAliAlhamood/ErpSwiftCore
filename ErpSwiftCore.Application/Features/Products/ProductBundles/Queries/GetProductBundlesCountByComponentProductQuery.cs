using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundlesCountByComponentProductQuery : IRequest<APIResponseDto>
    {
        public Guid ComponentProductId { get; }
        public GetProductBundlesCountByComponentProductQuery(Guid componentProductId)
        {
            ComponentProductId = componentProductId;
        }
    }

}
