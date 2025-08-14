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

    public class SoftDeleteAllProductUnitConversionsCommandHandler
        : BaseHandler<SoftDeleteAllProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public SoftDeleteAllProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<SoftDeleteAllProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SoftDeleteAllProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.SoftDeleteAllUnitConversionsAsync(ct);
            return new { SoftDeletedAll = ok };
        }
    }

}
