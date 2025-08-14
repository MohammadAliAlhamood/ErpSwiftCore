using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.QueriesHandler
{
    public class GetProductUnitConversionsCountQueryHandler
      : BaseHandler<GetProductUnitConversionsCountQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        public GetProductUnitConversionsCountQueryHandler(
            IProductUnitConversionQueryService svc,
            ILogger<GetProductUnitConversionsCountQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductUnitConversionsCountQuery request,
            CancellationToken ct)
        {
            var count = await _svc.GetUnitConversionsCountAsync(ct);
            return count;
        }
    }
}
