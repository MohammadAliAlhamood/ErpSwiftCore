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
    public class BulkSoftDeleteProductUnitConversionsCommandHandler
       : BaseHandler<BulkSoftDeleteProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public BulkSoftDeleteProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<BulkSoftDeleteProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkSoftDeleteProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var count = await _svc.BulkSoftDeleteUnitConversionsAsync(request.ConversionIds, ct);
            return new { SoftDeletedCount = count };
        }
    }




}
