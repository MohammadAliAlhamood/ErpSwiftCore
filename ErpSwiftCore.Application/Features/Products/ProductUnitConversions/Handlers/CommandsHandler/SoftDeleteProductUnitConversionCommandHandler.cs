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

    public class SoftDeleteProductUnitConversionCommandHandler
        : BaseHandler<SoftDeleteProductUnitConversionCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public SoftDeleteProductUnitConversionCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<SoftDeleteProductUnitConversionCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SoftDeleteProductUnitConversionCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteUnitConversionAsync(request.ConversionId, ct);
            return new { SoftDeleted = ok };
        }
    }

}
