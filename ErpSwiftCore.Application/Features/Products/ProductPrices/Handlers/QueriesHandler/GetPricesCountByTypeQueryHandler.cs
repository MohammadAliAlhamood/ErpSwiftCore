using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesCountByTypeQueryHandler : BaseHandler<GetPricesCountByTypeQuery>
    {
        private readonly IProductPriceQueryService _svc;

        public GetPricesCountByTypeQueryHandler(
            IProductPriceQueryService svc,
            ILogger<BaseHandler<GetPricesCountByTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesCountByTypeQuery req, CancellationToken ct)
        {
            return await _svc.GetPricesCountByTypeAsync(req.PriceType, ct);
        }
    }



}
