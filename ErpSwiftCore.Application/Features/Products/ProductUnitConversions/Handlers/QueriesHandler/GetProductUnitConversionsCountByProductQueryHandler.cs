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

    public class GetProductUnitConversionsCountByProductQueryHandler
        : BaseHandler<GetProductUnitConversionsCountByProductQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        public GetProductUnitConversionsCountByProductQueryHandler(
            IProductUnitConversionQueryService svc,
            ILogger<GetProductUnitConversionsCountByProductQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductUnitConversionsCountByProductQuery request,
            CancellationToken ct)
        {
            var count = await _svc.GetUnitConversionsCountByProductAsync(request.ProductId, ct);
            return count;
        }
    }

}
