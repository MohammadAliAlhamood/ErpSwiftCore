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

    public class BulkRestoreProductUnitConversionsCommandHandler
        : BaseHandler<BulkRestoreProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public BulkRestoreProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<BulkRestoreProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkRestoreProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var count = await _svc.BulkRestoreUnitConversionsAsync(request.ConversionIds, ct);
            return new { RestoredCount = count };
        }
    }

}
