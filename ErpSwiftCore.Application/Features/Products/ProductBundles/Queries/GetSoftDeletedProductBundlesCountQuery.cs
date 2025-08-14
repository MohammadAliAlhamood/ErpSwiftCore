using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{

    public class GetSoftDeletedProductBundlesCountQuery 
        : IRequest<APIResponseDto>
    {
        public GetSoftDeletedProductBundlesCountQuery() { }
    }

}
