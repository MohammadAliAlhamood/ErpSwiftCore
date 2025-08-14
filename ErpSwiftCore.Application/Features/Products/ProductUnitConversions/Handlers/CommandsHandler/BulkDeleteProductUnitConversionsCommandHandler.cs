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

    public class BulkDeleteProductUnitConversionsCommandHandler
        : BaseHandler<BulkDeleteProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public BulkDeleteProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<BulkDeleteProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var count = await _svc.BulkDeleteUnitConversionsAsync(request.ConversionIds, ct);
            return new { DeletedCount = count };
        }
    }


}
