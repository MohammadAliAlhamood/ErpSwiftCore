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

    public class GetSoftDeletedProductUnitConversionsCountQueryHandler
        : BaseHandler<GetSoftDeletedProductUnitConversionsCountQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        public GetSoftDeletedProductUnitConversionsCountQueryHandler(
            IProductUnitConversionQueryService svc,
            ILogger<GetSoftDeletedProductUnitConversionsCountQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedProductUnitConversionsCountQuery request,
            CancellationToken ct)
        {
            var count = await _svc.GetSoftDeletedUnitConversionsCountAsync(ct);
            return count;
        }
    }


}
