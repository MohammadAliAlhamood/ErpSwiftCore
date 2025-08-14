using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    // Counts & Stats
    public class GetProductBundlesCountQuery : IRequest<APIResponseDto>
    {
        public GetProductBundlesCountQuery() { }
    }

}
