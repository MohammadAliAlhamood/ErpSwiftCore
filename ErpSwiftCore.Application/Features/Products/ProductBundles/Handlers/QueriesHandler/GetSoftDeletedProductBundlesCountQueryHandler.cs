using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetSoftDeletedProductBundlesCountQueryHandler : BaseHandler<GetSoftDeletedProductBundlesCountQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        public GetSoftDeletedProductBundlesCountQueryHandler(
            IProductBundleQueryService queryService,
            ILogger<GetSoftDeletedProductBundlesCountQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
        }
        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductBundlesCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _queryService.GetSoftDeletedBundlesCountAsync(cancellationToken);
            return new { SoftDeletedCount = count };
        }
    }

}
