using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetAllProductBundlesQuery : IRequest<APIResponseDto>
    {
        public GetAllProductBundlesQuery() { }
    }
}
