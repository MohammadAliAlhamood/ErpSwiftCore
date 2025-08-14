using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundlesByComponentProductQuery 
               : IRequest<APIResponseDto>
    {
        public Guid ComponentProductId { get; }
        public GetProductBundlesByComponentProductQuery
            (Guid componentProductId)
        {
            ComponentProductId = componentProductId;
        }
    } 
}
