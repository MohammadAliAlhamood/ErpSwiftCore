using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundlesCountByComponentProductQueryHandler : BaseHandler<GetProductBundlesCountByComponentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        public GetProductBundlesCountByComponentProductQueryHandler(
            IProductBundleQueryService queryService,
            ILogger<GetProductBundlesCountByComponentProductQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesCountByComponentProductQuery request, CancellationToken cancellationToken)
        {
            var count = await _queryService.GetBundlesCountByComponentProductAsync(request.ComponentProductId, cancellationToken);
            return new { CountByComponent = count };
        }
    }

}
