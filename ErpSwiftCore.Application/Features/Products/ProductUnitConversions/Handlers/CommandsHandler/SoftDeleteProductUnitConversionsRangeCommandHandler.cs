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

    public class SoftDeleteProductUnitConversionsRangeCommandHandler
        : BaseHandler<SoftDeleteProductUnitConversionsRangeCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public SoftDeleteProductUnitConversionsRangeCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<SoftDeleteProductUnitConversionsRangeCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SoftDeleteProductUnitConversionsRangeCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteUnitConversionsRangeAsync(request.ConversionIds, ct);
            return new { SoftDeletedCount = ok };
        }
    }

}
