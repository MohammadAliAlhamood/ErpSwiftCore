using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{

    public class RestoreProductUnitConversionsRangeCommandHandler
        : BaseHandler<RestoreProductUnitConversionsRangeCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public RestoreProductUnitConversionsRangeCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<RestoreProductUnitConversionsRangeCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreProductUnitConversionsRangeCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreUnitConversionsRangeAsync(request.ConversionIds, ct);
            return new { RestoredCount = ok };
        }
    }

}
