using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundlesCountByParentProductQueryHandler : BaseHandler<GetProductBundlesCountByParentProductQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        public GetProductBundlesCountByParentProductQueryHandler(
            IProductBundleQueryService queryService,
            ILogger<GetProductBundlesCountByParentProductQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesCountByParentProductQuery request, CancellationToken cancellationToken)
        {
            var count = await _queryService.GetBundlesCountByParentProductAsync(request.ParentProductId, cancellationToken);
            return new { CountByParent = count };
        }
    }


}
