using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundlesCountQueryHandler : BaseHandler<GetProductBundlesCountQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        public GetProductBundlesCountQueryHandler(
            IProductBundleQueryService queryService,
            ILogger<GetProductBundlesCountQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _queryService.GetBundlesCountAsync(cancellationToken);
            return new { TotalCount = count };
        }
    }

}
