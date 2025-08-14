using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetSoftDeletedProductBundlesQuery :
        IRequest<APIResponseDto>
    {
        public GetSoftDeletedProductBundlesQuery() { }
    } 
}
